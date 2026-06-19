using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Data.GeometryInspection;
using Project_CG_Paint.CoreModel.Geometry;

namespace Project_CG_Paint.Data.Shapes2D
{
    public class TriangleShape : Shape2D
    {
        public Point2D VertexA { get; private set; }
        public Point2D VertexB { get; private set; }
        public Point2D VertexC { get; private set; }
        public TriangleShape(Point2D vertexA, Point2D vertexB, Point2D vertexC)
        {
            SetDefinition(vertexA, vertexB, vertexC);
        }
        private void SetDefinition(Point2D vertexA, Point2D vertexB, Point2D vertexC)
        {
            if(Math.Abs(Cross(vertexA, vertexB, vertexC)) < double.Epsilon)
            {
                throw new ArgumentException("The vertices must not be collinear.");
            }

            VertexA = vertexA;
            VertexB = vertexB;
            VertexC = vertexC;

            InitializeShapeData();
        }

        protected override void RebuildInspectionGeometry()
        {
            InspectionGeometry.Clear();

            InspectionGeometry.OriginalVertices.Add(VertexA);
            InspectionGeometry.OriginalVertices.Add(VertexB);
            InspectionGeometry.OriginalVertices.Add(VertexC);

            InspectionGeometry.Edges.Add(new EdgeIndex(0, 1));
            InspectionGeometry.Edges.Add(new EdgeIndex(1, 2));
            InspectionGeometry.Edges.Add(new EdgeIndex(2, 0));

            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("VertexA", VertexA));
            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("VertexB", VertexB));
            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("VertexC", VertexC));

            InspectionGeometry.ResetCurrentToOriginal();
        }
        protected override Point2D CalculateDefaultPivot()
        {
            return new Point2D((VertexA.X + VertexB.X + VertexC.X) / 3.0, (VertexA.Y + VertexB.Y + VertexC.Y) / 3.0);
        }
        private double Cross(Point2D p1, Point2D p2, Point2D p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
        }
        public override BoundingBox2D GetLocalBounds()
        {
            double minX = Math.Min(VertexA.X, Math.Min(VertexB.X, VertexC.X));
            double minY = Math.Min(VertexA.Y, Math.Min(VertexB.Y, VertexC.Y));
            double maxX = Math.Max(VertexA.X, Math.Max(VertexB.X, VertexC.X));
            double maxY = Math.Max(VertexA.Y, Math.Max(VertexB.Y, VertexC.Y));
            return new BoundingBox2D(minX, minY, maxX, maxY);
        }
    }
}
