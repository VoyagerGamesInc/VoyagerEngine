using Silk.NET.Input;
using VoyagerEngine.Components;

namespace VoyagerEngine.Input
{
    public static class InputUtility
    {
        public static bool HasKey(ControlHandlerComponent inputComponent, Key key)
        {
            //foreach (InputPayloadKey payload in inputComponent.FrameInputs.OfType<InputPayloadKey>())
            //{
            //    if(payload.Key == key)
            //    {
            //        return true;
            //    }
            //}
            return false;
        }
        //public static bool HasFloat(in InputComponent inputComponent, out float value)
        //{
        //    foreach(InputPayloadFloat payload in inputComponent.FrameInputs.OfType<InputPayloadFloat>())
        //    {
        //    }
        //}
    }
}
