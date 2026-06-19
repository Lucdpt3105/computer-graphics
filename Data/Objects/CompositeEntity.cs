using Project_CG_Paint.Data.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Objects
{
    public class CompositeEntity : SceneNode2D
    {
        private List<SceneNode2D> _children = new List<SceneNode2D>();
        public IReadOnlyList<SceneNode2D> Children => _children;
        public void AddChild(SceneNode2D child)
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
        public bool RemoveChild(SceneNode2D child)
        {
            if (!_children.Remove(child))
                return false;

            child.SetParent(null);
            return true;
        }
    }
}
