using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Animation
{
    public class AnimationChannel<T>
    {
        public string Name { get; set; } = "";
        public bool IsEnabled { get; set; } = true;
        public List<KeyFrame<T>> KeyFrames { get; set; } = new List<KeyFrame<T>>();
    }
}
