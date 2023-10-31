
namespace VoyagerEngine.Framework
{
    internal interface IRenderSystem : ISystem
    {
        void Render(in EntityRegistry registry);
    }
}
