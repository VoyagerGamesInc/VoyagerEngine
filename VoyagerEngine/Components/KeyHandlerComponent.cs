using Silk.NET.Input;
using VoyagerEngine.Framework;
using VoyagerEngine.Input;

namespace VoyagerEngine.Components
{
    public class KeyHandlerComponent : IComponent
    {
        public Dictionary<Key, KeyControl> KeyResponses { get; set; }
    }
}
