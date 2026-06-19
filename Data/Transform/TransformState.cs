using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Transform
{
    public class TransformState
    {
        public Matrix3x3 CurrentMatrix3x3 { get; set; } = Matrix3x3.Identity();
    }
}
