namespace VoyagerEngine.Framework
{
    public class GameServices
    {
        private Dictionary<Type, IService> services = new();
        IGameServicesHandler handler;
        internal GameServices(IGameServicesHandler handler)
        {
            this.handler = handler;
        }
        internal void Init()
        {
            handler.RegisterServices(this);
        }
        public void RegisterService<T>() where T : class, IService, new()
        {
            Type serviceType = typeof(T);
            if (!services.ContainsKey(serviceType))
            {
                services.Add(serviceType, new T());
            }
        }
        internal T GetService<T>() where T : IService
        {
            if (services.TryGetValue(typeof(T), out IService? service))
            {
                return (T)service;
            }
            throw new Exception("Attempted to retrieve an unregistered service.");
        }
        internal bool HasService(Type type)
        {
            return services.ContainsKey(type);
        }
    }
}

