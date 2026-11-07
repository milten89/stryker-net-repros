# C: `x is { } v` negated to `is not`, and its neighbour discarded

`Lib/Sample.cs` has `Next` (the bug), which uses a pattern variable, and `NextControl`, which has the same logic without one (`HasValue` and `GetValueOrDefault()`).

```bash
dotnet tool restore
cd Lib
dotnet stryker
```

Result on 5.0.0:

| Line | Mutant | Expected | Actual |
|---|---|---|---|
| 9 | Equality `value is not { } v` | not generated | CompileError |
| 10 | Arithmetic `v - 1` | Killed (by `Next_Value_Increments`) | **CompileError** |
| 11 | Arithmetic `fallback / 2` | Killed | Killed |
| 17-19 (control) | Negate, Arithmetic ×2 | Killed | Killed |

The injected condition is `(IsActive(2) ? value is not { } v : value is { } v)`. It declares `v` twice (CS0128, stryker-net#3597), and the negated pattern leaves `v` unassigned at `v + 1` (CS0165). The CS0165 rollback finds no mutant that erases an assignment, so it walks up from the use and discards the Arithmetic mutant there, which compiles on its own.
