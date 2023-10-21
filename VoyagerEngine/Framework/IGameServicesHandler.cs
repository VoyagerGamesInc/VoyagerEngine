using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Framework
{
    internal interface IGameServicesHandler
    {
        void RegisterServices(in GameServices gameServices);
    }
}
