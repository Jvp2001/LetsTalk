namespace LetsTalk.Core.Helpers;

public static class StringExtensions
{
    public static string EndsWithAny(this string value, params string[] strings)
    {
        for (var index = 0; index < strings.Length; index++)
        {
            var s = strings[index];
            if (value.EndsWith(s))
            {
                return s;
            }
        }

        return null;
    }
    public static string RemoveIfEndsWithAny(this string value, params string[] strings)
    {
        for (var index = 0; index < strings.Length; index++)
        {
            var s = strings[index];
            if (value.EndsWith(s))
            {
                return value.Substring(0, value.Length - s.Length);
            }
        }

        return value;
    }
}
