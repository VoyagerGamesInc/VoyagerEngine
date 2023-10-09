using Timer = System.Timers.Timer;
using System.Timers;
using System.Diagnostics;

namespace VoyagerEngine
{
    internal class Performance
    {
        private Stopwatch cpu;
        private Stopwatch gpu;
        private Timer timer;
        private double cpuCount;
        private double cpuTime;
        private double gpuCount;
        private double gpuTime;
        internal Performance()
        {
            cpu = new Stopwatch();
            gpu = new Stopwatch();
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        internal void StartCpu()
        {
            cpu.Restart();
        }
        internal void StopCpu()
        {
            cpu.Stop();
            cpuTime += cpu.Elapsed.TotalNanoseconds;
            cpuCount++;
        }
        internal void StartGpu()
        {
            gpu.Restart();
        }
        internal void StopGpu()
        {
            gpu.Stop();
            gpuTime += cpu.Elapsed.TotalNanoseconds;
            gpuCount++;
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            double cAvg = cpuTime / cpuCount;
            double gAvg = gpuTime / gpuCount;
            Debug.Log($"[CPU: {cpuCount} ticks | {cAvg.ToString("0")} ns] [GPU: {gpuCount} ticks | {gAvg.ToString("0")} ns]");
            cpuTime = 0;
            gpuTime = 0;
            gpuCount = 0;
            cpuCount = 0;
        }
    }
}