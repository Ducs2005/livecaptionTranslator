# Khám phá Sản phẩm (Product Discovery)

> v0.1 · 08/09/2026 · Dự thảo. Ý tưởng cốt lõi đã được chủ dự án xác nhận; persona, kết quả kỳ vọng và phạm vi MVP bên dưới là đề xuất, chưa có khảo sát thực tế.

## 1. Bối cảnh và vấn đề

Chủ dự án muốn dịch nội dung **Windows Live Captions** sang ngôn ngữ mình lựa chọn. Trong tình huống của người dùng, phụ đề gốc đã có nhưng chưa đáp ứng nhu cầu đọc bản dịch mong muốn.

Theo tài liệu Microsoft, tính năng dịch Live Captions có trên Copilot+ PC chạy Windows 11 24H2 trở lên với các ngôn ngữ đầu ra được nêu trong tài liệu. Vì vậy, bài toán nên được phát biểu là **bổ sung lựa chọn dịch phù hợp thiết bị và ngôn ngữ đích của người dùng**, thay vì khẳng định Windows hoàn toàn không có dịch. [Nguồn Microsoft](https://support.microsoft.com/en-us/accessibility/windows/use-live-captions-to-better-understand-audio) — tham khảo 08/09/2026.

**Phát biểu vấn đề:** người đang dùng Windows Live Captions cần đọc bản dịch liên tục sang ngôn ngữ quen thuộc, thay vì chép từng đoạn phụ đề sang công cụ khác.

## 2. Ranh giới sản phẩm

Windows Live Captions chịu trách nhiệm tạo phụ đề từ âm thanh. livecaptionTranslator chịu trách nhiệm đọc văn bản phụ đề, chuẩn hóa cập nhật, dịch và hiển thị. Chất lượng nhận dạng ban đầu phụ thuộc Windows; dự án không xây dựng bộ nhận dạng giọng nói và không tự thu âm.

Khả năng đọc được phụ đề từ một ứng dụng Windows khác là **giả thuyết kỹ thuật cần kiểm chứng**. Chưa xác nhận API, UI Automation, OCR hoặc phương pháp nào là phù hợp; không coi các lựa chọn này là thiết kế đã chốt.

## 3. Người dùng và bên liên quan

| Nhóm | Bối cảnh giả định | Nhu cầu | Vai trò |
| --- | --- | --- | --- |
| Sinh viên/người tự học | Xem bài giảng ngoại ngữ trên Windows | Đọc bản dịch tiếng Việt cùng nội dung đang phát | Thử nghiệm trải nghiệm |
| Người xem nội dung đa ngôn ngữ | Video/âm thanh được Windows tạo phụ đề | Chọn ngôn ngữ đích quen thuộc | Xác nhận danh sách ngôn ngữ |
| Chủ dự án/nhóm phát triển | Xây dựng sản phẩm học phần | Phạm vi khả thi và rõ ràng | Chốt yêu cầu và thực hiện |
| Giảng viên học phần | Đánh giá đồ án | Tài liệu đầy đủ, truy vết và nghiệm thu | Xác nhận rubric/phạm vi |

Các mô tả trên là persona dự kiến, không phải người đã được phỏng vấn.

## 4. Jobs to Be Done

- Khi Windows đang hiển thị phụ đề ngoại ngữ, tôi muốn đọc bản dịch liên tục để theo dõi nội dung mà không chép từng câu.
- Khi xem video trong ứng dụng khác, tôi muốn cửa sổ dịch có thể nằm phía trên để không phải chuyển qua lại.
- Khi muốn đổi ngôn ngữ, tôi muốn chọn ngôn ngữ đích và biết rõ bản dịch mới đang dùng lựa chọn nào.
- Khi phụ đề hoặc dịch vụ dịch bị gián đoạn, tôi muốn hiểu nguyên nhân và tiếp tục mà không mất nội dung đang đọc.

## 5. Hành trình và cơ hội

| Giai đoạn | Khó khăn giả định | Cơ hội |
| --- | --- | --- |
| Chuẩn bị | Chưa bật hoặc chưa thiết lập Live Captions | Hướng dẫn và trạng thái nguồn rõ ràng |
| Kết nối | Không biết ứng dụng đã đọc được phụ đề chưa | Phân biệt đã kết nối/chưa có nội dung/lỗi đọc |
| Theo dõi | Phụ đề thay đổi từng phần, chép tay gây gián đoạn | Cập nhật theo đoạn, hạn chế trùng và nhấp nháy |
| Đổi ngôn ngữ | Bản dịch cũ/mới dễ lẫn nhau | Nhãn ngôn ngữ và chính sách loại kết quả cũ |
| Gián đoạn | Đóng Live Captions hoặc mất mạng | Giữ nội dung, báo trạng thái và thử lại chủ động |

## 6. Giá trị đề xuất và phương án thay thế

Giá trị đề xuất là một cửa sổ dịch đồng hành với Windows Live Captions, cho phép chọn ngôn ngữ trong phạm vi dịch vụ hỗ trợ, kiểm soát hoạt động dịch và vị trí hiển thị.

Các phương án cần đối chiếu khi khảo sát: dịch tích hợp trên thiết bị hỗ trợ, phụ đề dịch của nền tảng video và dịch văn bản thủ công. Tiêu chí: ngôn ngữ đích, phần cứng, ứng dụng phát nội dung, thao tác, độ trễ và cách xử lý dữ liệu. Chưa có nghiên cứu so sánh chứng minh sản phẩm tốt hơn các phương án này.

## 7. Giả thuyết và kế hoạch kiểm chứng

| Mã | Giả thuyết | Cách kiểm chứng dự kiến | Điều kiện đề xuất |
| --- | --- | --- | --- |
| H-01 | Dịch phụ đề có sẵn giải quyết nhu cầu thực tế | Phỏng vấn 5 người đang dùng Windows xem nội dung ngoại ngữ | Ít nhất 4/5 mô tả được tình huống cần bản dịch |
| H-02 | Có thể đọc phụ đề ổn định trên máy đích | Khảo sát kỹ thuật ở giai đoạn sau, thử cập nhật/nối câu/cuộn/đóng-mở | Đạt NFR-02 trên cấu hình ghi nhận, không giả định có API công khai |
| H-03 | Cửa sổ dịch dễ sử dụng trong lúc xem nội dung | Thử nguyên mẫu với 5 người | Ít nhất 4/5 bắt đầu đọc bản dịch trong 60 giây khi Live Captions đã sẵn sàng |
| H-04 | Độ trễ và chất lượng dịch đủ cho theo dõi | Bộ mẫu phụ đề được phép sử dụng | Đạt NFR-01 và NFR-03 |

Đây là kế hoạch, chưa có kết quả. Mẫu nhỏ phục vụ khám phá học phần, không dùng để suy rộng thống kê.

## 8. Câu hỏi khảo sát

1. Bạn dùng phiên bản Windows/thiết bị nào và đã dùng Live Captions chưa?
2. Bạn muốn dịch từ ngôn ngữ nào sang ngôn ngữ nào?
3. Nội dung thường phát trong ứng dụng nào? Có cần xem toàn màn hình không?
4. Hiện bạn xử lý thế nào khi chỉ có phụ đề gốc?
5. Khi phụ đề dịch chậm/sai, mức nào khiến bạn không thể theo dõi?
6. Bạn có chấp nhận gửi văn bản phụ đề tới dịch vụ dịch bên ngoài không?

Xin phép trước khi ghi nhận dữ liệu và không dùng nội dung riêng tư để demo nếu chưa được cho phép.

## 9. Xác nhận và giả định

| Mã | Nội dung | Trạng thái |
| --- | --- | --- |
| C-01 | Đọc Windows Live Captions và dịch sang ngôn ngữ người dùng chọn | Chủ dự án xác nhận |
| A-01 | Ứng dụng đồng hành trên Windows; không phải web nhận microphone | Đề xuất theo C-01, hình thức UI cụ thể chưa chốt |
| A-02 | Máy đích Windows 11 có Live Captions; build cụ thể sẽ ghi nhận | Chưa xác nhận cấu hình |
| A-03 | MVP nghiệm thu ngôn ngữ đích Việt và Anh; danh sách có thể mở rộng theo dịch vụ | Chưa xác nhận danh sách |
| A-04 | Dùng dịch vụ dịch trực tuyến trong MVP | Chưa chọn nhà cung cấp/ngân sách |
| A-05 | Không tài khoản và không lưu lịch sử sau khi thoát | Đề xuất tối giản MVP |

## 10. Điều kiện kết thúc khám phá

Cần có rubric giảng viên, xác nhận môi trường/ngôn ngữ, kết quả khảo sát và bằng chứng khả thi việc đọc phụ đề trước khi chốt thiết kế. Nếu không đọc được phụ đề ổn định, phải trình phương án thay đổi phạm vi để chủ dự án xác nhận; không tự chuyển sang tự nhận dạng âm thanh.

Đầu ra tiếp theo: [PRD](3.2-prd.md) và [các quyết định mở](3.3-requirements-analysis.md).
