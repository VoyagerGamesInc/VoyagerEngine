namespace VoyagerEngine.Framework
{
    public abstract partial class ViewDeducer
    {
        protected abstract IEnumerable<Entity> entities { get; }
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
            return new ExcludedViewDeducer(entities.Where(entity => entity.ExcludesComponents(components)));
        }
    }
    internal class ExcludedViewDeducer : ViewDeducer
    {
        protected override IEnumerable<Entity> entities => viewedEntities;
        internal IEnumerable<Entity> viewedEntities;
        internal ExcludedViewDeducer(IEnumerable<Entity> entities)
        {
            viewedEntities = entities;
        }
    }
}
