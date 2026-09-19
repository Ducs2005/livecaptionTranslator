using System.Collections.ObjectModel;

namespace LiveCaptionTranslator.Models;

public enum SessionState { Ready, Running, Paused, Stopped, Completed }
public sealed record TargetLanguage(string Code, string Name);

/// <summary>Local UI demonstration only: no capture, network requests, or persistent history.</summary>
public sealed class DemoSession : ObservableModel
{
    private static readonly DemoLine[] Lines =
    [
        new("A new language opens a window to a different world.", "Một ngôn ngữ mới mở ra cánh cửa đến một thế giới khác."),
        new("You don't have to understand every word to follow the story.", "Bạn không cần hiểu từng từ để theo dõi câu chuyện."),
        new("Small steps, taken every day, can make a big difference.", "Những bước nhỏ mỗi ngày có thể tạo nên sự khác biệt lớn."),
        new("Stay curious. There is always something new to learn.", "Hãy giữ sự tò mò. Luôn có điều mới để học hỏi."),
        new("Technology should help us connect with one another.", "Công nghệ nên giúp chúng ta kết nối với nhau."),
        new("And sometimes, understanding starts with a single sentence.", "Và đôi khi, sự thấu hiểu bắt đầu chỉ từ một câu nói.")
    ];

    private SessionState _state;
    private TargetLanguage _selectedLanguage;
    private int _nextLine;
    private bool _showSource = true;
    private bool _alwaysOnTop;
    private double _captionSize = 22;
    private string _notice = "Chọn ngôn ngữ, rồi bắt đầu xem thử giao diện.";

    public DemoSession() => _selectedLanguage = Languages[0];

    public IReadOnlyList<TargetLanguage> Languages { get; } =
        [new("vi", "Tiếng Việt"), new("en", "English")];
    public ObservableCollection<CaptionEntry> Captions { get; } = [];
    public SessionState State => _state;
    public bool IsEmpty => Captions.Count == 0;
    public bool CanStart => _state is SessionState.Ready or SessionState.Stopped or SessionState.Completed;
    public bool CanPause => _state is SessionState.Running or SessionState.Paused;
    public bool CanStop => _state is SessionState.Running or SessionState.Paused;
    public bool CanClear => !IsEmpty;
    public string PauseLabel => _state == SessionState.Paused ? "Tiếp tục" : "Tạm dừng";
    public string StartLabel => IsEmpty ? "Bắt đầu demo" : "Demo mới";
    public string CountLabel => $"{Captions.Count:D2} đoạn";
    public string ProgressLabel => $"{Captions.Count} / {Lines.Length} đoạn mẫu";
    public string Notice => _notice;
    public string Status => _state switch
    {
        SessionState.Running => "Đang phát demo",
        SessionState.Paused => "Đã tạm dừng",
        SessionState.Stopped => "Đã dừng",
        SessionState.Completed => "Đã hết nội dung mẫu",
        _ => "Sẵn sàng"
    };
    public string StatusColor => _state switch
    {
        SessionState.Running => "#176C5B",
        SessionState.Paused => "#96601F",
        _ => "#607571"
    };

    public TargetLanguage SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            if (value is null || !Languages.Contains(value) || value == _selectedLanguage) return;
            _selectedLanguage = value;
            Notify();
            // Running: update only the current caption. Historical translations keep their labels.
            if (_state == SessionState.Running && Captions.Count > 0)
                Captions[^1].ChangeTarget(value.Code);
            SetNotice($"Đích: {value.Name}. Lịch sử giữ ngôn ngữ tại thời điểm hiển thị.");
        }
    }

    public bool ShowSource
    {
        get => _showSource;
        set { _showSource = value; Notify(); }
    }
    public bool AlwaysOnTop
    {
        get => _alwaysOnTop;
        set { _alwaysOnTop = value; Notify(); }
    }
    public double CaptionSize
    {
        get => _captionSize;
        set { _captionSize = Math.Clamp(value, 18, 30); Notify(); }
    }

    // The view must confirm replacing existing content before calling Start.
    public void Start()
    {
        if (!CanStart) return;
        Captions.Clear();
        _nextLine = 0;
        _state = SessionState.Running;
        _notice = "Đang phát văn bản có sẵn trên máy. Không thu âm hoặc gửi dữ liệu.";
        Advance();
    }

    public void Advance()
    {
        if (_state != SessionState.Running) return;
        if (_nextLine < Lines.Length)
        {
            Captions.Add(new CaptionEntry(_nextLine + 1, Lines[_nextLine], SelectedLanguage.Code));
            _nextLine++;
        }
        if (_nextLine == Lines.Length)
        {
            _state = SessionState.Completed;
            _notice = "Đã hết 6 đoạn mẫu. Bạn có thể xem lại hoặc bắt đầu demo mới.";
        }
        NotifySession();
    }

    public void TogglePause()
    {
        if (!CanPause) return;
        _state = _state == SessionState.Running ? SessionState.Paused : SessionState.Running;
        if (_state == SessionState.Running && Captions.Count > 0)
            Captions[^1].ChangeTarget(SelectedLanguage.Code);
        _notice = _state == SessionState.Paused
            ? "Demo đã tạm dừng. Nội dung đang hiển thị được giữ lại."
            : "Đã tiếp tục phát nội dung mẫu.";
        NotifySession();
    }

    public void Stop()
    {
        if (!CanStop) return;
        _state = SessionState.Stopped;
        _notice = "Phiên đã dừng. Nội dung được giữ lại để xem trong lần mở này.";
        NotifySession();
    }

    public void Clear()
    {
        _state = SessionState.Ready;
        _nextLine = 0;
        Captions.Clear();
        _notice = "Đã xóa nội dung demo. Sẵn sàng cho một phiên mới.";
        NotifySession();
    }

    private void SetNotice(string text) { _notice = text; Notify(nameof(Notice)); }
    private void NotifySession()
    {
        foreach (var name in new[] { nameof(State), nameof(IsEmpty), nameof(CanStart), nameof(CanPause),
            nameof(CanStop), nameof(CanClear), nameof(PauseLabel), nameof(StartLabel), nameof(CountLabel),
            nameof(ProgressLabel), nameof(Status), nameof(StatusColor), nameof(Notice) }) Notify(name);
    }
}
