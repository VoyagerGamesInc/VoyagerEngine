using Silk.NET.Input;

namespace VoyagerEngine.Input
{
    internal interface IInput_Controller
    {
        bool WasUpdatedThisFrame { get; set; }
        IInputDevice InputDevice { get; }
        IInput_Listener Listener { get; set; }
        void SetListener(IInput_Listener listener);
    }
}