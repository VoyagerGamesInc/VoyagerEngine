using Silk.NET.Input;
using Silk.NET.OpenAL;
using VoyagerEngine.Framework;
using VoyagerEngine.Input;

namespace VoyagerEngine.Services
{
    public sealed class InputService : IService
    {
        private IInputContext inputContext;
        private HashSet<Controller> idleControllers = new();
        private HashSet<IInputDevice> foundDevices = new();
        private Queue<IControllerRequest> requestQueue = new();
        public InputService()
        {
            inputContext = Engine.GetInputContext();
            Engine.GetInputContext().ConnectionChanged += OnDeviceChange;
            CollectDevices();
        }
        internal void RequestController(IControllerRequest request)
        {
            if(!requestQueue.Contains(request))
                requestQueue.Enqueue(request);
        }
        private void CollectDevices()
        {
            List<IInputDevice> devices = new List<IInputDevice>();
            devices.AddRange(inputContext.Keyboards);
            devices.AddRange(inputContext.Mice);
            devices.AddRange(inputContext.Gamepads);
            foreach (IInputDevice device in devices)
            {
                TryAddDevice(device);
            }
        }
        private void TryAddDevice(IInputDevice device)
        {

            if (!foundDevices.Contains(device))
            {
                foundDevices.Add(device);
                Controller controller = CreateController(device);
                QueueControllerForRequests(controller);
            }
        }
        private void QueueControllerForRequests(Controller controller)
        {
            controller.AssignedEntity.Reset();
            controller.OnAnyInput += OnAnyInput;
            idleControllers.Add(controller);
        }
        private void OnAnyInput(Controller controller)
        {
            if(requestQueue.TryPeek(out IControllerRequest request)) {
                if(request.ControllerType == controller.Device.GetType())
                {
                    controller.AssignedEntity = request.EntityId;
                }
            }
        }
        private void UnregisterDevice(IInputDevice device)
        {

        }

        private void OnDeviceChange(IInputDevice device, bool added)
        {
            if (added)
            {
                TryAddDevice(device);
            }
            else
            {
                UnregisterDevice(device);
            }
        }
        private Controller CreateController(IInputDevice device)
        {
            switch (device)
            {
                case IMouse mouse:
                    return new Mouse(mouse);
                case IKeyboard keyboard:
                    return new Keyboard(keyboard);
                case IGamepad gamepad:
                    return new Gamepad(gamepad);
            }
            throw new NotSupportedException("Unsupported device detected.");
        }
    }
}