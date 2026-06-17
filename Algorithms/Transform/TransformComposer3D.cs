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
        /// Công thức: T(pivot) × Scale(sx, sy, sz) × T(-pivot)
        /// </summary>
        public static Matrix4x4 BuildScaleByPoint(Point3D pivot, double scaleX, double scaleY, double scaleZ)
        {
            // B1: Tịnh tiến pivot về gốc
            Matrix4x4 toOrigin = MatrixFactory.CreateTranslation3D(new Point3D(-pivot.X, -pivot.Y, -pivot.Z));
            // B2: Scale tại gốc
            Matrix4x4 scale = MatrixFactory.CreateScale3D(scaleX, scaleY, scaleZ);
            // B3: Tịnh tiến ngược lại vị trí ban đầu
            Matrix4x4 fromOrigin = MatrixFactory.CreateTranslation3D(pivot);

            // Kết hợp: point × T(pivot) × Scale × T(-pivot)
            return fromOrigin * scale * toOrigin;
        }

        /// <summary>
        /// Ma trận xoay (rotation) quanh trục X, quanh một tâm pivot bất kỳ
        /// Công thức: T(pivot) × RotationX(angle) × T(-pivot)
        /// </summary>
        public static Matrix4x4 BuildRotationByAxisXAroundPoint(Point3D pivot, double angleDegrees)
        {
            // B1: Tịnh tiến pivot về gốc
            Matrix4x4 toOrigin = MatrixFactory.CreateTranslation3D(new Point3D(-pivot.X, -pivot.Y, -pivot.Z));
            // B2: Xoay quanh trục X tại gốc
            Matrix4x4 rotationX = MatrixFactory.CreateRotation3DX(angleDegrees);
            // B3: Tịnh tiến ngược lại
            Matrix4x4 fromOrigin = MatrixFactory.CreateTranslation3D(pivot);

            // Kết hợp: point × T(pivot) × RotationX × T(-pivot)
            return fromOrigin * rotationX * toOrigin;
        }

        /// <summary>
        /// Ma trận xoay (rotation) quanh trục Y, quanh một tâm pivot bất kỳ
        /// Công thức: T(pivot) × RotationY(angle) × T(-pivot)
        /// </summary>
        public static Matrix4x4 BuildRotationByAxisYAroundPoint(Point3D pivot, double angleDegrees)
        {
            // B1: Tịnh tiến pivot về gốc
            Matrix4x4 toOrigin = MatrixFactory.CreateTranslation3D(new Point3D(-pivot.X, -pivot.Y, -pivot.Z));
            // B2: Xoay quanh trục Y tại gốc
            Matrix4x4 rotationY = MatrixFactory.CreateRotation3DY(angleDegrees);
            // B3: Tịnh tiến ngược lại
            Matrix4x4 fromOrigin = MatrixFactory.CreateTranslation3D(pivot);

            // Kết hợp: point × T(pivot) × RotationY × T(-pivot)
            return fromOrigin * rotationY * toOrigin;
        }

        /// <summary>
        /// Ma trận xoay (rotation) quanh trục Z, quanh một tâm pivot bất kỳ
        /// Công thức: T(pivot) × RotationZ(angle) × T(-pivot)
        /// </summary>
        public static Matrix4x4 BuildRotationByAxisZAroundPoint(Point3D pivot, double angleDegrees)
        {
            // B1: Tịnh tiến pivot về gốc
            Matrix4x4 toOrigin = MatrixFactory.CreateTranslation3D(new Point3D(-pivot.X, -pivot.Y, -pivot.Z));
            // B2: Xoay quanh trục Z tại gốc
            Matrix4x4 rotationZ = MatrixFactory.CreateRotation3DZ(angleDegrees);
            // B3: Tịnh tiến ngược lại
            Matrix4x4 fromOrigin = MatrixFactory.CreateTranslation3D(pivot);

            // Kết hợp: point × T(pivot) × RotationZ × T(-pivot)
            return fromOrigin * rotationZ * toOrigin;
        }

        /// <summary>
        /// Ma trận xoay (rotation) quanh trục X tại gốc tọa độ
        /// </summary>
        public static Matrix4x4 BuildRotationByAxisXAtOrigin(double angleDegrees)
        {
            return MatrixFactory.CreateRotation3DX(angleDegrees);
        }

        /// <summary>
        /// Ma trận xoay (rotation) quanh trục Y tại gốc tọa độ
        /// </summary>
        public static Matrix4x4 BuildRotationByAxisYAtOrigin(double angleDegrees)
        {
            return MatrixFactory.CreateRotation3DY(angleDegrees);
        }

        /// <summary>
        /// Ma trận xoay (rotation) quanh trục Z tại gốc tọa độ
        /// </summary>
        public static Matrix4x4 BuildRotationByAxisZAtOrigin(double angleDegrees)
        {
            return MatrixFactory.CreateRotation3DZ(angleDegrees);
        }

        /// <summary>
        /// Ma trận đối xứng (reflection) qua gốc tọa độ (Origin)
        /// Lật cả X, Y, Z
        /// </summary>
        public static Matrix4x4 BuildReflectionByOrigin()
        {
            return MatrixFactory.CreateReflection3DOrigin();
        }

        /// <summary>
        /// Ma trận đối xứng (reflection) qua trục Ox
        /// Lật Y và Z, giữ X
        /// </summary>
        public static Matrix4x4 BuildReflectionByAxisX()
        {
            return MatrixFactory.CreateReflection3DX();
        }

        /// <summary>
        /// Ma trận đối xứng (reflection) qua trục Oy
        /// Lật X và Z, giữ Y
        /// </summary>
        public static Matrix4x4 BuildReflectionByAxisY()
        {
            return MatrixFactory.CreateReflection3DY();
        }

        /// <summary>
        /// Ma trận đối xứng (reflection) qua trục Oz
        /// Lật X và Y, giữ Z
        /// </summary>
        public static Matrix4x4 BuildReflectionByAxisZ()
        {
            return MatrixFactory.CreateReflection3DZ();
        }

        /// <summary>
        /// Ma trận đối xứng (reflection) qua mặt phẳng Oxy
        /// Lật Z, giữ X và Y
        /// </summary>
        public static Matrix4x4 BuildReflectionByPlaneXY()
        {
            return MatrixFactory.CreateReflection3DXY();
        }

        /// <summary>
        /// Ma trận đối xứng (reflection) qua mặt phẳng Xz
        /// Lật Y, giữ X và Z
        /// </summary>
        public static Matrix4x4 BuildReflectionByPlaneXZ()
        {
            return MatrixFactory.CreateReflection3DXZ();
        }

        /// <summary>
        /// Ma trận đối xứng (reflection) qua mặt phẳng Yz
        /// Lật X, giữ Y và Z
        /// </summary>
        public static Matrix4x4 BuildReflectionByPlaneYZ()
        {
            return MatrixFactory.CreateReflection3DYZ();
        }

        /// <summary>
        /// Ma trận đối xứng (reflection) qua một điểm pivot bất kỳ
        /// = phép scale(-1, -1, -1) quanh pivot
        /// </summary>
        public static Matrix4x4 BuildReflectionByPoint(Point3D pivot)
        {
            // Đối xứng qua điểm = scale(-1,-1,-1) quanh điểm đó
            return BuildScaleByPoint(pivot, -1, -1, -1);
        }

        /// <summary>
        /// Phép biến dạng (Shearing) 3D - toàn bộ 6 tham số
        /// Công thức:
        /// xq = x + hxy*y + hxz*z
        /// yq = y + hyx*x + hyz*z
        /// zq = z + hzx*x + hzy*y
        /// </summary>
        public static Matrix4x4 BuildShearing(double hxy, double hxz, double hyx, double hyz, double hzx, double hzy)
        {
            return MatrixFactory.CreateShearing3D(hxy, hxz, hyx, hyz, hzx, hzy);
        }

        /// <summary>
        /// Phép biến dạng theo trục X
        /// Công thức: xq = x + hxy*y + hxz*z
        /// Y và Z không thay đổi
        /// </summary>
        public static Matrix4x4 BuildShearingAlongAxisX(double hxy, double hxz)
        {
            return MatrixFactory.CreateShearingX(hxy, hxz);
        }

        /// <summary>
        /// Phép biến dạng theo trục Y
        /// Công thức: yq = y + hyx*x + hyz*z
        /// X và Z không thay đổi
        /// </summary>
        public static Matrix4x4 BuildShearingAlongAxisY(double hyx, double hyz)
        {
            return MatrixFactory.CreateShearingY(hyx, hyz);
        }

        /// <summary>
        /// Phép biến dạng theo trục Z
        /// Công thức: zq = z + hzx*x + hzy*y
        /// X và Y không thay đổi
        /// </summary>
        public static Matrix4x4 BuildShearingAlongAxisZ(double hzx, double hzy)
        {
            return MatrixFactory.CreateShearingZ(hzx, hzy);
        }

        /// <summary>
        /// Phép biến dạng trong mặt phẳng XY (Z không thay đổi)
        /// Công thức:
        /// xq = x + hxy*y
        /// yq = y + hyx*x
        /// z không thay đổi
        /// </summary>
        public static Matrix4x4 BuildShearingInPlaneXY(double hxy, double hyx)
        {
            return MatrixFactory.CreateShearing3D(hxy, 0, hyx, 0, 0, 0);
        }

        /// <summary>
        /// Phép biến dạng trong mặt phẳng XZ (Y không thay đổi)
        /// Công thức:
        /// xq = x + hxz*z
        /// zq = z + hzx*x
        /// y không thay đổi
        /// </summary>
        public static Matrix4x4 BuildShearingInPlaneXZ(double hxz, double hzx)
        {
            return MatrixFactory.CreateShearing3D(0, hxz, 0, 0, hzx, 0);
        }

        /// <summary>
        /// Phép biến dạng trong mặt phẳng YZ (X không thay đổi)
        /// Công thức:
        /// yq = y + hyz*z
        /// zq = z + hzy*y
        /// x không thay đổi
        /// </summary>
        public static Matrix4x4 BuildShearingInPlaneYZ(double hyz, double hzy)
        {
            return MatrixFactory.CreateShearing3D(0, 0, 0, hyz, 0, hzy);
        }
    }
}
