using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Objects
{
    public class CompositeEntity : GraphicObject
    {
        public List<GraphicObject> _children = new List<GraphicObject>();
        public IReadOnlyList<GraphicObject> Children => _children;
        public void AddChild(GraphicObject child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (child.Parent != null) throw new InvalidOperationException("Child already has a parent.");
            _children.Add(child);
            child.Parent = this;
        }
        public void RemoveChild(GraphicObject child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (_children.Remove(child))
            {
                child.Parent = null;
            }
        }
    }
}
