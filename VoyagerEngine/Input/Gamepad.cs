using Silk.NET.Input;
using System.Numerics;

namespace VoyagerEngine.Input
{
    internal class Gamepad : InputDevice<IGamepad>
    {
        private Dictionary<int,float> triggers = new();
        private Dictionary<int,Vector2> thumbsticks = new();
        private HashSet<ButtonName> pressedButtons = new();
        private HashSet<ButtonName> releasedButtons = new();
        internal Gamepad(IGamepad device) : base(device)
        {
            device.ButtonDown += Device_ButtonDown;
            device.ButtonUp += Device_ButtonUp;
            device.ThumbstickMoved += Device_ThumbstickMoved;
            device.TriggerMoved += Device_TriggerMoved;
        }
        protected override void Collect()
        {
            foreach (ButtonName button in pressedButtons)
            {
                FrameInputs.Add(new InputPayloadButton(button, true));
            }
            foreach (ButtonName button in releasedButtons)
            {
                FrameInputs.Add(new InputPayloadButton(button, false));
            }
            foreach (KeyValuePair<int,float> kv in triggers)
            {
                FrameInputs.Add(new InputPayloadFloat(kv.Key.ToString(), kv.Value));
                Debug.Log($"{kv.Key} / {kv.Key}");
            }
            foreach(KeyValuePair<int,Vector2> kv in thumbsticks)
            {
                FrameInputs.Add(new InputPayloadStick(kv.Key.ToString(), kv.Value));
            }
            pressedButtons.Clear();
            releasedButtons.Clear();
            triggers.Clear();
            thumbsticks.Clear();
        }

        private void Device_TriggerMoved(IGamepad device, Trigger trigger)
        {
            if (!triggers.ContainsKey(trigger.Index))
            {
                triggers.Add(trigger.Index,trigger.Position);
            }
            else
            {
                triggers[trigger.Index] = trigger.Position;
            }
        }

        private void Device_ThumbstickMoved(IGamepad device, Thumbstick thumbstick)
        {
            if (!thumbsticks.ContainsKey(thumbstick.Index))
            {
                thumbsticks.Add(thumbstick.Index, new Vector2(thumbstick.X, thumbstick.Y));
            }
            else
            {
                thumbsticks[thumbstick.Index] = new Vector2(thumbstick.X, thumbstick.Y);
            }
        }

        private void Device_ButtonUp(IGamepad device, Button button)
        {
            pressedButtons.Add(button.Name);
        }

        private void Device_ButtonDown(IGamepad device, Button button)
        {
            releasedButtons.Remove(button.Name);
        }
    }
}
