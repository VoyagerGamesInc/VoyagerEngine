namespace VoyagerEngine.Framework
{
    internal interface IGameSystemsHandler
    {
        /// <summary>
        /// Register systems here
        /// </summary>
        void RegisterSystems(in GameSystems gameSystems);
    }
}
