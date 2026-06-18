using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;

namespace Project_CG_Paint.Data.Shapes2D
{
    public class CircleShape : Shape2D
    {
        public Point2D Center { get; set; }
        public double Radius { get; set; }

        public CircleShape(Point2D center, double radius)
        {
            Center = center;
            Radius = radius;
            this.Pivot = Center;
        }
    }
}
