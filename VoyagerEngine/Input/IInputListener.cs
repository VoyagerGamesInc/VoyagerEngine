namespace VoyagerEngine.Input
{
    public interface IInputListener
    {
        string Name { get; }
        List<IInputController> Devices { get; set; }
        void OnInputEvent(IInputPayload payload);
    }
}
