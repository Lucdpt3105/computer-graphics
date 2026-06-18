using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Shapes2D
{
    public class DiamondShape : Shape2D
    {
        public Point2D Center { get; set; }
        public double RadiusX { get; set; }
        public double RadiusY { get; set; }
        public DiamondShape(Point2D center, double radiusX, double radiusY)
        {
            Center = center;
            RadiusX = radiusX;
            RadiusY = radiusY;
            this.Pivot = Center;
        }
    }   
}
