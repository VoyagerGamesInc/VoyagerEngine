using Silk.NET.Input;
using System.Numerics;
using static VoyagerEngine.Input.InputUtils;

namespace VoyagerEngine.Input
{
    internal class Gamepad : DeviceController<IGamepad,ControlHandler>
    {
        internal Dictionary<ControlName, Control> ControlEvents { get; private set; } = new();
        internal Gamepad(IGamepad device) : base(device)
        {
            device.Deadzone = new Deadzone(0.1f, DeadzoneMethod.Traditional);
            device.ButtonDown += Device_ButtonDown;
            device.ButtonUp += Device_ButtonUp;
            device.ThumbstickMoved += Device_ThumbstickMoved;
            device.TriggerMoved += Device_TriggerMoved;
        }

        private void Device_TriggerMoved(IGamepad device, Trigger trigger)
        {
            ControlName name = trigger.GetControlName();
            if (ControlEvents.TryGetValue(name, out Control control))
            {
                if(control is TriggerControl triggerControl)
                {
                    triggerControl.Value = trigger.Position;
                }
            }
            else
            {
                ControlEvents.Add(name, new TriggerControl(device, name, trigger.Position));
            }
        }

        private void Device_ThumbstickMoved(IGamepad device, Thumbstick stick)
        {
            ControlName name = stick.GetControlName();
            if (ControlEvents.TryGetValue(name, out Control control))
            {
                if (control is StickControl stickControl)
                {
                    stickControl.Vector = new Vector2(stick.X,stick.Y);
                }
            }
            else
            {
                ControlEvents.Add(name, new StickControl(device, name, stick.X, stick.Y));
            }
        }

        private void Device_ButtonUp(IGamepad device, Button button)
        {
            //Debug.Log($"Device_ButtonUp: {device.Name} : {button.Index} : {button.Name} : {FromButtonName(button.Name)}");
            ControlName name = button.GetControlName();
            if (!ControlEvents.ContainsKey(name))
            {
                ControlEvents.Add(name, new ButtonControl(device, name, false));
            }
        }

        private void Device_ButtonDown(IGamepad device, Button button)
        {
            OnAnyInput?.Invoke(this);
            ControlName name = button.GetControlName();
            if (!ControlEvents.ContainsKey(name))
            {
                ControlEvents.Add(name, new ButtonControl(device, name, true));
            }
        }
    }
}
