using Silk.NET.Input;
using VoyagerEngine.Attributes;
using VoyagerEngine.Input;

namespace VoyagerEngine.Services
{
    [RequiresService(typeof(RenderService))]
    public class InputService : IService
    {
        private IInputContext inputContext;
        private Dictionary<IInputDevice, IInputController> deviceMap = new();
        public InputService()
        {
            inputContext = Engine.GetInputContext();
            Engine.GetInputContext().ConnectionChanged += OnDeviceChange;
            CollectDevices();
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
                if (!deviceMap.ContainsKey(device))
                    deviceMap.Add(device, CreateVoyagerInput_Device(device));
            }
            else
            {
                if (deviceMap.ContainsKey(device))
                    deviceMap.Remove(device);
            }
        }
        public bool RequestController(InputUtility.RequestTypes requestType, HashSet<InputListener> listeners)
        {
            foreach (KeyValuePair<IInputDevice, IInputController> kv in deviceMap)
            {
                bool inputMatch = false;
                switch (requestType)
                {
                    case InputUtility.RequestTypes.Any:
                        inputMatch = true;
                        break;
                    case InputUtility.RequestTypes.Keyboard:
                        inputMatch = kv.Value is Keyboard;
                        break;
                    case InputUtility.RequestTypes.Mouse:
                        inputMatch = kv.Value is Mouse;
                        break;
                    case InputUtility.RequestTypes.Gamepad:
                        inputMatch = kv.Value is Gamepad;
                        break;
                }
                if (inputMatch && kv.Value.FrameInputs.Count > 0)
                {
                }
            }
            return false;
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