using VoyagerEngine.Framework;

namespace VoyagerEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RequiresServiceAttribute : Attribute
    {
        public HashSet<Type> Services { get; private set; }
        public RequiresServiceAttribute(params Type[] services)
        {
            Services = services.ToHashSet();
        }
    }
}
