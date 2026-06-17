using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape3D
{
    public static class CubeWireFrame
    {
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            Generate(double size)
        {
            double s = size / 2.0;
            var v = new List<Point3D>
            {
                new Point3D(-s, -s, -s), // 0
                new Point3D( s, -s, -s), // 1
                new Point3D( s,  s, -s), // 2
                new Point3D(-s,  s, -s), // 3
                new Point3D(-s, -s,  s), // 4
                new Point3D( s, -s,  s), // 5
                new Point3D( s,  s,  s), // 6
                new Point3D(-s,  s,  s), // 7
            };

            var e = new List<Edge<Point3D>>
            {
                // Mặt sau (z âm)
                new Edge<Point3D>(v[0], v[1]),
                new Edge<Point3D>(v[1], v[2]),
                new Edge<Point3D>(v[2], v[3]),
                new Edge<Point3D>(v[3], v[0]),
                // Mặt trước (z dương)
                new Edge<Point3D>(v[4], v[5]),
                new Edge<Point3D>(v[5], v[6]),
                new Edge<Point3D>(v[6], v[7]),
                new Edge<Point3D>(v[7], v[4]),
                // Cạnh nối 2 mặt
                new Edge<Point3D>(v[0], v[4]),
                new Edge<Point3D>(v[1], v[5]),
                new Edge<Point3D>(v[2], v[6]),
                new Edge<Point3D>(v[3], v[7]),
            };

            return (v, e);
        }
    }
}
