using VoyagerEngine.Framework;
using VoyagerEngine.Input;

namespace VoyagerEngine.Components
{
    public class InputListenerComponent : IComponent
    {
        public HashSet<InputHandler> Listeners = new HashSet<InputHandler>();
    }
}
