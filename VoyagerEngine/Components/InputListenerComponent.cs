using VoyagerEngine.Framework;
using VoyagerEngine.Input;

namespace VoyagerEngine.Components
{
    public class InputListenerComponent : IComponent
    {
        public InputUtility.RequestTypes RequestType = InputUtility.RequestTypes.Gamepad;
        public HashSet<InputListener> Listeners = new HashSet<InputListener>();
    }
}
