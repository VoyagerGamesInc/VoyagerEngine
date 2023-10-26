using VoyagerEngine.Attributes;
using VoyagerEngine.Framework;
using VoyagerEngine.Services;

namespace VoyagerEngine
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
                CheckIfServiceExists<T, RequiresServiceAttribute>();
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
        internal bool HasServices(Type[] services)
        {
            HashSet<Type> hashSet = services.ToHashSet();
            return hashSet.All(this.services.ContainsKey);
        }
        internal static void CheckIfServiceExists<T, A>() where A : RequiresServiceAttribute
        {
            if (Attribute.IsDefined(typeof(T), typeof(RequiresServiceAttribute)))
            {
                RequiresServiceAttribute attribute = (RequiresServiceAttribute)typeof(T).GetCustomAttributes(typeof(RequiresServiceAttribute), false)[0];
                foreach (var service in attribute.Services)
                {
                    Debug.Assert(Engine.HasService(service), $"Service dependency for {typeof(T).Name} is missing: {service.Name}");
                }
            }
        }
    }
}

