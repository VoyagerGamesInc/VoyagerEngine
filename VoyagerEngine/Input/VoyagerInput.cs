using Silk.NET.Input;
using static VoyagerEngine.VoyagerUtility_Log;

namespace VoyagerEngine.Input
{
    public class VoyagerInput
    {
        private IInputContext _inputContext;
        private List<IVoyagerInput_Device> _idleDevices = new();
        private Dictionary<IInputDevice, IVoyagerInput_Device> _deviceMap = new();
        private List<IVoyagerInput_Listener> _requestingListeners = new();

        private Dictionary<string, IVoyagerInput_Listener> disconnectedDevices = new();

        internal void Init(IInputContext inputContext)
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
                IVoyagerInput_Device voyagerDevice = CreateVoyagerInput_Device(device);
                Print.Log($"Found Device: {device.Name}");
                _idleDevices.Add(voyagerDevice);
            }
        }

        internal void Update(double deltaTime)
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
                        if (device is IVoyagerInput_Controller controller)
                        {
                           TrySettingController(controller);
                        }
                    }

                    device.PostUpdate();
                }
            }
        }

        private void OnDeviceChange(IInputDevice device, bool change)
        {
            if (change)
            {
                Print.Log($"Added Device: {device.Name}");
            }
            else
            {
                Print.Log($"Removed Device: {device.Name}");

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
        private bool TryGetVoyagerInputDevice(IInputDevice device, out IVoyagerInput_Device voyagerDevice)
        {
            foreach (IVoyagerInput_Device storedDevice in _idleDevices)
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
        private void TrySettingController(IVoyagerInput_Controller controller)
        {
            if (controller.Listener == null && controller.WasUpdatedThisFrame)
            {
                IVoyagerInput_Listener listener = _requestingListeners.GetFirst();
                controller.SetListener(listener);
                Print.Log($"{LogMagenta(listener.Name)} was assigned [{LogYellow(controller.GetType().Name)}]");
                _requestingListeners.Remove(listener);

                foreach (var kv in disconnectedDevices)
                {
                    if (kv.Value == listener)
                    {
                        disconnectedDevices.Remove(kv.Key);
                        Print.Log($"{LogMagenta(listener.Name)} will stop waiting for [{LogYellow(controller.InputDevice.Name)}]");
                        break;
                    }
                }
            }

        }
        internal void SetReceiverFor<T>(IVoyagerInput_Listener listener) where T : IVoyagerInput_Device
        {
            foreach (IVoyagerInput_Device storedDevice in _idleDevices)
            {
                if (storedDevice.Listener == null && storedDevice is T)
                {
                    storedDevice.SetListener(listener);
                    Print.Log($"Set {LogMagenta(listener.Name)} as the listener for {LogYellow(storedDevice.GetType().Name)}.");
                    return;
                }
            }
        }
        internal void Request(IVoyagerInput_Listener listener)
        {
            if (!_requestingListeners.Contains(listener))
            {
                Print.Log($"{LogMagenta(listener.Name)} has requested a device");
                _requestingListeners.Add(listener);
            }
        }
        internal void CancelRequest(IVoyagerInput_Listener listener)
        {
            Print.Log($"{LogMagenta(listener.Name)} has cancelled a device request");
            _requestingListeners.Remove(listener);
        }
        private IVoyagerInput_Device CreateVoyagerInput_Device(IInputDevice device)
        {
            switch (device)
            {
                case IMouse mouse:
                    return new VoyagerInput_Mouse(mouse);
                case IKeyboard keyboard:
                    return new VoyagerInput_Keyboard(keyboard);
                case IGamepad gamepad:
                    return new VoyagerInput_Gamepad(gamepad);
            }
            throw new NotSupportedException("Unsupported device detected.");
        }
    }
}