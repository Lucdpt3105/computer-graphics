using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class PolygonRasterizer
    {
        /// <summary>
        /// Rasterize đa giác từ danh sách các đỉnh (theo thứ tự)
        /// Trả về các điểm trên cạnh và bên trong đa giác
        /// Sử dụng thuật toán Scanline Fill cho đa giác
        /// </summary>
        public static List<Point2D> RasterizePoints(List<Point2D> vertices)
        {
            if (vertices == null || vertices.Count < 3)
                return new List<Point2D>();

            HashSet<Point2D> points = new HashSet<Point2D>();

            // 1. Vẽ các cạnh đa giác
            for (int i = 0; i < vertices.Count; i++)
            {
                Point2D current = vertices[i];
                Point2D next = vertices[(i + 1) % vertices.Count];
                foreach (var p in BresenhamLine.RasterizePoints(current, next))
                    points.Add(p);
            }

            // 2. Tô bên trong bằng Scanline
            var interior = ScanlineFillPolygon(vertices);
            foreach (var p in interior)
                points.Add(p);

            return points.ToList();
        }

        /// <summary>
        /// Thuật toán Scanline Fill cho đa giác
        /// </summary>
        private static List<Point2D> ScanlineFillPolygon(List<Point2D> vertices)
        {
            List<Point2D> filledPoints = new List<Point2D>();

            // Tìm y_min và y_max
            double yMin = vertices.Min(v => v.Y);
            double yMax = vertices.Max(v => v.Y);

            int yStart = (int)Math.Round(yMin);
            int yEnd = (int)Math.Round(yMax);

            for (int y = yStart; y <= yEnd; y++)
            {
                List<double> intersections = new List<double>();

                // Tìm giao điểm của scanline y với tất cả các cạnh
                for (int i = 0; i < vertices.Count; i++)
                {
                    Point2D p1 = vertices[i];
                    Point2D p2 = vertices[(i + 1) % vertices.Count];

                    double y1 = p1.Y;
                    double y2 = p2.Y;

                    // Bỏ qua cạnh nằm ngang
                    if (Math.Abs(y1 - y2) < 1e-10)
                        continue;

                    // Kiểm tra y nằm trong khoảng [y1, y2]
                    if (y >= Math.Min(y1, y2) && y <= Math.Max(y1, y2))
                    {
                        // Chỉ lấy giao điểm ở đỉnh trên một lần (tránh trùng)
                        if (y == Math.Max(y1, y2))
                            continue;

                        double x = p1.X + (y - y1) * (p2.X - p1.X) / (y2 - y1);
                        intersections.Add(x);
                    }
                }

                // Sắp xếp và vẽ từng cặp giao điểm
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
    }
}
