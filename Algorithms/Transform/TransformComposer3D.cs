using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Transform
{
    public static class TransformComposer3D
    {
        /// <summary>
        /// Ma trận tịnh tiến (translation) trong 3D
        /// Công thức: x1 = x + trx, y1 = y + try, z1 = z + trz
        /// </summary>
        public static Matrix4x4 BuildTranslationByOffset(Point3D offset)
        {
            return MatrixFactory.CreateTranslation3D(offset);
        }

        /// <summary>
        /// Ma trận tỷ lệ (scale) quanh một tâm pivot bất kỳ trong 3D
        /// Công thức theo row-vector: T(-pivot) × Scale(sx, sy, sz) × T(pivot)
        /// </summary>
        public static Matrix4x4 BuildScaleByPoint(Point3D pivot, double scaleX, double scaleY, double scaleZ)
        {
            // B1: Tịnh tiến pivot về gốc
            Matrix4x4 toOrigin = MatrixFactory.CreateTranslation3D(new Point3D(-pivot.X, -pivot.Y, -pivot.Z));
            // B2: Scale tại gốc
            Matrix4x4 scale = MatrixFactory.CreateScale3D(scaleX, scaleY, scaleZ);
            // B3: Tịnh tiến ngược lại vị trí ban đầu
            Matrix4x4 fromOrigin = MatrixFactory.CreateTranslation3D(pivot);

            // Kết hợp: point × T(-pivot) × Scale × T(pivot)
            return toOrigin * scale * fromOrigin;
        }

        /// <summary>
        /// Ma trận xoay (rotation) quanh trục X, quanh một tâm pivot bất kỳ
        /// Công thức theo row-vector: T(-pivot) × RotationX(angle) × T(pivot)
        /// </summary>
        public static Matrix4x4 BuildRotationByAxisXAroundPoint(Point3D pivot, double angleDegrees)
        {
            // B1: Tịnh tiến pivot về gốc
            Matrix4x4 toOrigin = MatrixFactory.CreateTranslation3D(new Point3D(-pivot.X, -pivot.Y, -pivot.Z));
            // B2: Xoay quanh trục X tại gốc
            Matrix4x4 rotationX = MatrixFactory.CreateRotation3DX(angleDegrees);
            // B3: Tịnh tiến ngược lại
            Matrix4x4 fromOrigin = MatrixFactory.CreateTranslation3D(pivot);

            // Kết hợp: point × T(-pivot) × RotationX × T(pivot)
            return toOrigin * rotationX * fromOrigin;
        }

        /// <summary>
        /// Ma trận xoay (rotation) quanh trục Y, quanh một tâm pivot bất kỳ
        /// Công thức theo row-vector: T(-pivot) × RotationY(angle) × T(pivot)
        /// </summary>
        public static Matrix4x4 BuildRotationByAxisYAroundPoint(Point3D pivot, double angleDegrees)
        {
            // B1: Tịnh tiến pivot về gốc
            Matrix4x4 toOrigin = MatrixFactory.CreateTranslation3D(new Point3D(-pivot.X, -pivot.Y, -pivot.Z));
            // B2: Xoay quanh trục Y tại gốc
            Matrix4x4 rotationY = MatrixFactory.CreateRotation3DY(angleDegrees);
            // B3: Tịnh tiến ngược lại
            Matrix4x4 fromOrigin = MatrixFactory.CreateTranslation3D(pivot);

            // Kết hợp: point × T(-pivot) × RotationY × T(pivot)
            return toOrigin * rotationY * fromOrigin;
        }

        /// <summary>
        /// Ma trận xoay (rotation) quanh trục Z, quanh một tâm pivot bất kỳ
        /// Công thức theo row-vector: T(-pivot) × RotationZ(angle) × T(pivot)
        /// </summary>
        public static Matrix4x4 BuildRotationByAxisZAroundPoint(Point3D pivot, double angleDegrees)
        {
            // B1: Tịnh tiến pivot về gốc
            Matrix4x4 toOrigin = MatrixFactory.CreateTranslation3D(new Point3D(-pivot.X, -pivot.Y, -pivot.Z));
            // B2: Xoay quanh trục Z tại gốc
            Matrix4x4 rotationZ = MatrixFactory.CreateRotation3DZ(angleDegrees);
            // B3: Tịnh tiến ngược lại
            Matrix4x4 fromOrigin = MatrixFactory.CreateTranslation3D(pivot);

            // Kết hợp: point × T(-pivot) × RotationZ × T(pivot)
            return toOrigin * rotationZ * fromOrigin;
        }

        /// <summary>
        /// Phép quay điểm P3 quanh trục bất kỳ P1P2 một góc ∂ (độ).
        ///
        /// Thuật toán 7 bước (dùng MatrixFactory):
        ///   B1: T(-x1,-y1,-z1)              — tịnh tiến P1 về gốc O
        ///   B2: RotX(∂1)                    — quay quanh X để P1P2 nằm trên mặt phẳng Oxz
        ///   B3: RotY(∂2)                    — quay quanh Y để P1P2 trùng trục Ox
        ///   B4: RotX(∂)                     — quay P3 quanh Ox góc ∂
        ///   B5: RotY(-∂2)                   — đảo ngược B3
        ///   B6: RotX(-∂1)                   — đảo ngược B2
        ///   B7: T(x1,y1,z1)                 — đảo ngược B1
        ///
        /// Cách tính góc từ vector (dx, dy, dz):
        ///   d1  = sqrt(dy²+dz²)
        ///   ∂1  = atan2(dy, dz)   → đưa vector về mặt phẳng Oxz (triệt tiêu dy)
        ///   ∂2  = atan2(d1, dx)   → đưa vector về trùng trục Ox  (triệt tiêu dz)
        ///
        /// Công thức: P = P3 · T1 · Rx(∂1) · Ry(∂2) · Rx(∂) · Ry(-∂2) · Rx(-∂1) · T7
        /// </summary>
        public static Matrix4x4 BuildRotationAroundArbitraryAxis(Point3D p1, Point3D p2, double angleDegrees)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double dz = p2.Z - p1.Z;

            // d1: độ dài hình chiếu của vector lên mặt phẳng YZ
            double d1 = Math.Sqrt(dy * dy + dz * dz);

            // ∂1: góc quanh X để triệt tiêu thành phần Y → P1P2 nằm trên Oxz
            double angle1Deg = (d1 < 1e-10) ? 0.0 : Math.Atan2(dy, dz) * 180.0 / Math.PI;

            // L: độ dài toàn bộ vector P1P2
            double L = Math.Sqrt(dx * dx + dy * dy + dz * dz);

            // ∂2: góc quanh Y để triệt tiêu thành phần Z → P1P2 trùng trục Ox
            double angle2Deg = (L < 1e-10) ? 0.0 : Math.Atan2(d1, dx) * 180.0 / Math.PI;

            // B1: tịnh tiến P1 về gốc O
            Matrix4x4 t1 = MatrixFactory.CreateTranslation3D(new Point3D(-p1.X, -p1.Y, -p1.Z));

            // B2: quay quanh X góc ∂1
            Matrix4x4 rx1 = MatrixFactory.CreateRotation3DX(angle1Deg);

            // B3: quay quanh Y góc ∂2
            Matrix4x4 ry2 = MatrixFactory.CreateRotation3DY(angle2Deg);

            // B4: quay P3 quanh trục Ox góc ∂
            Matrix4x4 rxAngle = MatrixFactory.CreateRotation3DX(angleDegrees);

            // B5: nghịch đảo B3
            Matrix4x4 ry2Inv = MatrixFactory.CreateRotation3DY(-angle2Deg);

            // B6: nghịch đảo B2
            Matrix4x4 rx1Inv = MatrixFactory.CreateRotation3DX(-angle1Deg);

            // B7: tịnh tiến ngược về vị trí ban đầu
            Matrix4x4 t7 = MatrixFactory.CreateTranslation3D(p1);

            // P = P3 · T1 · Rx(∂1) · Ry(∂2) · Rx(∂) · Ry(-∂2) · Rx(-∂1) · T7
            return t1 * rx1 * ry2 * rxAngle * ry2Inv * rx1Inv * t7;
        }

        /// <summary>
        /// Phép đối xứng qua trục bất kỳ P1P2 trong 3D.
        ///
        /// Ý tưởng: đưa trục P1P2 về trùng Ox (giống quay trục bất kỳ),
        /// thực hiện đối xứng qua Ox (lật Y và Z) bằng CreateReflection3DX(),
        /// rồi đảo ngược các bước căn chỉnh.
        ///
        /// Công thức: P = P3 · T1 · Rx(∂1) · Ry(∂2) · ReflectX · Ry(-∂2) · Rx(-∂1) · T7
        /// </summary>
        public static Matrix4x4 BuildReflectionAroundArbitraryAxis(Point3D p1, Point3D p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double dz = p2.Z - p1.Z;

            double d1 = Math.Sqrt(dy * dy + dz * dz);
            double angle1Deg = (d1 < 1e-10) ? 0.0 : Math.Atan2(dy, dz) * 180.0 / Math.PI;

            double L = Math.Sqrt(dx * dx + dy * dy + dz * dz);
            double angle2Deg = (L < 1e-10) ? 0.0 : Math.Atan2(d1, dx) * 180.0 / Math.PI;

            // B1: tịnh tiến P1 về gốc O
            Matrix4x4 t1 = MatrixFactory.CreateTranslation3D(new Point3D(-p1.X, -p1.Y, -p1.Z));

            // B2: quay quanh X góc ∂1
            Matrix4x4 rx1 = MatrixFactory.CreateRotation3DX(angle1Deg);

            // B3: quay quanh Y góc ∂2
            Matrix4x4 ry2 = MatrixFactory.CreateRotation3DY(angle2Deg);

            // B4: đối xứng qua trục Ox (lật Y và Z) — dùng MatrixFactory
            Matrix4x4 reflectX = MatrixFactory.CreateReflection3DX();

            // B5: nghịch đảo B3
            Matrix4x4 ry2Inv = MatrixFactory.CreateRotation3DY(-angle2Deg);

            // B6: nghịch đảo B2
            Matrix4x4 rx1Inv = MatrixFactory.CreateRotation3DX(-angle1Deg);

            // B7: tịnh tiến ngược về vị trí ban đầu
            Matrix4x4 t7 = MatrixFactory.CreateTranslation3D(p1);

            // P = P3 · T1 · Rx(∂1) · Ry(∂2) · ReflectX · Ry(-∂2) · Rx(-∂1) · T7
            return t1 * rx1 * ry2 * reflectX * ry2Inv * rx1Inv * t7;
        }
    }
}
