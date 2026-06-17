using System.Collections.Generic;
using System.Linq;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Projection
{
    public static class CavalierProjection
    {
        public static Point2D ProjectPoint(Point3D point, double alpha)
        {
            Matrix4x4 cavalier = MatrixFactory.CreateCavalierProjection(alpha);
            Point3D projected = cavalier.Transform(point);
            return new Point2D(projected.X, projected.Y);
        }

        public static List<Point2D> ProjectPoints(IEnumerable<Point3D> points, double alpha)
        {
            Matrix4x4 cavalier = MatrixFactory.CreateCavalierProjection(alpha);
            return points.Select(p =>
            {
                Point3D projected = cavalier.Transform(p);
                return new Point2D(projected.X, projected.Y);
            }).ToList();
        }

        public static List<Edge<Point2D>> ProjectEdges(IEnumerable<Edge<Point3D>> edges, double alpha)
        {
            Matrix4x4 cavalier = MatrixFactory.CreateCavalierProjection(alpha);
            return edges.Select(edge =>
            {
                Point3D s = cavalier.Transform(edge.Start);
                Point3D e = cavalier.Transform(edge.End);
                return new Edge<Point2D>(new Point2D(s.X, s.Y), new Point2D(e.X, e.Y));
            }).ToList();
        }
    }
}
