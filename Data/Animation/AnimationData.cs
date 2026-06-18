using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Animation
{
    public class AnimationData
    {
        public AnimationChannel<Point2D> PositionChannel { get; set; } = new AnimationChannel<Point2D>();
        public AnimationChannel<double> RotationChannel { get; set; } = new AnimationChannel<double>();
        public AnimationChannel<Point2D> ScaleChannel { get; set; } = new AnimationChannel<Point2D>();
    }
}
