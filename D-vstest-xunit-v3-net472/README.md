# D: VSTest runner with xUnit v3 kills nothing on .NET Framework (net472)

`Lib` targets `netstandard2.0;net10.0`. `Masker.Mask` has a `#if NET` branch and a netstandard2.0 `#else` branch, a loop. `Lib.Tests` runs two xUnit v3 tests on `net472` and `net10.0`, with `xunit.runner.visualstudio` 4.0.1 and `Microsoft.NET.Test.Sdk` 18.11.0 for VSTest. Both tests pass (`Lib.Tests/bin/Debug/net472/Lib.Tests.exe`: Total 2, Failed 0).

```bash
dotnet tool restore
dotnet build Repro.sln
cd Lib
dotnet stryker --config-file stryker-vstest-net472.json --solution ../Repro.sln
```

.NET Framework needs `--solution`. Without it Stryker stops with "Stryker could not build your project as no solution file was presented".

Result on 5.0.0, `test-runner: vstest`, `concurrency: 1`:

| Config | Mutated code | Result |
|---|---|---|
| `stryker-vstest-net472.json` | the netstandard2.0 `#else` branch | 11 tested, all Survived, score 0.00 % |
| `stryker-vstest-net472-off.json` (`coverage-analysis: off`) | the same | the same, 0.00 % |
| `stryker-vstest-netstandard2.0.json` | the same | 0.00 % |
| `stryker-vstest-net10.0.json` | the `#if NET` branch | 0.00 % |

Stryker never activates the mutants. Even `i++` → `i--` in the loop (an endless loop, so Timeout is expected) and removing the `CopyTo` statement end as Survived. With coverage analysis on, the log also shows `It looks like the test coverage capture failed. Disable coverage based optimisation.`

Stryker does pair the net472 test project with the library's netstandard2.0 build and mutates the `#else` branch, so choosing the target works.
