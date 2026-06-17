---
name: build-and-run
description: "Use when: building, running, debugging, or verifying the WinForms C# computer graphics project."
---

# Build and Run Workflow

## Mục tiêu
Cung cấp quy trình chuẩn để build, chạy và kiểm tra dự án WinForms C# trong workspace.

## Lệnh khuyến nghị
Từ thư mục gốc workspace `/workspaces/computer-graphics`:

```bash
dotnet build Project_CG_Paint.slnx
```

Nếu `dotnet build .slnx` gặp vấn đề, thử:

```bash
dotnet build Project_CG_Paint.csproj
```

## Kiểm tra .NET
```bash
dotnet --info
dotnet --list-sdks
```

Nếu thiếu SDK, cần thông báo rõ cho người dùng và không tự giả định phiên bản.

## Chạy ứng dụng
```bash
dotnet run --project Project_CG_Paint.csproj
```

Lưu ý: WinForms thường yêu cầu môi trường có UI. Nếu đang chạy trong headless container, có thể chỉ build/test được chứ không mở form được.

## Sau khi sửa code
1. Build solution.
2. Nếu có lỗi biên dịch, sửa theo từng file.
3. Nếu lỗi runtime liên quan WinForms/UI, kiểm tra:
   - Event handler có khớp tên trong `.Designer.cs`.
   - Control được khởi tạo qua `InitializeComponent()`.
   - Tọa độ/size không âm hoặc vượt bounds.
4. Nếu có thuật toán mới, kiểm tra output điểm/màu bằng một test nhỏ hoặc breakpoint.

## Tiêu chí hoàn thành
- Không còn lỗi biên dịch.
- `Program.cs` và form chính khởi tạo đúng.
- Nếu thay đổi UI, form mở được và không ném ngoại lệ ở `InitializeComponent`.
