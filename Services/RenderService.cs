using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Project_CG_Paint.Algorithms.Projection;
using Project_CG_Paint.Algorithms.Rasterization.Shape2D;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Data.Shapes2D;

namespace Project_CG_Paint.Services
{
    internal class RenderService
    {
        public const int PixelsPerUnit = 5;
        private const double CavalierAngleDegrees = 225.0;

        public Bitmap Render(Size canvasSize, IReadOnlyList<GraphicObject> objects, bool is3DMode)
        {
            Bitmap bitmap = new Bitmap(Math.Max(1, canvasSize.Width), Math.Max(1, canvasSize.Height));

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                DrawGrid(graphics, canvasSize, is3DMode);
                DrawAxes(graphics, canvasSize, is3DMode);
            }

            if (objects != null)
            {
                foreach (GraphicObject obj in objects.Where(o => o.Metadata.IsVisible))
                {
                    RenderObject(bitmap, obj);
                }

                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    foreach (GraphicObject obj in objects.Where(o => o.Metadata.IsVisible && o.Metadata.IsSelected))
                    {
                        DrawSelectionOverlay(graphics, canvasSize, obj);
                    }
                }
            }

            return bitmap;
        }

        public Point2D ScreenToWorld(Point screenPoint, Size canvasSize)
        {
            double originX = canvasSize.Width / 2.0;
            double originY = canvasSize.Height / 2.0;
            return new Point2D(
                (screenPoint.X - originX) / PixelsPerUnit,
                (originY - screenPoint.Y) / PixelsPerUnit);
        }

        public Point WorldToScreen(Point2D worldPoint, Size canvasSize)
        {
            double originX = canvasSize.Width / 2.0;
            double originY = canvasSize.Height / 2.0;
            return new Point(
                (int)Math.Round(originX + worldPoint.X * PixelsPerUnit),
                (int)Math.Round(originY - worldPoint.Y * PixelsPerUnit));
        }

        public BoundingBox2D GetObjectWorldBounds(GraphicObject obj)
        {
            List<Point2D> points = GetObjectWorldPoints(obj, includeFill: true);
            if (points.Count == 0)
                return null;

            return new BoundingBox2D(
                points.Min(p => p.X),
                points.Min(p => p.Y),
                points.Max(p => p.X),
                points.Max(p => p.Y));
        }

        private void RenderObject(Bitmap bitmap, GraphicObject obj)
        {
            if (obj is Shape2D shape2D)
            {
                if (shape2D.Style.IsFilled && shape2D.Style.FillColor != Color.Transparent)
                {
                    DrawPoints(bitmap, GetFillWorldPoints(shape2D), shape2D.Style.FillColor);
                }

                DrawPoints(bitmap, GetStrokeWorldPoints(shape2D), shape2D.Style.StrokeColor);
                return;
            }

            if (obj is Shape3D shape3D)
            {
                DrawWireframe3D(bitmap, shape3D);
            }
        }

        private void DrawPoints(Bitmap bitmap, IEnumerable<Point2D> worldPoints, Color color)
        {
            if (color == Color.Transparent)
                return;

            foreach (Point2D worldPoint in worldPoints)
            {
                Point screenPoint = WorldToScreen(worldPoint, bitmap.Size);
                DrawWorldCell(bitmap, screenPoint, color);
            }
        }

        private static void DrawWorldCell(Bitmap bitmap, Point center, Color color)
        {
            int size = Math.Max(1, PixelsPerUnit);
            int half = size / 2;

            for (int y = center.Y - half; y < center.Y - half + size; y++)
            {
                if (y < 0 || y >= bitmap.Height)
                    continue;

                for (int x = center.X - half; x < center.X - half + size; x++)
                {
                    if (x >= 0 && x < bitmap.Width)
                        bitmap.SetPixel(x, y, color);
                }
            }
        }

        private void DrawGrid(Graphics graphics, Size canvasSize, bool is3DMode)
        {
            DrawOxyGrid(graphics, canvasSize);

            if (is3DMode)
                DrawOxyzDepthGrid(graphics, canvasSize);
        }

        private void DrawOxyGrid(Graphics graphics, Size canvasSize)
        {
            Point origin = WorldToScreen(new Point2D(0, 0), canvasSize);
            int minXUnit = (int)Math.Floor((0 - origin.X) / (double)PixelsPerUnit);
            int maxXUnit = (int)Math.Ceiling((canvasSize.Width - origin.X) / (double)PixelsPerUnit);
            int minYUnit = (int)Math.Floor((origin.Y - canvasSize.Height) / (double)PixelsPerUnit);
            int maxYUnit = (int)Math.Ceiling(origin.Y / (double)PixelsPerUnit);

            using (Pen gridPen = new Pen(Color.FromArgb(238, 238, 238), 1))
            using (Pen majorGridPen = new Pen(Color.FromArgb(220, 220, 220), 1))
            {
                for (int xUnit = minXUnit; xUnit <= maxXUnit; xUnit++)
                {
                    int x = origin.X + xUnit * PixelsPerUnit;
                    Pen pen = xUnit % 5 == 0 ? majorGridPen : gridPen;
                    graphics.DrawLine(pen, x, 0, x, canvasSize.Height);
                }

                for (int yUnit = minYUnit; yUnit <= maxYUnit; yUnit++)
                {
                    int y = origin.Y - yUnit * PixelsPerUnit;
                    Pen pen = yUnit % 5 == 0 ? majorGridPen : gridPen;
                    graphics.DrawLine(pen, 0, y, canvasSize.Width, y);
                }
            }
        }

        private void DrawOxyzDepthGrid(Graphics graphics, Size canvasSize)
        {
            int halfExtent = (int)Math.Ceiling(Math.Max(canvasSize.Width, canvasSize.Height) / (double)PixelsPerUnit / 2.0);
            int depth = Math.Max(1, halfExtent);

            using (Pen gridPen = new Pen(Color.FromArgb(232, 238, 250), 1))
            using (Pen majorGridPen = new Pen(Color.FromArgb(207, 219, 241), 1))
            {
                // Đường song song trục Z trên mặt sàn XZ (y=0)
                for (int x = -halfExtent; x <= halfExtent; x++)
                {
                    Pen pen = x % 5 == 0 ? majorGridPen : gridPen;
                    DrawProjected3DLine(graphics, canvasSize, pen, new Point3D(x, 0, 0), new Point3D(x, 0, depth));
                }

                // Đường song song trục X trên mặt sàn XZ (y=0)
                for (int z = 0; z <= depth; z++)
                {
                    Pen pen = z % 5 == 0 ? majorGridPen : gridPen;
                    DrawProjected3DLine(graphics, canvasSize, pen, new Point3D(-halfExtent, 0, z), new Point3D(halfExtent, 0, z));
                }
            }
        }

        private void DrawProjected3DLine(Graphics graphics, Size canvasSize, Pen pen, Point3D start, Point3D end)
        {
            graphics.DrawLine(pen, Project3DToScreen(start, canvasSize), Project3DToScreen(end, canvasSize));
        }

        private Point Project3DToScreen(Point3D point, Size canvasSize)
        {
            return WorldToScreen(Project3D(point), canvasSize);
        }

        private void DrawAxes(Graphics graphics, Size canvasSize, bool is3DMode)
        {
            Point origin = WorldToScreen(new Point2D(0, 0), canvasSize);
            using (Pen axisPen = new Pen(Color.FromArgb(90, 90, 90), 1))
            using (Pen xPen = new Pen(Color.FromArgb(200, 30, 30), 1))
            using (Pen yPen = new Pen(Color.FromArgb(30, 150, 60), 1))
            using (Pen zPen = new Pen(Color.FromArgb(40, 90, 190), 1))
            using (Font font = new Font("Segoe UI", 8))
            using (Brush brush = new SolidBrush(Color.FromArgb(70, 70, 70)))
            {
                graphics.DrawLine(axisPen, 0, origin.Y, canvasSize.Width, origin.Y);
                graphics.DrawLine(axisPen, origin.X, 0, origin.X, canvasSize.Height);
                graphics.DrawLine(xPen, origin.X, origin.Y, canvasSize.Width, origin.Y);
                graphics.DrawLine(yPen, origin.X, origin.Y, origin.X, 0);
                graphics.DrawString("O", font, brush, origin.X + 4, origin.Y + 4);
                graphics.DrawString("X", font, brush, canvasSize.Width - 18, origin.Y + 4);
                graphics.DrawString("Y", font, brush, origin.X + 4, 4);

                if (is3DMode)
                {
                    Point zEnd = WorldToScreen(new Point2D(-Math.Min(canvasSize.Width, canvasSize.Height) / (PixelsPerUnit * 3.0), -Math.Min(canvasSize.Width, canvasSize.Height) / (PixelsPerUnit * 3.0)), canvasSize);
                    graphics.DrawLine(zPen, origin, zEnd);
                    graphics.DrawString("Z", font, brush, zEnd.X + 4, zEnd.Y + 4);
                }
            }
        }

        private void DrawSelectionOverlay(Graphics graphics, Size canvasSize, GraphicObject obj)
        {
            BoundingBox2D bounds = GetObjectWorldBounds(obj);
            if (bounds == null)
                return;

            Point topLeft = WorldToScreen(new Point2D(bounds.MinX, bounds.MaxY), canvasSize);
            Point bottomRight = WorldToScreen(new Point2D(bounds.MaxX, bounds.MinY), canvasSize);
            Rectangle rect = Rectangle.FromLTRB(
                Math.Min(topLeft.X, bottomRight.X),
                Math.Min(topLeft.Y, bottomRight.Y),
                Math.Max(topLeft.X, bottomRight.X),
                Math.Max(topLeft.Y, bottomRight.Y));

            using (Pen pen = new Pen(Color.DeepSkyBlue, 1))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                graphics.DrawRectangle(pen, rect);
            }
        }

        private List<Point2D> GetObjectWorldPoints(GraphicObject obj, bool includeFill)
        {
            if (obj is Shape2D shape2D)
            {
                List<Point2D> points = GetStrokeWorldPoints(shape2D);
                if (includeFill)
                    points.AddRange(GetFillWorldPoints(shape2D));
                return points;
            }

            if (obj is Shape3D shape3D)
                return GetWireframeWorldPoints(shape3D);

            return new List<Point2D>();
        }

        private List<Point2D> GetStrokeWorldPoints(Shape2D shape)
        {
            IEnumerable<Point2D> localPoints;

            if (shape is LineShape line)
                localPoints = BresenhamLine.RasterizePoints(line.Start, line.End);
            else if (shape is RectangleShape rectangle)
                localPoints = RectangleRasterizer.RasterizePoints(rectangle.TopLeft, rectangle.BottomRight);
            else if (shape is CircleShape circle)
                localPoints = MidpointCircle.RasterizePoints(circle.Center, circle.Radius);
            else if (shape is EllipseShape ellipse)
                localPoints = MidpointEllipse.RasterizePoints(ellipse.Center, ellipse.RadiusX, ellipse.RadiusY);
            else if (shape is TriangleShape triangle)
                localPoints = TriangleRasterizer.RasterizePoints(triangle.VertexA, triangle.VertexB, triangle.VertexC);
            else if (shape is DiamondShape diamond)
                localPoints = DiamondRasterizer.RasterizePoints(diamond.Center, diamond.RadiusX, diamond.RadiusY);
            else if (shape is ParallelogramShape parallelogram)
                localPoints = ParallelogramRasterizer.RasterizePoints(parallelogram.VertexA, parallelogram.VertexB, parallelogram.VertexC);
            else if (shape is PolygonShape polygon)
                localPoints = PolygonRasterizer.RasterizePoints(polygon.InputVertices.ToList());
            else
                localPoints = new List<Point2D>();

            List<Point2D> patternedPoints = MethodApplyPattern.ApplyPattern(
                localPoints.ToList(),
                shape.Style.LinePattern?.Pattern?.ToArray());

            return TransformPoints(patternedPoints, shape);
        }

        private List<Point2D> GetFillWorldPoints(Shape2D shape)
        {
            if (!shape.Style.IsFilled)
                return new List<Point2D>();

            IEnumerable<Point2D> localPoints;

            if (shape is RectangleShape rectangle)
                localPoints = Shape2DFill.FillRectangle((int)Math.Round(rectangle.TopLeft.X), (int)Math.Round(rectangle.TopLeft.Y), (int)Math.Round(rectangle.BottomRight.X), (int)Math.Round(rectangle.BottomRight.Y));
            else if (shape is CircleShape circle)
                localPoints = Shape2DFill.FillCircle(circle.Center, circle.Radius);
            else if (shape is EllipseShape ellipse)
                localPoints = Shape2DFill.FillEllipse(ellipse.Center, ellipse.RadiusX, ellipse.RadiusY);
            else if (shape is TriangleShape triangle)
                localPoints = Shape2DFill.FillTriangle(triangle.VertexA, triangle.VertexB, triangle.VertexC);
            else if (shape is DiamondShape || shape is ParallelogramShape || shape is PolygonShape)
                localPoints = Shape2DFill.FillPolygon(shape.OriginalVertices.ToList());
            else
                localPoints = new List<Point2D>();

            return TransformPoints(localPoints, shape);
        }

        private static List<Point2D> TransformPoints(IEnumerable<Point2D> points, Shape2D shape)
        {
            Matrix3x3 matrix = shape.CurrentMatrix.CurrentMatrix3x3;
            return points.Select(p => matrix.Transform(p)).ToList();
        }

        private List<Point2D> GetWireframeWorldPoints(Shape3D shape)
        {
            var model = Shape3DGeometryBuilder.BuildModel(shape);
            List<Point2D> result = new List<Point2D>();

            foreach (Edge<Point2D> edge in CavalierProjection.ProjectEdges(model.Edges, CavalierAngleDegrees))
            {
                result.AddRange(BresenhamLine.RasterizePoints(edge.Start, edge.End));
            }

            return result;
        }

        private void DrawWireframe3D(Bitmap bitmap, Shape3D shape)
        {
            var model = Shape3DGeometryBuilder.BuildModel(shape);

            foreach (Edge<Point2D> edge in CavalierProjection.ProjectEdges(model.Edges, CavalierAngleDegrees))
            {
                DrawPoints(bitmap, BresenhamLine.RasterizePoints(edge.Start, edge.End), shape.Style.StrokeColor);
            }
        }

        private static Point2D Project3D(Point3D point)
        {
            return CavalierProjection.ProjectPoint(point, CavalierAngleDegrees);
        }
    }
}
