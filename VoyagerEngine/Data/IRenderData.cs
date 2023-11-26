using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Data
{
    public interface IRenderData
    {
        internal uint Program { get; set; }
        internal uint VAO { get; set; }
        internal uint VBO { get; set; }
        internal uint EBO { get; set; }
        internal float[] Buffer { get; }
    }
}
