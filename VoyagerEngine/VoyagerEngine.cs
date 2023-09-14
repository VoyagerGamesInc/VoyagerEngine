using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
internal class Program
{
    private static IWindow _window;
    private static IInputContext _input;
    private static void Main(string[] args)
    {
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(800, 600),
            Title = "Mystery Dungeon"
        };
        _window = Window.Create(options);

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;

        _window.Run();
        //_window.Close();
    }
    private static void OnLoad() {
        _input = _window.CreateInput();
    }

    private static void OnUpdate(double deltaTime) { }

    private static void OnRender(double deltaTime) { }

}