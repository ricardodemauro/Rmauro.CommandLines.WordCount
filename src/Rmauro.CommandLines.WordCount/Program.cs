// See https://aka.ms/new-console-template for more information
using Rmauro.CommandLines.WordCount;
using static System.Console;

if (args.Length == 0)
{
    PrintUsage();
    return;
}

var ag = ArgumentsParser.Parse(args);

WriteLine("Processing file: {0}", ag.FilePath);

static void PrintUsage()
{
    WriteLine("Usage: ccwc [<file-path>] [-c <file-path>] [-l <file-path>] [-w <file-path>] [-m <file-path>]");
    WriteLine("\nExamples:");
    WriteLine("  ccwc <file-path>     # Outputs the number of bytes, lines, and words in the file");
    WriteLine("  ccwc -c <file-path>  # Outputs the number of bytes in the file");
    WriteLine("  ccwc -l <file-path>  # Outputs the number of lines in the file");
    WriteLine("  ccwc -w <file-path>  # Outputs the number of words in the file");
    WriteLine("  ccwc -m <file-path>  # Outputs the number of chars in the file");
    WriteLine("\ncat <file-path> | ccwc <option>  # Outputs the option taken from standard input");
}