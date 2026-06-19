using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Project_CG_Paint.CoreModel.Geometry
{
    public class Edge<TPoint>
    {
        public string Name {  get; set; }
        public TPoint Start { get; }
        public TPoint End { get; }
        public Edge(TPoint start, TPoint end)
            : this(string.Empty, start, end)
        {
        }
        public Edge(string name, TPoint start, TPoint end)
        {
            Name = name;
            Start = start;
            End = end;
        }

        public static double AngleBetweenOx(Edge<Point2D> edge)
        {
            double dx = edge.End.X - edge.Start.X;
            double dy = edge.End.Y - edge.Start.Y;

            double angleDegrees = Math.Atan2(dy, dx) * (180.0 / Math.PI);
            return angleDegrees;
        }
        public string BuildEdge()
        {
            return $"Edge {Name}: Start({Start.ToString()}), End({End.ToString()})";
        }
    }
}
