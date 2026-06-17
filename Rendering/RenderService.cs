using System;
using System.Collections.Generic;
using System.Drawing;
using Project_CG_Paint.Algorithms.Rasterization.Shape2D;

namespace Project_CG_Paint.Rendering
{
    internal static class RenderService
    {
        public static Bitmap RenderColoredPoints(int width, int height, IEnumerable<ColoredPoint> points, Color backgroundColor)
        {
            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(backgroundColor);
            }

            if (points == null)
                return bitmap;

            foreach (var coloredPoint in points)
            {
                int x = (int)Math.Round(coloredPoint.Point.X);
                int y = (int)Math.Round(coloredPoint.Point.Y);

                if (x < 0 || x >= width || y < 0 || y >= height)
                    continue;

                bitmap.SetPixel(x, y, coloredPoint.Color);
            }

            return bitmap;
        }
    }
}
