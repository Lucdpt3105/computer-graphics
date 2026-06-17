using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class TriangleRasterizer
    {
        /// <summary>
        /// Rasterize tam giac: tra ve cac diem tren 3 canh va ben trong tam giac.
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D vertex1, Point2D vertex2, Point2D vertex3)
        {
            HashSet<Point2D> points = new HashSet<Point2D>();

            foreach (var point in BresenhamLine.RasterizePoints(vertex1, vertex2))
                points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(vertex2, vertex3))
                points.Add(point);
            foreach (var point in BresenhamLine.RasterizePoints(vertex3, vertex1))
                points.Add(point);

            foreach (var point in Shape2DFill.FillTriangle(vertex1, vertex2, vertex3))
                points.Add(point);

            return new List<Point2D>(points);
        }
    }
}
