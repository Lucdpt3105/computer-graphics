using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;

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
            this.Pivot = CalculateDefaultPivot();
        }
        public CubeShape(double size, Point3D position)
        {
            Size = size;
            Position = position;
            this.Pivot = CalculateDefaultPivot();
        }
        public CubeShape(double size, Point3D position, Point3D pivot)
        {
            Size = size;
            Position = position;
            this.Pivot = pivot;
        }

        protected override void RebuildInspectionGeometry()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateInspectionGeometry()
        {
            throw new NotImplementedException();
        }

        protected override Point3D CalculateDefaultPivot()
        {
            return new Point3D(0,0, 0);
        }
    }
}
