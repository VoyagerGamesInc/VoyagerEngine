using VoyagerEngine.Framework;
using VoyagerEngine.Input;

namespace VoyagerEngine.Components
{
    public class InputListenerComponent : IComponent
    {
        public HashSet<InputListener> Listeners = new HashSet<InputListener>();
    }
}
