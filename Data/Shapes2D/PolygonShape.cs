using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.GeometryInspection;
using Project_CG_Paint.Data.Objects;
using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Geometry;

namespace Project_CG_Paint.Data.Shapes2D
{
    public class PolygonShape : Shape2D
    {
        private readonly List<Point2D> _inputVertices = new List<Point2D>();
        public IReadOnlyList<Point2D> InputVertices => _inputVertices;

        public PolygonShape(IEnumerable<Point2D> vertices)
        {
            SetDefinition(vertices);
        }

        public void SetDefinition(IEnumerable<Point2D> vertices)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }

            List<Point2D> copiedVertices = new List<Point2D>(vertices);

            if (copiedVertices.Count < 3)
            {
                throw new ArgumentException(
                    "Polygon requires at least three vertices.",
                    nameof(vertices));
            }

            double signedDoubleArea = CalculateSignedDoubleArea(copiedVertices);

            if (Math.Abs(signedDoubleArea) < double.Epsilon)
            {
                throw new ArgumentException(
                    "Polygon area must be greater than zero.",
                    nameof(vertices));
            }

            _inputVertices.Clear();
            _inputVertices.AddRange(copiedVertices);

            InitializeShapeData();
        }

        protected override void RebuildInspectionGeometry()
        {
            InspectionGeometry.Clear();

            InspectionGeometry.OriginalVertices.AddRange(_inputVertices);

            for (int i = 0; i < _inputVertices.Count; i++)
            {
                int nextIndex = (i + 1) % _inputVertices.Count;

                InspectionGeometry.Edges.Add(new EdgeIndex(i, nextIndex));

                InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>( $"Vertex[{i}]", _inputVertices[i]));
            }

            InspectionGeometry.ResetCurrentToOriginal();
        }

        protected override Point2D CalculateDefaultPivot()
        {
            double signedDoubleArea = CalculateSignedDoubleArea(_inputVertices);

            if (Math.Abs(signedDoubleArea) < double.Epsilon)
            {
                return CalculateAveragePoint( _inputVertices);
            }

            double centroidX = 0;
            double centroidY = 0;

            for (int i = 0; i < _inputVertices.Count; i++)
            {
                Point2D current = _inputVertices[i];

                Point2D next = _inputVertices[(i + 1) % _inputVertices.Count];

                double cross = current.X * next.Y - next.X * current.Y;

                centroidX += (current.X + next.X) * cross;

                centroidY += (current.Y + next.Y) * cross;
            }

            double denominator = 3.0 * signedDoubleArea;

            return new Point2D(centroidX / denominator, centroidY / denominator);
        }

        private static double CalculateSignedDoubleArea(IReadOnlyList<Point2D> vertices)
        {
            double result = 0;

            for (int i = 0; i < vertices.Count; i++)
            {
                Point2D current = vertices[i];

                Point2D next =vertices[(i + 1) % vertices.Count];

                result += current.X * next.Y - next.X * current.Y;
            }

            return result;
        }

        private static Point2D CalculateAveragePoint(IReadOnlyList<Point2D> vertices)
        {
            double sumX = 0;
            double sumY = 0;

            for (int i = 0; i < vertices.Count; i++)
            {
                sumX += vertices[i].X;
                sumY += vertices[i].Y;
            }

            return new Point2D(sumX / vertices.Count, sumY / vertices.Count);
        }
        public override BoundingBox2D GetLocalBounds()
        {
            if (_inputVertices.Count == 0)
            {
                return new BoundingBox2D(0, 0, 0, 0);
            }
            double minX = _inputVertices[0].X;
            double minY = _inputVertices[0].Y;
            double maxX = _inputVertices[0].X;
            double maxY = _inputVertices[0].Y;
            for (int i = 1; i < _inputVertices.Count; i++)
            {
                Point2D vertex = _inputVertices[i];
                if (vertex.X < minX) minX = vertex.X;
                if (vertex.Y < minY) minY = vertex.Y;
                if (vertex.X > maxX) maxX = vertex.X;
                if (vertex.Y > maxY) maxY = vertex.Y;
            }
            return new BoundingBox2D(minX, minY, maxX, maxY);
        }
    }
}