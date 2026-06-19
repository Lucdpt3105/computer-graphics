using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class PolygonRasterizer
    {
        /// <summary>
        /// Rasterize da giac tu danh sach dinh theo thu tu.
        /// </summary>
        public static List<Point2D> RasterizePoints(List<Point2D> vertices)
        {
            if (vertices == null || vertices.Count < 3)
                return new List<Point2D>();

            HashSet<Point2D> points = new HashSet<Point2D>();

            for (int i = 0; i < vertices.Count; i++)
            {
                Point2D current = vertices[i];
                Point2D next = vertices[(i + 1) % vertices.Count];

                foreach (var point in BresenhamLine.RasterizePoints(current, next))
                    points.Add(point);
            }

            return new List<Point2D>(points);
        }
    }
}
