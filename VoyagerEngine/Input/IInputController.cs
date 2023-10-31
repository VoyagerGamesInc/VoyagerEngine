using Silk.NET.Input;
using VoyagerEngine.Components;

namespace VoyagerEngine.Input
{
    public interface IInputController
    {
        List<IInputPayload> FrameInputs { get; set; }
        InputHandler Listener { get; set; }
        IInputDevice Device { get; }
    }
    internal abstract class InputDevice<T> : IInputController where T : IInputDevice
    {
        public InputHandler Listener { get; set; }
        public List<IInputPayload> FrameInputs { get; set; } = new();
        public IInputDevice Device => device;

        protected T device;

        internal InputDevice(T device)
        {
            this.device = device;
        }
        protected abstract void Collect();
    }
}
