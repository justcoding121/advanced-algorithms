using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A sparse set implementation.
/// </summary>
public class SparseSet : IEnumerable<int>
{
    private readonly int[] dense;
    private readonly int[] sparse;

    public SparseSet(int maxVal, int capacity)
    {
        sparse = Enumerable.Repeat(-1, maxVal + 1).ToArray();
        dense = Enumerable.Repeat(-1, capacity).ToArray();
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<int> GetEnumerator()
    {
        return dense.Take(Count).GetEnumerator();
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Add(int value)
    {
        if (value < 0) throw new ArgumentException("Negative values not supported.");

        if (value >= sparse.Length) throw new ArgumentException("Item is greater than max value.");

        if (HasItem(value)) throw new ArgumentException("Item already exists.");

        if (Count >= dense.Length) throw new InvalidOperationException("Set reached its capacity.");

        sparse[value] = Count;
        dense[Count] = value;
        Count++;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Remove(int value)
    {
        if (value < 0) throw new ArgumentException("Negative values not supported.");

        if (value >= sparse.Length) throw new ArgumentException("Item is greater than max value.");

        if (!HasItem(value)) throw new ArgumentException("Item do not exist.");

        //find element
        var index = sparse[value];
        sparse[value] = -1;

        //replace index with last value of dense
        var lastVal = dense[Count - 1];
        dense[index] = lastVal;
        dense[Count - 1] = -1;

        //update sparse for lastVal
        sparse[lastVal] = index;

        Count--;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasItem(int value)
    {
        if (value < 0 || value >= sparse.Length) return false;

        var index = sparse[value];
        return index != -1 && index < Count && dense[index] == value;
    }

    /// <summary>
    ///     Time complexity: O(n) where n is the number of items currently in the set.
    /// </summary>
    public void Clear()
    {
        for (var i = 0; i < Count; i++)
        {
            sparse[dense[i]] = -1;
            dense[i] = -1;
        }

        Count = 0;
    }
}