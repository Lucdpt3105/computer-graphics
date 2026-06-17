using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class TriangleRasterizer
    {
        /// <summary>
        /// Rasterize tam giác: trả về danh sách các điểm trên 3 cạnh và bên trong tam giác
        /// Sử dụng kết hợp BresenhamLine để vẽ cạnh và thuật toán Scanline để tô màu
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D vertex1, Point2D vertex2, Point2D vertex3)
        {
            return RasterizePoints(vertex1, vertex2, vertex3, Shape2DFill.SolidFill)
                .Select(coloredPoint => coloredPoint.Point)
                .ToList();
        }

        public static List<ColoredPoint> RasterizeColoredPoints(Point2D vertex1, Point2D vertex2, Point2D vertex3, Color color)
        {
            return RasterizePoints(vertex1, vertex2, vertex3, point => color);
        }

        public static List<ColoredPoint> RasterizePoints(Point2D vertex1, Point2D vertex2, Point2D vertex3, FillColorFunction fillColorFunction)
        {
            HashSet<Point2D> points = new HashSet<Point2D>();

            // 1. Vẽ 3 cạnh tam giác bằng Bresenham
            var edge1 = BresenhamLine.RasterizePoints(vertex1, vertex2);
            var edge2 = BresenhamLine.RasterizePoints(vertex2, vertex3);
            var edge3 = BresenhamLine.RasterizePoints(vertex3, vertex1);

            foreach (var p in edge1) points.Add(p);
            foreach (var p in edge2) points.Add(p);
            foreach (var p in edge3) points.Add(p);

            // 2. Tô bên trong bằng thuật toán fill riêng
            foreach (var p in Shape2DFill.FillTriangle(vertex1, vertex2, vertex3)) points.Add(p);

            return Shape2DFill.ApplyColorFunction(points, fillColorFunction);
        }
    }
}
