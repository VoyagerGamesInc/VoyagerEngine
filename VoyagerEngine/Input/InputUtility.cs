using Silk.NET.Input;

namespace VoyagerEngine.Input
{
    public static class InputUtility
    {
        public enum RequestTypes
        {
            Any,
            Keyboard,
            Mouse,
            Gamepad
        }
        public static bool HasKey(InputListener inputComponent, Key key)
        {
            foreach (InputPayloadKey payload in inputComponent.FrameInputs.OfType<InputPayloadKey>())
            {
                if(payload.Key == key)
                {
                    return true;
                }
            }
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
