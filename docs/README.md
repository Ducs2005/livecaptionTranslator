# Hồ sơ tài liệu

- Phiên bản: 0.1 — ngày 08/09/2026.
- Trạng thái: dự thảo; chưa được giảng viên phê duyệt.
- Chủ sở hữu: nhóm dự án, chưa điền thành viên.
- Người phê duyệt và hạn nộp: chưa xác định.
- Đã xác nhận từ chủ dự án: dịch văn bản của Windows Live Captions sang ngôn ngữ người dùng muốn.
- Chưa xác nhận: rubric chi tiết, bản Windows, kỹ thuật lấy phụ đề, dịch vụ dịch, danh sách ngôn ngữ, chỉ tiêu hiệu năng.

## Thứ tự đọc

1. [3.1 Khám phá Sản phẩm](requirements/product-discovery.md): vấn đề, người dùng và giả thuyết cần kiểm chứng.
2. [3.2 PRD](requirements/prd.md): mục tiêu, phạm vi và điều kiện bàn giao.
3. [3.3 Phân tích Yêu cầu](requirements/requirements-analysis.md): yêu cầu, quy tắc, ưu tiên và ma trận truy vết.
4. [3.4 User Stories & Tiêu chí Chấp nhận](requirements/user-stories-acceptance-criteria.md): các tình huống nghiệm thu.
5. [3.5 Đặc tả Tính năng](requirements/feature-specification.md): luồng, trạng thái, dữ liệu và lỗi.

## Quy ước

`FR`: yêu cầu chức năng; `NFR`: yêu cầu phi chức năng; `BR`: quy tắc nghiệp vụ; `US`: user story; `AC`: tiêu chí chấp nhận; `FEAT`: tính năng. Mã giữ ổn định khi sửa tài liệu. Các mục tiêu định lượng đều là đề xuất cần xác nhận, không phải kết quả đã đo.

`design/` dành cho thiết kế sau khi chốt yêu cầu; `testing/` dành cho kế hoạch chi tiết và bằng chứng thực tế; [references/](references/README.md) lưu nguồn, đề bài và xác nhận. Hiện chưa có báo cáo khảo sát hoặc kiểm thử sản phẩm.

## Lịch sử

| Phiên bản | Ngày | Nội dung | Phê duyệt |
| --- | --- | --- | --- |
| 0.1 | 08/09/2026 | Cấu trúc repo và đặc tả 3.1–3.5 theo mục tiêu dịch Windows Live Captions | Chưa phê duyệt |


## UI cơ bản — 19/09/2026

Chủ dự án đã yêu cầu tạo nhánh dev và triển khai UI cơ bản trên nhánh tính năng. Xem [phạm vi UI](design/basic-ui.md) và [kết quả kiểm tra](testing/basic-ui-checks.md). Các đặc tả yêu cầu tiếp tục là dự thảo mục tiêu, không đồng nghĩa tích hợp thật đã hoàn thành.
