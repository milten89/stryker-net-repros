# Stryker.NET 5.0.0 compile-error repros

Three minimal projects, each with one case that shows a bug and one control case that doesn't. All use Stryker.NET 5.0.0 (pinned in each `.config/dotnet-tools.json`), `test-runner: mtp`, xUnit v3 4.0.2 and net10.0. I ran them on Windows 11 with the .NET 10.0.403 SDK on 2026-11-07.

| Folder | Bug | Upstream |
|---|---|---|
| [A-linq-count-property](A-linq-count-property) | The Linq mutator turns the `Count` property into `Sum`. The rollback also discards the compilable Equality mutant next to it, and `ignore-mutations` can't prevent this. | stryker-mutator/stryker-net#2762 |
| [B-interpolated-handler-cs1620](B-interpolated-handler-cs1620) | The String mutant of `string.Create(provider, $"…")` never compiles (CS1620). | new |
| [C-negated-pattern-designation](C-negated-pattern-designation) | The Equality mutant `x is not { } v` never compiles (CS0128, CS0165), and the rollback blames the compilable mutant at the variable's use. | stryker-mutator/stryker-net#3891, #3597 |

Run one:

```bash
cd A-linq-count-property
dotnet tool restore
cd Lib
dotnet stryker
```

Add `--diag --verbosity trace` to see the injected code and the compiler errors. `run-all.sh` runs every repro and every config, and writes the reports to `<repro>/out/<config>/`.

License: MIT ([LICENSE](LICENSE)).
