namespace VoyagerEngine.Framework
{
    public abstract partial class ViewDeducer
    {
        // 1 Component
        public void View<T1>(Action<T1> callback)
            where T1 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                callback(entity.GetComponent<T1>());
                return false;
            }, new HashSet<Type>() { typeof(T1) });
        }
        public void View<T1>(Action<Entity,T1> callback)
            where T1 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                callback(entity, entity.GetComponent<T1>());
                return false;
            }, new HashSet<Type>() { typeof(T1) });
        }

        // 2 Components
        public void View<T1, T2>(Action<T1, T2> callback)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                callback(entity.GetComponent<T1>(), entity.GetComponent<T2>());
                return false;
            }, new HashSet<Type>() { typeof(T2), typeof(T1) });
        }
        public void View<T1, T2>(Action<Entity, T1, T2> callback)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                callback(entity, entity.GetComponent<T1>(), entity.GetComponent<T2>());
                return false;
            }, new HashSet<Type>() { typeof(T2), typeof(T1) });
        }
    }
}
