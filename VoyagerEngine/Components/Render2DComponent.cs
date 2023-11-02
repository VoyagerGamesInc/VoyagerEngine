using VoyagerEngine.Framework;
using VoyagerEngine.Data;
using VoyagerEngine.Services;

namespace VoyagerEngine.Components
{
    public class Render2DComponent : IComponent
    {
        public IRenderData Data { get; set; }
        public UpdateFlags UpdateFlag { get; set; }
    }
}
