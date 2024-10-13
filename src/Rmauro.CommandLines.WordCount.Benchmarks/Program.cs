// See https://aka.ms/new-console-template for more information
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Rmauro.CommandLines.WordCount;

var summary = BenchmarkRunner.Run<CharacterCountBenchmarks>();

[SimpleJob]
[MemoryDiagnoser]
public class CharacterCountBenchmarks
{
    static readonly FileStreamOptions _options = new()
    {
        Access = FileAccess.Read,
        BufferSize = 1024,
        Mode = FileMode.Open,
        Options = FileOptions.SequentialScan,
        Share = FileShare.Read
    };

    static readonly string big_file = Path.Combine(Directory.GetCurrentDirectory(), "bigfile.txt");

    MemoryStream? sourceStream;

    [GlobalSetup]
    public void GlobalSetup()
    {
        using var fStream = new FileStream(big_file, _options);

        sourceStream = new MemoryStream();

        fStream.CopyTo(sourceStream);

        fStream.Close();
    }

    [GlobalCleanup]
    public void GlobalCleanup() => sourceStream?.Dispose();

    [Benchmark(Baseline = true)]
    public void GetStreamReaderCountCharacter()
    {
        sourceStream!.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(sourceStream!, Encoding.UTF8);

        _ = WordCounter.CountCharacter(reader);
    }

    [Benchmark()]
    public void GetWordReaderCountCharacter()
    {
        sourceStream!.Seek(0, SeekOrigin.Begin);
        var reader = new WordReader(sourceStream!, Encoding.UTF8);

        _ = WordCounter.CountCharacter(reader);
    }

    [Benchmark()]
    public void GetWordReaderUnsafeCountCharacter()
    {
        sourceStream!.Seek(0, SeekOrigin.Begin);
        var reader = new WordReader(sourceStream!, Encoding.UTF8);

        _ = WordCounter.UnsafeCountCharacter(reader);
    }
}