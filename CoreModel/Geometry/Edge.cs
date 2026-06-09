using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.CoreModel.Geometry
{
    public class Edge<TPoint>
    {
        public TPoint Start { get; }
        public TPoint End { get; }

        public Edge(TPoint start, TPoint end)
        {
            Start = start;
            End = end;
        }
    }
}
