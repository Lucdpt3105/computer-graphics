using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;

namespace Project_CG_Paint.Data.Shapes3D
{
    public class CubeShape : Shape3D
    {
        public double Size { get; set; }
        public Point3D Center { get; set; }
        public CubeShape(double size, Point3D center)
        {
            Size = size;
            Center = center;
            this.Pivot = Center;
        }
    }
}
