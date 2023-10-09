using VoyagerEngine.Core;
namespace VoyagerEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class IncludeFlagsAttribute : Attribute
    {
        public HashSet<Type> Flags { get; private set; }
        public IncludeFlagsAttribute(params Type[] flags)
        {
            Flags = flags.ToHashSet();
        }
    }
}
