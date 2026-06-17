---
name: winforms-ui
description: "Use when: modifying PaintForm, TransformForm, AnimationForm, Designer files, controls, events, or WinForms rendering behavior."
---

# WinForms UI Workflow

## Mục tiêu
Hỗ trợ sửa giao diện WinForms, event handler, canvas vẽ, input tọa độ và tương tác người dùng.

## Các file thường sửa
- `Forms/Paint/PaintForm.cs`
- `Forms/Paint/PaintForm.Designer.cs`
- `Forms/Transform/TransformForm.cs`
- `Forms/Transform/TransformForm.Designer.cs`
- `Forms/Animation/AnimationForm.cs`
- `Forms/Animation/AnimationForm.Designer.cs`

## Quy tắc quan trọng
1. Luôn kiểm tra `InitializeComponent()` nếu UI không hiển thị control hoặc event không chạy.
2. Không sửa layout thủ công trong `.cs` nếu control được tạo trong `.Designer.cs`, trừ khi có lý do rõ.
3. Khi thêm control:
   - Thêm khai báo field.
   - Khởi tạo trong `InitializeComponent()`.
   - Gán event handler nếu cần.
4. Khi thêm event handler:
   - Tên method thống nhất: `buttonName_Click`, `comboBoxName_SelectedIndexChanged`, `pictureBoxName_MouseDown`, v.v.
5. Khi vẽ lên control, kiểm tra kích thước control trước khi vẽ.
6. Nếu dùng `Graphics`, luôn `using` hoặc dispose đúng cách khi tự tạo bitmap.

## Debug UI
- Nếu click không chạy: kiểm tra event handler trong Designer.
- Nếu control không hiện: kiểm tra `Controls.Add`, `Visible`, `Dock/Anchor`, `Location`, `Size`.
- Nếu vẽ bị mất: kiểm tra paint event, double buffer, invalidate/refresh.
- Nếu nhập sai gây crash: validate input trước khi gọi thuật toán.

## Tiêu chí hoàn thành
- Form mở được.
- Event chạy đúng.
- Input hợp lệ được xử lý, input sai có thông báo rõ.
- Không có exception từ Designer hoặc Paint handler.
