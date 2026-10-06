using System.IO;
using System.Text.Json;
using LiveCaptionTranslator.Models;

namespace LiveCaptionTranslator.Services;

/// <summary>Stores display preferences locally; caption content is never persisted.</summary>
public sealed class JsonPreferencesStore(string? filePath = null)
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private readonly string _filePath = filePath ?? Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "LiveCaptionTranslator", "preferences.json");

    public UserPreferences Load()
    {
        try
        {
            if (!File.Exists(_filePath)) return new UserPreferences();
            return JsonSerializer.Deserialize<UserPreferences>(File.ReadAllText(_filePath))
                ?? new UserPreferences();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            return new UserPreferences();
        }
    }

    public void Save(UserPreferences preferences)
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
        var temporaryPath = _filePath + ".tmp";
        File.WriteAllText(temporaryPath, JsonSerializer.Serialize(preferences, JsonOptions));
        File.Move(temporaryPath, _filePath, overwrite: true);
    }
}
