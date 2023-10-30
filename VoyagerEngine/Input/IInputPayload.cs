using Silk.NET.Input;
using System.Numerics;

namespace VoyagerEngine.Input
{
    public enum InputStates
    {
        Hold,
        Pressed,
        Released
    }
    public interface IInputPayload
    {
    }
    public abstract class InputPayload : IInputPayload
    {
        protected string Name { get; private set; }
        public InputStates State { get; private set; }
        public InputPayload(string name, InputStates state)
        {
            Name = name;
            State = state;
        }
    }
    public class InputPayloadStick : InputPayload
    {
        public new string Name => base.Name;
        public Vector2 Value { get; private set; }
        public InputPayloadStick(string name, Vector2 value) : base(name, InputStates.Hold)
        {
            Value = value;
        }
        public override string ToString()
        {
            return $"Stick: {Name} - ({Value.X},{Value.Y}) - {State}";
        }
    }
    public class InputPayloadFloat : InputPayload
    {
        public new string Name => base.Name;
        public float Value { get; private set; }
        public InputPayloadFloat(string name, float value) : base(name, InputStates.Hold)
        {
            Value = value;
        }
        public override string ToString()
        {
            return $"Float: {Name} - ({Value}) - {State}";
        }
    }
    public class InputPayloadKey : InputPayload
    {
        public Key Key { get; private set; }
        public InputPayloadKey(Key key, bool pressed) : base(key.ToString(), pressed ? InputStates.Pressed : InputStates.Released)
        {
            Key = key;
        }
        public override string ToString()
        {
            return $"Key: {Key} - {State}";
        }
    }
    public class InputPayloadButton : InputPayload
    {
        public ButtonName ButtonName { get; private set; }
        public InputPayloadButton(ButtonName name, bool pressed) : base(name.ToString(), pressed ? InputStates.Pressed : InputStates.Released)
        {
            ButtonName = name;
        }
        public override string ToString()
        {
            return $"Button: {Name} - {State}";
        }
    }
    public class InputPayloadChar : InputPayload
    {
        public new string Name => base.Name;
        public InputPayloadChar(char name) : base(name.ToString(), InputStates.Pressed)
        {
        }
        public override string ToString()
        {
            return $"Char: {Name} - {State}";
        }
    }
}
