using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Geometry;

namespace Project_CG_Paint.CoreModel.Model
{
    public static class ShapeFactory3D
    {
        // cube-hình lập phương
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            CreateCube(double size)
        {
            return CreateCuboid(size, size, size);
        }

        // cuboid-hộp chữ nhật (8 đỉnh, 12 cạnh)
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            CreateCuboid(double width, double height, double depth)
        {
            double w = width / 2.0;
            double h = height / 2.0;
            double d = depth / 2.0;
            var v = new List<Point3D>
            {
                new Point3D(-w, -h, -d), // 0
                new Point3D( w, -h, -d), // 1
                new Point3D( w,  h, -d), // 2
                new Point3D(-w,  h, -d), // 3
                new Point3D(-w, -h,  d), // 4
                new Point3D( w, -h,  d), // 5
                new Point3D( w,  h,  d), // 6
                new Point3D(-w,  h,  d), // 7
            };

            var e = new List<Edge<Point3D>>
            {
                // Mặt sau (z = -d)
                new Edge<Point3D>(v[0], v[1]),
                new Edge<Point3D>(v[1], v[2]),
                new Edge<Point3D>(v[2], v[3]),
                new Edge<Point3D>(v[3], v[0]),
                // Mặt trước (z = +d)
                new Edge<Point3D>(v[4], v[5]),
                new Edge<Point3D>(v[5], v[6]),
                new Edge<Point3D>(v[6], v[7]),
                new Edge<Point3D>(v[7], v[4]),
                // Cạnh nối hai mặt
                new Edge<Point3D>(v[0], v[4]),
                new Edge<Point3D>(v[1], v[5]),
                new Edge<Point3D>(v[2], v[6]),
                new Edge<Point3D>(v[3], v[7]),
            };

            return (v, e);
        }

        // sphere-hình cầu (lưới kinh / vĩ tuyến)
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            CreateSphere(double radius, int stacks = 8, int slices = 12)
        {
            var vertices = new List<Point3D>();
            var edges = new List<Edge<Point3D>>();

            // Sinh đỉnh: từ cực Nam → cực Bắc
            for (int i = 0; i <= stacks; i++)
            {
                double phi = Math.PI * i / stacks;          // 0 → π
                for (int j = 0; j < slices; j++)
                {
                    double theta = 2 * Math.PI * j / slices; // 0 → 2π
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
                    int next = i * slices + (j + 1) % slices;
                    int below = (i + 1) * slices + j;

                    // Cạnh ngang (vĩ tuyến)
                    edges.Add(new Edge<Point3D>(vertices[curr], vertices[next]));
                    // Cạnh dọc (kinh tuyến)
                    edges.Add(new Edge<Point3D>(vertices[curr], vertices[below]));
                }
            }

            return (vertices, edges);
        }

        // cylinder-hình trụ
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            CreateCylinder(double radius, double height, int segments = 16)
        {
            var vertices = new List<Point3D>();
            var edges = new List<Edge<Point3D>>();

            double halfH = height / 2.0;

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

                // Cạnh đứng
                edges.Add(new Edge<Point3D>(vertices[bottom], vertices[top]));
                // Cạnh vòng dưới
                edges.Add(new Edge<Point3D>(vertices[bottom], vertices[nextBottom]));
                // Cạnh vòng trên
                edges.Add(new Edge<Point3D>(vertices[top], vertices[nextTop]));
            }

            return (vertices, edges);
        }

        // cone-hình nón
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            CreateCone(double radius, double height, int segments = 16)
        {
            var vertices = new List<Point3D>();
            var edges = new List<Edge<Point3D>>();

            double halfH = height / 2.0;

            // Đỉnh nón
            vertices.Add(new Point3D(0, halfH, 0)); // index 0

            // Đỉnh vòng đáy
            for (int i = 0; i < segments; i++)
            {
                double angle = 2 * Math.PI * i / segments;
                double x = radius * Math.Cos(angle);
                double z = radius * Math.Sin(angle);
                vertices.Add(new Point3D(x, -halfH, z)); // index 1..segments
            }

            for (int i = 0; i < segments; i++)
            {
                int curr = 1 + i;
                int next = 1 + (i + 1) % segments;

                // Cạnh từ đỉnh nón xuống đáy
                edges.Add(new Edge<Point3D>(vertices[0], vertices[curr]));
                // Cạnh vòng đáy
                edges.Add(new Edge<Point3D>(vertices[curr], vertices[next]));
            }

            return (vertices, edges);
        }

        // pyramid-hình chóp chữ nhật
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            CreatePyramid(double baseWidth, double baseDepth, double height)
        {
            double w = baseWidth / 2.0;
            double d = baseDepth / 2.0;
            double halfH = height / 2.0;

            var v = new List<Point3D>
            {
                new Point3D( 0,  halfH,  0), // 0 — đỉnh chóp
                new Point3D(-w, -halfH, -d), // 1
                new Point3D( w, -halfH, -d), // 2
                new Point3D( w, -halfH,  d), // 3
                new Point3D(-w, -halfH,  d), // 4
            };

            var e = new List<Edge<Point3D>>
            {
                // Đáy
                new Edge<Point3D>(v[1], v[2]),
                new Edge<Point3D>(v[2], v[3]),
                new Edge<Point3D>(v[3], v[4]),
                new Edge<Point3D>(v[4], v[1]),
                // Cạnh bên
                new Edge<Point3D>(v[0], v[1]),
                new Edge<Point3D>(v[0], v[2]),
                new Edge<Point3D>(v[0], v[3]),
                new Edge<Point3D>(v[0], v[4]),
            };

            return (v, e);
        }

        // prism-lăng trụ đều (đáy đa giác đều)
        public static (List<Point3D> Vertices, List<Edge<Point3D>> Edges)
            CreatePrism(double radius, int sides, double height)
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
