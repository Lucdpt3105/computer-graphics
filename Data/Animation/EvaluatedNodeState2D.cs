using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Animation
{
    public class EvaluatedNodeState2D
    {
        public Guid ObjectId { get; set; }
        public double TimeSeconds { get; set; }
        public LocalTransform2D EvaluatedTransform { get; set; }
        public Matrix3x3 LocalMatrix { get; set; }
        public Matrix3x3 WorldMatrix { get; set; }
        public List<Point2D> EvaluatedVertices { get; } = new List<Point2D>();
    }
}
