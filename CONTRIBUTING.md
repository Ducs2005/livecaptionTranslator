# Quy ước đóng góp

Giai đoạn hiện tại đã được chủ dự án yêu cầu triển khai UI cơ bản. UI dùng dữ liệu demo có nhãn rõ ràng; tích hợp Windows Live Captions và dịch vụ dịch thật thuộc giai đoạn tiếp theo.

- Viết tài liệu bằng tiếng Việt, UTF-8; giải thích thuật ngữ tiếng Anh ở lần xuất hiện đầu.
- Giữ nguyên mã yêu cầu `FR`, `NFR`, `BR`, mã user story `US` và mã tính năng `FEAT` khi chỉnh sửa nội dung. Không tái sử dụng mã đã bỏ.
- Khi thay đổi phạm vi, cập nhật đồng thời PRD, yêu cầu, user story và đặc tả tính năng liên quan.
- Phân biệt rõ giả định, đề xuất, bằng chứng thực tế và quyết định đã xác nhận.
- Với thay đổi yêu cầu, mô tả lý do, tài liệu bị ảnh hưởng và cách nghiệm thu trong pull request.
- Không đưa API key, âm thanh riêng tư, nội dung cuộc họp thật hoặc dữ liệu cá nhân vào repo.
- Chỉ bổ sung nguồn, kết quả phỏng vấn và bằng chứng kiểm thử có thật. Dữ liệu mô phỏng phải được gắn nhãn.

Trước khi bàn giao: kiểm tra liên kết Markdown, tính nhất quán các mã, phạm vi MVP và các tiêu chí Given/When/Then. Việc phê duyệt yêu cầu do nhóm và giảng viên xác nhận; cập nhật tên người duyệt và ngày duyệt khi có quyết định thực tế.

## Quy trình nhánh và kiểm tra UI

- Tạo nhánh tính năng từ nhánh dev; UI cơ bản ở nhánh codex/basic-ui.
- Build Release và chạy tests/LiveCaptionTranslator.Checks trước khi bàn giao.
- Không commit bin/, obj/, cấu hình cục bộ hoặc khóa dịch vụ.
- Kiểm tra giao diện thật trên Windows; ghi rõ phần đã thử và phần chưa thử trong docs/testing/.
