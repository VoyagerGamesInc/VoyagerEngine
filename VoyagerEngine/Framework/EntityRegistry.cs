namespace VoyagerEngine.Framework
{
    public class EntityRegistry
    {
        private HashSet<Entity> _entities = new HashSet<Entity>();

        public static Entity CreateEntity()
        {
            return GameSystems.Instance.entityRegistry.CreateEntity_Internal();
        }
        private Entity CreateEntity_Internal()
        {
            Entity entity = new Entity();
            _entities.Add(entity);
            return entity;
        }
        public static void RemoveEntity(Entity entity)
        {
            GameSystems.Instance?.entityRegistry.RemoveEntity_Internal(entity);
        }
        private void RemoveEntity_Internal(Entity entity)
        {
            _entities.Remove(entity);

        }
        private void View_Internal(Action<Entity> perEntity, HashSet<Type> components)
        {
            foreach (Entity entity in _entities)
            {
                if (entity.HasComponents(components))
                {
                    perEntity(entity);
                }
            }
        }
        public void View<T1,T2>(Action<Entity,T1,T2> callback)
            where T1 : class, IComponent, new()
            where T2 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                callback(entity, entity.GetComponent<T1>(), entity.GetComponent<T2>());
            }, new HashSet<Type>() { typeof(T2), typeof(T1) });
        }
        public void View<T1>(Action<Entity,T1> callback)
            where T1 : class, IComponent, new()
        {
            View_Internal(entity =>
            {
                callback(entity, entity.GetComponent<T1>());
            }, new HashSet<Type>() { typeof(T1) });
        }
    }
}
