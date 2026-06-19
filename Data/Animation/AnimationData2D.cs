using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Animation
{
    public class AnimationData2D
    {
        public bool IsEnabled { get; set; } = true;

        public AnimationChannel<Point2D> Position { get; }
            = new AnimationChannel<Point2D>(
                AnimationProperty2D.Position);

        public AnimationChannel<double> Rotation { get; }
            = new AnimationChannel<double>(
                AnimationProperty2D.Rotation);

        public AnimationChannel<Point2D> Scale { get; }
            = new AnimationChannel<Point2D>(
                AnimationProperty2D.Scale);
    }
}
