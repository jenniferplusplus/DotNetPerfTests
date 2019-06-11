using BenchmarkDotNet.Running;

namespace StringPerf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<PerfTest>();
        }
    }
}