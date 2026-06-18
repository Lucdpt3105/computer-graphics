using Project_CG_Paint.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Scene
{
    public class Scene
    {
        public string Name { get; set; }
        public List<GraphicObject> RootObjects { get; set; } = new List<GraphicObject>();
    }
}
