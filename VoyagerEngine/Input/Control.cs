using Silk.NET.Input;
using System.Numerics;

namespace VoyagerEngine.Input
{
    public enum ControlName
    {
        None = 0,
        ButtonSouth,
        ButtonEast,
        ButtonWest,
        ButtonNorth,
        LeftShoulder,
        RightShoulder,
        Back,
        Start,
        Home,
        LeftStickButton,
        RightStickButton,
        DPadUp,
        DPadRight,
        DPadDown,
        DPadLeft,
        LeftStick,
        RightStick,
        LeftTrigger,
        RightTrigger
    }
    public class KeyControl : Control
    {
        public Key Key { get; set; }
        public KeyControl(IInputDevice device, ControlName name, Key key) : base(device, name)
        {
            Key = key;
        }
    }
    public class ButtonControl : Control
    {
        public bool Pressed { get; set; }
        public ButtonControl(IInputDevice device, ControlName name, bool pressed) : base(device, name)
        {
            Pressed = pressed;
        }
    }
    public class StickControl : Control
    {
        public Vector2 Vector { get; set; }
        public StickControl(IInputDevice device, ControlName name, float x, float y) : base(device, name)
        {
            Vector = new Vector2(x, y);
        }
    }

    public class TriggerControl : Control
    {
        public float Value { get; set; }
        public TriggerControl(IInputDevice device, ControlName name, float value) : base(device,name)
        {
            Value = value;
        }
    }

    public abstract class Control
    {
        public IInputDevice Device { get; set; }
        public ControlName Name { get; }
        public Control(IInputDevice device, ControlName name)
        {
            Device = device;
            Name = name;
        }
    }
}
