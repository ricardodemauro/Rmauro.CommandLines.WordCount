using System;

namespace Rmauro.CommandLines.WordCount;

public record class CWArgument(string FilePath, bool Default);

public static class ArgumentsParser
{
    public static CWArgument Parse(string[] arguments)
    {
        return new CWArgument(arguments[0], true);
    }
}
