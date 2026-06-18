# Hướng dẫn Thiết kế Giao diện (UI Design) cho Đồ án Đồ họa Máy tính

Dựa trên yêu cầu của đồ án, bạn có 3 form chính (`PaintForm`, `TransformForm`, `AnimationForm`). Dưới đây là gợi ý thiết kế chi tiết và bố cục (layout) tối ưu cho từng form để đảm bảo tính mỹ thuật, dễ sử dụng, và đầy đủ chức năng cần thiết của một phần mềm đồ hoạ bằng C# WinForms.

## 1. `PaintForm` (Giao diện vẽ hình 2D cơ bản)
**Mục đích:** Cung cấp công cụ vẽ các hình khối cơ bản (Đường thẳng, tròn, elip, đa giác...) và tô màu. Không gian làm việc chính của người dùng.

**Bố cục đề xuất:**
- **Menu Bar (Trên cùng):** 
  - `File` (New, Open, Save, Exit).
  - `Mode` (Chuyển sang *Transform Mode* hoặc *Animation Mode*).
- **Toolbox (Bên trái hoặc phía trên - Panel/ToolStrip):**
  - Các nút (Button/RadioButton) chọn loại hình cần vẽ: `Line`, `Circle`, `Ellipse`, `Rectangle`, `Polygon`, v.v.
  - Nút công cụ nâng cao: `Fill` (Tô màu), `Clear` (Xóa toàn bộ).
  - Bảng chọn màu (Nút gọi ra `ColorDialog`) và một `NumericUpDown` để chỉnh kích thước nét vẽ (Pen Width).
- **Canvas (Chính giữa - chiếm tối đa không gian):**
  - Một `PictureBox` hoặc `Panel` lớn (thường set thuộc tính `Dock = Fill`).
  - Hỗ trợ bắt sự kiện chuột (`MouseDown`, `MouseMove`, `MouseUp`) để vẽ tương tác.
  - Cần vẽ sẵn lưới toạ độ (Grid) 2D mờ nếu cần dễ hình dung tọa độ.
- **Status Bar (Dưới cùng - StatusStrip):**
  - Hiển thị toạ độ con trỏ chuột hiện tại: `X: 100, Y: 200`.
  - Hiển thị công cụ / chế độ vẽ đang chọn.

---

## 2. `TransformForm` (Giao diện biến đổi 2D & 3D)
**Mục đích:** Thực hiện và quan sát trực quan các phép biến đổi Affine (Tịnh tiến, Xoay, Tỷ lệ, Đối xứng, Biến dạng) trên đồ vật 2D/3D.

**Bố cục đề xuất:**
- **Header/Menu (Trên cùng):** Nút quay lại hoặc chuyển form.
- **Canvas (Bên trái / Giữa - PictureBox):** 
  - Hiển thị trục tọa độ rõ ràng (Gốc tọa độ thường đặt ở giữa màn hình thay vì góc trái trên kiểu mặc định của máy tính). Có thể là trục 2D (Oxy) hoặc 3D (Oxyz).
  - Hiển thị vật thể trước (nét đứt) và sau (nét liền) khi biến đổi.
- **Bảng điều khiển Biến đổi (Bên phải - Panel / GroupBox):**
  - **Danh sách đối tượng:** `ComboBox` hoặc `ListBox` để chọn đối tượng cần điều khiển.
  - **TabControl** chia theo phép biến đổi:
    - *Translate (Tịnh tiến):* Label + `TextBox/NumericUpDown` cho `Tx`, `Ty`, `Tz`.
    - *Rotate (Quay):* Nhập góc `Angle`, chọn tâm xoay (Gốc tọa độ O hoặc 1 điểm nhập tay).
    - *Scale (Tỷ lệ):* Nhập `Sx`, `Sy`, `Sz`.
    - *Reflect (Đối xứng):* Các nút Radio chọn hệ quy chiếu (Qua Ox, Oy, Tâm O hoặc một đường thẳng).
  - **Nhóm nút thực thi:**
    - Button `Áp dụng` (Apply).
    - Button `Khôi phục` (Reset về vị trí ban đầu).

---

## 3. `AnimationForm` (Giao diện hoạt hình)
**Mục đích:** Trình diễn các hoạt cảnh hoặc chuyển động dựa vào bộ đếm thời gian (Timer). Ví dụ: Xe di chuyển, cối xay gió quay, hệ mặt trời, đồng hồ...

**Bố cục đề xuất:**
- **Khung trình diễn (Phần lớn diện tích - PictureBox):** 
  - Nơi đối tượng đồ hoạ được vẽ lại liên tục. 
  - Phải bật thuộc tính `DoubleBuffered` cho Form hoặc PictureBox (nếu xài panel tự vẽ) để tránh chớp giật (Flickering).
- **Bảng điều khiển Animation (Bên dưới hoặc bên phải - Panel):**
  - **Danh sách hoạt cảnh:** `ComboBox` chọn ví dụ (Đồng hồ, Quả lắc, Chiếc xe bus...).
  - **Nút điều khiển (Player Controls):**
    - `Play` (Bắt đầu Timer).
    - `Pause` (Tạm dừng Timer).
    - `Stop` (Reset vị trí đồ vật và dừng Timer).
  - **Cài đặt tốc độ (Speed):** Một `TrackBar` để chỉnh thuộc tính `Interval` của Timer (ví dụ: từ chậm đến nhanh).

---

## Mẹo thiết kế UI cho WinForms
1. **Sử dụng SplitContainer hoặc TableLayoutPanel:** Sẽ giúp UI của bạn "responsive" co giãn linh hoạt khi người dùng phóng to/thu nhỏ Form, thay vì set vị trí cứng (Location `x,y`).
2. **DoubleBuffering:** Hãy tận dụng đoạn code `DoubleBuffered = true;` đối với những class vẽ hình liên tục hoặc Animation, giúp triệt để hiện tượng giật (flicker).
3. **Thống nhất màu sắc:** Đặt màu nền vùng Canvas là Trắng, hoặc Đen (kèm grid kẻ sọc nhạt) để làm nổi bật nét vẽ.
4. **Anchor & Dock:** Học cách gán thuộc tính `Anchor` (cho các nút) và `Dock` (Fill cho PictureBox, Right/Left cho menu) để form đẹp mà không tốn sức viết code chia lại tọa độ.