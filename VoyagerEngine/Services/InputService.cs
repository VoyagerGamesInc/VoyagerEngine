using Silk.NET.Input;
using Silk.NET.Windowing;
using VoyagerEngine.Attributes;
using VoyagerEngine.Input;
using VoyagerEngine.Utilities;

namespace VoyagerEngine.Services
{
    [RequiresService(typeof(RenderService))]
    public class InputService : IService
    {
        private IInputContext inputContext;
        private Dictionary<IInputDevice, IInputController> deviceMap = new();
        private List<IInputListener> requests = new();
        public InputService()
        {
            Engine.GetInputContext().ConnectionChanged += OnDeviceChange;
        }
        internal void Tick(double deltaTime)
        {
            if (deviceMap.Count == 0)
            {
                CollectDevices();
            }
            foreach (var device in deviceMap)
            {
                if (!device.Key.IsConnected)
                {
                    device.Value.Update();

                    if (requests.Count > 0)
                    {
                        TrySettingDevice(device.Value);
                    }

                    device.Value.ProcessFrame();
                }
            }
        }

        private void CollectDevices()
        {
            List<IInputDevice> devices = new List<IInputDevice>();
            devices.AddRange(inputContext.Keyboards);
            devices.AddRange(inputContext.Mice);
            devices.AddRange(inputContext.Gamepads);
            devices.AddRange(inputContext.Joysticks);
            foreach (IInputDevice device in devices)
            {
                if (!deviceMap.ContainsKey(device))
                    deviceMap.Add(device, CreateVoyagerInput_Device(device));
            }
        }

        private void OnDeviceChange(IInputDevice device, bool added)
        {
            if (added)
            {
            }
            else // removed
            {
            }

        }
        private void TrySettingDevice(IInputController device)
        {
            if (device.Listener == null && device.WasUpdatedThisFrame)
            {
                IInputListener listener = requests[0];
                device.SetListener(listener);
                requests.Remove(listener);
            }
        }
        public void RequestController(IInputListener listener)
        {
            if (!requests.Contains(listener))
            {
                requests.Add(listener);
            }
        }
        internal void CancelRequest(IInputListener listener)
        {
            requests.Remove(listener);
        }
        private IInputController CreateVoyagerInput_Device(IInputDevice device)
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