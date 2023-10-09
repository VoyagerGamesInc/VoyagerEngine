namespace VoyagerEngine.Core
{
    public interface ITickingSystem : ISystem
    {
        void Tick(in EntityRegistry registry);
    }
}
