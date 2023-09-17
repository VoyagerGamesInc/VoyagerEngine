using System.Diagnostics;
using Silk.NET.Windowing;
using VoyagerEngine.Core;
using VoyagerEngine.ECS;
using VoyagerEngine.Input;

namespace VoyagerEngine
{
    public static class VoyagerEngine
    {
        internal static VoyagerEngine_Internal Instance;
        public static void Main(string[] args)
        {

        }
        public static void Start(IVoyagerGame game)
        {
            Start(game, WindowOptions.Default);
        }
        public static void Start(IVoyagerGame game, WindowOptions windowOptions)
        {
            Instance = new VoyagerEngine_Internal(game, windowOptions);
        }
        public static void RegisterService(IService service)
        {
            Instance.RegisterService(service);
        }
        public static void RequestController(IVoyagerInput_Listener listener)
        {
            Instance.Input.Request(listener);
        }
        public static void CancelRequestController(IVoyagerInput_Listener listener)
        {
            Instance.Input.CancelRequest(listener);
        }
    }
    public static class Print
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
            Trace.WriteLine(message);
        }
    }

}