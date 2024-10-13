using System;

namespace Rmauro.CommandLines.WordCount;

public record class CWArgument(
    string FilePath, 
    bool CountBytes, 
    bool CountWords, 
    bool CountLines, 
    bool CountCharacters);

public static class ArgumentsParser
{
    /*
     * -c : number of bytes
     * -l : number of lines
     * -w : number of words
     * -m : number of characters
     * Default options - equivalent to -c -l -w
     */
    public static CWArgument Parse(string[] arguments)
    {
        var countB = arguments.Contains("-c") || arguments.Contains("--bytes");
        var countW = arguments.Contains("-w")|| arguments.Contains("--words");
        var countL = arguments.Contains("-l")|| arguments.Contains("--lines");
        var countM = arguments.Contains("-m")|| arguments.Contains("--chars");
        var path = arguments[^1];

        var defaults = !countB && !countW && !countL && !countM;
        if (defaults) return new CWArgument(path, true, true, true, false);

        return new CWArgument(path, countB, countW, countL, countM);
    }
}
