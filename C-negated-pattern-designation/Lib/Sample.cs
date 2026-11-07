namespace Lib;

public static class Sample
{
    // Bug: the Equality mutant `value is not { } v` can't compile (CS0128, CS0165),
    // and the rollback also discards the compilable Arithmetic mutant `v - 1`.
    public static int Next(int? value, int fallback)
    {
        if (value is { } v)
            return v + 1;
        return fallback * 2;
    }

    // Control: the same logic without a pattern variable; every mutant is tested.
    public static int NextControl(int? value, int fallback)
    {
        if (value.HasValue)
            return value.GetValueOrDefault() + 1;
        return fallback * 2;
    }
}
