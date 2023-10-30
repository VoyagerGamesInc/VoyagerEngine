namespace VoyagerEngine.Framework
{
    public abstract partial class ViewDeducer
    {
        // 1 Component
        public void ViewBreak<T1>(Func<T1,bool> callback)
            where T1 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                return callback(entity.GetComponent<T1>());
            }, new HashSet<Type>() { typeof(T1) });
        }
        public void ViewBreak<T1>(Func<Entity,T1, bool> callback)
            where T1 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                return callback(entity, entity.GetComponent<T1>());
            }, new HashSet<Type>() { typeof(T1) });
        }

        // 2 Components
        public void ViewBreak<T1, T2>(Func<T1, T2, bool> callback)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                return callback(entity.GetComponent<T1>(), entity.GetComponent<T2>());
            }, new HashSet<Type>() { typeof(T2), typeof(T1) });
        }
        public void ViewBreak<T1, T2>(Func<Entity, T1, T2, bool> callback)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                return callback(entity, entity.GetComponent<T1>(), entity.GetComponent<T2>());
            }, new HashSet<Type>() { typeof(T2), typeof(T1) });
        }
    }
}
