using Silk.NET.Input;
using VoyagerEngine.Utilities;

namespace VoyagerEngine.Input
{
    public class InputManager
    {
        private IInputContext _inputContext;
        private List<IInput_Device> _idleDevices = new();
        private Dictionary<IInputDevice, IInput_Device> _deviceMap = new();
        private List<IInput_Listener> _requestingListeners = new();

        private Dictionary<string, IInput_Listener> disconnectedDevices = new();

        internal void OnLoad(IInputContext inputContext)
        {
            _inputContext = inputContext;
            CollectDevices();
            _inputContext.ConnectionChanged += OnDeviceChange;
        }
        private void CollectDevices()
        {
            List<IInputDevice> devices = new List<IInputDevice>();
            devices.AddRange(_inputContext.Keyboards);
            devices.AddRange(_inputContext.Mice);
            devices.AddRange(_inputContext.Gamepads);
            devices.AddRange(_inputContext.Joysticks);
            foreach (IInputDevice device in devices)
            {
                IInput_Device voyagerDevice = CreateVoyagerInput_Device(device);
                _idleDevices.Add(voyagerDevice);
            }
        }

        internal void OnUpdate(double deltaTime)
        {
            if (_idleDevices.Count == 0)
            {
                CollectDevices();
            }
            foreach (var device in _idleDevices)
            {
                if (!device.Disconnected)
                {
                    device.Update();

                    if (_requestingListeners.Count > 0)
                    {
                        if (device is IInput_Controller controller)
                        {
                           TrySettingController(controller);
                        }
                    }

                    device.ProcessFrame();
                }
            }
        }

        private void OnDeviceChange(IInputDevice device, bool change)
        {
            if (change)
            {
            }
            else
            {

            }

            /* # REMOVED
            if (TryGetVoyagerInputDevice(device, out IVoyagerInput_Device voyagerDevice))
            {
                VoyagerInput_Listener listener = voyagerDevice.Listener;
                disconnectedDevices.Add(device.Name, listener);
                Debug.WriteLine($"{LogMagenta(listener.Name)} will try reconnecting to assigned [{LogYellow(device.Name)}]");
                RequestController(listener);
                voyagerDevice.WasRemoved();
                _devices.Remove(voyagerDevice);
            }
            */
            /* # ADDED
            IVoyagerInput_Device newDevice = CreateVoyagerInput_Device(device);
            _devices.Add(newDevice);
            if (disconnectedDevices.TryGetValue(device.Name, out VoyagerInput_Listener listener))
            {
                Debug.WriteLine($"{LogYellow(device.Name)} was reassigned to [{LogMagenta(listener.Name)}]");
                newDevice.SetListener(listener);
                _requestingListeners.Remove(listener);
                disconnectedDevices.Remove(device.Name);
            }
            */
        }
        private bool TryGetVoyagerInputDevice(IInputDevice device, out IInput_Device voyagerDevice)
        {
            foreach (IInput_Device storedDevice in _idleDevices)
            {
                if (storedDevice.InputDevice == device)
                {
                    voyagerDevice = storedDevice;
                    return true;
                }
            }
            voyagerDevice = null;
            return false;
        }
        private void TrySettingController(IInput_Controller controller)
        {
            if (controller.Listener == null && controller.WasUpdatedThisFrame)
            {
                IInput_Listener listener = _requestingListeners.GetFirst();
                controller.SetListener(listener);
                _requestingListeners.Remove(listener);

                foreach (var kv in disconnectedDevices)
                {
                    if (kv.Value == listener)
                    {
                        disconnectedDevices.Remove(kv.Key);
                        break;
                    }
                }
            }

        }
        internal void SetReceiverFor<T>(IInput_Listener listener) where T : IInput_Device
        {
            foreach (IInput_Device storedDevice in _idleDevices)
            {
                if (storedDevice.Listener == null && storedDevice is T)
                {
                    storedDevice.SetListener(listener);
                    return;
                }
            }
        }
        internal void Request(IInput_Listener listener)
        {
            if (!_requestingListeners.Contains(listener))
            {
                _requestingListeners.Add(listener);
            }
        }
        internal void CancelRequest(IInput_Listener listener)
        {
            _requestingListeners.Remove(listener);
        }
        private IInput_Device CreateVoyagerInput_Device(IInputDevice device)
        {
            switch (device)
            {
                case IMouse mouse:
                    return new Input_Mouse(mouse);
                case IKeyboard keyboard:
                    return new Input_Keyboard(keyboard);
                case IGamepad gamepad:
                    return new Input_Gamepad(gamepad);
            }
            throw new NotSupportedException("Unsupported device detected.");
        }
    }
}