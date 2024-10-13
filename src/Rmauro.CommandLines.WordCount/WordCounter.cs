using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Rmauro.CommandLines.WordCount.Benchmarks")]
[assembly: InternalsVisibleTo("Rmauro.CommandLines.WordCount.Tests")]

namespace Rmauro.CommandLines.WordCount;

public record class WordCountResult(long Count, string FileName, bool IsComputed, string Reason)
{
    public static WordCountResult NotFound() => new WordCountResult(0, string.Empty, false, "File not found");

    public static WordCountResult Computed(long count) => new WordCountResult(count, string.Empty, true, string.Empty);

    public static WordCountResult Computed(long count, string fileName) => new WordCountResult(count, fileName, true, string.Empty);
}

public static class WordCounter
{
    static readonly FileStreamOptions _options = new()
    {
        Access = FileAccess.Read,
        BufferSize = 4096,
        Mode = FileMode.Open,
        Options = FileOptions.SequentialScan,
        Share = FileShare.Read
    };

    static long CountWords(StreamReader reader)
    {
        var allLines = reader.ReadToEnd();

        return allLines.Split([' ', '\t', '\n', '\r'], StringSplitOptions.RemoveEmptyEntries).Length;
    }

    static long CountWords(WordReader reader)
    {
        long wordCount = reader.CountWords();

        return wordCount;
    }

    public static WordCountResult GetWordCount(string filePath)
    {
        // if (!File.Exists(filePath))
        //     return WordCountResult.NotFound();

        // using var fStream = File.OpenRead(filePath);
        // var reader = new StreamReader(fStream, Encoding.UTF8);

        // return WordCountResult.Computed(CountWords(reader), filePath.GetFileName());
        if (!File.Exists(filePath))
            return WordCountResult.NotFound();

        using var fStream = new FileStream(filePath, _options);
        var reader = new WordReader(fStream, Encoding.UTF8);

        return WordCountResult.Computed(CountWords(reader), filePath.GetFileName());
    }

    internal static long CountCharacter(StreamReader reader)
    {
        var allLines = reader.ReadToEnd();

        return allLines.Length;
    }

    internal static long CountCharacter(WordReader reader)
    {
        var characterCount = reader.CountCharacters();
        return characterCount;
    }

    internal static long UnsafeCountCharacter(WordReader reader)
    {
        var characterCount = reader.UnsafeCountCharacters();

        return characterCount;
    }

    public static WordCountResult GetCharacterCount(string filePath)
    {
        if (!File.Exists(filePath))
            return WordCountResult.NotFound();

        using var fStream = new FileStream(filePath, _options);
        var reader = new WordReader(fStream, Encoding.UTF8);

        return WordCountResult.Computed(CountCharacter(reader), filePath.GetFileName());
    }

    public static WordCountResult GetUnsafeCharacterCount(string filePath)
    {
        if (!File.Exists(filePath))
            return WordCountResult.NotFound();

        using var fStream = new FileStream(filePath, _options);
        var reader = new WordReader(fStream, Encoding.UTF8);

        return WordCountResult.Computed(UnsafeCountCharacter(reader), filePath.GetFileName());
    }

    public static long GetFileBytes(FileInfo fileInfo) => fileInfo.Length;

    public static WordCountResult GetFileBytes(string filePath)
    {
        if (!File.Exists(filePath))
            return WordCountResult.NotFound();

        var fInfo = new FileInfo(filePath);
        return WordCountResult.Computed(GetFileBytes(fInfo), filePath.GetFileName());
    }

    static long LineCount(StreamReader reader)
    {
        var allLines = reader.ReadToEnd();

        return allLines.Split(Environment.NewLine).LongLength - 1;
    }

    public static long GetLineCount(FileStream fStream)
    {
        var reader = new StreamReader(fStream, Encoding.UTF8);
        return LineCount(reader);
    }

    public static WordCountResult GetLineCount(string filePath)
    {
        if (!File.Exists(filePath))
            return WordCountResult.NotFound();

        using var fStream = File.OpenRead(filePath);
        var reader = new StreamReader(fStream, Encoding.UTF8);

        return WordCountResult.Computed(LineCount(reader), filePath.GetFileName());
    }
}
