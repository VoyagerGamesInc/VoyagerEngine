namespace VoyagerEngine.Framework
{
    public interface ITickingSystem : ISystem
    {
        void Tick(in EntityRegistry registry);
    }
}
