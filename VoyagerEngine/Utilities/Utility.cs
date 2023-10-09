using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Utilities
{
    internal static class Utility
    {
        internal static Vector4 NormalizeToVector4(this Color color)
        {
            return new Vector4(color.R / 256f,
                color.G / 256f,
                color.B / 256f,
                color.A / 256f);
        }
        internal static HashSet<T> ParamsToHashSet<T,TConstraint>(this T[] array) where TConstraint : IComponent
        {
            return new HashSet<T>(array).Where(obj => obj is IComponent).ToHashSet();
        }
    }
}
