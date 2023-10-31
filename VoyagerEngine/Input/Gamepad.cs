using Silk.NET.Input;
using System.Numerics;

namespace VoyagerEngine.Input
{
    internal class Gamepad : InputDevice<IGamepad>
    {
        private Dictionary<ControlName, KeyControl> controls = new();
        internal Gamepad(IGamepad device) : base(device)
        {
            device.Deadzone = new Deadzone(0.1f, DeadzoneMethod.Traditional);
            device.ButtonDown += Device_ButtonDown;
            device.ButtonUp += Device_ButtonUp;
            device.ThumbstickMoved += Device_ThumbstickMoved;
            device.TriggerMoved += Device_TriggerMoved;
        }
        protected override void Collect()
        {
        }

        private void Device_TriggerMoved(IGamepad device, Trigger trigger)
        {
            ControlName name = trigger.Index == 0 ? ControlName.LeftTrigger : ControlName.RightTrigger;
            if (controls.TryGetValue(name, out KeyControl control))
            {
                if(control is TriggerControl triggerControl)
                {
                    triggerControl.Value = trigger.Position;
                }
            }
            else
            {
                controls.Add(name, new TriggerControl(device, name, trigger.Position));
            }
        }

        private void Device_ThumbstickMoved(IGamepad device, Thumbstick stick)
        {
            ControlName name = stick.Index == 0 ? ControlName.LeftStick : ControlName.RightStick;
            if (controls.TryGetValue(name, out KeyControl control))
            {
                if (control is StickControl stickControl)
                {
                    stickControl.Vector = new Vector2(stick.X,stick.Y);
                }
            }
            else
            {
                controls.Add(name, new StickControl(device, name, stick.X, stick.Y));
            }
        }

        private void Device_ButtonUp(IGamepad device, Button button)
        {
            //Debug.Log($"Device_ButtonUp: {device.Name} : {button.Index} : {button.Name} : {FromButtonName(button.Name)}");
            ControlName name = FromButtonName(button.Name);
            if (!controls.ContainsKey(name))
            {
                controls.Add(name, new ButtonControl(device, name, false));
            }
        }

        private void Device_ButtonDown(IGamepad device, Button button)
        {
            ControlName name = FromButtonName(button.Name);
            if (!controls.ContainsKey(name))
            {
                controls.Add(name, new ButtonControl(device, name, true));
            }
        }
        private static ControlName FromButtonName(ButtonName buttonName)
        {
            return (ControlName)((int)buttonName + 1);
        }
    }
}
