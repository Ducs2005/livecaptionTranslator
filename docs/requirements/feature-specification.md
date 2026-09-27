# Đặc tả Tính năng (Feature Specification)

> v0.1 · 08/09/2026 · Dự thảo, không chứa code triển khai. Các mô hình bên dưới mô tả hành vi và dữ liệu logic, không chốt framework, API hay phương pháp đọc Windows Live Captions.

## 1. Phạm vi và thuật ngữ

- **Snapshot nguồn:** nội dung văn bản đọc được tại một thời điểm từ Live Captions.
- **Đoạn:** đơn vị văn bản được ứng dụng nhận diện trong luồng nguồn, có mã và thứ tự; không mặc định tương đương một dòng hiển thị.
- **Phiên bản đoạn:** số lần nội dung một đoạn thay đổi; bản sửa không trở thành một đoạn mới chỉ vì xuống dòng.
- **Đoạn ổn định:** đoạn đủ điều kiện gửi dịch theo FEAT-02; vẫn có thể bị Windows sửa sau đó.
- **Thế hệ xử lý:** mốc logic dùng để vô hiệu hóa công việc cũ khi tạm dừng, dừng, đổi đích, mất nguồn hoặc xóa.
- **Ngôn ngữ đích:** ngôn ngữ bản dịch người dùng chọn, thuộc danh sách dịch vụ hỗ trợ.

Quan hệ yêu cầu, story và tính năng xem tại [ma trận truy vết 3.3](requirements-analysis.md). Toàn bộ FEAT-01–FEAT-05 là Must đề xuất.

## 2. Trạng thái chung

### 2.1 Trạng thái phiên

| Trạng thái | Ý nghĩa | Thao tác/sự kiện → kết quả |
| --- | --- | --- |
| Sẵn sàng | Chưa đọc nguồn, chưa dịch | Bắt đầu với đích hợp lệ và thông báo được chấp nhận → Đang kết nối |
| Đang kết nối | Đang kiểm tra nguồn | Đọc được → Đang dịch; không có/không đọc được → Mất nguồn; Dừng → Đã dừng |
| Đang dịch | Đọc nguồn, chuẩn hóa và dịch nếu cổng dịch cho phép | Tạm dừng → Tạm dừng; Dừng → Đã dừng; mất nguồn → Mất nguồn |
| Tạm dừng | Không đọc mới/gửi dịch mới, giữ nội dung | Tiếp tục → Đang kết nối; Dừng → Đã dừng |
| Mất nguồn | Không đọc được nguồn, không gửi dịch mới | Thử kết nối lại → Đang kết nối; Dừng → Đã dừng |
| Đã dừng | Ngắt phiên xử lý, giữ nội dung | Bắt đầu → xác nhận thay thế nội dung cũ nếu có → Đang kết nối |

Xác nhận Xóa phiên từ bất kỳ trạng thái nào → vô hiệu hóa công việc cũ, xóa nội dung và về Sẵn sàng. Hủy xác nhận giữ trạng thái cũ. Đóng ứng dụng kết thúc mọi xử lý và loại dữ liệu phiên. Không tự mở/đóng Windows Live Captions.

### 2.2 Trạng thái dịch và đoạn

Trạng thái dịch độc lập với kết nối nguồn: **Sẵn sàng dịch**, **Đang có yêu cầu**, **Dịch tạm ngưng**. Dịch tạm ngưng do mạng/xác thực/hạn mức/quá tải vẫn có thể đọc nguồn nếu phiên đang ở Đang dịch, nhưng không gửi mới; giao diện phải hiển thị cả hai trạng thái để không gây hiểu nhầm.

Trạng thái đoạn: **Đang cập nhật → Chờ dịch → Đang dịch → Đã dịch / Lỗi dịch**. Đoạn bị sửa quay về Đang cập nhật; bản dịch cũ được gắn Hết hạn cho tới khi có bản dịch mới. Đoạn chưa được gửi do dịch tạm ngưng ghi **Chưa dịch**. Ngừng phiên không tự xóa các đoạn đã hoàn tất.

## 3. FEAT-01 — Kết nối Windows Live Captions

**Mục đích:** xác nhận nguồn phụ đề có thể đọc được trước khi dịch. Liên kết: FR-01, FR-08; US-01, US-08.

**Tiền điều kiện:** Windows mục tiêu nằm trong phạm vi kiểm chứng; người dùng có thể mở Live Captions. Đích dịch hợp lệ đã chọn; thông báo xử lý văn bản đã được chấp nhận trước khi bắt đầu.

**Đầu vào:** thao tác Bắt đầu/Thử kết nối lại; tình trạng nguồn trên hệ điều hành.

**Luồng chính:**

1. Chuyển Đang kết nối, vô hiệu hóa thao tác bắt đầu lặp.
2. Nhận diện đúng nguồn Windows Live Captions bằng phương pháp sẽ được kiểm chứng ở giai đoạn thiết kế.
3. Kiểm tra khả năng đọc độc lập với việc có văn bản hay không.
4. Khi đọc được, tạo mốc kết nối và chuyển Đang dịch; nguồn rỗng hiển thị Chờ phụ đề.
5. Chỉ chuyển văn bản mới/hợp lệ cho FEAT-02; không gửi toàn bộ văn bản ứng dụng khác.

**Ngoại lệ:** không tìm thấy nguồn → hướng dẫn mở Live Captions và thử lại; có nguồn nhưng không đọc được → thông báo lỗi truy cập/tương thích. Nếu sau 5 giây chưa xác định được nguồn, dừng lần kết nối và cho thử lại. Ngưỡng 5 giây là đề xuất cần xác nhận, không phải năng lực đã đo.

**Mất nguồn:** căn cứ cửa sổ không còn tồn tại hoặc bằng chứng đọc thất bại, không chỉ căn cứ thiếu cập nhật. Chuyển Mất nguồn, tăng thế hệ xử lý, bỏ các kết quả chưa hiển thị và giữ nội dung. Khi kết nối lại, chỉ nhập phần hiện còn đọc được, đánh dấu khoảng gián đoạn và tránh lặp phần đã nhận diện được.

**Hậu điều kiện:** nguồn kết nối rõ ràng hoặc lỗi có thể xử lý; không tiếp tục ngầm đọc khi phiên đã dừng.

**Điểm chưa chốt:** API/cơ chế đọc, tín hiệu mất nguồn, dữ liệu định danh vị trí đoạn, khả năng hoạt động khi cửa sổ thu nhỏ hoặc thay đổi bố cục. Phải kiểm chứng trên build mục tiêu; không giả định Windows cung cấp API phụ đề công khai.

## 4. FEAT-02 — Chuẩn hóa và theo dõi phụ đề

**Mục đích:** chuyển cập nhật phụ đề thành các đoạn có phiên bản để tránh dịch trùng/sai đoạn. Liên kết: FR-02; US-02; BR-01–BR-03.

**Tiền điều kiện:** phiên Đang dịch và nguồn đọc được. **Đầu vào:** snapshot hoặc sự kiện nguồn. **Đầu ra:** đoạn và phiên bản hiện hành có thứ tự, trạng thái ổn định.

### 4.1 Quy tắc xử lý

1. Giữ nguyên nội dung và dấu Unicode; chỉ chuẩn hóa khoảng trắng phục vụ đối chiếu, không tự sửa nghĩa hoặc dấu câu.
2. Bỏ snapshot rỗng và snapshot không đổi tại cùng vị trí nguồn.
3. Phân biệt nối/sửa đoạn hiện tại với đoạn mới dựa trên tiến trình nguồn; mỗi sửa đổi tăng phiên bản.
4. Dòng bị wrap do đổi kích thước không mặc nhiên là câu mới. Khi cuộn, phần chồng lặp không tạo lại đoạn.
5. Hai câu có văn bản giống nhau ở hai vị trí thực sự khác nhau phải được giữ cả hai. Không chống trùng bằng nội dung trên toàn phiên.
6. Khi thiếu thông tin để phân biệt reset/cuộn/câu lặp, ghi trạng thái nguồn không chắc chắn và thông báo khả năng thiếu/trùng; đây là giới hạn cần xử lý trước khi tuyên bố đạt NFR-02.

### 4.2 Điều kiện ổn định đề xuất

Một đoạn không rỗng được coi là ổn định khi nội dung không đổi trong **800 ms**, hoặc nguồn cung cấp dấu hiệu kết thúc đoạn đáng tin cậy đã được kiểm chứng. Không dùng dấu chấm đơn lẻ làm bằng chứng chắc chắn. Mốc 800 ms là tham số đặc tả tạm thời, cần xác nhận tại OQ-06.

Nếu đoạn bị sửa sau khi đã ổn định/gửi dịch: tăng phiên bản, gắn Hết hạn cho bản dịch cũ và chờ phiên bản mới ổn định. Chỉ yêu cầu cho phiên bản hiện hành được gửi; kết quả cho phiên bản cũ bị bỏ.

### 4.3 Ví dụ hành vi — dữ liệu minh họa

| Sự kiện nguồn | Kết quả mong đợi |
| --- | --- |
| “I like” rồi “I like this course” trong 800 ms | Một đoạn, cập nhật phiên bản, chưa dịch bản tạm |
| “I like this course” không đổi đủ 800 ms | Một yêu cầu dịch cho phiên bản ổn định |
| Snapshot trên được đọc lại không thay đổi | Không có yêu cầu mới |
| Cùng câu xuất hiện ở một vị trí mới sau đoạn khác | Một đoạn mới dù chuỗi ký tự giống nhau |
| Bản dịch cũ trả về sau khi nguồn sửa câu | Bỏ kết quả cũ, không ghi đè bản mới |

Ví dụ là hợp đồng hành vi cần kiểm thử, không phải chứng cứ phương pháp đọc đã phân biệt được mọi trường hợp.

## 5. FEAT-03 — Chọn ngôn ngữ và dịch

**Mục đích:** dịch đúng đoạn sang đích được chọn và kiểm soát lỗi/kết quả bất đồng bộ. Liên kết: FR-03, FR-04, FR-09; US-03, US-04, US-09.

### 5.1 Thiết lập

- Danh sách đích có tên dễ đọc và mã ngôn ngữ logic tương ứng; chỉ hiển thị/cho chọn mục được dịch vụ hỗ trợ.
- Chưa chọn đích hoặc danh sách lỗi: khóa Bắt đầu và cho hướng dẫn/chọn lại/thử nạp lại.
- Đề xuất danh sách nghiệm thu tối thiểu Việt và Anh; phạm vi cuối cùng cần xác nhận theo OQ-05.
- Ngôn ngữ nguồn do Windows tạo phụ đề. Tích hợp dịch phải cung cấp nguồn từ cấu hình đã xác nhận hoặc dùng phát hiện nguồn nếu dịch vụ hỗ trợ; không tự giả định API nào cũng tự nhận diện.

### 5.2 Gửi và nhận kết quả

1. Chỉ gửi khi phiên Đang dịch, cổng dịch không tạm ngưng và đoạn ổn định không rỗng.
2. Gắn yêu cầu với mã phiên, thế hệ xử lý, mã đoạn, phiên bản và mã đích.
3. Hiển thị trạng thái Đang dịch cho đoạn; nhận kết quả không làm đổi thứ tự các đoạn.
4. Chỉ áp dụng kết quả nếu tất cả thông tin định danh còn hợp lệ. Không dùng kết quả phiên cũ, đích cũ hoặc phiên bản cũ cho nội dung mới.
5. Kết quả rỗng/không hợp lệ được xử lý như lỗi, không giả hiển thị là dịch thành công.
6. Khi biết nguồn trùng đích, giữ nguyên văn bản với nhãn Cùng ngôn ngữ, không gọi dịch.

### 5.3 Đổi đích khi đang hoạt động

Tăng thế hệ xử lý, vô hiệu hóa mọi kết quả đang chờ. Đoạn hiện tại được yêu cầu dịch sang đích mới khi ổn định; các đoạn mới dùng đích mới. Lịch sử đã hoàn tất giữ kết quả và nhãn đích cũ. Đoạn lịch sử còn chờ bị đánh dấu Hết hạn, không tự dịch hàng loạt. Khi đang tạm dừng/đã dừng, việc chọn đích không tạo yêu cầu tới khi tiếp tục/bắt đầu.

### 5.4 Timeout, hạn mức và tải

- Timeout đề xuất: **10 giây mỗi yêu cầu**. Sau timeout, đánh dấu Lỗi dịch; phản hồi đến sau timeout không được áp dụng.
- Tối đa đề xuất **20 yêu cầu chưa hoàn tất**. Khi đạt ngưỡng, ngừng gửi mới và báo Hệ thống bận; đoạn tiếp theo ghi Chưa dịch. Không tích lũy một hàng đợi tự gửi lại không giới hạn.
- Khi phát hiện mất mạng, lỗi xác thực hoặc hết hạn mức, chuyển Dịch tạm ngưng để không tiếp tục gửi yêu cầu chắc chắn thất bại. Những yêu cầu đã gửi vẫn được xử lý theo quy tắc hợp lệ/timeout.
- Hành động **Tiếp tục dịch** chỉ hoạt động khi phiên Đang dịch, nguồn còn đọc được và nguyên nhân đã được xử lý; thử với đoạn hiện tại, sau đó nhận đoạn mới, không tự gửi lại toàn bộ lịch sử.
- **Thử dịch lại** trên một đoạn lỗi gửi đúng một yêu cầu; khóa nút khi chờ. Chỉ cho phép nếu đích của đoạn bằng đích hiện hành và phiên đang chạy. Lỗi thuộc đích cũ yêu cầu người dùng chọn lại đích; không thay đổi đích ngầm.
- Lỗi dịch riêng lẻ không làm mất văn bản nguồn. Không đưa thông tin bí mật hoặc phản hồi kỹ thuật nguyên văn vào thông báo người dùng.

## 6. FEAT-04 — Cửa sổ bản dịch

**Mục đích:** đọc bản dịch trong lúc dùng ứng dụng khác. Liên kết: FR-05, FR-06; US-05, US-06; NFR-05, NFR-06.

### 6.1 Thành phần giao diện

| Vùng | Nội dung |
| --- | --- |
| Thanh trạng thái | Nguồn đã kết nối/chờ phụ đề/mất nguồn; trạng thái dịch; đích hiện hành |
| Điều khiển | Bắt đầu, Tạm dừng/Tiếp tục, Dừng, chọn đích, Luôn nổi, Xóa phiên |
| Nội dung | Đoạn nguồn và bản dịch, nhãn ngôn ngữ, trạng thái từng đoạn, thứ tự ổn định |
| Phản hồi lỗi | Thông điệp theo nguyên nhân; Thử kết nối lại, Tiếp tục dịch hoặc Thử dịch lại theo ngữ cảnh |

### 6.2 Hành vi hiển thị

- Cửa sổ riêng của ứng dụng, không sửa hoặc chèn vào UI của Windows Live Captions.
- Cho di chuyển và đổi kích thước; mức tối thiểu đề xuất **360 × 240 đơn vị độc lập DPI**, cần kiểm chứng không che điều khiển ở tỷ lệ hiển thị của máy nghiệm thu.
- Luôn nổi mặc định tắt; bật theo ý người dùng và không tự giành focus khi cập nhật văn bản. Không cam kết xuất hiện trên màn hình bảo mật hoặc chế độ toàn màn hình độc quyền.
- Đoạn dài tự xuống dòng; có cuộn lịch sử phiên. Nếu đang ở cuối thì theo nội dung mới; nếu người dùng cuộn lên thì giữ vị trí và hiển thị Về mới nhất.
- Bản dịch cũ của đoạn đang sửa phải gắn Hết hạn, không hiển thị như bản dịch hiện hành.
- Văn bản nguồn/đích, lỗi và trạng thái được phân biệt bằng nhãn chữ; điều khiển chính có focus và dùng được bằng bàn phím.

**Hậu điều kiện:** người dùng biết đang đọc ngôn ngữ nào, nội dung thuộc đoạn nào và tình trạng xử lý mà không cần xem log.

## 7. FEAT-05 — Điều khiển, phục hồi và xóa phiên

**Mục đích:** kiểm soát việc đọc/dịch và dữ liệu đang giữ. Liên kết: FR-07–FR-10; US-07–US-10.

| Hành động | Tiền điều kiện | Kết quả |
| --- | --- | --- |
| Tạm dừng | Đang dịch | Ngừng nhận nguồn/gửi mới, tăng thế hệ xử lý, bỏ kết quả về muộn; giữ văn bản |
| Tiếp tục | Tạm dừng | Tái kiểm tra nguồn, đọc phần hiện có và mới, thông báo khoảng gián đoạn |
| Dừng | Đang kết nối/Đang dịch/Tạm dừng/Mất nguồn | Ngắt xử lý, tăng thế hệ, giữ nội dung, về Đã dừng |
| Bắt đầu phiên mới | Sẵn sàng hoặc Đã dừng | Nếu còn nội dung, hỏi xác nhận thay thế; Đồng ý xóa nội dung cũ và kết nối mới, Hủy giữ nguyên |
| Thử kết nối lại | Mất nguồn | Kiểm tra nguồn mới, không tự nhận lại phần đã mất |
| Tiếp tục dịch | Phiên Đang dịch, cổng dịch tạm ngưng | Kiểm tra khả năng dịch bằng đoạn hiện tại, mở lại việc dịch khi thành công |
| Xóa phiên | Có nội dung hoặc phiên đang hoạt động | Hiển thị xác nhận; Đồng ý dừng xử lý/xóa/về Sẵn sàng, Hủy không thay đổi |
| Đóng ứng dụng | Bất kỳ | Dừng công việc, loại dữ liệu phiên, không tự tắt Live Captions |

Không có xuất hoặc lưu lịch sử trong MVP. Xóa trong ứng dụng không đồng nghĩa xóa nội dung tại Windows hoặc nhà cung cấp đã nhận yêu cầu; giới hạn này phải được nêu trong thông báo xử lý dữ liệu.

## 8. Hợp đồng dữ liệu logic và riêng tư

Một yêu cầu dịch chỉ cần văn bản, thông tin ngôn ngữ và metadata cần cho ghép kết quả; mã nội bộ không bắt buộc phải gửi ra ngoài nếu có thể đối chiếu cục bộ. Không gửi âm thanh, ảnh màn hình hoặc văn bản cửa sổ khác như một phần của tính năng dịch.

Nếu giải pháp lấy nguồn sau này cần ảnh màn hình cục bộ, phải được đánh giá và cập nhật đặc tả trước; không coi OCR hay thu ảnh là đã được chọn. Nội dung phụ đề chứa câu mệnh lệnh vẫn được coi là dữ liệu dịch, không có quyền thay đổi cấu hình hoặc kích hoạt thao tác.

Thông báo trước khi bắt đầu cần nêu: đọc văn bản từ Live Captions, tên/nơi xử lý của dịch vụ dịch thực tế, chính sách giữ dữ liệu đã xác nhận và cách tạm dừng/dừng. Hiện chưa chọn dịch vụ nên chưa thể khẳng định nơi xử lý hoặc thời gian lưu của bên ngoài.

## 9. Danh mục lỗi và hành động

| Mã | Điều kiện | Thông điệp mẫu | Hành động |
| --- | --- | --- | --- |
| ERR-01 | Không có nguồn | Chưa tìm thấy Windows Live Captions | Mở nguồn, Thử kết nối lại |
| ERR-02 | Có nguồn nhưng không đọc được | Không đọc được nội dung phụ đề trên cấu hình hiện tại | Kiểm tra cấu hình hỗ trợ, thử lại |
| ERR-03 | Mất nguồn khi chạy | Kết nối phụ đề đã bị gián đoạn | Giữ nội dung, kết nối lại |
| ERR-04 | Mất mạng/timeout | Chưa nhận được bản dịch. Hãy kiểm tra kết nối và thử lại | Thử dịch lại/Tiếp tục dịch |
| ERR-05 | Xác thực/hạn mức | Dịch vụ dịch chưa sẵn sàng hoặc đã hết hạn mức | Tạm ngưng dịch, kiểm tra cấu hình dịch vụ |
| ERR-06 | Đích không hợp lệ | Ngôn ngữ này chưa được hỗ trợ | Chọn đích hợp lệ |
| ERR-07 | Đủ 20 yêu cầu chờ | Hệ thống đang bận, dịch mới đã tạm ngưng | Chờ yêu cầu hiện có kết thúc, Tiếp tục dịch |
| ERR-08 | Không xác định được ranh giới nguồn | Có thể thiếu hoặc lặp đoạn sau khi nguồn thay đổi | Báo giới hạn, tái kết nối khi cần |

## 10. Điều kiện kiểm chứng đặc tả

Chạy AC của [3.4](user-stories-acceptance-criteria.md), NFR của [3.3](requirements-analysis.md) và lưu bằng chứng trong `docs/testing/`. Bộ mẫu phải bao gồm nguồn rỗng, cập nhật/sửa câu, câu lặp thật, cuộn/reset, trả dịch sai thứ tự, đổi đích giữa yêu cầu, dừng/xóa với kết quả đến muộn, lỗi mạng, hạn mức và nguồn đóng/mở lại.

Ngưỡng 800 ms, 5 giây, 10 giây, 20 yêu cầu và kích thước cửa sổ là đề xuất để tài liệu có thể nghiệm thu; cần xác nhận hoặc điều chỉnh có ghi nhận trước khi lập trình. Chưa có kết quả thử nghiệm sản phẩm trong repo này.
