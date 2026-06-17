using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class ParallelogramRasterizer
    {
        /// <summary>
        /// Sinh cac diem cua hinh binh hanh tu 3 dinh A, B, C.
        /// Dinh thu 4 D duoc tinh: D = B + C - A.
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D vertexA, Point2D vertexB, Point2D vertexC)
        {
            return RasterizePoints(vertexA, vertexB, vertexC, Shape2DFill.SolidFill)
                .Select(coloredPoint => coloredPoint.Point)
                .ToList();
        }

        public static List<ColoredPoint> RasterizeColoredPoints(Point2D vertexA, Point2D vertexB, Point2D vertexC, Color color)
        {
            return RasterizePoints(vertexA, vertexB, vertexC, point => color);
        }

        public static List<ColoredPoint> RasterizePoints(Point2D vertexA, Point2D vertexB, Point2D vertexC, FillColorFunction fillColorFunction)
        {
            HashSet<Point2D> points = new HashSet<Point2D>();

            Point2D vertexD = new Point2D(
                vertexB.X + vertexC.X - vertexA.X,
                vertexB.Y + vertexC.Y - vertexA.Y);

            foreach (var point in BresenhamLine.RasterizePoints(vertexA, vertexB)) points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(vertexB, vertexC)) points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(vertexC, vertexD)) points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(vertexD, vertexA)) points.Add(point);

            foreach (var point in Shape2DFill.FillPolygon(new List<Point2D> { vertexA, vertexB, vertexC, vertexD }))
                points.Add(point);

            return Shape2DFill.ApplyColorFunction(points, fillColorFunction);
        }
    }
}
