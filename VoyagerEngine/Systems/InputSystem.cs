using VoyagerEngine.Attributes;
using VoyagerEngine.Framework;
using VoyagerEngine.Services;

namespace VoyagerEngine.Systems
{
    [RequiresService(typeof(InputService))]
    internal class InputSystem : ITickingSystem
    {
        public InputSystem()
        {
        }

        public void Tick(in EntityRegistry registry)
        {
        }
    }
}
