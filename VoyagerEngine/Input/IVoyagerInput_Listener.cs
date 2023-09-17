namespace VoyagerEngine.Input
{
    public interface IVoyagerInput_Listener
    {
        string Name { get; }
        List<IVoyagerInput_Device> Devices { get; set; }
        void OnInputEvent(IVoyagerInput_Value payload);
    }
}
