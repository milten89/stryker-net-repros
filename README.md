# Stryker.NET 5.0.0 repros

Minimal projects for Stryker.NET 5.0.0 bugs, each with a case that shows the bug and a control case that doesn't. All pin Stryker.NET 5.0.0 in their `.config/dotnet-tools.json` and use xUnit v3 4.0.2. A, B and C run `test-runner: mtp` on net10.0; D runs `test-runner: vstest` on net472 and net10.0 and needs Windows. I ran them on Windows 11 with the .NET 10.0.403 SDK on 2026-11-07 and 2026-11-08.

| Folder | Bug | Upstream |
|---|---|---|
| [A-linq-count-property](A-linq-count-property) | The Linq mutator turns the `Count` property into `Sum`. The rollback also discards the compilable Equality mutant next to it, and `ignore-mutations` can't prevent this. | stryker-mutator/stryker-net#2762 |
| [B-interpolated-handler-cs1620](B-interpolated-handler-cs1620) | The String mutant of `string.Create(provider, $"…")` never compiles (CS1620). | new |
| [C-negated-pattern-designation](C-negated-pattern-designation) | The Equality mutant `x is not { } v` never compiles (CS0128, CS0165), and the rollback blames the compilable mutant at the variable's use. | stryker-mutator/stryker-net#3891, #3597 |
| [D-vstest-xunit-v3-net472](D-vstest-xunit-v3-net472) | The VSTest runner with xUnit v3 never activates mutants, on net472 as on net10.0: every mutant survives. | stryker-mutator/stryker-net#3920, #3117 |

Run one:

```bash
cd A-linq-count-property
dotnet tool restore
cd Lib
dotnet stryker
```

D needs `--solution ../Repro.sln`; its README has the commands. Add `--diag --verbosity trace` to see the injected code and the compiler errors. `run-all.sh` runs every repro and every config, and writes the reports to `<repro>/out/<config>/`.

License: MIT ([LICENSE](LICENSE)).
