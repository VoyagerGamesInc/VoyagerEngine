using VoyagerEngine.Components;
using VoyagerEngine.Framework;
using VoyagerEngine.Input;
using VoyagerEngine.Services;

namespace VoyagerEngine.Systems
{
    public class DeviceAssignerSystem : ITickingSystem
    {
        InputService inputService;
        public DeviceAssignerSystem()
        {
            inputService = Engine.GetService<InputService>();
        }
        public void Tick(in EntityRegistry registry)
        {
            registry.View<RequestControllerComponent>(RequestController);
        }
        public void RequestController(Entity entity, RequestControllerComponent component)
        {
            ControllerRequest<Gamepad> request = new ControllerRequest<Gamepad>()
            {
                EntityId = entity.Id
            };
            inputService.RequestController(request);
        }
    }
}
