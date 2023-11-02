using VoyagerEngine.Framework;
using VoyagerEngine.Rendering;

namespace VoyagerEngine.Components
{
    public class InitializeRender2DComponent : IComponent
    {
        public string ShaderName { get; set; } = ShaderData.DefaultSpriteShaderName;
        public string TextureName { get; set; }
    }
}
