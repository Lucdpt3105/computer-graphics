using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Data.Shapes2D;
using Project_CG_Paint.Data.Shapes3D;

namespace Project_CG_Paint.Controllers.Paint
{
    public class DrawingController
    {
        private int _objectCounter = 1;

        /// <summary>
        /// Creates a 2D object from a list of clicked points on the canvas.
        /// </summary>
        public GraphicObject CreateObjectFromClicks(DrawType drawType, IReadOnlyList<Point2D> clicks, Color strokeColor)
        {
            GraphicObject result;

            switch (drawType)
            {
                case DrawType.Point:
                    Require(clicks, 1);
                    result = new CircleShape(clicks[0], 0.5);
                    break;
                case DrawType.Line:
                    Require(clicks, 2);
                    result = new LineShape(clicks[0], clicks[1]);
                    break;
                case DrawType.Rectangle:
                    Require(clicks, 2);
                    result = new RectangleShape(clicks[0], clicks[1]);
                    break;
                case DrawType.Circle:
                    Require(clicks, 2);
                    result = new CircleShape(clicks[0], Distance(clicks[0], clicks[1]));
                    break;
                case DrawType.Ellipse:
                    Require(clicks, 2);
                    result = new EllipseShape(
                        clicks[0],
                        Math.Abs(clicks[1].X - clicks[0].X),
                        Math.Abs(clicks[1].Y - clicks[0].Y));
                    break;
                case DrawType.Triangle:
                    Require(clicks, 3);
                    result = new TriangleShape(clicks[0], clicks[1], clicks[2]);
                    break;
                case DrawType.Diamond:
                    Require(clicks, 2);
                    result = new DiamondShape(
                        clicks[0],
                        Math.Abs(clicks[1].X - clicks[0].X),
                        Math.Abs(clicks[1].Y - clicks[0].Y));
                    break;
                case DrawType.Parallelogram:
                    Require(clicks, 3);
                    result = new ParallelogramShape(clicks[0], clicks[1], clicks[2]);
                    break;
                case DrawType.Polygon:
                    if (clicks.Count < 3)
                        throw new ArgumentException("Polygon cần tối thiểu 3 đỉnh.");
                    result = new PolygonShape(clicks.ToArray());
                    break;
                default:
                    throw new NotSupportedException($"Không hỗ trợ loại hình: {drawType}");
            }

            result.Metadata.Name = $"{drawType} {_objectCounter++}";
            result.Style.StrokeColor = strokeColor;
            result.Style.FillColor = Color.Transparent;
            result.Style.IsFilled = false;
            return result;
        }

        /// <summary>
        /// Creates a 3D object from parameter dictionary and position.
        /// </summary>
        public GraphicObject CreateObject3DFromParams(DrawType drawType, Dictionary<string, double> parameters, Point3D position, Color strokeColor)
        {
            GraphicObject result;

            switch (drawType)
            {
                case DrawType.Cube:
                    result = new CubeShape(
                        GetParam(parameters, "Size"),
                        position);
                    break;
                case DrawType.Sphere:
                    result = new SphereShape(
                        GetParam(parameters, "Radius"),
                        (int)GetParam(parameters, "Stacks"),
                        (int)GetParam(parameters, "Slices"),
                        position);
                    break;
                case DrawType.Pyramid:
                    result = new PyramidShape(
                        GetParam(parameters, "BaseWidth"),
                        GetParam(parameters, "BaseDepth"),
                        GetParam(parameters, "Height"),
                        position);
                    break;
                case DrawType.Prism:
                    result = new PrismShape(
                        GetParam(parameters, "Radius"),
                        (int)GetParam(parameters, "Sides"),
                        GetParam(parameters, "Height"),
                        position);
                    break;
                case DrawType.Cylinder:
                    result = new CylinderShape(
                        GetParam(parameters, "Radius"),
                        GetParam(parameters, "Height"),
                        (int)GetParam(parameters, "Segments"),
                        position);
                    break;
                default:
                    throw new NotSupportedException($"Không hỗ trợ loại hình 3D: {drawType}");
            }

            result.Metadata.Name = $"{drawType} {_objectCounter++}";
            result.Style.StrokeColor = strokeColor;
            result.Style.FillColor = Color.Transparent;
            result.Style.IsFilled = false;
            return result;
        }

        /// <summary>
        /// Original method kept for backward compatibility.
        /// </summary>
        public GraphicObject CreateObject(DrawType drawType, Point2D start, Point2D end, Color strokeColor)
        {
            return CreateObjectFromClicks(drawType, new[] { start, end }, strokeColor);
        }

        private static void Require(IReadOnlyList<Point2D> clicks, int count)
        {
            if (clicks == null || clicks.Count < count)
                throw new ArgumentException($"Cần ít nhất {count} điểm.");
        }

        private static double GetParam(Dictionary<string, double> parameters, string key)
        {
            if (parameters == null || !parameters.ContainsKey(key))
                throw new ArgumentException($"Thiếu tham số: {key}");
            return parameters[key];
        }

        private static double Distance(Point2D start, Point2D end)
        {
            double dx = end.X - start.X;
            double dy = end.Y - start.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
