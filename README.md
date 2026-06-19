# Project CG Paint (Computer Graphics)

Đồ án ứng dụng vẽ đồ họa máy tính (Computer Graphics) được xây dựng bằng C# và Windows Forms (.NET Framework 4.8). Ứng dụng cung cấp các công cụ vẽ đa dạng, hỗ trợ cả đối tượng 2D, 3D, các phép biến đổi hình học (Transformations) và diễn hoạt (Animation).

## 🌟 Tính năng chính

### 1. Vẽ hình 2D (Shape 2D Rasterization)
Ứng dụng cài đặt các thuật toán vẽ đường cơ sở (Bresenham, Midpoint) để vẽ các hình cơ bản:
- Đường thẳng (Line)
- Hình tròn (Circle)
- Hình elip (Ellipse)
- Hình chữ nhật (Rectangle)
- Hình bình hành (Parallelogram)
- Hình thoi (Diamond)
- Hình tam giác (Triangle)
- Đa giác (Polygon)

### 2. Vẽ hình 3D (Shape 3D Wireframe)
Hỗ trợ hiển thị dạng khung lưới (Wireframe) của các hình khối 3D cùng với các phép chiếu (Cabinet, Cavalier):
- Hình hộp chữ nhật / Hình lập phương (Cube)
- Hình trụ (Cylinder)
- Hình lăng trụ (Prism)
- Hình chóp (Pyramid)
- Hình cầu (Sphere)

### 3. Phép biến đổi hình học (Transformations)
Áp dụng ma trận biến đổi (Matrix 3x3 cho 2D, Matrix 4x4 cho 3D) để thực hiện:
- Tịnh tiến (Translation)
- Quay (Rotation)
- Thu phóng (Scaling)
- Đối xứng (Reflection)

### 4. Diễn hoạt và Animation
- Hệ thống Timeline & Keyframe cho phép người dùng tạo hiệu ứng chuyển động cho các đối tượng trên Scene.
- Animation Form giúp quản lý và chỉnh sửa luồng diễn hoạt.

### 5. Quản lý đối tượng
- Hỗ trợ chọn (Selection) đối tượng bằng Bounding Box.
- Object Inspector: Xem và thay đổi thuộc tính, kích thước, vị trí của đối tượng.
- Gộp nhóm đối tượng (Composite Entity).

## 🛠️ Công nghệ sử dụng
- **Ngôn ngữ:** C#
- **Framework:** .NET Framework 4.8
- **UI:** Windows Forms
- **Kiến trúc:** Phân chia thư mục rõ ràng theo các module: Algorithms, Controllers, CoreModel, Data, Rendering, Services, ViewModel.

## 📂 Cấu trúc dự án
- `Algorithms`: Chứa các thuật toán xử lý đồ họa cốt lõi như Rasterization (Bresenham, Midpoint...), Projection, Transform.
- `Controllers`: Quản lý các logic xử lý sự kiện giao diện (Paint, Animation, Transform...).
- `CoreModel`: Các lớp cấu trúc dữ liệu nền tảng như ma trận (Matrix3x3, Matrix4x4), điểm (Point2D, Point3D), Face, Edge...
- `Data`: Chứa các Model đại diện cho các hình học (Shape2D, Shape3D), Style, Scene.
- `Rendering`: Quản lý Canvas, Pipeline Render, chịu trách nhiệm vẽ các đối tượng từ Data Model ra giao diện.
- `Forms`: Các cửa sổ giao diện chính của ứng dụng.

## 🚀 Hướng dẫn chạy chương trình
1. Mở file `Project_CG_Paint.csproj` hoặc `Project_CG_Paint.slnx` bằng **Visual Studio** (khuyến nghị phiên bản 2019 hoặc mới hơn).
2. Chờ Visual Studio tải các file và references.
3. Nhấn **F5** hoặc nút **Start** để build và chạy chương trình.

---
*Đồ án Môn học Đồ họa Máy tính.*
