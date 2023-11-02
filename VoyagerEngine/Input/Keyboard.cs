using Silk.NET.Input;
namespace VoyagerEngine.Input
{
    internal class Keyboard : DeviceController<IKeyboard,KeyHandler>
    {
        private HashSet<Key> heldKeys = new();
        private HashSet<char> chars = new();
        internal Keyboard(IKeyboard device) : base(device)
        {
            device.KeyDown += Device_KeyDown;
            device.KeyUp += Device_KeyUp;
            device.KeyChar += Device_KeyChar;
        }
        private void Device_KeyChar(IKeyboard device, char character)
        {
            //FrameInputs.Add(character,new InputPayloadChar(character));
            //WasUpdatedThisFrame = true;
        }

        private void Device_KeyUp(IKeyboard device, Key key, int value)
        {
            //FrameInputs.Add(new InputPayloadKey(key, false));
            //heldKeys.Remove(key);
            //WasUpdatedThisFrame = true;
        }

        private void Device_KeyDown(IKeyboard device, Key key, int value)
        {
            //if (!heldKeys.Contains(key))
            //{
            //    FrameInputs.Add(new InputPayloadKey(key, true));
            //}
            //heldKeys.Add(key);
            //WasUpdatedThisFrame = true;
        }
    }
}
