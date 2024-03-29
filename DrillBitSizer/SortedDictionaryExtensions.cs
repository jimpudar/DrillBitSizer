// https://gist.github.com/R2D221/f0363e0ee7123c4d7693#file-sorteddictionaryextensions-cs
namespace System.Collections.Generic;

// based on http://stackoverflow.com/a/3486820/1858296
public static class SortedDictionaryExtensions
{
    private static Tuple<int, int> GetPossibleIndices<TKey, TValue>(SortedDictionary<TKey, TValue> dictionary, TKey key,
        bool strictlyDifferent, out List<TKey> list)
    {
        list = dictionary.Keys.ToList();
        var index = list.BinarySearch(key, dictionary.Comparer);
        if (index >= 0)
        {
            // exists
            if (strictlyDifferent)
                return Tuple.Create(index - 1, index + 1);
            return Tuple.Create(index, index);
        }

        // doesn't exist
        var indexOfBiggerNeighbour = ~index; //bitwise complement of the return value

        if (indexOfBiggerNeighbour == list.Count)
            // bigger than all elements
            return Tuple.Create(list.Count - 1, list.Count);

        if (indexOfBiggerNeighbour == 0)
        {
            // smaller than all elements
            return Tuple.Create(-1, 0);
        }

        // Between 2 elements
        var indexOfSmallerNeighbour = indexOfBiggerNeighbour - 1;
        return Tuple.Create(indexOfSmallerNeighbour, indexOfBiggerNeighbour);
    }

    public static TKey LowerKey<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary, TKey key)
    {
        List<TKey> list;
        var indices = GetPossibleIndices(dictionary, key, true, out list);
        if (indices.Item1 < 0)
            return default;

        return list[indices.Item1];
    }

    public static KeyValuePair<TKey, TValue> LowerEntry<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary,
        TKey key)
    {
        List<TKey> list;
        var indices = GetPossibleIndices(dictionary, key, true, out list);
        if (indices.Item1 < 0)
            return default;

        var newKey = list[indices.Item1];
        return new KeyValuePair<TKey, TValue>(newKey, dictionary[newKey]);
    }

    public static TKey FloorKey<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary, TKey key)
    {
        List<TKey> list;
        var indices = GetPossibleIndices(dictionary, key, false, out list);
        if (indices.Item1 < 0)
            return default;

        return list[indices.Item1];
    }

    public static KeyValuePair<TKey, TValue> FloorEntry<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary,
        TKey key)
    {
        List<TKey> list;
        var indices = GetPossibleIndices(dictionary, key, false, out list);
        if (indices.Item1 < 0)
            return default;

        var newKey = list[indices.Item1];
        return new KeyValuePair<TKey, TValue>(newKey, dictionary[newKey]);
    }

    public static TKey CeilingKey<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary, TKey key)
    {
        List<TKey> list;
        var indices = GetPossibleIndices(dictionary, key, false, out list);
        if (indices.Item2 == list.Count)
            return default;

        return list[indices.Item2];
    }

    public static KeyValuePair<TKey, TValue> CeilingEntry<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary,
        TKey key)
    {
        List<TKey> list;
        var indices = GetPossibleIndices(dictionary, key, false, out list);
        if (indices.Item2 == list.Count)
            return default;

        var newKey = list[indices.Item2];
        return new KeyValuePair<TKey, TValue>(newKey, dictionary[newKey]);
    }

    public static TKey HigherKey<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary, TKey key)
    {
        List<TKey> list;
        var indices = GetPossibleIndices(dictionary, key, true, out list);
        if (indices.Item2 == list.Count)
            return default;

        return list[indices.Item2];
    }

    public static KeyValuePair<TKey, TValue> HigherEntry<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary,
        TKey key)
    {
        List<TKey> list;
        var indices = GetPossibleIndices(dictionary, key, true, out list);
        if (indices.Item2 == list.Count)
            return default;

        var newKey = list[indices.Item2];
        return new KeyValuePair<TKey, TValue>(newKey, dictionary[newKey]);
    }
}