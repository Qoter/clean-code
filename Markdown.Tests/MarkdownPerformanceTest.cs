using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Running;
using NUnit.Framework;

namespace Markdown.Tests
{
    [MarkdownExporter]
    [MeanColumn, MedianColumn]
    public class MarkdownPerformanceTest
    {
        private static readonly string text10k = GenerateRandomMarkdownString(10000);
        private static readonly string text100k = GenerateRandomMarkdownString(100000);
        private static readonly string text1m = GenerateRandomMarkdownString(1000000);

        [Benchmark]
        public string On10K() => new Md(MdSettings.Default).RenderParagraphToHtml(text10k);

        [Benchmark]
        public string On100K() => new Md(MdSettings.Default).RenderParagraphToHtml(text100k);

        [Benchmark]
        public string On1M() => new Md(MdSettings.Default).RenderParagraphToHtml(text1m);

        public static string GenerateRandomMarkdownString(int length, int seed = 0)
        {
            var symbols = new[] { '_', '_', 'a', 'b', 'c', 'd', 'e', ' ' };
            var random = new Random(seed);
            return string.Join("", Enumerable.Range(0, length).Select(_ => symbols[random.Next(symbols.Length)]));
        }

        [Test, Explicit]
        public void RunBenchmark()
        {
            BenchmarkRunner.Run<MarkdownPerformanceTest>();
        }

    }
}
