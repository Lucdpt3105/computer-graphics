using System;
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
        public static ProjectionResult ProjectEdgesWithVisibility(List<Point3D> vertices, List<Edge<Point3D>> edges, List<Face> faces, double alpha)
        {
            double rad = alpha * Math.PI / 180.0;
            double vx = -Math.Cos(rad);
            double vy = -Math.Sin(rad);
            double vz = 1.0;
            var edgeVisible = new bool[edges.Count];
            foreach (var face in faces)
            {
                Point3D n = face.Normal;
                double dot = n.X * vx + n.Y * vy + n.Z * vz;
                if (dot > 0)
                    foreach (int idx in face.EdgeIndices)
                        edgeVisible[idx] = true;
            }
            Matrix4x4 cavalier = MatrixFactory.CreateCavalierProjection(alpha);
            var visible = new List<Edge<Point2D>>();
            var hidden = new List<Edge<Point2D>>();
            for (int i = 0; i < edges.Count; i++)
            {
                Point3D s = cavalier.Transform(edges[i].Start);
                Point3D t = cavalier.Transform(edges[i].End);
                var edge2D = new Edge<Point2D>(new Point2D(s.X, s.Y), new Point2D(t.X, t.Y));
                if (edgeVisible[i]) visible.Add(edge2D);
                else hidden.Add(edge2D);
            }
            return new ProjectionResult(visible, hidden);
        }
    }
}
