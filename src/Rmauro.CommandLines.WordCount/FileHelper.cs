namespace Rmauro.CommandLines.WordCount;

public static class FileHelper
{
    public static int WordCount(StreamReader reader)
    {
        var allLines = reader.ReadToEnd();

        return allLines.Split(['\t', ' '], StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
