# UI cơ bản — 19/09/2026

## Phạm vi đã triển khai

Cửa sổ desktop WPF bằng C#/.NET 10, giao diện tiếng Việt, bảng phụ đề gốc/bản dịch, chọn đích Việt/Anh và điều khiển phiên demo. UI tách trạng thái vào `Models/DemoSession.cs`; `MainWindow` chỉ điều khiển timer, hộp xác nhận, cuộn và cửa sổ.

Nguồn dữ liệu là 6 câu tiếng Anh và bản dịch tiếng Việt có sẵn trong mã nguồn. Câu đầu xuất hiện khi bắt đầu; các câu sau cách 4 giây. Kết thúc tự động khi hết mẫu. Không giả báo đã kết nối Windows, không yêu cầu microphone, không gọi dịch vụ mạng.

## Quyết định thiết kế

- WPF phù hợp mục tiêu cửa sổ Windows và có hành vi `Topmost`, resize, kéo cửa sổ bằng thanh tiêu đề. Xem [tài liệu Microsoft WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/).
- .NET 10 là SDK có sẵn trên môi trường phát triển; không thêm package bên ngoài cho UI này.
- Tông nền sáng, xanh lục đậm cho thao tác chính; nguồn và đích có nhãn chữ độc lập với màu.
- Điều khiển dùng thành phần WPF tiêu chuẩn, có tên truy cập và focus bàn phím. Cỡ chữ phụ đề 18–30.
- Thay đổi đích trong phiên đang chạy chỉ cập nhật đoạn hiện tại và các đoạn sau; lịch sử giữ nhãn đích cũ. Khi tạm dừng, đổi đích áp dụng khi tiếp tục.
- Cuộn lên xem lịch sử không bị ép về cuối; có nút Về mới nhất.
- Dừng giữ nội dung; xóa hoặc thay phiên yêu cầu xác nhận với lựa chọn mặc định No. Đóng cửa sổ dừng timer và xóa bộ nhớ phiên.
- Ngôn ngữ đích và tùy chọn hiển thị được ghi vào `%LOCALAPPDATA%/LiveCaptionTranslator/preferences.json`; chỉ lưu file cấu hình, không lưu nội dung phụ đề. JSON hỏng hoặc không đọc được sẽ dùng mặc định.

## Mức bao phủ đặc tả

| Nhóm | Bản UI này |
| --- | --- |
| FEAT-01, FEAT-02: nguồn Windows/chuẩn hóa caption | Chưa triển khai; giao diện thông báo rõ nguồn chưa kết nối |
| FEAT-03: chọn đích/dịch | Có lựa chọn đích với dữ liệu mẫu; không có dịch vụ dịch thật |
| FEAT-04: hiển thị/cửa sổ | Có song ngữ, cuộn, cỡ chữ, ẩn nguồn, resize và luôn nổi |
| FEAT-05: điều khiển | Có trạng thái demo, tạm dừng/tiếp tục/dừng/xóa; chưa xử lý mất nguồn hay lỗi dịch vụ thật |

## Giới hạn

- Kích thước tối thiểu UI cơ bản là 780 × 650 DIP để giữ đủ vùng điều khiển. Chưa đạt chế độ cửa sổ nhỏ 360 × 240 được đề xuất trong đặc tả; cần thiết kế compact ở bước sau.
- Không lưu nội dung phiên khi đóng, không xuất phụ đề, không dùng tài khoản. Tùy chọn giao diện được lưu cục bộ.
- Không có trạng thái đang dịch qua mạng, retry API, hạn mức hoặc mô phỏng sai lệch nguồn; các chức năng này không được đánh dấu hoàn tất chỉ nhờ UI demo.
- Các chỉ tiêu hiệu năng/chất lượng dịch trong NFR cần tích hợp thật mới kiểm chứng được.

Các đặc tả 3.1–3.5 tiếp tục mô tả mục tiêu sản phẩm, không được coi là danh sách tính năng đã triển khai.
