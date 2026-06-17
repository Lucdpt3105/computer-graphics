using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class RectangleRasterizer
    {
        /// <summary>
        /// Rasterize hinh chu nhat tu 2 goc doi dien.
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D topLeft, Point2D bottomRight)
        {
            HashSet<Point2D> points = new HashSet<Point2D>();

            Point2D topRight = new Point2D(bottomRight.X, topLeft.Y);
            Point2D bottomLeft = new Point2D(topLeft.X, bottomRight.Y);

            foreach (var point in BresenhamLine.RasterizePoints(topLeft, topRight))
                points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(bottomLeft, bottomRight))
                points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(topLeft, bottomLeft))
                points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(topRight, bottomRight))
                points.Add(point);

            int yMin = (int)Math.Round(topLeft.Y);
            int yMax = (int)Math.Round(bottomRight.Y);
            int xMin = (int)Math.Round(topLeft.X);
            int xMax = (int)Math.Round(bottomRight.X);

            foreach (var point in Shape2DFill.FillRectangle(xMin, yMin, xMax, yMax))
                points.Add(point);

            return new List<Point2D>(points);
        }
    }
}
