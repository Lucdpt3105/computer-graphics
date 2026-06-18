using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.Data.ShapeStyle;
using Project_CG_Paint.Data.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.Data.Animation;
using Project_CG_Paint.Data.Transform;

namespace Project_CG_Paint.Data.Objects
{
    public abstract class GraphicObject
    {
        public ObjectMetadata Metadata { get; set; } = new ObjectMetadata();
        public BoundingBox2D BoundingBox { get; set; } = new BoundingBox2D();
        public ShapeStyles Style { get; set; } = new ShapeStyles();
        public AnimationData AnimationData { get; set; } = new AnimationData();
        public Queue<TransformRecord> TransformHistory { get; set; } = new Queue<TransformRecord>();
    }
}
