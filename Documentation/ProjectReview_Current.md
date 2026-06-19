# Review do an hien tai - Project_CG_Paint

Ngay review: 2026-06-19

## 1. Tom tat nhanh

Project hien tai la ung dung WinForms C# target .NET Framework 4.8 cho do hoa may tinh 2D/3D. Codebase da co nen tang kha rong: thuat toan rasterization 2D, wireframe 3D, matrix transform 2D/3D, projection Cabinet/Cavalier, data model cho scene/object/style/animation, va UI gom PaintForm, TransformForm, AnimationForm.

Trang thai thuc te: do an dang o giai doan co khung kien truc va nhieu module mien du lieu/thuat toan, nhung tang dieu phoi ung dung chua duoc ket noi day du. Nhieu controller/service/render pipeline hien con trong, nen cac nut UI da co tren form nhung phan lon chua tao thanh workflow ve/chon/bien doi/render hoan chinh.

Danh gia tong quan:

- Diem manh: cau truc thu muc ro, tach domain/thuat toan/UI tuong doi tot, co nhieu thuat toan dung de trinh bay mon Do hoa may tinh.
- Diem yeu lon nhat: chua co luong chay end-to-end tu UI -> controller -> scene -> rasterizer/projection -> render.
- Rui ro ky thuat can sua som: thu tu compose matrix transform dang co kha nang sai voi quy uoc nhan ma tran hien tai; build bang .NET SDK moi dang fail do resource WinForms.

## 2. Pham vi da doc

Cac nhom file chinh da duoc kiem tra:

- Cau hinh/entry point: `Project_CG_Paint.csproj`, `Program.cs`, `README.md`, `Documentation/ProjectFlow.md`.
- UI: `Forms/Paint/PaintForm.cs`, `Forms/Paint/PaintForm.Designer.cs`, `Forms/Transform/TransformForm.cs`, `Forms/Transform/TransformForm.Designer.cs`, `Forms/Animation/AnimationForm.cs`, `Forms/Animation/AnimationForm.Designer.cs`.
- Thuat toan: `Algorithms/Rasterization/Shape2D/*`, `Algorithms/Rasterization/Shape3D/*`, `Algorithms/Transform/*`, `Algorithms/Projection/*`.
- Core model: `CoreModel/Model/*`, `CoreModel/Geometry/*`.
- Data model: `Data/Objects/*`, `Data/Scene/*`, `Data/Shapes2D/*`, `Data/Shapes3D/*`, `Data/Animation/*`.
- Controllers/services/rendering: `Controllers/*`, `Services/*`, `Rendering/*`.

## 3. Kien truc hien tai

### 3.1. Tang UI

`Program.cs` khoi dong truc tiep `Forms.Paint.PaintForm`.

`PaintForm` hien co cac chuc nang code-behind dang hoat dong:

- Cap nhat toa do chuot tren status bar.
- Mo `AnimationForm`.
- Mo `TransformForm`.
- Thoat ung dung.

Designer cua `PaintForm` da co toolbar/giao dien kha day du: Selection, Shape2D, Shape3D, Tool, Color, canvas, tree view. Tuy nhien code-behind chua gan event cho phan lon nut ve hinh, chon doi tuong, to mau, doi style, clear, reflect.

`TransformForm` co UI nhap translate/rotate/scale/reflect kha chi tiet, nhung code-behind chi goi `InitializeComponent()`. Cac nut Apply/Clear/Translate/Rotate/Scale/Reflect chua thay duoc ket noi logic.

`AnimationForm` co menu scene, timeline, canvas va tree view. Code-behind chi co su kien close goi `Application.Exit()`, chua ket noi timeline/scene/animation controller.

### 3.2. Tang controller/service

Nhieu lop da duoc tao theo dung y tuong MVC/MVVM nhe, nhung hien con trong:

- `Controllers/Paint/DrawingController.cs`
- `Controllers/Paint/SelectionController.cs`
- `Controllers/Paint/SceneController.cs`
- `Controllers/Paint/RenderController.cs`
- `Controllers/Paint/ObjectInspectorController.cs`
- `Controllers/Transform/TransformController.cs`
- `Controllers/Animation/AnimationController.cs`
- `Controllers/Animation/TimelineController.cs`
- `Services/RenderService.cs`
- `Services/TransformService.cs`
- `Services/SelectionService.cs`
- `Services/AnimationService.cs`
- `Services/InspectorService.cs`
- `Rendering/Pipeline/RenderPipeline2D.cs`
- `Rendering/Renderers/Shape2DRenderer.cs`
- `Rendering/Renderers/Shape3DRenderer.cs`

`SceneManager` co `Scenes`, `CurrentScene`, `AddScene()`, nhung logic set `CurrentScene` hien khong thuc su can thiet vi `CurrentScene` da khoi tao bang `new Scene()`, nen dieu kien `CurrentScene == null` gan nhu khong xay ra.

### 3.3. Tang data/domain

Nhom 2D kha tot:

- `Shape2D` ke thua `SceneNode2D`, co `Pivot`, `InspectionGeometry`, `CurrentMatrix`, bounds va reset geometry.
- Cac shape 2D nhu line/rectangle/circle/ellipse/triangle/polygon/parallelogram/diamond co validation va geometry inspection.
- `CompositeEntity` co children va chan add self/child da co parent.

Nhom scene/animation da co thiet ke:

- `Scene` co `Root`, duration, FPS, loop mode.
- `SceneNode2D` co `BaseLocalTransform` va `AnimationData`.
- Data animation co keyframe/channel/local transform/evaluated state.

Nhom 3D chua dong bo:

- Cac wireframe generator trong `Algorithms/Rasterization/Shape3D` da sinh vertices/edges/faces.
- Nhung cac domain object 3D trong `Data/Shapes3D` nhieu lop van `throw new NotImplementedException()` o `RebuildInspectionGeometry()` va/hoac `CalculateDefaultPivot()`.

## 4. Diem manh

1. Cau truc module ro rang

Project tach thanh `Algorithms`, `CoreModel`, `Data`, `Controllers`, `Services`, `Forms`, `Rendering`, `ViewModel`. Day la huong tot cho do an lon hon mot bai demo don file.

2. Thuat toan do hoa co do phu kha rong

Da co Bresenham line, midpoint circle, midpoint ellipse, rasterize polygon/rectangle/triangle/diamond/parallelogram, fill helper, wireframe cube/cylinder/prism/pyramid/sphere, projection Cabinet/Cavalier.

3. Core matrix rieng, khong phu thuoc GDI+

`Matrix3x3`, `Matrix4x4`, `MatrixFactory`, `Point2D`, `Point3D` la nen tot de giai thich bien doi hinh hoc trong bao cao.

4. Co huong toi scene graph va animation

`CompositeEntity`, `SceneNode2D`, `LocalTransform2D`, `AnimationData2D`, `AnimationChannel<T>` cho thay do an khong chi ve diem pixel ma dang huong toi quan ly canh va chuyen dong.

5. UI da phac thao day du tinh nang mong muon

Toolbar PaintForm co cac nhom shape/tool/color; TransformForm co input cho translate/rotate/scale/reflect; AnimationForm co timeline/canvas/tree. Ve mat demo giao dien, nen tang da co.

## 5. Van de uu tien cao

### 5.1. Build bang .NET SDK hien tai dang fail

Lenh da chay:

```powershell
dotnet build Project_CG_Paint.csproj -c Debug
```

Ket qua:

- `MSB3823: Non-string resources require the property GenerateResourceUsePreserializedResources to be set to true.`
- `MSB3822: Non-string resources require the System.Resources.Extensions assembly at runtime, but it was not found in this project's references.`

Nguyen nhan kha nang cao: project WinForms .NET Framework kieu cu co `.resx` chua non-string resources, nhung dang build bang .NET SDK/MSBuild moi. `msbuild` Visual Studio khong co trong PATH, `where.exe msbuild` khong tim thay.

Khuyen nghi:

- Neu build bang Visual Studio/MSBuild full: cai Visual Studio Build Tools va dung `MSBuild.exe` cua VS.
- Neu muon build bang `dotnet build`: can cap nhat csproj cho resource preserialized va reference `System.Resources.Extensions`, hoac chuyen project sang SDK-style neu phu hop.

### 5.2. UI chua ket noi workflow chinh

`PaintForm.Designer.cs` co rat nhieu nut, nhung `PaintForm.cs` chi xu ly mouse position, open AnimationForm, open TransformForm, exit. Cac nut ve line/rectangle/circle/3D/color/fill/clear/selection chua co event handler logic.

Tac dong: nguoi dung mo app co the thay UI day du, nhung tinh nang chinh cua paint app chua hoat dong end-to-end.

Khuyen nghi:

- Hoan thien `ToolAndDrawContext` de luu tool hien tai, draw type, style, fill color.
- Gan event cho toolbar trong `PaintForm`.
- Them mouse down/move/up tren canvas de tao shape.
- Sau khi tao shape, dua vao `SceneManager.CurrentScene.Root`.
- Goi render pipeline de ve lai canvas.

### 5.3. Controller/service/render pipeline con trong

Nhieu lop quan trong chi la shell rong. Day la khoang trong lon nhat giua kien truc va tinh nang:

- `DrawingController`
- `TransformController`
- `AnimationController`
- `RenderService`
- `TransformService`
- `SelectionService`
- `RenderPipeline2D/3D`
- `Shape2DRenderer/Shape3DRenderer`

Tac dong: thuat toan va data model co nhung khong duoc dieu phoi boi ung dung.

Khuyen nghi uu tien:

1. Lam `DrawingController` tao object 2D tu mouse input.
2. Lam `RenderService` ve scene len `Graphics` hoac bitmap canvas.
3. Lam `TransformService` ap dung matrix len `InspectionGeometry.CurrentVertices`.
4. Lam `SelectionService` chon object theo bounding box/hit test.

### 5.4. Thu tu compose transform co kha nang sai

`Matrix3x3.Transform()` va `Matrix4x4.Transform()` dang tinh theo quy uoc row-vector: diem nhan ma tran o ben trai, dang `point * matrix`.

Trong quy uoc nay, neu scale quanh pivot thi thu tu dung thuong la:

```text
T(-pivot) * Scale * T(pivot)
```

Nhung `TransformComposer2D.BuildScaleByPoint()` dang return:

```csharp
return fromOrigin * scale * toOrigin;
```

Tuong tu voi rotation 2D va cac ham scale/rotation quanh pivot trong `TransformComposer3D`.

Vi du de thay loi:

- Diem P = (2, 0), pivot = (1, 0), scaleX = 2.
- Ket qua dung: x = 3.
- Neu dung thu tu hien tai `T(pivot) * Scale * T(-pivot)`, ket qua co the thanh x = 5.

Khuyen nghi:

- Viet unit test nho cho translate/scale/rotate quanh pivot.
- Chon ro convention row-vector hoac column-vector.
- Neu giu `Matrix3x3.Transform()` hien tai, sua compose thanh `toOrigin * scale * fromOrigin`, `toOrigin * rotation * fromOrigin`, va tuong tu 3D.

### 5.5. Reflection by line 2D tinh goc chua dung khi line khong di qua goc

`TransformComposer2D.BuildReflectionByLine(start, end)` tao:

```csharp
Point2D origin = new Point2D(0,0);
Point2D pointEnd = new Point2D(end.X, end.Y);
Edge<Point2D> edge = new Edge<Point2D>(origin, pointEnd);
```

Ham nay bo qua vector `end - start`. Neu line start khac `(0,0)`, goc cua duong thang co the bi tinh sai. Nen tao edge theo `start -> end` hoac vector `(0,0) -> (end.X - start.X, end.Y - start.Y)`.

Ngoai ra thu tu compose reflection cung dang theo pattern co kha nang bi dao nguoc nhu muc 5.4.

### 5.6. Rasterizer va fill chua dong bo voi tai lieu

`Documentation/ProjectFlow.md` mo ta triangle/rectangle/polygon rasterizer tra ve canh va mien trong. Nhung code hien tai:

- `TriangleRasterizer.RasterizePoints()` chi ve 3 canh.
- `RectangleRasterizer.RasterizePoints()` chi ve 4 canh.
- `PolygonRasterizer.RasterizePoints()` chi ve cac canh.
- Fill nam rieng trong `Shape2DFill` (`FillTriangle`, `FillRectangle`, `FillPolygon`, `FillCircle`, `FillEllipse`) nhung chua thay controller/render goi.

Khuyen nghi:

- Cap nhat tai lieu de dung voi code, hoac
- Doi rasterizer thanh co tham so `includeFill`, hoac
- Render layer ket hop outline rasterizer + `Shape2DFill` dua theo `ShapeStyles.FillColor`.

### 5.7. Shape3D domain object chua hoan thien

`CubeShape`, `SphereShape`, `CylinderShape`, `PrismShape`, `PyramidShape` trong `Data/Shapes3D` con `NotImplementedException`. Trong khi do wireframe generator o `Algorithms/Rasterization/Shape3D` da co logic sinh hinh.

Khuyen nghi:

- Trong moi `Shape3D.RebuildInspectionGeometry()`, goi wireframe generator tuong ung.
- Fill `InspectionGeometry.OriginalVertices`, `Edges`, va neu can thi them faces.
- Tinh pivot mac dinh theo center/position.

## 6. Van de trung binh va nho

1. Co hai `RenderService`

Project co `Services/RenderService.cs` va `Rendering/RenderService.cs`, deu con trong. Nen thong nhat mot noi chiu trach nhiem render de tranh nham namespace.

2. `Matrix3x3` va `Matrix4x4` constructor bat exception roi `Console.WriteLine`

Neu input sai kich thuoc, constructor bat exception nhung object co the o trang thai khong hop le. Nen de exception throw ra ngoai hoac validate truoc va throw `ArgumentException` truc tiep.

3. `SceneManager.CurrentScene`

Dang khoi tao san `new Scene()`, nen `AddScene()` khong bao gio gan scene dau tien vao current theo logic `if (CurrentScene == null)`. Can quyet dinh: current ban dau null, hoac add scene dau tien thay current mac dinh.

4. `Point2D`/`Point3D` dung double va HashSet

Readonly struct mac dinh co value equality, dung duoc voi `HashSet`. Tuy nhien double equality la exact equality. Vi rasterizer da round ve int truoc khi tao point, phan nay on. Voi geometry transform dung double, can can than neu dung HashSet/so sanh exact.

5. README qua ngan

`README.md` chi co ma so/tac gia. Nen bo sung muc tieu, cach build, cach chay, tinh nang, cau truc project, va trang thai hoan thanh.

6. Chua co test project

Nhung ham algorithm/matrix rat phu hop unit test. Nen them test cho Bresenham, midpoint circle/ellipse, fill polygon, transform compose, projection.

## 7. De xuat roadmap hoan thien

### Giai doan 1 - Lam project build/chay on dinh

- Sua cau hinh build WinForms resource.
- Xac dinh cach build chuan: Visual Studio Build Tools hay `dotnet build`.
- Them README cach build/chay.

### Giai doan 2 - Hoan thien luong ve 2D

- Implement `ToolAndDrawContext`.
- Gan event cho cac nut 2D trong `PaintForm`.
- Implement `DrawingController`.
- Tao shape 2D tu canvas mouse input.
- Dua object vao scene.
- Implement `Shape2DRenderer` va ve len canvas.

### Giai doan 3 - Hoan thien transform

- Sua/kiem thu thu tu compose matrix.
- Implement `TransformService`.
- Ket noi `TransformForm` voi object dang chon.
- Luu `TransformHistory`.
- Cap nhat `InspectionGeometry.CurrentVertices`.

### Giai doan 4 - Selection/Inspector

- Implement hit test theo bounding box/tren canh.
- Hien object tree va thong tin geometry/style/transform.
- Cho phep clear/delete/select object.

### Giai doan 5 - 3D/projection/render hidden line

- Hoan thien `Shape3D` domain object bang cach goi wireframe generator.
- Implement `Shape3DRenderer`.
- Ket noi Cabinet/Cavalier projection.
- Render visible/hidden edges theo line style khac nhau.

### Giai doan 6 - Animation

- Implement timeline, keyframe, interpolation.
- Evaluate scene theo time.
- Render scene theo frame.
- Ho tro loop/ping-pong theo `Scene.LoopMode`.

## 8. Ket luan

Do an hien tai co nen mong tot ve mat cau truc va thuat toan, dac biet la phan core geometry, matrix, rasterization 2D va wireframe/projection 3D. Tuy nhien, neu danh gia nhu mot ung dung Paint/Graphics hoan chinh, phan tich hop tu UI den controller/service/render van con thieu nhieu. Uu tien nen la: sua build, ket noi luong ve 2D end-to-end, sua transform matrix, sau do moi mo rong 3D va animation.

Neu can chon diem de demo som, nen tap trung vao 2D:

1. Chon tool Line/Rectangle/Circle.
2. Ve bang mouse tren canvas.
3. Luu vao scene.
4. Render lai.
5. Chon object va translate/rotate/scale.

Day se tao duoc mot duong demo ro rang, thuyet phuc, va tan dung duoc nhieu code hien da co.
