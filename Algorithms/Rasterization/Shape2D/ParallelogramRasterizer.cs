using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class ParallelogramRasterizer
    {
        /// <summary>
        /// Sinh các điểm của hình bình hành từ 3 đỉnh A, B, C
        /// Đỉnh thứ 4 D được tính: D = B + C - A
        /// Sơ đồ: A---B
        ///        |   |
        ///        D---C
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D vertexA, Point2D vertexB, Point2D vertexC)
        {
            HashSet<Point2D> points = new HashSet<Point2D>();

            // Tính đỉnh thứ 4
            Point2D vertexD = new Point2D(
                vertexB.X + vertexC.X - vertexA.X,
                vertexB.Y + vertexC.Y - vertexA.Y
            );

            // Vẽ 4 cạnh
            foreach (var p in BresenhamLine.RasterizePoints(vertexA, vertexB)) points.Add(p);
            foreach (var p in BresenhamLine.RasterizePoints(vertexB, vertexC)) points.Add(p);
            foreach (var p in BresenhamLine.RasterizePoints(vertexC, vertexD)) points.Add(p);
            foreach (var p in BresenhamLine.RasterizePoints(vertexD, vertexA)) points.Add(p);

            // Tô bên trong = 2 tam giác ABC + ACD
            foreach (var p in TriangleRasterizer.RasterizePoints(vertexA, vertexB, vertexC)) points.Add(p);
            foreach (var p in TriangleRasterizer.RasterizePoints(vertexA, vertexC, vertexD)) points.Add(p);

            return points.ToList();
        }
    }
}
