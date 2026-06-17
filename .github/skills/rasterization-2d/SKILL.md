---
name: rasterization-2d
description: "Use when: implementing, debugging, or explaining 2D rasterization algorithms such as Bresenham, midpoint circle/ellipse, polygon fill, triangle, rectangle, diamond, or parallelogram."
---

# 2D Rasterization Workflow

## Mục tiêu
Hỗ trợ viết/sửa thuật toán raster hóa hình 2D trong `Algorithms/Rasterization/Shape2D`.

## Quy ước API hiện tại
- Trả về `List<Point2D>` khi chỉ cần tọa độ.
- Trả về `List<ColoredPoint>` khi cần áp màu.
- Màu thường dùng `Shape2DFill.SolidFill` hoặc `FillColorFunction`.
- Các thuật toán nên dùng `HashSet<Point2D>` để tránh trùng điểm.

## Mẫu thuật toán
```csharp
public static List<Point2D> RasterizePoints(...)
{
    HashSet<Point2D> points = new HashSet<Point2D>();

    // 1. Sinh điểm biên.
    // 2. Sinh điểm miền trong nếu cần.
    // 3. Trả về Shape2DFill.ApplyColorFunction(points, fillColorFunction).
}
```

## Các thuật toán nên tái sử dụng
- `BresenhamLine.RasterizePoints`: vẽ cạnh line.
- `Shape2DFill.FillPolygon`: fill đa giác.
- `TriangleRasterizer`: fill tam giác.
- `RectangleRasterizer`: fill rectangle.
- `MidpointCircle`, `MidpointEllipse`: sinh biên tròn/elip.

## Khi debug
1. Kiểm tra thứ tự đỉnh đầu vào. Một số thuật toán cần đỉnh theo chiều kim đồng hồ/ngược chiều kim đồng hồ.
2. Kiểm tra điểm biên có bị thiếu do làm tròn hay không.
3. Kiểm tra tọa độ âm/nằm ngoài bitmap trước khi vẽ vào `Graphics`.
4. Nếu hình bị rỗng, in/log số điểm sinh được ở vài trường hợp nhỏ.
5. Với hình bình hành, nếu chỉ vẽ 4 cạnh thì chưa phải fill; dùng `Shape2DFill.FillPolygon` hoặc phân rã thành tam giác.

## Tiêu chí hoàn thành
- Không trùng điểm quá mức cần thiết.
- Không vẽ ra ngoài bounds.
- Có overload màu và không màu.
- Comment tiếng Việt giải thích ý tưởng chính.
