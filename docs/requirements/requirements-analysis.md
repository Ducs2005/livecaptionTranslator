# Phân tích Yêu cầu

> v0.1 · 08/09/2026 · Dự thảo. Cơ sở: [PRD](prd.md). Mọi yêu cầu dưới đây là đặc tả đề xuất cho MVP, chưa phải hành vi đã triển khai.

## 1. Phạm vi hệ thống và tác nhân

- **Người dùng:** mở nguồn, chọn ngôn ngữ đích, điều khiển cửa sổ/phiên dịch.
- **Windows Live Captions:** hệ thống ngoài cung cấp văn bản phụ đề; tự xử lý âm thanh.
- **Dịch vụ dịch:** hệ thống ngoài nhận văn bản và ngôn ngữ đích, trả bản dịch hoặc lỗi; chưa lựa chọn.
- **livecaptionTranslator:** đọc nguồn → chuẩn hóa đoạn → yêu cầu dịch → ghép đúng kết quả → hiển thị.

Không yêu cầu truy cập microphone, thay đổi phụ đề Windows hoặc điều khiển ứng dụng phát âm thanh. Hình thức tích hợp nguồn chưa được chứng minh.

## 2. Yêu cầu chức năng

Tất cả FR trong bảng là Must cho phạm vi MVP đề xuất.

| Mã | Yêu cầu | Đầu vào/điều kiện | Kết quả quan sát được |
| --- | --- | --- | --- |
| FR-01 | Kiểm tra và kết nối nguồn Windows Live Captions khi người dùng bắt đầu | Người dùng chọn đích, chấp nhận thông báo xử lý dữ liệu | Phân biệt không tìm thấy, không đọc được, đã kết nối nhưng chưa có văn bản |
| FR-02 | Nhận và chuẩn hóa các lần cập nhật phụ đề | Nguồn đang kết nối | Đoạn có thứ tự và phiên bản; không nhân đôi do snapshot lặp; giữ câu lặp thật |
| FR-03 | Cho chọn ngôn ngữ đích từ danh sách hỗ trợ và đổi khi đang chạy | Danh sách dịch vụ đã xác nhận | Đích hiện hành rõ ràng; mục không hỗ trợ không được gửi dịch |
| FR-04 | Dịch đoạn ổn định và xử lý kết quả theo phiên bản | Văn bản không rỗng, dịch đang bật | Bản dịch đúng đoạn, đúng đích; kết quả cũ không ghi đè mới |
| FR-05 | Hiển thị phụ đề gốc, bản dịch và trạng thái đoạn | Đoạn đã nhận/kết quả dịch | Phân biệt đang cập nhật, chờ dịch, đã dịch, lỗi và hết hạn |
| FR-06 | Cung cấp cửa sổ có thể di chuyển, đổi kích thước và bật/tắt luôn nổi | Ứng dụng đang mở | Văn bản vẫn đọc được khi chuyển sang ứng dụng cửa sổ khác |
| FR-07 | Tạm dừng, tiếp tục và dừng phiên theo thao tác người dùng | Phiên đang hoạt động hoặc tạm dừng | Không nhận/gửi mới ngoài trạng thái cho phép; không tự đóng Live Captions |
| FR-08 | Phát hiện mất nguồn và cho kết nối lại | Cửa sổ nguồn đóng hoặc không còn đọc được | Giữ nội dung, báo lỗi; không hứa lấy lại phần đã trôi mất |
| FR-09 | Xử lý lỗi dịch và cho thử lại có kiểm soát | Mất mạng, timeout, hạn mức hoặc kết quả không hợp lệ | Giữ đoạn gốc, thông báo dễ hiểu; không tự thử vô hạn |
| FR-10 | Xóa dữ liệu phiên theo yêu cầu và khi thoát | Xác nhận xóa hoặc đóng ứng dụng | Nội dung bị loại khỏi phiên; kết quả đến muộn không làm xuất hiện lại |

## 3. Yêu cầu phi chức năng

Ngưỡng dưới đây là đề xuất. Trước kiểm thử phải ghi phiên bản Windows/build, phần cứng, kích thước cửa sổ Live Captions, ngôn ngữ nguồn/đích, nhà cung cấp, điều kiện mạng và phiên bản ứng dụng. Không báo đạt nếu chưa thực đo.

| Mã | Thuộc tính và chỉ tiêu đề xuất | Cách nghiệm thu |
| --- | --- | --- |
| NFR-01 | Hiệu năng: P95 từ lúc đoạn nguồn đáp ứng điều kiện ổn định đến khi bản dịch đúng phiên bản hiển thị ≤ 3 giây | ≥ 100 đoạn cho mỗi cặp ngôn ngữ nghiệm thu; ghi mốc thời gian, số thành công/lỗi/timeout. P95 tính trên đoạn thành công và tỷ lệ thành công phải ≥ 95%; không loại lỗi khỏi báo cáo để tuyên bố đạt |
| NFR-02 | Độ tin cậy đọc nguồn: ≥ 99% đoạn ổn định trong bộ mẫu được lấy đúng và đúng một lần; phiên 15 phút không treo/crash | Bộ mẫu ≥ 100 đoạn có sửa chữ, nối câu, snapshot lặp, câu lặp thật, cuộn và reset cửa sổ; đối chiếu văn bản hiển thị của Windows sau chuẩn hóa khoảng trắng, không chấm chất lượng nhận dạng âm thanh |
| NFR-03 | Chất lượng dịch: ≥ 80% đoạn đạt mức 2 hoặc 3 trong rubric bên dưới, cho từng cặp kiểm thử | ≥ 50 đoạn/cặp, có ngữ cảnh đời thường và học tập; hai người có khả năng đọc hai ngôn ngữ chấm độc lập, trao đổi các điểm khác nhau và lưu kết quả thống nhất |
| NFR-04 | Riêng tư: không tự thu âm, không ghi nội dung phụ đề/bản dịch vào file log hoặc lưu bền vững của ứng dụng; chỉ gửi văn bản cần dịch sau khi người dùng bắt đầu và đã được thông báo | Kiểm tra lưu trữ/log/luồng dữ liệu; thử dừng, xóa, thoát. Phải xác nhận chính sách giữ dữ liệu của nhà cung cấp trước dùng dữ liệu thật; không suy diễn rằng nhà cung cấp không lưu |
| NFR-05 | Khả dụng: mọi thao tác chính dùng được bằng bàn phím, có focus rõ, trạng thái không chỉ phân biệt bằng màu | Kiểm tra bắt đầu, chọn đích, tạm dừng, tiếp tục, dừng, luôn nổi, thử lại và xóa bằng bàn phím; ≥ 4/5 người thử bắt đầu trong 60 giây khi nguồn sẵn sàng |
| NFR-06 | Tương thích: đạt toàn bộ AC trên ít nhất một cấu hình Windows 11/build được công bố; không cần quyền quản trị trong vận hành thông thường | Ghi cấu hình chính xác và quyền tài khoản, thử nguồn đóng/mở lại và ứng dụng cửa sổ khác; nếu cần quyền cao hơn phải đánh giá lại yêu cầu trước chốt thiết kế |

Rubric dịch: **0** = sai nghĩa chính/không có bản dịch; **1** = giữ một phần nhưng thiếu hoặc sai ý quan trọng; **2** = giữ ý chính, có lỗi nhỏ không đảo nghĩa; **3** = đúng ý chính, đủ ý và dễ đọc. Không chấm sai số nhận dạng Windows như lỗi của bộ lấy phụ đề; chất lượng dịch đánh giá theo văn bản nguồn thực sự nhận được.

## 4. Quy tắc nghiệp vụ

| Mã | Quy tắc |
| --- | --- |
| BR-01 | Chỉ đọc văn bản thuộc nguồn Windows Live Captions được kết nối; không lấy văn bản tùy ý từ ứng dụng khác |
| BR-02 | Chỉ dịch đoạn không rỗng, đã ổn định theo FEAT-02; snapshot không đổi không tạo lần dịch mới |
| BR-03 | Mỗi kết quả gắn với phiên, thế hệ xử lý, đoạn, phiên bản văn bản và ngôn ngữ đích; chỉ kết quả còn hợp lệ mới cập nhật giao diện |
| BR-04 | Đổi đích chỉ dịch lại đoạn hiện tại và áp dụng cho đoạn mới; lịch sử giữ bản dịch và nhãn đích cũ. Ngôn ngữ nguồn trùng đích thì hiển thị nguyên văn kèm nhãn khi xác định được |
| BR-05 | Tạm dừng/dừng ngăn nhận nguồn và yêu cầu dịch mới; yêu cầu đã gửi có thể chưa hủy được ở nhà cung cấp nhưng kết quả về sau bị bỏ qua |
| BR-06 | Tiếp tục/kết nối lại không bù dữ liệu không còn trong nguồn; giao diện thông báo có thể thiếu đoạn trong thời gian gián đoạn |
| BR-07 | Xóa phiên phải xác nhận, chuyển về trạng thái sẵn sàng và vô hiệu hóa kết quả đang chờ; thoát xóa dữ liệu phiên cục bộ |
| BR-08 | Lỗi dịch không xóa văn bản nguồn; thử lại chỉ thực hiện khi người dùng yêu cầu, theo phiên bản và đích hiện hành của đoạn được phép thử |
| BR-09 | Văn bản phụ đề là dữ liệu để dịch, không phải chỉ dẫn điều khiển hệ thống. Dịch vụ dựa trên mô hình ngôn ngữ cũng phải tuân thủ ranh giới này |

## 5. Thực thể nghiệp vụ và vòng đời dữ liệu

| Thực thể | Thuộc tính khái niệm | Vòng đời |
| --- | --- | --- |
| Phiên dịch | Mã phiên, trạng thái, nguồn, đích hiện hành, thế hệ xử lý | Từ Bắt đầu tới xóa/thoát; Dừng giữ nội dung cho xem lại |
| Đoạn phụ đề | Mã đoạn, thứ tự, phiên bản, văn bản gốc, thời điểm nhận, trạng thái ổn định | Bộ nhớ phiên, xóa theo FR-10 |
| Kết quả dịch | Mã đoạn/phiên bản/đích/thế hệ, văn bản dịch, trạng thái lỗi | Bộ nhớ phiên; kết quả lỗi thời không được áp dụng |
| Lựa chọn giao diện | Đích đang chọn, luôn nổi, kích thước/vị trí | Chỉ trong lần chạy ở MVP; lưu qua lần mở là Should |

Đây là mô hình dữ liệu logic, không yêu cầu tạo database, lớp hoặc schema ở giai đoạn tài liệu.

## 6. Ma trận truy vết

| Mục tiêu | Yêu cầu | User story / AC | Tính năng | NFR liên quan |
| --- | --- | --- | --- | --- |
| G-01 | FR-01 | US-01 / AC-01.1–AC-01.4 | FEAT-01 | NFR-05, NFR-06 |
| G-03 | FR-02 | US-02 / AC-02.1–AC-02.4 | FEAT-02 | NFR-02 |
| G-01, G-04 | FR-03 | US-03 / AC-03.1–AC-03.3 | FEAT-03 | NFR-05 |
| G-02, G-03 | FR-04 | US-04 / AC-04.1–AC-04.3 | FEAT-03 | NFR-01, NFR-03 |
| G-01 | FR-05 | US-05 / AC-05.1–AC-05.2 | FEAT-04 | NFR-05 |
| G-01 | FR-06 | US-06 / AC-06.1–AC-06.2 | FEAT-04 | NFR-05, NFR-06 |
| G-04 | FR-07 | US-07 / AC-07.1–AC-07.3 | FEAT-05 | NFR-04 |
| G-04 | FR-08 | US-08 / AC-08.1–AC-08.2 | FEAT-01, FEAT-05 | NFR-02, NFR-06 |
| G-04 | FR-09 | US-09 / AC-09.1–AC-09.3 | FEAT-03, FEAT-05 | NFR-01 |
| G-04 | FR-10 | US-10 / AC-10.1–AC-10.3 | FEAT-05 | NFR-04 |

## 7. Xung đột và quyết định đề xuất

- **Dịch nhanh và phụ đề chưa ổn định:** dùng cửa sổ ổn định theo FEAT-02 để giảm gọi dịch, chấp nhận độ trễ bổ sung đã tính trong phép đo.
- **Câu lặp và chống trùng:** chống trùng theo vị trí/phiên bản trong luồng; không loại câu chỉ vì có văn bản giống một câu cũ.
- **Chọn ngôn ngữ bất kỳ và năng lực dịch vụ:** chỉ công bố danh sách thực sự hỗ trợ, bổ sung ngôn ngữ sau khi xác nhận.
- **Tiện ích dịch trực tuyến và dữ liệu riêng tư:** thông báo trước lần bắt đầu, không lưu bền vững tại ứng dụng; đánh giá riêng nhà cung cấp.

## 8. Quyết định mở và người cần xác nhận

| Mã | Cần quyết định | Bên xác nhận | Điều kiện phải chốt |
| --- | --- | --- | --- |
| OQ-01 | Rubric, mẫu nộp, thời hạn và nhóm | Giảng viên/chủ dự án | Trước duyệt bộ yêu cầu |
| OQ-02 | Windows build, thiết bị, cấu hình Live Captions | Chủ dự án/nhóm | Trước kiểm chứng tích hợp |
| OQ-03 | Phương pháp đọc nguồn, nhận diện đoạn và phát hiện mất kết nối | Nhóm kỹ thuật | Trước thiết kế; chưa viết code trong lần bàn giao này |
| OQ-04 | Nhà cung cấp, hạn mức, ngân sách, chính sách dữ liệu, ngôn ngữ nguồn | Chủ dự án/nhóm | Trước tích hợp dịch vụ |
| OQ-05 | Danh sách ngôn ngữ đích MVP, các cặp thử | Chủ dự án/giảng viên | Trước khóa phạm vi; đề xuất Anh → Việt và Việt → Anh nếu nguồn hỗ trợ |
| OQ-06 | Ngưỡng ổn định/timeout, hiệu năng, cỡ cửa sổ và bộ mẫu | Nhóm/giảng viên | Trước thiết kế chi tiết và nghiệm thu |

Mọi thay đổi sau khi chốt phải ghi lý do, tác động tới FR/US/FEAT/NFR và người xác nhận trong lịch sử tài liệu.
