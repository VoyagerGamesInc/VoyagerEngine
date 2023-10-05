using System.Diagnostics;

namespace VoyagerEngine
{
    public static class Log
    {

        public static void Write(string message)
        {
            Console.WriteLine(message);
            Trace.WriteLine(message);
        }
    }
}