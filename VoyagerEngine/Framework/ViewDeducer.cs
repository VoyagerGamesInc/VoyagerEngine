namespace VoyagerEngine.Framework
{
    public abstract partial class ViewDeducer
    {
        protected abstract HashSet<Entity> entities { get; }
        private void View_Internal(Func<Entity, bool> perEntity, HashSet<Type> components)
        {
            foreach (Entity entity in entities)
            {
                if (entity.HasComponents(components))
                {
                    if (perEntity(entity))
                    {
                        break;
                    }
                }
            }
        }
        public ViewDeducer Exclude_Internal(HashSet<Type> components)
        {
            return new ExcludedViewDeducer(entities.Where(entity => entity.ExcludesComponents(components)).ToHashSet());
        }
    }
    internal class ExcludedViewDeducer : ViewDeducer
    {
        protected override HashSet<Entity> entities => viewedEntities;
        internal HashSet<Entity> viewedEntities;
        internal ExcludedViewDeducer(HashSet<Entity> entities)
        {
            viewedEntities = entities;
        }
    }
}
