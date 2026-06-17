---
name: project-context
description: "Use when: working on the computer graphics project, understanding the codebase, or making project-wide changes for the WinForms C# computer graphics assignment."
---

# Computer Graphics Project Context

## Mục tiêu
Bạn đang hỗ trợ người dùng làm đồ án Computer Graphics bằng C# WinForms. Dự án là ứng dụng vẽ/raster hóa 2D và wireframe 3D trong môi trường học thuật.

## Công nghệ chính
- Ngôn ngữ: C#
- Framework: .NET/WinForms
- Namespace gốc: `Project_CG_Paint`
- Entry point: `Program.cs`
- Form chính: `PaintForm`
- Service vẽ: `RenderService`
- Controller vẽ: `DrawingController`

## Cấu trúc quan trọng
- `Algorithms/Rasterization/Shape2D/`: thuật toán raster hóa hình 2D như line, rectangle, circle, ellipse, polygon, triangle, parallelogram.
- `Algorithms/Rasterization/Shape3D/`: wireframe các khối 3D như cube, cylinder, prism, pyramid, sphere.
- `Algorithms/Projection/`: phép chiếu Cabinet/Cavalier.
- `Algorithms/Transform/`: composer biến đổi 2D/3D.
- `CoreModel/Model/`: các lớp `Point2D`, `Point3D`, `Matrix3x3`, `Matrix4x4`, `MatrixFactory`.
- `CoreModel/Geometry/`: `Edge`, `BoundingBox2D`, `BoundingBox3D`.
- `Data/Objects/`: `GraphicObject`.
- `Data/Scene/`: `Scene`.
- `Rendering/RenderService.cs`: nơi biến điểm/màu thành thao tác vẽ thực tế.
- `Controllers/Drawing/DrawingController.cs`: điều phối thao tác vẽ.
- `Forms/Paint/`: UI chính.
- `Forms/Transform/`, `Forms/Animation/`: UI phụ.

## Nguyên tắc khi sửa code
1. Ưu tiên giữ phong cách hiện tại: `static class`, `List<Point2D>`, `List<ColoredPoint>`, tiếng Việt trong comment.
2. Không thay đổi API công khai nếu không cần. Nếu cần thêm overload, giữ overload cũ.
3. Với thuật toán raster, ưu tiên tính đúng trước, tối ưu sau.
4. Khi thêm thuật toán mới, kiểm tra xem đã có `Shape2DFill`, `BresenhamLine`, `TriangleRasterizer` để tái sử dụng chưa.
5. Khi sửa UI WinForms, luôn kiểm tra cả file `.cs` và `.Designer.cs` nếu liên quan đến layout/control/event.
6. Sau khi sửa, build solution bằng `dotnet build` hoặc `msbuild` tùy môi trường.

## Tiêu chí hoàn thành
- Code biên dịch được.
- Hình vẽ đúng logic hình học.
- UI không crash khi người dùng nhập tọa độ/ràng buộc hợp lệ.
- Comment tiếng Việt rõ ràng, ngắn gọn.
