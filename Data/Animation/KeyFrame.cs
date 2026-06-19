using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Animation
{
    public class KeyFrame<T>
    {
        public double TimeSeconds { get; }
        public T Value { get; }
        public KeyFrame(double timeSeconds, T value)
        {
            if (timeSeconds < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(timeSeconds),
                    "Keyframe time cannot be negative.");
            }

            TimeSeconds = timeSeconds;
            Value = value;
        }
    }
}
