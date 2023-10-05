using MouseButton = Silk.NET.Input.MouseButton;
using Silk.NET.Input;
using System.Numerics;

namespace VoyagerEngine.Input
{
    internal class Input_Mouse : Input_Device<IMouse>
    {
        internal Input_Mouse(IMouse device) : base(device)
        {
            Device.DoubleClickRange = 10;
            Device.DoubleClickTime = 150;
            Device.MouseDown += Device_MouseDown;
            Device.MouseUp += Device_MouseUp;
            Device.MouseMove += Device_MouseMove;
            Device.DoubleClick += Device_DoubleClick;
            Device.Click += Device_Click;
            Device.Scroll += Device_Scroll;
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
