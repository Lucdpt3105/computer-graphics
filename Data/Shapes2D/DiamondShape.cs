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
    public class DiamondShape : Shape2D
    {
        public Point2D Center { get; private set; }
        public double RadiusX { get; private set; }
        public double RadiusY { get; private set; }
        public DiamondShape(Point2D center, double radiusX, double radiusY)
        {
            SetDefinition(center, radiusX, radiusY);
        }
        private void SetDefinition(Point2D center, double radiusX, double radiusY)
        {
            if(radiusX <= 0 || radiusY <= 0)
            {
                throw new ArgumentException("RadiusX and RadiusY must be positive.");
            }

            Center = center;
            RadiusX = radiusX;
            RadiusY = radiusY;

            InitializeShapeData();
        }
        protected override void RebuildInspectionGeometry()
        {
            InspectionGeometry.Clear();

            Point2D top = new Point2D(Center.X, Center.Y - RadiusY);
            Point2D right = new Point2D(Center.X + RadiusX, Center.Y);
            Point2D bottom = new Point2D(Center.X, Center.Y + RadiusY);
            Point2D left = new Point2D(Center.X - RadiusX, Center.Y);

            InspectionGeometry.OriginalVertices.Add(top);
            InspectionGeometry.OriginalVertices.Add(right);
            InspectionGeometry.OriginalVertices.Add(bottom);
            InspectionGeometry.OriginalVertices.Add(left);

            InspectionGeometry.Edges.Add(new EdgeIndex(0, 1));
            InspectionGeometry.Edges.Add(new EdgeIndex(1, 2));
            InspectionGeometry.Edges.Add(new EdgeIndex(2, 3));
            InspectionGeometry.Edges.Add(new EdgeIndex(3, 0));

            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("Center", Center));
            
            InspectionGeometry.ResetCurrentToOriginal();
        }
        protected override Point2D CalculateDefaultPivot()
        {
            return Center;
        }
        public override BoundingBox2D GetLocalBounds()
        {
            double minX = Center.X - RadiusX;
            double minY = Center.Y - RadiusY;
            double maxX = Center.X + RadiusX;
            double maxY = Center.Y + RadiusY;
            return new BoundingBox2D(minX, minY, maxX, maxY);
        }
    }   
}
