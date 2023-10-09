using Silk.NET.Windowing;
using Silk.NET.OpenGLES;
using VoyagerEngine.Core;
using VoyagerEngine.Input;
using VoyagerEngine.Rendering;
using VoyagerEngine.Attributes;

namespace VoyagerEngine
{
    public class Engine
    {
        internal static Engine Instance;
        public static void Main(string[] args)
        {
        }
        public static void Start<T>(WindowOptions windowOptions, params string[] args) where T : Game, new ()
        {
            if (Instance != null)
                return;
            T game = new T();
            new Engine(game, windowOptions, args);
        }
        public static void SetGameMode<T>() where T : GameMode, new ()
        {
            Instance.SetGameMode_Internal<T>();
        }
        public static void RegisterService<T>(T service) where T : IService
        {
            if(Instance._gameMode != null)
            {
                Instance._gameMode._gameServices.RegisterService(service);
            }
            else
            {
                Instance._globalServices.RegisterService(service);
            }
        }
        public static T GetService<T>() where T : IService
        {
            return Instance._globalServices.GetService<T>();
        }
        public static bool HasService(Type t)
        {
            if (Instance._gameMode != null && Instance._gameMode._gameServices.HasService(t))
            {
                return true;
            }
            return Instance._globalServices.HasService(t);
        }


        private Game _game;
        private GameServices _globalServices = new GameServices();
        private Performance _performance = new Performance();

        private GameMode? _gameMode;
        internal Engine(Game game, WindowOptions windowOptions, params string[] args)
        {
            Instance = this;
            _game = game;

            RegisterService(new RenderService(out IWindow _window, windowOptions));
            RegisterService(new InputService());

            _globalServices.OnAddServices(_game);

            _window.Load += Init;
            _window.Update += Tick;
            _window.Render += Render;
            _window.Run();
        }
        private void Init()
        {
            _game.StartGame();
        }
       
        private void Tick(double deltaTime)
        {
            _performance.StartCpu();
            _gameMode?.Tick(deltaTime);
            _performance.StopCpu();
        }

        private void Render(double deltaTime)
        {
            _performance.StartGpu();
            _gameMode?.Render(deltaTime);
            _performance.StopGpu();
        }
        private void SetGameMode_Internal<T>() where T : GameMode, new()
        {
            if(_gameMode != null)
            {
                _gameMode.CleanUp();
            }
            _gameMode = new T();
            _gameMode.StartMode_Internal();
        }
    }
}
