# livecaptionTranslator

Ứng dụng Windows hỗ trợ đọc bản dịch của **Windows Live Captions** sang ngôn ngữ người dùng lựa chọn.

> Hiện có **UI desktop cơ bản bằng C# / .NET 10 WPF**, dùng 6 câu mẫu cục bộ để trải nghiệm. Chưa kết nối Windows Live Captions, chưa thu âm và chưa gọi dịch vụ dịch thật.

## Chạy UI

Yêu cầu: Windows với **.NET 10 SDK** có thành phần Windows Desktop. Không dùng thư viện NuGet bên thứ ba.

Tại thư mục gốc repo:

```powershell
dotnet run --project src/presentation/LiveCaptionTranslator.App
```

Build bản Release:

```powershell
dotnet build src/presentation/LiveCaptionTranslator.App --configuration Release
```

Chạy kiểm tra logic demo:

```powershell
dotnet run --project tests/LiveCaptionTranslator.Checks --configuration Release
```

Bản build nằm trong `src/presentation/LiveCaptionTranslator.App/bin/Release/net10.0-windows/`. Đây là bản framework-dependent, cần .NET 10 Desktop Runtime nếu chạy trên máy chỉ có runtime; giữ các file build cùng nhau. Chưa có bộ cài hoặc bản phát hành đóng gói.

## Trải nghiệm hiện có

- Bắt đầu phiên demo: một câu ngay lập tức, sau đó thêm một câu mỗi 4 giây, kết thúc sau 6 câu.
- Chọn Tiếng Việt hoặc English. Các bản dịch là văn bản mẫu đã soạn sẵn; English dùng lại câu nguồn.
- Tạm dừng, tiếp tục và dừng; xác nhận trước khi xóa hoặc thay phiên có nội dung.
- Hiện/ẩn bản gốc, chỉnh cỡ chữ, cuộn xem lịch sử và trở về đoạn mới nhất.
- Ghi nhớ ngôn ngữ đích và tùy chọn hiển thị trong `%LOCALAPPDATA%/LiveCaptionTranslator/preferences.json`; không lưu nội dung phụ đề.
- Di chuyển, đổi kích thước cửa sổ; tùy chọn Luôn nổi hoạt động bằng cửa sổ Windows thật.
- Nội dung chỉ tồn tại trong bộ nhớ, không gửi mạng hoặc lưu lịch sử.

Xem [phạm vi UI](docs/design/basic-ui.md) và [kết quả kiểm tra](docs/testing/basic-ui-checks.md). UI cơ bản chưa phải bản nghiệm thu các FR/NFR của sản phẩm hoàn chỉnh.

## Nhánh phát triển

- `dev`: nhánh tích hợp thay đổi.
- `main`: nhánh ổn định nhận các thay đổi đã tích hợp.

## Tài liệu theo yêu cầu học phần

| Mục | Tài liệu |
| --- | --- |
| 3.1 | [Khám phá Sản phẩm](docs/requirements/product-discovery.md) |
| 3.2 | [Tài liệu Yêu cầu Sản phẩm — PRD](docs/requirements/prd.md) |
| 3.3 | [Phân tích Yêu cầu](docs/requirements/requirements-analysis.md) |
| 3.4 | [User Stories & Tiêu chí Chấp nhận](docs/requirements/user-stories-acceptance-criteria.md) |
| 3.5 | [Đặc tả Tính năng](docs/requirements/feature-specification.md) |

## Cấu trúc

```text
src/
  capture/                           # Giữ chỗ cho tích hợp Live Captions
  translation/                       # Giữ chỗ cho dịch vụ dịch
  settings/                          # Giữ chỗ cho cấu hình sau này
  presentation/
    LiveCaptionTranslator.App/
      App.xaml                       # Tài nguyên giao diện
      MainWindow.xaml                # Cửa sổ chính
      MainWindow.xaml.cs             # Tương tác cửa sổ và timer demo
      Models/                        # Trạng thái phiên, tùy chọn và câu mẫu
      Services/                      # Lưu tùy chọn cục bộ dạng JSON
tests/
  LiveCaptionTranslator.Checks/      # Kiểm tra logic, không cần framework test ngoài
docs/
  requirements/                      # Đặc tả 3.1–3.5
  design/                            # Phạm vi UI và quyết định thiết kế
  testing/                           # Bằng chứng kiểm tra
  references/                        # Nguồn và căn cứ yêu cầu
```

## Hướng phát triển

Kiểm chứng cách đọc văn bản Live Captions trên Windows mục tiêu, chọn nhà cung cấp dịch và ngôn ngữ hỗ trợ, sau đó thay dữ liệu demo bằng tích hợp thật. Không giả định mọi phiên bản Windows đều thiếu tính năng dịch; xem [bối cảnh và nguồn](docs/references/README.md).

Chưa chọn giấy phép; thư mục `LICENSE/` là phần nền có sẵn, không phải văn bản cấp phép.
