using VoyagerEngine.Attributes;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Input
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
