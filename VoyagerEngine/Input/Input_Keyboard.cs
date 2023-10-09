using Silk.NET.Input;
namespace VoyagerEngine.Input
{
    internal class Input_Keyboard : Input_Device<IKeyboard>
    {
        private HashSet<Key> heldKeys = new();
        internal Input_Keyboard(IKeyboard device) : base(device)
        {
            Device.KeyDown -= Device_KeyDown;
            Device.KeyDown += Device_KeyDown;

            Device.KeyUp -= Device_KeyUp;
            Device.KeyUp += Device_KeyUp;

            Device.KeyChar -= Device_KeyChar;
            Device.KeyChar += Device_KeyChar;
        }

        private void Device_KeyChar(IKeyboard device, char character)
        {
            FrameInputs.Add(new InputValue_Char(character));
            WasUpdatedThisFrame = true;
        }

        private void Device_KeyUp(IKeyboard device, Key key, int value)
        {
            FrameInputs.Add(new InputValue_Key(key, false));
            heldKeys.Remove(key);
            WasUpdatedThisFrame = true;
        }

        private void Device_KeyDown(IKeyboard device, Key key, int value)
        {
            if (!heldKeys.Contains(key))
            {
                FrameInputs.Add(new InputValue_Key(key, true));
            }
            heldKeys.Add(key);
            WasUpdatedThisFrame = true;
        }
    }
}
