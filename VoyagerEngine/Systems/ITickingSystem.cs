using VoyagerEngine.Framework;

namespace VoyagerEngine.Systems
{
    public interface ITickingSystem : ISystem
    {
        void Tick(in EntityRegistry registry);
    }
}
