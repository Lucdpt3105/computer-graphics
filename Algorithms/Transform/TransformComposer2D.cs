using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Transform
{
    public static class TransformComposer2D
    {
        public static Matrix3x3 BuildTranslationByOffset(Point2D offset)
        {
            return MatrixFactory.CreateTranslation2D(offset);
        }

        public static Matrix3x3 BuildScaleByPoint(Point2D pivot, double scaleX, double scaleY)
        {
            return null;
        }

        public static Matrix3x3 BuildRotationByPoint(Point2D pivot, double angleDegrees)
        {
            return null;
        }

        public static Matrix3x3 BuildReflectionByPoint(Point2D pivot)
        {
            return null;
        }

        public static Matrix3x3 BuildReflectionByLine(Point2D start, Point2D end)
        {
            return null;
        }
    }
}
