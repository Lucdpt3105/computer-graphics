using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.CoreModel.Model
{
    public class Matrix4x4
    {
        private readonly double[,] _data;
        public double this[int row, int col]
        {
            get => _data[row, col];
            set => _data[row, col] = value;
        }
        public Matrix4x4()
        {
            _data = new double[4, 4];
        }
        public Matrix4x4(double[,] data)
        {
            try
            {
                if (data.GetLength(0) != 4 || data.GetLength(1) != 4)
                {
                    throw new ArgumentException("Data must be a 4x4 array.");
                }
                _data = data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static Matrix4x4 Identity()
        {
            return new Matrix4x4(new double[,]
            {
                {1, 0, 0, 0},
                {0, 1, 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            });
        }
        public static Matrix4x4 operator *(Matrix4x4 left, Matrix4x4 right)
        {
            Matrix4x4 result = new Matrix4x4();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    double sum = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        sum += left[row, k] * right[k, col];
                    }
                    result[row, col] = sum;
                }
            }
            return result;
        }
        public Point3D Transform(Point3D point)
        {
            double x = point.X * this[0, 0] + point.Y * this[1, 0] + point.Z * this[2, 0] + this[3, 0];

            double y = point.X * this[0, 1] + point.Y * this[1, 1] + point.Z * this[2, 1] + this[3, 1];

            double z = point.X * this[0, 2] + point.Y * this[1, 2] + point.Z * this[2, 2] + this[3, 2];

            double w = point.X * this[0, 3] + point.Y * this[1, 3] + point.Z * this[2, 3] + this[3, 3];

            if (Math.Abs(w) > 1e-10)
            {
                x /= w;
                y /= w;
                z /= w;
            }

            return new Point3D(x, y, z);
        }
        public List<Point3D> Transform(IEnumerable<Point3D> points)
        {
            return points.Select(Transform).ToList();
        }
        public Point3D Transform(double x, double y, double z)
        {
            return Transform(new Point3D(x, y, z));
        }
        public override string ToString()
        {
            return $"Matrix4x4 ({GetHashCode()})";
        }
        public string ToPrettyString()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < 4; row++)
            {
                sb.Append("[ ");

                for (int col = 0; col < 4; col++)
                {
                    sb.Append($"{this[row, col],10:F2}");
                }

                sb.AppendLine(" ]");
            }

            return sb.ToString();
        }
        public double[,] ToArray()
        {
            return (double[,])_data.Clone();
        }
        public List<double> ToList()
        {
            List<double> result = new List<double>();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    result.Add(this[row, col]);
                }
            }

            return result;
        }
        public string ToSingleLineString(int precision = 2)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < 4; row++)
            {
                sb.Append("[");

                for (int col = 0; col < 4; col++)
                {
                    sb.Append(
                        this[row, col].ToString($"F{precision}"));

                    if (col < 3)
                        sb.Append(", ");
                }

                sb.Append("]");

                if (row < 3)
                    sb.Append(" "); 
            }

            return sb.ToString();
        }
    }
}
