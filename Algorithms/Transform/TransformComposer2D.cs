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
        /// <summary>
        /// Ma trận tịnh tiến (translation)
        /// </summary>
        public static Matrix3x3 BuildTranslationByOffset(Point2D offset)
        {
            return MatrixFactory.CreateTranslation2D(offset);
        }

        /// <summary>
        /// Ma trận tỷ lệ (scale) quanh một tâm pivot bất kỳ
        /// Công thức: T(pivot) × Scale(sx, sy) × T(-pivot)
        /// </summary>
        public static Matrix3x3 BuildScaleByPoint(Point2D pivot, double scaleX, double scaleY)
        {
            // B1: Tịnh tiến pivot về gốc
            Matrix3x3 toOrigin = MatrixFactory.CreateTranslation2D(new Point2D(-pivot.X, -pivot.Y));
            // B2: Scale tại gốc
            Matrix3x3 scale = MatrixFactory.CreateScale2D(scaleX, scaleY);
            // B3: Tịnh tiến ngược lại vị trí ban đầu
            Matrix3x3 fromOrigin = MatrixFactory.CreateTranslation2D(pivot);

            // Kết hợp: point × T(pivot) × Scale × T(-pivot)
            return fromOrigin * scale * toOrigin;
        }

        /// <summary>
        /// Ma trận xoay (rotation) quanh một tâm pivot bất kỳ
        /// Công thức: T(pivot) × Rotation(angle) × T(-pivot)
        /// </summary>
        public static Matrix3x3 BuildRotationByPoint(Point2D pivot, double angleDegrees)
        {
            // B1: Tịnh tiến pivot về gốc
            Matrix3x3 toOrigin = MatrixFactory.CreateTranslation2D(new Point2D(-pivot.X, -pivot.Y));
            // B2: Xoay tại gốc
            Matrix3x3 rotation = MatrixFactory.CreateRotation2D(angleDegrees);
            // B3: Tịnh tiến ngược lại
            Matrix3x3 fromOrigin = MatrixFactory.CreateTranslation2D(pivot);

            // Kết hợp: point × T(pivot) × Rotation × T(-pivot)
            return fromOrigin * rotation * toOrigin;
        }

        /// <summary>
        /// Ma trận đối xứng (reflection) qua một điểm pivot
        /// = phép scale(-1, -1) quanh pivot (lật cả X và Y)
        /// Công thức: T(pivot) × Scale(-1, -1) × T(-pivot)
        /// </summary>
        public static Matrix3x3 BuildReflectionByPoint(Point2D pivot)
        {
            // Đối xứng qua điểm = scale(-1,-1) quanh điểm đó
            return BuildScaleByPoint(pivot, -1, -1);
        }

        /// <summary>
        /// Ma trận đối xứng (reflection) qua một đường thẳng (start → end)
        /// Công thức: T(start) × R(angle) × RefX × R(-angle) × T(-start)
        /// </summary>
        public static Matrix3x3 BuildReflectionByLine(Point2D start, Point2D end)
        {
            // Tính góc của đường thẳng so với trục X
            double dx = end.X - start.X;
            double dy = end.Y - start.Y;
            double angleDegrees = Math.Atan2(dy, dx) * 180.0 / Math.PI;

            // B1: Tịnh tiến start về gốc
            Matrix3x3 toOrigin = MatrixFactory.CreateTranslation2D(new Point2D(-start.X, -start.Y));
            // B2: Xoay đường thẳng về trùng trục X
            Matrix3x3 alignToX = MatrixFactory.CreateRotation2D(-angleDegrees);
            // B3: Đối xứng qua trục X (lật Y)
            Matrix3x3 reflectX = MatrixFactory.CreateReflection2DX();
            // B4: Xoay ngược lại
            Matrix3x3 unrotate = MatrixFactory.CreateRotation2D(angleDegrees);
            // B5: Tịnh tiến ngược lại
            Matrix3x3 fromOrigin = MatrixFactory.CreateTranslation2D(start);

            // Kết hợp: point × T(start) × R(angle) × RefX × R(-angle) × T(-start)
            return fromOrigin * unrotate * reflectX * alignToX * toOrigin;
        }
    }
}
