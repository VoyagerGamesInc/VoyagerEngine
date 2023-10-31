using VoyagerEngine.Attributes;
using VoyagerEngine.Framework;
using VoyagerEngine.Services;

namespace VoyagerEngine.Systems
{
    [RequiresService(typeof(InputService))]
    public class InputAssignerSystem : ITickingSystem
    {
        InputService inputService;
        public InputAssignerSystem()
        {
            inputService = Engine.GetService<InputService>();
        }

        public void Tick(in EntityRegistry registry)
        {
        }
    }
}
