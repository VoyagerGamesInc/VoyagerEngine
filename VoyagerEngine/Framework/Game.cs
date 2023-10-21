namespace VoyagerEngine.Framework
{
    public abstract class Game : IGameServicesHandler
    {
        /// <summary>
        /// Register Services Here
        /// </summary>
        public virtual void RegisterServices(in GameServices gameServices)
        {
        }
        /// <summary>
        /// Register Services Here
        /// </summary>
        public abstract void StartGame();
    }
}
