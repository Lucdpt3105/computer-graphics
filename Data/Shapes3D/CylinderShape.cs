using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model; 

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
            this.Pivot = Center;
        }
    }
}
