using Silk.NET.Input;
namespace VoyagerEngine.Input
{
    public enum InputStates
    {
        Hold,
        Pressed,
        Released
    }
    public interface IInput_Value
    {
    }
    public abstract class Input_Value : IInput_Value
    {
        public string Name { get; private set; }
        public InputStates State { get; private set; }
        public Input_Value(string name, InputStates state)
        {
            Name = name;
            State = state;
        }
    }
    public class InputValue_Stick : Input_Value
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public InputValue_Stick(string name, float x, float y) : base(name, InputStates.Hold)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return $"Stick: {Name} - ({X},{Y}) - {State}";
        }
    }
    public class InputValue_Float : Input_Value
    {
        public float Value { get; private set; }
        public InputValue_Float(string name, float value) : base(name, InputStates.Hold)
        {
            Value = value;
        }
        public override string ToString()
        {
            return $"Float: {Name} - ({Value}) - {State}";
        }
    }
    public class InputValue_Key : Input_Value
    {
        public Key Key { get; private set; }
        public InputValue_Key(Key key, bool pressed) : base(key.ToString(), pressed ? InputStates.Pressed : InputStates.Released)
        {
            Key = key;
        }
        public override string ToString()
        {
            return $"Key: {Key} - {State}";
        }
    }
    public class InputValue_Button : Input_Value
    {
        public InputValue_Button(string name, bool pressed) : base(name, pressed ? InputStates.Pressed : InputStates.Released)
        {
        }
        public override string ToString()
        {
            return $"Button: {Name} - {State}";
        }
    }
    public class InputValue_Char : Input_Value
    {
        public InputValue_Char(char name) : base(name.ToString(), InputStates.Pressed)
        {
        }
        public override string ToString()
        {
            return $"Char: {Name} - {State}";
        }
    }
}
