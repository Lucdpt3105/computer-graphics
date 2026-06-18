using Project_CG_Paint.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.CompositeEntity
{
    public class CompositeEntity : GraphicObject
    {
        List<GraphicObject> _children = new List<GraphicObject>();
    }
}
