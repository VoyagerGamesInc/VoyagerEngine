namespace VoyagerEngine.Framework
{
    public abstract partial class ViewDeducer
    {
        // 1 Component
        public void View<T1>(Action<T1> view)
            where T1 : class, IComponent, new()
        {
            View<T1>((entity,c1) => {
                view(c1);
                return false;
            });
        }
        public void View<T1>(Func<T1, bool> viewBreak)
            where T1 : class, IComponent, new()
        {
            View<T1>((entity, c1) => {
                return viewBreak(c1);
            });
        }
        public void View<T1>(Action<Entity,T1> viewEntity)
            where T1 : class, IComponent, new()
        {
            View<T1>((entity,c1) => {
                viewEntity(entity,c1);
                return false;
            });
        }
        public void View<T1>(Func<Entity, T1, bool> viewEntityBreak)
            where T1 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                return viewEntityBreak(entity, entity.GetComponent<T1>());
            }, new HashSet<Type>() { typeof(T1) });
        }
        public ViewDeducer Exclude<T1>()
            where T1 : class, IComponent, new()
        {
            return Exclude_Internal(new HashSet<Type>() { typeof(T1) });
        }

        // 2 Components
        public void View<T1, T2>(Action<T1, T2> view)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
            View<T1,T2>((entity, c1, c2) => {
                view(c1,c2);
                return false;
            });
        }
        public void View<T1, T2>(Func<T1, T2, bool> viewBreak)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
            View<T1,T2>((entity, c1, c2) => {
                return viewBreak(c1, c2);
            });
        }
        public void View<T1, T2>(Action<Entity, T1, T2> viewEntity)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
            View<T1, T2>((entity, c1, c2) => {
                viewEntity(entity, c1, c2);
                return false;
            });
        }
        public void View<T1, T2>(Func<Entity, T1, T2, bool> viewEntityBreak)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                return viewEntityBreak(entity, entity.GetComponent<T1>(), entity.GetComponent<T2>());
            }, new HashSet<Type>() { typeof(T2), typeof(T1) });
        }
        public ViewDeducer Exclude<T1, T2>()
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
            return Exclude_Internal(new HashSet<Type>() { typeof(T2), typeof(T1) });
        }


        // 3 Components
        public void View<T1, T2, T3>(Action<T1, T2, T3> view)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
            where T3 : class, IComponent, new()
        {
            View<T1, T2, T3>((entity, c1, c2, c3) => {
                view(c1, c2, c3);
                return false;
            });
        }
        public void View<T1, T2, T3>(Func<T1, T2, T3, bool> viewBreak)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
            where T3 : class, IComponent, new()
        {
            View<T1, T2, T3>((entity, c1, c2, c3) => {
                return viewBreak(c1, c2, c3);
            });
        }
        public void View<T1, T2, T3>(Action<Entity, T1, T2, T3> viewEntity)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
            where T3 : class, IComponent, new()
        {
            View<T1, T2, T3>((entity, c1, c2, c3) => {
                viewEntity(entity, c1, c2, c3);
                return false;
            });
        }
        public void View<T1, T2, T3>(Func<Entity, T1, T2, T3, bool> viewEntityBreak)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
            where T3 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                return viewEntityBreak(entity, entity.GetComponent<T1>(), entity.GetComponent<T2>(), entity.GetComponent<T3>());
            }, new HashSet<Type>() { typeof(T3), typeof(T2), typeof(T1) });
        }
        public ViewDeducer Exclude<T1, T2, T3>()
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
            where T3 : class, IComponent, new()
        {
            return Exclude_Internal(new HashSet<Type>() { typeof(T3), typeof(T2), typeof(T1) });
        }
    }
}
