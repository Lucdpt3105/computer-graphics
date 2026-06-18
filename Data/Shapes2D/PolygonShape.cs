using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Data.Shapes2D
{
    public class PolygonShape : Shape2D
    {
        public PolygonShape(List<Point2D> vertices)
        { 
            this.Vertices = vertices;
        }
    }
}
