using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape3D
{
    public static class PrismWireFrame
    {
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            Generate(double radius, int sides, double height)
        {
            var vertices = new List<Point3D>();
            var edges = new List<Edge<Point3D>>();

            double halfH = height / 2.0;

            for (int i = 0; i < sides; i++)
            {
                double angle = 2 * Math.PI * i / sides;
                double x = radius * Math.Cos(angle);
                double z = radius * Math.Sin(angle);
                vertices.Add(new Point3D(x, -halfH, z)); // đáy dưới
                vertices.Add(new Point3D(x, halfH, z)); // đáy trên
            }

            for (int i = 0; i < sides; i++)
            {
                int bottom = i * 2;
                int top = i * 2 + 1;
                int nextBottom = ((i + 1) % sides) * 2;
                int nextTop = ((i + 1) % sides) * 2 + 1;

                // Cạnh đứng
                edges.Add(new Edge<Point3D>(vertices[bottom], vertices[top]));
                // Cạnh đáy dưới
                edges.Add(new Edge<Point3D>(vertices[bottom], vertices[nextBottom]));
                // Cạnh đáy trên
                edges.Add(new Edge<Point3D>(vertices[top], vertices[nextTop]));
            }

            return (vertices, edges);
        }
    }
}
