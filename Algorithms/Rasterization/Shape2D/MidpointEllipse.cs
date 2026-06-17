using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class MidpointEllipse
    {
        /// <summary>
        /// Trien khai thuat toan Midpoint de sinh cac diem cua ellipse va mien trong.
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D center, double radiusX, double radiusY)
        {
            HashSet<Point2D> points = new HashSet<Point2D>();

            int cx = (int)Math.Round(center.X);
            int cy = (int)Math.Round(center.Y);
            int rx = (int)Math.Round(radiusX);
            int ry = (int)Math.Round(radiusY);

            if (rx < 0 || ry < 0)
                return new List<Point2D>();

            int x = 0;
            int y = ry;
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            int p1 = ry2 - (rx2 * ry) + (rx2 / 4);

            while ((2 * x * ry2) < (2 * y * rx2))
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

            foreach (var point in Shape2DFill.FillEllipse(center, radiusX, radiusY))
                points.Add(point);

            return new List<Point2D>(points);
        }

        private static void AddEllipseSymmetricPoints(HashSet<Point2D> points, int cx, int cy, int x, int y)
        {
            points.Add(new Point2D(cx + x, cy + y));
            points.Add(new Point2D(cx - x, cy + y));
            points.Add(new Point2D(cx + x, cy - y));
            points.Add(new Point2D(cx - x, cy - y));
        }
    }
}
