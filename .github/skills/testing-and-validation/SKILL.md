---
name: testing-and-validation
description: "Use when: validating algorithm correctness, creating tests, checking edge cases, or reviewing changes before finishing."
---

# Testing and Validation Workflow

## Mục tiêu
Đảm bảo code đồ án đúng về hình học, biên dịch được và không crash UI.

## Quy trình kiểm tra tối thiểu
1. Build solution.
2. Kiểm tra file vừa sửa không có lỗi biên dịch.
3. Với thuật toán raster:
   - Test hình nhỏ, ví dụ line ngắn, rectangle 5x5, triangle đơn giản.
   - Test điểm trùng, đoạn thẳng nằm ngang/dọc/chéo.
   - Test tọa độ âm nếu thuật toán hỗ trợ.
4. Với UI:
   - Kiểm tra form mở được.
   - Nhập dữ liệu hợp lệ.
   - Nhập dữ liệu sai để xem thông báo/validate.
5. Với transform/projection:
   - Test điểm đã biết trước/sau biến đổi.
   - Test ma trận đơn vị.
   - Test projection của các điểm 3D đơn giản.

## Edge cases cần nhớ
- Điểm trùng nhau.
- Cạnh dài 0.
- Tọa độ âm.
- Kích thước 0.
- Đa giác không lồi.
- Input không phải số.
- Control chưa khởi tạo.
- Bitmap/control có kích thước 0.

## Tiêu chí hoàn thành
- Build thành công.
- Không có lỗi runtime rõ ràng.
- Thuật toán trả về số điểm hợp lý.
- UI xử lý input sai an toàn.
