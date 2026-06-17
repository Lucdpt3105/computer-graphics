using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape3D
{
    public static class PyramidWireFrame
    {
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            Generate(double baseWidth, double baseDepth, double height)
        {
            double w = baseWidth / 2.0;
            double d = baseDepth / 2.0;
            double h = height / 2.0;

            var v = new List<Point3D>
            {
                new Point3D( 0,  h,  0), // 0 — đỉnh chóp
                new Point3D(-w, -h, -d), // 1
                new Point3D( w, -h, -d), // 2
                new Point3D( w, -h,  d), // 3
                new Point3D(-w, -h,  d), // 4
            };

            var e = new List<Edge<Point3D>>
            {
                // Đáy
                new Edge<Point3D>(v[1], v[2]),
                new Edge<Point3D>(v[2], v[3]),
                new Edge<Point3D>(v[3], v[4]),
                new Edge<Point3D>(v[4], v[1]),
                // Cạnh bên từ đỉnh xuống 4 góc đáy
                new Edge<Point3D>(v[0], v[1]),
                new Edge<Point3D>(v[0], v[2]),
                new Edge<Point3D>(v[0], v[3]),
                new Edge<Point3D>(v[0], v[4]),
            };

            return (v, e);
        }
    }
}
