using VoyagerEngine.Core;

namespace VoyagerEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ServiceDependencyAttribute : Attribute
    {
        public HashSet<Type> Services { get; private set; }
        public ServiceDependencyAttribute(params Type[] services)
        {
            Services = services.ToHashSet();
        }
    }
}
