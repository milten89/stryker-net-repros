using System.Globalization;

namespace Lib;

public static class Handler
{
    // Bug: the String mutant `$""` is injected as `IsActive(n) ? $"" : $"..."`,
    // which loses the ref interpolated-string-handler conversion (CS1620).
    public static string Describe(IReadOnlyList<int> items) =>
        string.Create(CultureInfo.InvariantCulture, $"First is {items[0]}");

    // Control: the same string through string.Format; the String mutant is killed.
    public static string DescribeControl(IReadOnlyList<int> items) =>
        string.Format(CultureInfo.InvariantCulture, "First is {0}", items[0]);
}
