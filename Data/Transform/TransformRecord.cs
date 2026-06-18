using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Transform
{
    public class TransformRecord
    {
        public TransformType Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public object AppliedMatrix { get; set; } = null;
    }
}
