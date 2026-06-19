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
    public class EllipseShape : Shape2D
    {
        public Point2D Center { get; private set; }
        public double RadiusX { get; private set; }
        public double RadiusY { get; private set; }
        public EllipseShape(Point2D center, double radiusX, double radiusY)
        {
            SetDefinition(center, radiusX, radiusY);
        }
        private void SetDefinition(Point2D center, double radiusX, double radiusY)
        {
            if (radiusX <= 0 || radiusY <= 0)
            {
                throw new ArgumentException("Radii must be positive.");
            }
            Center = center;
            RadiusX = radiusX;
            RadiusY = radiusY;
            InitializeShapeData();
        }
        protected override void RebuildInspectionGeometry()
        {
            InspectionGeometry.Clear();
            InspectionGeometry.ControlPoints.Add(new NamePoint<Point2D>("Center", Center));
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
