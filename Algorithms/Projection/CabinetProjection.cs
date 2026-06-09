using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Projection
{
    public static class CabinetProjection
    {
        public static Point2D ProjectPoint(Point3D point, double alpha)
        {
            Matrix4x4 cabinet = MatrixFactory.CreateCabinetProjection(alpha);

            Point3D projectedPoint = cabinet.Transform(point);
            return new Point2D(projectedPoint.X, projectedPoint.Y);
        }
    }
}
