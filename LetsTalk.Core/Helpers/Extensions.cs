using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LetsTalk.Core.Helpers;

public static class Extensions
{

    public static void AddAll<T>(this ObservableCollection<T> source, IEnumerable<T> other)
    {
        foreach (var item in other)
        {
            source.Add(item);
        }
    }
    public static void AddAll<T>(this ObservableCollection<T> source, params T[] other)
    {
        foreach (var item in other)
        {
            source.Add(item);
        }
    }


}
