using Silk.NET.Input;

namespace VoyagerEngine.Input
{
    internal interface IVoyagerInput_Controller
    {
        bool WasUpdatedThisFrame { get; set; }
        IInputDevice InputDevice { get; }
        IVoyagerInput_Listener Listener { get; set; }
        void SetListener(IVoyagerInput_Listener listener);
    }
}