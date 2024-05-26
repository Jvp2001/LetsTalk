using System;
using System.Collections.Concurrent;

namespace LetsTalk.Core.Helpers;

public static class Singleton<T>
    where T : new()
{
    private static ConcurrentDictionary<Type, T> instances = new();

    public static T Instance
    {
        get
        {
            return instances.GetOrAdd(typeof(T), (t) => new T());
        }
    }
}
