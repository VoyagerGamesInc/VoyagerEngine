using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.GameMode
{
    public interface IGameMode
    {
        Task Init();
        Task CleanUp();
        void Update(double deltaTime);
    }
}
