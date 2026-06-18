using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.Data.Objects;

namespace Project_CG_Paint.Data.Shapes2D
{
    public class RectangleShape : Shape2D
    {
        public Point2D TopLeft { get; set; }
        public Point2D BottomRight { get; set; }
        public RectangleShape(Point2D topLeft, Point2D bottomRight) 
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
            this.Pivot = new Point2D((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);
        }
    }
}
