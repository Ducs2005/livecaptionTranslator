namespace LiveCaptionTranslator.Models;

public sealed record DemoLine(string English, string Vietnamese);

public sealed class CaptionEntry(int number, DemoLine line, string targetCode) : ObservableModel
{
    private string _targetCode = targetCode;
    public string Number { get; } = number.ToString("D2");
    public string Source => line.English;
    public string Translation => _targetCode == "vi" ? line.Vietnamese : line.English;
    public string TargetLabel => _targetCode == "vi" ? "TIẾNG VIỆT" : "ENGLISH · CÙNG NGÔN NGỮ";

    public void ChangeTarget(string targetCode)
    {
        _targetCode = targetCode;
        Notify(nameof(Translation));
        Notify(nameof(TargetLabel));
    }
}
