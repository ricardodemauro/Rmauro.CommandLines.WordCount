using System.Text;

namespace Rmauro.CommandLines.WordCount;

public record class WordCountResult(long Count, string FileName, bool IsComputed, string Reason)
{
    public static WordCountResult NotFound() => new WordCountResult(0, string.Empty, false, "File not found");

    public static WordCountResult Computed(long count) => new WordCountResult(count, string.Empty, true, string.Empty);

    public static WordCountResult Computed(long count, string fileName) => new WordCountResult(count, fileName, true, string.Empty);
}

public static class WordCounter
{
    static long CountWords(StreamReader reader)
    {
        var allLines = reader.ReadToEnd();

        return allLines.Split([' ', '\t', '\n', '\r'], StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public static WordCountResult GetWordCount(string filePath)
    {
        if (!File.Exists(filePath))
            return WordCountResult.NotFound();

        using var fStream = File.OpenRead(filePath);
        var reader = new StreamReader(fStream, Encoding.UTF8);

        return WordCountResult.Computed(CountWords(reader), filePath.GetFileName());
    }

    static long CountCharacter(StreamReader reader)
    {
        var allLines = reader.ReadToEnd();

        return allLines.Length + 1;
    }

    public static WordCountResult GetCharacterCount(string filePath)
    {
        if (!File.Exists(filePath))
            return WordCountResult.NotFound();

        using var fStream = File.OpenRead(filePath);
        var reader = new StreamReader(fStream, Encoding.UTF8);

        return WordCountResult.Computed(CountCharacter(reader), filePath.GetFileName());
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
