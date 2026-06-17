using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    /// <summary>
    /// Đại diện cho một điểm raster và màu được tô tại điểm đó.
    /// </summary>
    public sealed class ColoredPoint
    {
        public Point2D Point { get; }
        public Color Color { get; }

        public ColoredPoint(Point2D point, Color color)
        {
            Point = point;
            Color = color;
        }
    }

    /// <summary>
    /// Hàm nhận điểm raster và trả về màu cần vẽ tại điểm đó.
    /// Dùng để thay màu hard-code bằng màu động/gradient/texture sau này.
    /// </summary>
    public delegate Color FillColorFunction(Point2D point);

    /// <summary>
    /// Tập hợp các thuật toán fill màu riêng, độc lập với các shape.
    /// </summary>
    public static class Shape2DFill
    {
        public static readonly FillColorFunction SolidFill = point => Color.Black;

        public static List<ColoredPoint> ApplySolidColor(IEnumerable<Point2D> points, Color color)
        {
            if (points == null)
                return new List<ColoredPoint>();

            return points.Select(point => new ColoredPoint(point, color)).ToList();
        }

        public static List<ColoredPoint> ApplyColorFunction(IEnumerable<Point2D> points, FillColorFunction fillColorFunction)
        {
            if (points == null)
                return new List<ColoredPoint>();

            if (fillColorFunction == null)
                throw new ArgumentNullException(nameof(fillColorFunction));

            return points.Select(point => new ColoredPoint(point, fillColorFunction(point))).ToList();
        }

        public static List<Point2D> FillRectangle(int xMin, int yMin, int xMax, int yMax)
        {
            List<Point2D> filledPoints = new List<Point2D>();

            if (xMin > xMax)
            {
                int tempX = xMin;
                xMin = xMax;
                xMax = tempX;
            }

            if (yMin > yMax)
            {
                int tempY = yMin;
                yMin = yMax;
                yMax = tempY;
            }

            for (int y = yMin; y <= yMax; y++)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    filledPoints.Add(new Point2D(x, y));
                }
            }

            return filledPoints;
        }

        public static List<Point2D> FillTriangle(Point2D vertex1, Point2D vertex2, Point2D vertex3)
        {
            List<Point2D> filledPoints = new List<Point2D>();

            var vertices = new List<Point2D> { vertex1, vertex2, vertex3 };
            vertices.Sort((a, b) => a.Y.CompareTo(b.Y));

            Point2D top = vertices[0];
            Point2D mid = vertices[1];
            Point2D bot = vertices[2];

            int yMin = (int)Math.Round(top.Y);
            int yMax = (int)Math.Round(bot.Y);

            for (int y = yMin; y <= yMax; y++)
            {
                List<double> xIntersections = new List<double>();

                AddIntersection(xIntersections, top, mid, y);
                AddIntersection(xIntersections, mid, bot, y);
                AddIntersection(xIntersections, top, bot, y);

                if (xIntersections.Count >= 2)
                {
                    xIntersections.Sort();
                    int xStart = (int)Math.Round(xIntersections[0]);
                    int xEnd = (int)Math.Round(xIntersections[xIntersections.Count - 1]);

                    for (int x = xStart; x <= xEnd; x++)
                    {
                        filledPoints.Add(new Point2D(x, y));
                    }
                }
            }

            return filledPoints;
        }

        public static List<Point2D> FillPolygon(List<Point2D> vertices)
        {
            if (vertices == null || vertices.Count < 3)
                return new List<Point2D>();

            List<Point2D> filledPoints = new List<Point2D>();

            double yMin = vertices.Min(v => v.Y);
            double yMax = vertices.Max(v => v.Y);

            int yStart = (int)Math.Round(yMin);
            int yEnd = (int)Math.Round(yMax);

            for (int y = yStart; y <= yEnd; y++)
            {
                List<double> intersections = new List<double>();

                for (int i = 0; i < vertices.Count; i++)
                {
                    Point2D p1 = vertices[i];
                    Point2D p2 = vertices[(i + 1) % vertices.Count];

                    double y1 = p1.Y;
                    double y2 = p2.Y;

                    if (Math.Abs(y1 - y2) < 1e-10)
                        continue;

                    if (y >= Math.Min(y1, y2) && y <= Math.Max(y1, y2))
                    {
                        if (y == Math.Max(y1, y2))
                            continue;

                        double x = p1.X + (y - y1) * (p2.X - p1.X) / (y2 - y1);
                        intersections.Add(x);
                    }
                }

                intersections.Sort();
                for (int i = 0; i + 1 < intersections.Count; i += 2)
                {
                    int xStart = (int)Math.Round(intersections[i]);
                    int xEnd = (int)Math.Round(intersections[i + 1]);

                    for (int x = xStart; x <= xEnd; x++)
                    {
                        filledPoints.Add(new Point2D(x, y));
                    }
                }
            }

            return filledPoints;
        }

        public static List<Point2D> FillCircle(Point2D center, double radius)
        {
            int cx = (int)Math.Round(center.X);
            int cy = (int)Math.Round(center.Y);
            int r = (int)Math.Round(radius);

            if (r < 0)
                return new List<Point2D>();

            return FillRectangle(cx - r, cy - r, cx + r, cy + r)
                .Where(point => IsInsideCircle(point, cx, cy, r))
                .ToList();
        }

        public static List<Point2D> FillEllipse(Point2D center, double radiusX, double radiusY)
        {
            int cx = (int)Math.Round(center.X);
            int cy = (int)Math.Round(center.Y);
            int rx = (int)Math.Round(radiusX);
            int ry = (int)Math.Round(radiusY);

            if (rx < 0 || ry < 0)
                return new List<Point2D>();

            return FillRectangle(cx - rx, cy - ry, cx + rx, cy + ry)
                .Where(point => IsInsideEllipse(point, cx, cy, rx, ry))
                .ToList();
        }

        private static void AddIntersection(List<double> intersections, Point2D p1, Point2D p2, double y)
        {
            double y1 = p1.Y;
            double y2 = p2.Y;

            if (Math.Abs(y1 - y2) < 1e-10)
                return;

            if ((y >= Math.Min(y1, y2) && y <= Math.Max(y1, y2)))
            {
                double x = p1.X + (y - y1) * (p2.X - p1.X) / (y2 - y1);
                intersections.Add(x);
            }
        }

        private static bool IsInsideCircle(Point2D point, int cx, int cy, int radius)
        {
            int dx = (int)Math.Round(point.X) - cx;
            int dy = (int)Math.Round(point.Y) - cy;
            return dx * dx + dy * dy <= radius * radius;
        }

        private static bool IsInsideEllipse(Point2D point, int cx, int cy, int radiusX, int radiusY)
        {
            int dx = (int)Math.Round(point.X) - cx;
            int dy = (int)Math.Round(point.Y) - cy;
            return dx * dx * radiusY * radiusY + dy * dy * radiusX * radiusX <= radiusX * radiusX * radiusY * radiusY;
        }
    }
}
