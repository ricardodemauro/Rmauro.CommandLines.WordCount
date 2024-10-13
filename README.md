# wc-tool or Rmauro.CommandLines.WordCount

A version of the Unix command line tool, "wc", written in .NET.

---

## Implemented Options

- [x] -c : number of bytes
- [x] -l : number of lines
- [x] -w : number of words
- [x] -m : number of characters
- [x] Default options - equivalent to -c -l -w
- [ ] Read from Standard input

---

This repository takes inspiration from John Crickett's [Coding Challenge's](https://codingchallenges.fyi/) website, following the the [Build Your Own wc Tool](https://codingchallenges.fyi/challenges/challenge-wc) challenge. 

## Benchmarks

Benchmark Test using StreamReader vs WordReader Implementation. 

> Remember that StreamReader it's a implementation for all use cases, while WordReader was optmized for this scenarion only.

1. Test for Character Count

| Method                            | Mean        | Error     | StdDev    | Ratio | RatioSD | Gen0      | Gen1      | Gen2     | Allocated  | Alloc Ratio |
|---------------------------------- |------------:|----------:|----------:|------:|--------:|----------:|----------:|---------:|-----------:|------------:|
| GetStreamReaderCountCharacter     | 14,482.7 us | 272.88 us | 255.26 us |  1.00 |    0.02 | 4156.2500 | 2625.0000 | 968.7500 | 40510780 B |       1.000 |
| GetWordReaderCountCharacter       |    958.2 us |  15.10 us |  12.61 us |  0.07 |    0.00 |         - |         - |        - |      161 B |       0.000 |
| GetWordReaderUnsafeCountCharacter |    952.3 us |  18.04 us |  17.72 us |  0.07 |    0.00 |         - |         - |        - |      161 B |       0.000 |

## Useful Tools

To generate a random big file

```bash
tr -dc "A-Za-z 0-9" < /dev/urandom | fold -w100|head -n 100000 > bigfile.txt
```

Found on: https://stackoverflow.com/a/139289/1652594

## Good to Know

WordReader was inspired on this implementation: https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/IO/StreamReader.cs#L631

## Future

Check out this implementation on SimdUnicode: https://github.com/simdutf/SimdUnicode/tree/main
This is a fast C# library to validate UTF-8 strings.