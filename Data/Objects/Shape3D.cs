using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Shapes3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Objects
{
    public abstract class Shape3D : GraphicObject
    {
        public List<Point3D> Vertices { get; set; } = new List<Point3D>();
        public List<Edge<Point3D>> Edges { get; set; } = new List<Edge<Point3D>>();
        public Point3D Pivot { get; set; } = new Point3D();
        public WireframeStyle WireframeStyle { get; set; } = new WireframeStyle();
    }
}
