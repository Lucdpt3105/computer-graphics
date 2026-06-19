using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model; 
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Services;

namespace Project_CG_Paint.Data.Shapes3D
{
    public class CylinderShape : Shape3D
    {
        public double Radius { get; set; }
        public double Height { get; set; }
        public int Segments { get; set; }
        public Point3D Center { get; set; }
        public CylinderShape(double radius, double height, int segments, Point3D center)
        {
            Radius = radius;
            Height = height;
            Segments = segments;
            Center = center;
            InitializeShapeData();
        }

        protected override void RebuildInspectionGeometry()
        {
            Shape3DGeometryBuilder.PopulateInspectionGeometry(this);
        }

        protected override Point3D CalculateDefaultPivot()
        {
            return Center;
        }
    }
}
