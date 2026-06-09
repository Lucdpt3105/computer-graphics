using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.CoreModel.Model
{
    public class Matrix3x3
    {
        private readonly double[,] _data;
        public double this[int row, int col]
        {
            get => _data[row, col];
            set => _data[row, col] = value;
        }

        public Matrix3x3()
        {
            _data = new double[3,3];
        }
        public Matrix3x3(double[,] data)
        {
            try
            {
                if (data.GetLength(0) != 3 || data.GetLength(1) != 3)
                {
                    throw new ArgumentException("Data must be a 3x3 array.");
                }

                _data = data;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static Matrix3x3 Identity()
        {
            return new Matrix3x3(new double[,]
            {
                {1, 0, 0},
                {0, 1, 0},
                {0, 0, 1}
            });
        }
        public static Matrix3x3 operator *(Matrix3x3 left, Matrix3x3 right)
        {
            Matrix3x3 result = new Matrix3x3();

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    double sum = 0;

                    for (int k = 0; k < 3; k++)
                    {
                        sum += left[row, k] * right[k, col];
                    }

                    result[row, col] = sum;
                }
            }

            return result;
        }
        public Point2D Transform(Point2D point)
        {
            double x = point.X * this[0, 0] + point.Y * this[1, 0] + this[2, 0];

            double y = point.X * this[0, 1] + point.Y * this[1, 1] + this[2, 1];

            double w = point.X * this[0, 2] + point.Y * this[1, 2] + this[2, 2];

            if (Math.Abs(w) > 1e-10)
            {
                x /= w;
                y /= w;
            }

            return new Point2D(x, y);
        }
        public List<Point2D> Transform(IEnumerable<Point2D> points)
        {
            return points.Select(Transform).ToList();
        }
        public Point2D Transform(double x, double y)
        {
            return Transform(new Point2D(x, y));
        }
        public override string ToString()
        {
            return $"Matrix3x3 ({GetHashCode()})";
        }
        public string ToPrettyString()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < 3; row++)
            {
                sb.Append("[ ");

                for (int col = 0; col < 3; col++)
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

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    result.Add(this[row, col]);
                }
            }

            return result;
        }
        public string ToSingleLineString(int precision = 2)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < 3; row++)
            {
                sb.Append("[");

                for (int col = 0; col < 3; col++)
                {
                    sb.Append(
                        this[row, col].ToString($"F{precision}"));

                    if (col < 2)
                        sb.Append(", ");
                }

                sb.Append("]");

                if (row < 2)
                    sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}
