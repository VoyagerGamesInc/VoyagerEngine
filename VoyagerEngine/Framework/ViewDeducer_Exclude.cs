namespace VoyagerEngine.Framework
{
    public abstract partial class ViewDeducer
    {
        // 1 Component
        public ViewDeducer Exclude<T1>()
            where T1 : class, IComponent, new()
        {
            return Exclude_Internal(new HashSet<Type>() { typeof(T1) });
        }

        // 2 Components
        public ViewDeducer Exclude<T1, T2>()
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
             return Exclude_Internal(new HashSet<Type>() { typeof(T2), typeof(T1) });
        }
    }
}
