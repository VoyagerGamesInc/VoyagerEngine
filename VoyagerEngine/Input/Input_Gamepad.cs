using Silk.NET.Input;
namespace VoyagerEngine.Input
{
    internal class Input_Gamepad : Input_Device<IGamepad>, IInput_Controller
    {
        internal Input_Gamepad(IGamepad device) : base(device)
        {
            Device.ButtonDown += Device_ButtonDown;
            Device.ButtonUp += Device_ButtonUp;
            Device.ThumbstickMoved += Device_ThumbstickMoved;
            Device.TriggerMoved += Device_TriggerMoved;
        }

        private void Device_TriggerMoved(IGamepad device, Trigger trigger)
        {
            FrameInputs.Add(new InputValue_Float(trigger.Index.ToString(), trigger.Position));
            WasUpdatedThisFrame = true;
        }

        private void Device_ThumbstickMoved(IGamepad device, Thumbstick thumbstick)
        {
            FrameInputs.Add(new InputValue_Stick(thumbstick.Index.ToString(), thumbstick.X, thumbstick.Y));
            WasUpdatedThisFrame = true;
        }

        private void Device_ButtonUp(IGamepad device, Button button)
        {
            FrameInputs.Add(new InputValue_Button(button.Name.ToString(), true));
            WasUpdatedThisFrame = true;
        }

        private void Device_ButtonDown(IGamepad device, Button button)
        {
            FrameInputs.Add(new InputValue_Button(button.Name.ToString(), false));
            WasUpdatedThisFrame = true;
        }
    }
}
