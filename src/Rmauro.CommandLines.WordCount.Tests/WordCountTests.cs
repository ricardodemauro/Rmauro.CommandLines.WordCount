namespace Rmauro.CommandLines.WordCount.Tests;

public class WordCountTests
{
    [Fact]
    public void TestBytesCount()
    {
        string file = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");

        var result = WordCounter.GetFileBytes(filePath: file);

        Assert.True(result.IsComputed);
        Assert.Equal(342384, result.Count);
    }

    [Fact]
    public void TestLineCount()
    {
        string file = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");

        var result = WordCounter.GetLineCount(filePath: file);

        Assert.True(result.IsComputed);
        Assert.Equal(7190, result.Count);
    }

    [Fact]
    public void TestWordCount()
    {
        string file = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");

        var result = WordCounter.GetWordCount(filePath: file);

        Assert.True(result.IsComputed);
        Assert.Equal(58164, result.Count);
    }

    [Fact]
    public void TestCharactersCount()
    {
        string file = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");

        var result = WordCounter.GetCharacterCount(filePath: file);

        Assert.True(result.IsComputed);

        Assert.Equal(339485, result.Count);
    }
}