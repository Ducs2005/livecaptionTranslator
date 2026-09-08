#  liệu Yêu cầu Sản phẩm (PRD)

> v0.1 · 08/09/2026 · Dự thảo. Mục tiêu dịch Windows Live Captions đã xác nhận; ưu tiên, chỉ tiêu và ngôn ngữ MVP cần phê duyệt.

## 1. Tổng quan

livecaptionTranslator bổ sung lớp dịch cho văn bản xuất hiện trong Windows Live Captions. Người dùng mở Live Captions, chọn ngôn ngữ đích và bắt đầu đọc bản dịch trong cửa sổ riêng. Phạm vi không bao gồm thu âm hoặc chuyển giọng nói thành văn bản.

Người dùng mục tiêu và vấn đề được trình bày tại [3.1](3.1-product-discovery.md). Giá trị chính: theo dõi bản dịch mà không sao chép phụ đề thủ công, với ngôn ngữ đích phù hợp nhu cầu và dịch vụ được chọn.

## 2. Mục tiêu sản phẩm

| Mã | Mục tiêu | Ngưỡng đề xuất | Cách đánh giá |
| --- | --- | --- | --- |
| G-01 | Bắt đầu sử dụng dễ dàng | ≥ 4/5 người thử có bản dịch trong 60 giây khi nguồn sẵn sàng | Quan sát thử nghiệm có mẫu nội dung thống nhất |
| G-02 | Dịch theo kịp phụ đề | P95 ≤ 3 giây từ lúc đoạn nguồn ổn định tới bản dịch hiển thị | NFR-01 |
| G-03 | Bảo toàn nguồn và dịch có nghĩa | ≥ 99% đoạn nguồn được lấy đúng, không trùng trong bộ mẫu; ≥ 80% bản dịch đạt rubric | NFR-02, NFR-03 |
| G-04 | Người dùng kiểm soát được hoạt động | Đạt toàn bộ ca dừng, đổi đích, mất nguồn, lỗi dịch và riêng tư | AC tương ứng và NFR-04 |

Không tính độ trễ nhận dạng âm thanh của Windows vào độ trễ lớp dịch; vẫn ghi nhận riêng nếu đánh giá trải nghiệm đầu cuối.

## 3. Phạm vi MVP — MoSCoW

Must: bắt buộc để nghiệm thu; Should: nên có nếu đủ nguồn lực; Could: cân nhắc sau; Won't: ngoài phiên bản này.

| Tính năng | Ưu tiên | Phạm vi |
| --- | --- | --- |
| FEAT-01 Kết nối Windows Live Captions | Must | Kiểm tra nguồn, kết nối chủ động, báo không tìm thấy/không đọc được |
| FEAT-02 Chuẩn hóa phụ đề | Must | Xử lý đoạn cập nhật, trùng, nối/cuộn; đánh dấu giới hạn khi mất đoạn |
| FEAT-03 Chọn ngôn ngữ và dịch | Must | Chọn đích được hỗ trợ, dịch theo đoạn, bỏ kết quả lỗi thời |
| FEAT-04 Cửa sổ bản dịch | Must | Hiển thị song ngữ, di chuyển/đổi kích thước, bật/tắt luôn nổi |
| FEAT-05 Điều khiển và phục hồi | Must | Tạm dừng, tiếp tục, dừng, thử kết nối/dịch lại và xóa phiên |
| Nhớ ngôn ngữ/cỡ chữ giữa các lần mở | Should | Chưa đặc tả hoặc cam kết trong MVP |
| Xuất TXT/SRT, phím tắt tùy chỉnh | Could | Không chặn nghiệm thu; cần đặc tả riêng nếu bổ sung |
| Thu âm, tự nhận dạng giọng nói, dịch file | Won't | Windows là nguồn phụ đề duy nhất |
| Tài khoản, lịch sử đám mây, đọc bản dịch thành tiếng | Won't | Ngoài MVP |
| Thay đổi/chèn bản dịch vào chính cửa sổ Windows Live Captions | Won't | Dùng cửa sổ riêng |
| Mọi ngôn ngữ, mọi build Windows, offline, toàn màn hình độc quyền | Won't | Không cam kết khi chưa có hỗ trợ/kiểm chứng |

## 4. Luồng sử dụng chính

1. Người dùng bật và thiết lập Windows Live Captions.
2. Mở livecaptionTranslator, đọc thông báo nơi xử lý văn bản, chọn ngôn ngữ đích được hỗ trợ.
3. Bấm Bắt đầu; ứng dụng xác nhận đọc được nguồn hoặc đưa hướng dẫn xử lý.
4. Phụ đề ổn định được đưa đi dịch; giao diện hiển thị đúng đoạn nguồn và bản dịch tương ứng.
5. Người dùng có thể bật luôn nổi để xem khi sử dụng ứng dụng khác.
6. Người dùng đổi ngôn ngữ, tạm dừng/tiếp tục hoặc dừng phiên.
7. Khi đóng ứng dụng, dữ liệu phiên được loại bỏ theo NFR-04.

## 5. Chính sách sản phẩm

- Ngôn ngữ người dùng chọn là **ngôn ngữ đích**. Ngôn ngữ nguồn thuộc cấu hình Live Captions; cách cung cấp nguồn cho dịch vụ cần xác nhận theo nhà cung cấp.
- Danh sách đích phản ánh năng lực dịch thực tế. Đề xuất nghiệm thu tiếng Việt và tiếng Anh; không hứa hỗ trợ tất cả ngôn ngữ.
- Đổi ngôn ngữ trong khi chạy áp dụng cho đoạn hiện tại và đoạn mới. Lịch sử cũ giữ nhãn ngôn ngữ tại thời điểm dịch, không tự dịch lại toàn bộ.
- Khi tạm dừng, ứng dụng ngừng nhận văn bản mới và gửi yêu cầu dịch mới; Windows Live Captions vẫn có thể tiếp tục hoạt động độc lập.
- Khi tiếp tục/kết nối lại, chỉ đọc nội dung hiện còn truy cập được và nội dung mới. Không hứa khôi phục phần đã trôi mất.
- Không lưu lâu dài nội dung phụ đề; nếu dùng dịch vụ ngoài phải thông báo chính xác và xác nhận chính sách lưu dữ liệu trước khi dùng dữ liệu thật.

## 6. Ràng buộc, phụ thuộc và rủi ro

| Nội dung | Ảnh hưởng | Cách xử lý dự kiến |
| --- | --- | --- |
| Cách đọc phụ đề chưa kiểm chứng | Có thể chặn toàn bộ sản phẩm | Kiểm chứng trước chốt kiến trúc; lưu build/điều kiện thử |
| Windows thay đổi cấu trúc phụ đề | Mất kết nối hoặc tách đoạn sai | Khai báo phạm vi hỗ trợ; ca hồi quy cho cập nhật/cuộn |
| Dịch vụ mạng/giới hạn hạn mức | Trễ, lỗi hoặc chi phí ngoài dự kiến | Timeout rõ, không lặp vô hạn, chủ động thử lại |
| Văn bản chứa dữ liệu riêng tư | Rủi ro gửi nội dung ra ngoài thiết bị | NFR-04, thông báo và quyết định nhà cung cấp |
| Caption tạm thay đổi liên tục | Dịch trùng hoặc kết quả cũ ghi đè mới | Mã đoạn, phiên bản, điều kiện ổn định và kiểm tra kết quả |

## 7. Mốc bàn giao

| Mốc | Đầu ra | Điều kiện chuyển bước |
| --- | --- | --- |
| M1 Khám phá | Khảo sát, rubric và cấu hình Windows | Vấn đề và phạm vi được xác nhận |
| M2 Chốt yêu cầu | Bộ 3.1–3.5, ngôn ngữ, NFR và khả thi tích hợp | Nhóm/giảng viên phê duyệt |
| M3 Thiết kế | Wireframe, thiết kế tích hợp, chính sách dữ liệu | Thiết kế đáp ứng yêu cầu đã chốt |
| M4 Triển khai và kiểm thử | Sản phẩm, kết quả AC/NFR | Thực hiện khi có yêu cầu triển khai |
| M5 Nghiệm thu | Demo và biên bản đối chiếu rubric | Đạt điều kiện nghiệm thu |

Chưa có thời hạn hoặc phân công thành viên; cần bổ sung khi có thông tin thực tế.

## 8. Điều kiện nghiệm thu

- FR-01–FR-10 và mọi AC liên quan đạt trên môi trường Windows đã ghi nhận.
- NFR-01–NFR-06 có bằng chứng đánh giá, ngưỡng thay đổi phải được duyệt trước nghiệm thu.
- Luồng kết nối → phụ đề → dịch → đổi ngôn ngữ → tạm dừng/tiếp tục → dừng hoạt động đúng.
- Không có lỗi chặn dịch, trộn sai đoạn/ngôn ngữ, gửi yêu cầu mới sau dừng hoặc lưu nội dung trái chính sách.
- Tài liệu khớp hành vi thực tế và nêu giới hạn tương thích.

Bàn giao hiện tại chỉ gồm cấu trúc và tài liệu; chưa chứng minh sản phẩm chạy được và chưa đạt nghiệm thu phần mềm.
