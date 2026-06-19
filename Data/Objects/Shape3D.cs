using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.GeometryInspection;
using Project_CG_Paint.Data.Shapes3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Objects
{
    public abstract class Shape3D : GraphicObject
    {
        public Point3D Pivot { get; set; } = new Point3D();
        public BoundingBox3D BoundingBox { get; set; } = new BoundingBox3D();
        public GeometryInspectionData3D InspectionGeometry { get; set; } = new GeometryInspectionData3D();
        public WireframeStyle WireframeStyle { get; set; } = new WireframeStyle();
        public IReadOnlyList<Point3D> OriginalVertices => InspectionGeometry.OriginalVertices;
        public IReadOnlyList<Point3D> CurrentVertices => InspectionGeometry.CurrentVertices;
        public IReadOnlyList<EdgeIndex> Edges => InspectionGeometry.Edges;
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
        protected abstract Point3D CalculateDefaultPivot();
        protected abstract void RebuildInspectionGeometry();
    }
}
