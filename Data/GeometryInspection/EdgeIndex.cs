using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.GeometryInspection
{
    public readonly struct EdgeIndex
    {
        public int StartIndex { get; }
        public int EndIndex { get; }

        public EdgeIndex(int startIndex, int endIndex)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        public override string ToString()
        {
            return $"{StartIndex} -> {EndIndex}";
        }
    }
}
