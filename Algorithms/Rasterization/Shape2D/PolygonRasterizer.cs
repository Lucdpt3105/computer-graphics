using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class PolygonRasterizer
    {
        /// <summary>
        /// Rasterize đa giác từ danh sách các đỉnh (theo thứ tự)
        /// Trả về các điểm trên cạnh và bên trong đa giác
        /// Sử dụng thuật toán Scanline Fill cho đa giác
        /// </summary>
        public static List<Point2D> RasterizePoints(List<Point2D> vertices)
        {
            return RasterizePoints(vertices, Shape2DFill.SolidFill);
        }

        public static List<ColoredPoint> RasterizeColoredPoints(List<Point2D> vertices, Color color)
        {
            return RasterizePoints(vertices, point => color);
        }

        public static List<ColoredPoint> RasterizePoints(List<Point2D> vertices, FillColorFunction fillColorFunction)
        {
            if (vertices == null || vertices.Count < 3)
                return new List<ColoredPoint>();

            HashSet<Point2D> points = new HashSet<Point2D>();

            // 1. Vẽ các cạnh đa giác
            for (int i = 0; i < vertices.Count; i++)
            {
                Point2D current = vertices[i];
                Point2D next = vertices[(i + 1) % vertices.Count];
                foreach (var p in BresenhamLine.RasterizePoints(current, next))
                    points.Add(p);
            }

            // 2. Tô bên trong bằng thuật toán fill riêng
            foreach (var p in Shape2DFill.FillPolygon(vertices))
                points.Add(p);

            return Shape2DFill.ApplyColorFunction(points, fillColorFunction);
        }
    }
}
