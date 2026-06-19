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
        private enum AnimationScene
        {
            Scene1,
            Scene2
        }

        private const double Scene1DurationSeconds = 16.0;
        private const double Scene2DurationSeconds = 30.0;
        private const double DesignedSceneHalfWidth = 105.0;
        private const double DesignedSceneHalfHeight = 70.0;
        private readonly RenderService _renderService = new RenderService();
        private readonly Timer _sceneTimer = new Timer();
        private DateTime _sceneStartTime;
        private bool _sceneRunning;
        private AnimationScene _activeScene = AnimationScene.Scene1;

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
            scene2.Click += (sender, e) => StartScene2();
            Shown += (sender, e) => QueueRenderSceneFrame();
            Resize += (sender, e) =>
            {
                ConfigureSceneCanvas();
                QueueRenderSceneFrame();
            };
            sceneCanvas.Resize += (sender, e) => QueueRenderSceneFrame();
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
            _activeScene = AnimationScene.Scene1;
            _sceneRunning = true;
            _sceneStartTime = DateTime.Now;
            PopulateSceneTree();
            _sceneTimer.Start();
            QueueRenderSceneFrame();
        }

        private void StartScene2()
        {
            _activeScene = AnimationScene.Scene2;
            _sceneRunning = true;
            _sceneStartTime = DateTime.Now;
            PopulateSceneTree();
            _sceneTimer.Start();
            QueueRenderSceneFrame();
        }

        private void QueueRenderSceneFrame()
        {
            if (!IsHandleCreated)
                return;

            BeginInvoke(new Action(() => RenderSceneFrame(GetSceneTime())));
        }

        private void SceneTimer_Tick(object sender, EventArgs e)
        {
            if (_sceneRunning)
                RenderSceneFrame(GetSceneTime());
        }

        private double GetSceneTime()
        {
            if (!_sceneRunning)
                return 0;

            double elapsed = (DateTime.Now - _sceneStartTime).TotalSeconds;
            return elapsed % GetActiveSceneDuration();
        }

        private double GetActiveSceneDuration()
        {
            return _activeScene == AnimationScene.Scene2
                ? Scene2DurationSeconds
                : Scene1DurationSeconds;
        }

        private void RenderSceneFrame(double time)
        {
            Size canvasSize = sceneCanvas.ClientSize;
            if (canvasSize.Width <= 0 || canvasSize.Height <= 0)
                return;

            List<GraphicObject> objects = _activeScene == AnimationScene.Scene2
                ? BuildScene2(time, canvasSize)
                : BuildScene1(time, canvasSize);

            Image oldImage = sceneCanvas.Image;
            sceneCanvas.Image = _renderService.Render(canvasSize, objects, is3DMode: false);
            oldImage?.Dispose();

            timeline.Text = _activeScene == AnimationScene.Scene2
                ? $"Scene 2 | t = {time:0.00}s / 30s | translate cars, dodge lanes, falling crate, rotate wheels, scale smoke, FINISHED"
                : $"Scene 1 | t = {time:0.00}s | translate car/missile/plane, rotate wheels/fall, scale explosion";
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

        private List<GraphicObject> BuildScene2(double time, Size canvasSize)
        {
            List<GraphicObject> objects = new List<GraphicObject>();

            AddNightCity(objects, time);
            AddRaceRoad(objects, time);
            AddRaceObstacles(objects, time);
            AddFinishLine(objects, time);
            AddRaceCars(objects, time);
            AddFinishedOverlay(objects, time);
            FitSceneToCanvas(objects, canvasSize);

            return objects;
        }

        private void AddNightCity(List<GraphicObject> objects, double time)
        {
            AddFilledRectangle(objects, "Night sky", new Point2D(-100, -65), new Point2D(100, 65),
                Color.FromArgb(8, 12, 30), Color.FromArgb(8, 12, 30));

            for (int i = 0; i < 26; i++)
            {
                double starX = -94 + (i * 37 % 188);
                double starY = 18 + (i * 17 % 42);
                double radius = 0.7 + 0.25 * Math.Sin(time * 2.0 + i);
                AddCircle(objects, "Night star pulse", new Point2D(starX, starY), radius,
                    Color.FromArgb(190, 210, 255), Color.FromArgb(190, 210, 255));
            }

            AddCircle(objects, "Moon scale glow", new Point2D(74, 50), 7,
                Color.FromArgb(245, 245, 205), Color.FromArgb(235, 235, 180),
                ScaleAround(new Point2D(74, 50), 1.0 + 0.03 * Math.Sin(time), 1.0 + 0.03 * Math.Sin(time)));

            double[] buildingX = { -96, -82, -66, -49, -31, -15, 3, 19, 38, 56, 72, 88 };
            double[] buildingW = { 12, 14, 13, 16, 12, 14, 15, 11, 16, 13, 12, 15 };
            double[] buildingH = { 45, 56, 38, 62, 49, 43, 59, 41, 64, 47, 54, 39 };

            for (int i = 0; i < buildingX.Length; i++)
            {
                double left = buildingX[i];
                double width = buildingW[i];
                double top = buildingH[i];
                Color fill = Color.FromArgb(18 + i % 3 * 7, 22 + i % 4 * 5, 42 + i % 3 * 9);
                AddFilledRectangle(objects, "Skyscraper silhouette", new Point2D(left, -12), new Point2D(left + width, top),
                    fill, Color.FromArgb(10, 12, 24));

                if (i % 3 == 0)
                {
                    AddTriangle(objects, "Skyscraper roof", new Point2D(left, top), new Point2D(left + width / 2, top + 7), new Point2D(left + width, top),
                        fill, Color.FromArgb(10, 12, 24));
                }

                AddBuildingWindows(objects, left, width, -6, top - 5, i, time);
            }

            AddFilledRectangle(objects, "City sidewalk glow", new Point2D(-100, -15), new Point2D(100, -10),
                Color.FromArgb(38, 37, 50), Color.FromArgb(72, 74, 92));
        }

        private void AddBuildingWindows(List<GraphicObject> objects, double left, double width, double bottom, double top, int buildingIndex, double time)
        {
            int columns = Math.Max(1, (int)(width / 4));
            int rows = Math.Max(1, (int)((top - bottom) / 7));

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    bool lit = ((row * 3 + col * 5 + buildingIndex) % 4) != 0;
                    bool blink = Math.Sin(time * 1.6 + row + buildingIndex) > 0.72 && ((row + col) % 3 == 0);
                    if (!lit && !blink)
                        continue;

                    double x = left + 2 + col * 4;
                    double y = bottom + 3 + row * 7;
                    Color glow = blink
                        ? Color.FromArgb(255, 238, 120)
                        : Color.FromArgb(230, 205, 95);
                    AddFilledRectangle(objects, "Building window glow", new Point2D(x, y), new Point2D(x + 1.8, y + 2.4),
                        glow, glow);
                }
            }
        }

        private void AddRaceRoad(List<GraphicObject> objects, double time)
        {
            AddFilledRectangle(objects, "Race road base", new Point2D(-100, -60), new Point2D(100, -15),
                Color.FromArgb(28, 29, 35), Color.FromArgb(18, 18, 24));
            AddFilledRectangle(objects, "Left neon curb", new Point2D(-100, -20), new Point2D(100, -18),
                Color.FromArgb(0, 170, 210), Color.FromArgb(0, 170, 210));
            AddFilledRectangle(objects, "Right neon curb", new Point2D(-100, -57), new Point2D(100, -55),
                Color.FromArgb(210, 35, 95), Color.FromArgb(210, 35, 95));

            for (int i = -8; i <= 11; i++)
            {
                double x = i * 16 - (time * 18 % 16);
                AddFilledRectangle(objects, "Animated lane dash", new Point2D(x, -38), new Point2D(x + 7, -36),
                    Color.FromArgb(220, 220, 205), Color.FromArgb(220, 220, 205));
            }

            for (int i = -8; i <= 8; i++)
            {
                double x = i * 24 - (time * 9 % 24);
                AddLine(objects, "Road speed streak", new Point2D(x, -50), new Point2D(x + 11, -50),
                    Color.FromArgb(55, 70, 100));
                AddLine(objects, "Road speed streak", new Point2D(x + 8, -27), new Point2D(x + 22, -27),
                    Color.FromArgb(55, 70, 100));
            }
        }

        private void AddRaceObstacles(List<GraphicObject> objects, double time)
        {
            AddConeObstacle(objects, "Obstacle 1 cone", new Point2D(-46, -43), 1.0, time);
            AddBarrierObstacle(objects, "Obstacle 2 barrier", new Point2D(-8, -29), time);
            AddFallingCrateObstacle(objects, time);
        }

        private void AddConeObstacle(List<GraphicObject> objects, string name, Point2D position, double scale, double time)
        {
            Matrix3x3 matrix = ScaleAround(new Point2D(0, 0), scale, scale)
                * RotateAround(new Point2D(0, 0), Math.Sin(time * 3.5) * 3.0)
                * MatrixFactory.CreateTranslation2D(position);

            AddTriangle(objects, name, new Point2D(-3, -3), new Point2D(0, 5), new Point2D(3, -3),
                Color.FromArgb(255, 125, 25), Color.FromArgb(140, 70, 10), matrix);
            AddFilledRectangle(objects, "Cone white stripe", new Point2D(-2, 0), new Point2D(2, 1),
                Color.White, Color.White, matrix);
            AddFilledRectangle(objects, "Cone base", new Point2D(-4, -4), new Point2D(4, -3),
                Color.FromArgb(55, 55, 55), Color.FromArgb(55, 55, 55), matrix);
        }

        private void AddBarrierObstacle(List<GraphicObject> objects, string name, Point2D position, double time)
        {
            Matrix3x3 wobble = RotateAround(new Point2D(0, 0), Math.Sin(time * 2.7) * 2.5)
                * MatrixFactory.CreateTranslation2D(position);

            AddFilledRectangle(objects, name, new Point2D(-8, -3), new Point2D(8, 4),
                Color.FromArgb(245, 190, 40), Color.FromArgb(80, 60, 25), wobble);
            AddLine(objects, "Barrier slash", new Point2D(-7, -3), new Point2D(-1, 4), Color.FromArgb(60, 50, 35), wobble);
            AddLine(objects, "Barrier slash", new Point2D(0, -3), new Point2D(6, 4), Color.FromArgb(60, 50, 35), wobble);
            AddFilledRectangle(objects, "Barrier stand L", new Point2D(-7, -9), new Point2D(-5, -3),
                Color.FromArgb(75, 75, 75), Color.FromArgb(60, 60, 60), wobble);
            AddFilledRectangle(objects, "Barrier stand R", new Point2D(5, -9), new Point2D(7, -3),
                Color.FromArgb(75, 75, 75), Color.FromArgb(60, 60, 60), wobble);
        }

        private void AddFallingCrateObstacle(List<GraphicObject> objects, double time)
        {
            if (time < 16.6)
                return;

            double dropStart = 17.35;
            double dropTime = Math.Max(0, time - dropStart);
            double y = Math.Max(-31, 63 - dropTime * dropTime * 26);
            double squash = time > 19.25 ? 1.0 + 0.2 * Math.Exp(-(time - 19.25) * 2.0) : 1.0;
            double angle = dropTime * 410;
            Matrix3x3 matrix = ScaleAround(new Point2D(0, 0), 1.0 + (squash - 1.0), 1.0 / squash)
                * RotateAround(new Point2D(0, 0), angle)
                * MatrixFactory.CreateTranslation2D(new Point2D(35 + Math.Sin(time * 5) * 1.4, y));

            AddEllipse(objects, "Crate shadow scale", new Point2D(35, -34), 7 + dropTime * 0.6, 2,
                Color.Transparent, Color.FromArgb(45, 45, 50));
            AddFilledRectangle(objects, "Falling crate rotate/translate", new Point2D(-5, -5), new Point2D(5, 5),
                Color.FromArgb(150, 90, 42), Color.FromArgb(80, 45, 24), matrix);
            AddLine(objects, "Crate plank", new Point2D(-5, 0), new Point2D(5, 0), Color.FromArgb(85, 45, 25), matrix);
            AddLine(objects, "Crate plank", new Point2D(0, -5), new Point2D(0, 5), Color.FromArgb(85, 45, 25), matrix);
            AddLine(objects, "Crate cross", new Point2D(-5, -5), new Point2D(5, 5), Color.FromArgb(85, 45, 25), matrix);
            AddLine(objects, "Crate cross", new Point2D(-5, 5), new Point2D(5, -5), Color.FromArgb(85, 45, 25), matrix);
        }

        private void AddFinishLine(List<GraphicObject> objects, double time)
        {
            double x = 82;
            AddLine(objects, "Finish pole", new Point2D(x, -58), new Point2D(x, -14), Color.White);
            AddLine(objects, "Finish neon pole", new Point2D(x + 1, -58), new Point2D(x + 1, -14), Color.FromArgb(0, 210, 255));

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    Color fill = (row + col) % 2 == 0 ? Color.White : Color.Black;
                    AddFilledRectangle(objects, "Finish checker", new Point2D(x + col * 3, -56 + row * 4), new Point2D(x + col * 3 + 3, -52 + row * 4),
                        fill, fill);
                }
            }

            if (time > 25.5)
            {
                double flash = 0.6 + 0.4 * Math.Sin(time * 18);
                AddLine(objects, "Finish flash scale", new Point2D(x - 8, -12), new Point2D(x + 11, -12),
                    Color.FromArgb((int)(180 + 70 * flash), 240, 255));
            }
        }

        private void AddRaceCars(List<GraphicObject> objects, double time)
        {
            double raceProgress = time < 24.0
                ? time / 24.0
                : 1.0 + SmoothStep((time - 24.0) / 6.0) * 0.22;

            double carX = -92 + raceProgress * 148;
            if (time >= 24.0)
                carX = 56 + SmoothStep((time - 24.0) / 6.0) * 37;

            double dodge1 = DodgePulse(time, 7.0, 7.5, 1.25);
            double dodge2 = DodgePulse(time, 13.4, -6.0, 1.4);
            double slalom = SlalomPulse(time, 18.2, 22.4, 6.5);
            double boostScale = time > 24.0 ? 1.0 + 0.10 * SmoothStep((time - 24.0) / 3.0) : 1.0;

            double car1Y = -47 + dodge1 + dodge2 + slalom;
            double car2Y = -30 - dodge1 * 0.75 - dodge2 * 0.8 - slalom * 0.65 + 1.8 * Math.Sin(time * 2.4);

            AddRaceCar(objects, "Red racer", carX, car1Y, Color.FromArgb(230, 25, 45), Color.FromArgb(255, 215, 65),
                time, boostScale, GetCarTilt(time, 1), time > 23.5);
            AddRaceCar(objects, "Blue racer", carX - 12 + Math.Sin(time * 0.8) * 2.0, car2Y, Color.FromArgb(30, 120, 255), Color.FromArgb(150, 235, 255),
                time + 0.4, boostScale * 0.96, GetCarTilt(time, -1), time > 24.0);
        }

        private void AddRaceCar(List<GraphicObject> objects, string name, double x, double y, Color bodyColor, Color accentColor, double time, double scale, double tilt, bool boostSmoke)
        {
            Matrix3x3 carMatrix = ScaleAround(new Point2D(0, 0), scale, scale)
                * RotateAround(new Point2D(0, 0), tilt)
                * MatrixFactory.CreateTranslation2D(new Point2D(x, y));

            AddFilledRectangle(objects, $"{name} body translate", new Point2D(-9, -3), new Point2D(9, 3),
                bodyColor, Color.FromArgb(30, 30, 35), carMatrix);
            AddPolygon(objects, $"{name} cabin", new[]
            {
                new Point2D(-4, 3),
                new Point2D(0, 7),
                new Point2D(6, 6),
                new Point2D(8, 3)
            }, Color.FromArgb(25, 30, 45), Color.FromArgb(120, 180, 220), carMatrix);
            AddPolygon(objects, $"{name} nose scale", new[]
            {
                new Point2D(9, -3),
                new Point2D(14, 0),
                new Point2D(9, 3)
            }, bodyColor, Color.FromArgb(30, 30, 35), carMatrix);
            AddLine(objects, $"{name} spoiler", new Point2D(-11, 4), new Point2D(-6, 5), accentColor, carMatrix);
            AddLine(objects, $"{name} headlight", new Point2D(11, 1), new Point2D(15, 2), Color.FromArgb(255, 255, 185), carMatrix);
            AddLine(objects, $"{name} headlight", new Point2D(11, -1), new Point2D(15, -2), Color.FromArgb(255, 255, 185), carMatrix);

            double wheelAngle = -time * 520;
            Matrix3x3 leftWheel = RotateAround(new Point2D(-5, -3), wheelAngle) * carMatrix;
            Matrix3x3 rightWheel = RotateAround(new Point2D(6, -3), wheelAngle) * carMatrix;
            AddCircle(objects, $"{name} left wheel rotation", new Point2D(-5, -3), 2.6, Color.Black, Color.FromArgb(20, 20, 22), leftWheel);
            AddCircle(objects, $"{name} right wheel rotation", new Point2D(6, -3), 2.6, Color.Black, Color.FromArgb(20, 20, 22), rightWheel);
            AddLine(objects, $"{name} left spoke rotation", new Point2D(-7.5, -3), new Point2D(-2.5, -3), Color.White, leftWheel);
            AddLine(objects, $"{name} right spoke rotation", new Point2D(3.5, -3), new Point2D(8.5, -3), Color.White, rightWheel);

            if (boostSmoke)
                AddBoostSmoke(objects, new Point2D(x - 13, y - 1), time, accentColor);
        }

        private void AddBoostSmoke(List<GraphicObject> objects, Point2D origin, double time, Color accentColor)
        {
            for (int i = 0; i < 5; i++)
            {
                double phase = (time * 5 + i) % 5;
                double scale = 0.7 + phase * 0.35;
                double x = origin.X - phase * 3.0;
                double y = origin.Y + Math.Sin(time * 4 + i) * 1.4;
                Color smoke = i % 2 == 0 ? Color.FromArgb(145, 145, 150) : Color.FromArgb(95, 95, 105);
                AddCircle(objects, "Boost smoke scale/translate", new Point2D(x, y), 2.3,
                    smoke, smoke, ScaleAround(new Point2D(x, y), scale, scale));
            }

            AddTriangle(objects, "Boost flame scale", new Point2D(origin.X + 1, origin.Y - 2), new Point2D(origin.X - 9, origin.Y), new Point2D(origin.X + 1, origin.Y + 2),
                Color.OrangeRed, accentColor, ScaleAround(origin, 1.0 + 0.3 * Math.Sin(time * 16), 1.0));
        }

        private void AddFinishedOverlay(List<GraphicObject> objects, double time)
        {
            if (time < 27.0)
                return;

            double appear = SmoothStep((time - 27.0) / 1.5);
            Matrix3x3 matrix = ScaleAround(new Point2D(-38, 43), appear, appear);
            AddSegmentText(objects, "FINISHED", new Point2D(-37, 42), 8, 12, 3, Color.FromArgb(80, 25, 20), matrix);
            AddSegmentText(objects, "FINISHED", new Point2D(-38, 43), 8, 12, 3, Color.FromArgb(255, 225, 75), matrix);
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

        private Shape2D AddEllipse(List<GraphicObject> objects, string name, Point2D center, double radiusX, double radiusY, Color stroke, Color fill, Matrix3x3 matrix = null)
        {
            return AddShape(objects, new EllipseShape(center, radiusX, radiusY), name, stroke, fill, fill != Color.Transparent, matrix);
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

        private static double DodgePulse(double time, double centerTime, double amplitude, double duration)
        {
            double distance = Math.Abs(time - centerTime);
            if (distance >= duration)
                return 0;

            double phase = 1.0 - distance / duration;
            return amplitude * Math.Sin(phase * Math.PI);
        }

        private static double SlalomPulse(double time, double startTime, double endTime, double amplitude)
        {
            if (time < startTime || time > endTime)
                return 0;

            double normalized = (time - startTime) / (endTime - startTime);
            return amplitude * Math.Sin(normalized * Math.PI * 4.0) * Math.Sin(normalized * Math.PI);
        }

        private static double GetCarTilt(double time, int direction)
        {
            double tilt = DodgePulse(time, 7.0, -8.0 * direction, 1.2)
                + DodgePulse(time, 13.4, 7.0 * direction, 1.4);

            if (time >= 18.2 && time <= 22.4)
                tilt += Math.Sin((time - 18.2) * Math.PI * 2.1) * 7.0 * direction;

            if (time > 24.0)
                tilt += Math.Sin(time * 14.0) * 1.8;

            return tilt;
        }

        private static double SmoothStep(double value)
        {
            double t = Clamp01(value);
            return t * t * (3.0 - 2.0 * t);
        }

        private static double Clamp01(double value)
        {
            if (value < 0)
                return 0;
            if (value > 1)
                return 1;
            return value;
        }

        private void AddSegmentText(List<GraphicObject> objects, string text, Point2D origin, double width, double height, double spacing, Color color, Matrix3x3 matrix = null)
        {
            double cursorX = origin.X;
            foreach (char ch in text)
            {
                if (ch == ' ')
                {
                    cursorX += width + spacing;
                    continue;
                }

                AddSegmentLetter(objects, ch, new Point2D(cursorX, origin.Y), width, height, color, matrix);
                cursorX += width + spacing;
            }
        }

        private void AddSegmentLetter(List<GraphicObject> objects, char letter, Point2D origin, double width, double height, Color color, Matrix3x3 matrix)
        {
            Point2D topLeft = origin;
            Point2D topRight = new Point2D(origin.X + width, origin.Y);
            Point2D midLeft = new Point2D(origin.X, origin.Y - height / 2.0);
            Point2D midRight = new Point2D(origin.X + width, origin.Y - height / 2.0);
            Point2D bottomLeft = new Point2D(origin.X, origin.Y - height);
            Point2D bottomRight = new Point2D(origin.X + width, origin.Y - height);
            Point2D centerTop = new Point2D(origin.X + width / 2.0, origin.Y);
            Point2D centerBottom = new Point2D(origin.X + width / 2.0, origin.Y - height);

            switch (char.ToUpperInvariant(letter))
            {
                case 'F':
                    Segment(objects, topLeft, topRight, color, matrix);
                    Segment(objects, topLeft, bottomLeft, color, matrix);
                    Segment(objects, midLeft, midRight, color, matrix);
                    break;
                case 'I':
                    Segment(objects, topLeft, topRight, color, matrix);
                    Segment(objects, centerTop, centerBottom, color, matrix);
                    Segment(objects, bottomLeft, bottomRight, color, matrix);
                    break;
                case 'N':
                    Segment(objects, topLeft, bottomLeft, color, matrix);
                    Segment(objects, bottomLeft, topRight, color, matrix);
                    Segment(objects, topRight, bottomRight, color, matrix);
                    break;
                case 'S':
                    Segment(objects, topRight, topLeft, color, matrix);
                    Segment(objects, topLeft, midLeft, color, matrix);
                    Segment(objects, midLeft, midRight, color, matrix);
                    Segment(objects, midRight, bottomRight, color, matrix);
                    Segment(objects, bottomRight, bottomLeft, color, matrix);
                    break;
                case 'H':
                    Segment(objects, topLeft, bottomLeft, color, matrix);
                    Segment(objects, topRight, bottomRight, color, matrix);
                    Segment(objects, midLeft, midRight, color, matrix);
                    break;
                case 'E':
                    Segment(objects, topLeft, topRight, color, matrix);
                    Segment(objects, topLeft, bottomLeft, color, matrix);
                    Segment(objects, midLeft, midRight, color, matrix);
                    Segment(objects, bottomLeft, bottomRight, color, matrix);
                    break;
                case 'D':
                    Segment(objects, topLeft, bottomLeft, color, matrix);
                    Segment(objects, topLeft, new Point2D(origin.X + width * 0.75, origin.Y - height * 0.12), color, matrix);
                    Segment(objects, new Point2D(origin.X + width * 0.75, origin.Y - height * 0.12), new Point2D(origin.X + width, origin.Y - height / 2.0), color, matrix);
                    Segment(objects, new Point2D(origin.X + width, origin.Y - height / 2.0), new Point2D(origin.X + width * 0.75, origin.Y - height * 0.88), color, matrix);
                    Segment(objects, new Point2D(origin.X + width * 0.75, origin.Y - height * 0.88), bottomLeft, color, matrix);
                    break;
            }
        }

        private void Segment(List<GraphicObject> objects, Point2D start, Point2D end, Color color, Matrix3x3 matrix)
        {
            AddLine(objects, "FINISHED segment", start, end, color, matrix);
            AddLine(objects, "FINISHED segment bold", new Point2D(start.X, start.Y - 0.6), new Point2D(end.X, end.Y - 0.6), color, matrix);
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
            TreeNode root = new TreeNode(_activeScene == AnimationScene.Scene2 ? "Scene 2" : "Scene 1");

            if (_activeScene == AnimationScene.Scene2)
            {
                root.Nodes.Add("Night city: glowing windows + moon scale");
                root.Nodes.Add("Road: animated lane dashes + speed streaks");
                root.Nodes.Add("Red/blue racers: translate + dodge lane changes");
                root.Nodes.Add("Wheels: rotation around wheel centers");
                root.Nodes.Add("Obstacles: cone, barrier, falling crate");
                root.Nodes.Add("Crate: fast translate down + rotation + squash scale");
                root.Nodes.Add("Boost smoke: translate + scale");
                root.Nodes.Add("Finish: checker line + FINISHED line-font scale");
            }
            else
            {
                root.Nodes.Add("Bridge and river");
                root.Nodes.Add("Car: translate + wheel rotation");
                root.Nodes.Add("Sun: pulse scale + ray rotation");
                root.Nodes.Add("Missile: translate + flame scale");
                root.Nodes.Add("Explosion: scale + shard rotation");
                root.Nodes.Add("Plane: translate, rotate, scale, fall");
            }

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
