using Silk.NET.Input;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Input
{
    public abstract class InputListener
    {
        public HashSet<IInputPayload> FrameInputs = new HashSet<IInputPayload>();
        public HashSet<ButtonName> Buttons = new HashSet<ButtonName>();
    }
}
