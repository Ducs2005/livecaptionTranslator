# livecaptionTranslator

Ứng dụng hỗ trợ trên Windows, đọc văn bản từ **Windows Live Captions** và dịch liên tục sang ngôn ngữ người dùng lựa chọn.

> Giai đoạn hiện tại: cấu trúc repo và đặc tả yêu cầu v0.1. **Chưa có code triển khai**. Mục tiêu sản phẩm đã được chủ dự án xác nhận; các chi tiết MVP, chỉ tiêu và giải pháp tích hợp là đề xuất chờ xác nhận.

## Bài toán

Người dùng đã có phụ đề do Windows Live Captions tạo ra nhưng cần bản dịch sang ngôn ngữ mong muốn. Dự án bổ sung lớp dịch và cửa sổ hiển thị bản dịch, tận dụng phụ đề có sẵn của Windows.

Khả năng dịch tích hợp của Windows phụ thuộc thiết bị, phiên bản và ngôn ngữ đích. Không giả định mọi phiên bản Windows đều thiếu tính năng dịch. Xem [nguồn và bối cảnh](docs/references/README.md).

## Bộ đặc tả theo yêu cầu giảng viên

| Mục | Tài liệu |
| --- | --- |
| 3.1 | [Khám phá Sản phẩm](docs/requirements/3.1-product-discovery.md) |
| 3.2 | [Tài liệu Yêu cầu Sản phẩm — PRD](docs/requirements/3.2-prd.md) |
| 3.3 | [Phân tích Yêu cầu](docs/requirements/3.3-requirements-analysis.md) |
| 3.4 | [User Stories & Tiêu chí Chấp nhận](docs/requirements/3.4-user-stories-acceptance-criteria.md) |
| 3.5 | [Đặc tả Tính năng](docs/requirements/3.5-feature-specification.md) |

## Cấu trúc repo

```text
livecaptionTranslator/
├── README.md
├── CONTRIBUTING.md
├── .gitignore
├── docs/
│   ├── README.md
│   ├── requirements/     # Bộ đặc tả 3.1–3.5
│   ├── design/           # Giữ chỗ cho thiết kế sau khi chốt yêu cầu
│   ├── testing/          # Giữ chỗ cho ca kiểm thử và bằng chứng nghiệm thu
│   └── references/       # Nguồn, đề bài và biên bản xác nhận
├── src/                 # Chỉ có thư mục nền, chưa có mã nguồn
│   ├── capture/         # Tích hợp đọc phụ đề Windows
│   ├── translation/     # Dịch văn bản
│   ├── presentation/    # Hiển thị và điều khiển
│   └── settings/        # Cấu hình người dùng
├── public/              # Thư mục tài nguyên có sẵn
└── LICENSE/             # Thư mục có sẵn, chưa chọn giấy phép
```

Các thư mục giữ chỗ dùng `.gitkeep` để Git theo dõi. Phân chia trong `src/` mang tính định hướng trách nhiệm, chưa chốt framework hoặc kiến trúc thực thi. Không có dependency, cấu hình build hoặc hướng dẫn chạy ở giai đoạn này.

## Phạm vi đề xuất

- Kết nối nguồn văn bản Windows Live Captions, không tự thu âm/nhận dạng giọng nói.
- Chọn ngôn ngữ đích trong danh sách được dịch vụ hỗ trợ; ưu tiên tiếng Việt và tiếng Anh để nghiệm thu MVP.
- Xử lý phụ đề đang thay đổi, hạn chế dịch trùng và loại kết quả cũ.
- Hiển thị bản dịch trong cửa sổ riêng có tùy chọn luôn nổi, tạm dừng/tiếp tục và phục hồi kết nối.

Cần kiểm chứng khả năng đọc Live Captions trên phiên bản Windows mục tiêu trước khi cam kết triển khai. Chưa chọn nhà cung cấp dịch, giấy phép hay thời hạn. Đề bài/rubric chi tiết của giảng viên chưa được cung cấp.
