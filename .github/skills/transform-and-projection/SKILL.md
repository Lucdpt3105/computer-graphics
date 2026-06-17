---
name: transform-and-projection
description: "Use when: implementing, debugging, or explaining 2D/3D transforms, matrices, Cabinet/Cavalier projection, or wireframe 3D drawing."
---

# Transform and Projection Workflow

## Mục tiêu
Hỗ trợ làm việc với biến đổi hình học 2D/3D, ma trận, phép chiếu và wireframe 3D.

## Các thành phần liên quan
- `CoreModel/Model/Matrix3x3.cs`: biến đổi 2D.
- `CoreModel/Model/Matrix4x4.cs`: biến đổi 3D.
- `CoreModel/Model/MatrixFactory.cs`: tạo ma trận translate/scale/rotate/shear nếu có.
- `Algorithms/Transform/TransformComposer2D.cs`
- `Algorithms/Transform/TransformComposer3D.cs`
- `Algorithms/Projection/CabinetProjection.cs`
- `Algorithms/Projection/CavalierProjection.cs`
- `Algorithms/Rasterization/Shape3D/`: các wireframe 3D.

## Quy tắc hình học
1. Phân biệt tọa độ thế giới, tọa độ màn hình và tọa độ sau biến đổi.
2. Khi vẽ lên WinForms, cần chuyển đổi hệ tọa độ nếu hệ tọa độ bài toán khác hệ tọa độ màn hình.
3. Với 3D, wireframe chỉ cần nối các cạnh bằng line rasterizer.
4. Với projection xiên, kiểm tra hệ số góc chiếu và hướng trục phụ.
5. Luôn kiểm tra overflow khi nhân ma trận hoặc tính điểm sau transform.

## Khi debug
- Kiểm tra ma trận đơn vị có giữ nguyên điểm không.
- Kiểm tra translate/scale/rotate riêng lẻ trước khi ghép nhiều phép biến đổi.
- Với wireframe, vẽ từng edge một và kiểm tra danh sách cạnh.
- Với projection, kiểm tra đầu ra 2D của một số điểm 3D đã biết.

## Tiêu chí hoàn thành
- Ma trận có API rõ ràng.
- Biến đổi không làm mất điểm.
- Projection tạo ra tọa độ 2D hợp lệ để vẽ.
- Wireframe 3D dùng đúng danh sách cạnh.
