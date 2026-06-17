using System.Collections.Generic;
using System.Linq;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Projection
{
    public static class CabinetProjection
    {
        public static Point2D ProjectPoint(Point3D point, double alpha)
        {
            Matrix4x4 cabinet = MatrixFactory.CreateCabinetProjection(alpha);
            Point3D projected = cabinet.Transform(point);
            return new Point2D(projected.X, projected.Y);
        }

        public static List<Point2D> ProjectPoints(IEnumerable<Point3D> points, double alpha)
        {
            Matrix4x4 cabinet = MatrixFactory.CreateCabinetProjection(alpha); 
            return points.Select(p =>
            {
                Point3D projected = cabinet.Transform(p);
                return new Point2D(projected.X, projected.Y);
            }).ToList();
        }

        public static List<Edge<Point2D>> ProjectEdges(IEnumerable<Edge<Point3D>> edges, double alpha)
        {
            Matrix4x4 cabinet = MatrixFactory.CreateCabinetProjection(alpha); 
            return edges.Select(edge =>
            {
                Point3D s = cabinet.Transform(edge.Start);
                Point3D e = cabinet.Transform(edge.End);
                return new Edge<Point2D>(new Point2D(s.X, s.Y), new Point2D(e.X, e.Y));
            }).ToList();
        }
    }
}
