using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;

namespace Project_CG_Paint.Data.Shapes3D
{
    public class PyramidShape : Shape3D
    {
        public double BaseWidth { get; set; }
        public double BaseDepth { get; set; }
        public double Height { get; set; }
        public Point3D Center { get; set; }
        public PyramidShape(double baseWidth, double baseDepth, double height, Point3D center)
        {
            BaseWidth = baseWidth;
            BaseDepth = baseDepth;
            Height = height;
            Center = center;
            this.Pivot = Center;
        }
    }
}
