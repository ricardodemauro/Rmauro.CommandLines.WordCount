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

BenchmarkDotNet v0.14.0, Ubuntu 20.04.6 LTS (Focal Fossa) WSL
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK 8.0.402
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2


| Method                            | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0      | Gen1      | Gen2     | Allocated  | Alloc Ratio |
|---------------------------------- |----------:|----------:|----------:|----------:|------:|--------:|----------:|----------:|---------:|-----------:|------------:|
| GetStreamReaderCountCharacter     | 16.441 ms | 0.3414 ms | 1.0066 ms | 16.622 ms |  1.00 |    0.09 | 4156.2500 | 2625.0000 | 937.5000 | 40510697 B |       1.000 |
| GetWordReaderCountCharacter       |  1.020 ms | 0.0324 ms | 0.0886 ms |  1.001 ms |  0.06 |    0.01 |         - |         - |        - |       97 B |       0.000 |
| GetWordReaderUnsafeCountCharacter |  1.096 ms | 0.0609 ms | 0.1709 ms |  1.040 ms |  0.07 |    0.01 |         - |         - |        - |       97 B |       0.000 |

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