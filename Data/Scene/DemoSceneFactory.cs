using System;
using System.Collections.Generic;
using System.Drawing;
using Project_CG_Paint.Algorithms.Rasterization.Shape2D;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Data.Scene
{
    internal static class DemoSceneFactory
    {
        public const int Width = 1000;
        public const int Height = 600;

        public static List<ColoredPoint> CreateScene()
        {
            List<ColoredPoint> scene = new List<ColoredPoint>();

            AddSun(scene);
            AddCloud(scene, 200, 92, 1.0);
            AddCloud(scene, 680, 78, 0.85);
            AddAirplane(scene);
            AddGroundAndRiver(scene);
            AddBridge(scene);
            AddTruck(scene);
            AddCar(scene);
            AddExplosion(scene, new Point2D(835, 310));

            return scene;
        }

        private static void AddSun(List<ColoredPoint> scene)
        {
            Point2D center = new Point2D(865, 92);
            Color rayColor = Color.FromArgb(255, 204, 73);

            for (int i = 0; i < 16; i++)
            {
                double angle = i * Math.PI * 2.0 / 16.0;
                Point2D start = Polar(center, 46, angle);
                Point2D end = Polar(center, 72, angle);
                AddSolid(scene, BresenhamLine.RasterizePoints(start, end), rayColor);
            }

            scene.AddRange(MidpointCircle.RasterizeColoredPoints(center, 42, Color.FromArgb(255, 221, 87)));
            scene.AddRange(MidpointCircle.RasterizeColoredPoints(center, 28, Color.FromArgb(255, 236, 128)));
        }

        private static void AddCloud(List<ColoredPoint> scene, int x, int y, double scale)
        {
            var left = Shape2DFill.FillEllipse(new Point2D(x - 42 * scale, y + 8 * scale), 58 * scale, 24 * scale);
            var mid = Shape2DFill.FillEllipse(new Point2D(x + 8 * scale, y - 6 * scale), 72 * scale, 34 * scale);
            var right = Shape2DFill.FillEllipse(new Point2D(x + 70 * scale, y + 9 * scale), 60 * scale, 25 * scale);

            var body = Shape2DComposer.Union(left, mid, right);
            var contour = Shape2DComposer.BoundaryOfFilledArea(body);

            AddSolid(scene, body, Color.FromArgb(246, 250, 255));
            AddSolid(scene, contour, Color.FromArgb(210, 225, 238));
        }

        private static void AddAirplane(List<ColoredPoint> scene)
        {
            var body = new List<Point2D>
            {
                new Point2D(76, 143),
                new Point2D(226, 111),
                new Point2D(316, 132),
                new Point2D(226, 153),
                new Point2D(84, 152)
            };

            scene.AddRange(PolygonRasterizer.RasterizeColoredPoints(body, Color.FromArgb(236, 241, 245)));
            scene.AddRange(TriangleRasterizer.RasterizeColoredPoints(
                new Point2D(176, 143),
                new Point2D(250, 218),
                new Point2D(218, 143),
                Color.FromArgb(77, 142, 188)));
            scene.AddRange(TriangleRasterizer.RasterizeColoredPoints(
                new Point2D(105, 142),
                new Point2D(58, 94),
                new Point2D(134, 130),
                Color.FromArgb(77, 142, 188)));
            scene.AddRange(TriangleRasterizer.RasterizeColoredPoints(
                new Point2D(105, 151),
                new Point2D(55, 191),
                new Point2D(138, 154),
                Color.FromArgb(56, 112, 164)));

            for (int i = 0; i < 4; i++)
            {
                scene.AddRange(MidpointCircle.RasterizeColoredPoints(
                    new Point2D(178 + i * 28, 132 + i * 2),
                    7,
                    Color.FromArgb(72, 152, 210)));
            }
        }

        private static void AddGroundAndRiver(List<ColoredPoint> scene)
        {
            AddSolid(scene, Shape2DFill.FillRectangle(0, 374, Width - 1, Height - 1), Color.FromArgb(92, 154, 82));

            var river = new List<Point2D>
            {
                new Point2D(0, 426),
                new Point2D(1000, 392),
                new Point2D(1000, 600),
                new Point2D(0, 600)
            };
            scene.AddRange(PolygonRasterizer.RasterizeColoredPoints(river, Color.FromArgb(74, 151, 198)));

            for (int i = 0; i < 9; i++)
            {
                int y = 448 + i * 16;
                AddSolid(scene, BresenhamLine.RasterizePoints(new Point2D(42 + i * 55, y), new Point2D(185 + i * 58, y - 10)), Color.FromArgb(147, 205, 226));
            }
        }

        private static void AddBridge(List<ColoredPoint> scene)
        {
            var bridgeWall = Shape2DFill.FillRectangle(170, 334, 830, 414);
            var arch1 = Shape2DFill.FillEllipse(new Point2D(330, 414), 82, 58);
            var arch2 = Shape2DFill.FillEllipse(new Point2D(500, 414), 82, 58);
            var arch3 = Shape2DFill.FillEllipse(new Point2D(670, 414), 82, 58);
            var wallWithArches = Shape2DComposer.Subtract(bridgeWall, arch1, arch2, arch3);

            AddSolid(scene, wallWithArches, Color.FromArgb(172, 164, 153));
            AddSolid(scene, Shape2DFill.FillRectangle(145, 306, 855, 341), Color.FromArgb(106, 112, 118));
            AddSolid(scene, Shape2DFill.FillRectangle(145, 296, 855, 305), Color.FromArgb(196, 188, 177));

            for (int x = 190; x <= 810; x += 48)
                AddSolid(scene, Shape2DFill.FillRectangle(x, 270, x + 8, 296), Color.FromArgb(116, 123, 130));

            AddSolid(scene, BresenhamLine.RasterizePoints(new Point2D(160, 270), new Point2D(840, 270)), Color.FromArgb(116, 123, 130));
        }

        private static void AddTruck(List<ColoredPoint> scene)
        {
            AddSolid(scene, Shape2DFill.FillRectangle(554, 247, 722, 294), Color.FromArgb(211, 70, 57));

            var cab = new List<Point2D>
            {
                new Point2D(722, 262),
                new Point2D(776, 262),
                new Point2D(798, 294),
                new Point2D(722, 294)
            };
            scene.AddRange(PolygonRasterizer.RasterizeColoredPoints(cab, Color.FromArgb(226, 99, 72)));
            AddSolid(scene, Shape2DFill.FillRectangle(738, 267, 769, 285), Color.FromArgb(159, 211, 232));

            AddWheel(scene, 596, 298, 18);
            AddWheel(scene, 745, 298, 18);
        }

        private static void AddCar(List<ColoredPoint> scene)
        {
            var car = new List<Point2D>
            {
                new Point2D(270, 276),
                new Point2D(314, 246),
                new Point2D(390, 246),
                new Point2D(432, 276),
                new Point2D(445, 296),
                new Point2D(252, 296)
            };
            scene.AddRange(PolygonRasterizer.RasterizeColoredPoints(car, Color.FromArgb(47, 126, 202)));
            AddSolid(scene, Shape2DFill.FillRectangle(318, 253, 382, 271), Color.FromArgb(178, 225, 240));

            AddWheel(scene, 304, 299, 16);
            AddWheel(scene, 399, 299, 16);
        }

        private static void AddExplosion(List<ColoredPoint> scene, Point2D center)
        {
            Color[] colors =
            {
                Color.FromArgb(255, 214, 64),
                Color.FromArgb(255, 137, 38),
                Color.FromArgb(210, 50, 34)
            };

            for (int i = 0; i < 14; i++)
            {
                double a1 = -Math.PI / 2 + i * Math.PI * 2.0 / 14.0;
                double a2 = -Math.PI / 2 + (i + 1) * Math.PI * 2.0 / 14.0;
                Point2D p1 = Polar(center, i % 2 == 0 ? 82 : 58, a1);
                Point2D p2 = Polar(center, i % 2 == 0 ? 58 : 82, a2);
                scene.AddRange(TriangleRasterizer.RasterizeColoredPoints(center, p1, p2, colors[i % colors.Length]));
            }

            scene.AddRange(MidpointCircle.RasterizeColoredPoints(center, 34, Color.FromArgb(255, 232, 87)));
            scene.AddRange(DiamondRasterizer.RasterizeColoredPoints(center, 26, 18, Color.FromArgb(255, 159, 41)));
        }

        private static void AddWheel(List<ColoredPoint> scene, int x, int y, int radius)
        {
            scene.AddRange(MidpointCircle.RasterizeColoredPoints(new Point2D(x, y), radius, Color.FromArgb(39, 43, 48)));
            scene.AddRange(MidpointCircle.RasterizeColoredPoints(new Point2D(x, y), radius / 2, Color.FromArgb(179, 184, 190)));
        }

        private static Point2D Polar(Point2D center, double radius, double angle)
        {
            return new Point2D(
                center.X + Math.Cos(angle) * radius,
                center.Y + Math.Sin(angle) * radius);
        }

        private static void AddSolid(List<ColoredPoint> scene, IEnumerable<Point2D> points, Color color)
        {
            scene.AddRange(Shape2DComposer.ApplyColor(points, color));
        }
    }
}
