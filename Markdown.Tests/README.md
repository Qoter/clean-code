**Performance tests on 10k, 100k and 1m random symbols**

``` ini

BenchmarkDotNet=v0.10.0
OS=Microsoft Windows NT 10.0.14393.0
Processor=Intel(R) Core(TM) i5-4210U CPU 1.70GHz, ProcessorCount=4
Frequency=2338337 Hz, Resolution=427.6544 ns, Timer=TSC
Host Runtime=Clr 4.0.30319.42000, Arch=64-bit  [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0
Job Runtime(s):
	Clr 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]


```
 Method |        Mean |    StdDev |      Median |
------- |------------ |---------- |------------ |
  On10K |   2.9254 ms | 0.0709 ms |   2.9189 ms |
 On100K |  29.1692 ms | 0.3626 ms |  29.2235 ms |
   On1M | 288.0315 ms | 2.6425 ms | 287.4628 ms |
