using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.CoreModel.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.Data.GeometryInspection;

namespace Project_CG_Paint.Data.Objects
{
    public abstract class Shape2D : GraphicObject
    {
        public Point2D Pivot { get; set; } = new Point2D();
        public GeometryInspectionData2D InspectionGeometry { get; set; } = new GeometryInspectionData2D();
        public IReadOnlyList<Point2D> OriginalVertices => InspectionGeometry.OriginalVertices;
        public IReadOnlyList<Point2D> CurrentVertices => InspectionGeometry.CurrentVertices;
        public IReadOnlyList<EdgeIndex> Edges => InspectionGeometry.Edges;
        public abstract BoundingBox2D GetLocalBounds();
        protected void InitializeShapeData()
        {
            this.Pivot = CalculateDefaultPivot();
            RebuildInspectionGeometry();
        }
        public void RefreshInspectionGeometry()
        {
            RebuildInspectionGeometry();
        }
        public void ResetPivotToDefault()
        {
            Pivot = CalculateDefaultPivot();
        }
        public void ResetCurrentGeometry()
        {
            InspectionGeometry.ResetCurrentToOriginal();
        }
        protected abstract Point2D CalculateDefaultPivot();
        protected abstract void RebuildInspectionGeometry();
    }
}
