using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using LiveCaptionTranslator.Models;

namespace LiveCaptionTranslator;

public partial class MainWindow : Window
{
    private readonly DemoSession _session = new();
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(4) };
    private bool _followLatest = true;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _session;
        _timer.Tick += OnTick;
        Closed += (_, _) =>
        {
            _timer.Stop();
            _timer.Tick -= OnTick;
            _session.Clear();
        };
    }

    private void OnTick(object? sender, EventArgs e)
    {
        _session.Advance();
        SynchronizeTimer();
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        if (!_session.IsEmpty && !Confirm("Bắt đầu demo mới và thay thế nội dung phiên hiện tại?")) return;
        _followLatest = true;
        _session.Start();
        SynchronizeTimer();
    }

    private void Pause_Click(object sender, RoutedEventArgs e)
    {
        _session.TogglePause();
        SynchronizeTimer();
    }

    private void Stop_Click(object sender, RoutedEventArgs e)
    {
        _session.Stop();
        SynchronizeTimer();
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        // Freeze the demo while the modal is open so Cancel preserves what the user saw.
        _timer.Stop();
        if (Confirm("Xóa nội dung demo và trở về trạng thái sẵn sàng?"))
        {
            _session.Clear();
            _followLatest = true;
            LatestButton.Visibility = Visibility.Collapsed;
        }
        SynchronizeTimer();
    }

    private bool Confirm(string message) => MessageBox.Show(this, message, "LiveCaption Translator",
        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes;

    private void SynchronizeTimer()
    {
        _timer.Stop();
        if (_session.State == SessionState.Running) _timer.Start();
    }

    private void CaptionScroll_Changed(object sender, ScrollChangedEventArgs e)
    {
        if (LatestButton is null) return;
        if (e.ExtentHeightChange == 0 && e.ViewportHeightChange == 0)
            _followLatest = CaptionScroll.VerticalOffset >= CaptionScroll.ScrollableHeight - 8;
        if (_followLatest && (e.ExtentHeightChange != 0 || e.ViewportHeightChange != 0))
            CaptionScroll.ScrollToEnd();
        LatestButton.Visibility = !_followLatest && CaptionScroll.ScrollableHeight > 0
            ? Visibility.Visible : Visibility.Collapsed;
    }

    private void Latest_Click(object sender, RoutedEventArgs e)
    {
        _followLatest = true;
        CaptionScroll.ScrollToEnd();
        LatestButton.Visibility = Visibility.Collapsed;
    }

    private void Guide_Click(object sender, RoutedEventArgs e) => MessageBox.Show(this,
        "BẢN UI DEMO\n\nChọn Tiếng Việt hoặc English, rồi bấm Bắt đầu demo. " +
        "Sáu câu mẫu xuất hiện cách nhau 4 giây. Bạn có thể tạm dừng, tiếp tục, đổi ngôn ngữ, " +
        "ẩn bản gốc và điều chỉnh cỡ chữ.\n\nLuôn nổi giữ cửa sổ phía trên các cửa sổ thông thường. " +
        "Nội dung phiên chỉ nằm trong bộ nhớ và không được gửi ra ngoài.\n\n" +
        "TÍCH HỢP Ở GIAI ĐOẠN SAU\n\nWindows Live Captions là nguồn phụ đề dự kiến. " +
        "Phiên bản này chưa kết nối với Windows Live Captions, chưa nhận âm thanh và chưa dịch văn bản thật.",
        "Hướng dẫn · LiveCaption Translator", MessageBoxButton.OK, MessageBoxImage.Information);
}
