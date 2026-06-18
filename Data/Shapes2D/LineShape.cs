using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Data.Shapes2D
{
    public class LineShape : Shape2D
    {
        public Point2D Start { get; set; }
        public Point2D End { get; set; }
        public LineShape(Point2D start, Point2D end)
        {
            Start = start;
            End = end;
            this.Pivot = new Point2D((start.X + end.X) / 2, (start.Y + end.Y) / 2);
        }
    }
}
