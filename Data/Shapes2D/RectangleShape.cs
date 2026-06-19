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
    public class RectangleShape : Shape2D
    {
        public Point2D TopLeft { get; private set; }
        public Point2D BottomRight { get; private set; }
        public RectangleShape(Point2D topLeft, Point2D bottomRight) 
        {
            this.SetDefinition(topLeft, bottomRight);
        }
        private void SetDefinition(Point2D topLeft, Point2D bottomRight)
        {
            double left = Math.Min(topLeft.X, bottomRight.X);
            double right = Math.Max(topLeft.X, bottomRight.X);
            double top = Math.Min(topLeft.Y, bottomRight.Y);
            double bottom = Math.Max(topLeft.Y, bottomRight.Y);

            if(left == right || top == bottom)
            {
                throw new ArgumentException("Rectangle width and height must be greater than zero.");
            }
            this.TopLeft = new Point2D(left, top);
            this.BottomRight = new Point2D(right, bottom);

            this.InitializeShapeData();
        }

        protected override void RebuildInspectionGeometry()
        {
            InspectionGeometry.Clear();

            Point2D topRight = new Point2D(BottomRight.X, TopLeft.Y);
            Point2D bottomLeft = new Point2D(TopLeft.X, BottomRight.Y);

            InspectionGeometry.OriginalVertices.Add(TopLeft);
            InspectionGeometry.OriginalVertices.Add(topRight);
            InspectionGeometry.OriginalVertices.Add(BottomRight);
            InspectionGeometry.OriginalVertices.Add(bottomLeft);

            InspectionGeometry.Edges.Add(new EdgeIndex(0, 1));
            InspectionGeometry.Edges.Add(new EdgeIndex(1, 2));
            InspectionGeometry.Edges.Add(new EdgeIndex(2, 3));
            InspectionGeometry.Edges.Add(new EdgeIndex(3, 0));

            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("TopLeft", TopLeft));
            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("BottomRight", BottomRight));

            InspectionGeometry.ResetCurrentToOriginal();
        }
        protected override Point2D CalculateDefaultPivot()
        {
            return new Point2D
                ((TopLeft.X + BottomRight.X) / 2.0, 
                (TopLeft.Y + BottomRight.Y) / 2.0);
        }
        public override BoundingBox2D GetLocalBounds()
        {
            return new BoundingBox2D(TopLeft.X, TopLeft.Y, BottomRight.X, BottomRight.Y);
        }
    }
}
