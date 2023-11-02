using System.Collections.Generic;

namespace VoyagerEngine.Framework
{
    public sealed class EntityRegistry : ViewDeducer
    {
        protected override IEnumerable<Entity> entities => entityRegistry.Values;
        private Dictionary<EntityId, Entity> entityRegistry = new Dictionary<EntityId, Entity>();
        private uint entityCounter = 1;

        public static Entity CreateEntity()
        {
            return GameSystems.Instance.entityRegistry.CreateEntity_Internal();
        }

        public static bool FindEntity(EntityId id, out Entity entity)
        {
            return GameSystems.Instance.entityRegistry.entityRegistry.TryGetValue(id, out entity);
        }
        private Entity CreateEntity_Internal()
        {
            Entity entity = new Entity();
            entityRegistry.Add(new EntityId() { Id = entityCounter++ }, entity);
            return entity;
        }
        public static void RemoveEntity(EntityId entityId)
        {
            GameSystems.Instance?.entityRegistry.RemoveEntity_Internal(entityId);
        }
        private void RemoveEntity_Internal(EntityId entityId)
        {
            entityRegistry.Remove(entityId);
        }
        public static void RemoveEntity(Entity entity)
        {
            GameSystems.Instance?.entityRegistry.RemoveEntity_Internal(entity);
        }
        private void RemoveEntity_Internal(Entity entity)
        {
            entityRegistry.Remove(entity.Id);
        }
    }
}
