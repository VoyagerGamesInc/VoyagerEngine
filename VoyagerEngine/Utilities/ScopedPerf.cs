using System.Diagnostics;

namespace VoyagerEngine.Utilities
{
    public class ScopedPerf : IDisposable
    {
        private string Name;
        public Stopwatch stopwatch;
        public ScopedPerf(string name)
        {
            Name = name;
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        public void Dispose()
        {
            stopwatch.Stop();
            Debug.Log($"{Name} [{stopwatch.Elapsed.Nanoseconds}]");
        }
    }
}
