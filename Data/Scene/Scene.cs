using Project_CG_Paint.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Scene
{
    public class Scene
    {
        public string Name { get; set; }
        public CompositeEntity Root { get; set; } = new CompositeEntity();
        public double DurationSeconds { get; set; } = 5.0;
        public int FramesPerSecond { get; set; } = 30;
    }
}
