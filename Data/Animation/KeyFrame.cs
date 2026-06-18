using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Animation
{
    public class KeyFrame<T>
    {
        public double Time { get; set; }
        public T Value { get; set; }
    }
}
