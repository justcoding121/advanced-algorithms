using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Foundation;

/// <summary>
///     A sorted Dictionary implementation using balanced binary search tree. IEnumerable will enumerate in sorted order.
///     This may be better than regular Dictionary implementation which can give o(K) in worst case (but O(1) amortized
///     when collisions K is avoided).
/// </summary>
/// <typeparam name="TK">The key datatype.</typeparam>
/// <typeparam name="TV">The value datatype.</typeparam>
public class OrderedDictionary<TK, TV> : IEnumerable<KeyValuePair<TK, TV>> where TK : IComparable
{
    //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
    private readonly RedBlackTree<OrderedKeyValuePair<TK, TV>> binarySearchTree;

    public OrderedDictionary()
    {
        binarySearchTree = new RedBlackTree<OrderedKeyValuePair<TK, TV>>();
    }

    /// <summary>
    ///     Initialize the dictionary with given key value pairs sorted by key.
    ///     Time complexity: log(n).
    /// </summary>
    public OrderedDictionary(IEnumerable<KeyValuePair<TK, TV>> sortedKeyValuePairs)
    {
        binarySearchTree =
            new RedBlackTree<OrderedKeyValuePair<TK, TV>>(sortedKeyValuePairs.Select(x =>
                new OrderedKeyValuePair<TK, TV>(x.Key, x.Value)));
    }

    public int Count => binarySearchTree.Count;

    /// <summary>
    ///     Get/set value for given key.
    ///     Time complexity: O(log(n)).
    /// </summary>
    public TV this[TK key]
    {
        get
        {
            var node = binarySearchTree.FindNode(new OrderedKeyValuePair<TK, TV>(key, default));
            if (node == null) throw new Exception("Key not found.");

            return node.Value.Value;
        }
        set
        {
            if (ContainsKey(key)) Remove(key);

            Add(key, value);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
    {
        return new SortedDictionaryEnumerator<TK, TV>(binarySearchTree);
    }

    /// <summary>
    ///     Does this dictionary contains the given key.
    ///     Time complexity: O(log(n)).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if this dictionary contains the given key.</returns>
    public bool ContainsKey(TK key)
    {
        return binarySearchTree.HasItem(new OrderedKeyValuePair<TK, TV>(key, default));
    }

    /// <summary>
    ///     Add a new value for given key.
    ///     Time complexity: O(log(n)).
    ///     Returns the position (index) of the key in sorted order of this OrderedDictionary.
    /// </summary>
    public int Add(TK key, TV value)
    {
        return binarySearchTree.Insert(new OrderedKeyValuePair<TK, TV>(key, value));
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public KeyValuePair<TK, TV> ElementAt(int index)
    {
        return binarySearchTree.ElementAt(index).ToKeyValuePair();
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public int IndexOf(TK key)
    {
        return binarySearchTree.IndexOf(new OrderedKeyValuePair<TK, TV>(key, default));
    }

    /// <summary>
    ///     Remove the given key if it exists.
    ///     Time complexity: O(log(n)).
    ///     Returns the position (index) of the removed key if removed. Otherwise returns -1.
    /// </summary>
    public int Remove(TK key)
    {
        return binarySearchTree.Delete(new OrderedKeyValuePair<TK, TV>(key, default));
    }

    /// <summary>
    ///     Remove the element at given index.
    ///     Time complexity: O(log(n)).
    /// </summary>
    public KeyValuePair<TK, TV> RemoveAt(int index)
    {
        return binarySearchTree.RemoveAt(index).ToKeyValuePair();
    }

    /// <summary>
    ///     Return the next higher key-value pair after given key in this dictionary.
    ///     Time complexity: O(log(n)).
    /// </summary>
    /// <returns>Null if the given key does'nt exist or next key does'nt exist.</returns>
    public KeyValuePair<TK, TV> NextHigher(TK key)
    {
        var next = binarySearchTree.NextHigher(new OrderedKeyValuePair<TK, TV>(key, default));

        if (next.Equals(default(OrderedKeyValuePair<TK, TV>))) return default;

        return new KeyValuePair<TK, TV>(next.Key, next.Value);
    }

    /// <summary>
    ///     Return the next lower key-value pair before given key in this dictionary.
    ///     Time complexity: O(log(n)).
    /// </summary>
    /// <returns>Null if the given key does'nt exist or previous key does'nt exist.</returns>
    public KeyValuePair<TK, TV> NextLower(TK key)
    {
        var prev = binarySearchTree.NextLower(new OrderedKeyValuePair<TK, TV>(key, default));

        if (prev.Equals(default(OrderedKeyValuePair<TK, TV>))) return default;

        return new KeyValuePair<TK, TV>(prev.Key, prev.Value);
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public KeyValuePair<TK, TV> Max()
    {
        var max = binarySearchTree.Max();
        return max.Equals(default(OrderedKeyValuePair<TK, TV>))
            ? default
            : max.ToKeyValuePair();
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public KeyValuePair<TK, TV> Min()
    {
        var min = binarySearchTree.Min();
        return min.Equals(default(OrderedKeyValuePair<TK, TV>))
            ? default
            : min.ToKeyValuePair();
    }


    /// <summary>
    ///     Clear the dictionary.
    ///     Time complexity: O(log(n)).
    /// </summary>
    internal void Clear()
    {
        binarySearchTree.Clear();
    }

    /// <summary>
    ///     Descending enumerable.
    /// </summary>
    public IEnumerable<KeyValuePair<TK, TV>> AsEnumerableDesc()
    {
        return GetEnumeratorDesc().AsEnumerable();
    }

    public IEnumerator<KeyValuePair<TK, TV>> GetEnumeratorDesc()
    {
        return new SortedDictionaryEnumerator<TK, TV>(binarySearchTree, false);
    }
}

internal struct OrderedKeyValuePair<TK, TV> : IComparable
    where TK : IComparable
{
    internal TK Key { get; }
    internal TV Value { get; set; }

    internal OrderedKeyValuePair(TK key, TV value)
    {
        Key = key;
        Value = value;
    }

    public KeyValuePair<TK, TV> ToKeyValuePair()
    {
        return new KeyValuePair<TK, TV>(Key, Value);
    }

    public int CompareTo(object obj)
    {
        if (obj is OrderedKeyValuePair<TK, TV> itemToComare) return Key.CompareTo(itemToComare.Key);

        throw new ArgumentException("Compare object is nu");
    }

    public override bool Equals(object obj)
    {
        return Key.Equals(((OrderedKeyValuePair<TK, TV>)obj).Key);
    }

    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }
}

internal class SortedDictionaryEnumerator<TK, TV> : IEnumerator<KeyValuePair<TK, TV>> where TK : IComparable
{
    private bool asc;

    private RedBlackTree<OrderedKeyValuePair<TK, TV>> bst;
    private IEnumerator<OrderedKeyValuePair<TK, TV>> enumerator;

    internal SortedDictionaryEnumerator(RedBlackTree<OrderedKeyValuePair<TK, TV>> bst, bool asc = true)
    {
        this.bst = bst;
        enumerator = asc ? bst.GetEnumerator() : bst.GetEnumeratorDesc();
    }

    public bool MoveNext()
    {
        return enumerator.MoveNext();
    }

    public void Reset()
    {
        enumerator.Reset();
    }

    object IEnumerator.Current => Current;

    public KeyValuePair<TK, TV> Current => new KeyValuePair<TK, TV>(enumerator.Current.Key, enumerator.Current.Value);

    public void Dispose()
    {
        bst = null;
        enumerator = null;
    }
}