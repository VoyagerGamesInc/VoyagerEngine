using VoyagerEngine.Attributes;

namespace VoyagerEngine.Core
{
    public class Entity
    {
        internal Dictionary<Type, IComponent> Components = new Dictionary<Type, IComponent>();
        internal HashSet<Type> Flags = new HashSet<Type>();
        public Entity()
        {
        }
        public T AddComponent<T>() where T : class, IComponent, new()
        {
            if (!Components.TryGetValue(typeof(T), out IComponent? component))
            {
                component = new T();
                Components.Add(typeof(T), component);
                if (Attribute.IsDefined(typeof(T), typeof(IncludeFlagsAttribute)))
                {
                    IncludeFlagsAttribute attribute = (IncludeFlagsAttribute)typeof(T).GetCustomAttributes(typeof(IncludeFlagsAttribute), false)[0];
                    foreach (Type t in attribute.Flags)
                    {
                        AddFlag(t);
                    }
                }
            }
            return (T)component;
        }
        public T AddComponentWithFlags<T>(HashSet<Type> flags) where T : class, IComponent, new()
        {
            foreach(Type flag in flags)
            {
                AddFlag(flag);
            }
            return AddComponent<T>();
        }
        public bool HasComponent<T>() where T : class, IComponent, new()
        {
            return Components.ContainsKey(typeof(T));
        }
        public T? GetComponent<T>() where T : class, IComponent, new()
        {
            if (Components.TryGetValue(typeof(T), out IComponent? component))
            {
                return (T)component;
            }
            return null;
        }
        public T GetOrAddComponent<T>() where T : class, IComponent, new()
        {
            T? component = GetComponent<T>();
            return component == null ? AddComponent<T>() : component;
        }
        public void RemoveComponent<T>() where T : class, IComponent, new()
        {
            if (Components.ContainsKey(typeof(T)))
            {
                Components.Remove(typeof(T));
            }
        }
        public void AddFlag<T>() where T : IFlag
        {
            if (!Flags.Contains(typeof(T)))
            {
                Flags.Add(typeof(T));
            }
        }
        public void AddFlag(Type t)
        {
            if (t is IFlag && !Flags.Contains(t))
            {
                Flags.Add(t);
            }
        }
        public void RemoveFlag<T>() where T : IFlag
        {
            if (Flags.Contains(typeof(T)))
            {
                Flags.Remove(typeof(T));
            }
        }
        public bool HasFlag<T>() where T : IFlag
        {
            return Flags.Contains(typeof(T));
        }

        public bool HasFlags(params Type[] flags)
        {
            if (flags.Length == 0)
                return true;
            return Flags.Intersect(flags).Distinct().Count() == flags.Length;
        }
    }
}
