using VoyagerEngine.Core;

namespace VoyagerEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class IncludeComponentAttribute<T> : Attribute where T : IComponent
    {
        public T Component { get; private set; }
        public IncludeComponentAttribute(T component)
        {
            Component = component;
    }
    }
}
