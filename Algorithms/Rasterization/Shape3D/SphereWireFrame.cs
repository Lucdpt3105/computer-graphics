using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape3D
{
    public static class SphereWireFrame
    {
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            Generate(double radius, int stacks = 8, int slices = 12)
        {
            var vertices = new List<Point3D>();
            var edges = new List<Edge<Point3D>>();

            for (int i = 0; i <= stacks; i++)
            {
                double phi = Math.PI * i / stacks;
                for (int j = 0; j < slices; j++)
                {
                    double theta = 2 * Math.PI * j / slices;
                    double x = radius * Math.Sin(phi) * Math.Cos(theta);
                    double y = radius * Math.Cos(phi);
                    double z = radius * Math.Sin(phi) * Math.Sin(theta);
                    vertices.Add(new Point3D(x, y, z));
                }
            }

            // Sinh cạnh
            for (int i = 0; i < stacks; i++)
            {
                for (int j = 0; j < slices; j++)
                {
                    int curr = i * slices + j;
                    int next = i * slices + (j + 1) % slices; // điểm kế trên cùng vĩ tuyến
                    int below = (i + 1) * slices + j;           // điểm ngay dưới

                    // Cạnh ngang (vĩ tuyến)
                    edges.Add(new Edge<Point3D>(vertices[curr], vertices[next]));
                    // Cạnh dọc (kinh tuyến)
                    edges.Add(new Edge<Point3D>(vertices[curr], vertices[below]));
                }
            }

            return (vertices, edges);
        }
    }
}
