using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Core
{
    public interface IVoyagerGame
    {
        void Init();
        void Update(double deltaTime);
    }
}
