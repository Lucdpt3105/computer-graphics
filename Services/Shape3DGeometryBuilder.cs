using Project_CG_Paint.Algorithms.Rasterization.Shape3D;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.GeometryInspection;
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Data.Shapes3D;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_CG_Paint.Services
{
    internal static class Shape3DGeometryBuilder
    {
        private const double Epsilon = 1e-8;

        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges, List<Face> Faces) BuildModel(Shape3D shape)
        {
            if (shape is CubeShape cube)
                return Offset(CubeWireFrame.Generate(cube.Size), cube.Position);
            if (shape is SphereShape sphere)
                return Offset(SphereWireFrame.Generate(sphere.Radius, sphere.Stacks, sphere.Slices), sphere.Center);
            if (shape is CylinderShape cylinder)
                return Offset(CylinderWireFrame.Generate(cylinder.Radius, cylinder.Height, cylinder.Segments), cylinder.Center);
            if (shape is PrismShape prism)
                return Offset(PrismWireFrame.Generate(prism.Radius, prism.Sides, prism.Height), prism.Center);
            if (shape is PyramidShape pyramid)
                return Offset(PyramidWireFrame.Generate(pyramid.BaseWidth, pyramid.BaseDepth, pyramid.Height), pyramid.Center);

            return (new List<Point3D>(), new List<Edge<Point3D>>(), new List<Face>());
        }

        public static void PopulateInspectionGeometry(Shape3D shape)
        {
            var model = BuildModel(shape);
            shape.InspectionGeometry.Clear();
            shape.InspectionGeometry.OriginalVertices.AddRange(model.Vertices);

            foreach (Edge<Point3D> edge in model.Edges)
            {
                int start = FindVertexIndex(model.Vertices, edge.Start);
                int end = FindVertexIndex(model.Vertices, edge.End);

                if (start >= 0 && end >= 0)
                    shape.InspectionGeometry.Edges.Add(new EdgeIndex(start, end));
            }

            AddControlPoints(shape);
            shape.InspectionGeometry.ResetCurrentToOriginal();
        }

        private static void AddControlPoints(Shape3D shape)
        {
            shape.InspectionGeometry.ControlPoints.Add(new NamePoint<Point3D>("Pivot", shape.Pivot));

            if (shape is CubeShape cube)
                shape.InspectionGeometry.ControlPoints.Add(new NamePoint<Point3D>("Position", cube.Position));
            else if (shape is SphereShape sphere)
                shape.InspectionGeometry.ControlPoints.Add(new NamePoint<Point3D>("Center", sphere.Center));
            else if (shape is CylinderShape cylinder)
                shape.InspectionGeometry.ControlPoints.Add(new NamePoint<Point3D>("Center", cylinder.Center));
            else if (shape is PrismShape prism)
                shape.InspectionGeometry.ControlPoints.Add(new NamePoint<Point3D>("Center", prism.Center));
            else if (shape is PyramidShape pyramid)
                shape.InspectionGeometry.ControlPoints.Add(new NamePoint<Point3D>("Center", pyramid.Center));
        }

        private static int FindVertexIndex(IReadOnlyList<Point3D> vertices, Point3D point)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (AreSame(vertices[i], point))
                    return i;
            }

            return -1;
        }

        private static bool AreSame(Point3D a, Point3D b)
        {
            return Math.Abs(a.X - b.X) < Epsilon
                && Math.Abs(a.Y - b.Y) < Epsilon
                && Math.Abs(a.Z - b.Z) < Epsilon;
        }

        private static (List<Point3D> Vertices, List<Edge<Point3D>> Edges, List<Face> Faces) Offset(
            (List<Point3D> Vertices, List<Edge<Point3D>> Edges, List<Face> Faces) model,
            Point3D offset)
        {
            List<Point3D> vertices = model.Vertices.Select(point => Add(point, offset)).ToList();
            List<Edge<Point3D>> edges = model.Edges
                .Select(edge => new Edge<Point3D>(Add(edge.Start, offset), Add(edge.End, offset)))
                .ToList();
            List<Face> faces = model.Faces
                .Select(face => new Face(face.VertexIndices, face.EdgeIndices, vertices))
                .ToList();

            return (vertices, edges, faces);
        }

        private static Point3D Add(Point3D point, Point3D offset)
        {
            return new Point3D(point.X + offset.X, point.Y + offset.Y, point.Z + offset.Z);
        }
    }
}
