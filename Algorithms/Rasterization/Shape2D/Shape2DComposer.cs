using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class Shape2DComposer
    {
        public static List<Point2D> Union(params IEnumerable<Point2D>[] shapes)
        {
            HashSet<Point2D> points = new HashSet<Point2D>();

            if (shapes == null)
                return points.ToList();

            foreach (var shape in shapes)
            {
                if (shape == null)
                    continue;

                foreach (var point in shape)
                    points.Add(ToRasterPoint(point));
            }

            return points.ToList();
        }

        public static List<Point2D> Subtract(IEnumerable<Point2D> source, params IEnumerable<Point2D>[] cutters)
        {
            HashSet<Point2D> points = ToPointSet(source);

            if (cutters == null)
                return points.ToList();

            foreach (var cutter in cutters)
            {
                foreach (var point in ToPointSet(cutter))
                    points.Remove(point);
            }

            return points.ToList();
        }

        public static List<Point2D> Intersect(params IEnumerable<Point2D>[] shapes)
        {
            if (shapes == null || shapes.Length == 0 || shapes[0] == null)
                return new List<Point2D>();

            HashSet<Point2D> result = ToPointSet(shapes[0]);

            for (int i = 1; i < shapes.Length; i++)
                result.IntersectWith(ToPointSet(shapes[i]));

            return result.ToList();
        }

        public static List<Point2D> BoundaryOfFilledArea(IEnumerable<Point2D> filledArea)
        {
            HashSet<Point2D> points = ToPointSet(filledArea);
            HashSet<Point2D> boundary = new HashSet<Point2D>();

            foreach (var point in points)
            {
                int x = (int)point.X;
                int y = (int)point.Y;

                if (!points.Contains(new Point2D(x + 1, y)) ||
                    !points.Contains(new Point2D(x - 1, y)) ||
                    !points.Contains(new Point2D(x, y + 1)) ||
                    !points.Contains(new Point2D(x, y - 1)))
                {
                    boundary.Add(point);
                }
            }

            return boundary.ToList();
        }

        public static List<ColoredPoint> ApplyColor(IEnumerable<Point2D> points, Color color)
        {
            return Shape2DFill.ApplySolidColor(points, color);
        }

        private static HashSet<Point2D> ToPointSet(IEnumerable<Point2D> points)
        {
            HashSet<Point2D> set = new HashSet<Point2D>();

            if (points == null)
                return set;

            foreach (var point in points)
                set.Add(ToRasterPoint(point));

            return set;
        }

        private static Point2D ToRasterPoint(Point2D point)
        {
            return new Point2D((int)System.Math.Round(point.X), (int)System.Math.Round(point.Y));
        }
    }
}
