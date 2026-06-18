using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Shapes2D
{
    public abstract class Shape2D : GraphicObject
    {
        public Point2D Pivot { get; set; } = new Point2D();
        public List<Point2D> Vertices { get; set; } = new List<Point2D>();
        public List<Edge<Point2D>> Edges { get; set; } = new List<Edge<Point2D>>();
    }
}
