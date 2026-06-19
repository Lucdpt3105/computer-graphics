using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Data.GeometryInspection;
using Project_CG_Paint.CoreModel.Geometry;

namespace Project_CG_Paint.Data.Shapes2D
{
    public class LineShape : Shape2D
    {
        public Point2D Start { get; private set; }
        public Point2D End { get; private set; }
        public LineShape(Point2D start, Point2D end)
        {
            SetDefinition(start, end);
        }
        private void SetDefinition(Point2D start, Point2D end)
        {
            if(AreSamePoint(start, end))
            {
                throw new ArgumentException("Start and end points cannot be the same.");
            }    
            Start = start;
            End = end;
            InitializeShapeData();
        }
        protected override void RebuildInspectionGeometry()
        {
            InspectionGeometry.Clear();

            InspectionGeometry.OriginalVertices.Add(Start);
            InspectionGeometry.OriginalVertices.Add(End);

            InspectionGeometry.Edges.Add(new EdgeIndex(0, 1));

            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("Start", Start));
            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("End", End));

            InspectionGeometry.ResetCurrentToOriginal();
        }
        protected override Point2D CalculateDefaultPivot()
        {
            return new Point2D((Start.X + End.X) / 2.0, (Start.Y + End.Y) / 2.0);
        }
        private bool AreSamePoint(Point2D p1, Point2D p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        public override BoundingBox2D GetLocalBounds()
        {
            double minX = Math.Min(Start.X, End.X);
            double minY = Math.Min(Start.Y, End.Y);
            double maxX = Math.Max(Start.X, End.X);
            double maxY = Math.Max(Start.Y, End.Y);
            return new BoundingBox2D(minX, minY, maxX, maxY);
        }
    }
}
