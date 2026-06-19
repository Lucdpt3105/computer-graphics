using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Styles
{
    public class WireframeStyle
    {
        public LinePattern VisibleEdgeStyle { get; set; } = new LinePattern()
        {
            Name = "Solid",
            Pattern = new List<int>() { 1 }
        };
        public LinePattern HiddenEdgeStyle { get; set; } = new LinePattern()
        {
            Name = "Dashed",
            Pattern = new List<int>() { 5, 5 }
        };
    }
}
