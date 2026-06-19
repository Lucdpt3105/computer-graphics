using Project_CG_Paint.Data.Animation;
using Project_CG_Paint.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Scene
{
    public abstract class SceneNode2D : GraphicObject
    {
        /// <summary>
        /// Transform gốc tương đối với parent.
        /// </summary>
        public LocalTransform2D BaseLocalTransform { get; set; } = new LocalTransform2D();

        /// <summary>
        /// Null nghĩa là node này không có animation riêng.
        /// Node vẫn bị ảnh hưởng bởi animation của parent.
        /// </summary>
        public AnimationData2D AnimationData { get; set; }
    }
}
