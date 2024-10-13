namespace Rmauro.CommandLines.WordCount.Tests;

public class WordCountTests
{
    [Theory]
    [InlineData("bigfile.txt", 10100000)]
    [InlineData("test.txt", 342190)]
    public void TestBytesCount_Theory(string fileName, int count)
    {
        string file = Path.Combine(Directory.GetCurrentDirectory(), "../../../../../assets/", fileName);

        var result = WordCounter.GetFileBytes(filePath: file);

        Assert.True(result.IsComputed);
        Assert.Equal(count, result.Count);
    }

    [Theory]
    [InlineData("bigfile.txt", 100000)]
    [InlineData("test.txt", 7145)]
    public void TestLineCount_Theory(string fileName, int count)
    {
        string file = Path.Combine(Directory.GetCurrentDirectory(), "../../../../../assets/", fileName);

        var result = WordCounter.GetLineCount(filePath: file);

        Assert.True(result.IsComputed);
        Assert.Equal(count, result.Count);
    }

    [Theory]
    [InlineData("bigfile.txt", 253049)]
    [InlineData("test.txt", 58164)]
    public void TestWordCount_Theory(string fileName, int count)
    {
        string file = Path.Combine(Directory.GetCurrentDirectory(), "../../../../../assets/", fileName);

        var result = WordCounter.GetWordCount(filePath: file);

        Assert.True(result.IsComputed);
        Assert.Equal(count, result.Count);
    }

    [Theory]
    [InlineData("bigfile.txt", 10100000)]
    [InlineData("test.txt", 339292)]
    public void TestCharactersCount_Theory(string fileName, int charactersCount)
    {
        string file = Path.Combine(Directory.GetCurrentDirectory(), "../../../../../assets/", fileName);

        var result = WordCounter.GetCharacterCount(filePath: file);

        Assert.True(result.IsComputed);

        Assert.Equal(charactersCount, result.Count);
    }
}