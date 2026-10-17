using System.IO;
using System.Text;
using LiveCaptionTranslator.Models;

namespace LiveCaptionTranslator.Services;

/// <summary>Formats and exports the captions explicitly selected by the user.</summary>
public sealed class CaptionExportService
{
    public string Format(IEnumerable<CaptionEntry> captions)
    {
        ArgumentNullException.ThrowIfNull(captions);
        var entries = captions.ToArray();
        if (entries.Length == 0)
            throw new InvalidOperationException("Không có phụ đề để xuất.");

        var output = new StringBuilder("LiveCaption Translator · Bản chép phụ đề demo\r\n\r\n");
        foreach (var caption in entries)
        {
            output.Append(caption.Number).Append(". ").AppendLine(caption.TargetLabel);
            output.Append("   EN: ").AppendLine(caption.Source);
            output.Append("   Bản dịch: ").AppendLine(caption.Translation);
            output.AppendLine();
        }
        return output.ToString();
    }

    public Task ExportAsync(IEnumerable<CaptionEntry> captions, string filePath, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        return File.WriteAllTextAsync(filePath, Format(captions), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), cancellationToken);
    }
}
