using System.Drawing;

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
        public ToolType ToolType { get; set; } = ToolType.Draw;
        public DrawType DrawType { get; set; } = DrawType.Line;
        public DrawStyle DrawStyle { get; set; } = DrawStyle.Solid;
        public ReflectType ReflectType { get; set; } = ReflectType.ReflectOrigin;
        public FillColor FillColor { get; set; } = FillColor.None;
        public Color CurrentColor { get; set; } = Color.Black;
        public bool Is3DMode { get; set; }

        public bool CanDraw2D =>
            !Is3DMode &&
            ToolType == ToolType.Draw &&
            DrawType != DrawType.None &&
            DrawType != DrawType.Cube &&
            DrawType != DrawType.Sphere &&
            DrawType != DrawType.Pyramid &&
            DrawType != DrawType.Prism &&
            DrawType != DrawType.Cylinder;

        public bool CanDraw3D =>
            Is3DMode &&
            ToolType == ToolType.Draw &&
            (DrawType == DrawType.Cube ||
             DrawType == DrawType.Sphere ||
             DrawType == DrawType.Pyramid ||
             DrawType == DrawType.Prism ||
             DrawType == DrawType.Cylinder);
    }
}
