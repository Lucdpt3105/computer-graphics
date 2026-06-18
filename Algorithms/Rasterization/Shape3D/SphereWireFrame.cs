using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape3D
{
    public static class SphereWireFrame
    {
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges, List<Face> Faces)
            Generate(double radius, int stacks = 8, int slices = 12)
        {
            var vertices = new List<Point3D>();
            var edges = new List<Edge<Point3D>>();
            var faces = new List<Face>();

            for (int i = 0; i <= stacks; i++)
            {
                double phi = Math.PI * i / stacks;
                for (int j = 0; j < slices; j++)
                {
                    double theta = 2 * Math.PI * j / slices;
                    vertices.Add(new Point3D(
                        radius * Math.Sin(phi) * Math.Cos(theta),
                        radius * Math.Cos(phi),
                        radius * Math.Sin(phi) * Math.Sin(theta)
                    ));
                }
            }

            // Sinh cạnh
            for (int i = 0; i < stacks; i++)
            {
                for (int j = 0; j < slices; j++)
                {
                    int curr = i * slices + j;
                    int next = i * slices + (j + 1) % slices;
                    int below = (i + 1) * slices + j;
                    edges.Add(new Edge<Point3D>(vertices[curr], vertices[next]));  // 2*curr
                    edges.Add(new Edge<Point3D>(vertices[curr], vertices[below])); // 2*curr+1
                }
            }
            // Sinh mặt
            for (int i = 0; i < stacks; i++)
            {
                for (int j = 0; j < slices; j++)
                {
                    int curr = i * slices + j;
                    int next = i * slices + (j + 1) % slices;
                    int belowNext = (i + 1) * slices + (j + 1) % slices;
                    int below = (i + 1) * slices + j;
                    var faceVerts = new List<int> { curr, next, belowNext, below };
                    // Edge indices
                    var faceEdges = new List<int>
                    {
                        2 * curr,        
                        2 * next + 1,     
                        2 * curr + 1      
                    };
                    // Horizontal edge at row i+1 chỉ tồn tại khi i < stacks-1
                    if (i < stacks - 1)
                        faceEdges.Add(2 * below); 
                    faces.Add(new Face(faceVerts, faceEdges, vertices));
                }
            }

            return (vertices, edges, faces);
        }
    }
}
