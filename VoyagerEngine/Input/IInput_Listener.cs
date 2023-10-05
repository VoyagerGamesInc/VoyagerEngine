namespace VoyagerEngine.Input
{
    public interface IInput_Listener
    {
        string Name { get; }
        List<IInput_Device> Devices { get; set; }
        void OnInputEvent(IInput_Value payload);
    }
}
