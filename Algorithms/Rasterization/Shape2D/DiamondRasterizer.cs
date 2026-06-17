using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Rasterization.Shape2D
{
    public static class DiamondRasterizer
    {
        /// <summary>
        /// Rasterize hình thoi từ tâm và 2 bán kính (theo trục X và Y)
        /// Hình thoi là tứ giác có 4 đỉnh: (cx, cy+ry), (cx+rx, cy), (cx, cy-ry), (cx-rx, cy)
        /// </summary>
        public static List<Point2D> RasterizePoints(Point2D center, double radiusX, double radiusY)
        {
            int cx = (int)Math.Round(center.X);
            int cy = (int)Math.Round(center.Y);
            int rx = (int)Math.Round(radiusX);
            int ry = (int)Math.Round(radiusY);

            // 4 đỉnh của hình thoi
            Point2D top = new Point2D(cx, cy - ry);
            Point2D right = new Point2D(cx + rx, cy);
            Point2D bottom = new Point2D(cx, cy + ry);
            Point2D left = new Point2D(cx - rx, cy);

            HashSet<Point2D> points = new HashSet<Point2D>();

            // Vẽ 4 cạnh
            foreach (var p in BresenhamLine.RasterizePoints(top, right)) points.Add(p);
            foreach (var p in BresenhamLine.RasterizePoints(right, bottom)) points.Add(p);
            foreach (var p in BresenhamLine.RasterizePoints(bottom, left)) points.Add(p);
            foreach (var p in BresenhamLine.RasterizePoints(left, top)) points.Add(p);

            // Tô bên trong = 2 tam giác
            foreach (var p in TriangleRasterizer.RasterizePoints(top, right, bottom)) points.Add(p);
            foreach (var p in TriangleRasterizer.RasterizePoints(top, bottom, left)) points.Add(p);

            return points.ToList();
        }
    }
}
