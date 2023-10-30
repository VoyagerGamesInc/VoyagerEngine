using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Rendering
{
    internal interface IUniformArgs
    {
        public string Name { get; set; }
    }
    internal class UniformArgs<T> : IUniformArgs
    {
        internal UniformArgs(string name, T value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public T Value { get; set; }
    }
}
