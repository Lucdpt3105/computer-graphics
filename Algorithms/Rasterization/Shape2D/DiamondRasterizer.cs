using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class DiamondRasterizer
    {
        /// <summary>
        /// Rasterize hinh thoi tu tam va 2 ban kinh theo truc X/Y.
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D center, double radiusX, double radiusY)
        {
            return RasterizePoints(center, radiusX, radiusY, Shape2DFill.SolidFill)
                .Select(coloredPoint => coloredPoint.Point)
                .ToList();
        }

        public static List<ColoredPoint> RasterizeColoredPoints(Point2D center, double radiusX, double radiusY, Color color)
        {
            return RasterizePoints(center, radiusX, radiusY, point => color);
        }

        public static List<ColoredPoint> RasterizePoints(Point2D center, double radiusX, double radiusY, FillColorFunction fillColorFunction)
        {
            int cx = (int)Math.Round(center.X);
            int cy = (int)Math.Round(center.Y);
            int rx = (int)Math.Round(radiusX);
            int ry = (int)Math.Round(radiusY);

            Point2D top = new Point2D(cx, cy - ry);
            Point2D right = new Point2D(cx + rx, cy);
            Point2D bottom = new Point2D(cx, cy + ry);
            Point2D left = new Point2D(cx - rx, cy);

            HashSet<Point2D> points = new HashSet<Point2D>();

            foreach (var point in BresenhamLine.RasterizePoints(top, right)) points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(right, bottom)) points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(bottom, left)) points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(left, top)) points.Add(point);

            foreach (var point in Shape2DFill.FillPolygon(new List<Point2D> { top, right, bottom, left }))
                points.Add(point);

            return Shape2DFill.ApplyColorFunction(points, fillColorFunction);
        }
    }
}
