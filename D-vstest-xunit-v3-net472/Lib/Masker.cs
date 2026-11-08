namespace Lib;

public static class Masker
{
    // Shows only the last 4 characters, as OpenUrzednik's SensitiveValue.Mask does.
    public static string Mask(string value)
    {
        int hidden = value.Length > 4 ? value.Length - 4 : value.Length;
#if NET
        return new string('*', hidden) + value.Substring(hidden);
#else
        char[] chars = new char[value.Length];
        for (int i = 0; i < hidden; i++)
        {
            chars[i] = '*';
        }

        value.CopyTo(hidden, chars, hidden, value.Length - hidden);
        return new string(chars);
#endif
    }
}
