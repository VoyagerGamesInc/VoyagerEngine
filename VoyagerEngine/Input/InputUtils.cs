using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoyagerEngine.Input
{
    internal static class InputUtils
    {
        internal static ControlName GetControlName(this Trigger trigger)
        {
            return trigger.Index == 0 ? ControlName.LeftTrigger : ControlName.RightTrigger;
        }
        internal static ControlName GetControlName(this Thumbstick stick)
        {
            return stick.Index == 0 ? ControlName.LeftStick : ControlName.RightStick;
        }
        internal static ControlName GetControlName(this Button button)
        {
            return (ControlName)((int)button.Name + 1);
        }
    }
}
