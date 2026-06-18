using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape3D
{
    public static class CylinderWireFrame
    {
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges, List<Face> Faces)
            Generate(double radius, double height, int segments = 16)
        {
            var vertices = new List<Point3D>();
            var edges = new List<Edge<Point3D>>();
            var faces = new List<Face>();

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
                int next = (i + 1) % segments;
                edges.Add(new Edge<Point3D>(vertices[i * 2], vertices[i * 2 + 1]));
                edges.Add(new Edge<Point3D>(vertices[i * 2], vertices[next * 2]));
                edges.Add(new Edge<Point3D>(vertices[i * 2 + 1], vertices[next * 2 + 1]));
            }
            for (int i = 0; i < segments; i++)
            {
                int next = (i + 1) % segments;
                faces.Add(new Face(
                    new List<int> { i * 2, i * 2 + 1, next * 2 + 1, next * 2 },
                    new List<int> { 3 * i, 3 * i + 2, 3 * next, 3 * i + 1 },
                    vertices
                ));
            }
            var botVerts = new List<int>(); var botEdges = new List<int>();
            for (int i = 0; i < segments; i++) { botVerts.Add(i * 2); botEdges.Add(3 * i + 1); }
            faces.Add(new Face(botVerts, botEdges, vertices));
            var topVerts = new List<int>(); var topEdges = new List<int>();
            for (int i = segments - 1; i >= 0; i--) topVerts.Add(i * 2 + 1);
            for (int i = 0; i < segments; i++) topEdges.Add(3 * i + 2);
            faces.Add(new Face(topVerts, topEdges, vertices));

            return (vertices, edges, faces);
        }
    }
}
