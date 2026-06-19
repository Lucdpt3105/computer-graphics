using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Services;

namespace Project_CG_Paint.Data.Shapes3D
{
    public class CubeShape : Shape3D
    {
        public double Size { get; set; }
        public Point3D Position { get; set; }
        public CubeShape(double size)
        {
            Size = size;
            Position = new Point3D(0, 0, 0);
            InitializeShapeData();
        }
        public CubeShape(double size, Point3D position)
        {
            Size = size;
            Position = position;
            InitializeShapeData();
        }
        public CubeShape(double size, Point3D position, Point3D pivot)
        {
            Size = size;
            Position = position;
            this.Pivot = pivot;
            RefreshInspectionGeometry();
        }

        protected override void RebuildInspectionGeometry()
        {
            Shape3DGeometryBuilder.PopulateInspectionGeometry(this);
        }


        protected override Point3D CalculateDefaultPivot()
        {
            return Position;
        }
    }
}
