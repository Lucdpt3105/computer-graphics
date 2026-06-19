using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class MidpointCircle
    {
        /// <summary>
        /// Trien khai thuat toan Midpoint de sinh cac diem cua duong tron va mien trong.
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D center, double radius)
        {
            HashSet<Point2D> points = new HashSet<Point2D>();

            int cx = (int)Math.Round(center.X);
            int cy = (int)Math.Round(center.Y);
            int r = (int)Math.Round(radius);

            if (r < 0)
                return new List<Point2D>();

            int x = 0;
            int y = r;
            int decision = 1 - r;

            AddCircleSymmetricPoints(points, cx, cy, x, y);

            while (x < y)
            {
                x++;
                if (decision < 0)
                {
                    decision += 2 * x + 1;
                }
                else
                {
                    y--;
                    decision += 2 * (x - y) + 1;
                }

                AddCircleSymmetricPoints(points, cx, cy, x, y);
            }

            return new List<Point2D>(points);
        }

        private static void AddCircleSymmetricPoints(HashSet<Point2D> points, int cx, int cy, int x, int y)
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
