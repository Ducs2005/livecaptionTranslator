# Kiểm tra UI cơ bản — 19/09/2026

## Môi trường

Windows trên máy phát triển; .NET SDK 10.0.100. Ứng dụng WPF target `net10.0-windows`. Không bổ sung package test bên thứ ba.

## Lệnh kiểm tra

```powershell
dotnet build src/presentation/LiveCaptionTranslator.App --configuration Release
dotnet run --project tests/LiveCaptionTranslator.Checks --configuration Release
```

Bộ kiểm tra là console harness trả exit code khác 0 nếu có lỗi, không phải dự án `dotnet test`. Harness biên dịch cùng file model của ứng dụng, kiểm tra logic phiên; không thay thế UI automation hoặc kiểm thử tích hợp Windows Live Captions.

## Kết quả

- Build Release: thành công, 0 cảnh báo, 0 lỗi.
- 15/15 kiểm tra logic đạt: trạng thái phiên; chọn đích và lịch sử; tùy chỉnh hiển thị; lưu/khôi phục cấu hình; xử lý JSON hỏng; giới hạn giá trị; định dạng bản chép; xuất UTF-8 và từ chối phiên rỗng.
- Lưu cấu hình và xuất bản chép: ghi/đọc tùy chọn, JSON lỗi trở về mặc định, cỡ chữ được giới hạn; nội dung xuất gồm bản gốc và bản dịch đang gắn với từng câu. File kiểm tra tạo trong thư mục build và được xóa sau đó.
- Đã mở được cửa sổ bằng bản build thật, quan sát bố cục ban đầu và cây accessibility: tiếng Việt, bộ chọn ngôn ngữ, trạng thái sẵn sàng, các nút bị vô hiệu hóa đúng lúc chưa chạy và thông báo demo xuất hiện.
- Kiểm tra bằng Computer Use dừng theo thao tác Escape của người dùng trước khi xác nhận tương tác Bắt đầu. Chưa xác minh thủ công đầy đủ: các luồng click, resize, focus bàn phím, cuộn và luôn nổi trên ứng dụng khác.
- Rà soát ban đầu phát hiện màu chữ nút chính chưa kế thừa màu trắng; đã sửa bằng cách cho TextBlock kế thừa Foreground từ nút/cửa sổ. Chưa chụp lại giao diện sau sửa vì Computer Use đã dừng.

## Cần kiểm tra tiếp bằng tay

1. Bắt đầu → tạm dừng → chờ >4 giây không thêm câu → tiếp tục → dừng.
2. Đổi Việt/Anh trong khi chạy, khi tạm dừng và sau dừng; kiểm tra nhãn lịch sử.
3. Hủy/đồng ý hộp Xóa phiên và Demo mới; không có câu xuất hiện lại sau xóa.
4. Hiện/ẩn nguồn, cỡ chữ 18–30, cuộn lên rồi Về mới nhất, kéo resize tới mức tối thiểu.
5. Tab qua điều khiển, chọn ngôn ngữ bằng bàn phím, thử Luôn nổi với ứng dụng cửa sổ khác.
6. Đóng và mở lại: không khôi phục nội dung phiên cũ.

Đây là kiểm tra UI/demo, chưa phải bằng chứng đạt FR/NFR hoặc nghiệm thu sản phẩm hoàn chỉnh.
