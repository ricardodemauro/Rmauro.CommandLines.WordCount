// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Rmauro.CommandLines.WordCount;

var summary = BenchmarkRunner.Run<LineCountBenchmarks>();

[SimpleJob]
[MemoryDiagnoser]
public class LineCountBenchmarks
{
    static readonly string file = Path.Combine(Directory.GetCurrentDirectory(), "test");

    [Benchmark(Baseline = true)]
    public void GetLineCount()
    {
        _ = WordCounter.GetLineCount(filePath: file);
    }

    static readonly string big_file = Path.Combine(Directory.GetCurrentDirectory(), "../../assets/bigfile.txt");

    [Benchmark()]
    public void GetLineCountBigFile()
    {
        _ = WordCounter.GetLineCount(filePath: big_file);
    }
}