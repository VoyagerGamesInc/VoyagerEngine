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
            using (new ScopedPerf("InputDeviceSystem.Tick"))
            {
                registry.Exclude<DeviceOwnerComponent>().View<InputListenerComponent>(RequestController);
                registry.Exclude<DeviceOwnerComponent>().View<InputListenerComponent>(RequestController);
            }
        }
        private void RequestController(Entity entity, InputListenerComponent requestDeviceComponent)
        {
            if(inputService.RequestController(requestDeviceComponent.RequestType, requestDeviceComponent.Listeners))
            {
                entity.RemoveComponent<InputListenerComponent>();
            }
        }
    }
}
