using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class MidpointCircle
    {
        /// <summary>
        /// Triển khai thuật toán Midpoint (Bresenham) để vẽ đường tròn
        /// Sinh các điểm trên đường tròn với tâm (center) và bán kính (radius)
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D center, double radius)
        {
            List<Point2D> points = new List<Point2D>();

            int cx = (int)Math.Round(center.X);
            int cy = (int)Math.Round(center.Y);
            int r = (int)Math.Round(radius);

            int x = 0;
            int y = r;
            int p = 1 - r; // Decision parameter

            // Add initial symmetric points
            AddCircleSymmetricPoints(points, cx, cy, x, y);

            while (x < y)
            {
                x++;
                if (p < 0)
                {
                    p += 2 * x + 1;
                }
                else
                {
                    y--;
                    p += 2 * (x - y) + 1;
                }
                AddCircleSymmetricPoints(points, cx, cy, x, y);
            }

            return points;
        }

        /// <summary>
        /// Thêm 8 điểm đối xứng của (x, y) quanh tâm (cx, cy)
        /// </summary>
        private static void AddCircleSymmetricPoints(List<Point2D> points, int cx, int cy, int x, int y)
        {
            points.Add(new Point2D(cx + x, cy + y));
            points.Add(new Point2D(cx - x, cy + y));
            points.Add(new Point2D(cx + x, cy - y));
            points.Add(new Point2D(cx - x, cy - y));
            points.Add(new Point2D(cx + y, cy + x));
            points.Add(new Point2D(cx - y, cy + x));
            points.Add(new Point2D(cx + y, cy - x));
            points.Add(new Point2D(cx - y, cy - x));
        }
    }
}
