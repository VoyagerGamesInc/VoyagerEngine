using VoyagerEngine.Attributes;
using VoyagerEngine.Core;

namespace VoyagerEngine.Input
{
    [ServiceDependency(typeof(InputService))]
    internal class InputSystem : ITickingSystem
    {
        public void Init()
        {
        }

        public void Tick(in EntityRegistry registry)
        {
        }
    }
}
