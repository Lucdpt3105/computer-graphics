using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class TriangleRasterizer
    {
        /// <summary>
        /// Rasterize tam giác: trả về danh sách các điểm trên 3 cạnh và bên trong tam giác
        /// Sử dụng kết hợp BresenhamLine để vẽ cạnh và thuật toán Scanline để tô màu
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D vertex1, Point2D vertex2, Point2D vertex3)
        {
            HashSet<Point2D> points = new HashSet<Point2D>();

            // 1. Vẽ 3 cạnh tam giác bằng Bresenham
            var edge1 = BresenhamLine.RasterizePoints(vertex1, vertex2);
            var edge2 = BresenhamLine.RasterizePoints(vertex2, vertex3);
            var edge3 = BresenhamLine.RasterizePoints(vertex3, vertex1);

            foreach (var p in edge1) points.Add(p);
            foreach (var p in edge2) points.Add(p);
            foreach (var p in edge3) points.Add(p);

            // 2. Tô màu bên trong tam giác bằng thuật toán Scanline
            var filledPoints = FillTriangle(vertex1, vertex2, vertex3);
            foreach (var p in filledPoints) points.Add(p);

            return points.ToList();
        }

        /// <summary>
        /// Tô màu bên trong tam giác bằng thuật toán Scanline
        /// </summary>
        private static List<Point2D> FillTriangle(Point2D v1, Point2D v2, Point2D v3)
        {
            List<Point2D> filledPoints = new List<Point2D>();

            // Sắp xếp các đỉnh theo y tăng dần
            var vertices = new List<Point2D> { v1, v2, v3 };
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

        /// <summary>
        /// Tính giao điểm x của đường thẳng ngang y với đoạn thẳng (p1, p2)
        /// </summary>
        private static void AddIntersection(List<double> intersections, Point2D p1, Point2D p2, double y)
        {
            double y1 = p1.Y;
            double y2 = p2.Y;

            // Không xét các cạnh nằm ngang (horizontal)
            if (Math.Abs(y1 - y2) < 1e-10)
                return;

            // Kiểm tra y nằm trong đoạn [y1, y2]
            if ((y >= Math.Min(y1, y2) && y <= Math.Max(y1, y2)))
            {
                double x = p1.X + (y - y1) * (p2.X - p1.X) / (y2 - y1);
                intersections.Add(x);
            }
        }
    }
}
