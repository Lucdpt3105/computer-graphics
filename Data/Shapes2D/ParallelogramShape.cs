using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.Data.Objects;

namespace Project_CG_Paint.Data.Shapes2D
{
    public class ParallelogramShape : Shape2D
    {
        public Point2D VertexA { get; set; }
        public Point2D VertexB { get; set; }
        public Point2D VertexC { get; set; }
        public ParallelogramShape(Point2D vertexA, Point2D vertexB, Point2D vertexC)
        {
            VertexA = vertexA;
            VertexB = vertexB;
            VertexC = vertexC;
            this.Pivot = new Point2D((vertexA.X + vertexC.X) / 2.0, (vertexA.Y + vertexC.Y) / 2.0);
        }
    }
}