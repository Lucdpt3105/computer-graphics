using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.ShapeStyle
{
    public class ShapeStyles
    {
        public Color StrokeColor { get; set; } = Color.Black;
        public Color FillColor { get; set; } = Color.Transparent;
        public bool IsFilled { get; set; }
        public int StrokeWidth { get; set; } = 1;
        public LinePattern LinePattern { get; set; } = new LinePattern() 
        { 
            Name = "Solid", 
            Pattern = new List<int>() { 1 }
        };
    }
}
