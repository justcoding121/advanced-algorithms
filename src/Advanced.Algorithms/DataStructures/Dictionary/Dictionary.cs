using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

/// <summary>
///     A dictionary implementation.
/// </summary>
/// <typeparam name="TK">The key datatype.</typeparam>
/// <typeparam name="TV">The value datatype.</typeparam>
public class Dictionary<TK, TV> : IEnumerable<KeyValuePair<TK, TV>>
{
    private readonly IDictionary<TK, TV> dictionary;

    /// <param name="type">The dictionary implementation to use.</param>
    /// <param name="initialBucketSize">The larger the bucket size lesser the collision, but memory matters!</param>
    public Dictionary(DictionaryType type = DictionaryType.SeparateChaining, int initialBucketSize = 2)
    {
        if (initialBucketSize < 2) throw new Exception("Bucket Size must be greater than 2.");

        if (type == DictionaryType.SeparateChaining)
            dictionary = new SeparateChainingDictionary<TK, TV>(initialBucketSize);
        else
            dictionary = new OpenAddressDictionary<TK, TV>(initialBucketSize);
    }

    /// <summary>
    ///     The number of items in this hashset.
    /// </summary>
    public int Count => dictionary.Count;

    /// <summary>
    ///     Get/set value for given key.
    ///     Time complexity: O(1) amortized.
    /// </summary>
    public TV this[TK key]
    {
        get => dictionary[key];
        set => dictionary[key] = value;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
    {
        return dictionary.GetEnumerator();
    }

    /// <summary>
    ///     Does this dictionary contains the given key.
    ///     Time complexity: O(1) amortized.
    /// </summary>
    /// <param name="value">The key to check.</param>
    /// <returns>True if this dictionary contains the given key.</returns>
    public bool ContainsKey(TK key)
    {
        return dictionary.ContainsKey(key);
    }

    /// <summary>
    ///     Add a new key for given value.
    ///     Time complexity: O(1) amortized.
    /// </summary>
    /// <param name="key">The key to add.</param>
    /// <param name="value">The value for the given key.</param>
    public void Add(TK key, TV value)
    {
        dictionary.Add(key, value);
    }

    /// <summary>
    ///     Remove the given key along with its value.
    ///     Time complexity: O(1) amortized.
    /// </summary>
    /// <param name="key">The key to remove.</param>
    public void Remove(TK key)
    {
        dictionary.Remove(key);
    }

    /// <summary>
    ///     Clear the dictionary.
    ///     Time complexity: O(1).
    /// </summary>
    public void Clear()
    {
        dictionary.Clear();
    }
}

internal interface IDictionary<TK, TV> : IEnumerable<KeyValuePair<TK, TV>>
{
    TV this[TK key] { get; set; }

    int Count { get; }

    bool ContainsKey(TK key);
    void Add(TK key, TV value);
    void Remove(TK key);
    void Clear();
}

/// <summary>
///     The dictionary implementation type.
/// </summary>
public enum DictionaryType
{
    SeparateChaining,
    OpenAddressing
}