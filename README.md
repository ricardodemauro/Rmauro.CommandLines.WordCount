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


## Tools

To generate a random big file

```bash
tr -dc "A-Za-z 0-9" < /dev/urandom | fold -w100|head -n 100000 > bigfile.txt
```

Found on: https://stackoverflow.com/a/139289/1652594