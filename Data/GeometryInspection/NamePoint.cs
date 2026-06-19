using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.GeometryInspection
{
    public class NamePoint<T>
    {
        public string Name { get; set; } = string.Empty;
        public T OriginalValue { get; set; }
        public T CurrentValue { get; set; }
        public NamePoint(string name, T originalValue)
            : this(name, originalValue, originalValue)
        {
        }

        public NamePoint(string name, T originalValue, T currentValue)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Point name cannot be empty.",
                    nameof(name));
            }

            Name = name;
            OriginalValue = originalValue;
            CurrentValue = currentValue;
        }

        public override string ToString()
        {
            return $"{Name}: {OriginalValue} -> {CurrentValue}";
        }
    }
}
