using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class MidpointEllipse
    {
        /// <summary>
        /// Triển khai thuật toán Midpoint để vẽ ellipse
        /// Sinh các điểm trên ellipse với tâm (center) và bán kính (radiusX, radiusY)
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D center, double radiusX, double radiusY)
        {
            List<Point2D> points = new List<Point2D>();

            int cx = (int)Math.Round(center.X);
            int cy = (int)Math.Round(center.Y);
            int rx = (int)Math.Round(radiusX);
            int ry = (int)Math.Round(radiusY);

            int x = 0;
            int y = ry;

            // Region 1: Decision parameter p1
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            int p1 = ry2 - (rx2 * ry) + (rx2 / 4);

            while ( (2 * x * ry2) < (2 * y * rx2) )
            {
                AddEllipseSymmetricPoints(points, cx, cy, x, y);

                x++;
                if (p1 < 0)
                {
                    p1 += (2 * ry2 * x) + ry2;
                }
                else
                {
                    y--;
                    p1 += (2 * ry2 * x) - (2 * rx2 * y) + ry2;
                }
            }

            // Region 2: Decision parameter p2
            int p2 = (int)(ry2 * (x + 0.5) * (x + 0.5) + rx2 * (y - 1) * (y - 1) - rx2 * ry2);

            while (y >= 0)
            {
                AddEllipseSymmetricPoints(points, cx, cy, x, y);

                y--;
                if (p2 > 0)
                {
                    p2 += rx2 - (2 * rx2 * y);
                }
                else
                {
                    x++;
                    p2 += (2 * ry2 * x) - (2 * rx2 * y) + rx2;
                }
            }

            return points;
        }

        /// <summary>
        /// Thêm 4 điểm đối xứng của (x, y) quanh tâm (cx, cy) của ellipse
        /// </summary>
        private static void AddEllipseSymmetricPoints(List<Point2D> points, int cx, int cy, int x, int y)
        {
            points.Add(new Point2D(cx + x, cy + y));
            points.Add(new Point2D(cx - x, cy + y));
            points.Add(new Point2D(cx + x, cy - y));
            points.Add(new Point2D(cx - x, cy - y));
        }
    }
}
