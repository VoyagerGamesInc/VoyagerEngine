using VoyagerEngine.Attributes;
using VoyagerEngine.Rendering;

namespace VoyagerEngine.Framework
{
    public class GameSystems
    {
        public static double DeltaTime { get; private set; }
        internal static GameSystems? Instance { get; private set; }

        internal EntityRegistry entityRegistry = new EntityRegistry();

        private List<ITickingSystem> tickingSystems = new List<ITickingSystem>();
        private List<IRenderSystem> renderSystems = new List<IRenderSystem>();
        private IGameSystemsHandler handler;

        internal GameSystems(IGameSystemsHandler handler) : base()
        {
            Instance = this;
            this.handler = handler;
        }
        internal void Init()
        {
            handler.RegisterSystems(this);
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
        public void RegisterSystem<T>() where T : class, ISystem, new()
        {
            GameServices.CheckIfServiceExists<T, RequiresServiceAttribute>();
            if (typeof(T).IsAssignableTo(typeof(ITickingSystem)))
            {
                tickingSystems.Add(new T() as ITickingSystem);
            }
            else if (typeof(T).IsAssignableTo(typeof(IRenderSystem)))
            {
                renderSystems.Add(new T() as IRenderSystem);
            }
        }
    }
}
