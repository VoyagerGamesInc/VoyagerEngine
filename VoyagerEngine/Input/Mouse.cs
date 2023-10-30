using MouseButton = Silk.NET.Input.MouseButton;
using Silk.NET.Input;
using System.Numerics;

namespace VoyagerEngine.Input
{
    internal class Mouse : InputDevice<IMouse>
    {
        internal Mouse(IMouse device) : base(device)
        {
            device.DoubleClickRange = 10;
            device.DoubleClickTime = 150;
            device.MouseDown += Device_MouseDown;
            device.MouseUp += Device_MouseUp;
            device.MouseMove += Device_MouseMove;
            device.DoubleClick += Device_DoubleClick;
            device.Click += Device_Click;
            device.Scroll += Device_Scroll;
        }
        protected override void Collect()
        {

        }

        private void Device_Click(IMouse device, MouseButton mouseButton, Vector2 position)
        {
        }
        private void Device_DoubleClick(IMouse device, MouseButton mouseButton, Vector2 position)
        {
        }
        private void Device_MouseMove(IMouse device, Vector2 position)
        {
        }
        private void Device_MouseUp(IMouse device, MouseButton mouseButton)
        {
        }
        private void Device_MouseDown(IMouse device, MouseButton mouseButton)
        {
        }
        private void Device_Scroll(IMouse device, ScrollWheel scroll)
        {
        }

    }
}
