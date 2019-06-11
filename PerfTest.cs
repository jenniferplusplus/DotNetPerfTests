using System;
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
        private int count;

//        [Params(100, 200, 400, 600, 800, 1000)] public int N;
        [Params(10, 20, 50)] public int N;
//        [Params(100, 1000, 10000)] public int N;

        [GlobalSetup]
        public void Setup()
        {
            count = N;
        }

        [Benchmark(Baseline = true)]
        public string Concat() => Concat(N);
        
        [Benchmark]
        public string Interpolation() => Interpolation(N);

        [Benchmark]
        public string Format() => Format(N);

        [Benchmark]
        public string StringBuilder() => StringBuilder(N);
        
        public string Concat(int n)
        {
            var result = "";
            for (int i = 0; i < n; i++)
            {
                result += i;
            }

            return result;
        }
        
        public string Interpolation(int n)
        {
            var result = "";
            for (int i = 0; i < n; i++)
            {
                result = $"{result}{n}";
            }

            return result;
        }
        
        public string Format(int n)
        {
            var result = "";
            for (int i = 0; i < n; i++)
            {
                result = string.Format("{0}{1}", result, i);
            }

            return result;
        }
        
        public string StringBuilder(int n)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < n; i++)
            {
                sb.Append(i);
            }

            return sb.ToString();
        }
    }
}