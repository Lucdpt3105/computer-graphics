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
    public class ParallelogramShape : Shape2D
    {
        public Point2D VertexA { get; private set; }
        public Point2D VertexB { get; private set; }
        public Point2D VertexC { get; private set; }
        public ParallelogramShape(Point2D vertexA, Point2D vertexB, Point2D vertexC)
        {
            SetDefinition(vertexA, vertexB, vertexC);
        }
        private void SetDefinition(Point2D vertexA, Point2D vertexB, Point2D vertexC)
        {
            if(Math.Abs(Cross(vertexA, vertexB, vertexC)) < double.Epsilon)
            {
                throw new ArgumentException("The provided points do not form a valid parallelogram.");
            }
            VertexA = vertexA;
            VertexB = vertexB;
            VertexC = vertexC;
            
            InitializeShapeData();
        }
        protected override void RebuildInspectionGeometry()
        {
            InspectionGeometry.Clear();

            Point2D vertexD = CalculateVertexD();

            InspectionGeometry.OriginalVertices.Add(VertexA);
            InspectionGeometry.OriginalVertices.Add(VertexB);
            InspectionGeometry.OriginalVertices.Add(vertexD);
            InspectionGeometry.OriginalVertices.Add(VertexC);

            InspectionGeometry.Edges.Add(new EdgeIndex(0, 1));
            InspectionGeometry.Edges.Add(new EdgeIndex(1, 2));
            InspectionGeometry.Edges.Add(new EdgeIndex(2, 3));
            InspectionGeometry.Edges.Add(new EdgeIndex(3, 0));

            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("A", VertexA));
            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("B", VertexB));
            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("C", VertexC));

            InspectionGeometry.ResetCurrentToOriginal();
        }
        protected override Point2D CalculateDefaultPivot()
        {
            Point2D vertexD = CalculateVertexD();
            return new Point2D((VertexA.X + vertexD.X) / 2.0, (VertexA.Y + vertexD.Y) / 2.0);
        }
        private Point2D CalculateVertexD()
        {
            double dx = VertexB.X - VertexA.X;
            double dy = VertexB.Y - VertexA.Y;
            return new Point2D(VertexC.X + dx, VertexC.Y + dy);
        }
        private double Cross(Point2D p1, Point2D p2, Point2D p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
        }
        public override BoundingBox2D GetLocalBounds()
        {
            Point2D vertexD = CalculateVertexD();
            double minX = Math.Min(Math.Min(VertexA.X, VertexB.X), Math.Min(VertexC.X, vertexD.X));
            double minY = Math.Min(Math.Min(VertexA.Y, VertexB.Y), Math.Min(VertexC.Y, vertexD.Y));
            double maxX = Math.Max(Math.Max(VertexA.X, VertexB.X), Math.Max(VertexC.X, vertexD.X));
            double maxY = Math.Max(Math.Max(VertexA.Y, VertexB.Y), Math.Max(VertexC.Y, vertexD.Y));
            return new BoundingBox2D(minX, minY, maxX, maxY);
        }
    }
}
