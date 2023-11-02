using Silk.NET.Input;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Input
{
    public abstract class Controller
    {
        internal bool HasListener => AssignedEntity.IsValid;
        internal IInputDevice Device;

        internal EntityId AssignedEntity;

        internal Action<Controller> OnAnyInput;
    }
}
