using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.CoreModel.Model
{
    public readonly struct Point2D
    {
        public double X { get; }
        public double Y { get; }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
}
