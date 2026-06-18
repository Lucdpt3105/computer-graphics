using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Metadata
{
    public class ObjectMetadata
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public bool IsVisible { get; set; } = true;
        public bool IsSelected { get; set; }
    }
}
