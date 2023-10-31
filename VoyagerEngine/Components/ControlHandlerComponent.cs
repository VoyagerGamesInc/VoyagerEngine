using VoyagerEngine.Framework;
using VoyagerEngine.Input;

namespace VoyagerEngine.Components
{
    public class ControlHandlerComponent : IComponent
    {
        public Dictionary<ControlName, KeyControl> ControlResponses { get; set; }
    }
}
