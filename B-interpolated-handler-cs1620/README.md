# B: String mutant of `string.Create(provider, $"…")` never compiles (CS1620)

`Lib/Handler.cs` has `Describe` (the bug), which uses `string.Create(CultureInfo.InvariantCulture, $"…")`, and `DescribeControl`, which builds the same string with `string.Format`.

```bash
dotnet tool restore
cd Lib
dotnet stryker
```

Result on 5.0.0:

| Line | Mutant | Expected | Actual |
|---|---|---|---|
| 10 | String `$""` | Killed, or not generated | **CompileError** |
| 14 (control) | String `""` | Killed | Killed |

The mutant is injected as `string.Create(provider, (IsActive(0) ? $"" : $"First is {items[0]}"))`. A conditional expression doesn't get the conversion to the `ref DefaultInterpolatedStringHandler` parameter, so the call fails with CS1620. The rollback blames the right mutant, so no other mutant is lost.
