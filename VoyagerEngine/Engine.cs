using Silk.NET.OpenAL;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Input;
using Silk.NET.Windowing;
using VoyagerEngine.Framework;
using System.Reflection;
using VoyagerEngine.Services;

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
            Engine engine = new Engine(game, windowOptions, args);
        }
        public static void SetGameMode<T>() where T : GameMode, new ()
        {
            Instance.SetGameMode_Internal<T>();
        }
        public static T GetService<T>() where T : IService
        {
            return Instance.gameServices.GetService<T>();
        }
        public static bool HasService(Type t)
        {
            return Instance.gameServices.HasService(t);
        }
        internal static IWindow GetWindow()
        {
            return Instance.window;
        }
        internal static GL GetOpenGL()
        {
            return Instance.gl;
        }
        internal static AL GetOpenAL()
        {
            return Instance.al;
        }
        internal static ALContext GetALContext()
        {
            return Instance.alContext;
        }
        internal static IInputContext GetInputContext()
        {
            return Instance.inputContext;
        }
        internal static WindowOptions GetWindowOptions()
        {
            return Instance.windowOptions;
        }
        internal static Stream LoadResource(string resourceName)
        {
            return Assembly.GetEntryAssembly().GetManifestResourceStream(resourceName);
        }
        internal static IEnumerable<string> FindResources(string resourcePath)
        {
            return Assembly.GetEntryAssembly().GetManifestResourceNames().Where(resourceName => resourceName.StartsWith(resourcePath));
        }

        private Game game;
        private GameServices gameServices;
        private Performance performance = new Performance();
        private IWindow window;
        private WindowOptions windowOptions;

        private IInputContext? inputContext;
        private AL? al;
        private ALContext? alContext;
        private GL? gl;
        private GameMode? gameMode;
        private ImGuiController? imguiController;
        internal Engine(Game game, WindowOptions windowOptions, params string[] args)
        {
            Instance = this;
            this.game = game;
            gameServices = new GameServices(game);
            this.windowOptions = windowOptions;
            window = Window.Create(windowOptions);

            window.Load += Init;
            window.Update += Tick;
            window.Render += Render;
            window.Closing += Close;

            window.Run();
        }
        private void Init()
        {
            al = AL.GetApi();
            alContext = ALContext.GetApi();
            gl = window.CreateOpenGL();
            inputContext = window.CreateInput();
            imguiController = new ImGuiController(gl, window, inputContext);
            game.RegisterServices(gameServices);
            game.StartGame();
            gameServices.Init();

        }
        private void Tick(double deltaTime)
        {
            performance.StartCpu();
            gameMode?.Tick(deltaTime);
            performance.StopCpu();
        }
        private void Render(double deltaTime)
        {
            performance.StartGpu();
            imguiController?.Update((float)deltaTime);
            gameMode?.Render(deltaTime);
            imguiController?.Render();
            performance.StopGpu();
        }
        private void Close()
        {
            al.Dispose();
            alContext.Dispose();
            imguiController?.Dispose();
            inputContext?.Dispose();
            gl?.Dispose();
        }
        private void SetGameMode_Internal<T>() where T : GameMode, new()
        {
            if(gameMode != null)
            {
                gameMode.CleanUp();
            }
            gameMode = new T();
            gameMode.StartMode_Internal();
        }
    }
}
