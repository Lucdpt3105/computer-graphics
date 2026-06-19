# AI Handoff Readme - Project_CG_Paint

Ngay cap nhat: 2026-06-20

## 1. Muc tieu cuoi cua nhom

## TÔi có thể tự thao tác build bên Visual Studio, nhưng AI moi se tiep tuc phat trien va sua code logic.

Nhom dang xay dung do an **Computer Graphics Paint** bang **C# WinForms / .NET Framework 4.8**.

Muc tieu cuoi:

1. **PaintForm**
   - Ve duoc cac hinh 2D tren he truc `Oxy`.
   - Ve duoc cac vat the 3D dang wireframe tren he truc `Oxyz`.
   - Co mapping toa do giua canvas pixel va toa do toan hoc.
   - Co selection/hit-test, bounding box overlay, stroke style, fill rieng cho 2D.

2. **TransformForm**
   - Nhan object dang chon tu PaintForm.
   - Ap dung cac phep bien doi bang ma tran len object trong PaintForm.
   - Cac transform can co: translate, rotate, scale, reflect.
   - TransformForm chi la noi nhap tham so; PaintForm/render pipeline la noi cap nhat va ve lai object.

3. **AnimationForm**
   - Co cac animation scene mau duoc tao san.
   - Animation su dung ma tran bien doi theo thoi gian.
   - Muc tieu khong phai editor animation day du ngay tu dau, ma la co scene dong demo duoc.

## 2. Pipeline render mong muon

Pipeline render tong quat:

```text
GraphicObject
-> Transform neu co input tu TransformForm
-> Projection neu la object 3D
-> Fill neu la shape 2D va dang bat fill
-> Stroke bang cac diem sinh tu thuat toan rasterization
-> Selection overlay neu object dang duoc chon
-> Render ra canvas PictureBox
```

Quy uoc:

- PaintForm khoi dong mac dinh o che do `Oxy`.
- Nhan `btnDraw2D` thi render he truc `Oxy`, chi ve shape 2D.
- Nhan `btnDraw3D` thi render he truc `Oxyz`, chi ve shape 3D wireframe.
- He `Oxy`: truc X huong sang phai, truc Y huong len tren.
- He `Oxyz`: X sang phai, Y len tren, Z cheo xuong duoi ben trai.
- Mapping: **1 don vi toa do = 5 pixel**.
- Thuat toan lam viec tren toa do toan hoc; RenderService chuyen qua pixel man hinh.

## 3. Cac form chinh

### PaintForm

Vai tro:

- Quan ly danh sach object dang ve.
- Quan ly tool hien tai: draw, selection, fill, reflect.
- Nhan input mouse de tao hinh.
- Goi controller/service de tao object, hit-test, fill, transform nhanh va render.
- Render canvas moi khi object thay doi.

Dang huong toi:

- Shape 2D mac dinh chi co stroke, khong fill.
- Fill la che do rieng: chon Fill roi chon object/mau de cap nhat fill.
- Shape 3D chi ve wireframe.
- Visible edge va hidden edge cua 3D can co style khac nhau: visible solid, hidden dash/nhat mau hon.

### TransformForm

Vai tro:

- Hien danh sach object tu PaintForm.
- Nhan input translate/rotate/scale/reflect.
- Build ma tran bang `TransformComposer2D`.
- Gui matrix ve PaintForm bang callback de ap dung va render lai.

Chu y:

- Hien tai TransformForm uu tien shape 2D vi UI moi co input X/Y.
- ComboBox phu chi nen hien placeholder, khong tu chon item dau tien.
- RadioButton khong nen tu bat khi mo form.

### AnimationForm

Vai tro mong muon:

- Chua cac scene mau co animation.
- Animation duoc tao bang transform matrix theo thoi gian.
- Co the bat dau bang hard-code scene mau truoc khi lam UI editor.

Scene mau de demo sau nay:

- 2D: xe chay, banh xe quay, may troi, mat troi tinh.
- 3D: cube xoay, sphere/cylinder/prism di chuyen hoac quay quanh truc.

## 4. Cac module quan trong

### Algorithms/Rasterization/Shape2D

Chua cac thuat toan sinh diem:

- `BresenhamLine`
- `MidpointCircle`
- `MidpointEllipse`
- `RectangleRasterizer`
- `TriangleRasterizer`
- `PolygonRasterizer`
- `DiamondRasterizer`
- `ParallelogramRasterizer`
- `Shape2DFill`

Ghi chu:

- Stroke va fill dang duoc tach rieng.
- Fill color chi ap dung khi shape duoc bat `Style.IsFilled`.

### Algorithms/Rasterization/Shape3D

Chua wireframe generator:

- `CubeWireFrame`
- `SphereWireFrame`
- `CylinderWireFrame`
- `PrismWireFrame`
- `PyramidWireFrame`

3D hien ve wireframe, khong fill mat.

### Algorithms/Transform

Chua composer ma tran:

- `TransformComposer2D`
- `TransformComposer3D`

Quy uoc hien tai:

- `Matrix3x3.Transform()` va `Matrix4x4.Transform()` dang theo row-vector: `point * matrix`.
- Compose quanh pivot can theo thu tu:

```text
T(-pivot) * Transform * T(pivot)
```

### Services

Cac service dang duoc dung/huong toi:

- `RenderService`: mapping toa do, ve he truc, fill, stroke, 3D wireframe, selection overlay.
- `SelectionService`: hit-test object va set selected object.
- `TransformService`: ap matrix vao object dang chon.
- `MethodApplyPattern`: ap line pattern vao list diem sinh tu rasterizer.

## 5. Tien do hien tai

Da lam:

- PaintForm da co logic noi tool button voi controller/service/render.
- PaintForm co mode 2D/3D.
- RenderService co mapping 1 don vi = 5 pixel.
- Canvas render he truc Oxy/Oxyz.
- Ve duoc shape 2D tu mouse drag.
- Ve duoc shape 3D wireframe tu mouse drag.
- Selection co hit-test va bounding box overlay.
- Fill tach rieng cho shape 2D.
- TransformForm nhan object va build matrix cho transform 2D.
- Sua thu tu matrix transform theo row-vector.
- Sua logic parallelogram: D = B + C - A va ve canh A-B, B-D, D-C, C-A.
- Them `MethodApplyPattern` vao namespace project va include trong csproj.

Can tiep tuc:

- Test lai toan bo tren Visual Studio.
- Kiem tra UX fill co dung nhu mong muon chua.
- Hoan thien line style button de doi pattern solid/dash/dot.
- Toi uu render neu canvas lon vi hien co dung `SetPixel`.
- Hoan thien transform cho 3D neu can.
- Hoan thien AnimationForm va scene mau.

## 6. Cac loi/van de can AI moi luu y

1. **Khong duoc doi mapping tuy tien**
   - Yeu cau cua nhom: 1 don vi toa do = 5 pixel.
   - Neu hinh bi roi rac, sua cach render diem thanh block/brush, khong doi mapping.

2. **PaintForm phai render lai khi resize**
   - PictureBox da Dock Fill nhung layout table co the lam canvas khong mo rong dung.
   - Can kiem tra `tableLayoutPanel3` va column span cua canvas.

3. **Fill la mode rieng**
   - UX mong muon: nhan Fill de vao fill mode.
   - Sau do chon mau thi object dang chon/render fill cap nhat theo mau moi.
   - Khong bat nguoi dung chon mau truoc roi moi nhan Fill moi co tac dung.

4. **Shape 2D mac dinh stroke-only**
   - Khi tao shape moi: `IsFilled = false`, `FillColor = Transparent`.

5. **3D chi wireframe**
   - Khong can fill mat 3D.
   - Visible edge nen solid; hidden edge nen dashed/nhat mau de phan biet.

6. **TransformForm khong nen auto-check radio**
   - Khi mo form, radio pivot/line khong tu chon.
   - ComboBox phu nen co placeholder, khong auto-select item dau.

7. **Build bang CLI co the loi resource WinForms**
   - Project chay duoc bang Visual Studio.
   - Neu `dotnet build` loi `MSB3823/MSB3822`, day co the la loi resource `.resx` voi .NET SDK moi, khong nhat thiet la loi code logic.

## 7. Huong uu tien tiep theo

Uu tien gan:

1. Chay Visual Studio va test PaintForm:
   - Resize form/canvas.
   - Ve line/rectangle/circle/ellipse/triangle/diamond/parallelogram/polygon.
   - Chon object va xem bounding box.
   - Fill object, doi mau khi dang Fill mode.
   - Chuyen sang 3D va ve cube/sphere/cylinder/prism/pyramid.

2. Sua cac loi render/UX neu test phat hien.

3. Hoan thien `btnLineStyle`:
   - Solid: `{1}`
   - Dashed: `{5, 5}`
   - Dotted: `{1, 3}`
   - DashDot: `{5, 3, 1, 3}`

4. Lam TransformForm tot hon:
   - Validate input.
   - Cap nhat object combo khi PaintForm co object moi.
   - Them transform 3D neu nhom can.

5. Lam AnimationForm scene mau:
   - Tao scene hard-code.
   - Dung timer theo FPS.
   - Moi frame build matrix moi va render lai.

## 8. Cach giao tiep voi AI moi

Neu mo chat moi, co the dua file nay va noi:

```text
Hay doc Documentation/AI_Handoff_Readme.md truoc. Day la muc tieu va tien do cua project. Sau do tiep tuc tu phan can tiep tuc, khong refactor lon neu khong can.
```

AI moi nen:

- Doc code hien tai truoc khi sua.
- Khong revert thay doi trong Designer/resx neu khong duoc yeu cau.
- Uu tien sua dung bug dang bao cao.
- Chay build/test neu moi truong cho phep, nhung neu CLI loi resource WinForms thi can phan biet voi loi code.
