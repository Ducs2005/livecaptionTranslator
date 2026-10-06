using LiveCaptionTranslator.Models;
using LiveCaptionTranslator.Services;
using System.IO;

var checks = new (string Name, Action Check)[]
{
    ("Ready state has no captions or running controls", () =>
    {
        var session = new DemoSession();
        Require(session.IsEmpty && session.CanStart && !session.CanPause && !session.CanStop && !session.CanClear);
        session.Advance();
        Require(session.IsEmpty);
    }),
    ("Start emits a sample once; a repeated Start does not reset", () =>
    {
        var session = Running();
        Require(session.Captions.Count == 1 && !session.CanStart);
        session.Start();
        Require(session.Captions.Count == 1);
    }),
    ("Paused session ignores ticks and resumes without duplicate captions", () =>
    {
        var session = Running();
        session.TogglePause();
        session.Advance();
        Require(session.Captions.Count == 1 && session.PauseLabel == "Tiếp tục");
        session.TogglePause();
        session.Advance();
        Require(session.Captions.Count == 2 && session.Captions[0].Source != session.Captions[1].Source);
    }),
    ("Stop preserves content and ignores late ticks", () =>
    {
        var session = Running();
        session.Stop();
        session.Advance();
        session.TogglePause();
        Require(session.State == SessionState.Stopped && session.Captions.Count == 1 && session.CanStart);
    }),
    ("Target switch changes current caption, preserving historical labels", () =>
    {
        var session = Running();
        session.Advance();
        var first = session.Captions[0].Translation;
        session.SelectedLanguage = session.Languages[1];
        Require(session.Captions[0].Translation == first && session.Captions[0].TargetLabel == "TIẾNG VIỆT");
        Require(session.Captions[1].Translation == session.Captions[1].Source);
        session.Advance();
        Require(session.Captions[2].Translation == session.Captions[2].Source);
    }),
    ("Paused target switch waits until resume", () =>
    {
        var session = Running();
        var original = session.Captions[0].Translation;
        session.TogglePause();
        session.SelectedLanguage = session.Languages[1];
        Require(session.Captions[0].Translation == original);
        session.TogglePause();
        Require(session.Captions[0].Translation == session.Captions[0].Source);
    }),
    ("Clear invalidates a running demo; late ticks cannot restore content", () =>
    {
        var session = Running();
        session.Clear();
        session.Advance();
        Require(session.State == SessionState.Ready && session.IsEmpty && !session.CanClear);
        session.Start();
        Require(session.Captions.Count == 1 && session.Captions[0].Number == "01");
    }),
    ("Finite demo completes and restart resets numbering", () =>
    {
        var session = Running();
        for (var i = 0; i < 10; i++) session.Advance();
        Require(session.State == SessionState.Completed && session.Captions.Count == 6 && !session.CanPause);
        session.Start();
        Require(session.Captions.Count == 1 && session.State == SessionState.Running);
    }),
    ("Unsupported language is rejected", () =>
    {
        var session = new DemoSession();
        session.SelectedLanguage = new TargetLanguage("unknown", "Unsupported");
        Require(session.SelectedLanguage.Code == "vi");
    }),
    ("Presentation controls do not change caption content", () =>
    {
        var session = Running();
        var text = session.Captions[0].Translation;
        session.ShowSource = false;
        session.AlwaysOnTop = true;
        session.CaptionSize = 50;
        Require(!session.ShowSource && session.AlwaysOnTop && session.CaptionSize == 30);
        Require(session.Captions[0].Translation == text);
    }),
    ("Preferences persist locally and restore display settings", () =>
    {
        var path = Path.Combine(Path.GetTempPath(), $"livecaption-preferences-{Guid.NewGuid():N}.json");
        try
        {
            var store = new JsonPreferencesStore(path);
            store.Save(new UserPreferences("en", false, true, 27));
            var restored = new DemoSession(store.Load());
            Require(restored.SelectedLanguage.Code == "en" && !restored.ShowSource);
            Require(restored.AlwaysOnTop && restored.CaptionSize == 27 && restored.IsEmpty);
        }
        finally
        {
            if (File.Exists(path)) File.Delete(path);
        }
    }),
    ("Corrupt preferences safely fall back to defaults", () =>
    {
        var path = Path.Combine(Path.GetTempPath(), $"livecaption-preferences-{Guid.NewGuid():N}.json");
        try
        {
            File.WriteAllText(path, "{ invalid json }");
            var restored = new DemoSession(new JsonPreferencesStore(path).Load());
            Require(restored.SelectedLanguage.Code == "vi" && restored.ShowSource);
            Require(!restored.AlwaysOnTop && restored.CaptionSize == 22 && restored.IsEmpty);
        }
        finally
        {
            if (File.Exists(path)) File.Delete(path);
        }
    }),
    ("Persisted preferences clamp caption size and reject unknown languages", () =>
    {
        var restored = new DemoSession(new UserPreferences("xx", true, false, 100));
        Require(restored.SelectedLanguage.Code == "vi" && restored.CaptionSize == 30);
    })
};

var failures = 0;
foreach (var (name, check) in checks)
{
    try { check(); Console.WriteLine($"PASS {name}"); }
    catch (Exception ex) { failures++; Console.Error.WriteLine($"FAIL {name}: {ex.Message}"); }
}
Console.WriteLine($"{checks.Length - failures}/{checks.Length} checks passed.");
return failures == 0 ? 0 : 1;

static DemoSession Running() { var session = new DemoSession(); session.Start(); return session; }
static void Require(bool condition) { if (!condition) throw new InvalidOperationException("Unexpected session behavior."); }
