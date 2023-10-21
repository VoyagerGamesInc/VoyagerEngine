using VoyagerEngine.Framework;
namespace VoyagerEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class IncludeComponentAttribute : Attribute
    {
        public HashSet<Type> Components { get; private set; }
        public IncludeComponentAttribute(params Type[] services)
        {
            Components = services.ToHashSet();
        }
    }
}
