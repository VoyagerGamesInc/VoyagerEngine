using System.Numerics;
namespace VoyagerEngine.Input
{
    public enum InputStates
    {
        Hold,
        Pressed,
        Released
    }
    public interface IVoyagerInput_Value
    {
        string Name { get; set; }
        InputStates State { get; set; }
    }
    public abstract class VoyagerInput_Value : IVoyagerInput_Value
    {
        public string Name { get; set; }
        public InputStates State { get; set; }
        public VoyagerInput_Value(string name, InputStates state)
        {
            Name = name;
            State = state;
        }
    }
    public class InputValue_XY : VoyagerInput_Value
    {
        public float X;
        public float Y;
        public InputValue_XY(string name, float x, float y) : base(name, InputStates.Hold)
        {
            X = x;
            Y = y;
        }
    }
    public class InputValue_Float : VoyagerInput_Value
    {
        public float Value;
        public InputValue_Float(string name, float value) : base(name, InputStates.Hold)
        {
            Value = value;
        }
    }
    public class InputValue_Button : VoyagerInput_Value
    {
        public InputValue_Button(string name, bool pressed) : base(name, pressed ? InputStates.Pressed : InputStates.Released)
        {
        }
    }
    public class InputValue_Char : VoyagerInput_Value
    {
        public InputValue_Char(char name) : base(name.ToString(), InputStates.Pressed)
        {
        }
    }
}
