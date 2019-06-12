using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace StringPerf
{
//    [Orderer(SummaryOrderPolicy.FastestToSlowest))]
    [Orderer(SummaryOrderPolicy.Method)]
    [CoreJob]
    [MemoryDiagnoser]
    [RPlotExporter, RankColumn]
    public class PerfTest
    {
        [Params(2, 5, 10, 20, 40, 80, 160)] public int N;
        [Params(2, 5, 10, 20)] public int L;

        public string Fill;

        [GlobalSetup]
        public void Setup()
        {
            Fill = "0".PadRight(L);
        }

        [Benchmark(Baseline = true)]
        public string Concat() => Concat(N, Fill);
        
        [Benchmark]
        public string Interpolation() => Interpolation(N, Fill);

        [Benchmark]
        public string StringBuilder() => StringBuilder(N, Fill);
        
        [Benchmark]
        public string Join() => Join(N, Fill);
        
        public string Concat(int n, string fill)
        {
            var result = "";
            for (int i = 0; i < n; i++)
            {
                result += fill;
            }

            return result;
        }

        public string Interpolation(int n, string fill)
        {
            var result = "";
            for (int i = 0; i < n; i++)
            {
                result = $"{result}{fill}";
            }

            return result;
        }

        public string StringBuilder(int n, string fill)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < n; i++)
            {
                sb.Append(fill);
            }

            return sb.ToString();
        }

        public string Join(int n, string fill)
        {
            var list = Enumerable.Range(0, n).Select(i => fill);
            return String.Join("", list);
        }
    }
}