using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Transform
{
    public class TransformRecord
    {
        public TransformType Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public Dictionary<string, double> Parameters { get; set; } = new Dictionary<string, double>();
        public Matrix4x4 AppliedMatrix4x4 { get; set; } = null;
        public Matrix3x3 AppliedMatrix3x3 { get; set; } = null;
    }
}
