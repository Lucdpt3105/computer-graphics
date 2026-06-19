using System.Collections.Generic;
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
            HashSet<Point2D> points = new HashSet<Point2D>();

            Point2D vertexD = new Point2D(
                vertexB.X + vertexC.X - vertexA.X,
                vertexB.Y + vertexC.Y - vertexA.Y);

            foreach (var point in BresenhamLine.RasterizePoints(vertexA, vertexB))
                points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(vertexB, vertexC))
                points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(vertexC, vertexD))
                points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(vertexD, vertexA))
                points.Add(point);

            return new List<Point2D>(points);
        }
    }
}
