using Silk.NET.Input;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Input
{
    internal abstract class DeviceController<D,C> : Controller where D : IInputDevice where C : IControlHandler
    {
        public List<IInputPayload> FrameInputs { get; set; } = new();
        public C? ControlHandler { get; set; }

        internal DeviceController(D device)
        {
            Device = device;
        }
    }
}
