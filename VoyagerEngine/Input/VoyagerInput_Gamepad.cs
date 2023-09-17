using Silk.NET.Input;
using Log = System.Console;
namespace VoyagerEngine.Input
{
    internal class VoyagerInput_Gamepad : VoyagerInput_Device<IGamepad>, IVoyagerInput_Controller
    {
        internal VoyagerInput_Gamepad(IGamepad device) : base(device)
        {
            Print.Log($"Gamepad: \"{device.Name}\" - Activated");
            Device.ButtonDown += Device_ButtonDown;
            Device.ButtonUp += Device_ButtonUp;
            Device.ThumbstickMoved += Device_ThumbstickMoved;
            Device.TriggerMoved += Device_TriggerMoved;
        }

        private void Device_TriggerMoved(IGamepad device, Trigger trigger)
        {
            Print.Log($"Trigger: {trigger.Index} - {trigger.Position}");
            FrameInputs.Add(new InputValue_Float(trigger.Index.ToString(), trigger.Position));
        }

        private void Device_ThumbstickMoved(IGamepad device, Thumbstick thumbstick)
        {
            Print.Log($"Thumbstick: {thumbstick.Index} - {thumbstick.X},{thumbstick.Y}");
            FrameInputs.Add(new InputValue_XY(thumbstick.Index.ToString(), thumbstick.X, thumbstick.Y));
        }

        private void Device_ButtonUp(IGamepad device, Button button)
        {
            Print.Log($"Button Up: {button.Name}");
            FrameInputs.Add(new InputValue_Button(button.Name.ToString(), true));
        }

        private void Device_ButtonDown(IGamepad device, Button button)
        {
            Print.Log($"Button Down: {button.Name}");
            FrameInputs.Add(new InputValue_Button(button.Name.ToString(), false));
        }
    }
}
