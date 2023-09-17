using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.OpenGLES;
using VoyagerEngine.Core;
using VoyagerEngine.Input;
using VoyagerEngine.Rendering;
using VoyagerEngine.GameMode;
using VoyagerEngine.ECS;

namespace VoyagerEngine
{

    internal class VoyagerEngine_Internal
    {
        internal IVoyagerGame _game;
        internal IWindow _window;
        internal WindowOptions _windowOptions;

        internal VoyagerInput Input;
        internal VoyagerRenderer Renderer;

        internal Dictionary<Type, IService> Services = new();
        internal IGameMode GameMode;
        internal VoyagerEngine_Internal(IVoyagerGame game, WindowOptions windowOptions)
        {
            _game = game;
            _windowOptions = windowOptions;
            _window = Window.Create(_windowOptions);

            Input = new VoyagerInput();
            Renderer = new VoyagerRenderer();

            _window.Load += Init_Internal;
            _window.Update += Update_Internal;
            _window.Update += Input.Update;
            _window.Render += Renderer.Render;
            _window.Run();
        }
        private void Init_Internal()
        {
            Input.Init(_window.CreateInput());
            Renderer.Init(_window.CreateOpenGLES());
            _game.Init();
        }

        private void Update_Internal(double deltaTime)
        {
            if(GameMode != null)
            {
                GameMode.Update(deltaTime);
            }
        }

        internal async void SetGameMode<T>() where T : IGameMode
        {
            if(GameMode != null)
            {
                await GameMode.CleanUp();
            }
            GameMode = Activator.CreateInstance<T>();
            await GameMode.Init();
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
            Print.Log("Attempted to retrieve an unregistered service.");
            return null;
        }
    }
}
