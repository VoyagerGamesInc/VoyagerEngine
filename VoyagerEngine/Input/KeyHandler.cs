using Silk.NET.Input;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Input
{
    public class KeyHandler : IControlHandler
    {
        public Dictionary<Key, Control> KeyResponses { get; set; }
    }
}
