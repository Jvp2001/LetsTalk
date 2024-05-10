using System.Collections.Generic;

namespace AutoRegisterSourceGen;

public static class Extensions
{
    public static void TryAdd<T,K>(this IDictionary<T,K> dictionary, T key, K value)
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, value);
        }
    }
}
