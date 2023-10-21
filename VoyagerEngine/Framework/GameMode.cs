using VoyagerEngine.Rendering;

namespace VoyagerEngine.Framework
{
    public abstract class GameMode : IGameSystemsHandler
    {
        internal GameSystems _gameSystems;

        public GameMode()
        {
            _gameSystems = new GameSystems(this);
        }
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
            OnSystemsInit(_gameSystems);
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

        public void RegisterSystems(in GameSystems gameSystems)
        {
        }
    }
}
