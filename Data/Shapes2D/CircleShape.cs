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
    public class CircleShape : Shape2D
    {
        public Point2D Center { get; private set; }
        public double Radius { get; private set; }

        public CircleShape(Point2D center, double radius)
        {
            SetDefinition(center, radius);
        }
        private void SetDefinition(Point2D center, double radius)
        {
            if(radius <= 0)
            {
                throw new ArgumentException("Radius must be positive.");
            }

            Center = center;
            Radius = radius;

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
            double minX = Center.X - Radius;
            double minY = Center.Y - Radius;
            double maxX = Center.X + Radius;
            double maxY = Center.Y + Radius;
            return new BoundingBox2D(minX, minY, maxX, maxY);
        }
    }
}
