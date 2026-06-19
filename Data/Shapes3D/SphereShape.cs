using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;

namespace Project_CG_Paint.Data.Shapes3D
{
    public class SphereShape : Shape3D
    {
        public double Radius { get; set; }
        public int Stacks { get; set; }
        public int Slices { get; set; }
        public Point3D Center { get; set; }
        public SphereShape(double radius, int stacks, int slices, Point3D center)
        {
            Radius = radius;
            Stacks = stacks;
            Slices = slices;
            Center = center;
            this.Pivot = Center;
        }

        protected override void RebuildInspectionGeometry()
        {
            throw new NotImplementedException();
        }

        protected override Point3D CalculateDefaultPivot()
        {
            throw new NotImplementedException();
        }
    }
}
