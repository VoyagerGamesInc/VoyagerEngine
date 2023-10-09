using System.Collections.Generic;
using VoyagerEngine.Attributes;

namespace VoyagerEngine.Core
{
    public class GameServices
    {
        private Dictionary<Type, IService> _services = new();
        private Action<GameServices>? OnInit;
        
        internal void OnAddServices(IGameServicesHandler handler)
        {
            OnInit -= handler.OnServicesInit;
            OnInit += handler.OnServicesInit;
        }
        internal void Init()
        {
            OnInit?.Invoke(this);
            foreach (var service in _services)
            {
                service.Value.Init();
            }
        }
        internal void RegisterService<T>(T service) where T : IService
        {
            Type serviceType = typeof(T);
            if (!_services.ContainsKey(serviceType))
            {
                CheckIfServiceExists<T, ServiceDependencyAttribute>();
                _services.Add(serviceType, service);
            }
        }
        internal T GetService<T>() where T : IService
        {
            if (_services.TryGetValue(typeof(T), out IService? service))
            {
                return (T)service;
            }
            throw new Exception("Attempted to retrieve an unregistered service.");
        }
        internal bool HasService(Type type)
        {
            return _services.ContainsKey(type);
        }
        internal bool HasServices(Type[] services)
        {
            HashSet<Type> hashSet = services.ToHashSet();
            return hashSet.All(_services.ContainsKey);
        }
        internal static void CheckIfServiceExists<T, A>() where A : ServiceDependencyAttribute
        {
            if (Attribute.IsDefined(typeof(T), typeof(ServiceDependencyAttribute)))
            {
                ServiceDependencyAttribute attribute = (ServiceDependencyAttribute)typeof(T).GetCustomAttributes(typeof(ServiceDependencyAttribute), false)[0];
                Debug.Assert(attribute.Services.All(Engine.HasService), "Service has a dependency");
            }
        }
    }
}
