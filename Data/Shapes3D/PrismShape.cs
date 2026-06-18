using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Shapes3D
{
    public class PrismShape : Shape3D
    {
        public double Radius { get; set; }
        public int Sides { get; set; }
        public double Height { get; set; }
        public Point3D Center { get; set; }
        public PrismShape(double radius, int sides, double height, Point3D center)
        {
            Radius = radius;
            Sides = sides;
            Height = height;
            Center = center;
            this.Pivot = Center;
        }
    }
}
