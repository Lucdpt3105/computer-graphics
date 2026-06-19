using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Objects
{
    public class CompositeEntity : GraphicObject
    {
        private List<GraphicObject> _children = new List<GraphicObject>();
        public IReadOnlyList<GraphicObject> Children => _children;
        public void AddChild(GraphicObject child)
        {
            if (child == null)
                throw new ArgumentNullException(nameof(child));

            if(ReferenceEquals(child, this)) 
                throw new InvalidOperationException("Cannot add self as child.");

            if (child.Parent != null)
                throw new InvalidOperationException("Child already has a parent.");

            _children.Add(child);
            child.SetParent(this);
        }
        public bool RemoveChild(GraphicObject child)
        {
            if (!_children.Remove(child))
                return false;

            child.SetParent(null);
            return true;
        }
    }
}
