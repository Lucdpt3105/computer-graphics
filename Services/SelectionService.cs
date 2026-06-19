using System;
using System.Collections.Generic;
using System.Linq;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;

namespace Project_CG_Paint.Services
{
    internal class SelectionService
    {
        public GraphicObject HitTest(IReadOnlyList<GraphicObject> objects, Point2D worldPoint, RenderService renderService)
        {
            if (objects == null || renderService == null)
                return null;

            for (int i = objects.Count - 1; i >= 0; i--)
            {
                GraphicObject obj = objects[i];
                if (!obj.Metadata.IsVisible)
                    continue;

                BoundingBox2D bounds = renderService.GetObjectWorldBounds(obj);
                if (bounds == null)
                    continue;

                const double tolerance = 2.0;
                if (worldPoint.X >= bounds.MinX - tolerance &&
                    worldPoint.X <= bounds.MaxX + tolerance &&
                    worldPoint.Y >= bounds.MinY - tolerance &&
                    worldPoint.Y <= bounds.MaxY + tolerance)
                {
                    return obj;
                }
            }

            return null;
        }

        public void SelectOnly(IEnumerable<GraphicObject> objects, GraphicObject selected)
        {
            if (objects == null)
                return;

            foreach (GraphicObject obj in objects)
            {
                obj.Metadata.IsSelected = ReferenceEquals(obj, selected);
            }
        }
    }
}
