namespace VoyagerEngine.Framework
{
    public sealed class EntityRegistry : ViewDeducer
    {
        protected override IEnumerable<Entity> entities => entityRegistry;
        private HashSet<Entity> entityRegistry = new HashSet<Entity>();

        public static Entity CreateEntity()
        {
            return GameSystems.Instance.entityRegistry.CreateEntity_Internal();
        }
        private Entity CreateEntity_Internal()
        {
            Entity entity = new Entity();
            entityRegistry.Add(entity);
            return entity;
        }
        public static void RemoveEntity(Entity entity)
        {
            GameSystems.Instance?.entityRegistry.RemoveEntity_Internal(entity);
        }
        private void RemoveEntity_Internal(Entity entity)
        {
            entityRegistry.Remove(entity);
        }
    }
}
