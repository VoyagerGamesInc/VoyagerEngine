using VoyagerEngine.Attributes;
using VoyagerEngine.Rendering;

namespace VoyagerEngine.Core
{
    public class GameSystems
    {
        public static double DeltaTime { get; private set; }
        internal static GameSystems? Instance { get; private set; }

        internal EntityRegistry entityRegistry = new EntityRegistry();

        private List<ITickingSystem> tickingSystems = new List<ITickingSystem>();
        private List<IRenderSystem> renderSystems = new List<IRenderSystem>();
        private Action<GameSystems>? onInit;

        public GameSystems()
        {
            Instance = this;
        }
        internal void OnAddSystems(IGameSystemsHandler handler)
        {
            onInit -= handler.OnSystemsInit;
            onInit += handler.OnSystemsInit;
        }
        internal void Init()
        {
            onInit?.Invoke(this);
            foreach (ITickingSystem tickingSystem in tickingSystems)
            {
                tickingSystem.Init();
            }

            foreach (IRenderSystem renderSystems in renderSystems)
            {
                renderSystems.Init();
            }
        }
        internal void Tick(double deltaTime)
        {
            DeltaTime = deltaTime;
            foreach (ITickingSystem system in tickingSystems)
            {
                system.Tick(in entityRegistry); ;
            }
        }
        internal void Render(double deltaTime)
        {
            foreach (IRenderSystem system in renderSystems)
            {
                system.Render(in entityRegistry);
            }
        }
        internal void RegisterSystem<T>() where T : class, ISystem, new()
        {
            GameServices.CheckIfServiceExists<T, ServiceDependencyAttribute>();
            if (typeof(T) is ITickingSystem tickingSystem)
            {
                tickingSystems.Add(tickingSystem);
            }
            else if (typeof(T) is IRenderSystem renderSystem)
            {
                renderSystems.Add(renderSystem);
            }
        }
    }
}
