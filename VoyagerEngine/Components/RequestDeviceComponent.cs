using VoyagerEngine.Framework;

namespace VoyagerEngine.Components
{
    public class RequestDeviceComponent : IComponent
    {
        public enum RequestTypes
        {
            Any,
            Keyboard,
            Mouse,
            Gamepad
        }
        public RequestTypes RequestType { get; private set; }
        public RequestDeviceComponent(RequestTypes requestType)
        {
            RequestType = requestType;
        }
    }
}
