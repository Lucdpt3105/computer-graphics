using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Animation
{
    public class LocalTransform2D
    {
        /// <summary>
        /// Vị trí của node trong hệ tọa độ local của parent.
        /// </summary>
        public Point2D Position { get; set; } = new Point2D(0, 0);

        public double RotationDegrees { get; set; } = 0.0;

        public Point2D Scale { get; set; } = new Point2D(1, 1);

        /// <summary>
        /// Pivot trong local space của node.
        /// </summary>
        public Point2D Pivot { get; set; } = new Point2D(0, 0);
    }
}
