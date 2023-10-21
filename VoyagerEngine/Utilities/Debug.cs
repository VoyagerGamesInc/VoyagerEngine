namespace VoyagerEngine
{
    public static class Debug
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }
        public static void Trace(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }
        public static void Assert(bool condition, string message)
        {
            System.Diagnostics.Debug.Assert(condition, message);
        }
    }
}