# A: `Count` property mutated to `Sum`, and its neighbour discarded

`Lib/Sample.cs` has `IsSingle` (the bug) and `IsSingleControl`, which is the same code with `// Stryker disable once Linq` above the `if`.

```bash
dotnet tool restore
cd Lib
dotnet stryker                                              # default config
dotnet stryker --config-file stryker-config.ignore-linq.json   # "ignore-mutations": ["linq"]
```

Result on 5.0.0. Both configs give the same statuses:

| Line | Mutant | Expected | Actual |
|---|---|---|---|
| 9 | Equality `items.Count != 1` | Killed | **CompileError** |
| 9 | Linq `items.Sum` | not generated | CompileError |
| 19 (control) | Equality `items.Count != 1` | Killed | Killed |
| 19 (control) | Linq `items.Sum` | Ignored | Ignored |

The injected condition is `(IsActive(1) ? items.Count != 1 : (IsActive(2) ? items.Sum : items.Count) == 1)`. The compiler reports CS0019 on the `==`, and the rollback blames the enclosing Equality mutant. `ignore-mutations` filters mutants only after compilation, so it frees nothing. Only the disable comment prevents the injection.
