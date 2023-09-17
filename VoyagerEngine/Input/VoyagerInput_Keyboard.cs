using Silk.NET.Input;
namespace VoyagerEngine.Input
{
    internal class VoyagerInput_Keyboard : VoyagerInput_Device<IKeyboard>, IVoyagerInput_Controller
    {
        private HashSet<Key> heldKeys = new();
        internal VoyagerInput_Keyboard(IKeyboard device) : base(device)
        {
            Device.KeyDown += Device_KeyDown;
            Device.KeyUp += Device_KeyUp;
            Device.KeyChar += Device_KeyChar;
        }

        private void Device_KeyChar(IKeyboard device, char character)
        {
            Print.Log($"Key Char: {character}");
            FrameInputs.Add(new InputValue_Button(character.ToString(), true));
        }

        private void Device_KeyUp(IKeyboard device, Key key, int value)
        {
            Print.Log($"Key Up: {key} - {value}");
            FrameInputs.Add(new InputValue_Button(key.ToString(), true));
            heldKeys.Remove(key);
        }

        private void Device_KeyDown(IKeyboard device, Key key, int value)
        {
            Print.Log($"Key Down: {key} - {value}");
            if (!heldKeys.Contains(key))
            {
                FrameInputs.Add(new InputValue_Button(key.ToString(), true));
            }
            heldKeys.Add(key);
        }
    }
}
