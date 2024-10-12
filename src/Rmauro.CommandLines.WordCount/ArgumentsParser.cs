using System;

namespace Rmauro.CommandLines.WordCount;

public record class CWArgument(string FilePath, bool CountBytes, bool CountWords, bool CountLines, bool CountCharacters);

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
        var countB = arguments.Contains("-c");
        var countW = arguments.Contains("-w");
        var countL = arguments.Contains("-l");
        var countC = arguments.Contains("-m");

        var defaults = !countB && !countW && !countL && !countC;
        if (defaults) return new CWArgument(arguments[0], true, true, true, true);

        return new CWArgument(arguments[0], countB, countW, countL, countC);
    }
}
