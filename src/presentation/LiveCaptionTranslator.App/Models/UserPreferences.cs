namespace LiveCaptionTranslator.Models;

public sealed record UserPreferences(
    string TargetLanguageCode = "vi",
    bool ShowSource = true,
    bool AlwaysOnTop = false,
    double CaptionSize = 22);
