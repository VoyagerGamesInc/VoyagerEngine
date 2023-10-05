using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.OpenGLES;
using VoyagerEngine.Core;
using VoyagerEngine.Input;
using VoyagerEngine.Rendering;
namespace VoyagerEngine
{
    public class Engine
    {
        internal static Engine Instance;
        public static void Main(string[] args)
        {

        }
        public static void Start(IGame game)
        {
            if (Instance != null)
                return;

            Start(game, WindowOptions.Default);
        }
        public static void Start(IGame game, WindowOptions windowOptions)
        {
            if (Instance != null)
                return;

            new Engine(game, windowOptions);
        }
        public static void RegisterService(IService service)
        {
            Instance.RegisterService(service);
        }
        public static void RequestController(IInput_Listener listener)
        {
            Instance.Input.Request(listener);
        }
        public static void CancelRequestController(IInput_Listener listener)
        {
            Instance.Input.CancelRequest(listener);

        }

        private IGame _game;
        private IWindow _window;
        private WindowOptions _windowOptions;

        internal InputManager Input;
        internal Renderer Renderer;

        internal Dictionary<Type, IService> Services = new();

        private Performance _fpsTracker;
        internal Engine(IGame game, WindowOptions windowOptions)
        {
            Instance = this;
            _game = game;
            _windowOptions = windowOptions with
            {
                VSync = false
            };
            _window = Window.Create(_windowOptions);
            _fpsTracker = new();

            Input = new InputManager();
            Renderer = new Renderer(_window);

            _window.Load += OnLoad;
            _window.Update += OnUpdate;
            _window.Render += OnRender;
            _window.Run();

        }
        private void OnLoad()
        {
            Input.OnLoad(_window.CreateInput());
            Renderer.OnLoad(_window.CreateOpenGLES());
            _game.OnLoad();
        }

        private void OnRender(double deltaTime)
        {
            _fpsTracker.StartGpu();
            Renderer.OnRender();
            _fpsTracker.StopGpu();
        }

        private void OnUpdate(double deltaTime)
        {
            _fpsTracker.StartCpu();
            Input.OnUpdate(deltaTime);
            _game.OnUpdate(deltaTime);
            _fpsTracker.StopCpu();
        }
        internal void RegisterService<T>(T service) where T : IService
        {
            Type serviceType = typeof(T);
            if (!Services.ContainsKey(serviceType))
            {
                Services.Add(serviceType, service);
            }
        }
        internal IService GetService<T>() where T : IService
        {
            if (Services.TryGetValue(typeof(T), out IService service))
            {
                return service;
            }
            Log.Write("Attempted to retrieve an unregistered service.");
            return null;
        }
    }
}
