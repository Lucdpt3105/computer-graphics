using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.CoreModel.Model
{
    public static class MatrixFactory
    {
        public static Matrix3x3 CreateTranslation2D(Point2D offset)
        {
            return new Matrix3x3(new double[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { offset.X, offset.Y, 1 }
            });
        }
        public static Matrix4x4 CreateTranslation3D(Point3D offset)
        {
            return new Matrix4x4(new double[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { offset.X, offset.Y, offset.Z, 1 }
            });
        }
        public static Matrix3x3 CreateScale2D(double scaleX, double scaleY)
        {
            return new Matrix3x3(new double[,]
            {
                { scaleX, 0, 0 },
                { 0, scaleY, 0 },
                { 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateScale3D(double scaleX, double scaleY, double scaleZ)
        {
            return new Matrix4x4(new double[,]
            {
                { scaleX, 0, 0, 0 },
                { 0, scaleY, 0, 0 },
                { 0, 0, scaleZ, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix3x3 CreateRotation2D(double angleDegrees)
        {
            double angleRadians = angleDegrees * Math.PI / 180.0;

            double cos = Math.Cos(angleRadians);
            double sin = Math.Sin(angleRadians);

            return new Matrix3x3(new double[,]
            {
                { cos, sin, 0 },
                { -sin, cos, 0 },
                { 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateRotation3DX(double angleDegrees)
        {
            double angleRadians = angleDegrees * Math.PI / 180.0;

            double cos = Math.Cos(angleRadians);
            double sin = Math.Sin(angleRadians);

            return new Matrix4x4(new double[,]
            {
                { 1, 0, 0, 0 },
                { 0, cos, sin, 0 },
                { 0, -sin, cos, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateRotation3DY(double angleDegrees)
        {
            double angleRadians = angleDegrees * Math.PI / 180.0;

            double cos = Math.Cos(angleRadians);
            double sin = Math.Sin(angleRadians);

            return new Matrix4x4(new double[,]
            {
                { cos, 0, -sin, 0 },
                { 0, 1, 0, 0 },
                { sin, 0, cos, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateRotation3DZ(double angleDegrees)
        {
            double angleRadians = angleDegrees * Math.PI / 180.0;

            double cos = Math.Cos(angleRadians);
            double sin = Math.Sin(angleRadians);

            return new Matrix4x4(new double[,]
            {
                { cos, sin, 0, 0 },
                { -sin, cos, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix3x3 CreateReflection2DOrigin()
        {
            return new Matrix3x3(new double[,]
            {
                { -1, 0, 0 },
                { 0, -1, 0 },
                { 0, 0, 1 }
            });
        }
        public static Matrix3x3 CreateReflection2DX()
        {
            return new Matrix3x3(new double[,]
            {
                { 1, 0, 0 },
                { 0, -1, 0 },
                { 0, 0, 1 }
            });
        }
        public static Matrix3x3 CreateReflection2DY()
        {
            return new Matrix3x3(new double[,]
            {
                { -1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateReflection3DOrigin()
        {
            return new Matrix4x4(new double[,]
            {
                { -1, 0, 0, 0 },
                { 0, -1, 0, 0 },
                { 0, 0, -1, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateReflection3DX()
        {
            return new Matrix4x4(new double[,]
            {
                { 1, 0, 0, 0 },
                { 0, -1, 0, 0 },
                { 0, 0, -1, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateReflection3DY()
        {
            return new Matrix4x4(new double[,]
            {
                { -1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, -1, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateReflection3DZ()
        {
            return new Matrix4x4(new double[,]
            {
                { -1, 0, 0, 0 },
                { 0, -1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateReflection3DXY()
        {
            return new Matrix4x4(new double[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, -1, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateReflection3DXZ()
        {
            return new Matrix4x4(new double[,]
            {
                { 1, 0, 0, 0 },
                { 0, -1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateReflection3DYZ()
        {
            return new Matrix4x4(new double[,]
            {
                { -1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            });
        }
        public static Matrix4x4 CreateCavalierProjection(double angleDegrees = 45)
        {
            double angleRadians = angleDegrees * Math.PI / 180.0;
            
            double cos = Math.Cos(angleRadians);
            double sin = Math.Sin(angleRadians);

            return new Matrix4x4(new double[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { cos, sin, 0, 0 },
                { 0, 0, 0, 1 }
            });
        }

        public static Matrix4x4 CreateCabinetProjection(double angleDegrees = 45)
        {
            double angleRadians = angleDegrees * Math.PI / 180.0;

            double cos = Math.Cos(angleRadians);
            double sin = Math.Sin(angleRadians);

            return new Matrix4x4(new double[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { cos / 2, sin / 2, 0, 0 },
                { 0, 0, 0, 1 }
            });
        }

        /// <summary>
        /// Phép biến dạng (Shearing) 3D
        /// Công thức: xq = x + hxy*y + hxz*z
        ///           yq = y + hyx*x + hyz*z
        ///           zq = z + hzx*x + hzy*y
        /// </summary>
        public static Matrix4x4 CreateShearing3D(double hxy, double hxz, double hyx, double hyz, double hzx, double hzy)
        {
            return new Matrix4x4(new double[,]
            {
                { 1, hyx, hzx, 0 },
                { hxy, 1, hzy, 0 },
                { hxz, hyz, 1, 0 },
                { 0, 0, 0, 1 }
            });
        }

        /// <summary>
        /// Phép biến dạng theo trục X (lấy Y và Z làm tham chiếu)
        /// xq = x + hxy*y + hxz*z, y không thay đổi, z không thay đổi
        /// </summary>
        public static Matrix4x4 CreateShearingX(double hxy, double hxz)
        {
            return new Matrix4x4(new double[,]
            {
                { 1, 0, 0, 0 },
                { hxy, 1, 0, 0 },
                { hxz, 0, 1, 0 },
                { 0, 0, 0, 1 }
            });
        }

        /// <summary>
        /// Phép biến dạng theo trục Y (lấy X và Z làm tham chiếu)
        /// x không thay đổi, yq = y + hyx*x + hyz*z, z không thay đổi
        /// </summary>
        public static Matrix4x4 CreateShearingY(double hyx, double hyz)
        {
            return new Matrix4x4(new double[,]
            {
                { 1, hyx, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, hyz, 1, 0 },
                { 0, 0, 0, 1 }
            });
        }

        /// <summary>
        /// Phép biến dạng theo trục Z (lấy X và Y làm tham chiếu)
        /// x không thay đổi, y không thay đổi, zq = z + hzx*x + hzy*y
        /// </summary>
        public static Matrix4x4 CreateShearingZ(double hzx, double hzy)
        {
            return new Matrix4x4(new double[,]
            {
                { 1, 0, hzx, 0 },
                { 0, 1, hzy, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            });
        }
    } 
}
