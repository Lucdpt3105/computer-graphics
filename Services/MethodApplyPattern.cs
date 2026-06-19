using System.Collections.Generic;
using System.Linq;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Services
{
    public static class MethodApplyPattern
    {
        public static List<Point2D> ApplyPattern(List<Point2D> points, int[] pattern)
        {
            if (points == null)
                return new List<Point2D>();

            if (pattern == null || pattern.Length == 0 || pattern.Length == 1)
                return new List<Point2D>(points);

            int[] safePattern = pattern.Where(value => value > 0).ToArray();
            if (safePattern.Length <= 1)
                return new List<Point2D>(points);

            List<Point2D> result = new List<Point2D>();
            int patternIndex = 0;
            int count = 0;
            bool drawing = true;

            foreach (Point2D point in points)
            {
                if (drawing)
                    result.Add(point);

                count++;
                if (count >= safePattern[patternIndex])
                {
                    count = 0;
                    patternIndex = (patternIndex + 1) % safePattern.Length;
                    drawing = !drawing;
                }
            }

            return result;
        }
    }
}
