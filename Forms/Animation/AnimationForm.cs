using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Data.Shapes2D;
using Project_CG_Paint.Data.Styles;
using Project_CG_Paint.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project_CG_Paint.Forms.Animation
{
    public partial class AnimationForm : Form
    {
        private const double SceneDurationSeconds = 16.0;
        private const double DesignedSceneHalfWidth = 105.0;
        private const double DesignedSceneHalfHeight = 70.0;
        private readonly RenderService _renderService = new RenderService();
        private readonly Timer _sceneTimer = new Timer();
        private DateTime _sceneStartTime;
        private bool _scene1Running;

        public AnimationForm()
        {
            InitializeComponent();
            ConfigureSceneCanvas();
            WireSceneEvents();
            StartScene1();
        }

        private void ConfigureSceneCanvas()
        {
            tableLayoutPanel1.Margin = Padding.Empty;
            tableLayoutPanel2.Margin = Padding.Empty;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel2.Dock = DockStyle.Fill;
            treeView1.Dock = DockStyle.Fill;

            sceneCanvas.Margin = Padding.Empty;
            sceneCanvas.SizeMode = PictureBoxSizeMode.Normal;
            sceneCanvas.Dock = DockStyle.Fill;

            if (tableLayoutPanel1.RowStyles.Count >= 3)
            {
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel1.RowStyles[0].Height = Math.Max(28, menuStrip1.GetPreferredSize(Size.Empty).Height + 2);
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 100F;
                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Absolute;
                tableLayoutPanel1.RowStyles[2].Height = Math.Max(34, timeline.GetPreferredSize(Size.Empty).Height + 8);
            }

            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Clear();
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.SetColumn(treeView1, 0);
            tableLayoutPanel2.SetColumnSpan(treeView1, 1);
            tableLayoutPanel2.SetColumn(sceneCanvas, 1);
            tableLayoutPanel2.SetColumnSpan(sceneCanvas, 1);
            tableLayoutPanel2.PerformLayout();
        }

        private void WireSceneEvents()
        {
            scene1.Click += (sender, e) => StartScene1();
            Shown += (sender, e) => QueueRenderScene1Frame();
            Resize += (sender, e) =>
            {
                ConfigureSceneCanvas();
                QueueRenderScene1Frame();
            };
            sceneCanvas.Resize += (sender, e) => QueueRenderScene1Frame();
            FormClosed += (sender, e) =>
            {
                _sceneTimer.Stop();
                _sceneTimer.Dispose();
            };
            _sceneTimer.Interval = 33;
            _sceneTimer.Tick += SceneTimer_Tick;
        }

        private void StartScene1()
        {
            _scene1Running = true;
            _sceneStartTime = DateTime.Now;
            PopulateSceneTree();
            _sceneTimer.Start();
            QueueRenderScene1Frame();
        }

        private void QueueRenderScene1Frame()
        {
            if (!IsHandleCreated)
                return;

            BeginInvoke(new Action(() => RenderScene1Frame(GetSceneTime())));
        }

        private void SceneTimer_Tick(object sender, EventArgs e)
        {
            if (_scene1Running)
                RenderScene1Frame(GetSceneTime());
        }

        private double GetSceneTime()
        {
            if (!_scene1Running)
                return 0;

            double elapsed = (DateTime.Now - _sceneStartTime).TotalSeconds;
            return elapsed % SceneDurationSeconds;
        }

        private void RenderScene1Frame(double time)
        {
            Size canvasSize = sceneCanvas.ClientSize;
            if (canvasSize.Width <= 0 || canvasSize.Height <= 0)
                return;

            List<GraphicObject> objects = BuildScene1(time, canvasSize);
            Image oldImage = sceneCanvas.Image;
            sceneCanvas.Image = _renderService.Render(canvasSize, objects, is3DMode: false);
            oldImage?.Dispose();

            timeline.Text = $"Scene 1 | t = {time:0.00}s | translate car/missile/plane, rotate wheels/fall, scale explosion";
        }

        private List<GraphicObject> BuildScene1(double time, Size canvasSize)
        {
            List<GraphicObject> objects = new List<GraphicObject>();

            AddWorld(objects);
            AddBridge(objects);
            AddSun(objects, time);
            AddCar(objects, time);
            AddMissileAndExplosion(objects, time);
            AddPlane(objects, time);
            FitSceneToCanvas(objects, canvasSize);

            return objects;
        }

        private void FitSceneToCanvas(List<GraphicObject> objects, Size canvasSize)
        {
            double visibleHalfWidth = canvasSize.Width / (RenderService.PixelsPerUnit * 2.0) - 4;
            double visibleHalfHeight = canvasSize.Height / (RenderService.PixelsPerUnit * 2.0) - 4;
            double scale = Math.Max(0.25, Math.Min(1.0, Math.Min(visibleHalfWidth / DesignedSceneHalfWidth, visibleHalfHeight / DesignedSceneHalfHeight)));

            if (scale >= 0.999)
                return;

            Matrix3x3 fitMatrix = ScaleAround(new Point2D(0, 0), scale, scale);
            foreach (GraphicObject obj in objects)
            {
                if (obj is Shape2D shape)
                    shape.CurrentMatrix.CurrentMatrix3x3 = shape.CurrentMatrix.CurrentMatrix3x3 * fitMatrix;
            }
        }

        private void AddWorld(List<GraphicObject> objects)
        {
            AddFilledRectangle(objects, "Sky panel", new Point2D(-95, 15), new Point2D(95, 60),
                Color.FromArgb(210, 235, 255), Color.FromArgb(210, 235, 255));
            AddFilledRectangle(objects, "River", new Point2D(-95, -52), new Point2D(95, -32),
                Color.FromArgb(90, 170, 220), Color.FromArgb(70, 140, 200));
            AddFilledRectangle(objects, "Ground", new Point2D(-95, -60), new Point2D(95, -52),
                Color.FromArgb(70, 165, 95), Color.FromArgb(55, 135, 75));

            for (int i = 0; i < 7; i++)
            {
                double x = -85 + i * 30;
                AddLine(objects, "River wave", new Point2D(x, -42), new Point2D(x + 12, -42), Color.White);
                AddLine(objects, "River wave", new Point2D(x + 5, -47), new Point2D(x + 18, -47), Color.White);
            }
        }

        private void AddBridge(List<GraphicObject> objects)
        {
            AddFilledRectangle(objects, "Bridge road", new Point2D(-85, -25), new Point2D(85, -20),
                Color.FromArgb(90, 90, 95), Color.FromArgb(60, 60, 65));

            for (int x = -80; x <= 80; x += 20)
            {
                AddLine(objects, "Bridge pillar", new Point2D(x, -25), new Point2D(x - 8, -52), Color.FromArgb(70, 70, 75));
                AddLine(objects, "Bridge pillar", new Point2D(x, -25), new Point2D(x + 8, -52), Color.FromArgb(70, 70, 75));
            }

            for (int x = -80; x <= 80; x += 10)
            {
                AddLine(objects, "Bridge rail", new Point2D(x, -20), new Point2D(x, -14), Color.FromArgb(95, 95, 100));
            }

            AddLine(objects, "Bridge rail top", new Point2D(-88, -14), new Point2D(88, -14), Color.FromArgb(85, 85, 90));
        }

        private void AddSun(List<GraphicObject> objects, double time)
        {
            double pulse = 1.0 + 0.08 * Math.Sin(time * 2.0);
            Matrix3x3 matrix = ScaleAround(new Point2D(62, 42), pulse, pulse);

            AddCircle(objects, "Sun pulse scale", new Point2D(62, 42), 8, Color.Orange, Color.Gold, matrix);

            for (int i = 0; i < 12; i++)
            {
                double angle = i * 30 + time * 20;
                Matrix3x3 rayMatrix = RotateAround(new Point2D(62, 42), angle);
                AddLine(objects, "Sun ray rotation", new Point2D(62, 53), new Point2D(62, 58), Color.Orange, rayMatrix);
            }
        }

        private void AddCar(List<GraphicObject> objects, double time)
        {
            double carX = Wrap(-95 + time * 15, -95, 95);
            Matrix3x3 carMove = MatrixFactory.CreateTranslation2D(new Point2D(carX, -16));

            AddFilledRectangle(objects, "Car body translate", new Point2D(-10, 0), new Point2D(10, 5),
                Color.FromArgb(210, 35, 45), Color.FromArgb(130, 20, 28), carMove);
            AddPolygon(objects, "Car cabin translate", new[]
            {
                new Point2D(-5, 5),
                new Point2D(0, 10),
                new Point2D(7, 10),
                new Point2D(11, 5)
            }, Color.FromArgb(235, 70, 80), Color.FromArgb(160, 30, 40), carMove);

            double wheelAngle = -time * 360;
            Matrix3x3 leftWheel = RotateAround(new Point2D(-6, 0), wheelAngle) * carMove;
            Matrix3x3 rightWheel = RotateAround(new Point2D(6, 0), wheelAngle) * carMove;

            AddCircle(objects, "Left wheel rotation", new Point2D(-6, 0), 3, Color.Black, Color.FromArgb(35, 35, 35), leftWheel);
            AddCircle(objects, "Right wheel rotation", new Point2D(6, 0), 3, Color.Black, Color.FromArgb(35, 35, 35), rightWheel);
            AddLine(objects, "Wheel spoke rotation", new Point2D(-9, 0), new Point2D(-3, 0), Color.White, leftWheel);
            AddLine(objects, "Wheel spoke rotation", new Point2D(-6, -3), new Point2D(-6, 3), Color.White, leftWheel);
            AddLine(objects, "Wheel spoke rotation", new Point2D(3, 0), new Point2D(9, 0), Color.White, rightWheel);
            AddLine(objects, "Wheel spoke rotation", new Point2D(6, -3), new Point2D(6, 3), Color.White, rightWheel);
        }

        private void AddPlane(List<GraphicObject> objects, double time)
        {
            Matrix3x3 planeMatrix;

            if (time < 7.0)
            {
                double planeX = -78 + time * 13;
                double planeY = 35 + Math.Sin(time * 1.2) * 2;
                planeMatrix = MatrixFactory.CreateTranslation2D(new Point2D(planeX, planeY));
            }
            else
            {
                double fall = time - 7.0;
                double planeX = 13 + fall * 8;
                double planeY = 34 - fall * fall * 3.1;
                double angle = -20 - fall * 52;
                double shrink = Math.Max(0.35, 1.0 - fall * 0.055);
                planeMatrix = ScaleAround(new Point2D(0, 0), shrink, shrink)
                    * RotateAround(new Point2D(0, 0), angle)
                    * MatrixFactory.CreateTranslation2D(new Point2D(planeX, planeY));
            }

            AddPolygon(objects, "Plane body translate/rotate/scale", new[]
            {
                new Point2D(-13, -2),
                new Point2D(9, -2),
                new Point2D(15, 0),
                new Point2D(9, 2),
                new Point2D(-13, 2)
            }, Color.FromArgb(230, 230, 235), Color.FromArgb(120, 120, 130), planeMatrix);

            AddTriangle(objects, "Plane wing rotation", new Point2D(-3, 2), new Point2D(4, 10), new Point2D(7, 2),
                Color.FromArgb(200, 205, 215), Color.FromArgb(110, 115, 125), planeMatrix);
            AddTriangle(objects, "Plane tail rotation", new Point2D(-12, 2), new Point2D(-17, 8), new Point2D(-8, 2),
                Color.FromArgb(200, 205, 215), Color.FromArgb(110, 115, 125), planeMatrix);

            if (time >= 7.0)
            {
                double fall = time - 7.0;
                Matrix3x3 smokeMatrix = ScaleAround(new Point2D(0, 0), 1 + fall * 0.18, 1 + fall * 0.18)
                    * MatrixFactory.CreateTranslation2D(new Point2D(15 + fall * 8, 30 - fall * fall * 2.5));

                AddCircle(objects, "Smoke scale", new Point2D(-12, 0), 4, Color.Gray, Color.FromArgb(120, 120, 120), smokeMatrix);
                AddCircle(objects, "Smoke scale", new Point2D(-17, 4), 3, Color.Gray, Color.FromArgb(150, 150, 150), smokeMatrix);
            }
        }

        private void AddMissileAndExplosion(List<GraphicObject> objects, double time)
        {
            if (time < 7.0)
            {
                double missileX = 88 - time * 11;
                double missileY = 20 + time * 2.2;
                Matrix3x3 missileMatrix = RotateAround(new Point2D(0, 0), 160)
                    * MatrixFactory.CreateTranslation2D(new Point2D(missileX, missileY));

                AddFilledRectangle(objects, "Missile translate", new Point2D(-8, -1), new Point2D(6, 1),
                    Color.FromArgb(95, 95, 95), Color.FromArgb(45, 45, 45), missileMatrix);
                AddTriangle(objects, "Missile nose", new Point2D(6, -2), new Point2D(11, 0), new Point2D(6, 2),
                    Color.FromArgb(160, 30, 30), Color.FromArgb(120, 20, 20), missileMatrix);
                AddTriangle(objects, "Missile flame scale", new Point2D(-8, -2), new Point2D(-15, 0), new Point2D(-8, 2),
                    Color.OrangeRed, Color.Orange, ScaleAround(new Point2D(-10, 0), 1.0 + 0.3 * Math.Sin(time * 8), 1.0) * missileMatrix);
            }

            if (time >= 6.5 && time <= 10.5)
            {
                double blastTime = time - 6.5;
                double blastScale = 0.4 + blastTime * 0.85;
                Matrix3x3 blastMatrix = ScaleAround(new Point2D(12, 35), blastScale, blastScale);

                AddCircle(objects, "Explosion scale", new Point2D(12, 35), 6, Color.OrangeRed, Color.Gold, blastMatrix);
                AddCircle(objects, "Explosion outer scale", new Point2D(12, 35), 10, Color.Red, Color.Transparent, blastMatrix);

                for (int i = 0; i < 10; i++)
                {
                    double angle = i * 36 + blastTime * 85;
                    Matrix3x3 shardMatrix = RotateAround(new Point2D(12, 35), angle)
                        * ScaleAround(new Point2D(12, 35), blastScale, blastScale);
                    AddLine(objects, "Explosion shard rotation/scale", new Point2D(12, 35), new Point2D(12, 47), Color.OrangeRed, shardMatrix);
                }
            }
        }

        private Shape2D AddLine(List<GraphicObject> objects, string name, Point2D start, Point2D end, Color stroke, Matrix3x3 matrix = null)
        {
            return AddShape(objects, new LineShape(start, end), name, stroke, Color.Transparent, false, matrix);
        }

        private Shape2D AddCircle(List<GraphicObject> objects, string name, Point2D center, double radius, Color stroke, Color fill, Matrix3x3 matrix = null)
        {
            return AddShape(objects, new CircleShape(center, radius), name, stroke, fill, fill != Color.Transparent, matrix);
        }

        private Shape2D AddFilledRectangle(List<GraphicObject> objects, string name, Point2D topLeft, Point2D bottomRight, Color fill, Color stroke, Matrix3x3 matrix = null)
        {
            return AddShape(objects, new RectangleShape(topLeft, bottomRight), name, stroke, fill, true, matrix);
        }

        private Shape2D AddTriangle(List<GraphicObject> objects, string name, Point2D a, Point2D b, Point2D c, Color fill, Color stroke, Matrix3x3 matrix = null)
        {
            return AddShape(objects, new TriangleShape(a, b, c), name, stroke, fill, true, matrix);
        }

        private Shape2D AddPolygon(List<GraphicObject> objects, string name, IEnumerable<Point2D> vertices, Color fill, Color stroke, Matrix3x3 matrix = null)
        {
            return AddShape(objects, new PolygonShape(vertices), name, stroke, fill, true, matrix);
        }

        private Shape2D AddShape(List<GraphicObject> objects, Shape2D shape, string name, Color stroke, Color fill, bool isFilled, Matrix3x3 matrix)
        {
            shape.Metadata.Name = name;
            shape.Style = new ShapeStyles
            {
                StrokeColor = stroke,
                FillColor = fill,
                IsFilled = isFilled,
                LinePattern = new LinePattern
                {
                    Name = "Solid",
                    Pattern = new List<int> { 1 }
                }
            };

            if (matrix != null)
                shape.CurrentMatrix.CurrentMatrix3x3 = matrix;

            objects.Add(shape);
            return shape;
        }

        private static Matrix3x3 RotateAround(Point2D pivot, double angleDegrees)
        {
            return MatrixFactory.CreateTranslation2D(new Point2D(-pivot.X, -pivot.Y))
                * MatrixFactory.CreateRotation2D(angleDegrees)
                * MatrixFactory.CreateTranslation2D(pivot);
        }

        private static Matrix3x3 ScaleAround(Point2D pivot, double scaleX, double scaleY)
        {
            return MatrixFactory.CreateTranslation2D(new Point2D(-pivot.X, -pivot.Y))
                * MatrixFactory.CreateScale2D(scaleX, scaleY)
                * MatrixFactory.CreateTranslation2D(pivot);
        }

        private static double Wrap(double value, double min, double max)
        {
            double range = max - min;
            while (value > max)
                value -= range;
            while (value < min)
                value += range;
            return value;
        }

        private void PopulateSceneTree()
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode("Scene 1");
            root.Nodes.Add("Bridge and river");
            root.Nodes.Add("Car: translate + wheel rotation");
            root.Nodes.Add("Sun: pulse scale + ray rotation");
            root.Nodes.Add("Missile: translate + flame scale");
            root.Nodes.Add("Explosion: scale + shard rotation");
            root.Nodes.Add("Plane: translate, rotate, scale, fall");
            treeView1.Nodes.Add(root);
            root.Expand();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sceneTimer.Stop();
            Application.Exit();
        }
    }
}
