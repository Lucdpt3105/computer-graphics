using Project_CG_Paint.Controllers.Paint;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Data.Shapes3D;
using Project_CG_Paint.Data.Transform;
using Project_CG_Paint.Forms.Animation;
using Project_CG_Paint.Forms.Transform;
using Project_CG_Paint.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Project_CG_Paint.Forms.Paint
{
    public partial class PaintForm : Form
    {
        private readonly List<GraphicObject> _objects = new List<GraphicObject>();
        private readonly ToolAndDrawContext _context = new ToolAndDrawContext();
        private readonly DrawingController _drawingController = new DrawingController();
        private readonly RenderService _renderService = new RenderService();
        private readonly SelectionService _selectionService = new SelectionService();
        private readonly TransformService _transformService = new TransformService();

        // Click-based drawing state for 2D
        private readonly List<Point2D> _clickedPoints = new List<Point2D>();

        public PaintForm()
        {
            InitializeComponent();
            ConfigureCanvasLayout();
            WireEvents();
            SetMode(is3DMode: false);
            SetSelectedColor(Color.Black);
            QueueRenderCanvas();
        }

        private GraphicObject SelectedObject => _objects.FirstOrDefault(o => o.Metadata.IsSelected);

        private void WireEvents()
        {
            EnsureRefreshMenuItem();

            btnDraw2D.Click += (sender, e) => SetMode(is3DMode: false);
            btnDraw3D.Click += (sender, e) => SetMode(is3DMode: true);
            btnRefresh.Click += (sender, e) => RefreshSceneView();

            btnSelection.Click += (sender, e) => { _context.ToolType = ToolType.Selection; ResetClickState(); };

            btnPoint.Click += (sender, e) => SelectDrawTool(DrawType.Point);
            btnLine.Click += (sender, e) => SelectDrawTool(DrawType.Line);
            btnRectangle.Click += (sender, e) => SelectDrawTool(DrawType.Rectangle);
            btnCircle.Click += (sender, e) => SelectDrawTool(DrawType.Circle);
            btnEllipse.Click += (sender, e) => SelectDrawTool(DrawType.Ellipse);
            btnTriangle.Click += (sender, e) => SelectDrawTool(DrawType.Triangle);
            btnDiamond.Click += (sender, e) => SelectDrawTool(DrawType.Diamond);
            btnParallelogram.Click += (sender, e) => SelectDrawTool(DrawType.Parallelogram);
            btnPolygon.Click += (sender, e) => SelectDrawTool(DrawType.Polygon);

            btnCube.Click += (sender, e) => ShowShape3DDialog(DrawType.Cube);
            btnSphere.Click += (sender, e) => ShowShape3DDialog(DrawType.Sphere);
            btnPyramid.Click += (sender, e) => ShowShape3DDialog(DrawType.Pyramid);
            btnPrism.Click += (sender, e) => ShowShape3DDialog(DrawType.Prism);
            btnCylinder.Click += (sender, e) => ShowShape3DDialog(DrawType.Cylinder);

            btnBlack.Click += (sender, e) => SetSelectedColor(Color.Black);
            btnWhite.Click += (sender, e) => SetSelectedColor(Color.White);
            btnRed.Click += (sender, e) => SetSelectedColor(Color.Red);
            btnOrange.Click += (sender, e) => SetSelectedColor(Color.Orange);
            btnYellow.Click += (sender, e) => SetSelectedColor(Color.Gold);
            btnGreen.Click += (sender, e) => SetSelectedColor(Color.Green);
            btnBlue.Click += (sender, e) => SetSelectedColor(Color.Blue);
            btnPurple.Click += (sender, e) => SetSelectedColor(Color.Purple);
            btnGray.Click += (sender, e) => SetSelectedColor(Color.Gray);

            btnFill.Click += (sender, e) => FillSelectedOrEnterFillMode();
            btnClear.Click += (sender, e) => ClearCanvas();
            btnReflectO.Click += (sender, e) => ApplyQuickReflect(ReflectType.ReflectOrigin);
            btnReflectX.Click += (sender, e) => ApplyQuickReflect(ReflectType.ReflectX);
            btnReflectY.Click += (sender, e) => ApplyQuickReflect(ReflectType.ReflectY);

            Shown += (sender, e) => QueueRenderCanvas();
            canvas.MouseDown += canvas_MouseDown;
            canvas.Resize += (sender, e) => QueueRenderCanvas();
            Resize += (sender, e) =>
            {
                ApplyResponsiveCanvasLayout();
                QueueRenderCanvas();
            };
            treeView1.AfterSelect += treeView1_AfterSelect;
        }

        private void ConfigureCanvasLayout()
        {
            tableLayoutPanel1.Margin = Padding.Empty;
            tableLayoutPanel2.Margin = Padding.Empty;
            tableLayoutPanel3.Margin = Padding.Empty;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel3.Dock = DockStyle.Fill;

            canvas.Margin = Padding.Empty;
            canvas.SizeMode = PictureBoxSizeMode.Normal;
            canvas.Dock = DockStyle.Fill;
            treeView1.Dock = DockStyle.Fill;
            inspectorGeometry.Dock = DockStyle.Fill;
            inspectorGeometry.HorizontalScrollbar = true;

            ApplyResponsiveCanvasLayout();
        }

        private void ApplyResponsiveCanvasLayout()
        {
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();

            if (tableLayoutPanel1.RowStyles.Count >= 3)
            {
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel1.RowStyles[0].Height = GetToolbarHeight();
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 100F;
                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Absolute;
                tableLayoutPanel1.RowStyles[2].Height = Math.Max(24, statusStrip1.GetPreferredSize(Size.Empty).Height + 2);
            }

            if (tableLayoutPanel2.RowStyles.Count >= 2)
            {
                tableLayoutPanel2.RowStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel2.RowStyles[0].Height = Math.Max(28, menuStrip1.GetPreferredSize(Size.Empty).Height + 2);
                tableLayoutPanel2.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel2.RowStyles[1].Height = 100F;
            }

            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Clear();
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 360F));
            tableLayoutPanel3.SetColumn(treeView1, 0);
            tableLayoutPanel3.SetColumnSpan(treeView1, 1);
            tableLayoutPanel3.SetColumn(canvas, 1);
            tableLayoutPanel3.SetColumnSpan(canvas, 1);
            tableLayoutPanel3.SetColumn(inspectorGeometry, 2);
            tableLayoutPanel3.SetColumnSpan(inspectorGeometry, 1);

            tableLayoutPanel3.ResumeLayout();
            tableLayoutPanel2.ResumeLayout();
            tableLayoutPanel1.ResumeLayout();
        }

        private int GetToolbarHeight()
        {
            if (ClientSize.Height <= 0)
                return 180;

            return Math.Max(160, Math.Min(230, (int)(ClientSize.Height * 0.18)));
        }

        private void EnsureRefreshMenuItem()
        {
            if (btnRefresh != null)
                return;

            btnRefresh = new ToolStripMenuItem
            {
                Name = "btnRefresh",
                Text = "Refresh"
            };

            int exitIndex = menuStrip1.Items.IndexOf(btnExitApplication);
            if (exitIndex >= 0)
                menuStrip1.Items.Insert(exitIndex, btnRefresh);
            else
                menuStrip1.Items.Add(btnRefresh);
        }

        private void SelectDrawTool(DrawType drawType)
        {
            _context.ToolType = ToolType.Draw;
            _context.DrawType = drawType;
            ResetClickState();
            UpdateDrawingStatus();
        }

        private void ResetClickState()
        {
            _clickedPoints.Clear();
            RenderCanvas();
        }

        private int GetRequiredClickCount(DrawType drawType)
        {
            switch (drawType)
            {
                case DrawType.Point: return 1;
                case DrawType.Line: return 2;
                case DrawType.Rectangle: return 2;
                case DrawType.Circle: return 2;
                case DrawType.Ellipse: return 2;
                case DrawType.Diamond: return 2;
                case DrawType.Triangle: return 3;
                case DrawType.Parallelogram: return 3;
                case DrawType.Polygon: return -1; // unlimited, finish with right-click
                default: return 2;
            }
        }

        private string GetClickHint(DrawType drawType, int currentCount)
        {
            switch (drawType)
            {
                case DrawType.Point:
                    return "Click vị trí điểm";
                case DrawType.Line:
                    return currentCount == 0 ? "Click điểm đầu" : "Click điểm cuối";
                case DrawType.Rectangle:
                    return currentCount == 0 ? "Click góc thứ nhất" : "Click góc đối diện";
                case DrawType.Circle:
                    return currentCount == 0 ? "Click tâm" : "Click điểm trên đường tròn (xác định bán kính)";
                case DrawType.Ellipse:
                    return currentCount == 0 ? "Click tâm" : "Click góc (xác định RadiusX, RadiusY)";
                case DrawType.Diamond:
                    return currentCount == 0 ? "Click tâm" : "Click góc (xác định RadiusX, RadiusY)";
                case DrawType.Triangle:
                    return $"Click đỉnh {(char)('A' + currentCount)} ({currentCount + 1}/3)";
                case DrawType.Parallelogram:
                    return $"Click đỉnh {(char)('A' + currentCount)} ({currentCount + 1}/3)";
                case DrawType.Polygon:
                    return currentCount < 3
                        ? $"Click đỉnh {currentCount + 1} (cần tối thiểu 3)"
                        : $"Click đỉnh {currentCount + 1} | Chuột phải để hoàn thành";
                default:
                    return "Click trên canvas";
            }
        }

        private void UpdateDrawingStatus()
        {
            if (_context.ToolType == ToolType.Draw && _context.CanDraw2D)
            {
                string hint = GetClickHint(_context.DrawType, _clickedPoints.Count);
                statusFormSize.Text = $"[{_context.DrawType}] {hint}";
            }
        }

        private void SetSelectedColor(Color color)
        {
            _context.CurrentColor = color;
            lbColor.Text = $"Color: {color.Name}";

            if (_context.ToolType == ToolType.Fill && SelectedObject is Shape2D shape2D)
            {
                shape2D.Style.IsFilled = true;
                shape2D.Style.FillColor = color;
                RenderCanvas();
            }
        }

        private void SetMode(bool is3DMode)
        {
            _context.Is3DMode = is3DMode;
            _context.ToolType = ToolType.Draw;
            _context.DrawType = is3DMode ? DrawType.Cube : DrawType.Line;
            ResetClickState();

            btnPoint.Enabled = !is3DMode;
            btnLine.Enabled = !is3DMode;
            btnRectangle.Enabled = !is3DMode;
            btnCircle.Enabled = !is3DMode;
            btnEllipse.Enabled = !is3DMode;
            btnTriangle.Enabled = !is3DMode;
            btnDiamond.Enabled = !is3DMode;
            btnParallelogram.Enabled = !is3DMode;
            btnPolygon.Enabled = !is3DMode;

            btnCube.Enabled = is3DMode;
            btnPrism.Enabled = is3DMode;
            btnCylinder.Enabled = is3DMode;
            btnPyramid.Enabled = is3DMode;
            btnSphere.Enabled = is3DMode;

            btnDraw2D.Checked = !is3DMode;
            btnDraw3D.Checked = is3DMode;
            RenderCanvas();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point2D world = _renderService.ScreenToWorld(e.Location, canvas.ClientSize);
            statusMousePos.Text = $"Screen: {e.Location.X}, {e.Location.Y} | O: {world.X:F2}, {world.Y:F2}";
            statusFormSize.Text = $"Canvas: {canvas.Width} x {canvas.Height}";
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Point2D world = _renderService.ScreenToWorld(e.Location, canvas.ClientSize);

            if (_context.ToolType == ToolType.Selection || _context.ToolType == ToolType.Fill)
            {
                GraphicObject hit = _selectionService.HitTest(_objects, world, _renderService);
                _selectionService.SelectOnly(_objects, hit);

                if (_context.ToolType == ToolType.Fill && hit is Shape2D shape2D)
                {
                    shape2D.Style.IsFilled = true;
                    shape2D.Style.FillColor = _context.CurrentColor;
                }

                UpdateObjectTree();
                RenderCanvas();
                return;
            }

            if (_context.CanDraw2D)
            {
                HandleDraw2DClick(world, e.Button);
                return;
            }
        }

        private void HandleDraw2DClick(Point2D world, MouseButtons button)
        {
            DrawType drawType = _context.DrawType;

            // Polygon: right-click finishes
            if (drawType == DrawType.Polygon && button == MouseButtons.Right)
            {
                if (_clickedPoints.Count >= 3)
                {
                    TryCreateObjectFromClicks();
                }
                else
                {
                    MessageBox.Show("Polygon cần tối thiểu 3 đỉnh.", "Chưa đủ điểm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (button != MouseButtons.Left)
                return;

            _clickedPoints.Add(world);
            RenderCanvas(); // re-render to show clicked point markers

            int required = GetRequiredClickCount(drawType);

            if (required > 0 && _clickedPoints.Count >= required)
            {
                TryCreateObjectFromClicks();
            }
            else
            {
                UpdateDrawingStatus();
            }
        }

        private void TryCreateObjectFromClicks()
        {
            try
            {
                GraphicObject obj = _drawingController.CreateObjectFromClicks(
                    _context.DrawType, _clickedPoints, _context.CurrentColor);
                _objects.Add(obj);
                _selectionService.SelectOnly(_objects, obj);
                UpdateObjectTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Không thể tạo đối tượng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                ResetClickState();
                UpdateDrawingStatus();
            }
        }

        private void ShowShape3DDialog(DrawType drawType)
        {
            _context.Is3DMode = true;
            _context.ToolType = ToolType.Draw;
            _context.DrawType = drawType;
            SetMode(is3DMode: true);

            using (Shape3DInputDialog dialog = new Shape3DInputDialog(drawType))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        GraphicObject obj = _drawingController.CreateObject3DFromParams(
                            drawType, dialog.Parameters, dialog.Position, _context.CurrentColor);
                        _objects.Add(obj);
                        _selectionService.SelectOnly(_objects, obj);
                        UpdateObjectTree();
                        RenderCanvas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Không thể tạo đối tượng 3D", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void FillSelectedOrEnterFillMode()
        {
            _context.ToolType = ToolType.Fill;

            if (SelectedObject is Shape2D shape2D)
            {
                shape2D.Style.IsFilled = true;
                shape2D.Style.FillColor = _context.CurrentColor;
                RenderCanvas();
                return;
            }
        }

        private void ApplyQuickReflect(ReflectType reflectType)
        {
            if (!(SelectedObject is Shape2D selected))
                return;

            Matrix3x3 matrix;
            if (reflectType == ReflectType.ReflectOrigin)
                matrix = Algorithms.Transform.TransformComposer2D.BuildReflectionByPoint(new Point2D(0, 0));
            else if (reflectType == ReflectType.ReflectX)
                matrix = CoreModel.Model.MatrixFactory.CreateReflection2DX();
            else
                matrix = CoreModel.Model.MatrixFactory.CreateReflection2DY();

            _transformService.ApplyTransform(selected, matrix, TransformType.Reflect);
            RenderCanvas();
        }

        private void ClearCanvas()
        {
            _objects.Clear();
            UpdateObjectTree();
            UpdateInspectorGeometry(null);
            RenderCanvas();
        }

        private void RefreshSceneView()
        {
            foreach (GraphicObject obj in _objects)
            {
                if (obj is Shape2D shape2D)
                    shape2D.RefreshInspectionGeometry();
                else if (obj is Shape3D shape3D)
                    shape3D.RefreshInspectionGeometry();
            }

            UpdateObjectTree();
            UpdateInspectorGeometry(SelectedObject);
            RenderCanvas();
        }

        private void QueueRenderCanvas()
        {
            if (!IsHandleCreated || IsDisposed)
                return;

            BeginInvoke(new Action(RenderCanvas));
        }

        private void RenderCanvas()
        {
            Size canvasSize = canvas.ClientSize;
            if (canvasSize.Width <= 0 || canvasSize.Height <= 0)
                return;

            Image oldImage = canvas.Image;
            Bitmap bmp = _renderService.Render(canvasSize, _objects, _context.Is3DMode);

            // Draw click markers for ongoing 2D point collection
            if (_clickedPoints.Count > 0)
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    for (int i = 0; i < _clickedPoints.Count; i++)
                    {
                        Point screenPt = _renderService.WorldToScreen(_clickedPoints[i], canvasSize);
                        int markerSize = 8;
                        g.FillEllipse(Brushes.Lime, screenPt.X - markerSize / 2, screenPt.Y - markerSize / 2, markerSize, markerSize);
                        g.DrawEllipse(Pens.DarkGreen, screenPt.X - markerSize / 2, screenPt.Y - markerSize / 2, markerSize, markerSize);

                        using (Font font = new Font("Segoe UI", 7))
                        {
                            string label = $"P{i + 1}({_clickedPoints[i].X:F1},{_clickedPoints[i].Y:F1})";
                            g.DrawString(label, font, Brushes.DarkGreen, screenPt.X + 6, screenPt.Y - 12);
                        }
                    }
                }
            }

            canvas.Image = bmp;
            oldImage?.Dispose();
        }

        private void UpdateObjectTree()
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            foreach (GraphicObject obj in _objects)
            {
                TreeNode node = CreateInspectorNode(obj);
                if (obj.Metadata.IsSelected)
                {
                    node.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
                    node.Expand();
                }
                treeView1.Nodes.Add(node);
            }
            treeView1.EndUpdate();

            if (SelectedObject != null)
                UpdateInspectorGeometry(SelectedObject);
            else
                UpdateInspectorGeometry(null);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is GraphicObject obj)
            {
                _selectionService.SelectOnly(_objects, obj);
                UpdateObjectTree();
                UpdateInspectorGeometry(obj);
                RenderCanvas();
            }
        }

        private TreeNode CreateInspectorNode(GraphicObject obj)
        {
            string objectName = string.IsNullOrWhiteSpace(obj.Metadata.Name)
                ? obj.GetType().Name
                : obj.Metadata.Name;

            TreeNode root = new TreeNode($"{objectName} [{obj.GetType().Name}]") { Tag = obj };

            root.Nodes.Add(CreateMetadataNode(obj));
            root.Nodes.Add(CreateStyleNode(obj));

            if (obj is Shape2D shape2D)
                root.Nodes.Add(CreateGeometry2DNode(shape2D));
            else if (obj is Shape3D shape3D)
                root.Nodes.Add(CreateGeometry3DNode(shape3D));
            else
                root.Nodes.Add(new TreeNode("GeometryInspectionData: unsupported object type") { Tag = obj });

            return root;
        }

        private void UpdateInspectorGeometry(GraphicObject obj)
        {
            inspectorGeometry.BeginUpdate();
            inspectorGeometry.Items.Clear();

            if (obj == null)
            {
                inspectorGeometry.Items.Add("No selected object.");
                inspectorGeometry.EndUpdate();
                return;
            }

            AddInspectorLine($"Object: {(string.IsNullOrWhiteSpace(obj.Metadata.Name) ? obj.GetType().Name : obj.Metadata.Name)}");
            AddInspectorLine($"Type: {obj.GetType().Name}");
            AddInspectorLine($"Visible: {obj.Metadata.IsVisible}");
            AddInspectorLine($"Stroke: {FormatColor(obj.Style.StrokeColor)}");
            AddInspectorLine("");

            if (obj is Shape2D shape2D)
                PopulateInspector2D(shape2D);
            else if (obj is Shape3D shape3D)
                PopulateInspector3D(shape3D);
            else
                AddInspectorLine("GeometryInspectionData is not available for this object type.");

            inspectorGeometry.EndUpdate();
        }

        private void PopulateInspector2D(Shape2D shape)
        {
            Matrix3x3 matrix = shape.CurrentMatrix.CurrentMatrix3x3;

            AddInspectorLine("GeometryInspectionData2D");
            AddInspectorLine($"Pivot: {FormatPoint(shape.Pivot)}");
            AddInspectorLine($"LocalBounds: {FormatBounds(shape.GetLocalBounds())}");
            AddInspectorLine("");

            AddInspectorLine($"OriginalVertices ({shape.OriginalVertices.Count})");
            for (int i = 0; i < shape.OriginalVertices.Count; i++)
                AddInspectorLine($"  V{i}: {FormatPoint(shape.OriginalVertices[i])}");
            AddInspectorLine("");

            AddInspectorLine($"After Transform ({shape.OriginalVertices.Count})");
            for (int i = 0; i < shape.OriginalVertices.Count; i++)
            {
                Point2D original = shape.OriginalVertices[i];
                AddInspectorLine($"  V{i}: {FormatPoint(original)} -> {FormatPoint(matrix.Transform(original))}");
            }
            AddInspectorLine("");

            AddInspectorLine($"ControlPoints ({shape.InspectionGeometry.ControlPoints.Count})");
            foreach (var point in shape.InspectionGeometry.ControlPoints)
                AddInspectorLine($"  {point.Name}: {FormatPoint(point.OriginalValue)} -> {FormatPoint(matrix.Transform(point.OriginalValue))}");
            AddInspectorLine("");

            AddInspectorLine($"Edges ({shape.Edges.Count})");
            foreach (var edge in shape.Edges)
                AddInspectorLine($"  {edge.StartIndex} -> {edge.EndIndex}");
            AddInspectorLine("");

            AddInspectorLine("CurrentMatrix3x3");
            for (int row = 0; row < 3; row++)
                AddInspectorLine($"  [{matrix[row, 0]:F2}, {matrix[row, 1]:F2}, {matrix[row, 2]:F2}]");
        }

        private void PopulateInspector3D(Shape3D shape)
        {
            AddInspectorLine("GeometryInspectionData3D");
            AddInspectorLine($"Pivot: {FormatPoint(shape.Pivot)}");
            AddShape3DParameterLines(shape);
            AddInspectorLine("");

            AddInspectorLine($"OriginalVertices ({shape.OriginalVertices.Count})");
            for (int i = 0; i < shape.OriginalVertices.Count; i++)
                AddInspectorLine($"  V{i}: {FormatPoint(shape.OriginalVertices[i])}");
            AddInspectorLine("");

            AddInspectorLine($"CurrentVertices ({shape.CurrentVertices.Count})");
            for (int i = 0; i < shape.CurrentVertices.Count; i++)
                AddInspectorLine($"  V{i}: {FormatPoint(shape.CurrentVertices[i])}");
            AddInspectorLine("");

            AddInspectorLine($"ControlPoints ({shape.InspectionGeometry.ControlPoints.Count})");
            foreach (var point in shape.InspectionGeometry.ControlPoints)
                AddInspectorLine($"  {point.Name}: {FormatPoint(point.OriginalValue)} -> {FormatPoint(point.CurrentValue)}");
            AddInspectorLine("");

            AddInspectorLine($"Edges ({shape.Edges.Count})");
            foreach (var edge in shape.Edges)
                AddInspectorLine($"  {edge.StartIndex} -> {edge.EndIndex}");
        }

        private void AddShape3DParameterLines(Shape3D shape)
        {
            if (shape is CubeShape cube)
            {
                AddInspectorLine($"Position: {FormatPoint(cube.Position)}");
                AddInspectorLine($"Size: {cube.Size:F2}");
            }
            else if (shape is SphereShape sphere)
            {
                AddInspectorLine($"Center: {FormatPoint(sphere.Center)}");
                AddInspectorLine($"Radius: {sphere.Radius:F2}");
                AddInspectorLine($"Stacks/Slices: {sphere.Stacks}/{sphere.Slices}");
            }
            else if (shape is CylinderShape cylinder)
            {
                AddInspectorLine($"Center: {FormatPoint(cylinder.Center)}");
                AddInspectorLine($"Radius: {cylinder.Radius:F2}");
                AddInspectorLine($"Height: {cylinder.Height:F2}");
                AddInspectorLine($"Segments: {cylinder.Segments}");
            }
            else if (shape is PrismShape prism)
            {
                AddInspectorLine($"Center: {FormatPoint(prism.Center)}");
                AddInspectorLine($"Radius: {prism.Radius:F2}");
                AddInspectorLine($"Height: {prism.Height:F2}");
                AddInspectorLine($"Sides: {prism.Sides}");
            }
            else if (shape is PyramidShape pyramid)
            {
                AddInspectorLine($"Center: {FormatPoint(pyramid.Center)}");
                AddInspectorLine($"BaseWidth: {pyramid.BaseWidth:F2}");
                AddInspectorLine($"BaseDepth: {pyramid.BaseDepth:F2}");
                AddInspectorLine($"Height: {pyramid.Height:F2}");
            }
        }

        private void AddInspectorLine(string text)
        {
            inspectorGeometry.Items.Add(text);
        }

        private TreeNode CreateMetadataNode(GraphicObject obj)
        {
            TreeNode node = new TreeNode("Metadata") { Tag = obj };
            node.Nodes.Add(new TreeNode($"Id: {obj.Metadata.Id}") { Tag = obj });
            node.Nodes.Add(new TreeNode($"Name: {obj.Metadata.Name}") { Tag = obj });
            node.Nodes.Add(new TreeNode($"Visible: {obj.Metadata.IsVisible}") { Tag = obj });
            node.Nodes.Add(new TreeNode($"Selected: {obj.Metadata.IsSelected}") { Tag = obj });
            return node;
        }

        private TreeNode CreateStyleNode(GraphicObject obj)
        {
            TreeNode node = new TreeNode("Style") { Tag = obj };
            node.Nodes.Add(new TreeNode($"Stroke: {FormatColor(obj.Style.StrokeColor)}") { Tag = obj });
            node.Nodes.Add(new TreeNode($"Fill: {FormatColor(obj.Style.FillColor)}") { Tag = obj });
            node.Nodes.Add(new TreeNode($"IsFilled: {obj.Style.IsFilled}") { Tag = obj });
            node.Nodes.Add(new TreeNode($"LinePattern: {obj.Style.LinePattern?.Name ?? "None"}") { Tag = obj });
            return node;
        }

        private TreeNode CreateGeometry2DNode(Shape2D shape)
        {
            TreeNode node = new TreeNode("GeometryInspectionData2D") { Tag = shape };
            node.Nodes.Add(new TreeNode($"Pivot: {FormatPoint(shape.Pivot)}") { Tag = shape });
            node.Nodes.Add(CreateOriginal2DNode(shape));
            node.Nodes.Add(CreateTransformed2DNode(shape));
            node.Nodes.Add(CreateEdgesNode(shape.Edges, shape));
            node.Nodes.Add(CreateMatrixNode(shape));
            return node;
        }

        private TreeNode CreateOriginal2DNode(Shape2D shape)
        {
            TreeNode node = new TreeNode("Original Geometry") { Tag = shape };

            TreeNode verticesNode = new TreeNode($"OriginalVertices ({shape.OriginalVertices.Count})") { Tag = shape };
            for (int i = 0; i < shape.OriginalVertices.Count; i++)
                verticesNode.Nodes.Add(new TreeNode($"V{i}: {FormatPoint(shape.OriginalVertices[i])}") { Tag = shape });
            node.Nodes.Add(verticesNode);

            TreeNode controlNode = new TreeNode($"ControlPoints ({shape.InspectionGeometry.ControlPoints.Count})") { Tag = shape };
            foreach (var point in shape.InspectionGeometry.ControlPoints)
                controlNode.Nodes.Add(new TreeNode($"{point.Name}: {FormatPoint(point.OriginalValue)}") { Tag = shape });
            node.Nodes.Add(controlNode);

            return node;
        }

        private TreeNode CreateTransformed2DNode(Shape2D shape)
        {
            Matrix3x3 matrix = shape.CurrentMatrix.CurrentMatrix3x3;
            TreeNode node = new TreeNode("After Transform") { Tag = shape };

            TreeNode verticesNode = new TreeNode($"TransformedVertices ({shape.OriginalVertices.Count})") { Tag = shape };
            for (int i = 0; i < shape.OriginalVertices.Count; i++)
            {
                Point2D original = shape.OriginalVertices[i];
                Point2D current = matrix.Transform(original);
                verticesNode.Nodes.Add(new TreeNode($"V{i}: {FormatPoint(original)} -> {FormatPoint(current)}") { Tag = shape });
            }
            node.Nodes.Add(verticesNode);

            TreeNode controlNode = new TreeNode($"TransformedControlPoints ({shape.InspectionGeometry.ControlPoints.Count})") { Tag = shape };
            foreach (var point in shape.InspectionGeometry.ControlPoints)
            {
                Point2D current = matrix.Transform(point.OriginalValue);
                controlNode.Nodes.Add(new TreeNode($"{point.Name}: {FormatPoint(point.OriginalValue)} -> {FormatPoint(current)}") { Tag = shape });
            }
            node.Nodes.Add(controlNode);

            node.Nodes.Add(new TreeNode($"LocalBounds: {FormatBounds(shape.GetLocalBounds())}") { Tag = shape });
            node.Nodes.Add(new TreeNode($"TransformHistory.Count: {shape.TransformHistory.Count}") { Tag = shape });
            return node;
        }

        private TreeNode CreateGeometry3DNode(Shape3D shape)
        {
            TreeNode node = new TreeNode("GeometryInspectionData3D") { Tag = shape };
            node.Nodes.Add(new TreeNode($"Pivot: {FormatPoint(shape.Pivot)}") { Tag = shape });

            TreeNode originalNode = new TreeNode($"OriginalVertices ({shape.OriginalVertices.Count})") { Tag = shape };
            for (int i = 0; i < shape.OriginalVertices.Count; i++)
                originalNode.Nodes.Add(new TreeNode($"V{i}: {FormatPoint(shape.OriginalVertices[i])}") { Tag = shape });
            node.Nodes.Add(originalNode);

            TreeNode currentNode = new TreeNode($"CurrentVertices ({shape.CurrentVertices.Count})") { Tag = shape };
            for (int i = 0; i < shape.CurrentVertices.Count; i++)
                currentNode.Nodes.Add(new TreeNode($"V{i}: {FormatPoint(shape.CurrentVertices[i])}") { Tag = shape });
            node.Nodes.Add(currentNode);

            node.Nodes.Add(CreateEdgesNode(shape.Edges, shape));

            if (shape.OriginalVertices.Count == 0 && shape.Edges.Count == 0)
                node.Nodes.Add(new TreeNode("Note: 3D shape data object has no built GeometryInspectionData yet.") { Tag = shape });

            return node;
        }

        private TreeNode CreateEdgesNode(IEnumerable<Project_CG_Paint.Data.GeometryInspection.EdgeIndex> edges, GraphicObject owner)
        {
            List<Project_CG_Paint.Data.GeometryInspection.EdgeIndex> edgeList = edges.ToList();
            TreeNode node = new TreeNode($"Edges ({edgeList.Count})") { Tag = owner };
            for (int i = 0; i < edgeList.Count; i++)
                node.Nodes.Add(new TreeNode($"E{i}: {edgeList[i].StartIndex} -> {edgeList[i].EndIndex}") { Tag = owner });
            return node;
        }

        private TreeNode CreateMatrixNode(Shape2D shape)
        {
            TreeNode node = new TreeNode("CurrentMatrix3x3") { Tag = shape };
            Matrix3x3 matrix = shape.CurrentMatrix.CurrentMatrix3x3;
            for (int row = 0; row < 3; row++)
                node.Nodes.Add(new TreeNode($"[{matrix[row, 0]:F2}, {matrix[row, 1]:F2}, {matrix[row, 2]:F2}]") { Tag = shape });
            return node;
        }

        private static string FormatPoint(Point2D point)
        {
            return $"({point.X:F2}, {point.Y:F2})";
        }

        private static string FormatPoint(Point3D point)
        {
            return $"({point.X:F2}, {point.Y:F2}, {point.Z:F2})";
        }

        private static string FormatBounds(Project_CG_Paint.CoreModel.Geometry.BoundingBox2D bounds)
        {
            if (bounds == null)
                return "None";

            return $"Min({bounds.MinX:F2}, {bounds.MinY:F2}) Max({bounds.MaxX:F2}, {bounds.MaxY:F2})";
        }

        private static string FormatColor(Color color)
        {
            if (color == Color.Transparent)
                return "Transparent";

            return $"{color.Name} [A:{color.A}, R:{color.R}, G:{color.G}, B:{color.B}]";
        }

        private void btnOpenAnimation_Click(object sender, EventArgs e)
        {
            AnimationForm animationForm = new AnimationForm();
            animationForm.Show();
        }

        private void btnOpenTransform_Click(object sender, EventArgs e)
        {
            TransformForm transformForm = new TransformForm(_objects, SelectedObject, ApplyTransformFromForm);
            transformForm.Show();
        }

        private void ApplyTransformFromForm(GraphicObject target, Matrix3x3 matrix, TransformType type, Dictionary<string, double> parameters)
        {
            if (target is Shape2D shape2D)
            {
                _transformService.ApplyTransform(shape2D, matrix, type, parameters);
                _selectionService.SelectOnly(_objects, target);
                UpdateObjectTree();
                RenderCanvas();
            }
        }

        private void btnExitApplication_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
