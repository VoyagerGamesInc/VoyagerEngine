using VoyagerEngine.Attributes;

namespace VoyagerEngine.Framework
{
    public class Entity
    {
        internal Dictionary<Type, IComponent> Components = new Dictionary<Type, IComponent>();
        public T AddComponent<T>() where T : class, IComponent, new()
        {
            if (!Components.TryGetValue(typeof(T), out IComponent component))
            {
                component = new T();
                Components.Add(typeof(T), component);
                if (Attribute.IsDefined(typeof(T), typeof(IncludeComponentAttribute)))
                {
                    IncludeComponentAttribute attribute = (IncludeComponentAttribute)typeof(T).GetCustomAttributes(typeof(IncludeComponentAttribute), false)[0];
                    foreach (Type t in attribute.Components)
                    {
                        if(typeof(IComponent).IsAssignableFrom(t))
                        {
                            Components.Add(t, Activator.CreateInstance(t) as IComponent);
                        }
                    }
                }
            }
            return (T)component;
        }
        public bool HasComponent<T>() where T : class, IComponent, new()
        {
            return Components.ContainsKey(typeof(T));
        }
        public bool HasComponents(HashSet<Type> components)
        {
            return components.All(key => Components.Keys.Contains(key));
        }
        internal bool ExcludesComponents(HashSet<Type> components)
        {
            return !components.Overlaps(Components.Keys);
        }
        public T GetComponent<T>() where T : class, IComponent, new()
        {
            if (Components.TryGetValue(typeof(T), out IComponent component))
            {
                return (T)component;
            }
            return null;
        }
        public T GetOrAddComponent<T>() where T : class, IComponent, new()
        {
            T component = GetComponent<T>();
            return component == null ? AddComponent<T>() : component;
        }
        public void RemoveComponent<T>() where T : class, IComponent, new()
        {
            if (Components.ContainsKey(typeof(T)))
            {
                Components.Remove(typeof(T));
            }
        }
    }
}
