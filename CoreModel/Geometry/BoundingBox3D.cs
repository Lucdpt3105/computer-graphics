using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.CoreModel.Geometry
{
    public class BoundingBox3D
    {
        public double MinX { get; set; }
        public double MinY { get; set; }
        public double MinZ { get; set; }
        public double MaxX { get; set; }
        public double MaxY { get; set; }
        public double MaxZ { get; set; }
        public double Width => MaxX - MinX;
        public double Height => MaxY - MinY;
        public double Depth => MaxZ - MinZ;

        public BoundingBox3D(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
        {
            MinX = minX;
            MinY = minY;
            MinZ = minZ;
            MaxX = maxX;
            MaxY = maxY;
            MaxZ = maxZ;
        }
    }
}
