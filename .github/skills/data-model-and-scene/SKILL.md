---
name: data-model-and-scene
description: "Use when: modifying GraphicObject, Scene, Point2D, Point3D, Edge, BoundingBox, or object/scene data structures."
---

# Data Model and Scene Workflow

## Mục tiêu
Hỗ trợ làm việc với các lớp dữ liệu nền của đồ án: điểm, cạnh, bounding box, đối tượng đồ họa và scene.

## Các lớp quan trọng
- `Point2D`, `Point3D`: biểu diễn tọa độ.
- `Edge`: biểu diễn cạnh nối hai điểm.
- `BoundingBox2D`, `BoundingBox3D`: bao hình học, dùng để kiểm tra bounds hoặc culling.
- `GraphicObject`: đại diện một đối tượng vẽ.
- `Scene`: tập hợp các đối tượng trong bản vẽ.

## Nguyên tắc thiết kế
1. Không nhúng logic vẽ phức tạp vào model. Model nên giữ dữ liệu và thao tác hình học cơ bản.
2. Thuật toán raster/projection nên nhận đầu vào từ model và trả về điểm/màu.
3. `Scene` nên là nơi quản lý collection đối tượng, thêm/xóa/lấy danh sách.
4. Nếu cần clone/sao chép đối tượng, thêm method rõ ràng thay vì truyền reference mơ hồ.
5. Nếu thêm property mới vào `GraphicObject`, cân nhắc ảnh hưởng đến serialization, UI, transform, và render.

## Khi debug
- Kiểm tra object có được thêm vào `Scene` trước khi render không.
- Kiểm tra bounding box có cập nhật sau transform không.
- Kiểm tra reference/clone khi người dùng undo/redo hoặc tạo nhiều bản sao.
- Kiểm tra null trước khi render scene/object.

## Tiêu chí hoàn thành
- Model đơn giản, dễ test.
- Scene quản lý object rõ ràng.
- RenderService không phải đoán cấu trúc object.
