using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Core
{
    public abstract class Game : IGameServicesHandler
    {
        /// <summary>
        /// Register Services Here
        /// </summary>
        public virtual void OnServicesInit(GameServices gameServices)
        {
        }
        /// <summary>
        /// Register Services Here
        /// </summary>
        public abstract void StartGame();
    }
}
