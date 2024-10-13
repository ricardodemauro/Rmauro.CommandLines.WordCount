// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Reflection;
using Rmauro.CommandLines.WordCount;
using static System.Console;

if (args.Length == 0 || args.Contains("--help"))
{
    PrintUsage();
    return;
}
if (args.Contains("--version"))
{
    PrintVersion();
    return;
}

var ag = ArgumentsParser.Parse(args);

var path = Path.Combine(Directory.GetCurrentDirectory(), ag.FilePath);

WriteLine("Processing file: {0}", ag.FilePath);

if (File.Exists(path.GetFullPath()) == false)
{
    WriteLine("Could not find file at {0}", ag.FilePath);
    return;
}

if (ag.CountBytes)
{
    var result = WordCounter.GetFileBytes(ag.FilePath.GetFullPath());
    Write("{0} ", result.Count);
}

if (ag.CountLines)
{
    var result = WordCounter.GetLineCount(ag.FilePath.GetFullPath());
    Write("{0} ", result.Count);
}

if (ag.CountWords)
{
    var result = WordCounter.GetWordCount(ag.FilePath.GetFullPath());
    Write("{0} ", result.Count);
}

if (ag.CountCharacters)
{
    var result = WordCounter.GetCharacterCount(ag.FilePath.GetFullPath());
    Write("{0} ", result.Count);
}

Write("{0}{1}", ag.FilePath, Environment.NewLine);

static void PrintUsage()
{
    const string usage_helper = @"Usage: ccwc [OPTION]... [FILE]...
  or:  ccwc [OPTION]... --files0-from=F
Print newline, word, and byte counts for each FILE, and a total line if
more than one FILE is specified.  A word is a non-zero-length sequence of
characters delimited by white space.

With no FILE, or when FILE is -, read standard input.

The options below may be used to select which counts are printed, always in
the following order: newline, word, character, byte, maximum line length.
  -c, --bytes            print the byte counts
  -m, --chars            print the character counts
  -l, --lines            print the newline counts
  -w, --words            print the word counts
      --help     display this help and exit
      --version  output version information and exit
      
!!!DISCLAIMER: This is a clone of famous wc (Word Count) command line. ";
    WriteLine(usage_helper);
}

static void PrintVersion()
{
    const string version_disclaimer = @"wc (GNU coreutils) {0}
Copyright (C) 2018 Free Software Foundation, Inc.
License GPLv3+: GNU GPL version 3 or later <https://gnu.org/licenses/gpl.html>.
This is free software: you are free to change and redistribute it.
There is NO WARRANTY, to the extent permitted by law.

Written by Ricardo Mauro (rmauro.dev).";

    var assemblyVersion = Assembly.GetExecutingAssembly().GetName()?.Version?.ToString() ?? "Unknow version";

    WriteLine(version_disclaimer, assemblyVersion);
}