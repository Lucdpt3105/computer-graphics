using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Controllers.Paint
{
    public enum ToolType
    {
        None,
        Selection,
        Draw,
        ShapeStyle,
        Fill,
        Reflect,
        Eraser
    }
    public enum ReflectType
    {
        ReflectOrigin,
        ReflectX,
        ReflectY
    }
    public enum DrawType
    {
        None,
        Point,
        Line,
        Rectangle,
        Circle,
        Polygon,
        Ellipse,
        Triangle,
        Parallelogram,
        Diamond,
        Cube,
        Sphere,
        Pyramid,
        Prism,
        Cylinder,
    }
    public enum DrawStyle
    {
        None,
        Solid,
        Dashed,
        Dotted,
        DashDot,
        DashDotDot,
    }
    public enum FillColor
    {
        None,
        Black,
        White,
        Red,
        Green,
        Yellow,
        Orange,
        Blue,
        Purple,
        Gray,
    }

    public class ToolAndDrawContext
    {
    }
}
