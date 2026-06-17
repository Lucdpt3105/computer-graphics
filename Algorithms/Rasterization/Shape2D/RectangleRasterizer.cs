using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class RectangleRasterizer
    {
        /// <summary>
        /// Rasterize hình chữ nhật từ 2 góc top-left và bottom-right
        /// Trả về danh sách các điểm trên 4 cạnh và bên trong
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D topLeft, Point2D bottomRight)
        {
            return RasterizePoints(topLeft, bottomRight, Shape2DFill.SolidFill);
        }

        public static List<ColoredPoint> RasterizeColoredPoints(Point2D topLeft, Point2D bottomRight, Color color)
        {
            return RasterizePoints(topLeft, bottomRight, point => color);
        }

        public static List<ColoredPoint> RasterizePoints(Point2D topLeft, Point2D bottomRight, FillColorFunction fillColorFunction)
        {
            HashSet<Point2D> points = new HashSet<Point2D>();

            Point2D topRight = new Point2D(bottomRight.X, topLeft.Y);
            Point2D bottomLeft = new Point2D(topLeft.X, bottomRight.Y);

            // Vẽ 4 cạnh
            var top = BresenhamLine.RasterizePoints(topLeft, topRight);
            var bottom = BresenhamLine.RasterizePoints(bottomLeft, bottomRight);
            var left = BresenhamLine.RasterizePoints(topLeft, bottomLeft);
            var right = BresenhamLine.RasterizePoints(topRight, bottomRight);

            foreach (var p in top) points.Add(p);
            foreach (var p in bottom) points.Add(p);
            foreach (var p in left) points.Add(p);
            foreach (var p in right) points.Add(p);

            // Tô bên trong bằng thuật toán fill riêng
            int yMin = (int)Math.Round(topLeft.Y);
            int yMax = (int)Math.Round(bottomRight.Y);
            int xMin = (int)Math.Round(topLeft.X);
            int xMax = (int)Math.Round(bottomRight.X);

            foreach (var p in Shape2DFill.FillRectangle(xMin, yMin, xMax, yMax))
                points.Add(p);

            return Shape2DFill.ApplyColorFunction(points, fillColorFunction);
        }
    }
}
