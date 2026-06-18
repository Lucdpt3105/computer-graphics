using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.CoreModel.Geometry
{
    public class Face
    {
        public List<int> VertexIndices { get; }
        public List<int> EdgeIndices { get; }
        public Point3D Normal { get; }

        public Face(List<int> vertexIndices, List<int> edgeIndices, List<Point3D> vertices)
        {
            VertexIndices = vertexIndices;
            EdgeIndices = edgeIndices;
            Normal = ComputeNormal(vertices);
        }


        private Point3D ComputeNormal(List<Point3D> vertices)
        {
            var v0 = vertices[VertexIndices[0]];
            var v1 = vertices[VertexIndices[1]];
            var v2 = vertices[VertexIndices[2]];

            double ax = v1.X - v0.X, ay = v1.Y - v0.Y, az = v1.Z - v0.Z;
            double bx = v2.X - v0.X, by = v2.Y - v0.Y, bz = v2.Z - v0.Z;

            return new Point3D(
                ay * bz - az * by,
                az * bx - ax * bz,
                ax * by - ay * bx
            );
        }
    }
}
