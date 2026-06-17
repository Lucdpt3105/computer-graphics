using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape3D
{
    public static class CylinderWireFrame
    {
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            Generate(double radius, double height, int segments = 16)
        {
            var vertices = new List<Point3D>();
            var edges = new List<Edge<Point3D>>();

            double halfH = height / 2.0;

            // index: đáy dưới = i*2, đáy trên = i*2+1
            for (int i = 0; i < segments; i++)
            {
                double angle = 2 * Math.PI * i / segments;
                double x = radius * Math.Cos(angle);
                double z = radius * Math.Sin(angle);
                vertices.Add(new Point3D(x, -halfH, z)); // vòng dưới
                vertices.Add(new Point3D(x, halfH, z)); // vòng trên
            }

            for (int i = 0; i < segments; i++)
            {
                int bottom = i * 2;
                int top = i * 2 + 1;
                int nextBottom = ((i + 1) % segments) * 2;
                int nextTop = ((i + 1) % segments) * 2 + 1;

                // Cạnh đứng (đường sinh)
                edges.Add(new Edge<Point3D>(vertices[bottom], vertices[top]));
                // Cạnh vòng dưới
                edges.Add(new Edge<Point3D>(vertices[bottom], vertices[nextBottom]));
                // Cạnh vòng trên
                edges.Add(new Edge<Point3D>(vertices[top], vertices[nextTop]));
            }

            return (vertices, edges);
        }
    }
}
