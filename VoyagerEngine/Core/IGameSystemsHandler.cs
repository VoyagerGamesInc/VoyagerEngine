namespace VoyagerEngine.Core
{
    internal interface IGameSystemsHandler
    {
        /// <summary>
        /// Register systems here
        /// </summary>
        void OnSystemsInit(GameSystems gameSystems);
    }
}
