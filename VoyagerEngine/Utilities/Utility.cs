using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Utilities
{
    internal static class Utility
    {
        internal static void FromColor(this ref Vector4 vector, Color color)
        {
            vector.X = color.R / 256f;
            vector.Y = color.G / 256f;
            vector.Z = color.B / 256f;
            vector.W = color.A / 256f;
        }
        public static T GetLast<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }
        public static T GetFirst<T>(this List<T> list)
        {
            return list[0];
        }
    }
}
