# User Stories & Tiêu chí Chấp nhận

> v0.1 · 08/09/2026 · Dự thảo. Tất cả story dưới đây là Must đề xuất. Tiêu chí mô tả kiểm thử tương lai, chưa được chạy.

Given = bối cảnh, When = hành động/sự kiện, Then = kết quả quan sát được. Các trạng thái, thời gian và quy tắc theo [3.5](feature-specification.md); yêu cầu/NFR theo [3.3](requirements-analysis.md).

## US-01 — Kết nối nguồn phụ đề

**Là** người dùng Windows Live Captions, **tôi muốn** kết nối ứng dụng với nguồn phụ đề, **để** bắt đầu dịch nội dung đang xem. Liên kết: FR-01, FEAT-01.

- **AC-01.1:** Given Live Captions đang mở và đọc được, đã chọn đích hợp lệ và chấp nhận thông báo xử lý văn bản; When bấm Bắt đầu; Then ứng dụng chuyển sang Đang dịch, hiển thị nguồn đã kết nối và đích đang chọn.
- **AC-01.2:** Given Live Captions chưa mở; When bấm Bắt đầu; Then hiển thị Không tìm thấy Live Captions, hướng dẫn mở nguồn và nút Thử kết nối lại; không gửi yêu cầu dịch.
- **AC-01.3:** Given cửa sổ nguồn tồn tại nhưng không đọc được; When kết nối thất bại; Then báo Không đọc được phụ đề, không báo nhầm là nguồn không có lời nói và cho thử lại.
- **AC-01.4:** Given nguồn kết nối được nhưng chưa có văn bản; When đang chờ; Then hiển thị Đã kết nối — chờ phụ đề, không dịch chuỗi rỗng và không báo mất nguồn chỉ vì không có cập nhật.

## US-02 — Nhận phụ đề chính xác theo cập nhật

**Là** người theo dõi nội dung, **tôi muốn** ứng dụng phân biệt đoạn mới với đoạn đang sửa, **để** không thấy bản dịch bị nhân đôi. Liên kết: FR-02, FEAT-02.

- **AC-02.1:** Given một snapshot đã được xử lý; When nhận lại snapshot giống hệt trong cùng vị trí nguồn; Then không thêm đoạn hoặc gửi yêu cầu dịch mới.
- **AC-02.2:** Given đoạn hiện tại lần lượt là “I like” rồi “I like this course”; When nội dung thay đổi trước khi ổn định; Then cập nhật cùng đoạn và chỉ đưa phiên bản ổn định mới nhất đi dịch.
- **AC-02.3:** Given bản dịch đoạn phiên bản 1 đang chờ; When nguồn sửa sang phiên bản 2 và kết quả phiên bản 1 về sau đó; Then kết quả phiên bản 1 không được hiển thị như bản dịch của phiên bản 2.
- **AC-02.4:** Given bộ mẫu có cuộn nguồn giữ lại dòng cũ và một câu giống hệt được nói lại ở vị trí mới; When ứng dụng xử lý luồng; Then dòng còn lại không bị thêm lần nữa, câu lặp thật ở vị trí mới vẫn được giữ; trường hợp không thể phân biệt phải báo giới hạn và ghi nhận ca chưa đạt thay vì âm thầm tuyên bố đúng.

## US-03 — Chọn và đổi ngôn ngữ đích

**Là** người dùng, **tôi muốn** chọn ngôn ngữ mình muốn đọc, **để** bản dịch phù hợp nhu cầu. Liên kết: FR-03, FEAT-03.

- **AC-03.1:** Given danh sách ngôn ngữ hỗ trợ đã nạp; When chọn một đích hợp lệ và bắt đầu; Then đích đó hiển thị rõ và được dùng cho yêu cầu dịch mới; khi chưa có đích hợp lệ, Bắt đầu bị vô hiệu hóa kèm hướng dẫn.
- **AC-03.2:** Given đang dịch sang tiếng Việt và còn kết quả đang chờ; When đổi sang tiếng Anh; Then đoạn hiện tại được dịch lại theo đích mới, kết quả cũ đang chờ bị bỏ qua, lịch sử đã hoàn tất giữ nhãn tiếng Việt và đoạn mới dùng tiếng Anh.
- **AC-03.3:** Given đích không được dịch vụ hỗ trợ hoặc danh sách không khả dụng; When người dùng thao tác; Then không gửi yêu cầu với đích đó, giải thích vấn đề và cho chọn lại/thử nạp lại; không âm thầm đổi sang ngôn ngữ khác.

## US-04 — Đọc bản dịch đúng đoạn

**Là** người nghe/xem nội dung ngoại ngữ, **tôi muốn** bản dịch đi cùng đoạn gốc tương ứng, **để** hiểu nội dung đang theo dõi. Liên kết: FR-04, FEAT-03.

- **AC-04.1:** Given đoạn không rỗng đã ổn định; When dịch thành công; Then hiển thị bản dịch cùng đoạn nguồn và nhãn đích, trạng thái chuyển từ Chờ dịch sang Đã dịch.
- **AC-04.2:** Given yêu cầu đoạn B được trả trước đoạn A; When cả hai hoàn tất; Then mỗi kết quả vẫn gắn đúng đoạn, thứ tự hiển thị A rồi B không bị đổi theo thứ tự trả về.
- **AC-04.3:** Given xác định được ngôn ngữ nguồn trùng đích; When xử lý đoạn; Then hiển thị nguyên văn kèm nhãn Cùng ngôn ngữ, không gọi dịch không cần thiết. Nếu chưa xác định được nguồn thì không suy đoán và vẫn dùng cơ chế nguồn hợp lệ của dịch vụ đã chọn.

## US-05 — Phân biệt nguồn, bản dịch và lỗi

**Là** người đọc phụ đề, **tôi muốn** nội dung và trạng thái rõ ràng, **để** biết bản dịch đã sẵn sàng hay chưa. Liên kết: FR-05, FEAT-04.

- **AC-05.1:** Given có đoạn đang sửa, đoạn đang chờ, đoạn hoàn tất và đoạn lỗi; When hiển thị; Then mỗi đoạn có nhãn trạng thái bằng chữ và nhãn nguồn/đích, không chỉ phân biệt bằng màu; văn bản Unicode giữ đúng dấu tiếng Việt.
- **AC-05.2:** Given văn bản dài hơn vùng hiển thị; When người dùng đổi kích thước hoặc cuộn xem đoạn trước; Then nội dung xuống dòng/cuộn được, không che nút điều khiển; cập nhật mới không cưỡng bức kéo người đang xem lịch sử xuống cuối, có nút Về mới nhất.

## US-06 — Theo dõi khi dùng ứng dụng khác

**Là** người xem video, **tôi muốn** cửa sổ dịch có thể luôn nổi và điều chỉnh vị trí, **để** vừa xem vừa đọc bản dịch. Liên kết: FR-06, FEAT-04.

- **AC-06.1:** Given hai ứng dụng chạy ở chế độ cửa sổ trên cấu hình nghiệm thu; When bật Luôn nổi rồi chuyển focus sang ứng dụng kia; Then cửa sổ dịch vẫn hiển thị phía trên và không liên tục giành focus bàn phím.
- **AC-06.2:** Given cửa sổ dịch đang mở; When di chuyển, đổi kích thước trong giới hạn tối thiểu hoặc tắt Luôn nổi; Then bố cục vẫn sử dụng được, tùy chọn phản ánh đúng trạng thái và cửa sổ trở về hành vi xếp lớp thông thường khi tắt.

## US-07 — Tạm dừng, tiếp tục và dừng

**Là** người dùng, **tôi muốn** kiểm soát lúc ứng dụng đọc và dịch, **để** không xử lý nội dung ngoài ý muốn. Liên kết: FR-07, FEAT-05.

- **AC-07.1:** Given phiên đang dịch và có yêu cầu đang chờ; When bấm Tạm dừng; Then không nhận phụ đề mới, không gửi yêu cầu mới, giữ nội dung đã hiển thị và bỏ kết quả về muộn; Windows Live Captions không bị đóng.
- **AC-07.2:** Given phiên tạm dừng và nguồn còn khả dụng; When bấm Tiếp tục; Then tái kiểm tra nguồn, đọc nội dung hiện có/nội dung mới, tránh lặp đoạn đã biết và thông báo không khôi phục được nội dung đã trôi mất.
- **AC-07.3:** Given phiên đang chạy hoặc tạm dừng; When bấm Dừng; Then ngắt xử lý nguồn/dịch, giữ nội dung cho xem lại, bỏ kết quả đang chờ và không tự tiếp tục. Bắt đầu phiên khác khi có nội dung cũ phải theo xác nhận thay thế tại FEAT-05.

## US-08 — Khôi phục khi mất Live Captions

**Là** người dùng, **tôi muốn** biết khi nguồn phụ đề bị mất và kết nối lại, **để** tiếp tục theo dõi. Liên kết: FR-08, FEAT-01, FEAT-05.

- **AC-08.1:** Given đang dịch; When cửa sổ Live Captions đóng hoặc có tín hiệu không còn đọc được; Then chuyển Mất nguồn, dừng tạo yêu cầu mới, bỏ kết quả còn chờ và giữ nội dung đã hiển thị. Khoảng im lặng với cửa sổ còn đọc được không gây trạng thái Mất nguồn.
- **AC-08.2:** Given trạng thái Mất nguồn và Live Captions đã mở lại; When bấm Thử kết nối lại; Then tạo mốc kết nối nguồn mới, tiếp tục với văn bản hiện có, không nhân đôi phần nhận diện được là đã xử lý và hiển thị thông báo về khoảng gián đoạn; nếu vẫn thất bại, giữ trạng thái lỗi và nút thử lại.

## US-09 — Xử lý lỗi dịch

**Là** người dùng, **tôi muốn** thấy nguyên nhân dịch thất bại và chủ động thử lại, **để** tiếp tục mà không mất phụ đề gốc. Liên kết: FR-09, FEAT-03, FEAT-05.

- **AC-09.1:** Given có đoạn đang chờ dịch; When mất mạng, vượt timeout 10 giây hoặc dịch vụ báo lỗi; Then giữ văn bản gốc, đánh dấu lỗi phù hợp và không tự gọi lại vô hạn.
- **AC-09.2:** Given đoạn lỗi vẫn thuộc phiên, phiên đang chạy, đích của đoạn còn là đích hiện hành và kết nối đã phục hồi; When bấm Thử dịch lại; Then gửi đúng một yêu cầu cho phiên bản mới nhất của đoạn đó; nút bị khóa khi đang chờ để tránh bấm lặp. Đoạn thuộc đích cũ hướng dẫn chọn lại đích thay vì tự đổi đích.
- **AC-09.3:** Given dịch vụ báo hết hạn mức/lỗi xác thực hoặc 20 yêu cầu còn chờ; When có thêm đoạn ổn định; Then không gửi yêu cầu vượt giới hạn, giữ nguồn và báo Dịch tạm ngưng/Hệ thống bận; tiếp tục phải do người dùng chủ động khi điều kiện cho phép.

## US-10 — Xóa phiên và kết thúc sử dụng

**Là** người dùng, **tôi muốn** xóa nội dung đã dịch, **để** kiểm soát dữ liệu còn trong ứng dụng. Liên kết: FR-10, FEAT-05.

- **AC-10.1:** Given phiên có nội dung; When bấm Xóa phiên rồi Hủy trong hộp xác nhận; Then nội dung và trạng thái trước thao tác không đổi.
- **AC-10.2:** Given phiên có nội dung/yêu cầu đang chờ; When xác nhận Xóa phiên; Then dừng xử lý, xóa nguồn/bản dịch trong bộ nhớ phiên, về Sẵn sàng và không cho kết quả về muộn xuất hiện lại.
- **AC-10.3:** Given ứng dụng có dữ liệu phiên; When đóng rồi mở lại; Then không có nội dung phiên cũ được phục hồi từ lưu trữ ứng dụng; việc Windows hoặc dịch vụ ngoài giữ dữ liệu được đánh giá riêng theo NFR-04.

## Điều kiện hoàn thành một story

AC của story đạt trên môi trường đã chốt, NFR liên quan có bằng chứng, không còn lỗi làm sai ý nghĩa story và tài liệu khớp hành vi cuối cùng. Người kiểm thử/ngày/kết quả thực tế sẽ lưu trong `docs/testing/` ở giai đoạn sau; không ghi Passed khi chưa chạy.
