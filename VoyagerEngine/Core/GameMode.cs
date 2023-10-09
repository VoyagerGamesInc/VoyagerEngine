using VoyagerEngine.Rendering;

namespace VoyagerEngine.Core
{
    public abstract class GameMode : IGameServicesHandler, IGameSystemsHandler
    {
        internal GameServices _gameServices = new GameServices();
        internal GameSystems _gameSystems = new GameSystems();
        /// <summary>
        /// Register Services Here
        /// </summary>
        public virtual void OnServicesInit(GameServices gameServices) { }

        /// <summary>
        /// Register render systems here
        /// </summary>
        public virtual void OnSystemsInit(GameSystems gameSystems) { }
        /// <summary>
        /// Main entry point.
        /// </summary>
        public abstract void StartMode();

        /// <summary>
        /// Clean up.
        /// </summary>
        public virtual void CleanUp() { }

        internal void StartMode_Internal()
        {
            _gameServices.Init();

            _gameSystems.RegisterSystem<RenderingSystem>();
            _gameSystems.Init();
            StartMode();
        }
        internal void Tick(double deltaTime)
        {
            _gameSystems.Tick(deltaTime);
        }

        internal void Render(double deltaTime)
        {
            _gameSystems.Render(deltaTime);
        }
    }
}
