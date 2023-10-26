using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Systems
{
    internal interface IRenderSystem : ISystem
    {
        void Render(in EntityRegistry registry);
    }
}
