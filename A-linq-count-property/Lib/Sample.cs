namespace Lib;

public static class Sample
{
    // Bug: the Linq mutant `items.Sum` (a method group) can't compile, and the
    // rollback also discards the compilable Equality mutant `items.Count != 1`.
    public static bool IsSingle(IReadOnlyList<int> items)
    {
        if (items.Count == 1)
            return true;
        return false;
    }

    // Control: the same code with the Linq mutant disabled before compilation;
    // the Equality mutant is tested and killed.
    public static bool IsSingleControl(IReadOnlyList<int> items)
    {
        // Stryker disable once Linq : control case for the repro
        if (items.Count == 1)
            return true;
        return false;
    }
}
