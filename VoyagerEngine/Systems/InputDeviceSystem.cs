using Silk.NET.Input;
using VoyagerEngine.Attributes;
using VoyagerEngine.Components;
using VoyagerEngine.Framework;
using VoyagerEngine.Services;
using VoyagerEngine.Utilities;

namespace VoyagerEngine.Systems
{
    [RequiresService(typeof(InputService))]
    public class InputDeviceSystem : ITickingSystem
    {
        InputService inputService;
        public InputDeviceSystem()
        {
            inputService = Engine.GetService<InputService>();
        }

        public void Tick(in EntityRegistry registry)
        {
        }
    }
}
