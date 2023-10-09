namespace VoyagerEngine.Core
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
            GameSystems.Instance.entityRegistry.RemoveEntity_Internal(entity);
        }
        private void RemoveEntity_Internal(Entity entity)
        {
            _entities.Remove(entity);

        }
        private HashSet<Entity> DeduceView<T>(params Type[] flags) where T : class, IComponent, new()
        {
            return _entities.Where(entity => DeduceView_Predicate<T>(entity,flags)).ToHashSet();
        }
        private bool DeduceView_Predicate<T>(Entity entity, params Type[] flags) where T : class, IComponent, new()
        {
            return entity.HasComponent<T>() && entity.HasFlags(flags);
        }
        public void View<T>(Action<Entity,T?> callback, params Type[] flags) where T : class, IComponent, new()
        {
            HashSet<Entity> entities = DeduceView<T>();
            foreach (Entity entity in entities)
            {
                callback.Invoke(entity,entity.GetComponent<T>());
            }
        }
    }
}
