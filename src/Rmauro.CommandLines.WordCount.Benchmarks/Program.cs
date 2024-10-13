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

    byte[] memory;

    [GlobalSetup]
    public void GlobalSetup()
    {
        using var fStream = new FileStream(big_file, _options);

        using var memStream = new MemoryStream();

        fStream.CopyTo(memStream);

        memory = memStream.ToArray();

        fStream.Close();
    }

    static readonly string big_file = Path.Combine(Directory.GetCurrentDirectory(), "bigfile.txt");

    [Benchmark(Baseline = true)]
    public void GetStreamReaderCountCharacter()
    {
        using var memStream = new MemoryStream(memory);
        memStream.Seek(0, SeekOrigin.Begin);

        using var reader = new StreamReader(memStream, Encoding.UTF8);

        _ = WordCounter.CountCharacter(reader);
    }

    [Benchmark()]
    public void GetWordReaderCountCharacter()
    {
        using var memStream = new MemoryStream(memory);
        memStream.Seek(0, SeekOrigin.Begin);

        using var reader = new WordReader(memStream, Encoding.UTF8);

        _ = WordCounter.CountCharacter(reader);
    }

    [Benchmark()]
    public void GetWordReaderUnsafeCountCharacter()
    {
        using var memStream = new MemoryStream(memory);
        memStream.Seek(0, SeekOrigin.Begin);

        using var reader = new WordReader(memStream, Encoding.UTF8);

        _ = WordCounter.UnsafeCountCharacter(reader);
    }
}