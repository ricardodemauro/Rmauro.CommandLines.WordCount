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

Test using StreamReader vs Custom Implementation

1. Test for Character Count

| Method                        | Mean        | Error     | StdDev      | Ratio | RatioSD | Gen0      | Gen1      | Gen2      | Allocated   | Alloc Ratio |
|------------------------------ |------------:|----------:|------------:|------:|--------:|----------:|----------:|----------:|------------:|------------:|
| GetCharacterCount             | 16,044.2 us | 426.22 us | 1,202.17 us |  1.01 |    0.11 | 4218.7500 | 2687.5000 | 1000.0000 | 39561.08 KB |       1.000 |
| GetCustomCharacterCount       |    995.6 us |  19.32 us |    18.07 us |  0.06 |    0.00 |         - |         - |         - |     2.19 KB |       0.000 |
| GetUnsafeCustomCharacterCount |    937.7 us |  11.74 us |     9.80 us |  0.06 |    0.00 |    0.9766 |         - |         - |     2.19 KB |       0.000 |

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