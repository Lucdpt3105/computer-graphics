# Luồng Hoạt Động của Project

## 1. Giới Thiệu
Project này tập trung vào việc tạo ra các cảnh đồ họa 2D và 3D bằng cách sử dụng các thuật toán biến đổi và rasterization.

## 2. Luồng Hoạt Động
1. **Khởi Tạo Scene**: Scene được khởi tạo với các đối tượng đồ họa ban đầu.
2. **Áp Dụng Thuật Toán Biến Đổi**: Các thuật toán biến đổi 2D được áp dụng để thay đổi vị trí, kích thước và hình dạng của các đối tượng.
3. **Rasterization**: Các đối tượng sau khi biến đổi được rasterization để hiển thị trên màn hình.
4. **Render Scene**: Scene được render và hiển thị cho người dùng.

## 3. Các Thuật Toán Biến Đổi 2D
- **TransformComposer2D**: Kết hợp các phép biến đổi như dịch chuyển, xoay và tỷ lệ.
- **BresenhamLine**: Vẽ đường thẳng sử dụng thuật toán Bresenham.
- **MidpointCircle**: Vẽ đường tròn sử dụng thuật toán Midpoint.
- **MidpointEllipse**: Vẽ đường ellipse sử dụng thuật toán Midpoint.
- **TriangleRasterizer**: Vẽ tam giác và tô màu tam giác.

## 4. Hình Dung Bức Tranh Toàn Cảnh
Sau khi hoàn thành các thuật toán biến đổi 2D, bạn sẽ có thể hình dung được cách các đối tượng đồ họa được biến đổi và hiển thị trên màn hình. Điều này sẽ giúp bạn hiểu rõ hơn về cách các cảnh đồ họa được tạo ra và cách các thuật toán hoạt động trong project.