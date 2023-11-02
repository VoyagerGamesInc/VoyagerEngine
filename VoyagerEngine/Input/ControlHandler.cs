using VoyagerEngine.Framework;

namespace VoyagerEngine.Input
{
    public class ControlHandler : IControlHandler
    {
        public Dictionary<ControlName, Control> ControlResponses { get; set; }
    }
}

