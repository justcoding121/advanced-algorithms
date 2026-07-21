›	
aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Combinatorics\Combination.cs‚using System.Collections.Generic;

namespace Advanced.Algorithms.Combinatorics;

/// <summary>
///     Combination generator (nCr).
/// </summary>
public class Combination
{
    public static List<List<T>> Find<T>(List<T> n, int r, bool withRepetition = false)
    {
        var result = new List<List<T>>();

        Recurse(n, r, withRepetition, 0, new List<T>(), new HashSet<int>(), result);

        return result;
    }

    private static void Recurse<T>(List<T> n, int r, bool withRepetition,
        int k, List<T> prefix, HashSet<int> prefixIndices,
        List<List<T>> result)
    {
        if (prefix.Count == r)
        {
            result.Add(new List<T>(prefix));
            return;
        }

        for (var j = k; j < n.Count; j++)
        {
            if (prefixIndices.Contains(j) && !withRepetition) continue;

            prefix.Add(n[j]);
            prefixIndices.Add(j);

            Recurse(n, r, withRepetition, j, prefix, prefixIndices, result);

            prefix.RemoveAt(prefix.Count - 1);
            prefixIndices.Remove(j);
        }
    }
}ParseOptions.0.json–	
aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Combinatorics\Permutation.cs’using System.Collections.Generic;

namespace Advanced.Algorithms.Combinatorics;

/// <summary>
///     Permutation generator (nPr).
/// </summary>
public class Permutation
{
    public static List<List<T>> Find<T>(List<T> n, int r, bool withRepetition = false)
    {
        var result = new List<List<T>>();

        Recurse(n, r, withRepetition, new List<T>(), new HashSet<int>(), result);

        return result;
    }

    private static void Recurse<T>(List<T> n, int r, bool withRepetition,
        List<T> prefix, HashSet<int> prefixIndices,
        List<List<T>> result)
    {
        if (prefix.Count == r)
        {
            result.Add(new List<T>(prefix));
            return;
        }

        for (var j = 0; j < n.Count; j++)
        {
            if (prefixIndices.Contains(j) && !withRepetition) continue;

            prefix.Add(n[j]);
            prefixIndices.Add(j);

            Recurse(n, r, withRepetition, prefix, prefixIndices, result);

            prefix.RemoveAt(prefix.Count - 1);
            prefixIndices.Remove(j);
        }
    }
}ParseOptions.0.jsonú
\D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Combinatorics\Subset.cs¶using System.Collections.Generic;

namespace Advanced.Algorithms.Combinatorics;

/// <summary>
///     Subset generator.
/// </summary>
public class Subset
{
    public static List<List<T>> Find<T>(List<T> input)
    {
        var result = new List<List<T>>();

        Recurse(input, 0, new List<T>(), new HashSet<int>(), result);

        return result;
    }

    private static void Recurse<T>(List<T> input,
        int k, List<T> prefix, HashSet<int> prefixIndices,
        List<List<T>> result)
    {
        result.Add(new List<T>(prefix));

        for (var j = k; j < input.Count; j++)
        {
            if (prefixIndices.Contains(j)) continue;

            prefix.Add(input[j]);
            prefixIndices.Add(j);

            Recurse(input, j + 1, prefix, prefixIndices, result);

            prefix.RemoveAt(prefix.Count - 1);
            prefixIndices.Remove(j);
        }
    }
}ParseOptions.0.json–
aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Compression\HuffmanCoding.cs’using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Compression;

/// <summary>
///     A huffman coding implementation using Fibonacci Min Heap.
/// </summary>
public class HuffmanCoding<T>
{
    /// <summary>
    ///     Returns a dictionary of chosen encoding bytes for each distinct T.
    /// </summary>
    public Dictionary<T, byte[]> Compress(T[] input)
    {
        var frequencies = ComputeFrequency(input);

        var minHeap = new BHeap<FrequencyWrap>();

        foreach (var frequency in frequencies)
            minHeap.Insert(new FrequencyWrap(
                frequency.Key, frequency.Value));

        while (minHeap.Count > 1)
        {
            var a = minHeap.Extract();
            var b = minHeap.Extract();

            var newNode = new FrequencyWrap(
                default, a.Frequency + b.Frequency);

            newNode.Left = a;
            newNode.Right = b;

            minHeap.Insert(newNode);
        }

        var root = minHeap.Extract();

        var result = new Dictionary<T, byte[]>();

        Dfs(root, new List<byte>(), result);

        return result;
    }

    /// <summary>
    ///     Now gather the codes.
    /// </summary>
    private void Dfs(FrequencyWrap currentNode, List<byte> pathStack, Dictionary<T, byte[]> result)
    {
        if (currentNode.IsLeaf)
        {
            result.Add(currentNode.Item, pathStack.ToArray());
            return;
        }

        if (currentNode.Left != null)
        {
            pathStack.Add(0);
            Dfs(currentNode.Left, pathStack, result);
            pathStack.RemoveAt(pathStack.Count - 1);
        }

        if (currentNode.Right != null)
        {
            pathStack.Add(1);
            Dfs(currentNode.Right, pathStack, result);
            pathStack.RemoveAt(pathStack.Count - 1);
        }
    }

    /// <summary>
    ///     Computes frequencies of each of T in given input.
    /// </summary>
    private Dictionary<T, int> ComputeFrequency(T[] input)
    {
        var result = new Dictionary<T, int>();

        foreach (var item in input)
        {
            if (!result.ContainsKey(item))
            {
                result.Add(item, 1);
                continue;
            }

            result[item]++;
        }

        return result;
    }

    private class FrequencyWrap : IComparable
    {
        public FrequencyWrap(T item, int frequency)
        {
            Item = item;
            Frequency = frequency;
        }

        public T Item { get; }
        public int Frequency { get; }

        public FrequencyWrap Left { get; set; }

        public FrequencyWrap Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public int CompareTo(object obj)
        {
            return Frequency.CompareTo(((FrequencyWrap)obj).Frequency);
        }
    }
}ParseOptions.0.json«
lD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Dictionary\Dictionary.cs¡using System;
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
}ParseOptions.0.json∏J
wD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Dictionary\OpenAddressDictionary.csßIusing System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class OpenAddressDictionary<TK, TV> : IDictionary<TK, TV>
{
    private readonly int initialBucketSize;
    private DictionaryKeyValuePair<TK, TV>[] hashArray;

    internal OpenAddressDictionary(int initialBucketSize = 2)
    {
        this.initialBucketSize = initialBucketSize;
        hashArray = new DictionaryKeyValuePair<TK, TV>[initialBucketSize];
    }

    private int BucketSize => hashArray.Length;

    public int Count { get; private set; }

    public TV this[TK key]
    {
        get => GetValue(key);
        set => SetValue(key, value);
    }


    public bool ContainsKey(TK key)
    {
        var hashCode = GetHash(key);
        var index = hashCode % BucketSize;

        if (hashArray[index] == null) return false;

        var current = hashArray[index];

        //keep track of this so that we won't circle around infinitely
        var hitKey = current.Key;

        while (current != null)
        {
            if (current.Key.Equals(key)) return true;

            index++;

            //wrap around
            if (index == BucketSize)
                index = 0;

            current = hashArray[index];

            //reached original hit again
            if (current != null && current.Key.Equals(hitKey)) break;
        }

        return false;
    }

    public void Add(TK key, TV value)
    {
        Grow();

        var hashCode = GetHash(key);

        var index = hashCode % BucketSize;

        if (hashArray[index] == null)
        {
            hashArray[index] = new DictionaryKeyValuePair<TK, TV>(key, value);
        }
        else
        {
            var current = hashArray[index];
            //keep track of this so that we won't circle around infinitely
            var hitKey = current.Key;

            while (current != null)
            {
                if (current.Key.Equals(key)) throw new Exception("Duplicate key");

                index++;

                //wrap around
                if (index == BucketSize)
                    index = 0;

                current = hashArray[index];

                if (current != null && current.Key.Equals(hitKey)) throw new Exception("Dictionary is full");
            }

            hashArray[index] = new DictionaryKeyValuePair<TK, TV>(key, value);
        }

        Count++;
    }

    public void Remove(TK key)
    {
        var hashCode = GetHash(key);
        var curIndex = hashCode % BucketSize;

        if (hashArray[curIndex] == null) throw new Exception("No such item for given key");

        var current = hashArray[curIndex];

        //prevent circling around infinitely
        var hitKey = current.Key;

        DictionaryKeyValuePair<TK, TV> target = null;

        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                target = current;
                break;
            }

            curIndex++;

            //wrap around
            if (curIndex == BucketSize)
                curIndex = 0;

            current = hashArray[curIndex];

            if (current != null && current.Key.Equals(hitKey)) throw new Exception("No such item for given key");
        }

        //remove
        if (target == null)
        {
            throw new Exception("No such item for given key");
        }

        //delete this element
        hashArray[curIndex] = null;

        //now time to cleanup subsequent broken hash elements due to this emptied cell
        curIndex++;

        //wrap around
        if (curIndex == BucketSize)
            curIndex = 0;

        current = hashArray[curIndex];

        //until an empty cell
        while (current != null)
        {
            //delete current
            hashArray[curIndex] = null;

            //add current back to table
            Add(current.Key, current.Value);
            Count--;

            curIndex++;

            //wrap around
            if (curIndex == BucketSize)
                curIndex = 0;

            current = hashArray[curIndex];
        }

        Count--;

        Shrink();
    }


    public void Clear()
    {
        hashArray = new DictionaryKeyValuePair<TK, TV>[initialBucketSize];
        Count = 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
    {
        return new OpenAddressDictionaryEnumerator<TK, TV>(hashArray, hashArray.Length);
    }


    private void SetValue(TK key, TV value)
    {
        var index = GetHash(key) % BucketSize;

        if (hashArray[index] == null)
        {
            Add(key, value);
        }
        else
        {
            var current = hashArray[index];
            var hitKey = current.Key;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    Remove(key);
                    Add(key, value);
                    return;
                }

                index++;

                //wrap around
                if (index == BucketSize)
                    index = 0;

                current = hashArray[index];

                //reached original hit again
                if (current != null && current.Key.Equals(hitKey)) throw new Exception("Item not found");
            }
        }

        throw new Exception("Item not found");
    }

    private TV GetValue(TK key)
    {
        var index = GetHash(key) % BucketSize;

        if (hashArray[index] == null) throw new Exception("Item not found");

        var current = hashArray[index];
        var hitKey = current.Key;

        while (current != null)
        {
            if (current.Key.Equals(key)) return current.Value;

            index++;

            //wrap around
            if (index == BucketSize)
                index = 0;

            current = hashArray[index];

            //reached original hit again
            if (current != null && current.Key.Equals(hitKey)) throw new Exception("Item not found");
        }

        throw new Exception("Item not found");
    }

    private void Grow()
    {
        if (BucketSize * 0.7 <= Count)
        {
            var orgBucketSize = BucketSize;
            var currentArray = hashArray;

            //increase array size exponentially on demand
            hashArray = new DictionaryKeyValuePair<TK, TV>[BucketSize * 2];

            for (var i = 0; i < orgBucketSize; i++)
            {
                var current = currentArray[i];

                if (current != null)
                {
                    Add(current.Key, current.Value);
                    Count--;
                }
            }

            currentArray = null;
        }
    }


    private void Shrink()
    {
        if (Count <= BucketSize * 0.3 && BucketSize / 2 > initialBucketSize)
        {
            var orgBucketSize = BucketSize;

            var currentArray = hashArray;

            //reduce array by half logarithamic
            hashArray = new DictionaryKeyValuePair<TK, TV>[BucketSize / 2];

            for (var i = 0; i < orgBucketSize; i++)
            {
                var current = currentArray[i];

                if (current != null)
                {
                    Add(current.Key, current.Value);
                    Count--;
                }
            }

            currentArray = null;
        }
    }

    private int GetHash(TK key)
    {
        return Math.Abs(key.GetHashCode());
    }
}

internal class DictionaryKeyValuePair<TK, TV>
{
    internal TK Key;
    internal TV Value;

    internal DictionaryKeyValuePair(TK key, TV value)
    {
        Key = key;
        Value = value;
    }
}

internal class OpenAddressDictionaryEnumerator<TK, TV> : IEnumerator<KeyValuePair<TK, TV>>
{
    private readonly int length;
    internal DictionaryKeyValuePair<TK, TV>[] HashArray;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    private int position = -1;

    internal OpenAddressDictionaryEnumerator(DictionaryKeyValuePair<TK, TV>[] hashArray, int length)
    {
        this.length = length;
        HashArray = hashArray;
    }

    public bool MoveNext()
    {
        position++;

        while (position < length && HashArray[position] == null)
            position++;

        return position < length;
    }

    public void Reset()
    {
        position = -1;
    }

    object IEnumerator.Current => Current;

    public KeyValuePair<TK, TV> Current
    {
        get
        {
            try
            {
                return new KeyValuePair<TK, TV>(HashArray[position].Key, HashArray[position].Value);
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    public void Dispose()
    {
        HashArray = null;
    }
}ParseOptions.0.jsonŸ@
sD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Dictionary\OrderedDictionary.csÃ?using System;
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
}ParseOptions.0.json∆I
|D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Dictionary\SeparateChainingDictionary.cs∞Husing System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class SeparateChainingDictionary<TK, TV> : IDictionary<TK, TV>
{
    private const double Tolerance = 0.1;
    private readonly int initialBucketSize;
    private int filledBuckets;

    private DoublyLinkedList<KeyValuePair<TK, TV>>[] hashArray;


    public SeparateChainingDictionary(int initialBucketSize = 3)
    {
        this.initialBucketSize = initialBucketSize;
        hashArray = new DoublyLinkedList<KeyValuePair<TK, TV>>[initialBucketSize];
    }

    private int BucketSize => hashArray.Length;

    public int Count { get; private set; }

    public TV this[TK key]
    {
        get => GetValue(key);
        set => SetValue(key, value);
    }

    public bool ContainsKey(TK key)
    {
        var index = Math.Abs(key.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) return false;

        var current = hashArray[index].Head;

        while (current != null)
        {
            if (current.Data.Key.Equals(key)) return true;

            current = current.Next;
        }

        return false;
    }

    public void Add(TK key, TV value)
    {
        Grow();

        var index = Math.Abs(key.GetHashCode()) % BucketSize;

        if (hashArray[index] == null)
        {
            hashArray[index] = new DoublyLinkedList<KeyValuePair<TK, TV>>();
            hashArray[index].InsertFirst(new KeyValuePair<TK, TV>(key, value));
            filledBuckets++;
        }
        else
        {
            var current = hashArray[index].Head;

            while (current != null)
            {
                if (current.Data.Key.Equals(key)) throw new Exception("Duplicate key");

                current = current.Next;
            }

            hashArray[index].InsertFirst(new KeyValuePair<TK, TV>(key, value));
        }

        Count++;
    }

    public void Remove(TK key)
    {
        var index = Math.Abs(key.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) throw new Exception("No such item for given key");

        var current = hashArray[index].Head;

        //TODO merge both search and remove to a single loop here!
        DoublyLinkedListNode<KeyValuePair<TK, TV>> item = null;
        while (current != null)
        {
            if (current.Data.Key.Equals(key))
            {
                item = current;
                break;
            }

            current = current.Next;
        }

        //remove
        if (item == null)
        {
            throw new Exception("No such item for given key");
        }

        hashArray[index].Delete(item);

        //if list is empty mark bucket as null
        if (hashArray[index].Head == null)
        {
            hashArray[index] = null;
            filledBuckets--;
        }

        Count--;

        Shrink();
    }

    public void Clear()
    {
        hashArray = new DoublyLinkedList<KeyValuePair<TK, TV>>[initialBucketSize];
        Count = 0;
        filledBuckets = 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
    {
        return new SeparateChainingDictionaryEnumerator<TK, TV>(hashArray, BucketSize);
    }

    private void SetValue(TK key, TV value)
    {
        var index = Math.Abs(key.GetHashCode()) % BucketSize;

        if (hashArray[index] == null)
        {
            Add(key, value);
        }
        else
        {
            var current = hashArray[index].Head;

            while (current != null)
            {
                if (current.Data.Key.Equals(key))
                {
                    Remove(key);
                    Add(key, value);
                    return;
                }

                current = current.Next;
            }
        }

        throw new Exception("Item not found");
    }

    private TV GetValue(TK key)
    {
        var index = Math.Abs(key.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) throw new Exception("Item not found");

        var current = hashArray[index].Head;

        while (current != null)
        {
            if (current.Data.Key.Equals(key)) return current.Data.Value;

            current = current.Next;
        }

        throw new Exception("Item not found");
    }

    private void Grow()
    {
        if (filledBuckets >= BucketSize * 0.7)
        {
            filledBuckets = 0;
            //increase array size exponentially on demand
            var newBucketSize = BucketSize * 2;

            var biggerArray = new DoublyLinkedList<KeyValuePair<TK, TV>>[newBucketSize];

            for (var i = 0; i < BucketSize; i++)
            {
                var item = hashArray[i];

                //hashcode changes when bucket size changes
                if (item != null)
                    if (item.Head != null)
                    {
                        var current = item.Head;

                        //find new location for each item
                        while (current != null)
                        {
                            var next = current.Next;

                            var newIndex = Math.Abs(current.Data.Key.GetHashCode()) % newBucketSize;

                            if (biggerArray[newIndex] == null)
                            {
                                filledBuckets++;
                                biggerArray[newIndex] = new DoublyLinkedList<KeyValuePair<TK, TV>>();
                            }

                            biggerArray[newIndex].InsertFirst(current);

                            current = next;
                        }
                    }
            }

            hashArray = biggerArray;
        }
    }

    private void Shrink()
    {
        if (Math.Abs(filledBuckets - BucketSize * 0.3) < Tolerance && BucketSize / 2 > initialBucketSize)
        {
            filledBuckets = 0;
            //reduce array by half 
            var newBucketSize = BucketSize / 2;

            var smallerArray = new DoublyLinkedList<KeyValuePair<TK, TV>>[newBucketSize];

            for (var i = 0; i < BucketSize; i++)
            {
                var item = hashArray[i];

                //hashcode changes when bucket size changes
                if (item?.Head != null)
                {
                    var current = item.Head;

                    //find new location for each item
                    while (current != null)
                    {
                        var next = current.Next;

                        var newIndex = Math.Abs(current.Data.Key.GetHashCode()) % newBucketSize;

                        if (smallerArray[newIndex] == null)
                        {
                            filledBuckets++;
                            smallerArray[newIndex] = new DoublyLinkedList<KeyValuePair<TK, TV>>();
                        }

                        smallerArray[newIndex].InsertFirst(current);

                        current = next;
                    }
                }
            }

            hashArray = smallerArray;
        }
    }
}

internal class SeparateChainingDictionaryEnumerator<TK, TV> : IEnumerator<KeyValuePair<TK, TV>>
{
    private DoublyLinkedListNode<KeyValuePair<TK, TV>> currentNode;
    internal DoublyLinkedList<KeyValuePair<TK, TV>>[] HashList;

    private readonly int length;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    private int position = -1;

    internal SeparateChainingDictionaryEnumerator(DoublyLinkedList<KeyValuePair<TK, TV>>[] hashList, int length)
    {
        this.length = length;
        HashList = hashList;
    }

    public bool MoveNext()
    {
        if (currentNode?.Next != null)
        {
            currentNode = currentNode.Next;
            return true;
        }

        while (currentNode?.Next == null)
        {
            position++;

            if (position < length)
            {
                if (HashList[position] == null)
                    continue;

                currentNode = HashList[position].Head;

                if (currentNode == null)
                    continue;

                return true;
            }

            break;
        }

        return false;
    }

    public void Reset()
    {
        position = -1;
        currentNode = null;
    }

    object IEnumerator.Current => Current;

    public KeyValuePair<TK, TV> Current
    {
        get
        {
            try
            {
                return new KeyValuePair<TK, TV>(currentNode.Data.Key, currentNode.Data.Value);
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    public void Dispose()
    {
        HashList = null;
    }
}ParseOptions.0.json∫9
rD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Graph\AdjacencyList\DiGraph.csÆ8using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

/// <summary>
///     A directed graph implementation.
///     IEnumerable enumerates all vertices.
/// </summary>
public class DiGraph<T> : IGraph<T>, IDiGraph<T>, IEnumerable<T>
{
    public DiGraph()
    {
        Vertices = new Dictionary<T, DiGraphVertex<T>>();
    }

    private Dictionary<T, DiGraphVertex<T>> Vertices { get; }

    /// <summary>
    ///     Return a reference vertex to start traversing Vertices
    ///     Time complexity: O(1).
    /// </summary>
    private DiGraphVertex<T> ReferenceVertex
    {
        get
        {
            using (var enumerator = Vertices.GetEnumerator())
            {
                if (enumerator.MoveNext()) return enumerator.Current.Value;
            }

            return null;
        }
    }

    IDiGraphVertex<T> IDiGraph<T>.ReferenceVertex => ReferenceVertex;

    public IDiGraphVertex<T> GetVertex(T value)
    {
        return Vertices[value];
    }

    IDiGraph<T> IDiGraph<T>.Clone()
    {
        return Clone();
    }

    IEnumerable<IDiGraphVertex<T>> IDiGraph<T>.VerticesAsEnumberable => Vertices.Select(x => x.Value);

    public IEnumerator GetEnumerator()
    {
        return Vertices.Select(x => x.Key).GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator() as IEnumerator<T>;
    }

    public int VerticesCount => Vertices.Count;
    public bool IsWeightedGraph => false;
    IGraphVertex<T> IGraph<T>.ReferenceVertex => ReferenceVertex;

    /// <summary>
    ///     Do we have an edge between the given source and destination?
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasEdge(T source, T dest)
    {
        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new ArgumentException("source or destination is not in this graph.");

        return Vertices[source].OutEdges.Contains(Vertices[dest])
               && Vertices[dest].InEdges.Contains(Vertices[source]);
    }

    public bool ContainsVertex(T value)
    {
        return Vertices.ContainsKey(value);
    }

    IGraphVertex<T> IGraph<T>.GetVertex(T key)
    {
        return Vertices[key];
    }

    IGraph<T> IGraph<T>.Clone()
    {
        return Clone();
    }

    public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => Vertices.Select(x => x.Value);


    /// <summary>
    ///     Add a new vertex to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        var newVertex = new DiGraphVertex<T>(value);

        Vertices.Add(value, newVertex);
    }

    /// <summary>
    ///     Remove an existing vertex frm graph.
    ///     Time complexity: O(V) where V is the total number of vertices in this graph.
    /// </summary>
    public void RemoveVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        if (!Vertices.ContainsKey(value)) throw new Exception("Vertex not in this graph.");

        foreach (var vertex in Vertices[value].InEdges) vertex.OutEdges.Remove(Vertices[value]);

        foreach (var vertex in Vertices[value].OutEdges) vertex.InEdges.Remove(Vertices[value]);

        Vertices.Remove(value);
    }

    /// <summary>
    ///     Add an edge from source to destination vertex.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new Exception("Source or Destination Vertex is not in this graph.");

        if (Vertices[source].OutEdges.Contains(Vertices[dest]) || Vertices[dest].InEdges.Contains(Vertices[source]))
            throw new Exception("Edge already exists.");

        Vertices[source].OutEdges.Add(Vertices[dest]);
        Vertices[dest].InEdges.Add(Vertices[source]);
    }

    /// <summary>
    ///     Remove an existing edge between source and destination.
    ///     Time complexity: O(1).
    /// </summary>
    public void RemoveEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new Exception("Source or Destination Vertex is not in this graph.");

        if (!Vertices[source].OutEdges.Contains(Vertices[dest])
            || !Vertices[dest].InEdges.Contains(Vertices[source]))
            throw new Exception("Edge do not exists.");

        Vertices[source].OutEdges.Remove(Vertices[dest]);
        Vertices[dest].InEdges.Remove(Vertices[source]);
    }

    public IEnumerable<T> OutEdges(T vertex)
    {
        if (!Vertices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        return Vertices[vertex].OutEdges.Select(x => x.Key);
    }

    public IEnumerable<T> InEdges(T vertex)
    {
        if (!Vertices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        return Vertices[vertex].InEdges.Select(x => x.Key);
    }

    /// <summary>
    ///     Clones this graph.
    /// </summary>
    public DiGraph<T> Clone()
    {
        var newGraph = new DiGraph<T>();

        foreach (var vertex in Vertices) newGraph.AddVertex(vertex.Key);

        foreach (var vertex in Vertices)
        foreach (var edge in vertex.Value.OutEdges)
            newGraph.AddEdge(vertex.Value.Key, edge.Key);

        return newGraph;
    }
}

internal class DiGraphVertex<T> : IDiGraphVertex<T>, IGraphVertex<T>, IEnumerable<T>
{
    public DiGraphVertex(T value)
    {
        Key = value;
        OutEdges = new HashSet<DiGraphVertex<T>>();
        InEdges = new HashSet<DiGraphVertex<T>>();
    }

    public HashSet<DiGraphVertex<T>> OutEdges { get; }
    public HashSet<DiGraphVertex<T>> InEdges { get; }
    public T Key { get; set; }

    IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.OutEdges => OutEdges.Select(x => new DiEdge<T, int>(x, 1));
    IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.InEdges => InEdges.Select(x => new DiEdge<T, int>(x, 1));

    public int OutEdgeCount => OutEdges.Count;
    public int InEdgeCount => InEdges.Count;

    public IDiEdge<T> GetOutEdge(IDiGraphVertex<T> targetVertex)
    {
        return new DiEdge<T, int>(targetVertex, 1);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return OutEdges.Select(x => x.Key).GetEnumerator();
    }

    IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => OutEdges.Select(x => new Edge<T, int>(x, 1));

    public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
    {
        return new Edge<T, int>(targetVertex, 1);
    }
}ParseOptions.0.json¥0
pD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Graph\AdjacencyList\Graph.cs™/using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

/// <summary>
///     A graph implementation
///     IEnumerable enumerates all vertices.
/// </summary>
public class Graph<T> : IGraph<T>, IEnumerable<T>
{
    public Graph()
    {
        Vertices = new Dictionary<T, GraphVertex<T>>();
    }

    private Dictionary<T, GraphVertex<T>> Vertices { get; }

    /// <summary>
    ///     Returns a reference vertex.
    ///     Time complexity: O(1).
    /// </summary>
    private GraphVertex<T> ReferenceVertex
    {
        get
        {
            using (var enumerator = Vertices.GetEnumerator())
            {
                if (enumerator.MoveNext()) return enumerator.Current.Value;
            }

            return null;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return Vertices.Select(x => x.Key).GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator() as IEnumerator<T>;
    }

    public int VerticesCount => Vertices.Count;
    public bool IsWeightedGraph => false;

    IGraphVertex<T> IGraph<T>.ReferenceVertex => ReferenceVertex;

    /// <summary>
    ///     Do we have an edge between given source and destination?
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasEdge(T source, T dest)
    {
        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new ArgumentException("source or destination is not in this graph.");

        return Vertices[source].Edges.Contains(Vertices[dest])
               && Vertices[dest].Edges.Contains(Vertices[source]);
    }

    public bool ContainsVertex(T value)
    {
        return Vertices.ContainsKey(value);
    }

    public IGraphVertex<T> GetVertex(T value)
    {
        return Vertices[value];
    }

    IGraph<T> IGraph<T>.Clone()
    {
        return Clone();
    }

    public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => Vertices.Select(x => x.Value);


    /// <summary>
    ///     Add a new vertex to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        var newVertex = new GraphVertex<T>(value);

        Vertices.Add(value, newVertex);
    }

    /// <summary>
    ///     Remove an existing vertex from this graph.
    ///     Time complexity: O(V) where V is the number of vertices.
    /// </summary>
    public void RemoveVertex(T vertex)
    {
        if (vertex == null) throw new ArgumentNullException();

        if (!Vertices.ContainsKey(vertex)) throw new Exception("Vertex not in this graph.");

        foreach (var v in Vertices[vertex].Edges) v.Edges.Remove(Vertices[vertex]);

        Vertices.Remove(vertex);
    }

    /// <summary>
    ///     Add an edge to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new Exception("Source or Destination Vertex is not in this graph.");

        if (Vertices[source].Edges.Contains(Vertices[dest])
            || Vertices[dest].Edges.Contains(Vertices[source]))
            throw new Exception("Edge already exists.");

        Vertices[source].Edges.Add(Vertices[dest]);
        Vertices[dest].Edges.Add(Vertices[source]);
    }

    /// <summary>
    ///     Remove an edge from this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void RemoveEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new Exception("Source or Destination Vertex is not in this graph.");

        if (!Vertices[source].Edges.Contains(Vertices[dest])
            || !Vertices[dest].Edges.Contains(Vertices[source]))
            throw new Exception("Edge do not exists.");

        Vertices[source].Edges.Remove(Vertices[dest]);
        Vertices[dest].Edges.Remove(Vertices[source]);
    }

    public IEnumerable<T> Edges(T vertex)
    {
        if (!Vertices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        return Vertices[vertex].Edges.Select(x => x.Key);
    }

    /// <summary>
    ///     Clones this graph.
    /// </summary>
    public Graph<T> Clone()
    {
        var newGraph = new Graph<T>();

        foreach (var vertex in Vertices) newGraph.AddVertex(vertex.Key);

        foreach (var vertex in Vertices)
        foreach (var edge in vertex.Value.Edges)
            newGraph.AddEdge(vertex.Value.Key, edge.Key);

        return newGraph;
    }

    /// <summary>
    ///     Graph vertex for adjacency list Graph implementation.
    ///     IEnumerable enumerates all the outgoing edge destination vertices.
    /// </summary>
    private class GraphVertex<T> : IEnumerable<T>, IGraphVertex<T>
    {
        public GraphVertex(T value)
        {
            Key = value;
            Edges = new HashSet<GraphVertex<T>>();
        }

        public HashSet<GraphVertex<T>> Edges { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Edges.Select(x => x.Key).GetEnumerator();
        }

        public T Key { get; }

        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => Edges.Select(x => new Edge<T, int>(x, 1));

        public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
        {
            return new Edge<T, int>(targetVertex, 1);
        }
    }
}ParseOptions.0.json‹?
zD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Graph\AdjacencyList\WeightedDiGraph.cs»>using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

/// <summary>
///     A weighted graph implementation.
///     IEnumerable enumerates all vertices.
/// </summary>
public class WeightedDiGraph<T, TW> : IDiGraph<T>, IGraph<T>, IEnumerable<T> where TW : IComparable
{
    public WeightedDiGraph()
    {
        Vertices = new Dictionary<T, WeightedDiGraphVertex<T, TW>>();
    }

    internal Dictionary<T, WeightedDiGraphVertex<T, TW>> Vertices { get; set; }

    /// <summary>
    ///     Returns a reference vertex.
    ///     Time complexity: O(1).
    /// </summary>
    private WeightedDiGraphVertex<T, TW> ReferenceVertex
    {
        get
        {
            using (var enumerator = Vertices.GetEnumerator())
            {
                if (enumerator.MoveNext()) return enumerator.Current.Value;
            }

            return null;
        }
    }

    public int VerticesCount => Vertices.Count;
    public bool IsWeightedGraph => true;

    IDiGraphVertex<T> IDiGraph<T>.ReferenceVertex => ReferenceVertex;

    /// <summary>
    ///     Do we have an edge between given source and destination?
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasEdge(T source, T dest)
    {
        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new ArgumentException("source or destination is not in this graph.");

        return Vertices[source].OutEdges.ContainsKey(Vertices[dest])
               && Vertices[dest].InEdges.ContainsKey(Vertices[source]);
    }

    public bool ContainsVertex(T value)
    {
        return Vertices.ContainsKey(value);
    }

    public IDiGraphVertex<T> GetVertex(T key)
    {
        return Vertices[key];
    }

    IDiGraph<T> IDiGraph<T>.Clone()
    {
        return Clone();
    }

    IEnumerable<IDiGraphVertex<T>> IDiGraph<T>.VerticesAsEnumberable => Vertices.Select(x => x.Value);

    public IEnumerator GetEnumerator()
    {
        return Vertices.Select(x => x.Key).GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator() as IEnumerator<T>;
    }

    IGraphVertex<T> IGraph<T>.ReferenceVertex => ReferenceVertex;

    IGraphVertex<T> IGraph<T>.GetVertex(T key)
    {
        return Vertices[key];
    }

    IGraph<T> IGraph<T>.Clone()
    {
        return Clone();
    }

    public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => Vertices.Select(x => x.Value);

    /// <summary>
    ///     Add a new vertex to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        var newVertex = new WeightedDiGraphVertex<T, TW>(value);

        Vertices.Add(value, newVertex);
    }

    /// <summary>
    ///     Remove the given vertex.
    ///     Time complexity: O(V) where V is the number of vertices.
    /// </summary>
    public void RemoveVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        if (!Vertices.ContainsKey(value)) throw new Exception("Vertex not in this graph.");

        foreach (var vertex in Vertices[value].InEdges) vertex.Key.OutEdges.Remove(Vertices[value]);

        foreach (var vertex in Vertices[value].OutEdges) vertex.Key.InEdges.Remove(Vertices[value]);

        Vertices.Remove(value);
    }

    /// <summary>
    ///     Add a new edge to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddEdge(T source, T dest, TW weight)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!Vertices.ContainsKey(source)
            || !Vertices.ContainsKey(dest))
            throw new Exception("Source or Destination Vertex is not in this graph.");

        if (Vertices[source].OutEdges.ContainsKey(Vertices[dest])
            || Vertices[dest].InEdges.ContainsKey(Vertices[source]))
            throw new Exception("Edge already exists.");

        Vertices[source].OutEdges.Add(Vertices[dest], weight);
        Vertices[dest].InEdges.Add(Vertices[source], weight);
    }

    /// <summary>
    ///     Remove the given edge from this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void RemoveEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new Exception("Source or Destination Vertex is not in this graph.");

        if (!Vertices[source].OutEdges.ContainsKey(Vertices[dest])
            || !Vertices[dest].InEdges.ContainsKey(Vertices[source]))
            throw new Exception("Edge do not exist.");

        Vertices[source].OutEdges.Remove(Vertices[dest]);
        Vertices[dest].InEdges.Remove(Vertices[source]);
    }

    public IEnumerable<Tuple<T, TW>> OutEdges(T vertex)
    {
        if (!Vertices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        return Vertices[vertex].OutEdges.Select(x => new Tuple<T, TW>(x.Key.Key, x.Value));
    }

    public IEnumerable<Tuple<T, TW>> InEdges(T vertex)
    {
        if (!Vertices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        return Vertices[vertex].InEdges.Select(x => new Tuple<T, TW>(x.Key.Key, x.Value));
    }

    /// <summary>
    ///     Returns the vertex with given value.
    ///     Time complexity: O(1).
    /// </summary>
    internal WeightedDiGraphVertex<T, TW> FindVertex(T value)
    {
        if (Vertices.ContainsKey(value)) return Vertices[value];

        return null;
    }

    /// <summary>
    ///     Clone this graph.
    /// </summary>
    public WeightedDiGraph<T, TW> Clone()
    {
        var newGraph = new WeightedDiGraph<T, TW>();

        foreach (var vertex in Vertices) newGraph.AddVertex(vertex.Key);

        foreach (var vertex in Vertices)
        foreach (var edge in vertex.Value.OutEdges)
            newGraph.AddEdge(vertex.Value.Key, edge.Key.Key, edge.Value);

        return newGraph;
    }
}

internal class WeightedDiGraphVertex<T, TW> : IDiGraphVertex<T>, IGraphVertex<T>, IEnumerable<T> where TW : IComparable
{
    public WeightedDiGraphVertex(T value)
    {
        Key = value;

        OutEdges = new Dictionary<WeightedDiGraphVertex<T, TW>, TW>();
        InEdges = new Dictionary<WeightedDiGraphVertex<T, TW>, TW>();
    }

    public Dictionary<WeightedDiGraphVertex<T, TW>, TW> OutEdges { get; }
    public Dictionary<WeightedDiGraphVertex<T, TW>, TW> InEdges { get; }
    public T Key { get; set; }

    IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.OutEdges => OutEdges.Select(x => new DiEdge<T, TW>(x.Key, x.Value));
    IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.InEdges => InEdges.Select(x => new DiEdge<T, TW>(x.Key, x.Value));

    public int OutEdgeCount => OutEdges.Count;
    public int InEdgeCount => InEdges.Count;

    public IDiEdge<T> GetOutEdge(IDiGraphVertex<T> targetVertex)
    {
        var key = targetVertex as WeightedDiGraphVertex<T, TW>;
        return new DiEdge<T, TW>(targetVertex, OutEdges[key]);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return OutEdges.Select(x => x.Key.Key).GetEnumerator();
    }

    IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => OutEdges.Select(x => new Edge<T, TW>(x.Key, x.Value));

    public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
    {
        var key = targetVertex as WeightedDiGraphVertex<T, TW>;
        return new Edge<T, TW>(targetVertex, OutEdges[key]);
    }
}ParseOptions.0.json–0
xD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Graph\AdjacencyList\WeightedGraph.csæ/using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

/// <summary>
///     A weighted graph implementation.
///     IEnumerable enumerates all vertices.
/// </summary>
public class WeightedGraph<T, TW> : IGraph<T>, IEnumerable<T> where TW : IComparable
{
    public WeightedGraph()
    {
        Vertices = new Dictionary<T, WeightedGraphVertex<T, TW>>();
    }

    private Dictionary<T, WeightedGraphVertex<T, TW>> Vertices { get; }

    /// <summary>
    ///     Returns a reference vertex.
    ///     Time complexity: O(1).
    /// </summary>
    private WeightedGraphVertex<T, TW> ReferenceVertex
    {
        get
        {
            using (var enumerator = Vertices.GetEnumerator())
            {
                if (enumerator.MoveNext()) return enumerator.Current.Value;
            }

            return null;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return Vertices.Select(x => x.Key).GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator() as IEnumerator<T>;
    }

    public int VerticesCount => Vertices.Count;
    public bool IsWeightedGraph => true;

    IGraphVertex<T> IGraph<T>.ReferenceVertex => ReferenceVertex;

    /// <summary>
    ///     Do we have an edge between given source and destination?
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasEdge(T source, T dest)
    {
        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new ArgumentException("source or destination is not in this graph.");

        return Vertices[source].Edges.ContainsKey(Vertices[dest])
               && Vertices[dest].Edges.ContainsKey(Vertices[source]);
    }

    public bool ContainsVertex(T value)
    {
        return Vertices.ContainsKey(value);
    }

    public IGraphVertex<T> GetVertex(T value)
    {
        return Vertices[value];
    }

    IGraph<T> IGraph<T>.Clone()
    {
        return Clone();
    }

    public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => Vertices.Select(x => x.Value);


    /// <summary>
    ///     Add a new vertex to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        var newVertex = new WeightedGraphVertex<T, TW>(value);

        Vertices.Add(value, newVertex);
    }

    /// <summary>
    ///     Remove given vertex from this graph.
    ///     Time complexity: O(V) where V is the number of vertices.
    /// </summary>
    public void RemoveVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        if (!Vertices.ContainsKey(value)) throw new Exception("Vertex not in this graph.");


        foreach (var vertex in Vertices[value].Edges) vertex.Key.Edges.Remove(Vertices[value]);

        Vertices.Remove(value);
    }

    /// <summary>
    ///     Add a new edge to this graph with given weight
    ///     and between given source and destination vertex.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddEdge(T source, T dest, TW weight)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new Exception("Source or Destination Vertex is not in this graph.");


        Vertices[source].Edges.Add(Vertices[dest], weight);
        Vertices[dest].Edges.Add(Vertices[source], weight);
    }

    /// <summary>
    ///     Remove given edge.
    ///     Time complexity: O(1).
    /// </summary>
    public void RemoveEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new Exception("Source or Destination Vertex is not in this graph.");

        if (!Vertices[source].Edges.ContainsKey(Vertices[dest])
            || !Vertices[dest].Edges.ContainsKey(Vertices[source]))
            throw new Exception("Edge do not exists.");

        Vertices[source].Edges.Remove(Vertices[dest]);
        Vertices[dest].Edges.Remove(Vertices[source]);
    }

    public List<Tuple<T, TW>> GetAllEdges(T vertex)
    {
        if (!Vertices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        return Vertices[vertex].Edges.Select(x => new Tuple<T, TW>(x.Key.Key, x.Value)).ToList();
    }

    /// <summary>
    ///     Clones this graph.
    /// </summary>
    public WeightedGraph<T, TW> Clone()
    {
        var newGraph = new WeightedGraph<T, TW>();

        foreach (var vertex in Vertices) newGraph.AddVertex(vertex.Key);

        foreach (var vertex in Vertices)
        foreach (var edge in vertex.Value.Edges)
            newGraph.AddEdge(vertex.Value.Key, edge.Key.Key, edge.Value);

        return newGraph;
    }
}

internal class WeightedGraphVertex<T, TW> : IGraphVertex<T>, IEnumerable<T> where TW : IComparable
{
    public WeightedGraphVertex(T key)
    {
        Key = key;
        Edges = new Dictionary<WeightedGraphVertex<T, TW>, TW>();
    }

    public T Key { get; set; }

    public Dictionary<WeightedGraphVertex<T, TW>, TW> Edges { get; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Edges.Select(x => x.Key.Key).GetEnumerator();
    }

    T IGraphVertex<T>.Key => Key;

    IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => Edges.Select(x => new Edge<T, TW>(x.Key, x.Value));

    public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
    {
        return new Edge<T, TW>(targetVertex, Edges[targetVertex as WeightedGraphVertex<T, TW>]);
    }
}ParseOptions.0.jsonıa
tD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Graph\AdjacencyMatrix\DiGraph.csÁ`using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix;

/// <summary>
///     A directed graph implementation using dynamically growing/shrinking adjacency matrix array.
///     IEnumerable enumerates all vertices.
/// </summary>
public class DiGraph<T> : IGraph<T>, IDiGraph<T>, IEnumerable<T>
{
    private BitArray[] matrix;
    private int nextAvailableIndex;
    private Dictionary<int, T> reverseVertexIndices;

    private Dictionary<T, int> vertexIndices;
    private readonly Dictionary<T, DiGraphVertex<T>> vertexObjects;

    public DiGraph()
    {
        vertexIndices = new Dictionary<T, int>();
        reverseVertexIndices = new Dictionary<int, T>();
        matrix = new BitArray[1];
        vertexObjects = new Dictionary<T, DiGraphVertex<T>>();

        for (var i = 0; i < MaxSize; i++) matrix[i] = new BitArray(MaxSize);
    }

    private int MaxSize => matrix.Length;
    IDiGraphVertex<T> IDiGraph<T>.ReferenceVertex => GetReferenceVertex();

    IDiGraphVertex<T> IDiGraph<T>.GetVertex(T key)
    {
        return vertexObjects[key];
    }

    public IEnumerable<IDiGraphVertex<T>> VerticesAsEnumberable => GetVerticesAsEnumerable();

    IDiGraph<T> IDiGraph<T>.Clone()
    {
        return Clone();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return vertexIndices.Select(x => x.Key).GetEnumerator();
    }

    public int VerticesCount { get; private set; }

    public bool IsWeightedGraph => false;

    public IGraphVertex<T> ReferenceVertex => GetReferenceVertex();

    public bool ContainsVertex(T key)
    {
        return vertexIndices.ContainsKey(key);
    }

    public IGraphVertex<T> GetVertex(T key)
    {
        return vertexObjects[key];
    }

    /// <summary>
    ///     do we have an edge between the given source and destination?
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];

        return matrix[sourceIndex].Get(destIndex);
    }

    IEnumerable<IGraphVertex<T>> IGraph<T>.VerticesAsEnumberable => GetVerticesAsEnumerable();

    IGraph<T> IGraph<T>.Clone()
    {
        return Clone();
    }

    private DiGraphVertex<T> GetReferenceVertex()
    {
        if (VerticesCount == 0) throw new Exception("Empty graph.");

        return vertexObjects[this.First()];
    }

    /// <summary>
    ///     Add a new vertex to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        if (vertexIndices.ContainsKey(value)) throw new Exception("Vertex exists.");

        if (VerticesCount < MaxSize / 2) HalfMatrixSize();

        if (nextAvailableIndex == MaxSize) DoubleMatrixSize();

        vertexIndices.Add(value, nextAvailableIndex);
        reverseVertexIndices.Add(nextAvailableIndex, value);
        vertexObjects.Add(value, new DiGraphVertex<T>(this, value));

        nextAvailableIndex++;
        VerticesCount++;
    }

    /// <summary>
    ///     Remove an existing vertex from graph
    ///     Time complexity: O(V) where V is the number of vertices.
    /// </summary>
    public void RemoveVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        if (!vertexIndices.ContainsKey(value)) throw new Exception("Vertex does'nt exist.");

        if (VerticesCount <= MaxSize / 2) HalfMatrixSize();

        var index = vertexIndices[value];

        //clear edges
        for (var i = 0; i < MaxSize; i++)
        {
            matrix[i].Set(index, false);
            matrix[index].Set(i, false);
        }

        reverseVertexIndices.Remove(index);
        vertexIndices.Remove(value);
        vertexObjects.Remove(value);

        VerticesCount--;
    }

    /// <summary>
    ///     add an edge from source to destination vertex
    ///     Time complexity: O(1).
    /// </summary>
    public void AddEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (matrix[sourceIndex].Get(destIndex)) throw new Exception("Edge already exists.");

        matrix[sourceIndex].Set(destIndex, true);
    }

    /// <summary>
    ///     remove an existing edge between source and destination
    ///     Time complexity: O(1).
    /// </summary>
    public void RemoveEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (!matrix[sourceIndex].Get(destIndex)) throw new Exception("Edge do not exists.");

        matrix[sourceIndex].Set(destIndex, false);
    }

    public IEnumerable<T> OutEdges(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        var result = new List<T>();

        for (var i = 0; i < MaxSize; i++)
            if (matrix[index].Get(i))
                yield return reverseVertexIndices[i];
    }

    public int OutEdgeCount(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        var count = 0;

        for (var i = 0; i < MaxSize; i++)
            if (matrix[index].Get(i))
                count++;

        return count;
    }

    public IEnumerable<T> InEdges(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        var result = new List<T>();

        for (var i = 0; i < MaxSize; i++)
            if (matrix[i].Get(index))
                yield return reverseVertexIndices[i];
    }

    public int InEdgeCount(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        var count = 0;

        for (var i = 0; i < MaxSize; i++)
            if (matrix[i].Get(index))
                count++;

        return count;
    }

    private void DoubleMatrixSize()
    {
        var newMatrix = new BitArray[MaxSize * 2];
        for (var i = 0; i < MaxSize * 2; i++) newMatrix[i] = new BitArray(MaxSize * 2);

        var newVertexIndices = new Dictionary<T, int>();
        var newReverseIndices = new Dictionary<int, T>();

        var k = 0;
        foreach (var vertex in vertexIndices)
        {
            newVertexIndices.Add(vertex.Key, k);
            newReverseIndices.Add(k, vertex.Key);
            k++;
        }

        nextAvailableIndex = k;

        for (var i = 0; i < MaxSize; i++)
        {
            newMatrix[i] = new BitArray(MaxSize * 2);
            for (var j = 0; j < MaxSize; j++)
            {
                if (!matrix[i].Get(j) || !reverseVertexIndices.ContainsKey(i) ||
                    !reverseVertexIndices.ContainsKey(j))
                    continue;

                var newI = newVertexIndices[reverseVertexIndices[i]];
                var newJ = newVertexIndices[reverseVertexIndices[j]];

                newMatrix[newI].Set(newJ, true);
            }
        }

        matrix = newMatrix;
        vertexIndices = newVertexIndices;
        reverseVertexIndices = newReverseIndices;
    }

    private void HalfMatrixSize()
    {
        var newMatrix = new BitArray[MaxSize / 2];
        for (var i = 0; i < MaxSize / 2; i++) newMatrix[i] = new BitArray(MaxSize / 2);

        var newVertexIndices = new Dictionary<T, int>();
        var newReverseIndices = new Dictionary<int, T>();

        var k = 0;
        foreach (var vertex in vertexIndices)
        {
            newVertexIndices.Add(vertex.Key, k);
            newReverseIndices.Add(k, vertex.Key);
            k++;
        }

        nextAvailableIndex = k;

        for (var i = 0; i < MaxSize; i++)
        for (var j = 0; j < MaxSize; j++)
        {
            if (!matrix[i].Get(j) || !reverseVertexIndices.ContainsKey(i) ||
                !reverseVertexIndices.ContainsKey(j))
                continue;

            var newI = newVertexIndices[reverseVertexIndices[i]];
            var newJ = newVertexIndices[reverseVertexIndices[j]];

            newMatrix[newI].Set(newJ, true);
        }

        matrix = newMatrix;
        vertexIndices = newVertexIndices;
        reverseVertexIndices = newReverseIndices;
    }

    private IEnumerable<DiGraphVertex<T>> GetVerticesAsEnumerable()
    {
        return this.Select(x => vertexObjects[x]);
    }

    public DiGraph<T> Clone()
    {
        var graph = new DiGraph<T>();

        foreach (var vertex in this) graph.AddVertex(vertex);

        foreach (var vertex in this)
        foreach (var edge in OutEdges(vertex))
            graph.AddEdge(vertex, edge);

        return graph;
    }

    private class DiGraphVertex<T> : IDiGraphVertex<T>, IGraphVertex<T>
    {
        private readonly DiGraph<T> graph;
        private int vertexIndex;

        internal DiGraphVertex(DiGraph<T> graph, T vertexKey)
        {
            if (!graph.vertexIndices.ContainsKey(vertexKey))
                throw new ArgumentException("vertex is not in this graph.");

            this.graph = graph;
            Key = vertexKey;
            vertexIndex = graph.vertexIndices[vertexKey];
        }

        private int MaxSize => graph.MaxSize;
        private BitArray[] Matrix => graph.matrix;

        private Dictionary<T, int> VertexIndices => graph.vertexIndices;
        private Dictionary<int, T> ReverseVertexIndices => graph.reverseVertexIndices;

        public T Key { get; }

        IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.OutEdges => graph.OutEdges(Key)
            .Select(x => new DiEdge<T, int>(graph.vertexObjects[x], 1));

        IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.InEdges => graph.InEdges(Key)
            .Select(x => new DiEdge<T, int>(graph.vertexObjects[x], 1));

        public int OutEdgeCount => graph.OutEdgeCount(Key);
        public int InEdgeCount => graph.InEdgeCount(Key);

        public IDiEdge<T> GetOutEdge(IDiGraphVertex<T> targetVertex)
        {
            if (!VertexIndices.ContainsKey(targetVertex.Key))
                throw new ArgumentException("vertex is not in this graph.");

            var index = VertexIndices[targetVertex.Key];
            var key = targetVertex as DiGraphVertex<T>;
            return new DiEdge<T, int>(targetVertex, 1);
        }

        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => graph.OutEdges(Key)
            .Select(x => new Edge<T, int>(graph.vertexObjects[x], 1));

        public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
        {
            if (!VertexIndices.ContainsKey(targetVertex.Key))
                throw new ArgumentException("vertex is not in this graph.");

            var index = VertexIndices[targetVertex.Key];
            var key = targetVertex as DiGraphVertex<T>;
            return new Edge<T, int>(targetVertex, 1);
        }
    }
}ParseOptions.0.jsonÓV
rD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Graph\AdjacencyMatrix\Graph.cs‚Uusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix;

/// <summary>
///     A directed graph implementation using dynamically growing/shrinking adjacency matrix array.
///     IEnumerable enumerates all vertices.
/// </summary>
public class Graph<T> : IGraph<T>, IEnumerable<T>
{
    private BitArray[] matrix;
    private int nextAvailableIndex;
    private Dictionary<int, T> reverseVertexIndices;

    private Dictionary<T, int> vertexIndices;
    private readonly Dictionary<T, GraphVertex<T>> vertexObjects;

    public Graph()
    {
        vertexIndices = new Dictionary<T, int>();
        reverseVertexIndices = new Dictionary<int, T>();
        vertexObjects = new Dictionary<T, GraphVertex<T>>();

        matrix = new BitArray[1];

        for (var i = 0; i < MaxSize; i++) matrix[i] = new BitArray(MaxSize);
    }

    private int MaxSize => matrix.Length;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return vertexIndices.Select(x => x.Key).GetEnumerator();
    }

    public int VerticesCount { get; private set; }

    public bool IsWeightedGraph => false;

    public IGraphVertex<T> ReferenceVertex => GetReferenceVertex();

    /// <summary>
    ///     Do we have an edge between the given source and destination?
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (matrix[sourceIndex].Get(destIndex) && matrix[destIndex].Get(sourceIndex)) return true;

        return false;
    }

    public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => vertexObjects.Select(x => x.Value);

    public bool ContainsVertex(T key)
    {
        return vertexObjects.ContainsKey(key);
    }

    public IGraphVertex<T> GetVertex(T key)
    {
        return vertexObjects[key];
    }

    IGraph<T> IGraph<T>.Clone()
    {
        return Clone();
    }

    private GraphVertex<T> GetReferenceVertex()
    {
        if (VerticesCount == 0) throw new Exception("Empty graph.");

        return vertexObjects[this.First()];
    }

    /// <summary>
    ///     Add a new vertex to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        if (vertexIndices.ContainsKey(value)) throw new Exception("Vertex exists.");

        if (VerticesCount < MaxSize / 2) HalfMatrixSize();

        if (nextAvailableIndex == MaxSize) DoubleMatrixSize();

        vertexIndices.Add(value, nextAvailableIndex);
        reverseVertexIndices.Add(nextAvailableIndex, value);
        vertexObjects.Add(value, new GraphVertex<T>(this, value));

        nextAvailableIndex++;
        VerticesCount++;
    }


    /// <summary>
    ///     Remove an existing vertex from graph.
    ///     Time complexity: O(V) where V is the number of vertices.
    /// </summary>
    public void RemoveVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        if (!vertexIndices.ContainsKey(value)) throw new Exception("Vertex does'nt exist.");

        if (VerticesCount <= MaxSize / 2) HalfMatrixSize();

        var index = vertexIndices[value];

        //clear edges
        for (var i = 0; i < MaxSize; i++)
        {
            matrix[i].Set(index, false);
            matrix[index].Set(i, false);
        }

        reverseVertexIndices.Remove(index);
        vertexIndices.Remove(value);
        vertexObjects.Remove(value);

        VerticesCount--;
    }

    /// <summary>
    ///     Add an edge from source to destination vertex.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (matrix[sourceIndex].Get(destIndex) && matrix[destIndex].Get(sourceIndex))
            throw new Exception("Edge already exists.");

        matrix[sourceIndex].Set(destIndex, true);
        matrix[destIndex].Set(sourceIndex, true);
    }

    /// <summary>
    ///     Remove an existing edge between source and destination.
    ///     Time complexity: O(1).
    /// </summary>
    public void RemoveEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (!matrix[sourceIndex].Get(destIndex) || !matrix[destIndex].Get(sourceIndex))
            throw new Exception("Edge do not exists.");

        matrix[sourceIndex].Set(destIndex, false);
        matrix[destIndex].Set(sourceIndex, false);
    }


    public IEnumerable<T> Edges(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        for (var i = 0; i < MaxSize; i++)
            if (matrix[i].Get(index))
                yield return reverseVertexIndices[i];
    }

    public int EdgesCount(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var count = 0;
        var index = vertexIndices[vertex];

        for (var i = 0; i < MaxSize; i++)
            if (matrix[i].Get(index))
                count++;

        return count;
    }


    private void DoubleMatrixSize()
    {
        var newMatrix = new BitArray[MaxSize * 2];
        for (var i = 0; i < MaxSize * 2; i++) newMatrix[i] = new BitArray(MaxSize * 2);

        var newVertexIndices = new Dictionary<T, int>();
        var newReverseIndices = new Dictionary<int, T>();

        var k = 0;
        foreach (var vertex in vertexIndices)
        {
            newVertexIndices.Add(vertex.Key, k);
            newReverseIndices.Add(k, vertex.Key);
            k++;
        }

        nextAvailableIndex = k;

        for (var i = 0; i < MaxSize; i++)
        for (var j = i; j < MaxSize; j++)
            if (matrix[i].Get(j) && matrix[j].Get(i)
                                 && reverseVertexIndices.ContainsKey(i)
                                 && reverseVertexIndices.ContainsKey(j))
            {
                var newI = newVertexIndices[reverseVertexIndices[i]];
                var newJ = newVertexIndices[reverseVertexIndices[j]];

                newMatrix[newI].Set(newJ, true);
                newMatrix[newJ].Set(newI, true);
            }

        matrix = newMatrix;
        vertexIndices = newVertexIndices;
        reverseVertexIndices = newReverseIndices;
    }

    private void HalfMatrixSize()
    {
        var newMatrix = new BitArray[MaxSize / 2];
        for (var i = 0; i < MaxSize / 2; i++) newMatrix[i] = new BitArray(MaxSize / 2);

        var newVertexIndices = new Dictionary<T, int>();
        var newReverseIndices = new Dictionary<int, T>();

        var k = 0;
        foreach (var vertex in vertexIndices)
        {
            newVertexIndices.Add(vertex.Key, k);
            newReverseIndices.Add(k, vertex.Key);
            k++;
        }

        nextAvailableIndex = k;

        for (var i = 0; i < MaxSize; i++)
        for (var j = i; j < MaxSize; j++)
            if (matrix[i].Get(j) && matrix[j].Get(i)
                                 && reverseVertexIndices.ContainsKey(i)
                                 && reverseVertexIndices.ContainsKey(j))
            {
                var newI = newVertexIndices[reverseVertexIndices[i]];
                var newJ = newVertexIndices[reverseVertexIndices[j]];

                newMatrix[newI].Set(newJ, true);
                newMatrix[newJ].Set(newI, true);
            }

        matrix = newMatrix;
        vertexIndices = newVertexIndices;
        reverseVertexIndices = newReverseIndices;
    }

    public Graph<T> Clone()
    {
        var graph = new Graph<T>();

        foreach (var vertex in this) graph.AddVertex(vertex);

        foreach (var vertex in this)
        foreach (var edge in Edges(vertex))
            graph.AddEdge(vertex, edge);

        return graph;
    }

    private class GraphVertex<T> : IGraphVertex<T>
    {
        private readonly Graph<T> graph;
        private int vertexIndex;

        internal GraphVertex(Graph<T> graph, T vertexKey)
        {
            if (!graph.vertexIndices.ContainsKey(vertexKey))
                throw new ArgumentException("vertex is not in this graph.");

            this.graph = graph;
            Key = vertexKey;
            vertexIndex = graph.vertexIndices[vertexKey];
        }

        private int MaxSize => graph.MaxSize;
        private BitArray[] Matrix => graph.matrix;

        private Dictionary<T, int> VertexIndices => graph.vertexIndices;
        private Dictionary<int, T> ReverseVertexIndices => graph.reverseVertexIndices;

        public T Key { get; }


        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => graph.Edges(Key)
            .Select(x => new Edge<T, int>(graph.vertexObjects[x], 1));

        public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
        {
            if (!VertexIndices.ContainsKey(targetVertex.Key))
                throw new ArgumentException("vertex is not in this graph.");

            var index = VertexIndices[targetVertex.Key];
            var key = targetVertex as GraphVertex<T>;
            return new Edge<T, int>(targetVertex, 1);
        }

        public IEdge<T> GetOutEdge(IGraphVertex<T> targetVertex)
        {
            if (!VertexIndices.ContainsKey(targetVertex.Key))
                throw new ArgumentException("vertex is not in this graph.");

            var index = VertexIndices[targetVertex.Key];
            var key = targetVertex as GraphVertex<T>;
            return new Edge<T, int>(targetVertex, 1);
        }
    }
}ParseOptions.0.json c
|D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Graph\AdjacencyMatrix\WeightedDiGraph.cs¥busing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix;

/// <summary>
///     A weighted graph implementation using dynamically growing/shrinking adjacency matrix array.
///     IEnumerable enumerates all vertices.
/// </summary>
public class WeightedDiGraph<T, TW> : IDiGraph<T>, IGraph<T>, IEnumerable<T> where TW : IComparable
{
    private TW[,] matrix;
    private int nextAvailableIndex;
    private Dictionary<int, T> reverseVertexIndices;
    private Dictionary<T, int> vertexIndices;
    private readonly Dictionary<T, WeightedDiGraphVertex<T, TW>> vertexObjects;

    public WeightedDiGraph()
    {
        vertexIndices = new Dictionary<T, int>();
        reverseVertexIndices = new Dictionary<int, T>();
        vertexObjects = new Dictionary<T, WeightedDiGraphVertex<T, TW>>();

        matrix = new TW[1, 1];
    }

    private int MaxSize => matrix.GetLength(0);

    public bool IsWeightedGraph => true;
    public int VerticesCount => vertexObjects.Count;

    public IDiGraphVertex<T> ReferenceVertex => GetReferenceVertex();

    /// <summary>
    ///     Do we have an edge between given source and destination?
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];

        if (!matrix[sourceIndex, destIndex].Equals(default(TW))) return true;

        return false;
    }

    public IEnumerable<IDiGraphVertex<T>> VerticesAsEnumberable => GetVerticesAsEnumerable();

    public bool ContainsVertex(T value)
    {
        return vertexIndices.ContainsKey(value);
    }

    public IDiGraphVertex<T> GetVertex(T key)
    {
        return vertexObjects[key];
    }

    IDiGraph<T> IDiGraph<T>.Clone()
    {
        return Clone();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return vertexIndices.Select(x => x.Key).GetEnumerator();
    }

    IGraphVertex<T> IGraph<T>.ReferenceVertex => GetReferenceVertex();
    IEnumerable<IGraphVertex<T>> IGraph<T>.VerticesAsEnumberable => GetVerticesAsEnumerable();

    IGraphVertex<T> IGraph<T>.GetVertex(T key)
    {
        return vertexObjects[key];
    }

    IGraph<T> IGraph<T>.Clone()
    {
        return Clone();
    }

    private WeightedDiGraphVertex<T, TW> GetReferenceVertex()
    {
        if (VerticesCount == 0) throw new Exception("Empty graph.");

        return vertexObjects[this.First()];
    }

    /// <summary>
    ///     Add a new vertex to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        if (vertexIndices.ContainsKey(value)) throw new Exception("Vertex exists.");

        if (VerticesCount < MaxSize / 2) HalfMatrixSize();

        if (nextAvailableIndex == MaxSize) DoubleMatrixSize();

        vertexIndices.Add(value, nextAvailableIndex);
        reverseVertexIndices.Add(nextAvailableIndex, value);
        vertexObjects.Add(value, new WeightedDiGraphVertex<T, TW>(this, value));
        nextAvailableIndex++;
    }

    /// <summary>
    ///     Remove the given vertex.
    ///     Time complexity: O(V) where V is the number of vertices.
    /// </summary>
    public void RemoveVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        if (!vertexIndices.ContainsKey(value)) throw new Exception("Vertex does'nt exist.");

        if (VerticesCount <= MaxSize / 2) HalfMatrixSize();

        var index = vertexIndices[value];

        //clear edges
        for (var i = 0; i < MaxSize; i++)
        {
            matrix[i, index] = default;
            matrix[index, i] = default;
        }

        reverseVertexIndices.Remove(index);
        vertexIndices.Remove(value);
        vertexObjects.Remove(value);
    }

    /// <summary>
    ///     Add a new edge to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddEdge(T source, T dest, TW weight)
    {
        if (weight.Equals(default(TW))) throw new Exception("Cannot add default edge weight.");

        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (!matrix[sourceIndex, destIndex].Equals(default(TW))) throw new Exception("Edge already exists.");

        matrix[sourceIndex, destIndex] = weight;
    }

    /// <summary>
    ///     Remove the given edge from this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void RemoveEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (matrix[sourceIndex, destIndex].Equals(default(TW))) throw new Exception("Edge do not exists.");

        matrix[sourceIndex, destIndex] = default;
    }

    public int OutEdgeCount(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        var count = 0;

        for (var i = 0; i < MaxSize; i++)
            if (!matrix[index, i].Equals(default(TW)))
                count++;

        return count;
    }

    public IEnumerable<KeyValuePair<T, TW>> OutEdges(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        for (var i = 0; i < MaxSize; i++)
            if (!matrix[index, i].Equals(default(TW)))
                yield return new KeyValuePair<T, TW>(reverseVertexIndices[i], matrix[index, i]);
    }

    public int InEdgeCount(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        var count = 0;

        for (var i = 0; i < MaxSize; i++)
            if (!matrix[i, index].Equals(default(TW)))
                count++;

        return count;
    }

    public IEnumerable<KeyValuePair<T, TW>> InEdges(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        for (var i = 0; i < MaxSize; i++)
            if (!matrix[i, index].Equals(default(TW)))
                yield return new KeyValuePair<T, TW>(reverseVertexIndices[i], matrix[i, index]);
    }

    private void DoubleMatrixSize()
    {
        var newMatrix = new TW[MaxSize * 2, MaxSize * 2];

        var newVertexIndices = new Dictionary<T, int>();
        var newReverseIndices = new Dictionary<int, T>();

        var k = 0;
        foreach (var vertex in vertexIndices)
        {
            newVertexIndices.Add(vertex.Key, k);
            newReverseIndices.Add(k, vertex.Key);
            k++;
        }

        nextAvailableIndex = k;

        for (var i = 0; i < MaxSize; i++)
        for (var j = 0; j < MaxSize; j++)
            if (!matrix[i, j].Equals(default(TW))
                && reverseVertexIndices.ContainsKey(i)
                && reverseVertexIndices.ContainsKey(j))
            {
                var newI = newVertexIndices[reverseVertexIndices[i]];
                var newJ = newVertexIndices[reverseVertexIndices[j]];

                newMatrix[newI, newJ] = matrix[i, j];
            }

        matrix = newMatrix;
        vertexIndices = newVertexIndices;
        reverseVertexIndices = newReverseIndices;
    }

    private void HalfMatrixSize()
    {
        var newMatrix = new TW[MaxSize / 2, MaxSize / 2];

        var newVertexIndices = new Dictionary<T, int>();
        var newReverseIndices = new Dictionary<int, T>();

        var k = 0;
        foreach (var vertex in vertexIndices)
        {
            newVertexIndices.Add(vertex.Key, k);
            newReverseIndices.Add(k, vertex.Key);
            k++;
        }

        nextAvailableIndex = k;

        for (var i = 0; i < MaxSize; i++)
        for (var j = 0; j < MaxSize; j++)
            if (!matrix[i, j].Equals(default(TW))
                && reverseVertexIndices.ContainsKey(i)
                && reverseVertexIndices.ContainsKey(j))
            {
                var newI = newVertexIndices[reverseVertexIndices[i]];
                var newJ = newVertexIndices[reverseVertexIndices[j]];

                newMatrix[newI, newJ] = matrix[i, j];
            }

        matrix = newMatrix;
        vertexIndices = newVertexIndices;
        reverseVertexIndices = newReverseIndices;
    }

    private IEnumerable<WeightedDiGraphVertex<T, TW>> GetVerticesAsEnumerable()
    {
        return this.Select(x => vertexObjects[x]);
    }

    public WeightedDiGraph<T, TW> Clone()
    {
        var graph = new WeightedDiGraph<T, TW>();

        foreach (var vertex in this) graph.AddVertex(vertex);

        foreach (var vertex in this)
        foreach (var edge in OutEdges(vertex))
            graph.AddEdge(vertex, edge.Key, edge.Value);

        return graph;
    }

    private class WeightedDiGraphVertex<T, TW> : IDiGraphVertex<T>, IGraphVertex<T> where TW : IComparable
    {
        private readonly WeightedDiGraph<T, TW> graph;
        private readonly int vertexIndex;

        internal WeightedDiGraphVertex(WeightedDiGraph<T, TW> graph, T vertexKey)
        {
            if (!graph.vertexIndices.ContainsKey(vertexKey))
                throw new ArgumentException("vertex is not in this graph.");

            this.graph = graph;
            Key = vertexKey;
            vertexIndex = graph.vertexIndices[vertexKey];
        }

        private int MaxSize => graph.MaxSize;
        private TW[,] Matrix => graph.matrix;

        private Dictionary<T, int> VertexIndices => graph.vertexIndices;
        private Dictionary<int, T> ReverseVertexIndices => graph.reverseVertexIndices;

        public T Key { get; }

        IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.OutEdges => graph.OutEdges(Key)
            .Select(x => new DiEdge<T, TW>(graph.vertexObjects[x.Key], x.Value));

        IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.InEdges => graph.InEdges(Key)
            .Select(x => new DiEdge<T, TW>(graph.vertexObjects[x.Key], x.Value));

        public int OutEdgeCount => graph.OutEdgeCount(Key);
        public int InEdgeCount => graph.InEdgeCount(Key);

        public IDiEdge<T> GetOutEdge(IDiGraphVertex<T> targetVertex)
        {
            if (!VertexIndices.ContainsKey(targetVertex.Key))
                throw new ArgumentException("vertex is not in this graph.");

            var index = VertexIndices[targetVertex.Key];
            var key = targetVertex as WeightedDiGraphVertex<T, TW>;
            return new DiEdge<T, TW>(targetVertex, Matrix[vertexIndex, index]);
        }

        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => graph.OutEdges(Key)
            .Select(x => new Edge<T, TW>(graph.vertexObjects[x.Key], x.Value));

        public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
        {
            if (!VertexIndices.ContainsKey(targetVertex.Key))
                throw new ArgumentException("vertex is not in this graph.");

            var index = VertexIndices[targetVertex.Key];
            var key = targetVertex as WeightedDiGraphVertex<T, TW>;
            return new Edge<T, TW>(targetVertex, Matrix[vertexIndex, index]);
        }
    }
}ParseOptions.0.jsonÂ[
zD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Graph\AdjacencyMatrix\WeightedGraph.cs—Zusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix;

/// <summary>
///     A weighted graph implementation using dynamically growing/shrinking adjacency matrix array.
///     IEnumerable enumerates all vertices.
/// </summary>
public class WeightedGraph<T, TW> : IGraph<T>, IEnumerable<T> where TW : IComparable
{
    private TW[,] matrix;
    private int nextAvailableIndex;
    private Dictionary<int, T> reverseVertexIndices;

    private Dictionary<T, int> vertexIndices;
    private readonly Dictionary<T, WeightedGraphVertex<T, TW>> vertexObjects;

    public WeightedGraph()
    {
        vertexIndices = new Dictionary<T, int>();
        reverseVertexIndices = new Dictionary<int, T>();
        vertexObjects = new Dictionary<T, WeightedGraphVertex<T, TW>>();
        matrix = new TW[1, 1];
    }

    private int MaxSize => matrix.GetLength(0);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return vertexIndices.Select(x => x.Key).GetEnumerator();
    }

    public int VerticesCount { get; private set; }

    public bool IsWeightedGraph => true;

    public IGraphVertex<T> ReferenceVertex => GetReferenceVertex();

    /// <summary>
    ///     Do we have an edge between given source and destination?
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];

        if (!matrix[sourceIndex, destIndex].Equals(default(TW))
            && !matrix[destIndex, sourceIndex].Equals(default(TW)))
            return true;

        return false;
    }

    public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => vertexObjects.Select(x => x.Value);

    public bool ContainsVertex(T key)
    {
        return vertexObjects.ContainsKey(key);
    }

    public IGraphVertex<T> GetVertex(T key)
    {
        return vertexObjects[key];
    }

    IGraph<T> IGraph<T>.Clone()
    {
        return Clone();
    }

    private WeightedGraphVertex<T, TW> GetReferenceVertex()
    {
        if (VerticesCount == 0) throw new Exception("Empty graph.");

        return vertexObjects[this.First()];
    }

    /// <summary>
    ///     Add a new vertex to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();
        if (vertexIndices.ContainsKey(value)) throw new Exception("Vertex exists.");

        if (VerticesCount < MaxSize / 2) HalfMatrixSize();

        if (nextAvailableIndex == MaxSize) DoubleMatrixSize();

        vertexIndices.Add(value, nextAvailableIndex);
        reverseVertexIndices.Add(nextAvailableIndex, value);
        vertexObjects.Add(value, new WeightedGraphVertex<T, TW>(this, value));
        nextAvailableIndex++;
        VerticesCount++;
    }

    /// <summary>
    ///     Remove given vertex from this graph.
    ///     Time complexity: O(V) where V is the number of vertices.
    /// </summary>
    public void RemoveVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        if (!vertexIndices.ContainsKey(value)) throw new Exception("Vertex does'nt exist.");

        if (VerticesCount <= MaxSize / 2) HalfMatrixSize();

        var index = vertexIndices[value];

        //clear edges
        for (var i = 0; i < MaxSize; i++)
        {
            matrix[i, index] = default;
            matrix[index, i] = default;
        }

        reverseVertexIndices.Remove(index);
        vertexIndices.Remove(value);
        vertexObjects.Remove(value);

        VerticesCount--;
    }

    /// <summary>
    ///     Add a new edge to this graph with given weight
    ///     and between given source and destination vertex.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddEdge(T source, T dest, TW weight)
    {
        if (weight.Equals(default(TW))) throw new Exception("Cannot add default edge weight.");

        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (!matrix[sourceIndex, destIndex].Equals(default(TW))
            && !matrix[destIndex, sourceIndex].Equals(default(TW)))
            throw new Exception("Edge already exists.");

        matrix[sourceIndex, destIndex] = weight;
        matrix[destIndex, sourceIndex] = weight;
    }

    /// <summary>
    ///     Remove given edge.
    ///     Time complexity: O(1).
    /// </summary>
    public void RemoveEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new Exception("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (matrix[sourceIndex, destIndex].Equals(default(TW))
            && matrix[destIndex, sourceIndex].Equals(default(TW)))
            throw new Exception("Edge do not exists.");

        matrix[sourceIndex, destIndex] = default;
        matrix[destIndex, sourceIndex] = default;
    }

    public IEnumerable<KeyValuePair<T, TW>> Edges(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        for (var i = 0; i < MaxSize; i++)
            if (!matrix[i, index].Equals(default(TW)))
                yield return new KeyValuePair<T, TW>(reverseVertexIndices[i], matrix[i, index]);
    }

    public int EdgeCount(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        var index = vertexIndices[vertex];

        var count = 0;

        for (var i = 0; i < MaxSize; i++)
            if (!matrix[i, index].Equals(default(TW)))
                count++;

        return count;
    }

    private void DoubleMatrixSize()
    {
        var newMatrix = new TW[MaxSize * 2, MaxSize * 2];

        var newVertexIndices = new Dictionary<T, int>();
        var newReverseIndices = new Dictionary<int, T>();

        var k = 0;
        foreach (var vertex in vertexIndices)
        {
            newVertexIndices.Add(vertex.Key, k);
            newReverseIndices.Add(k, vertex.Key);
            k++;
        }

        nextAvailableIndex = k;

        for (var i = 0; i < MaxSize; i++)
        for (var j = i; j < MaxSize; j++)
            if (!matrix[i, j].Equals(default(TW)) && !matrix[j, i].Equals(default(TW))
                                                  && reverseVertexIndices.ContainsKey(i)
                                                  && reverseVertexIndices.ContainsKey(j))
            {
                var newI = newVertexIndices[reverseVertexIndices[i]];
                var newJ = newVertexIndices[reverseVertexIndices[j]];

                newMatrix[newI, newJ] = matrix[i, j];
                newMatrix[newJ, newI] = matrix[j, i];
            }

        matrix = newMatrix;
        vertexIndices = newVertexIndices;
        reverseVertexIndices = newReverseIndices;
    }

    private void HalfMatrixSize()
    {
        var newMatrix = new TW[MaxSize * 2, MaxSize * 2];

        var newVertexIndices = new Dictionary<T, int>();
        var newReverseIndices = new Dictionary<int, T>();

        var k = 0;
        foreach (var vertex in vertexIndices)
        {
            newVertexIndices.Add(vertex.Key, k);
            newReverseIndices.Add(k, vertex.Key);
            k++;
        }

        nextAvailableIndex = k;

        for (var i = 0; i < MaxSize; i++)
        for (var j = i; j < MaxSize; j++)
            if (!matrix[i, j].Equals(default(TW)) && !matrix[j, i].Equals(default(TW))
                                                  && reverseVertexIndices.ContainsKey(i)
                                                  && reverseVertexIndices.ContainsKey(j))
            {
                var newI = newVertexIndices[reverseVertexIndices[i]];
                var newJ = newVertexIndices[reverseVertexIndices[j]];

                newMatrix[newI, newJ] = matrix[i, j];
                newMatrix[newJ, newI] = matrix[j, i];
            }

        matrix = newMatrix;
        vertexIndices = newVertexIndices;
        reverseVertexIndices = newReverseIndices;
    }

    /// <summary>
    ///     Clones this graph.
    /// </summary>
    public WeightedGraph<T, TW> Clone()
    {
        var graph = new WeightedGraph<T, TW>();

        foreach (var vertex in this) graph.AddVertex(vertex);

        foreach (var vertex in this)
        foreach (var edge in Edges(vertex))
            graph.AddEdge(vertex, edge.Key, edge.Value);

        return graph;
    }

    private class WeightedGraphVertex<T, TW> : IGraphVertex<T> where TW : IComparable
    {
        private readonly WeightedGraph<T, TW> graph;
        private readonly int vertexIndex;

        internal WeightedGraphVertex(WeightedGraph<T, TW> graph, T vertexKey)
        {
            if (!graph.vertexIndices.ContainsKey(vertexKey))
                throw new ArgumentException("vertex is not in this graph.");

            this.graph = graph;
            Key = vertexKey;
            vertexIndex = graph.vertexIndices[vertexKey];
        }

        private int MaxSize => graph.MaxSize;
        private TW[,] Matrix => graph.matrix;

        private Dictionary<T, int> VertexIndices => graph.vertexIndices;
        private Dictionary<int, T> ReverseVertexIndices => graph.reverseVertexIndices;

        public T Key { get; }


        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => graph.Edges(Key)
            .Select(x => new Edge<T, TW>(graph.vertexObjects[x.Key], x.Value));

        public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
        {
            if (!VertexIndices.ContainsKey(targetVertex.Key))
                throw new ArgumentException("vertex is not in this graph.");

            var index = VertexIndices[targetVertex.Key];
            var key = targetVertex as WeightedGraphVertex<T, TW>;
            return new Edge<T, TW>(targetVertex, Matrix[vertexIndex, index]);
        }

        public IEdge<T> GetOutEdge(IGraphVertex<T> targetVertex)
        {
            if (!VertexIndices.ContainsKey(targetVertex.Key))
                throw new ArgumentException("vertex is not in this graph.");

            var index = VertexIndices[targetVertex.Key];
            var key = targetVertex as WeightedGraphVertex<T, TW>;
            return new Edge<T, TW>(targetVertex, Matrix[vertexIndex, index]);
        }
    }
}ParseOptions.0.json≥
eD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Graph\IDiGraph.cs¥using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Graph;

/// <summary>
///     Directed graph.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDiGraph<T>
{
    bool IsWeightedGraph { get; }
    IDiGraphVertex<T> ReferenceVertex { get; }
    IEnumerable<IDiGraphVertex<T>> VerticesAsEnumberable { get; }
    int VerticesCount { get; }

    bool ContainsVertex(T value);
    IDiGraphVertex<T> GetVertex(T key);

    bool HasEdge(T source, T destination);

    IDiGraph<T> Clone();
}

public interface IDiGraphVertex<T>
{
    T Key { get; }
    IEnumerable<IDiEdge<T>> OutEdges { get; }
    IEnumerable<IDiEdge<T>> InEdges { get; }

    int OutEdgeCount { get; }
    int InEdgeCount { get; }

    IDiEdge<T> GetOutEdge(IDiGraphVertex<T> targetVertex);
}

public interface IDiEdge<T>
{
    T TargetVertexKey { get; }
    IDiGraphVertex<T> TargetVertex { get; }
    TW Weight<TW>() where TW : IComparable;
}

internal class DiEdge<T, TC> : IDiEdge<T> where TC : IComparable
{
    private readonly object weight;

    internal DiEdge(IDiGraphVertex<T> target, TC weight)
    {
        TargetVertex = target;
        this.weight = weight;
    }

    public T TargetVertexKey => TargetVertex.Key;

    public IDiGraphVertex<T> TargetVertex { get; }

    public TW Weight<TW>() where TW : IComparable
    {
        return (TW)weight;
    }
}ParseOptions.0.jsonÛ
cD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Graph\IGraph.csˆ
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Graph;

/// <summary>
///     UnDirected graph. (When implemented on a directed graphs only outgoing edges are considered as Edges).
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGraph<T>
{
    bool IsWeightedGraph { get; }

    int VerticesCount { get; }
    IGraphVertex<T> ReferenceVertex { get; }
    IEnumerable<IGraphVertex<T>> VerticesAsEnumberable { get; }
    bool ContainsVertex(T key);
    IGraphVertex<T> GetVertex(T key);

    bool HasEdge(T source, T destination);

    IGraph<T> Clone();
}

public interface IGraphVertex<T>
{
    T Key { get; }
    IEnumerable<IEdge<T>> Edges { get; }

    IEdge<T> GetEdge(IGraphVertex<T> targetVertex);
}

public interface IEdge<T>
{
    T TargetVertexKey { get; }
    IGraphVertex<T> TargetVertex { get; }
    TW Weight<TW>() where TW : IComparable;
}

internal class Edge<T, TC> : IEdge<T> where TC : IComparable
{
    private readonly object weight;

    internal Edge(IGraphVertex<T> target, TC weight)
    {
        TargetVertex = target;
        this.weight = weight;
    }

    public T TargetVertexKey => TargetVertex.Key;

    public IGraphVertex<T> TargetVertex { get; }

    public TW Weight<TW>() where TW : IComparable
    {
        return (TW)weight;
    }
}ParseOptions.0.jsonÚ
fD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\HashSet\HashSet.csÚusing System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

/// <summary>
///     A hash table implementation.
/// </summary>
/// <typeparam name="T">The value datatype.</typeparam>
public class HashSet<T> : IEnumerable<T>
{
    private readonly IHashSet<T> hashSet;

    /// <param name="type">The hashSet implementation to use.</param>
    /// <param name="initialBucketSize"> The larger the bucket size lesser the collision, but memory matters!</param>
    public HashSet(HashSetType type = HashSetType.SeparateChaining, int initialBucketSize = 2)
    {
        if (initialBucketSize < 2) throw new Exception("Bucket Size must be greater than 2.");
        if (type == HashSetType.SeparateChaining)
            hashSet = new SeparateChainingHashSet<T>(initialBucketSize);
        else
            hashSet = new OpenAddressHashSet<T>(initialBucketSize);
    }

    /// <summary>
    ///     The number of items in this hashset.
    /// </summary>
    public int Count => hashSet.Count;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return hashSet.GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return hashSet.GetEnumerator();
    }


    /// <summary>
    ///     Does this hash table contains the given value.
    ///     Time complexity: O(1) amortized.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if this hashset contains the given value.</returns>
    public bool Contains(T value)
    {
        return hashSet.Contains(value);
    }

    /// <summary>
    ///     Add a new value.
    ///     Time complexity: O(1) amortized.
    /// </summary>
    /// <param name="value">The value to add.</param>
    public void Add(T value)
    {
        hashSet.Add(value);
    }

    /// <summary>
    ///     Remove the given value.
    ///     Time complexity: O(1) amortized.
    /// </summary>
    /// <param name="value">The value to remove.</param>
    public void Remove(T value)
    {
        hashSet.Remove(value);
    }

    /// <summary>
    ///     Clear the hashtable.
    ///     Time complexity: O(1).
    /// </summary>
    public void Clear()
    {
        hashSet.Clear();
    }
}

internal interface IHashSet<T> : IEnumerable<T>
{
    int Count { get; }
    bool Contains(T value);
    void Add(T value);
    void Remove(T key);
    void Clear();
}

/// <summary>
///     The hash set implementation type.
/// </summary>
public enum HashSetType
{
    SeparateChaining,
    OpenAddressing
}ParseOptions.0.jsonÿ8
qD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\HashSet\OpenAddressHashSet.csÕ7using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class OpenAddressHashSet<T> : IHashSet<T>
{
    private readonly int initialBucketSize;
    private HashSetNode<T>[] hashArray;

    internal OpenAddressHashSet(int initialBucketSize = 2)
    {
        this.initialBucketSize = initialBucketSize;
        hashArray = new HashSetNode<T>[initialBucketSize];
    }

    private int BucketSize => hashArray.Length;

    public int Count { get; private set; }

    public bool Contains(T value)
    {
        var hashCode = GetHash(value);
        var index = hashCode % BucketSize;

        if (hashArray[index] == null) return false;

        var current = hashArray[index];

        //keep track of this so that we won't circle around infinitely
        var hitKey = current.Value;

        while (current != null)
        {
            if (current.Value.Equals(value)) return true;

            index++;

            //wrap around
            if (index == BucketSize)
                index = 0;

            current = hashArray[index];

            //reached original hit again
            if (current != null && current.Value.Equals(hitKey)) break;
        }

        return false;
    }

    public void Add(T value)
    {
        Grow();

        var hashCode = GetHash(value);

        var index = hashCode % BucketSize;

        if (hashArray[index] == null)
        {
            hashArray[index] = new HashSetNode<T>(value);
        }
        else
        {
            var current = hashArray[index];
            //keep track of this so that we won't circle around infinitely
            var hitKey = current.Value;

            while (current != null)
            {
                if (current.Value.Equals(value)) throw new Exception("Duplicate value");

                index++;

                //wrap around
                if (index == BucketSize)
                    index = 0;

                current = hashArray[index];

                if (current != null && current.Value.Equals(hitKey)) throw new Exception("HashSet is full");
            }

            hashArray[index] = new HashSetNode<T>(value);
        }

        Count++;
    }

    public void Remove(T value)
    {
        var hashCode = GetHash(value);
        var curIndex = hashCode % BucketSize;

        if (hashArray[curIndex] == null) throw new Exception("No such item for given value");

        var current = hashArray[curIndex];

        //prevent circling around infinitely
        var hitKey = current.Value;

        HashSetNode<T> target = null;

        while (current != null)
        {
            if (current.Value.Equals(value))
            {
                target = current;
                break;
            }

            curIndex++;

            //wrap around
            if (curIndex == BucketSize)
                curIndex = 0;

            current = hashArray[curIndex];

            if (current != null && current.Value.Equals(hitKey)) throw new Exception("No such item for given value");
        }

        //remove
        if (target == null)
        {
            throw new Exception("No such item for given value");
        }

        //delete this element
        hashArray[curIndex] = null;

        //now time to cleanup subsequent broken hash elements due to this emptied cell
        curIndex++;

        //wrap around
        if (curIndex == BucketSize)
            curIndex = 0;

        current = hashArray[curIndex];

        //until an empty cell
        while (current != null)
        {
            //delete current
            hashArray[curIndex] = null;

            //add current back to table
            Add(current.Value);
            Count--;

            curIndex++;

            //wrap around
            if (curIndex == BucketSize)
                curIndex = 0;

            current = hashArray[curIndex];
        }

        Count--;

        Shrink();
    }

    public void Clear()
    {
        hashArray = new HashSetNode<T>[initialBucketSize];
        Count = 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new OpenAddressHashSetEnumerator<T>(hashArray, hashArray.Length);
    }

    private void Grow()
    {
        if (!(BucketSize * 0.7 <= Count)) return;

        var orgBucketSize = BucketSize;
        var currentArray = hashArray;

        //increase array size exponentially on demand
        hashArray = new HashSetNode<T>[BucketSize * 2];

        for (var i = 0; i < orgBucketSize; i++)
        {
            var current = currentArray[i];

            if (current != null)
            {
                Add(current.Value);
                Count--;
            }
        }

        currentArray = null;
    }


    private void Shrink()
    {
        if (Count <= BucketSize * 0.3 && BucketSize / 2 > initialBucketSize)
        {
            var orgBucketSize = BucketSize;

            var currentArray = hashArray;

            //reduce array by half logarithamic
            hashArray = new HashSetNode<T>[BucketSize / 2];

            for (var i = 0; i < orgBucketSize; i++)
            {
                var current = currentArray[i];

                if (current != null)
                {
                    Add(current.Value);
                    Count--;
                }
            }

            currentArray = null;
        }
    }

    private int GetHash(T value)
    {
        return Math.Abs(value.GetHashCode());
    }
}

internal class HashSetNode<T>
{
    internal T Value;

    internal HashSetNode(T value)
    {
        Value = value;
    }
}

internal class OpenAddressHashSetEnumerator<TV> : IEnumerator<TV>
{
    internal HashSetNode<TV>[] HashArray;
    private int length;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    private int position = -1;

    internal OpenAddressHashSetEnumerator(HashSetNode<TV>[] hashArray, int length)
    {
        this.length = length;
        this.HashArray = hashArray;
    }

    public bool MoveNext()
    {
        position++;

        while (position < length && HashArray[position] == null)
            position++;

        return position < length;
    }

    public void Reset()
    {
        position = -1;
    }

    object IEnumerator.Current => Current;

    public TV Current
    {
        get
        {
            try
            {
                return HashArray[position].Value;
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    public void Dispose()
    {
        length = 0;
        HashArray = null;
    }
}ParseOptions.0.json†%
mD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\HashSet\OrderedHashSet.csô$using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

/// <summary>
///     A sorted HashSet implementation using balanced binary search tree. IEnumerable will enumerate in sorted order.
///     This may be better than regular HashSet implementation which can give o(K) in worst case (but O(1) amortized when
///     collisions K is avoided).
/// </summary>
/// <typeparam name="T">The value datatype.</typeparam>
public class OrderedHashSet<T> : IEnumerable<T> where T : IComparable
{
    //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
    private readonly RedBlackTree<T> binarySearchTree;

    public OrderedHashSet()
    {
        binarySearchTree = new RedBlackTree<T>();
    }

    /// <summary>
    ///     Initialize the sorted hashset with given sorted key collection.
    ///     Time complexity: log(n).
    /// </summary>
    public OrderedHashSet(IEnumerable<T> sortedKeys)
    {
        binarySearchTree = new RedBlackTree<T>(sortedKeys);
    }

    public int Count => binarySearchTree.Count;

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T this[int index] => ElementAt(index);

    //Implementation for the GetEnumerator method.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return binarySearchTree.GetEnumerator();
    }

    /// <summary>
    ///     Does this hash table contains the given value.
    ///     Time complexity: O(log(n)).
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if this hashset contains the given value.</returns>
    public bool Contains(T value)
    {
        return binarySearchTree.HasItem(value);
    }

    /// <summary>
    ///     Add a new key.
    ///     Time complexity: O(log(n)).
    ///     Returns the position (index) of the key in sorted order of this OrderedHashSet.
    /// </summary>
    public int Add(T key)
    {
        return binarySearchTree.Insert(key);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T ElementAt(int index)
    {
        return binarySearchTree.ElementAt(index);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public int IndexOf(T key)
    {
        return binarySearchTree.IndexOf(key);
    }

    /// <summary>
    ///     Remove the given key if present.
    ///     Time complexity: O(log(n)).
    ///     Returns the position (index) of the removed key if removed. Otherwise returns -1.
    /// </summary>
    public int Remove(T key)
    {
        return binarySearchTree.Delete(key);
    }

    /// <summary>
    ///     Remove the element at given index.
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T RemoveAt(int index)
    {
        return binarySearchTree.RemoveAt(index);
    }

    /// <summary>
    ///     Return the next higher value after given value in this hashset.
    ///     Time complexity: O(log(n)).
    /// </summary>
    /// <returns>Null if the given value does'nt exist or next value does'nt exist.</returns>
    public T NextHigher(T value)
    {
        return binarySearchTree.NextHigher(value);
    }

    /// <summary>
    ///     Return the next lower value before given value in this HashSet.
    ///     Time complexity: O(log(n)).
    /// </summary>
    /// <returns>Null if the given value does'nt exist or previous value does'nt exist.</returns>
    public T NextLower(T value)
    {
        return binarySearchTree.NextLower(value);
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Max()
    {
        return binarySearchTree.Max();
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Min()
    {
        return binarySearchTree.Min();
    }

    /// <summary>
    ///     Clear the hashtable.
    ///     Time complexity: O(1).
    /// </summary>
    internal void Clear()
    {
        binarySearchTree.Clear();
    }

    /// <summary>
    ///     Descending enumerable.
    /// </summary>
    public IEnumerable<T> AsEnumerableDesc()
    {
        return GetEnumeratorDesc().AsEnumerable();
    }

    public IEnumerator<T> GetEnumeratorDesc()
    {
        return binarySearchTree.GetEnumeratorDesc();
    }
}ParseOptions.0.json÷B
vD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\HashSet\SeparateChainingHashSet.cs∆Ausing System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class SeparateChainingHashSet<T> : IHashSet<T>
{
    private const double Tolerance = 0.1;
    private readonly int initialBucketSize;
    private int filledBuckets;
    private DoublyLinkedList<HashSetNode<T>>[] hashArray;

    internal SeparateChainingHashSet(int initialBucketSize = 3)
    {
        this.initialBucketSize = initialBucketSize;
        hashArray = new DoublyLinkedList<HashSetNode<T>>[initialBucketSize];
    }

    private int BucketSize => hashArray.Length;

    public int Count { get; private set; }

    public bool Contains(T value)
    {
        var index = Math.Abs(value.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) return false;

        var current = hashArray[index].Head;

        while (current != null)
        {
            if (current.Data.Value.Equals(value)) return true;

            current = current.Next;
        }

        return false;
    }

    public void Add(T value)
    {
        Grow();

        var index = Math.Abs(value.GetHashCode()) % BucketSize;

        if (hashArray[index] == null)
        {
            hashArray[index] = new DoublyLinkedList<HashSetNode<T>>();
            hashArray[index].InsertFirst(new HashSetNode<T>(value));
            filledBuckets++;
        }
        else
        {
            var current = hashArray[index].Head;

            while (current != null)
            {
                if (current.Data.Value.Equals(value)) throw new Exception("Duplicate value");

                current = current.Next;
            }

            hashArray[index].InsertFirst(new HashSetNode<T>(value));
        }

        Count++;
    }

    public void Remove(T value)
    {
        var index = Math.Abs(value.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) throw new Exception("No such item for given value");

        var current = hashArray[index].Head;

        //TODO merge both search and remove to a single loop here!
        DoublyLinkedListNode<HashSetNode<T>> item = null;
        while (current != null)
        {
            if (current.Data.Value.Equals(value))
            {
                item = current;
                break;
            }

            current = current.Next;
        }

        //remove
        if (item == null)
        {
            throw new Exception("No such item for given value");
        }

        hashArray[index].Delete(item);

        //if list is empty mark bucket as null
        if (hashArray[index].Head == null)
        {
            hashArray[index] = null;
            filledBuckets--;
        }

        Count--;

        Shrink();
    }

    public void Clear()
    {
        hashArray = new DoublyLinkedList<HashSetNode<T>>[initialBucketSize];
        Count = 0;
        filledBuckets = 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new SeparateChainingHashSetEnumerator<T>(hashArray, BucketSize);
    }

    private void SetValue(T value)
    {
        var index = Math.Abs(value.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) throw new Exception("Item not found");

        var current = hashArray[index].Head;

        while (current != null)
        {
            if (current.Data.Value.Equals(value))
            {
                Remove(value);
                Add(value);
                return;
            }

            current = current.Next;
        }

        throw new Exception("Item not found");
    }

    private void Grow()
    {
        if (filledBuckets >= BucketSize * 0.7)
        {
            filledBuckets = 0;
            //increase array size exponentially on demand
            var newBucketSize = BucketSize * 2;

            var biggerArray = new DoublyLinkedList<HashSetNode<T>>[newBucketSize];

            for (var i = 0; i < BucketSize; i++)
            {
                var item = hashArray[i];

                //hashcode changes when bucket size changes
                if (item != null)
                    if (item.Head != null)
                    {
                        var current = item.Head;

                        //find new location for each item
                        while (current != null)
                        {
                            var next = current.Next;

                            var newIndex = Math.Abs(current.Data.Value.GetHashCode()) % newBucketSize;

                            if (biggerArray[newIndex] == null)
                            {
                                filledBuckets++;
                                biggerArray[newIndex] = new DoublyLinkedList<HashSetNode<T>>();
                            }

                            biggerArray[newIndex].InsertFirst(current);

                            current = next;
                        }
                    }
            }

            hashArray = biggerArray;
        }
    }

    private void Shrink()
    {
        if (Math.Abs(filledBuckets - BucketSize * 0.3) < Tolerance && BucketSize / 2 > initialBucketSize)
        {
            filledBuckets = 0;
            //reduce array by half 
            var newBucketSize = BucketSize / 2;

            var smallerArray = new DoublyLinkedList<HashSetNode<T>>[newBucketSize];

            for (var i = 0; i < BucketSize; i++)
            {
                var item = hashArray[i];

                //hashcode changes when bucket size changes
                if (item?.Head != null)
                {
                    var current = item.Head;

                    //find new location for each item
                    while (current != null)
                    {
                        var next = current.Next;

                        var newIndex = Math.Abs(current.Data.Value.GetHashCode()) % newBucketSize;

                        if (smallerArray[newIndex] == null)
                        {
                            filledBuckets++;
                            smallerArray[newIndex] = new DoublyLinkedList<HashSetNode<T>>();
                        }

                        smallerArray[newIndex].InsertFirst(current);

                        current = next;
                    }
                }
            }

            hashArray = smallerArray;
        }
    }
}

internal class SeparateChainingHashSetEnumerator<T> : IEnumerator<T>
{
    private DoublyLinkedListNode<HashSetNode<T>> currentNode;
    internal DoublyLinkedList<HashSetNode<T>>[] HashList;

    private int length;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    private int position = -1;

    internal SeparateChainingHashSetEnumerator(DoublyLinkedList<HashSetNode<T>>[] hashList, int length)
    {
        this.length = length;
        this.HashList = hashList;
    }

    public bool MoveNext()
    {
        if (currentNode?.Next != null)
        {
            currentNode = currentNode.Next;
            return true;
        }

        while (currentNode?.Next == null)
        {
            position++;

            if (position < length)
            {
                if (HashList[position] == null)
                    continue;

                currentNode = HashList[position].Head;

                if (currentNode == null)
                    continue;

                return true;
            }

            break;
        }

        return false;
    }

    public void Reset()
    {
        position = -1;
        currentNode = null;
    }

    object IEnumerator.Current => Current;

    public T Current
    {
        get
        {
            try
            {
                return currentNode.Data.Value;
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    public void Dispose()
    {
        length = 0;
        HashList = null;
    }
}ParseOptions.0.jsonÅ>
aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Heap\BHeap.csÜ=using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A binary heap implementation.
/// </summary>
public class BHeap<T> : IEnumerable<T> where T : IComparable
{
    private readonly IComparer<T> comparer;
    private readonly bool isMaxHeap;

    private T[] heapArray;

    public BHeap(SortDirection sortDirection = SortDirection.Ascending)
        : this(sortDirection, null, null)
    {
    }

    public BHeap(SortDirection sortDirection, IEnumerable<T> initial)
        : this(sortDirection, initial, null)
    {
    }

    public BHeap(SortDirection sortDirection, IComparer<T> comparer)
        : this(sortDirection, null, comparer)
    {
    }

    /// <summary>
    ///     Time complexity: O(n) if initial is provided. Otherwise O(1).
    /// </summary>
    /// <param name="initial">The initial items in the heap.</param>
    public BHeap(SortDirection sortDirection, IEnumerable<T> initial, IComparer<T> comparer)
    {
        isMaxHeap = sortDirection == SortDirection.Descending;

        if (comparer != null)
            this.comparer = new CustomComparer<T>(sortDirection, comparer);
        else
            this.comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        if (initial != null)
        {
            var items = initial as T[] ?? initial.ToArray();
            var initArray = new T[items.Count()];

            var i = 0;
            foreach (var item in items)
            {
                initArray[i] = item;
                i++;
            }

            BulkInit(initArray);
            Count = initArray.Length;
        }
        else
        {
            heapArray = new T[2];
        }
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return heapArray.Take(Count).GetEnumerator();
    }

    private void BulkInit(T[] initial)
    {
        var i = (initial.Length - 1) / 2;

        while (i >= 0)
        {
            BulkInitRecursive(i, initial);
            i--;
        }

        heapArray = initial;
    }

    private void BulkInitRecursive(int i, T[] initial)
    {
        var parent = i;

        var left = 2 * i + 1;
        var right = 2 * i + 2;

        var minMax = left < initial.Length && right < initial.Length
            ? comparer.Compare(initial[left], initial[right]) < 0 ? left : right
            : left < initial.Length
                ? left
                : right < initial.Length
                    ? right
                    : -1;

        if (minMax != -1 && comparer.Compare(initial[minMax], initial[parent]) < 0)
        {
            var temp = initial[minMax];
            initial[minMax] = initial[parent];
            initial[parent] = temp;

            //if min is child then drill down child
            BulkInitRecursive(minMax, initial);
        }
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Insert(T newItem)
    {
        if (Count == heapArray.Length) DoubleArray();

        heapArray[Count] = newItem;

        for (var i = Count; i > 0; i = (i - 1) / 2)
            if (comparer.Compare(heapArray[i], heapArray[(i - 1) / 2]) < 0)
            {
                var temp = heapArray[(i - 1) / 2];
                heapArray[(i - 1) / 2] = heapArray[i];
                heapArray[i] = temp;
            }
            else
            {
                break;
            }

        Count++;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Extract()
    {
        if (Count == 0) throw new Exception("Empty heap");

        var minMax = heapArray[0];

        Delete(0);

        return minMax;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T Peek()
    {
        if (Count == 0) throw new Exception("Empty heap");

        return heapArray[0];
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public void Delete(T value)
    {
        var index = FindIndex(value);

        if (index != -1)
        {
            Delete(index);
            return;
        }

        throw new Exception("Item not found.");
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public bool Exists(T value)
    {
        return FindIndex(value) != -1;
    }

    private void Delete(int parentIndex)
    {
        heapArray[parentIndex] = heapArray[Count - 1];
        Count--;

        //percolate down
        while (true)
        {
            var leftIndex = 2 * parentIndex + 1;
            var rightIndex = 2 * parentIndex + 2;

            var parent = heapArray[parentIndex];

            if (leftIndex < Count && rightIndex < Count)
            {
                var leftChild = heapArray[leftIndex];
                var rightChild = heapArray[rightIndex];

                var leftIsMinMax = false;

                if (comparer.Compare(leftChild, rightChild) < 0) leftIsMinMax = true;

                var minMaxChildIndex = leftIsMinMax ? leftIndex : rightIndex;

                if (comparer.Compare(heapArray[minMaxChildIndex], parent) < 0)
                {
                    var temp = heapArray[parentIndex];
                    heapArray[parentIndex] = heapArray[minMaxChildIndex];
                    heapArray[minMaxChildIndex] = temp;

                    if (leftIsMinMax)
                        parentIndex = leftIndex;
                    else
                        parentIndex = rightIndex;
                }
                else
                {
                    break;
                }
            }
            else if (leftIndex < Count)
            {
                if (comparer.Compare(heapArray[leftIndex], parent) < 0)
                {
                    var temp = heapArray[parentIndex];
                    heapArray[parentIndex] = heapArray[leftIndex];
                    heapArray[leftIndex] = temp;

                    parentIndex = leftIndex;
                }
                else
                {
                    break;
                }
            }
            else if (rightIndex < Count)
            {
                if (comparer.Compare(heapArray[rightIndex], parent) < 0)
                {
                    var temp = heapArray[parentIndex];
                    heapArray[parentIndex] = heapArray[rightIndex];
                    heapArray[rightIndex] = temp;

                    parentIndex = rightIndex;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        if (heapArray.Length / 2 == Count && heapArray.Length > 2) HalfArray();
    }


    private int FindIndex(T value)
    {
        for (var i = 0; i < Count; i++)
            if (heapArray[i].Equals(value))
                return i;
        return -1;
    }

    private void HalfArray()
    {
        var smallerArray = new T[heapArray.Length / 2];

        for (var i = 0; i < Count; i++) smallerArray[i] = heapArray[i];

        heapArray = smallerArray;
    }

    private void DoubleArray()
    {
        var biggerArray = new T[heapArray.Length * 2];

        for (var i = 0; i < Count; i++) biggerArray[i] = heapArray[i];

        heapArray = biggerArray;
    }
}ParseOptions.0.json≠D
hD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Heap\BinomialHeap.cs´Cusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A binomial minMax heap implementation.
/// </summary>
public class BinomialHeap<T> : IEnumerable<T> where T : IComparable
{
    private readonly IComparer<T> comparer;
    private readonly bool isMaxHeap;

    private DoublyLinkedList<BinomialHeapNode<T>> heapForest = new();

    private readonly Dictionary<T, List<BinomialHeapNode<T>>> heapMapping = new();

    public BinomialHeap(SortDirection sortDirection = SortDirection.Ascending)
    {
        isMaxHeap = sortDirection == SortDirection.Descending;
        comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return heapMapping.SelectMany(x => x.Value).Select(x => x.Value).GetEnumerator();
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Insert(T newItem)
    {
        var newNode = new BinomialHeapNode<T>(newItem);

        var newHeapForest = new DoublyLinkedList<BinomialHeapNode<T>>();
        newHeapForest.InsertFirst(newNode);

        //updated pointer
        MergeSortedForests(newHeapForest);

        Meld();

        AddMapping(newItem, newNode);

        Count++;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Extract()
    {
        if (heapForest.Head == null) throw new Exception("Empty heap");

        var minMaxTree = heapForest.Head;
        var current = heapForest.Head;

        //find minMaximum tree
        while (current.Next != null)
        {
            current = current.Next;

            if (comparer.Compare(minMaxTree.Data.Value, current.Data.Value) > 0) minMaxTree = current;
        }

        //remove tree root
        heapForest.Delete(minMaxTree);

        var newHeapForest = new DoublyLinkedList<BinomialHeapNode<T>>();
        //add removed roots children as new trees to forest
        foreach (var child in minMaxTree.Data.Children)
        {
            child.Parent = null;
            newHeapForest.InsertLast(child);
        }

        MergeSortedForests(newHeapForest);

        Meld();

        RemoveMapping(minMaxTree.Data.Value, minMaxTree.Data);

        Count--;

        return minMaxTree.Data.Value;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    /// <param name="currentValue">The value to update.</param>
    /// <param name="newValue">The updated new value.</param>
    public void UpdateKey(T currentValue, T newValue)
    {
        var node = heapMapping[currentValue]?.Where(x => x.Value.Equals(currentValue)).FirstOrDefault();

        if (node == null) throw new Exception("Current value is not present in this heap.");

        if (comparer.Compare(newValue, node.Value) > 0)
            throw new Exception($"New value is not {(!isMaxHeap ? "less" : "greater")} than old value.");

        UpdateNodeValue(currentValue, newValue, node);

        var current = node;

        while (current.Parent != null
               && comparer.Compare(current.Value, current.Parent.Value) < 0)
        {
            //swap parent with child
            var tmp = current.Value;
            UpdateNodeValue(tmp, current.Parent.Value, current);
            UpdateNodeValue(current.Parent.Value, tmp, current.Parent);

            current = current.Parent;
        }
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    /// <param name="binomialHeap">The heap to union with.</param>
    public void Merge(BinomialHeap<T> binomialHeap)
    {
        MergeSortedForests(binomialHeap.heapForest);

        Meld();

        Count += binomialHeap.Count;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Peek()
    {
        if (heapForest.Head == null) throw new Exception("Empty heap");

        var minMaxTree = heapForest.Head;
        var current = heapForest.Head;

        //find  tree
        while (current.Next != null)
        {
            current = current.Next;

            if (comparer.Compare(minMaxTree.Data.Value, current.Data.Value) > 0) minMaxTree = current;
        }

        return minMaxTree.Data.Value;
    }

    /// <summary>
    ///     Merge roots with same degrees in Forest
    /// </summary>
    private void Meld()
    {
        if (heapForest.Head == null) return;


        var cur = heapForest.Head;
        var next = heapForest.Head.Next;

        while (next != null)
            //case 1
            //degrees are differant 
            //we are good to move ahead
            if (cur.Data.Degree != next.Data.Degree)
            {
                cur = next;
                next = cur.Next;
            }
            //degress of cur and next are same
            else
            {
                //case 2 next degree equals next-next degree
                if (next.Next != null &&
                    cur.Data.Degree == next.Next.Data.Degree)
                {
                    cur = next;
                    next = cur.Next;
                    continue;
                }

                //case 3 cur value is less than next
                if (comparer.Compare(cur.Data.Value, next.Data.Value) <= 0)
                {
                    //add next as child of current
                    cur.Data.Children.Add(next.Data);
                    next.Data.Parent = cur.Data;
                    heapForest.Delete(next);

                    next = cur.Next;
                    continue;
                }

                //case 4 cur value is greater than next
                if (comparer.Compare(cur.Data.Value, next.Data.Value) > 0)
                {
                    //add current as child of next
                    next.Data.Children.Add(cur.Data);
                    cur.Data.Parent = next.Data;

                    heapForest.Delete(cur);

                    cur = next;
                    next = cur.Next;
                }
            }
    }

    /// <summary>
    ///     Merges the given sorted forest to current sorted Forest
    ///     and returns the last inserted node (pointer required for update-key)
    /// </summary>
    private void MergeSortedForests(DoublyLinkedList<BinomialHeapNode<T>> newHeapForest)
    {
        var @new = newHeapForest.Head;

        if (heapForest.Head == null)
        {
            heapForest = newHeapForest;
            return;
        }

        var current = heapForest.Head;

        //insert at right spot and move forward
        while (@new != null && current != null)
            if (current.Data.Degree < @new.Data.Degree)
            {
                current = current.Next;
            }
            else if (current.Data.Degree > @new.Data.Degree)
            {
                heapForest.InsertBefore(current, new DoublyLinkedListNode<BinomialHeapNode<T>>(@new.Data));
                @new = @new.Next;
            }
            else
            {
                //equal
                heapForest.InsertAfter(current, new DoublyLinkedListNode<BinomialHeapNode<T>>(@new.Data));
                current = current.Next;
                @new = @new.Next;
            }

        //copy left overs
        while (@new != null)
        {
            heapForest.InsertAfter(heapForest.Tail, new DoublyLinkedListNode<BinomialHeapNode<T>>(@new.Data));
            @new = @new.Next;
        }
    }

    private void AddMapping(T newItem, BinomialHeapNode<T> newNode)
    {
        if (heapMapping.ContainsKey(newItem))
            heapMapping[newItem].Add(newNode);
        else
            heapMapping[newItem] = new List<BinomialHeapNode<T>>(new[] { newNode });
    }

    private void UpdateNodeValue(T currentValue, T newValue, BinomialHeapNode<T> node)
    {
        RemoveMapping(currentValue, node);
        node.Value = newValue;
        AddMapping(newValue, node);
    }

    private void RemoveMapping(T currentValue, BinomialHeapNode<T> node)
    {
        heapMapping[currentValue].Remove(node);
        if (heapMapping[currentValue].Count == 0) heapMapping.Remove(currentValue);
    }
}ParseOptions.0.jsonî.
eD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Heap\d-aryHeap.csï-using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A D-ary minMax heap implementation.
/// </summary>
public class DaryHeap<T> : IEnumerable<T> where T : IComparable
{
    private readonly IComparer<T> comparer;
    private readonly bool isMaxHeap;
    public int Count;

    private T[] heapArray;
    private readonly int k;

    /// <summary>
    ///     Time complexity: O(n) when initial is provided otherwise O(1).
    /// </summary>
    /// <param name="k">The number of children per heap node.</param>
    /// <param name="initial">The initial items if any.</param>
    public DaryHeap(int k, SortDirection sortDirection = SortDirection.Ascending, IEnumerable<T> initial = null)
    {
        isMaxHeap = sortDirection == SortDirection.Descending;
        comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        if (k <= 2) throw new Exception("Number of nodes k must be greater than 2.");

        this.k = k;

        if (initial != null)
        {
            var items = initial as T[] ?? initial.ToArray();
            var initArray = new T[items.Count()];

            var i = 0;
            foreach (var item in items)
            {
                initArray[i] = item;
                i++;
            }

            Count = initArray.Length;
            BulkInit(initArray);
        }
        else
        {
            heapArray = new T[k];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return heapArray.Take(Count).GetEnumerator();
    }

    /// <summary>
    ///     Initialize with given input.
    ///     Time complexity: O(n).
    /// </summary>
    private void BulkInit(T[] initial)
    {
        var i = (initial.Length - 1) / k;

        while (i >= 0)
        {
            BulkInitRecursive(i, initial);
            i--;
        }

        heapArray = initial;
    }

    /// <summary>
    ///     Recursively load bulk init values.
    /// </summary>
    private void BulkInitRecursive(int i, T[] initial)
    {
        var parent = i;
        var minMax = FindMinMaxChildIndex(i, initial);

        if (minMax != -1 && comparer.Compare(initial[minMax], initial[parent]) < 0)
        {
            var temp = initial[minMax];
            initial[minMax] = initial[parent];
            initial[parent] = temp;

            BulkInitRecursive(minMax, initial);
        }
    }


    /// <summary>
    ///     Time complexity: O(log(n) base K).
    /// </summary>
    public void Insert(T newItem)
    {
        if (Count == heapArray.Length) DoubleArray();

        heapArray[Count] = newItem;

        //percolate up
        for (var i = Count; i > 0; i = (i - 1) / k)
            if (comparer.Compare(heapArray[i], heapArray[(i - 1) / k]) < 0)
            {
                var temp = heapArray[(i - 1) / k];
                heapArray[(i - 1) / k] = heapArray[i];
                heapArray[i] = temp;
            }
            else
            {
                break;
            }

        Count++;
    }

    /// <summary>
    ///     Time complexity: O(log(n) base K).
    /// </summary>
    public T Extract()
    {
        if (Count == 0) throw new Exception("Empty heap");
        var minMax = heapArray[0];

        //move last element to top
        heapArray[0] = heapArray[Count - 1];
        Count--;

        var currentParent = 0;
        //now percolate down
        while (true)
        {
            var swapped = false;

            //init to left-most child
            var minMaxChildIndex = FindMinMaxChildIndex(currentParent, heapArray);

            if (minMaxChildIndex != -1 &&
                comparer.Compare(heapArray[currentParent], heapArray[minMaxChildIndex]) > 0)
            {
                var tmp = heapArray[minMaxChildIndex];
                heapArray[minMaxChildIndex] = heapArray[currentParent];
                heapArray[currentParent] = tmp;
                swapped = true;
            }

            if (!swapped) break;

            currentParent = minMaxChildIndex;
        }

        if (heapArray.Length / 2 == Count && heapArray.Length > 2) HalfArray();

        return minMax;
    }

    /// <summary>
    ///     Returns the max Index of child if any.
    ///     Otherwise returns -1.
    /// </summary>
    private int FindMinMaxChildIndex(int currentParent, T[] heap)
    {
        var currentMinMax = currentParent * k + 1;

        if (currentMinMax >= Count)
            return -1;

        for (var i = 2; i <= k; i++)
        {
            if (currentParent * k + i >= Count)
                break;

            var nextSibling = heap[currentParent * k + i];

            if (comparer.Compare(heap[currentMinMax], nextSibling) > 0) currentMinMax = currentParent * k + i;
        }

        return currentMinMax;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T Peek()
    {
        if (Count == 0) throw new Exception("Empty heap");

        return heapArray[0];
    }

    private void HalfArray()
    {
        var smallerArray = new T[heapArray.Length / 2];

        for (var i = 0; i < Count; i++) smallerArray[i] = heapArray[i];

        heapArray = smallerArray;
    }

    private void DoubleArray()
    {
        var biggerArray = new T[heapArray.Length * 2];

        for (var i = 0; i < Count; i++) biggerArray[i] = heapArray[i];

        heapArray = biggerArray;
    }
}ParseOptions.0.json›M
iD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Heap\FibonacciHeap.cs⁄Lusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A fibornacci minMax heap implementation.
/// </summary>
public class FibonacciHeap<T> : IEnumerable<T> where T : IComparable
{
    private readonly IComparer<T> comparer;
    private readonly bool isMaxHeap;

    private FibonacciHeapNode<T> heapForestHead;

    private readonly Dictionary<T, List<FibonacciHeapNode<T>>> heapMapping = new();

    //holds the min/max node at any given time
    private FibonacciHeapNode<T> minMaxNode;

    public FibonacciHeap(SortDirection sortDirection = SortDirection.Ascending)
    {
        isMaxHeap = sortDirection == SortDirection.Descending;
        comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return heapMapping.SelectMany(x => x.Value).Select(x => x.Value).GetEnumerator();
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Insert(T newItem)
    {
        var newNode = new FibonacciHeapNode<T>(newItem);

        //return pointer to new Node
        MergeForests(newNode);

        if (minMaxNode == null)
        {
            minMaxNode = newNode;
        }
        else
        {
            if (comparer.Compare(minMaxNode.Value, newNode.Value) > 0) minMaxNode = newNode;
        }

        AddMapping(newItem, newNode);

        Count++;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Extract()
    {
        if (heapForestHead == null) throw new Exception("Empty heap");

        var minMaxValue = minMaxNode.Value;

        RemoveMapping(minMaxValue, minMaxNode);

        //remove tree root
        DeleteNode(ref heapForestHead, minMaxNode);

        MergeForests(minMaxNode.ChildrenHead);
        Meld();

        Count--;

        return minMaxValue;
    }


    /// <summary>
    ///     Update the Heap with new value for this node pointer.
    ///     Time complexity: O(1).
    /// </summary>
    public void UpdateKey(T currentValue, T newValue)
    {
        var node = heapMapping[currentValue]?.Where(x => x.Value.Equals(currentValue)).FirstOrDefault();

        if (node == null) throw new Exception("Current value is not present in this heap.");

        if (comparer.Compare(newValue, node.Value) > 0)
            throw new Exception($"New value is not {(!isMaxHeap ? "less" : "greater")} than old value.");

        UpdateNodeValue(currentValue, newValue, node);

        if (node.Parent == null
            && comparer.Compare(minMaxNode.Value, node.Value) > 0)
            minMaxNode = node;

        var current = node;

        if (current.Parent == null || comparer.Compare(current.Value, current.Parent.Value) >= 0) return;

        var parent = current.Parent;

        //if parent already lost one child
        //then cut current and parent
        if (parent.LostChild)
        {
            parent.LostChild = false;

            var grandParent = parent.Parent;

            //mark grand parent
            if (grandParent == null) return;

            Cut(parent);
            Cut(current);
        }
        else
        {
            Cut(current);
        }
    }

    /// <summary>
    ///     Unions this heap with another.
    ///     Time complexity: O(1).
    /// </summary>
    public void Merge(FibonacciHeap<T> fibonacciHeap)
    {
        MergeForests(fibonacciHeap.heapForestHead);
        Count = Count + fibonacciHeap.Count;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T Peek()
    {
        if (heapForestHead == null) throw new Exception("Empty heap");

        return minMaxNode.Value;
    }

    /// <summary>
    ///     Merge roots with same degrees in Forest.
    /// </summary>
    private void Meld()
    {
        if (heapForestHead == null)
        {
            minMaxNode = null;
            return;
        }

        //degree - node dictionary
        var mergeDictionary = new Dictionary<int, FibonacciHeapNode<T>>();

        var current = heapForestHead;
        minMaxNode = current;
        while (current != null)
        {
            current.Parent = null;
            var next = current.Next;
            //no same degree already in merge dictionary
            //add to hash table
            if (!mergeDictionary.ContainsKey(current.Degree))
            {
                mergeDictionary.Add(current.Degree, current);

                if (minMaxNode == current) minMaxNode = null;

                DeleteNode(ref heapForestHead, current);

                current = next;
            }
            //insert back to forest by merging current tree 
            //with existing tree in merge dictionary
            else
            {
                var currentDegree = current.Degree;
                var existing = mergeDictionary[currentDegree];

                if (comparer.Compare(existing.Value, current.Value) < 0)
                {
                    current.Parent = existing;

                    DeleteNode(ref heapForestHead, current);

                    var childHead = existing.ChildrenHead;
                    InsertNode(ref childHead, current);
                    existing.ChildrenHead = childHead;

                    existing.Degree++;

                    InsertNode(ref heapForestHead, existing);
                    current = existing;
                    current.Next = next;
                }
                else
                {
                    existing.Parent = current;

                    var childHead = current.ChildrenHead;
                    InsertNode(ref childHead, existing);
                    current.ChildrenHead = childHead;

                    current.Degree++;
                }


                if (minMaxNode == null
                    || comparer.Compare(minMaxNode.Value, current.Value) > 0)
                    minMaxNode = current;

                mergeDictionary.Remove(currentDegree);
            }
        }

        //insert back trees with unique degrees to forest
        if (mergeDictionary.Count > 0)
        {
            foreach (var node in mergeDictionary)
            {
                InsertNode(ref heapForestHead, node.Value);

                if (minMaxNode == null
                    || comparer.Compare(minMaxNode.Value, node.Value.Value) > 0)
                    minMaxNode = node.Value;
            }

            mergeDictionary.Clear();
        }
    }

    /// <summary>
    ///     Delete this node from Heap Tree and adds it to forest as a new tree
    /// </summary>
    private void Cut(FibonacciHeapNode<T> node)
    {
        var parent = node.Parent;

        //cut child and attach to heap Forest
        //and mark parent for lost child
        var childHead = node.Parent.ChildrenHead;
        DeleteNode(ref childHead, node);
        node.Parent.ChildrenHead = childHead;

        node.Parent.Degree--;
        if (parent.Parent != null) parent.LostChild = true;
        node.LostChild = false;
        node.Parent = null;

        InsertNode(ref heapForestHead, node);

        //update 
        if (comparer.Compare(minMaxNode.Value, node.Value) > 0) minMaxNode = node;
    }

    /// <summary>
    ///     Merges the given fibornacci node list to current Forest
    /// </summary>
    private void MergeForests(FibonacciHeapNode<T> headPointer)
    {
        var current = headPointer;
        while (current != null)
        {
            var next = current.Next;
            InsertNode(ref heapForestHead, current);
            current = next;
        }
    }

    private void InsertNode(ref FibonacciHeapNode<T> head, FibonacciHeapNode<T> newNode)
    {
        newNode.Next = newNode.Previous = null;

        if (head == null)
        {
            head = newNode;
            return;
        }

        head.Previous = newNode;
        newNode.Next = head;

        head = newNode;
    }

    private void DeleteNode(ref FibonacciHeapNode<T> heapForestHead, FibonacciHeapNode<T> deletionNode)
    {
        if (deletionNode == heapForestHead)
        {
            if (deletionNode.Next != null) deletionNode.Next.Previous = null;

            heapForestHead = deletionNode.Next;
            deletionNode.Next = null;
            deletionNode.Previous = null;
            return;
        }

        deletionNode.Previous.Next = deletionNode.Next;

        if (deletionNode.Next != null) deletionNode.Next.Previous = deletionNode.Previous;

        deletionNode.Next = null;
        deletionNode.Previous = null;
    }

    private void AddMapping(T newItem, FibonacciHeapNode<T> newNode)
    {
        if (heapMapping.ContainsKey(newItem))
            heapMapping[newItem].Add(newNode);
        else
            heapMapping[newItem] = new List<FibonacciHeapNode<T>>(new[] { newNode });
    }

    private void UpdateNodeValue(T currentValue, T newValue, FibonacciHeapNode<T> node)
    {
        RemoveMapping(currentValue, node);
        node.Value = newValue;
        AddMapping(newValue, node);
    }

    private void RemoveMapping(T currentValue, FibonacciHeapNode<T> node)
    {
        heapMapping[currentValue].Remove(node);
        if (heapMapping[currentValue].Count == 0) heapMapping.Remove(currentValue);
    }
}ParseOptions.0.jsonŒ7
gD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Heap\PairingHeap.csÕ6using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A pairing minMax heap implementation.
/// </summary>
public class PairingHeap<T> : IEnumerable<T> where T : IComparable
{
    private readonly IComparer<T> comparer;
    private readonly bool isMaxHeap;

    private readonly Dictionary<T, List<PairingHeapNode<T>>> heapMapping = new();

    private PairingHeapNode<T> root;

    public PairingHeap(SortDirection sortDirection = SortDirection.Ascending)
    {
        isMaxHeap = sortDirection == SortDirection.Descending;
        comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return heapMapping.SelectMany(x => x.Value).Select(x => x.Value).GetEnumerator();
    }

    /// <summary>
    ///     Insert a new Node.
    ///     Time complexity: O(1).
    /// </summary>
    public void Insert(T newItem)
    {
        var newNode = new PairingHeapNode<T>(newItem);
        root = Meld(root, newNode);
        AddMapping(newItem, newNode);
        Count++;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Extract()
    {
        var minMax = root;
        RemoveMapping(minMax.Value, minMax);
        Meld(root.ChildrenHead);
        Count--;
        return minMax.Value;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void UpdateKey(T currentValue, T newValue)
    {
        var node = heapMapping[currentValue]?.Where(x => x.Value.Equals(currentValue)).FirstOrDefault();

        if (node == null) throw new Exception("Current value is not present in this heap.");

        if (comparer.Compare(newValue, node.Value) > 0)
            throw new Exception($"New value is not {(!isMaxHeap ? "less" : "greater")} than old value.");

        UpdateNodeValue(currentValue, newValue, node);

        if (node == root) return;

        DeleteChild(node);

        root = Meld(root, node);
    }

    /// <summary>
    ///     Merge another heap with this heap.
    ///     Time complexity: O(1).
    /// </summary>
    public void Merge(PairingHeap<T> pairingHeap)
    {
        root = Meld(root, pairingHeap.root);
        Count = Count + pairingHeap.Count;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T Peek()
    {
        if (root == null)
            throw new Exception("Empty heap");

        return root.Value;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    private void Meld(PairingHeapNode<T> headNode)
    {
        if (headNode == null)
            return;

        var passOneResult = new List<PairingHeapNode<T>>();

        var current = headNode;

        if (current.Next == null)
        {
            headNode.Next = null;
            headNode.Previous = null;
            passOneResult.Add(headNode);
        }
        else
        {
            while (true)
            {
                if (current == null) break;

                if (current.Next != null)
                {
                    var next = current.Next;
                    var nextNext = next.Next;
                    passOneResult.Add(Meld(current, next));
                    current = nextNext;
                }
                else
                {
                    var lastInserted = passOneResult[passOneResult.Count - 1];
                    passOneResult[passOneResult.Count - 1] = Meld(lastInserted, current);
                    break;
                }
            }
        }

        var passTwoResult = passOneResult[passOneResult.Count - 1];

        if (passOneResult.Count == 1)
        {
            root = passTwoResult;
            return;
        }


        for (var i = passOneResult.Count - 2; i >= 0; i--)
        {
            current = passOneResult[i];
            passTwoResult = Meld(passTwoResult, current);
        }

        root = passTwoResult;
    }

    /// <summary>
    ///     makes the smaller node parent of other and returns the Parent
    /// </summary>
    private PairingHeapNode<T> Meld(PairingHeapNode<T> node1,
        PairingHeapNode<T> node2)
    {
        if (node2 != null)
        {
            node2.Previous = null;
            node2.Next = null;
        }

        if (node1 == null) return node2;

        node1.Previous = null;
        node1.Next = null;

        if (node2 != null && comparer.Compare(node1.Value, node2.Value) <= 0)
        {
            AddChild(ref node1, node2);
            return node1;
        }

        AddChild(ref node2, node1);
        return node2;
    }

    /// <summary>
    ///     Add new child to parent node
    /// </summary>
    private void AddChild(ref PairingHeapNode<T> parent, PairingHeapNode<T> child)
    {
        if (parent.ChildrenHead == null)
        {
            parent.ChildrenHead = child;
            child.Previous = parent;
            return;
        }

        var head = parent.ChildrenHead;

        child.Previous = head;
        child.Next = head.Next;

        if (head.Next != null) head.Next.Previous = child;

        head.Next = child;
    }

    /// <summary>
    ///     delete node from parent
    /// </summary>
    private void DeleteChild(PairingHeapNode<T> node)
    {
        //if this node is the child head pointer of parent
        if (node.IsHeadChild)
        {
            var parent = node.Previous;

            //use close sibling as new parent child pointer
            if (node.Next != null) node.Next.Previous = parent;

            parent.ChildrenHead = node.Next;
        }
        else
        {
            //just do regular deletion from linked list
            node.Previous.Next = node.Next;

            if (node.Next != null) node.Next.Previous = node.Previous;
        }
    }

    private void AddMapping(T newItem, PairingHeapNode<T> newNode)
    {
        if (heapMapping.ContainsKey(newItem))
            heapMapping[newItem].Add(newNode);
        else
            heapMapping[newItem] = new List<PairingHeapNode<T>>(new[] { newNode });
    }

    private void UpdateNodeValue(T currentValue, T newValue, PairingHeapNode<T> node)
    {
        RemoveMapping(currentValue, node);
        node.Value = newValue;
        AddMapping(newValue, node);
    }

    private void RemoveMapping(T currentValue, PairingHeapNode<T> node)
    {
        heapMapping[currentValue].Remove(node);
        if (heapMapping[currentValue].Count == 0) heapMapping.Remove(currentValue);
    }
}ParseOptions.0.jsonÜ
sD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Heap\Shared\BinomialHeapNode.cs˘using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

internal class BinomialHeapNode<T> : IComparable where T : IComparable
{
    internal BinomialHeapNode(T value)
    {
        Value = value;

        Children = new List<BinomialHeapNode<T>>();
    }

    internal T Value { get; set; }
    internal int Degree => Children.Count;

    internal BinomialHeapNode<T> Parent { get; set; }
    internal List<BinomialHeapNode<T>> Children { get; set; }

    public int CompareTo(object obj)
    {
        return Value.CompareTo(((BinomialHeapNode<T>)obj).Value);
    }
}ParseOptions.0.json§
uD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Heap\Shared\FibornacciHeapNode.csïusing System;

namespace Advanced.Algorithms.DataStructures;

internal class FibonacciHeapNode<T> : IComparable where T : IComparable
{
    internal int Degree;
    internal FibonacciHeapNode<T> Next;

    internal FibonacciHeapNode<T> Previous;

    internal FibonacciHeapNode(T value)
    {
        Value = value;
    }

    internal T Value { get; set; }
    internal FibonacciHeapNode<T> ChildrenHead { get; set; }

    internal FibonacciHeapNode<T> Parent { get; set; }
    internal bool LostChild { get; set; }

    public int CompareTo(object obj)
    {
        return Value.CompareTo(((FibonacciHeapNode<T>)obj).Value);
    }
}ParseOptions.0.jsonÌ
rD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Heap\Shared\PairingHeapNode.cs·using System;

namespace Advanced.Algorithms.DataStructures;

internal class PairingHeapNode<T> : IComparable where T : IComparable
{
    internal PairingHeapNode<T> Next;

    internal PairingHeapNode<T> Previous;

    internal PairingHeapNode(T value)
    {
        Value = value;
    }

    internal T Value { get; set; }

    internal PairingHeapNode<T> ChildrenHead { get; set; }
    internal bool IsHeadChild => Previous != null && Previous.ChildrenHead == this;

    public int CompareTo(object obj)
    {
        return Value.CompareTo(((PairingHeapNode<T>)obj).Value);
    }
}ParseOptions.0.json∞+
tD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\LinkedList\CircularLinkedList.cs¢*using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A circular linked list implementation.
/// </summary>
public class CircularLinkedList<T> : IEnumerable<T>
{
    public CircularLinkedListNode<T> ReferenceNode;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new CircularLinkedListEnumerator<T>(ref ReferenceNode);
    }

    /// <summary>
    ///     Marks this data as the new reference node after insertion.
    ///     Like insert first assuming that current reference node as head.
    ///     Time complexity: O(1).
    /// </summary>
    public CircularLinkedListNode<T> Insert(T data)
    {
        var newNode = new CircularLinkedListNode<T>(data);

        //if no item exist
        if (ReferenceNode == null)
        {
            //attach the item after reference node
            newNode.Next = newNode;
            newNode.Previous = newNode;
        }
        else
        {
            //attach the item after reference node
            newNode.Previous = ReferenceNode;
            newNode.Next = ReferenceNode.Next;

            ReferenceNode.Next.Previous = newNode;
            ReferenceNode.Next = newNode;
        }

        ReferenceNode = newNode;

        return newNode;
    }

    /// <summary>
    ///     Time complexity: O(1)
    /// </summary>
    public void Delete(CircularLinkedListNode<T> current)
    {
        if (ReferenceNode.Next == ReferenceNode)
        {
            if (ReferenceNode != current) throw new Exception("Not found");

            ReferenceNode = null;
            return;
        }

        current.Previous.Next = current.Next;
        current.Next.Previous = current.Previous;

        //match is a reference node
        if (current == ReferenceNode) ReferenceNode = current.Next;
    }

    /// <summary>
    ///     search and delete.
    ///     Time complexity:O(n).
    /// </summary>
    public void Delete(T data)
    {
        if (ReferenceNode == null) throw new Exception("Empty list");

        //only one element on list
        if (ReferenceNode.Next == ReferenceNode)
        {
            if (ReferenceNode.Data.Equals(data))
            {
                ReferenceNode = null;
                return;
            }

            throw new Exception("Not found");
        }

        //atleast two elements from here
        var current = ReferenceNode;
        var found = false;
        while (true)
        {
            if (current.Data.Equals(data))
            {
                current.Previous.Next = current.Next;
                current.Next.Previous = current.Previous;

                //match is a reference node
                if (current == ReferenceNode) ReferenceNode = current.Next;

                found = true;
                break;
            }

            //terminate loop if we are about to cycle
            if (current.Next == ReferenceNode) break;

            current = current.Next;
        }

        if (found == false) throw new Exception("Not found");
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public bool IsEmpty()
    {
        return ReferenceNode == null;
    }

    /// <summary>
    ///     Time complexity:  O(1).
    /// </summary>
    public void Clear()
    {
        if (ReferenceNode == null) throw new Exception("Empty list");

        ReferenceNode = null;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Union(CircularLinkedList<T> newList)
    {
        ReferenceNode.Previous.Next = newList.ReferenceNode;
        ReferenceNode.Previous = newList.ReferenceNode.Previous;

        newList.ReferenceNode.Previous.Next = ReferenceNode;
        newList.ReferenceNode.Previous = ReferenceNode.Previous;
    }
}

/// <summary>
///     Circular linked list node.
/// </summary>
public class CircularLinkedListNode<T>
{
    public T Data;
    public CircularLinkedListNode<T> Next;
    public CircularLinkedListNode<T> Previous;

    public CircularLinkedListNode(T data)
    {
        Data = data;
    }
}

internal class CircularLinkedListEnumerator<T> : IEnumerator<T>
{
    internal CircularLinkedListNode<T> CurrentNode;
    internal CircularLinkedListNode<T> ReferenceNode;

    internal CircularLinkedListEnumerator(ref CircularLinkedListNode<T> referenceNode)
    {
        this.ReferenceNode = referenceNode;
    }

    public bool MoveNext()
    {
        if (ReferenceNode == null)
            return false;

        if (CurrentNode == null)
        {
            CurrentNode = ReferenceNode;
            return true;
        }

        if (CurrentNode.Next != null && CurrentNode.Next != ReferenceNode)
        {
            CurrentNode = CurrentNode.Next;
            return true;
        }

        return false;
    }

    public void Reset()
    {
        CurrentNode = ReferenceNode;
    }


    object IEnumerator.Current => Current;

    public T Current => CurrentNode.Data;

    public void Dispose()
    {
        ReferenceNode = null;
        CurrentNode = null;
    }
}ParseOptions.0.jsonÔF
rD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\LinkedList\DoublyLinkedList.cs„Eusing System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A doubly linked list implementation.
/// </summary>
public class DoublyLinkedList<T> : IEnumerable<T>
{
    public DoublyLinkedListNode<T> Head;
    public DoublyLinkedListNode<T> Tail;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new DoublyLinkedListEnumerator<T>(ref Head);
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    /// <returns>The new node.</returns>
    public DoublyLinkedListNode<T> InsertFirst(T data)
    {
        var newNode = new DoublyLinkedListNode<T>(data);

        if (Head != null) Head.Previous = newNode;

        newNode.Next = Head;
        newNode.Previous = null;

        Head = newNode;

        if (Tail == null) Tail = Head;

        return newNode;
    }

    internal void InsertFirst(DoublyLinkedListNode<T> newNode)
    {
        if (Head != null) Head.Previous = newNode;

        newNode.Next = Head;
        newNode.Previous = null;

        Head = newNode;

        if (Tail == null) Tail = Head;
    }


    /// <summary>
    ///     Insert right after this node.
    ///     Time complexity: O(1).
    /// </summary>
    public DoublyLinkedListNode<T> InsertAfter(DoublyLinkedListNode<T> node, DoublyLinkedListNode<T> data)
    {
        if (node == null)
            throw new Exception("Empty reference node");

        if (node == Head && node == Tail)
        {
            node.Next = data;
            node.Previous = null;

            data.Previous = node;
            data.Next = null;

            Head = node;
            Tail = data;

            return data;
        }

        if (node != Tail)
        {
            data.Previous = node;
            data.Next = node.Next;

            node.Next.Previous = data;
            node.Next = data;
        }
        else
        {
            data.Previous = node;
            data.Next = null;

            node.Next = data;
            Tail = data;
        }

        return data;
    }

    /// <summary>
    ///     Insert right before this node.
    ///     Time complexity:O(1).
    /// </summary>
    public DoublyLinkedListNode<T> InsertBefore(DoublyLinkedListNode<T> node, DoublyLinkedListNode<T> data)
    {
        if (node == null)
            throw new Exception("Empty node");

        if (node == Head && node == Tail)
        {
            node.Previous = data;
            node.Next = null;
            Tail = node;

            data.Previous = null;
            data.Next = node;

            Head = data;

            return data;
        }

        if (node == Head)
        {
            data.Previous = null;
            data.Next = node;

            node.Previous = data;
            Head = data;
        }
        else
        {
            data.Previous = node.Previous;
            data.Next = node;

            node.Previous.Next = data;
            node.Previous = data;
        }

        return data;
    }

    /// <summary>
    ///     Time complexity:O(1).
    /// </summary>
    public DoublyLinkedListNode<T> InsertLast(T data)
    {
        if (Tail == null) return InsertFirst(data);

        var newNode = new DoublyLinkedListNode<T>(data);

        Tail.Next = newNode;

        newNode.Previous = Tail;
        newNode.Next = null;

        Tail = newNode;

        return newNode;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T DeleteFirst()
    {
        if (Head == null) throw new Exception("Empty list");

        var headData = Head.Data;

        if (Head == Tail)
        {
            Head = null;
            Tail = null;
        }
        else
        {
            Head.Next.Previous = null;
            Head = Head.Next;
        }

        return headData;
    }

    /// <summary>
    ///     Delete tail node.
    ///     Time complexity: O(1)
    /// </summary>
    public T DeleteLast()
    {
        if (Tail == null) throw new Exception("Empty list");

        var tailData = Tail.Data;

        if (Tail == Head)
        {
            Head = null;
            Tail = null;
        }
        else
        {
            Tail.Previous.Next = null;
            Tail = Tail.Previous;
        }

        return tailData;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public void Delete(T data)
    {
        if (Head == null) throw new Exception("Empty list");

        //eliminate single element list possibility
        if (Head == Tail)
        {
            if (Head.Data.Equals(data)) DeleteFirst();

            return;
        }

        //from here logic assumes atleast two elements in list
        var current = Head;

        while (current != null)
        {
            if (current.Data.Equals(data))
            {
                //current is the first element
                if (current.Previous == null)
                {
                    current.Next.Previous = null;
                    Head = current.Next;
                }
                //current is the last element
                else if (current.Next == null)
                {
                    current.Previous.Next = null;
                    Tail = current.Previous;
                }
                //current is somewhere in the middle
                else
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                }

                break;
            }

            current = current.Next;
        }
    }

    /// <summary>
    ///     Delete the given node.
    ///     Time complexity: O(n).
    /// </summary>
    public void Delete(DoublyLinkedListNode<T> node)
    {
        if (Head == null) throw new Exception("Empty list");

        //only one element
        if (node == Head && node == Tail)
        {
            Head = null;
            Tail = null;
            return;
        }

        //node is head
        if (node == Head)
        {
            node.Next.Previous = null;
            Head = node.Next;
        }
        //node is tail
        else if (node == Tail)
        {
            node.Previous.Next = null;
            Tail = node.Previous;
        }
        //current is somewhere in the middle
        else
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
        }
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    internal void Union(DoublyLinkedList<T> newList)
    {
        if (Head == null)
        {
            Head = newList.Head;
            Tail = newList.Tail;
            return;
        }

        if (newList.Head == null)
            return;

        Head.Previous = newList.Tail;
        newList.Tail.Next = Head;

        Head = newList.Head;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public bool IsEmpty()
    {
        return Head == null;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Clear()
    {
        if (Head == null) throw new Exception("Empty list");

        Head = null;
        Tail = null;
    }
}

/// <summary>
///     Doubly linked list node.
/// </summary>
public class DoublyLinkedListNode<T>
{
    public T Data;
    public DoublyLinkedListNode<T> Next;
    public DoublyLinkedListNode<T> Previous;

    public DoublyLinkedListNode(T data)
    {
        Data = data;
    }
}

internal class DoublyLinkedListEnumerator<T> : IEnumerator<T>
{
    internal DoublyLinkedListNode<T> CurrentNode;
    internal DoublyLinkedListNode<T> HeadNode;

    internal DoublyLinkedListEnumerator(ref DoublyLinkedListNode<T> headNode)
    {
        this.HeadNode = headNode;
    }

    public bool MoveNext()
    {
        if (HeadNode == null)
            return false;

        if (CurrentNode == null)
        {
            CurrentNode = HeadNode;
            return true;
        }

        if (CurrentNode.Next != null)
        {
            CurrentNode = CurrentNode.Next;
            return true;
        }

        return false;
    }

    public void Reset()
    {
        CurrentNode = HeadNode;
    }


    object IEnumerator.Current => Current;

    public T Current => CurrentNode.Data;

    public void Dispose()
    {
        HeadNode = null;
        CurrentNode = null;
    }
}ParseOptions.0.json±'
rD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\LinkedList\SinglyLinkedList.cs•&using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A singly linked list implementation.
/// </summary>
public class SinglyLinkedList<T> : IEnumerable<T>
{
    public SinglyLinkedListNode<T> Head;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new SinglyLinkedListEnumerator<T>(ref Head);
    }

    /// <summary>
    ///     Insert first. Time complexity: O(1).
    /// </summary>
    public void InsertFirst(T data)
    {
        var newNode = new SinglyLinkedListNode<T>(data);

        newNode.Next = Head;

        Head = newNode;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public void InsertLast(T data)
    {
        var newNode = new SinglyLinkedListNode<T>(data);

        if (Head == null)
        {
            Head = new SinglyLinkedListNode<T>(data);
        }
        else
        {
            var current = Head;

            while (current.Next != null) current = current.Next;

            current.Next = newNode;
        }
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T DeleteFirst()
    {
        if (Head == null) throw new Exception("Nothing to remove");

        var firstData = Head.Data;

        Head = Head.Next;

        return firstData;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public T DeleteLast()
    {
        if (Head == null) throw new Exception("Nothing to remove");

        var current = Head;
        SinglyLinkedListNode<T> prev = null;
        while (current.Next != null)
        {
            prev = current;
            current = current.Next;
        }

        var lastData = prev.Next.Data;
        prev.Next = null;
        return lastData;
    }

    /// <summary>
    ///     Delete given element.
    ///     Time complexity: O(n)
    /// </summary>
    public void Delete(T element)
    {
        if (Head == null) throw new Exception("Empty list");

        var current = Head;
        SinglyLinkedListNode<T> prev = null;

        do
        {
            if (current.Data.Equals(element))
            {
                //last element
                if (current.Next == null)
                {
                    //head is the only node
                    if (prev == null)
                        Head = null;
                    else
                        //last element
                        prev.Next = null;
                }
                else
                {
                    //current is head
                    if (prev == null)
                        Head = current.Next;
                    else
                        //delete
                        prev.Next = current.Next;
                }

                break;
            }

            prev = current;
            current = current.Next;
        } while (current != null);
    }

    // Time complexity: O(1).
    public bool IsEmpty()
    {
        return Head == null;
    }

    // Time complexity: O(1).
    public void Clear()
    {
        if (Head == null) throw new Exception("Empty list");

        Head = null;
    }

    /// <summary>
    ///     Inserts this element to the begining.
    ///     Time complexity: O(1).
    /// </summary>
    public void InsertFirst(SinglyLinkedListNode<T> current)
    {
        current.Next = Head;
        Head = current;
    }
}

/// <summary>
///     Singly linked list node.
/// </summary>
public class SinglyLinkedListNode<T>
{
    public T Data;
    public SinglyLinkedListNode<T> Next;

    public SinglyLinkedListNode(T data)
    {
        Data = data;
    }
}

internal class SinglyLinkedListEnumerator<T> : IEnumerator<T>
{
    internal SinglyLinkedListNode<T> CurrentNode;
    internal SinglyLinkedListNode<T> HeadNode;

    internal SinglyLinkedListEnumerator(ref SinglyLinkedListNode<T> headNode)
    {
        this.HeadNode = headNode;
    }

    public bool MoveNext()
    {
        if (HeadNode == null)
            return false;

        if (CurrentNode == null)
        {
            CurrentNode = HeadNode;
            return true;
        }

        if (CurrentNode.Next != null)
        {
            CurrentNode = CurrentNode.Next;
            return true;
        }

        return false;
    }

    public void Reset()
    {
        CurrentNode = HeadNode;
    }


    object IEnumerator.Current => Current;

    public T Current => CurrentNode.Data;

    public void Dispose()
    {
        HeadNode = null;
        CurrentNode = null;
    }
}ParseOptions.0.json∏$
eD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\List\ArrayList.csπ#using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Foundation;

/// <summary>
///     A self expanding array implementation.
/// </summary>
/// <typeparam name="T">The datatype of this ArrayList.</typeparam>
public class ArrayList<T> : IEnumerable<T>
{
    private readonly int initialArraySize;
    private T[] array;
    private int arraySize;

    /// <summary>
    ///     Time complexity: O(1) if initial is empty otherwise O(n).
    /// </summary>
    /// <param name="initalArraySize">The initial array size.</param>
    /// <param name="initial">Initial values if any.</param>
    public ArrayList(int initalArraySize = 2, IEnumerable<T> initial = null)
    {
        if (initalArraySize < 2) throw new Exception("Initial array size must be greater than 1");

        initialArraySize = initalArraySize;
        arraySize = initalArraySize;
        array = new T[arraySize];

        if (initial == null) return;

        foreach (var item in initial) Add(item);
    }

    /// <summary>
    ///     Time complexity: O(1) if initial is empty otherwise O(n).
    /// </summary>
    /// <param name="initial">Initial values if any.</param>
    public ArrayList(IEnumerable<T> initial)
        : this(2, initial)
    {
    }

    public int Length { get; private set; }

    /// <summary>
    ///     Indexed access to array.
    ///     Time complexity: O(1).
    /// </summary>
    /// <param name="index">The index to write or read.</param>
    public T this[int index]
    {
        get => ItemAt(index);
        set => SetItem(index, value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return array.Take(Length).GetEnumerator();
    }

    private T ItemAt(int i)
    {
        if (i >= Length)
            throw new Exception("Index exeeds array size");

        return array[i];
    }

    /// <summary>
    ///     Add a new item to this array list.
    ///     Time complexity: O(1) amortized.
    /// </summary>
    public void Add(T item)
    {
        Grow();

        array[Length] = item;
        Length++;
    }

    /// <summary>
    ///     Insert given item at specified index.
    ///     Time complexity: O(1) amortized.
    /// </summary>
    /// <param name="index">
    ///     The index to insert at.
    ///     <param>
    ///         <param name="item">The item to insert.</param>
    public void InsertAt(int index, T item)
    {
        Grow();

        Shift(index);

        array[index] = item;
        Length++;
    }

    /// <summary>
    ///     Shift the position of elements right by one starting at this index.
    ///     Creates a blank field at index.
    /// </summary>
    private void Shift(int index)
    {
        Array.Copy(array, index, array, index + 1, Length - index);
    }

    /// <summary>
    ///     Clears the array.
    ///     Time complexity: O(1).
    /// </summary>
    public void Clear()
    {
        arraySize = initialArraySize;
        array = new T[arraySize];
        Length = 0;
    }

    private void SetItem(int i, T item)
    {
        if (i >= Length)
            throw new Exception("Index exeeds array size");

        array[i] = item;
    }

    /// <summary>
    ///     Remove the item at given index.
    ///     Time complexity: O(1) amortized.
    /// </summary>
    /// <param name="i">The index to remove at.</param>
    public void RemoveAt(int i)
    {
        if (i >= Length)
            throw new Exception("Index exeeds array size");

        //shift elements
        for (var j = i; j < arraySize - 1; j++) array[j] = array[j + 1];

        Length--;

        Shrink();
    }

    private void Grow()
    {
        if (Length != arraySize) return;

        //increase array size exponentially on demand
        arraySize *= 2;

        var biggerArray = new T[arraySize];
        Array.Copy(array, 0, biggerArray, 0, Length);
        array = biggerArray;
    }

    private void Shrink()
    {
        if (Length != arraySize / 2 || arraySize == initialArraySize) return;

        //reduce array by half 
        arraySize /= 2;

        var smallerArray = new T[arraySize];
        Array.Copy(array, 0, smallerArray, 0, Length);
        array = smallerArray;
    }
}ParseOptions.0.jsonÔ4
dD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\List\SkipList.csÒ3using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A skip list implementation with IEnumerable support.
/// </summary>
/// <typeparam name="T">The data type of thi skip list.</typeparam>
public class SkipList<T> : IEnumerable<T> where T : IComparable
{
    private readonly Random coinFlipper = new();

    /// <summary>
    ///     The maximum height of this skip list with which it was initialized.
    /// </summary>
    public readonly int MaxHeight;

    /// <param name="maxHeight">The maximum height.</param>
    public SkipList(int maxHeight = 32)
    {
        MaxHeight = maxHeight;
        Head = new SkipListNode<T>
        {
            Prev = null,
            Next = new SkipListNode<T>[maxHeight],
            Value = default
        };
    }

    internal SkipListNode<T> Head { get; set; }

    /// <summary>
    ///     The number of elements in this skip list.
    /// </summary>
    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new SkipListEnumerator<T>(Head);
    }

    /// <summary>
    ///     Finds the given value in this skip list.
    ///     If item is not found default value of T will be returned.
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Find(T value)
    {
        var current = Head;
        //for each level of linked list from Top
        for (var i = MaxHeight - 1; i >= 0; i--)
            //iterate through the list until match
            //or with an itemless that match
            while (true)
            {
                if (current.Next[i] != null
                    && current.Next[i].Value.CompareTo(value) == 0)
                    return current.Next[i].Value;

                if (current.Next[i] == null
                    || current.Next[i].Value.CompareTo(value) > 0)
                    break;

                //update current
                //so that in next level we can start search from here
                current = current.Next[i];
            }

        return default;
    }

    /// <summary>
    ///     Inserts the given value to this skip list.
    ///     Will throw exception if the value already exists.
    ///     Time complexity: O(log(n))
    /// </summary>
    /// <param name="value">The value to insert.</param>
    public void Insert(T value)
    {
        if (!Find(value).Equals(default(T))) throw new Exception("Cannot insert duplicate values.");

        //find the random level up to which we link the new node
        var level = 0;
        for (var i = 0;
             i < MaxHeight
             && coinFlipper.Next(0, 2) == 1;
             i++)
            level++;

        var newNode = new SkipListNode<T>
        {
            Value = value,
            //only level + 1 number of links (level is zero index based)
            Next = new SkipListNode<T>[level + 1]
        };

        //init current to head before insertion
        var current = Head;

        //go down from top level
        for (var i = MaxHeight - 1; i >= 0; i--)
        {
            //move on current level of linked list
            //until next element is less than new value to insert
            while (true)
            {
                if (current.Next[i] == null
                    || current.Next[i].Value.CompareTo(value) > 0)
                    break;

                current = current.Next[i];
            }

            //if this level is greater than 
            //maximum levels of link for new node
            //then jump to next level
            if (i > level) continue;

            //insert and update pointers
            newNode.Next[i] = current.Next[i];
            current.Next[i] = newNode;

            //for base level set previous node
            if (i == 0) newNode.Prev = current;
        }

        Count++;
    }

    /// <summary>
    ///     Deletes the given value from this skip list.
    ///     Will throw exception if the value does'nt exist in this skip list.
    ///     Time complexity: O(log(n))
    /// </summary>
    /// <param name="value"> The value to delete.</param>
    public void Delete(T value)
    {
        //init current to head before insertion
        var current = Head;

        //go down from top level
        for (var i = MaxHeight - 1; i >= 0; i--)
        {
            //move on current level of linked list
            //until next element is less than new value to delete
            while (true)
            {
                if (current.Next[i] == null
                    || current.Next[i].Value.CompareTo(value) >= 0)
                    break;

                current = current.Next[i];
            }

            //item not found
            if (i == 0 && current.Next[i].Value.CompareTo(value) != 0)
                throw new Exception("Item to delete was not found in this skip list.");

            if (current.Next[i] != null
                && current.Next[i].Value.CompareTo(value) == 0)
            {
                //for base level set previous node
                if (i == 0 && current.Next[i].Next[i] != null) current.Next[i].Next[i].Prev = current;

                //insert and update pointers
                current.Next[i] = current.Next[i].Next[i];
            }
        }

        Count--;
    }
}

internal class SkipListNode<T> where T : IComparable
{
    internal SkipListNode<T> Prev { get; set; }
    internal SkipListNode<T>[] Next { get; set; }

    internal T Value { get; set; }
}

internal class SkipListEnumerator<T> : IEnumerator<T> where T : IComparable
{
    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    private SkipListNode<T> current;
    private SkipListNode<T> head;

    internal SkipListEnumerator(SkipListNode<T> head)
    {
        this.head = head;
        current = head;
    }

    public bool MoveNext()
    {
        if (current.Next[0] != null)
        {
            current = current.Next[0];
            return true;
        }

        return false;
    }

    public void Reset()
    {
        current = head;
    }

    object IEnumerator.Current => Current;

    public T Current => current.Value;

    public void Dispose()
    {
        head = null;
        current = null;
    }
}ParseOptions.0.json†
hD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Queues\ArrayQueue.csûusing System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class ArrayQueue<T> : IQueue<T>
{
    private readonly List<T> list = new();

    public int Count { get; private set; }

    public void Enqueue(T item)
    {
        list.Insert(0, item);
        Count++;
    }

    public T Dequeue()
    {
        if (list.Count == 0) throw new Exception("Empty Queue");

        var result = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        Count--;
        return result;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return list.GetEnumerator();
    }
}ParseOptions.0.jsonè
mD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Queues\LinkedListQueue.csàusing System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class LinkedListQueue<T> : IQueue<T>
{
    private readonly DoublyLinkedList<T> list = new();

    public int Count { get; private set; }

    public void Enqueue(T item)
    {
        list.InsertFirst(item);
        Count++;
    }

    public T Dequeue()
    {
        if (list.Head == null) throw new Exception("Empty Queue");

        var result = list.DeleteLast();
        Count--;
        return result;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return list.GetEnumerator();
    }
}ParseOptions.0.json”	
kD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Queues\PriorityQueue.csŒusing System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A priority queue implementation using heap
/// </summary>
public class PriorityQueue<T> : IEnumerable<T> where T : IComparable
{
    private readonly BHeap<T> heap;

    public PriorityQueue(SortDirection sortDirection = SortDirection.Ascending)
    {
        heap = new BHeap<T>(sortDirection);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return heap.GetEnumerator();
    }

    /// <summary>
    ///     Time complexity:O(log(n)).
    /// </summary>
    public void Enqueue(T item)
    {
        heap.Insert(item);
    }

    /// <summary>
    ///     Time complexity:O(log(n)).
    /// </summary>
    public T Dequeue()
    {
        return heap.Extract();
    }

    /// <summary>
    ///     Time complexity:O(1).
    /// </summary>
    public T Peek()
    {
        return heap.Peek();
    }
}ParseOptions.0.jsonë
cD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Queues\Queue.csîusing System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

/// <summary>
///     A queue implementation.
/// </summary>
public class Queue<T> : IEnumerable<T>
{
    private readonly IQueue<T> queue;

    /// <param name="type">The queue implementation type.</param>
    public Queue(QueueType type = QueueType.Array)
    {
        if (type == QueueType.Array)
            queue = new ArrayQueue<T>();
        else
            queue = new LinkedListQueue<T>();
    }

    /// <summary>
    ///     The number of items in the queue.
    /// </summary>
    public int Count => queue.Count;

    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return queue.GetEnumerator();
    }

    /// <summary>
    ///     Time complexity:O(1).
    /// </summary>
    public void Enqueue(T item)
    {
        queue.Enqueue(item);
    }

    /// <summary>
    ///     Time complexity:O(1).
    /// </summary>
    public T Dequeue()
    {
        return queue.Dequeue();
    }
}

internal interface IQueue<T> : IEnumerable<T>
{
    int Count { get; }
    void Enqueue(T item);
    T Dequeue();
}

/// <summary>
///     The queue implementation types.
/// </summary>
public enum QueueType
{
    Array = 0,
    LinkedList = 1
}ParseOptions.0.json˙
fD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Set\BloomFilter.cs˙using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A simple bloom filter implementation.
/// </summary>
public class BloomFilter<T>
{
    private readonly BitArray filter;

    private readonly int numberOfHashFunctions;

    /// <summary>
    ///     Higher the size lower the collision and
    ///     failure probablity.
    /// </summary>
    public BloomFilter(int size, int numberOfHashFunctions = 2)
    {
        if (size <= numberOfHashFunctions)
            throw new ArgumentException("size cannot be less than or equal to numberOfHashFunctions.");

        this.numberOfHashFunctions = numberOfHashFunctions;
        filter = new BitArray(size);
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void AddKey(T key)
    {
        foreach (var hash in GetHashes(key)) filter[hash % filter.Length] = true;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public bool KeyExists(T key)
    {
        foreach (var hash in GetHashes(key))
            if (filter[hash % filter.Length] == false)
                return false;

        return true;
    }

    private IEnumerable<int> GetHashes(T key)
    {
        for (var i = 1; i <= numberOfHashFunctions; i++)
        {
            var obj = new { Key = key, InitialValue = i };
            yield return Math.Abs(obj.GetHashCode());
        }
    }
}ParseOptions.0.json∏
fD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Set\DisJointSet.cs∏using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A disjoint set implementation.
/// </summary>
public class DisJointSet<T> : IEnumerable<T>
{
    /// <summary>
    ///     A Map for faster access for members.
    /// </summary>
    private readonly Dictionary<T, DisJointSetNode<T>> set = new();

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return set.Values.Select(x => x.Data).GetEnumerator();
    }

    /// <summary>
    ///     Creates a new set with given member.
    ///     Time complexity: log(n).
    /// </summary>
    public void MakeSet(T member)
    {
        if (set.ContainsKey(member)) throw new Exception("A set with given member already exists.");

        var newSet = new DisJointSetNode<T>
        {
            Data = member,
            Rank = 0
        };

        //Root's Parent is Root itself
        newSet.Parent = newSet;
        set.Add(member, newSet);

        Count++;
    }


    /// <summary>
    ///     Returns the reference member of the set where this member is part of.
    ///     Time complexity: log(n).
    /// </summary>
    public T FindSet(T member)
    {
        if (!set.ContainsKey(member)) throw new Exception("No such set with given member.");

        return FindSet(set[member]).Data;
    }

    /// <summary>
    ///     Recursively move up in the set tree till root
    ///     and return the Root.
    ///     Does path Compression on all visited members on way to root
    ///     by pointing their parent to Root.
    /// </summary>
    private DisJointSetNode<T> FindSet(DisJointSetNode<T> node)
    {
        var parent = node.Parent;

        if (node != parent)
        {
            //compress path by setting parent to Root
            node.Parent = FindSet(node.Parent);
            return node.Parent;
        }

        //reached root so return the Root (reference Member)
        return parent;
    }

    /// <summary>
    ///     Union's given member's sets if given members are in differant sets.
    ///     Otherwise does nothing.
    ///     Time complexity: log(n).
    /// </summary>
    public void Union(T memberA, T memberB)
    {
        var rootA = FindSet(memberA);
        var rootB = FindSet(memberB);

        if (rootA.Equals(rootB)) return;

        var nodeA = set[rootA];
        var nodeB = set[rootB];

        //equal rank so just pick any of two as Root
        //and increment rank
        if (nodeA.Rank == nodeB.Rank)
        {
            nodeB.Parent = nodeA;
            nodeA.Rank++;
        }
        else
        {
            //pick max Rank node as root
            if (nodeA.Rank < nodeB.Rank)
                nodeA.Parent = nodeB;
            else
                nodeB.Parent = nodeA;
        }
    }
}

internal class DisJointSetNode<T>
{
    internal T Data { get; set; }
    internal int Rank { get; set; }

    internal DisJointSetNode<T> Parent { get; set; }
}ParseOptions.0.jsoné
dD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Set\SparseSet.csêusing System;
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
        if (value < 0) throw new Exception("Negative values not supported.");

        if (value >= sparse.Length) throw new Exception("Item is greater than max value.");

        if (Count >= dense.Length) throw new Exception("Set reached its capacity.");

        sparse[value] = Count;
        dense[Count] = value;
        Count++;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Remove(int value)
    {
        if (value < 0) throw new Exception("Negative values not supported.");

        if (value >= sparse.Length) throw new Exception("Item is greater than max value.");

        if (HasItem(value) == false) throw new Exception("Item do not exist.");

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
        var index = sparse[value];
        return index != -1 && dense[index] == value;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Clear()
    {
        Count = 0;
    }
}ParseOptions.0.json¢
sD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Shared\IEnumerableExtensions.csïusing System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

internal static class EnumerableExtensions
{
    internal static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> e)
    {
        while (e.MoveNext()) yield return e.Current;
    }
}ParseOptions.0.jsonÅ
gD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Stack\ArrayStack.csÄusing System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class ArrayStack<T> : IStack<T>
{
    private readonly List<T> list = new();
    public int Count { get; private set; }

    public T Pop()
    {
        if (Count == 0) throw new Exception("Empty stack");

        var result = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        Count--;
        return result;
    }

    public void Push(T item)
    {
        list.Add(item);
        Count++;
    }

    public T Peek()
    {
        if (Count == 0) return default;

        return list[list.Count - 1];
    }

    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return list.GetEnumerator();
    }
}ParseOptions.0.json€
lD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Stack\LinkedListStack.cs’using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class LinkedListStack<T> : IStack<T>
{
    private readonly SinglyLinkedList<T> list = new();
    public int Count { get; private set; }

    public T Pop()
    {
        if (Count == 0) throw new Exception("Empty stack");

        var result = list.DeleteFirst();
        Count--;
        return result;
    }

    public void Push(T item)
    {
        list.InsertFirst(item);
        Count++;
    }

    public T Peek()
    {
        return Count == 0 ? default : list.Head.Data;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return list.GetEnumerator();
    }
}ParseOptions.0.json∆
bD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Stack\Stack.cs using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

/// <summary>
///     A stack implementation.
/// </summary>
public class Stack<T> : IEnumerable<T>
{
    private readonly IStack<T> stack;

    /// <param name="type">The stack type to use.</param>
    public Stack(StackType type = StackType.Array)
    {
        if (type == StackType.Array)
            stack = new ArrayStack<T>();
        else
            stack = new LinkedListStack<T>();
    }

    /// <summary>
    ///     The total number of items in this stack.
    /// </summary>
    public int Count => stack.Count;

    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return stack.GetEnumerator();
    }

    /// <summary>
    ///     Time complexity:O(1).
    /// </summary>
    /// <returns>The item popped.</returns>
    public T Pop()
    {
        return stack.Pop();
    }

    /// <summary>
    ///     Time complexity:O(1).
    /// </summary>
    /// <param name="item">The item to push.</param>
    public void Push(T item)
    {
        stack.Push(item);
    }

    /// <summary>
    ///     Peek from stack.
    ///     Time complexity:O(1).
    /// </summary>
    /// <returns>The item peeked.</returns>
    public T Peek()
    {
        return stack.Peek();
    }
}

internal interface IStack<T> : IEnumerable<T>
{
    int Count { get; }
    T Pop();
    void Push(T item);

    T Peek();
}

/// <summary>
///     The stack implementation types.
/// </summary>
public enum StackType
{
    Array = 0,
    LinkedList = 1
}ParseOptions.0.jsonæâ
cD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\AvlTree.cs¿àusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     An AVL tree implementation.
/// </summary>
public class AvlTree<T> : IEnumerable<T> where T : IComparable
{
    private readonly Dictionary<T, BstNodeBase<T>> nodeLookUp;

    /// <param name="enableNodeLookUp">
    ///     Enabling lookup will fasten deletion/insertion/exists operations
    ///     at the cost of additional space.
    /// </param>
    public AvlTree(bool enableNodeLookUp = false)
    {
        if (enableNodeLookUp) nodeLookUp = new Dictionary<T, BstNodeBase<T>>();
    }

    /// <summary>
    ///     Initialize the BST with given sorted keys.
    ///     Time complexity: O(n).
    /// </summary>
    /// <param name="sortedCollection">The initial sorted collection.</param>
    /// <param name="enableNodeLookUp">
    ///     Enabling lookup will fasten deletion/insertion/exists operations
    ///     at the cost of additional space.
    /// </param>
    public AvlTree(IEnumerable<T> sortedCollection, bool enableNodeLookUp = false)
    {
        BstHelpers.ValidateSortedCollection(sortedCollection);
        var nodes = sortedCollection.Select(x => new AvlTreeNode<T>(null, x)).ToArray();
        Root = (AvlTreeNode<T>)BstHelpers.ToBst(nodes);
        RecomputeHeight(Root);
        BstHelpers.AssignCount(Root);

        if (enableNodeLookUp) nodeLookUp = nodes.ToDictionary(x => x.Value, x => x as BstNodeBase<T>);
    }

    internal AvlTreeNode<T> Root { get; set; }

    public int Count => Root == null ? 0 : Root.Count;

    //Implementation for the GetEnumerator method.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BstEnumerator<T>(Root);
    }


    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public bool HasItem(T value)
    {
        if (Root == null) return false;

        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    internal int GetHeight()
    {
        if (Root == null)
            return -1;

        return Root.Height;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public void Insert(T value)
    {
        if (Root == null)
        {
            Root = new AvlTreeNode<T>(null, value);
            if (nodeLookUp != null) nodeLookUp[value] = Root;

            return;
        }

        Insert(Root, value);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    private void Insert(AvlTreeNode<T> node, T value)
    {
        var compareResult = node.Value.CompareTo(value);

        //node is less than the value so move right for insertion
        if (compareResult < 0)
        {
            if (node.Right == null)
            {
                node.Right = new AvlTreeNode<T>(node, value);
                if (nodeLookUp != null) nodeLookUp[value] = node.Right;
            }
            else
            {
                Insert(node.Right, value);
            }
        }
        //node is greater than the value so move left for insertion
        else if (compareResult > 0)
        {
            if (node.Left == null)
            {
                node.Left = new AvlTreeNode<T>(node, value);
                if (nodeLookUp != null) nodeLookUp[value] = node.Left;
            }
            else
            {
                Insert(node.Left, value);
            }
        }
        else
        {
            throw new Exception("Item exists");
        }

        UpdateHeight(node);
        Balance(node);

        node.UpdateCounts();
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public int IndexOf(T item)
    {
        return Root.Position(item);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T ElementAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentNullException("index");

        return Root.KthSmallest(index).Value;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Delete(T value)
    {
        if (Root == null) throw new Exception("Empty AVLTree");

        Delete(Root, value);

        if (nodeLookUp != null) nodeLookUp.Remove(value);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentException("index");

        var nodeToDelete = Root.KthSmallest(index) as AvlTreeNode<T>;
        var nodeToBalance = Delete(nodeToDelete, nodeToDelete.Value);

        while (nodeToBalance != null)
        {
            nodeToBalance.UpdateCounts();
            UpdateHeight(nodeToBalance);
            Balance(nodeToBalance);

            nodeToBalance = nodeToBalance.Parent;
        }

        if (nodeLookUp != null) nodeLookUp.Remove(nodeToDelete.Value);

        return nodeToDelete.Value;
    }

    private AvlTreeNode<T> Delete(AvlTreeNode<T> node, T value)
    {
        var baseCase = false;

        var compareResult = node.Value.CompareTo(value);

        //node is less than the search value so move right to find the deletion node
        if (compareResult < 0)
        {
            if (node.Right == null) throw new Exception("Item do not exist");

            Delete(node.Right, value);
        }
        //node is less than the search value so move left to find the deletion node
        else if (compareResult > 0)
        {
            if (node.Left == null) throw new Exception("Item do not exist");

            Delete(node.Left, value);
        }
        else
        {
            //node is a leaf node
            if (node.IsLeaf)
            {
                //if node is root
                if (node.Parent == null)
                    Root = null;
                //assign nodes parent.left/right to null
                else if (node.Parent.Left == node)
                    node.Parent.Left = null;
                else
                    node.Parent.Right = null;

                baseCase = true;
            }
            else
            {
                //case one - right tree is null (move sub tree up)
                if (node.Left != null && node.Right == null)
                {
                    //root
                    if (node.Parent == null)
                    {
                        Root.Left.Parent = null;
                        Root = Root.Left;
                    }
                    else
                    {
                        //node is left child of parent
                        if (node.Parent.Left == node)
                            node.Parent.Left = node.Left;
                        //node is right child of parent
                        else
                            node.Parent.Right = node.Left;

                        node.Left.Parent = node.Parent;
                    }

                    baseCase = true;
                }
                //case two - left tree is null  (move sub tree up)
                else if (node.Right != null && node.Left == null)
                {
                    //root
                    if (node.Parent == null)
                    {
                        Root.Right.Parent = null;
                        Root = Root.Right;
                    }
                    else
                    {
                        //node is left child of parent
                        if (node.Parent.Left == node)
                            node.Parent.Left = node.Right;
                        //node is right child of parent
                        else
                            node.Parent.Right = node.Right;

                        node.Right.Parent = node.Parent;
                    }

                    baseCase = true;
                }
                //case three - two child trees 
                //replace the node value with maximum element of left subtree (left max node)
                //and then delete the left max node
                else
                {
                    var maxLeftNode = FindMax(node.Left);

                    node.Value = maxLeftNode.Value;

                    if (nodeLookUp != null) nodeLookUp[node.Value] = node;

                    //delete left max node
                    Delete(node.Left, maxLeftNode.Value);
                }
            }
        }

        if (baseCase)
        {
            node.Parent.UpdateCounts();
            UpdateHeight(node.Parent);
            Balance(node.Parent);
            return node.Parent;
        }

        node.UpdateCounts();
        UpdateHeight(node);
        Balance(node);
        return node;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T FindMax()
    {
        return FindMax(Root).Value;
    }

    private AvlTreeNode<T> FindMax(AvlTreeNode<T> node)
    {
        while (true)
        {
            if (node.Right == null) return node;

            node = node.Right;
        }
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T FindMin()
    {
        return FindMin(Root).Value;
    }

    private AvlTreeNode<T> FindMin(AvlTreeNode<T> node)
    {
        while (true)
        {
            if (node.Left == null) return node;

            node = node.Left;
        }
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public bool Contains(T value)
    {
        if (Root == null) return false;

        return Find(Root, value) != null;
    }


    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    private AvlTreeNode<T> Find(T value)
    {
        if (nodeLookUp != null) return nodeLookUp[value] as AvlTreeNode<T>;

        return Root.Find(value).Item1 as AvlTreeNode<T>;
    }

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    private AvlTreeNode<T> Find(AvlTreeNode<T> parent, T value)
    {
        if (parent == null) return null;

        if (parent.Value.CompareTo(value) == 0) return parent;

        var left = Find(parent.Left, value);

        if (left != null) return left;

        var right = Find(parent.Right, value);

        return right;
    }

    private void Balance(AvlTreeNode<T> node)
    {
        if (node == null)
            return;

        if (node.Left == null && node.Right == null)
            return;

        var leftHeight = node.Left?.Height + 1 ?? 0;
        var rightHeight = node.Right?.Height + 1 ?? 0;

        var balanceFactor = leftHeight - rightHeight;
        //tree is left heavy
        //differance >=2 then do rotations
        if (balanceFactor >= 2)
        {
            leftHeight = node.Left?.Left?.Height + 1 ?? 0;
            rightHeight = node.Left?.Right?.Height + 1 ?? 0;

            //left child is left heavy
            if (leftHeight > rightHeight)
            {
                RightRotate(node);
            }
            //left child is right heavy
            else
            {
                LeftRotate(node.Left);
                RightRotate(node);
            }
        }
        //tree is right heavy
        //differance <=-2 then do rotations
        else if (balanceFactor <= -2)
        {
            leftHeight = node.Right?.Left?.Height + 1 ?? 0;
            rightHeight = node.Right?.Right?.Height + 1 ?? 0;

            //right child is right heavy
            if (rightHeight > leftHeight)
            {
                LeftRotate(node);
            }
            //right child is left heavy
            else
            {
                RightRotate(node.Right);
                LeftRotate(node);
            }
        }
    }

    private void RightRotate(AvlTreeNode<T> node)
    {
        var prevRoot = node;
        var leftRightChild = prevRoot.Left.Right;

        var newRoot = node.Left;

        //make left child as root
        prevRoot.Left.Parent = prevRoot.Parent;

        if (prevRoot.Parent != null)
        {
            if (prevRoot.Parent.Left == prevRoot)
                prevRoot.Parent.Left = prevRoot.Left;
            else
                prevRoot.Parent.Right = prevRoot.Left;
        }

        //move prev root as right child of current root
        newRoot.Right = prevRoot;
        prevRoot.Parent = newRoot;

        //move right child of left child of prev root to left child of right child of new root
        newRoot.Right.Left = leftRightChild;
        if (newRoot.Right.Left != null) newRoot.Right.Left.Parent = newRoot.Right;

        UpdateHeight(newRoot);

        newRoot.Left.UpdateCounts();
        newRoot.Right.UpdateCounts();
        newRoot.UpdateCounts();

        if (prevRoot == Root) Root = newRoot;
    }

    private void LeftRotate(AvlTreeNode<T> node)
    {
        var prevRoot = node;
        var rightLeftChild = prevRoot.Right.Left;

        var newRoot = node.Right;

        //make right child as root
        prevRoot.Right.Parent = prevRoot.Parent;

        if (prevRoot.Parent != null)
        {
            if (prevRoot.Parent.Left == prevRoot)
                prevRoot.Parent.Left = prevRoot.Right;
            else
                prevRoot.Parent.Right = prevRoot.Right;
        }

        //move prev root as left child of current root
        newRoot.Left = prevRoot;
        prevRoot.Parent = newRoot;

        //move left child of right child of prev root to right child of left child of new root
        newRoot.Left.Right = rightLeftChild;
        if (newRoot.Left.Right != null) newRoot.Left.Right.Parent = newRoot.Left;

        UpdateHeight(newRoot);

        newRoot.Left.UpdateCounts();
        newRoot.Right.UpdateCounts();
        newRoot.UpdateCounts();

        if (prevRoot == Root) Root = newRoot;
    }

    private void UpdateHeight(AvlTreeNode<T> node)
    {
        if (node == null) return;

        if (node.Left != null)
            node.Left.Height = Math.Max(node.Left.Left?.Height + 1 ?? 0,
                node.Left.Right?.Height + 1 ?? 0);

        if (node.Right != null)
            node.Right.Height = Math.Max(node.Right.Left?.Height + 1 ?? 0,
                node.Right.Right?.Height + 1 ?? 0);

        node.Height = Math.Max(node.Left?.Height + 1 ?? 0,
            node.Right?.Height + 1 ?? 0);
    }

    private void RecomputeHeight(AvlTreeNode<T> node)
    {
        if (node == null) return;

        RecomputeHeight(node.Left);
        RecomputeHeight(node.Right);

        UpdateHeight(node);
    }

    /// <summary>
    ///     Get the next lower value to given value in this BST.
    ///     Time complexity: O(log(n))
    /// </summary>
    public T NextLower(T value)
    {
        var node = Find(value);
        if (node == null) return default;

        var next = node.NextLower();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Get the next higher value to given value in this BST.
    ///     Time complexity: O(log(n))
    /// </summary>
    public T NextHigher(T value)
    {
        var node = Find(value);
        if (node == null) return default;

        var next = node.NextHigher();
        return next != null ? next.Value : default;
    }

    internal void Swap(T value1, T value2)
    {
        var node1 = Find(value1);
        var node2 = Find(value2);

        if (node1 == null || node2 == null) throw new Exception("Value1, Value2 or both was not found in this BST.");

        var tmp = node1.Value;
        node1.Value = node2.Value;
        node2.Value = tmp;

        if (nodeLookUp != null)
        {
            nodeLookUp[node1.Value] = node1;
            nodeLookUp[node2.Value] = node2;
        }
    }

    /// <summary>
    ///     Descending enumerable.
    /// </summary>
    public IEnumerable<T> AsEnumerableDesc()
    {
        return GetEnumeratorDesc().AsEnumerable();
    }

    public IEnumerator<T> GetEnumeratorDesc()
    {
        return new BstEnumerator<T>(Root, false);
    }
}

internal class AvlTreeNode<T> : BstNodeBase<T> where T : IComparable
{
    internal AvlTreeNode(AvlTreeNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;
        Height = 0;
    }

    internal new AvlTreeNode<T> Parent
    {
        get => (AvlTreeNode<T>)base.Parent;
        set => base.Parent = value;
    }

    internal new AvlTreeNode<T> Left
    {
        get => (AvlTreeNode<T>)base.Left;
        set => base.Left = value;
    }

    internal new AvlTreeNode<T> Right
    {
        get => (AvlTreeNode<T>)base.Right;
        set => base.Right = value;
    }

    internal int Height { get; set; }
}ParseOptions.0.json‡›
bD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\B+Tree.cs„‹using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A B+ tree implementation.
/// </summary>
public class BpTree<T> : IEnumerable<T> where T : IComparable
{
    private readonly int maxKeysPerNode;
    private readonly int minKeysPerNode;

    /// <summary>
    ///     Keep a reference of Bottom Left/Right Node
    ///     for fast ascending/descending enumeration using Next pointer.
    ///     See IEnumerable and IEnumerableDesc implementation at bottom.
    /// </summary>
    internal BpTreeNode<T> BottomLeftNode;

    internal BpTreeNode<T> BottomRightNode;

    internal BpTreeNode<T> Root;

    public BpTree(int maxKeysPerNode = 3)
    {
        if (maxKeysPerNode < 3) throw new Exception("Max keys per node should be atleast 3.");

        this.maxKeysPerNode = maxKeysPerNode;
        minKeysPerNode = maxKeysPerNode / 2;
    }

    public int Count { get; private set; }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T Max
    {
        get
        {
            if (Root == null) return default;

            var maxNode = BottomRightNode;
            return maxNode.Keys[maxNode.KeyCount - 1];
        }
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T Min
    {
        get
        {
            if (Root == null) return default;

            var minNode = BottomLeftNode;
            return minNode.Keys[0];
        }
    }

    //Implementation for the GetEnumerator method.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BpTreeEnumerator<T>(this);
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public bool HasItem(T value)
    {
        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Find the given value node under given node
    /// </summary>
    private BpTreeNode<T> Find(BpTreeNode<T> node, T value)
    {
        //if leaf then its time to insert
        if (node.IsLeaf)
        {
            for (var i = 0; i < node.KeyCount; i++)
                if (value.CompareTo(node.Keys[i]) == 0)
                    return node;
        }
        else
        {
            //if not leaf then drill down to leaf
            for (var i = 0; i < node.KeyCount; i++)
            {
                //current value is less than new value
                //drill down to left child of current value
                if (value.CompareTo(node.Keys[i]) < 0) return Find(node.Children[i], value);
                //current value is grearer than new value
                //and current value is last element 

                if (node.KeyCount == i + 1) return Find(node.Children[i + 1], value);
            }
        }

        return null;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Insert(T newValue)
    {
        if (Root == null)
        {
            Root = new BpTreeNode<T>(maxKeysPerNode, null) { Keys = { [0] = newValue } };
            Root.KeyCount++;
            Count++;
            BottomLeftNode = Root;
            BottomRightNode = Root;
            return;
        }

        var leafToInsert = FindInsertionLeaf(Root, newValue);

        InsertAndSplit(ref leafToInsert, newValue, null, null);
        Count++;
    }

    /// <summary>
    ///     Find the leaf node to start initial insertion.
    /// </summary>
    private BpTreeNode<T> FindInsertionLeaf(BpTreeNode<T> node, T newValue)
    {
        //if leaf then its time to insert
        if (node.IsLeaf) return node;

        //if not leaf then drill down to leaf
        for (var i = 0; i < node.KeyCount; i++)
        {
            //current value is less than new value
            //drill down to left child of current value
            if (newValue.CompareTo(node.Keys[i]) < 0) return FindInsertionLeaf(node.Children[i], newValue);
            //current value is grearer than new value
            //and current value is last element 

            if (node.KeyCount == i + 1) return FindInsertionLeaf(node.Children[i + 1], newValue);
        }

        return node;
    }

    /// <summary>
    ///     Insert and split recursively up until no split is required
    /// </summary>
    private void InsertAndSplit(ref BpTreeNode<T> node, T newValue,
        BpTreeNode<T> newValueLeft, BpTreeNode<T> newValueRight)
    {
        //add new item to current node
        //this increases the height of B+ tree by one by adding a new root at top
        if (node == null)
        {
            node = new BpTreeNode<T>(maxKeysPerNode, null);
            Root = node;
        }

        //newValue have room to fit in this node
        //so just insert in right spot in asc order of keys
        if (node.KeyCount != maxKeysPerNode)
        {
            InsertToNotFullNode(ref node, newValue, newValueLeft, newValueRight);
            return;
        }

        //if node is full then split node
        //and then insert new median to parent.

        //divide the current node values + new Node as left and right sub nodes
        var left = new BpTreeNode<T>(maxKeysPerNode, null);
        var right = new BpTreeNode<T>(maxKeysPerNode, null);

        //connect leaves via linked list for faster enumeration
        if (node.IsLeaf) ConnectLeaves(node, left, right);

        //median of current Node
        var currentMedianIndex = node.GetMedianIndex();

        //init currentNode under consideration to left
        var currentNode = left;
        var currentNodeIndex = 0;

        //new Median also takes new Value in to Account
        var newMedian = default(T);
        var newMedianSet = false;
        var newValueInserted = false;

        //keep track of each insertion
        var insertionCount = 0;

        //insert newValue and existing values in sorted order
        //to left and right nodes
        //set new median during sorting
        for (var i = 0; i < node.KeyCount; i++)
        {
            //if insertion count reached new median
            //set the new median by picking the next smallest value
            if (!newMedianSet && insertionCount == currentMedianIndex)
            {
                newMedianSet = true;

                //median can be the new value or node.keys[i] (next node key)
                //whichever is smaller
                if (!newValueInserted && newValue.CompareTo(node.Keys[i]) < 0)
                {
                    //median is new value
                    newMedian = newValue;
                    newValueInserted = true;

                    if (newValueLeft != null) SetChild(currentNode, currentNode.KeyCount, newValueLeft);

                    //now fill right node
                    currentNode = right;
                    currentNodeIndex = 0;

                    if (newValueRight != null) SetChild(currentNode, 0, newValueRight);

                    i--;
                    insertionCount++;
                    continue;
                }

                //median is next node
                newMedian = node.Keys[i];

                //now fill right node
                currentNode = right;
                currentNodeIndex = 0;

                continue;
            }

            //pick the smaller among newValue and node.Keys[i]
            //and insert in to currentNode (left and right nodes)
            //if new Value was already inserted then just copy from node.Keys in sequence
            //since node.Keys is already in sorted order it should be fine
            if (newValueInserted || node.Keys[i].CompareTo(newValue) < 0)
            {
                currentNode.Keys[currentNodeIndex] = node.Keys[i];
                currentNode.KeyCount++;

                //if child is set don't set again
                //the child was already set by last newValueRight or last node
                if (currentNode.Children[currentNodeIndex] == null)
                    SetChild(currentNode, currentNodeIndex, node.Children[i]);

                SetChild(currentNode, currentNodeIndex + 1, node.Children[i + 1]);
            }
            else
            {
                currentNode.Keys[currentNodeIndex] = newValue;
                currentNode.KeyCount++;

                SetChild(currentNode, currentNodeIndex, newValueLeft);
                SetChild(currentNode, currentNodeIndex + 1, newValueRight);

                i--;
                newValueInserted = true;
            }

            currentNodeIndex++;
            insertionCount++;
        }

        //could be that thew newKey is the greatest 
        //so insert at end
        if (!newValueInserted)
        {
            currentNode.Keys[currentNodeIndex] = newValue;
            currentNode.KeyCount++;

            SetChild(currentNode, currentNodeIndex, newValueLeft);
            SetChild(currentNode, currentNodeIndex + 1, newValueRight);
        }

        if (node.IsLeaf)
        {
            InsertAt(right.Keys, 0, newMedian);
            right.KeyCount++;
        }

        //insert overflow element (newMedian) to parent
        var parent = node.Parent;
        InsertAndSplit(ref parent, newMedian, left, right);
    }

    /// <summary>
    ///     Insert to a node that is not full.
    /// </summary>
    private void InsertToNotFullNode(ref BpTreeNode<T> node, T newValue,
        BpTreeNode<T> newValueLeft, BpTreeNode<T> newValueRight)
    {
        var inserted = false;

        //if left is not null
        //then right should'nt be null
        if (newValueLeft != null)
        {
            newValueLeft.Parent = node;
            newValueRight.Parent = node;
        }

        //insert in sorted order
        for (var i = 0; i < node.KeyCount; i++)
        {
            if (newValue.CompareTo(node.Keys[i]) >= 0) continue;

            InsertAt(node.Keys, i, newValue);
            node.KeyCount++;

            //Insert children if any
            SetChild(node, i, newValueLeft);
            InsertChild(node, i + 1, newValueRight);

            inserted = true;
            break;
        }

        //newValue is the greatest
        //element should be inserted at the end then
        if (inserted) return;

        node.Keys[node.KeyCount] = newValue;
        node.KeyCount++;

        SetChild(node, node.KeyCount - 1, newValueLeft);
        SetChild(node, node.KeyCount, newValueRight);
    }


    private void ConnectLeaves(BpTreeNode<T> node, BpTreeNode<T> left, BpTreeNode<T> right)
    {
        left.Next = right;
        right.Prev = left;

        if (node.Next != null)
        {
            right.Next = node.Next;
            node.Next.Prev = right;
        }
        else
        {
            //bottom right most node
            BottomRightNode = right;
        }

        if (node.Prev != null)
        {
            left.Prev = node.Prev;
            node.Prev.Next = left;
        }
        else
        {
            //bottom left most node
            BottomLeftNode = left;
        }
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Delete(T value)
    {
        var node = FindDeletionNode(Root, value);

        if (node == null) throw new Exception("Item do not exist in this tree.");

        for (var i = 0; i < node.KeyCount; i++)
        {
            if (value.CompareTo(node.Keys[i]) != 0) continue;

            RemoveAt(node.Keys, i);
            node.KeyCount--;

            if (node.Parent != null && node != node.Parent.Children[0] && node.KeyCount > 0)
            {
                var separatorIndex = GetPrevSeparatorIndex(node);
                node.Parent.Keys[separatorIndex] = node.Keys[0];
            }

            Balance(node, value);

            Count--;
            return;
        }
    }

    /// <summary>
    ///     return the node containing min value which will be a leaf at the left most
    /// </summary>
    private BpTreeNode<T> FindMinNode(BpTreeNode<T> node)
    {
        while (true)
        {
            //if leaf return node
            if (node.IsLeaf) return node;
            node = node.Children[0];
        }
    }

    /// <summary>
    ///     return the node containing max value which will be a leaf at the right most
    /// </summary>
    private BpTreeNode<T> FindMaxNode(BpTreeNode<T> node)
    {
        while (true)
        {
            //if leaf return node
            if (node.IsLeaf) return node;
            node = node.Children[node.KeyCount];
        }
    }

    /// <summary>
    ///     Balance a node which is short of Keys by rotations or merge
    /// </summary>
    private void Balance(BpTreeNode<T> node, T deleteKey)
    {
        if (node == Root) return;

        if (node.KeyCount >= minKeysPerNode)
        {
            UpdateIndex(node, deleteKey, true);
            return;
        }

        var rightSibling = GetRightSibling(node);

        if (rightSibling != null
            && rightSibling.KeyCount > minKeysPerNode)
        {
            LeftRotate(node, rightSibling);
            FindMinNode(node);
            UpdateIndex(node, deleteKey, true);
            return;
        }

        var leftSibling = GetLeftSibling(node);

        if (leftSibling != null
            && leftSibling.KeyCount > minKeysPerNode)
        {
            RightRotate(leftSibling, node);
            UpdateIndex(node, deleteKey, true);
            return;
        }

        if (rightSibling != null)
            Sandwich(node, rightSibling, deleteKey);
        else
            Sandwich(leftSibling, node, deleteKey);
    }

    /// <summary>
    ///     optionally recursively update outdated index with new min of right node
    ///     after deletion of a value
    /// </summary>
    private void UpdateIndex(BpTreeNode<T> node, T deleteKey, bool spiralUp)
    {
        while (true)
        {
            if (node == null) return;

            if (node.IsLeaf || node.Children[0].IsLeaf)
            {
                node = node.Parent;
                continue;
            }

            for (var i = 0; i < node.KeyCount; i++)
                if (node.Keys[i].CompareTo(deleteKey) == 0)
                    node.Keys[i] = FindMinNode(node.Children[i + 1]).Keys[0];

            if (spiralUp)
            {
                node = node.Parent;
                continue;
            }

            break;
        }
    }

    /// <summary>
    ///     merge two adjacent siblings to one node
    /// </summary>
    private void Sandwich(BpTreeNode<T> leftSibling, BpTreeNode<T> rightSibling, T deleteKey)
    {
        var separatorIndex = GetNextSeparatorIndex(leftSibling);
        var parent = leftSibling.Parent;

        var newNode = new BpTreeNode<T>(maxKeysPerNode, leftSibling.Parent);

        //if leaves are merged then update the Next and Prev pointers
        if (leftSibling.IsLeaf) MergeLeaves(newNode, leftSibling, rightSibling);

        var newIndex = 0;
        for (var i = 0; i < leftSibling.KeyCount; i++)
        {
            newNode.Keys[newIndex] = leftSibling.Keys[i];

            if (leftSibling.Children[i] != null) SetChild(newNode, newIndex, leftSibling.Children[i]);

            if (leftSibling.Children[i + 1] != null) SetChild(newNode, newIndex + 1, leftSibling.Children[i + 1]);

            newIndex++;
        }

        //special case when left sibling is empty 
        if (leftSibling.KeyCount == 0 && leftSibling.Children[0] != null)
            SetChild(newNode, newIndex, leftSibling.Children[0]);

        if (!rightSibling.IsLeaf)
        {
            newNode.Keys[newIndex] = parent.Keys[separatorIndex];
            newIndex++;
        }

        for (var i = 0; i < rightSibling.KeyCount; i++)
        {
            newNode.Keys[newIndex] = rightSibling.Keys[i];

            if (rightSibling.Children[i] != null)
            {
                SetChild(newNode, newIndex, rightSibling.Children[i]);

                //when a left leaf is added as right leaf
                //we need to push parent key as first node of new leaf
                if (i == 0 && rightSibling.Children[i].IsLeaf
                           && rightSibling.Children[i].Keys[0].CompareTo(newNode.Keys[newIndex - 1]) != 0)
                {
                    InsertAt(rightSibling.Children[i].Keys, 0, newNode.Keys[newIndex - 1]);
                    rightSibling.Children[i].KeyCount++;
                }
            }

            if (rightSibling.Children[i + 1] != null) SetChild(newNode, newIndex + 1, rightSibling.Children[i + 1]);

            newIndex++;
        }

        //special case when left sibling is empty 
        if (rightSibling.KeyCount == 0 && rightSibling.Children[0] != null)
        {
            SetChild(newNode, newIndex, rightSibling.Children[0]);

            if (newNode.Children[newIndex].IsLeaf) newNode.Keys[newIndex - 1] = newNode.Children[newIndex].Keys[0];
        }

        newNode.KeyCount = newIndex;

        SetChild(parent, separatorIndex, newNode);

        RemoveAt(parent.Keys, separatorIndex);
        parent.KeyCount--;

        RemoveChild(parent, separatorIndex + 1);

        if (newNode.IsLeaf && newNode.Parent.Children[0] != newNode)
        {
            separatorIndex = GetPrevSeparatorIndex(newNode);
            newNode.Parent.Keys[separatorIndex] = newNode.Keys[0];
        }

        UpdateIndex(newNode, deleteKey, false);

        if (parent.KeyCount == 0
            && parent == Root)
        {
            Root = newNode;
            Root.Parent = null;

            if (Root.KeyCount == 0) Root = null;
            return;
        }

        if (parent.KeyCount < minKeysPerNode) Balance(parent, deleteKey);

        UpdateIndex(newNode, deleteKey, true);
    }

    /// <summary>
    ///     do a right rotation
    /// </summary>
    private void RightRotate(BpTreeNode<T> leftSibling, BpTreeNode<T> rightSibling)
    {
        var parentIndex = GetNextSeparatorIndex(leftSibling);

        //move parent value to right
        InsertAt(rightSibling.Keys, 0, rightSibling.Parent.Keys[parentIndex]);
        rightSibling.KeyCount++;

        InsertChild(rightSibling, 0, leftSibling.Children[leftSibling.KeyCount]);

        if (rightSibling.Children[1] != null
            && rightSibling.Children[1].IsLeaf)
            rightSibling.Keys[0] = rightSibling.Children[1].Keys[0];


        //move rightmost element in left sibling to parent
        rightSibling.Parent.Keys[parentIndex] = leftSibling.Keys[leftSibling.KeyCount - 1];

        //remove rightmost element of left sibling
        RemoveAt(leftSibling.Keys, leftSibling.KeyCount - 1);
        leftSibling.KeyCount--;

        RemoveChild(leftSibling, leftSibling.KeyCount + 1);

        if (rightSibling.IsLeaf) rightSibling.Keys[0] = rightSibling.Parent.Keys[parentIndex];
    }

    /// <summary>
    ///     do a left rotation
    /// </summary>
    private void LeftRotate(BpTreeNode<T> leftSibling, BpTreeNode<T> rightSibling)
    {
        var parentIndex = GetNextSeparatorIndex(leftSibling);

        //move root to left
        leftSibling.Keys[leftSibling.KeyCount] = leftSibling.Parent.Keys[parentIndex];
        leftSibling.KeyCount++;

        SetChild(leftSibling, leftSibling.KeyCount, rightSibling.Children[0]);

        if (leftSibling.Children[leftSibling.KeyCount] != null
            && leftSibling.Children[leftSibling.KeyCount].IsLeaf)
            leftSibling.Keys[leftSibling.KeyCount - 1] = leftSibling.Children[leftSibling.KeyCount].Keys[0];

        //move right to parent
        leftSibling.Parent.Keys[parentIndex] = rightSibling.Keys[0];
        //remove right
        RemoveAt(rightSibling.Keys, 0);
        rightSibling.KeyCount--;

        RemoveChild(rightSibling, 0);

        if (rightSibling.IsLeaf) rightSibling.Parent.Keys[parentIndex] = rightSibling.Keys[0];

        if (leftSibling.IsLeaf && leftSibling.Parent.Children[0] != leftSibling)
        {
            parentIndex = GetPrevSeparatorIndex(leftSibling);
            leftSibling.Parent.Keys[parentIndex] = leftSibling.Keys[0];
        }
    }

    private void MergeLeaves(BpTreeNode<T> newNode, BpTreeNode<T> leftSibling, BpTreeNode<T> rightSibling)
    {
        if (leftSibling.Prev != null)
        {
            newNode.Prev = leftSibling.Prev;
            leftSibling.Prev.Next = newNode;
        }
        else
        {
            BottomLeftNode = newNode;
        }

        if (rightSibling.Next != null)
        {
            newNode.Next = rightSibling.Next;
            rightSibling.Next.Prev = newNode;
        }
        else
        {
            BottomRightNode = newNode;
        }
    }

    /// <summary>
    ///     Locate the node in which the item to delete exist
    /// </summary>
    private BpTreeNode<T> FindDeletionNode(BpTreeNode<T> node, T value)
    {
        //if leaf then its time to insert
        if (node.IsLeaf)
        {
            for (var i = 0; i < node.KeyCount; i++)
                if (value.CompareTo(node.Keys[i]) == 0)
                    return node;
        }
        else
        {
            //if not leaf then drill down to leaf
            for (var i = 0; i < node.KeyCount; i++)
            {
                //current value is less than new value
                //drill down to left child of current value
                if (value.CompareTo(node.Keys[i]) < 0) return FindDeletionNode(node.Children[i], value);
                //current value is grearer than new value
                //and current value is last element 

                if (node.KeyCount == i + 1) return FindDeletionNode(node.Children[i + 1], value);
            }
        }

        return null;
    }

    /// <summary>
    ///     Get prev separator key of this child Node in parent
    /// </summary>
    private int GetPrevSeparatorIndex(BpTreeNode<T> node)
    {
        var parent = node.Parent;

        if (node.Index == 0) return 0;

        if (node.Index == parent.KeyCount) return node.Index - 1;

        return node.Index - 1;
    }

    /// <summary>
    ///     Get next separator key of this child Node in parent
    /// </summary>
    private int GetNextSeparatorIndex(BpTreeNode<T> node)
    {
        var parent = node.Parent;

        if (node.Index == 0) return 0;

        if (node.Index == parent.KeyCount) return node.Index - 1;

        return node.Index;
    }

    /// <summary>
    ///     get the right sibling node
    /// </summary>
    private BpTreeNode<T> GetRightSibling(BpTreeNode<T> node)
    {
        var parent = node.Parent;
        return node.Index == parent.KeyCount ? null : parent.Children[node.Index + 1];
    }

    /// <summary>
    ///     get left sibling node
    /// </summary>
    private BpTreeNode<T> GetLeftSibling(BpTreeNode<T> node)
    {
        return node.Index == 0 ? null : node.Parent.Children[node.Index - 1];
    }

    private void SetChild(BpTreeNode<T> parent, int childIndex, BpTreeNode<T> child)
    {
        parent.Children[childIndex] = child;

        if (child == null) return;

        child.Parent = parent;
        child.Index = childIndex;
    }

    private void InsertChild(BpTreeNode<T> parent, int childIndex, BpTreeNode<T> child)
    {
        InsertAt(parent.Children, childIndex, child);

        if (child != null) child.Parent = parent;

        //update indices
        for (var i = childIndex; i <= parent.KeyCount; i++)
            if (parent.Children[i] != null)
                parent.Children[i].Index = i;
    }

    private void RemoveChild(BpTreeNode<T> parent, int childIndex)
    {
        RemoveAt(parent.Children, childIndex);

        //update indices
        for (var i = childIndex; i <= parent.KeyCount; i++)
            if (parent.Children[i] != null)
                parent.Children[i].Index = i;
    }

    /// <summary>
    ///     Shift array right at index to make room for new insertion
    ///     And then insert at index
    ///     Assumes array have atleast one empty index at end
    /// </summary>
    private void InsertAt<TS>(TS[] array, int index, TS newValue)
    {
        //shift elements right by one indice from index
        Array.Copy(array, index, array, index + 1, array.Length - index - 1);
        //now set the value
        array[index] = newValue;
    }

    /// <summary>
    ///     Shift array left at index
    /// </summary>
    private void RemoveAt<TS>(TS[] array, int index)
    {
        //shift elements right by one indice from index
        Array.Copy(array, index + 1, array, index, array.Length - index - 1);
    }

    /// <summary>
    ///     Descending enumerable.
    /// </summary>
    public IEnumerable<T> AsEnumerableDesc()
    {
        return GetEnumeratorDesc().AsEnumerable();
    }

    public IEnumerator<T> GetEnumeratorDesc()
    {
        return new BpTreeEnumerator<T>(this, false);
    }
}

internal class BpTreeNode<T> : BNode<T> where T : IComparable
{
    internal BpTreeNode(int maxKeysPerNode, BpTreeNode<T> parent)
        : base(maxKeysPerNode)
    {
        Parent = parent;
        Children = new BpTreeNode<T>[maxKeysPerNode + 1];
    }

    internal BpTreeNode<T> Parent { get; set; }
    internal BpTreeNode<T>[] Children { get; set; }

    internal bool IsLeaf => Children[0] == null;

    /// <summary>
    ///     Pointer to sibling leaf on left for faster enumeration
    /// </summary>
    public BpTreeNode<T> Prev { get; set; }

    /// <summary>
    ///     Pointer to sibling leaf on right for faster enumeration
    /// </summary>
    public BpTreeNode<T> Next { get; set; }

    /// <summary>
    ///     For shared test method accross B and B+ tree
    /// </summary>
    internal override BNode<T> GetParent()
    {
        return Parent;
    }

    /// <summary>
    ///     For shared test method accross B and B+ tree
    /// </summary>
    internal override BNode<T>[] GetChildren()
    {
        return Children;
    }
}

internal class BpTreeEnumerator<T> : IEnumerator<T> where T : IComparable
{
    private readonly bool asc;
    private BpTreeNode<T> current;

    private int index;

    private BpTreeNode<T> startNode;

    internal BpTreeEnumerator(BpTree<T> tree, bool asc = true)
    {
        this.asc = asc;

        startNode = asc ? tree.BottomLeftNode : tree.BottomRightNode;
        current = startNode;

        index = asc ? -1 : current.KeyCount;
    }

    public bool MoveNext()
    {
        if (current == null) return false;

        if (asc)
        {
            if (index + 1 < current.KeyCount)
            {
                index++;
                return true;
            }
        }
        else
        {
            if (index - 1 >= 0)
            {
                index--;
                return true;
            }
        }

        current = asc ? current.Next : current.Prev;

        var canMove = current != null && current.KeyCount > 0;
        if (canMove) index = asc ? 0 : current.KeyCount - 1;

        return canMove;
    }

    public void Reset()
    {
        current = startNode;
        index = asc ? -1 : current.KeyCount;
    }

    object IEnumerator.Current => Current;

    public T Current => current.Keys[index];

    public void Dispose()
    {
        current = null;
        startNode = null;
    }
}ParseOptions.0.jsonù5
fD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\BinaryTree.csù4using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A binary tree implementation using pointers.
/// </summary>
public class BinaryTree<T> : IEnumerable<T> where T : IComparable
{
    private BinaryTreeNode<T> Root { get; set; }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BinaryTreeEnumerator<T>(Root);
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public bool HasItem(T value)
    {
        if (Root == null) return false;

        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public int GetHeight()
    {
        return GetHeight(Root);
    }

    /// <summary>
    ///     Only inserts to unambiguous nodes (a node with two children cannot be inserted with a new child unambiguously).
    ///     Time complexity: O(n)
    /// </summary>
    /// <summary>
    public void Insert(T parent, T child)
    {
        if (Root == null)
        {
            Root = new BinaryTreeNode<T>(null, child);
            Count++;
            return;
        }

        var parentNode = Find(parent);

        if (parentNode == null) throw new Exception("Cannot find parent node");

        var exists = Find(Root, child) != null;

        if (exists) throw new ArgumentNullException("value already exists");

        switch (parentNode.Left)
        {
            case null when parentNode.Right == null:
                parentNode.Left = new BinaryTreeNode<T>(parentNode, child);
                break;
            case null:
                parentNode.Left = new BinaryTreeNode<T>(parentNode, child);
                break;
            default:
                if (parentNode.Right == null)
                    parentNode.Right = new BinaryTreeNode<T>(parentNode, child);
                else
                    throw new Exception("Cannot insert to a parent with two child node unambiguosly");

                break;
        }

        Count++;
    }

    /// <summary>
    ///     Only deletes unambiguous nodes (a node with two children cannot be deleted unambiguously).
    ///     Time complexity: O(n)
    /// </summary>
    public void Delete(T value)
    {
        var node = Find(value);

        if (node == null) throw new Exception("Cannot find node");

        switch (node.Left)
        {
            case null when node.Right == null:
                if (node.Parent == null)
                {
                    Root = null;
                }
                else
                {
                    if (node.Parent.Left == node)
                        node.Parent.Left = null;
                    else
                        node.Parent.Right = null;
                }

                break;
            case null when node.Right != null:
                node.Right.Parent = node.Parent;

                if (node.Parent.Left == node)
                    node.Parent.Left = node.Right;
                else
                    node.Parent.Right = node.Right;

                break;
            default:
                if (node.Right == null && node.Left != null)
                {
                    node.Left.Parent = node.Parent;

                    if (node.Parent.Left == node)
                        node.Parent.Left = node.Left;
                    else
                        node.Parent.Right = node.Left;
                }
                else
                {
                    throw new Exception("Cannot delete two child node unambiguosly");
                }

                break;
        }

        Count--;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public IEnumerable<T> Children(T value)
    {
        var node = Find(value);

        if (node != null) return new[] { node.Left, node.Right }.Where(x => x != null).Select(x => x.Value);

        return null;
    }

    private int GetHeight(BinaryTreeNode<T> node)
    {
        if (node == null) return -1;

        return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }

    private BinaryTreeNode<T> Find(T value)
    {
        return Root == null ? null : Find(Root, value);
    }

    private BinaryTreeNode<T> Find(BinaryTreeNode<T> parent, T value)
    {
        while (true)
        {
            if (parent == null) return null;

            if (parent.Value.CompareTo(value) == 0) return parent;

            var left = Find(parent.Left, value);

            if (left != null) return left;

            parent = parent.Right;
        }
    }
}

internal class BinaryTreeNode<T> : IComparable where T : IComparable
{
    internal BinaryTreeNode(BinaryTreeNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;
    }

    internal T Value { get; set; }

    internal BinaryTreeNode<T> Parent { get; set; }

    internal BinaryTreeNode<T> Left { get; set; }
    internal BinaryTreeNode<T> Right { get; set; }

    internal bool IsLeaf => Left == null && Right == null;

    public int CompareTo(object obj)
    {
        return Value.CompareTo(obj as BinaryTreeNode<T>);
    }
}

internal class BinaryTreeEnumerator<T> : IEnumerator<T> where T : IComparable
{
    private readonly BinaryTreeNode<T> root;
    private Stack<BinaryTreeNode<T>> progress;

    internal BinaryTreeEnumerator(BinaryTreeNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null)
        {
            progress = new Stack<BinaryTreeNode<T>>(new[] { root.Left, root.Right }.Where(x => x != null));
            Current = root.Value;
            return true;
        }

        if (progress.Count > 0)
        {
            var next = progress.Pop();
            Current = next.Value;

            foreach (var node in new[] { next.Left, next.Right }.Where(x => x != null)) progress.Push(node);

            return true;
        }

        return false;
    }

    public void Reset()
    {
        progress = null;
        Current = default;
    }

    public T Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        progress = null;
    }
}ParseOptions.0.json‹X
_D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\BST.cs„Wusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A binary search tree implementation.
/// </summary>
public class Bst<T> : IEnumerable<T> where T : IComparable
{
    public Bst()
    {
    }

    /// <summary>
    ///     Initialize the BST with given sorted keys.
    ///     Time complexity: O(n).
    /// </summary>
    public Bst(IEnumerable<T> sortedCollection) : this()
    {
        BstHelpers.ValidateSortedCollection(sortedCollection);
        var nodes = sortedCollection.Select(x => new BstNode<T>(null, x)).ToArray();
        Root = (BstNode<T>)BstHelpers.ToBst(nodes);
        BstHelpers.AssignCount(Root);
    }

    internal BstNode<T> Root { get; set; }

    public int Count => Root == null ? 0 : Root.Count;

    //Implementation for the GetEnumerator method.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BstEnumerator<T>(Root);
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public bool HasItem(T value)
    {
        if (Root == null) return false;

        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    internal int GetHeight()
    {
        return GetHeight(Root);
    }

    //worst O(n) for unbalanced tree
    private int GetHeight(BstNode<T> node)
    {
        if (node == null) return -1;

        return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }


    internal BstNode<T> InsertAndReturnNewNode(T value)
    {
        if (Root == null)
        {
            Root = new BstNode<T>(null, value);
            return Root;
        }

        var newNode = Insert(Root, value);
        return newNode;
    }


    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public void Insert(T value)
    {
        if (Root == null)
        {
            Root = new BstNode<T>(null, value);
            return;
        }

        var newNode = Insert(Root, value);
        newNode.UpdateCounts(true);
    }


    //worst O(n) for unbalanced tree
    private BstNode<T> Insert(BstNode<T> currentNode, T newNodeValue)
    {
        while (true)
        {
            var compareResult = currentNode.Value.CompareTo(newNodeValue);

            //current node is less than new item
            if (compareResult < 0)
            {
                //no right child
                if (currentNode.Right != null)
                {
                    currentNode = currentNode.Right;
                    continue;
                }

                //insert
                currentNode.Right = new BstNode<T>(currentNode, newNodeValue);
                return currentNode.Right;
            }
            //current node is greater than new node

            if (compareResult > 0)
            {
                if (currentNode.Left == null)
                {
                    //insert
                    currentNode.Left = new BstNode<T>(currentNode, newNodeValue);
                    return currentNode.Left;
                }

                currentNode = currentNode.Left;
            }
            else
            {
                throw new Exception("Item exists");
            }
        }
    }

    // <summary>
    /// Time complexity: O(n)
    /// </summary>
    public int IndexOf(T item)
    {
        return Root.Position(item);
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T ElementAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentNullException("index");

        return Root.KthSmallest(index).Value;
    }


    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public void Delete(T value)
    {
        if (Root == null) throw new Exception("Empty BST");

        var deleted = Delete(Root, value);
        deleted.UpdateCounts(true);
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentException("index");

        var nodeToDelete = Root.KthSmallest(index) as BstNode<T>;

        var deleted = Delete(nodeToDelete, nodeToDelete.Value);
        deleted.UpdateCounts(true);

        return nodeToDelete.Value;
    }

    //worst O(n) for unbalanced tree
    private BstNode<T> Delete(BstNode<T> node, T value)
    {
        while (true)
        {
            if (node != null)
            {
                var compareResult = node.Value.CompareTo(value);

                //node is less than the search value so move right to find the deletion node
                if (compareResult < 0)
                {
                    node = node.Right ?? throw new Exception("Item do not exist");
                    continue;
                }

                //node is less than the search value so move left to find the deletion node
                if (compareResult > 0)
                {
                    node = node.Left ?? throw new Exception("Item do not exist");
                    continue;
                }
            }

            if (node == null) return null;


            //node is a leaf node
            if (node.IsLeaf)
            {
                DeleteLeaf(node);
                return node;
            }

            //case one - right tree is null (move sub tree up)
            if (node.Left != null && node.Right == null)
            {
                DeleteLeftNode(node);
                return node;
            }

            //case two - left tree is null  (move sub tree up)
            if (node.Right != null && node.Left == null)
            {
                DeleteRightNode(node);
                return node;
            }

            //case three - two child trees 
            //replace the node value with maximum element of left subtree (left max node)
            //and then delete the left max node
            var maxLeftNode = FindMax(node.Left);

            node.Value = maxLeftNode.Value;

            //delete left max node
            node = node.Left;
            value = maxLeftNode.Value;
        }
    }

    private void DeleteLeaf(BstNode<T> node)
    {
        //if node is root
        if (node.Parent == null)
            Root = null;
        //assign nodes parent.left/right to null
        else if (node.IsLeftChild)
            node.Parent.Left = null;
        else
            node.Parent.Right = null;
    }

    private void DeleteRightNode(BstNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Right.Parent = null;
            Root = Root.Right;
        }
        else
        {
            //node is left child of parent
            if (node.IsLeftChild)
                node.Parent.Left = node.Right;
            //node is right child of parent
            else
                node.Parent.Right = node.Right;

            node.Right.Parent = node.Parent;
        }
    }

    private void DeleteLeftNode(BstNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Left.Parent = null;
            Root = Root.Left;
        }
        else
        {
            //node is left child of parent
            if (node.IsLeftChild)
                node.Parent.Left = node.Left;
            //node is right child of parent
            else
                node.Parent.Right = node.Left;

            node.Left.Parent = node.Parent;
        }
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T FindMax()
    {
        return FindMax(Root).Value;
    }

    private BstNode<T> FindMax(BstNode<T> node)
    {
        while (true)
        {
            if (node.Right == null) return node;
            node = node.Right;
        }
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T FindMin()
    {
        return FindMin(Root).Value;
    }

    private BstNode<T> FindMin(BstNode<T> node)
    {
        while (true)
        {
            if (node.Left == null) return node;
            node = node.Left;
        }
    }

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    //worst O(n) for unbalanced tree
    internal BstNode<T> FindNode(T value)
    {
        return Find(Root, value);
    }

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    //worst O(n) for unbalanced tree
    private BstNode<T> Find(BstNode<T> parent, T value)
    {
        while (true)
        {
            if (parent == null) return null;

            if (parent.Value.CompareTo(value) == 0) return parent;

            var left = Find(parent.Left, value);

            if (left != null) return left;

            parent = parent.Right;
        }
    }

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    //O(log(n)) worst O(n) for unbalanced tree
    private BstNodeBase<T> Find(T value)
    {
        return Root.Find(value).Item1;
    }

    /// <summary>
    ///     Get the next lower value to given value in this BST.
    ///     Time complexity: O(n)
    /// </summary>
    public T NextLower(T value)
    {
        var node = Find(value);
        if (node == null) return default;

        var next = node.NextLower();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Get the next higher value to given value in this BST.
    ///     Time complexity: O(n)
    /// </summary>
    public T NextHigher(T value)
    {
        var node = Find(value);
        if (node == null) return default;

        var next = node.NextHigher();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Descending enumerable.
    /// </summary>
    public IEnumerable<T> AsEnumerableDesc()
    {
        return GetEnumeratorDesc().AsEnumerable();
    }

    public IEnumerator<T> GetEnumeratorDesc()
    {
        return new BstEnumerator<T>(Root, false);
    }
}

internal class BstNode<T> : BstNodeBase<T> where T : IComparable
{
    internal BstNode(BstNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;
    }

    internal new BstNode<T> Parent
    {
        get => (BstNode<T>)base.Parent;
        set => base.Parent = value;
    }

    internal new BstNode<T> Left
    {
        get => (BstNode<T>)base.Left;
        set => base.Left = value;
    }

    internal new BstNode<T> Right
    {
        get => (BstNode<T>)base.Right;
        set => base.Right = value;
    }
}ParseOptions.0.json∏±
aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\BTree.csº∞using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A B-tree implementation.
/// </summary>
public class BTree<T> : IEnumerable<T> where T : IComparable
{
    private readonly int maxKeysPerNode;
    private readonly int minKeysPerNode;

    internal BTreeNode<T> Root;

    public BTree(int maxKeysPerNode)
    {
        if (maxKeysPerNode < 3) throw new Exception("Max keys per node should be atleast 3.");

        this.maxKeysPerNode = maxKeysPerNode;
        minKeysPerNode = maxKeysPerNode / 2;
    }

    public int Count { get; private set; }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Max
    {
        get
        {
            if (Root == null) return default;

            var maxNode = FindMaxNode(Root);
            return maxNode.Keys[maxNode.KeyCount - 1];
        }
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Min
    {
        get
        {
            if (Root == null) return default;

            var minNode = FindMinNode(Root);
            return minNode.Keys[0];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BTreeEnumerator<T>(Root);
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public bool HasItem(T value)
    {
        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Find the value node under given node.
    /// </summary>
    private BTreeNode<T> Find(BTreeNode<T> node, T value)
    {
        //if leaf then its time to insert
        if (node.IsLeaf)
        {
            for (var i = 0; i < node.KeyCount; i++)
                if (value.CompareTo(node.Keys[i]) == 0)
                    return node;
        }
        else
        {
            //if not leaf then drill down to leaf
            for (var i = 0; i < node.KeyCount; i++)
            {
                if (value.CompareTo(node.Keys[i]) == 0) return node;

                //current value is less than new value
                //drill down to left child of current value
                if (value.CompareTo(node.Keys[i]) < 0) return Find(node.Children[i], value);
                //current value is grearer than new value
                //and current value is last element 

                if (node.KeyCount == i + 1) return Find(node.Children[i + 1], value);
            }
        }

        return null;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Insert(T newValue)
    {
        if (Root == null)
        {
            Root = new BTreeNode<T>(maxKeysPerNode, null) { Keys = { [0] = newValue } };
            Root.KeyCount++;
            Count++;
            return;
        }

        var leafToInsert = FindInsertionLeaf(Root, newValue);
        InsertAndSplit(ref leafToInsert, newValue, null, null);
        Count++;
    }


    /// <summary>
    ///     Find the leaf node to start initial insertion
    /// </summary>
    private BTreeNode<T> FindInsertionLeaf(BTreeNode<T> node, T newValue)
    {
        //if leaf then its time to insert
        if (node.IsLeaf) return node;

        //if not leaf then drill down to leaf
        for (var i = 0; i < node.KeyCount; i++)
        {
            //current value is less than new value
            //drill down to left child of current value
            if (newValue.CompareTo(node.Keys[i]) < 0) return FindInsertionLeaf(node.Children[i], newValue);
            //current value is grearer than new value
            //and current value is last element 

            if (node.KeyCount == i + 1) return FindInsertionLeaf(node.Children[i + 1], newValue);
        }

        return node;
    }

    /// <summary>
    ///     Insert and split recursively up until no split is required
    /// </summary>
    private void InsertAndSplit(ref BTreeNode<T> node, T newValue,
        BTreeNode<T> newValueLeft, BTreeNode<T> newValueRight)
    {
        //add new item to current node
        if (node == null)
        {
            node = new BTreeNode<T>(maxKeysPerNode, null);
            Root = node;
        }

        //newValue have room to fit in this node
        //so just insert in right spot in asc order of keys
        if (node.KeyCount != maxKeysPerNode)
        {
            InsertToNotFullNode(ref node, newValue, newValueLeft, newValueRight);
            return;
        }

        //if node is full then split node
        //and  then insert new median to parent.

        //divide the current node values + new Node as left and right sub nodes
        var left = new BTreeNode<T>(maxKeysPerNode, null);
        var right = new BTreeNode<T>(maxKeysPerNode, null);

        //median of current Node
        var currentMedianIndex = node.GetMedianIndex();

        //init currentNode under consideration to left
        var currentNode = left;
        var currentNodeIndex = 0;

        //new Median also takes new Value in to Account
        var newMedian = default(T);
        var newMedianSet = false;
        var newValueInserted = false;

        //keep track of each insertion
        var insertionCount = 0;

        //insert newValue and existing values in sorted order
        //to left and right nodes
        //set new median during sorting
        for (var i = 0; i < node.KeyCount; i++)
        {
            //if insertion count reached new median
            //set the new median by picking the next smallest value
            if (!newMedianSet && insertionCount == currentMedianIndex)
            {
                newMedianSet = true;

                //median can be the new value or node.keys[i] (next node key)
                //whichever is smaller
                if (!newValueInserted && newValue.CompareTo(node.Keys[i]) < 0)
                {
                    //median is new value
                    newMedian = newValue;
                    newValueInserted = true;

                    if (newValueLeft != null) SetChild(currentNode, currentNode.KeyCount, newValueLeft);

                    //now fill right node
                    currentNode = right;
                    currentNodeIndex = 0;

                    if (newValueRight != null) SetChild(currentNode, 0, newValueRight);

                    i--;
                    insertionCount++;
                    continue;
                }

                //median is next node
                newMedian = node.Keys[i];

                //now fill right node
                currentNode = right;
                currentNodeIndex = 0;

                continue;
            }

            //pick the smaller among newValue and node.Keys[i]
            //and insert in to currentNode (left and right nodes)
            //if new Value was already inserted then just copy from node.Keys in sequence
            //since node.Keys is already in sorted order it should be fine
            if (newValueInserted || node.Keys[i].CompareTo(newValue) < 0)
            {
                currentNode.Keys[currentNodeIndex] = node.Keys[i];
                currentNode.KeyCount++;

                //if child is set don't set again
                //the child was already set by last newValueRight or last node
                if (currentNode.Children[currentNodeIndex] == null)
                    SetChild(currentNode, currentNodeIndex, node.Children[i]);

                SetChild(currentNode, currentNodeIndex + 1, node.Children[i + 1]);
            }
            else
            {
                currentNode.Keys[currentNodeIndex] = newValue;
                currentNode.KeyCount++;

                SetChild(currentNode, currentNodeIndex, newValueLeft);
                SetChild(currentNode, currentNodeIndex + 1, newValueRight);

                i--;
                newValueInserted = true;
            }

            currentNodeIndex++;
            insertionCount++;
        }

        //could be that thew newKey is the greatest 
        //so insert at end
        if (!newValueInserted)
        {
            currentNode.Keys[currentNodeIndex] = newValue;
            currentNode.KeyCount++;

            SetChild(currentNode, currentNodeIndex, newValueLeft);
            SetChild(currentNode, currentNodeIndex + 1, newValueRight);
        }

        //insert overflow element (newMedian) to parent
        var parent = node.Parent;
        InsertAndSplit(ref parent, newMedian, left, right);
    }

    /// <summary>
    ///     Insert to a node that is not full
    /// </summary>
    private void InsertToNotFullNode(ref BTreeNode<T> node, T newValue,
        BTreeNode<T> newValueLeft, BTreeNode<T> newValueRight)
    {
        var inserted = false;

        //insert in sorted order
        for (var i = 0; i < node.KeyCount; i++)
        {
            if (newValue.CompareTo(node.Keys[i]) >= 0) continue;

            InsertAt(node.Keys, i, newValue);
            node.KeyCount++;

            //Insert children if any
            SetChild(node, i, newValueLeft);
            InsertChild(node, i + 1, newValueRight);


            inserted = true;
            break;
        }

        //newValue is the greatest
        //element should be inserted at the end then
        if (inserted) return;

        node.Keys[node.KeyCount] = newValue;
        node.KeyCount++;

        SetChild(node, node.KeyCount - 1, newValueLeft);
        SetChild(node, node.KeyCount, newValueRight);
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Delete(T value)
    {
        var node = FindDeletionNode(Root, value);

        if (node == null) throw new Exception("Item do not exist in this tree.");

        for (var i = 0; i < node.KeyCount; i++)
        {
            if (value.CompareTo(node.Keys[i]) != 0) continue;

            //if node is leaf and no underflow
            //then just remove the node
            if (node.IsLeaf)
            {
                RemoveAt(node.Keys, i);
                node.KeyCount--;

                Balance(node);
            }
            else
            {
                //replace with max node of left tree
                var maxNode = FindMaxNode(node.Children[i]);
                node.Keys[i] = maxNode.Keys[maxNode.KeyCount - 1];

                RemoveAt(maxNode.Keys, maxNode.KeyCount - 1);
                maxNode.KeyCount--;

                Balance(maxNode);
            }

            Count--;
            return;
        }
    }

    /// <summary>
    ///     return the node containing max value which will be a leaf at the right most
    /// </summary>
    private BTreeNode<T> FindMinNode(BTreeNode<T> node)
    {
        //if leaf return node
        return node.IsLeaf ? node : FindMinNode(node.Children[0]);
    }

    /// <summary>
    ///     return the node containing max value which will be a leaf at the right most
    /// </summary>
    private BTreeNode<T> FindMaxNode(BTreeNode<T> node)
    {
        //if leaf return node
        return node.IsLeaf ? node : FindMaxNode(node.Children[node.KeyCount]);
    }

    /// <summary>
    ///     Balance a node which is short of Keys by rotations or merge
    /// </summary>
    private void Balance(BTreeNode<T> node)
    {
        if (node == Root || node.KeyCount >= minKeysPerNode) return;

        var rightSibling = GetRightSibling(node);

        if (rightSibling != null
            && rightSibling.KeyCount > minKeysPerNode)
        {
            LeftRotate(node, rightSibling);
            return;
        }

        var leftSibling = GetLeftSibling(node);

        if (leftSibling != null
            && leftSibling.KeyCount > minKeysPerNode)
        {
            RightRotate(leftSibling, node);
            return;
        }

        if (rightSibling != null)
            Sandwich(node, rightSibling);
        else
            Sandwich(leftSibling, node);
    }

    /// <summary>
    ///     merge two adjacent siblings to one node
    /// </summary>
    private void Sandwich(BTreeNode<T> leftSibling, BTreeNode<T> rightSibling)
    {
        var separatorIndex = GetNextSeparatorIndex(leftSibling);
        var parent = leftSibling.Parent;

        var newNode = new BTreeNode<T>(maxKeysPerNode, leftSibling.Parent);
        var newIndex = 0;

        for (var i = 0; i < leftSibling.KeyCount; i++)
        {
            newNode.Keys[newIndex] = leftSibling.Keys[i];

            if (leftSibling.Children[i] != null) SetChild(newNode, newIndex, leftSibling.Children[i]);

            if (leftSibling.Children[i + 1] != null) SetChild(newNode, newIndex + 1, leftSibling.Children[i + 1]);

            newIndex++;
        }

        //special case when left sibling is empty 
        if (leftSibling.KeyCount == 0 && leftSibling.Children[0] != null)
            SetChild(newNode, newIndex, leftSibling.Children[0]);

        newNode.Keys[newIndex] = parent.Keys[separatorIndex];
        newIndex++;

        for (var i = 0; i < rightSibling.KeyCount; i++)
        {
            newNode.Keys[newIndex] = rightSibling.Keys[i];

            if (rightSibling.Children[i] != null) SetChild(newNode, newIndex, rightSibling.Children[i]);

            if (rightSibling.Children[i + 1] != null) SetChild(newNode, newIndex + 1, rightSibling.Children[i + 1]);

            newIndex++;
        }

        //special case when left sibling is empty 
        if (rightSibling.KeyCount == 0 && rightSibling.Children[0] != null)
            SetChild(newNode, newIndex, rightSibling.Children[0]);

        newNode.KeyCount = newIndex;
        SetChild(parent, separatorIndex, newNode);
        RemoveAt(parent.Keys, separatorIndex);
        parent.KeyCount--;

        RemoveChild(parent, separatorIndex + 1);


        if (parent.KeyCount == 0
            && parent == Root)
        {
            Root = newNode;
            Root.Parent = null;

            if (Root.KeyCount == 0) Root = null;

            return;
        }

        if (parent.KeyCount < minKeysPerNode) Balance(parent);
    }

    /// <summary>
    ///     do a right rotation
    /// </summary>
    private void RightRotate(BTreeNode<T> leftSibling, BTreeNode<T> rightSibling)
    {
        var parentIndex = GetNextSeparatorIndex(leftSibling);

        InsertAt(rightSibling.Keys, 0, rightSibling.Parent.Keys[parentIndex]);
        rightSibling.KeyCount++;

        InsertChild(rightSibling, 0, leftSibling.Children[leftSibling.KeyCount]);

        rightSibling.Parent.Keys[parentIndex] = leftSibling.Keys[leftSibling.KeyCount - 1];

        RemoveAt(leftSibling.Keys, leftSibling.KeyCount - 1);
        leftSibling.KeyCount--;

        RemoveChild(leftSibling, leftSibling.KeyCount + 1);
    }

    /// <summary>
    ///     do a left rotation
    /// </summary>
    private void LeftRotate(BTreeNode<T> leftSibling, BTreeNode<T> rightSibling)
    {
        var parentIndex = GetNextSeparatorIndex(leftSibling);
        leftSibling.Keys[leftSibling.KeyCount] = leftSibling.Parent.Keys[parentIndex];
        leftSibling.KeyCount++;

        SetChild(leftSibling, leftSibling.KeyCount, rightSibling.Children[0]);


        leftSibling.Parent.Keys[parentIndex] = rightSibling.Keys[0];

        RemoveAt(rightSibling.Keys, 0);
        rightSibling.KeyCount--;

        RemoveChild(rightSibling, 0);
    }

    /// <summary>
    ///     Locate the node in which the item to delete exist
    /// </summary>
    private BTreeNode<T> FindDeletionNode(BTreeNode<T> node, T value)
    {
        //if leaf then its time to insert
        if (node.IsLeaf)
        {
            for (var i = 0; i < node.KeyCount; i++)
                if (value.CompareTo(node.Keys[i]) == 0)
                    return node;
        }
        else
        {
            //if not leaf then drill down to leaf
            for (var i = 0; i < node.KeyCount; i++)
            {
                if (value.CompareTo(node.Keys[i]) == 0) return node;

                //current value is less than new value
                //drill down to left child of current value
                if (value.CompareTo(node.Keys[i]) < 0) return FindDeletionNode(node.Children[i], value);
                //current value is grearer than new value
                //and current value is last element 

                if (node.KeyCount == i + 1) return FindDeletionNode(node.Children[i + 1], value);
            }
        }

        return null;
    }

    /// <summary>
    ///     Get next key separator index after this child Node in parent
    /// </summary>
    private int GetNextSeparatorIndex(BTreeNode<T> node)
    {
        var parent = node.Parent;

        if (node.Index == 0) return 0;

        if (node.Index == parent.KeyCount) return node.Index - 1;

        return node.Index;
    }

    /// <summary>
    ///     get the right sibling node
    /// </summary>
    private BTreeNode<T> GetRightSibling(BTreeNode<T> node)
    {
        var parent = node.Parent;

        return node.Index == parent.KeyCount ? null : parent.Children[node.Index + 1];
    }

    /// <summary>
    ///     get left sibling node
    /// </summary>
    private BTreeNode<T> GetLeftSibling(BTreeNode<T> node)
    {
        return node.Index == 0 ? null : node.Parent.Children[node.Index - 1];
    }

    private void SetChild(BTreeNode<T> parent, int childIndex, BTreeNode<T> child)
    {
        parent.Children[childIndex] = child;

        if (child == null) return;

        child.Parent = parent;
        child.Index = childIndex;
    }

    private void InsertChild(BTreeNode<T> parent, int childIndex, BTreeNode<T> child)
    {
        InsertAt(parent.Children, childIndex, child);

        if (child != null) child.Parent = parent;

        //update indices
        for (var i = childIndex; i <= parent.KeyCount; i++)
            if (parent.Children[i] != null)
                parent.Children[i].Index = i;
    }

    private void RemoveChild(BTreeNode<T> parent, int childIndex)
    {
        RemoveAt(parent.Children, childIndex);

        //update indices
        for (var i = childIndex; i <= parent.KeyCount; i++)
            if (parent.Children[i] != null)
                parent.Children[i].Index = i;
    }

    /// <summary>
    ///     Shift array right at index to make room for new insertion
    ///     And then insert at index
    ///     Assumes array have atleast one empty index at end
    /// </summary>
    private void InsertAt<TS>(TS[] array, int index, TS newValue)
    {
        //shift elements right by one indice from index
        Array.Copy(array, index, array, index + 1, array.Length - index - 1);
        //now set the value
        array[index] = newValue;
    }

    /// <summary>
    ///     Shift array left at index
    /// </summary>
    private void RemoveAt<TS>(TS[] array, int index)
    {
        //shift elements right by one indice from index
        Array.Copy(array, index + 1, array, index, array.Length - index - 1);
    }
}

/// <summary>
///     abstract node shared by both B and B+ tree nodes
///     so that we can use this for common tests across B and B+ tree
/// </summary>
internal abstract class BNode<T> where T : IComparable
{
    /// <summary>
    ///     Array Index of this node in parent's Children array
    /// </summary>
    internal int Index;

    internal int KeyCount;

    internal BNode(int maxKeysPerNode)
    {
        Keys = new T[maxKeysPerNode];
    }

    internal T[] Keys { get; set; }

    //for common unit testing across B and B+ tree
    internal abstract BNode<T> GetParent();
    internal abstract BNode<T>[] GetChildren();

    internal int GetMedianIndex()
    {
        return KeyCount / 2 + 1;
    }
}

internal class BTreeNode<T> : BNode<T> where T : IComparable
{
    internal BTreeNode(int maxKeysPerNode, BTreeNode<T> parent)
        : base(maxKeysPerNode)
    {
        Parent = parent;
        Children = new BTreeNode<T>[maxKeysPerNode + 1];
    }

    internal BTreeNode<T> Parent { get; set; }
    internal BTreeNode<T>[] Children { get; set; }

    internal bool IsLeaf => Children[0] == null;

    /// <summary>
    ///     For shared test method accross B and B+ tree
    /// </summary>
    internal override BNode<T> GetParent()
    {
        return Parent;
    }

    /// <summary>
    ///     For shared test method accross B and B+ tree
    /// </summary>
    internal override BNode<T>[] GetChildren()
    {
        return Children;
    }
}

internal class BTreeEnumerator<T> : IEnumerator<T> where T : IComparable
{
    private readonly BTreeNode<T> root;

    private BTreeNode<T> current;
    private int index;
    private Stack<BTreeNode<T>> progress;

    internal BTreeEnumerator(BTreeNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null)
        {
            current = root;
            progress = new Stack<BTreeNode<T>>(root.Children.Take(root.KeyCount + 1).Where(x => x != null));
            return current.KeyCount > 0;
        }

        if (current != null && index + 1 < current.KeyCount)
        {
            index++;
            return true;
        }

        if (progress.Count > 0)
        {
            index = 0;

            current = progress.Pop();

            foreach (var child in current.Children.Take(current.KeyCount + 1).Where(x => x != null))
                progress.Push(child);

            return true;
        }

        return false;
    }

    public void Reset()
    {
        progress = null;
        current = null;
        index = 0;
    }

    object IEnumerator.Current => Current;

    public T Current => current.Keys[index];

    public void Dispose()
    {
        progress = null;
    }
}ParseOptions.0.jsonå
gD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\FenwickTree.csãusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A Fenwick Tree (binary indexed tree) implementation for prefix sum.
/// </summary>
public class FenwickTree<T> : IEnumerable<T>
{
    private readonly T[] input;

    /// <summary>
    ///     Add operation on generic type.
    /// </summary>
    private readonly Func<T, T, T> sumOperation;

    private T[] tree;

    /// <summary>
    ///     constructs a Fenwick tree using the specified sum operation function.
    ///     Time complexity: O(nLog(n)).
    /// </summary>
    public FenwickTree(T[] input, Func<T, T, T> sumOperation)
    {
        if (input == null || sumOperation == null) throw new ArgumentNullException();

        this.input = input.Clone() as T[];

        this.sumOperation = sumOperation;
        Construct(input);
    }

    private int Length => tree.Length - 1;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return input.Select(x => x).GetEnumerator();
    }

    /// <summary>
    ///     Construct Fenwick tree from input array.
    /// </summary>
    private void Construct(T[] input)
    {
        tree = new T[input.Length + 1];

        for (var i = 0; i < input.Length; i++)
        {
            var j = i + 1;
            while (j < input.Length)
            {
                tree[j] = sumOperation(tree[j], input[i]);
                j = GetNextIndex(j);
            }
        }
    }

    /// <summary>
    ///     Gets the prefix sum from 0 till the given end index.
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T PrefixSum(int endIndex)
    {
        if (endIndex < 0 || endIndex > Length - 1) throw new ArgumentException();

        var sum = default(T);

        var currentIndex = endIndex + 1;

        while (currentIndex > 0)
        {
            sum = sumOperation(sum, tree[currentIndex]);
            currentIndex = GetParentIndex(currentIndex);
        }

        return sum;
    }

    /// <summary>
    ///     Get index of next sibling .
    /// </summary>
    private int GetNextIndex(int currentIndex)
    {
        //add current index with
        //twos complimant of currentIndex AND with currentIndex
        return currentIndex + (currentIndex & -currentIndex);
    }

    /// <summary>
    ///     Get parent node index.
    /// </summary>
    private int GetParentIndex(int currentIndex)
    {
        //substract current index with
        //twos complimant of currentIndex AND with currentIndex
        return currentIndex - (currentIndex & -currentIndex);
    }
}ParseOptions.0.json°ç
hD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\IntervalTree.csûåusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A multi-dimensional interval tree implementation.
/// </summary>
public class IntervalTree<T> : IEnumerable<Tuple<T[], T[]>> where T : IComparable
{
    /// <summary>
    ///     A cached function to override default(T)
    ///     so that for value types we can return min value as default.
    /// </summary>
    private readonly Lazy<T> defaultValue = new(() =>
    {
        var s = typeof(T);

        var isValueType = s.GetTypeInfo().IsValueType;

        if (isValueType) return (T)Convert.ChangeType(int.MinValue, s);

        return default;
    });

    private readonly int dimensions;
    private readonly OneDimentionalIntervalTree<T> tree;
    private readonly HashSet<Tuple<T[], T[]>> items = new(new IntervalComparer<T>());


    public IntervalTree(int dimension)
    {
        if (dimension <= 0) throw new Exception("Dimension should be greater than 0.");

        dimensions = dimension;
        tree = new OneDimentionalIntervalTree<T>(defaultValue);
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Tuple<T[], T[]>> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    /// <summary>
    ///     Add a new interval to this interval tree.
    ///     Time complexity : O(d(log(n) + m)) where d is dimensions and
    ///     m is the number of intervals that overlaps with this inserted interval.
    /// </summary>
    public void Insert(T[] start, T[] end)
    {
        ValidateDimensions(start, end);

        if (items.Contains(new Tuple<T[], T[]>(start, end))) throw new Exception("Inteval exists.");

        var currentTrees = new List<OneDimentionalIntervalTree<T>> { tree };

        for (var i = 0; i < dimensions; i++)
        {
            var allOverlaps = new List<OneDimentionalIntervalTree<T>>();

            foreach (var tree in currentTrees)
            {
                //insert in current dimension
                tree.Insert(new OneDimentionalInterval<T>(start[i], end[i], defaultValue));

                //get all overlaps
                //and insert next dimension value to each overlapping node
                var overlaps = tree.GetOverlaps(new OneDimentionalInterval<T>(start[i], end[i], defaultValue));
                foreach (var overlap in overlaps) allOverlaps.Add(overlap.NextDimensionIntervals);
            }

            currentTrees = allOverlaps;
        }

        items.Add(new Tuple<T[], T[]>(start, end));

        Count++;
    }

    /// <summary>
    ///     Delete this interval from this interval tree.
    ///     Time complexity :  O(d(log(n) + m)) where d is dimensions and
    ///     m is the number of intervals that overlap with this deleted interval.
    /// </summary>
    public void Delete(T[] start, T[] end)
    {
        if (!items.Contains(new Tuple<T[], T[]>(start, end))) throw new Exception("Inteval does'nt exist.");

        ValidateDimensions(start, end);

        var allOverlaps = new List<OneDimentionalIntervalTree<T>>();
        var overlaps = tree.GetOverlaps(new OneDimentionalInterval<T>(start[0], end[0], defaultValue));

        foreach (var overlap in overlaps) allOverlaps.Add(overlap.NextDimensionIntervals);

        DeleteOverlaps(allOverlaps, start, end, 1);
        tree.Delete(new OneDimentionalInterval<T>(start[0], end[0], defaultValue));

        items.Remove(new Tuple<T[], T[]>(start, end));
        Count--;
    }

    /// <summary>
    ///     Recursively delete values from overlaps in next dimension.
    /// </summary>
    private void DeleteOverlaps(List<OneDimentionalIntervalTree<T>> currentTrees, T[] start, T[] end, int index)
    {
        //base case
        if (index == start.Length)
            return;

        var allOverlaps = new List<OneDimentionalIntervalTree<T>>();

        foreach (var tree in currentTrees)
        {
            var overlaps = tree.GetOverlaps(new OneDimentionalInterval<T>(start[index], end[index], defaultValue));

            foreach (var overlap in overlaps) allOverlaps.Add(overlap.NextDimensionIntervals);
        }

        //dig in to next dimension to 
        DeleteOverlaps(allOverlaps, start, end, ++index);

        index--;

        //now delete
        foreach (var tree in allOverlaps)
            if (tree.Count > 0)
                tree.Delete(new OneDimentionalInterval<T>(start[index], end[index], defaultValue));
    }

    /// <summary>
    ///     Does this interval overlap with any interval in this interval tree?
    /// </summary>
    public bool DoOverlap(T[] start, T[] end)
    {
        ValidateDimensions(start, end);

        var allOverlaps = GetOverlaps(tree, start, end, 0);

        return allOverlaps.Count > 0;
    }

    /// <summary>
    ///     returns a list of matching intervals.
    ///     Time complexity : O(d(log(n) + m)) where d is dimensions and
    ///     m is the number of overlaps.
    /// </summary>
    public List<Tuple<T[], T[]>> GetOverlaps(T[] start, T[] end)
    {
        return GetOverlaps(tree, start, end, 0);
    }

    /// <summary>
    ///     Does this interval overlap with any interval in this interval tree?
    /// </summary>
    private List<Tuple<T[], T[]>> GetOverlaps(OneDimentionalIntervalTree<T> currentTree,
        T[] start, T[] end, int dimension)
    {
        var nodes = currentTree.GetOverlaps(new OneDimentionalInterval<T>(start[dimension], end[dimension],
            defaultValue));

        if (dimension + 1 == dimensions)
        {
            var result = new List<Tuple<T[], T[]>>();

            foreach (var node in nodes)
            {
                var fStart = new T[dimensions];
                var fEnd = new T[dimensions];

                fStart[dimension] = node.Start;
                fEnd[dimension] = node.End[node.MatchingEndIndex];

                var thisDimResult = new Tuple<T[], T[]>(fStart, fEnd);

                result.Add(thisDimResult);
            }

            return result;
        }
        else
        {
            var result = new List<Tuple<T[], T[]>>();

            foreach (var node in nodes)
            {
                var nextDimResult = GetOverlaps(node.NextDimensionIntervals, start, end, dimension + 1);

                foreach (var nextResult in nextDimResult)
                {
                    nextResult.Item1[dimension] = node.Start;
                    nextResult.Item2[dimension] = node.End[node.MatchingEndIndex];

                    result.Add(nextResult);
                }
            }

            return result;
        }
    }

    /// <summary>
    ///     validate dimensions for point length.
    /// </summary>
    private void ValidateDimensions(T[] start, T[] end)
    {
        if (start == null) throw new ArgumentNullException(nameof(start));

        if (end == null) throw new ArgumentNullException(nameof(end));

        if (start.Length != dimensions || start.Length != end.Length)
            throw new Exception($"Expecting {dimensions} points in start and end values for this interval.");

        if (start.Where((t, i) => t.Equals(defaultValue.Value)
                                  || end[i].Equals(defaultValue.Value)).Any())
            throw new Exception("Points cannot contain Minimum Value or Null values");
    }
}

/// <summary>
///     An interval tree implementation in one dimension using augmentation tree method.
/// </summary>
internal class OneDimentionalIntervalTree<T> where T : IComparable
{
    /// <summary>
    ///     A cached function to override default(T)
    ///     so that for value types we can return min value as default.
    /// </summary>
    private readonly Lazy<T> defaultValue;

    //use a height balanced binary search tree
    private readonly RedBlackTree<OneDimentionalInterval<T>> redBlackTree = new();

    internal OneDimentionalIntervalTree(Lazy<T> defaultValue)
    {
        this.defaultValue = defaultValue;
    }

    internal int Count { get; private set; }

    /// <summary>
    ///     Insert a new Interval.
    /// </summary>
    internal void Insert(OneDimentionalInterval<T> newInterval)
    {
        SortInterval(newInterval);
        var existing = redBlackTree.FindNode(newInterval);
        if (existing != null)
            existing.Value.End.Add(newInterval.End[0]);
        else
            existing = redBlackTree.InsertAndReturnNode(newInterval).Item1;
        UpdateMax(existing);
        Count++;
    }

    /// <summary>
    ///     Delete this interval
    /// </summary>
    internal void Delete(OneDimentionalInterval<T> interval)
    {
        SortInterval(interval);

        var existing = redBlackTree.FindNode(interval);
        if (existing != null && existing.Value.End.Count > 1)
        {
            existing.Value.End.RemoveAt(existing.Value.End.Count - 1);
            UpdateMax(existing);
        }
        else if (existing != null)
        {
            redBlackTree.Delete(interval);
            UpdateMax(existing.Parent);
        }
        else
        {
            throw new Exception("Interval not found in this interval tree.");
        }

        Count--;
    }

    /// <summary>
    ///     Returns an interval in this tree that overlaps with this search interval
    /// </summary>
    internal OneDimentionalInterval<T> GetOverlap(OneDimentionalInterval<T> searchInterval)
    {
        SortInterval(searchInterval);
        return GetOverlap(redBlackTree.Root, searchInterval);
    }

    /// <summary>
    ///     Returns an interval in this tree that overlaps with this search interval.
    /// </summary>
    internal List<OneDimentionalInterval<T>> GetOverlaps(OneDimentionalInterval<T> searchInterval)
    {
        SortInterval(searchInterval);
        return GetOverlaps(redBlackTree.Root, searchInterval);
    }

    /// <summary>
    ///     Does any interval overlaps with this search interval.
    /// </summary>
    internal bool DoOverlap(OneDimentionalInterval<T> searchInterval)
    {
        SortInterval(searchInterval);
        return GetOverlap(redBlackTree.Root, searchInterval) != null;
    }

    /// <summary>
    ///     Swap intervals so that start always appear before end.
    /// </summary>
    private void SortInterval(OneDimentionalInterval<T> value)
    {
        if (value.Start.CompareTo(value.End[0]) <= 0) return;

        var tmp = value.End[0];
        value.End[0] = value.Start;
        value.Start = tmp;
    }

    /// <summary>
    ///     Returns an interval that overlaps with this interval
    /// </summary>
    private OneDimentionalInterval<T> GetOverlap(RedBlackTreeNode<OneDimentionalInterval<T>> current,
        OneDimentionalInterval<T> searchInterval)
    {
        while (true)
        {
            if (current == null) return null;

            if (DoOverlap(current.Value, searchInterval)) return current.Value;

            //if left max is greater than search start
            //then the search interval can occur in left sub tree
            if (current.Left != null && current.Left.Value.MaxEnd.CompareTo(searchInterval.Start) >= 0)
            {
                current = current.Left;
                continue;
            }

            //otherwise look in right subtree
            current = current.Right;
        }
    }

    /// <summary>
    ///     Returns all intervals that overlaps with this interval.
    /// </summary>
    private List<OneDimentionalInterval<T>> GetOverlaps(RedBlackTreeNode<OneDimentionalInterval<T>> current,
        OneDimentionalInterval<T> searchInterval, List<OneDimentionalInterval<T>> result = null)
    {
        if (result == null) result = new List<OneDimentionalInterval<T>>();

        if (current == null) return result;

        if (DoOverlap(current.Value, searchInterval)) result.Add(current.Value);

        //if left max is greater than search start
        //then the search interval can occur in left sub tree
        if (current.Left != null
            && current.Left.Value.MaxEnd.CompareTo(searchInterval.Start) >= 0)
            GetOverlaps(current.Left, searchInterval, result);

        //otherwise look in right subtree
        GetOverlaps(current.Right, searchInterval, result);

        return result;
    }

    /// <summary>
    ///     Does this interval a overlap with b.
    /// </summary>
    private bool DoOverlap(OneDimentionalInterval<T> a, OneDimentionalInterval<T> b)
    {
        //lazy reset
        a.MatchingEndIndex = -1;
        b.MatchingEndIndex = -1;

        for (var i = 0; i < a.End.Count; i++)
        for (var j = 0; j < b.End.Count; j++)
        {
            //a.Start less than b.End and a.End greater than b.Start
            if (a.Start.CompareTo(b.End[j]) > 0 || a.End[i].CompareTo(b.Start) < 0) continue;

            a.MatchingEndIndex = i;
            b.MatchingEndIndex = j;

            return true;
        }

        return false;
    }

    /// <summary>
    ///     update max end value under each node in red-black tree recursively.
    /// </summary>
    private void UpdateMax(RedBlackTreeNode<OneDimentionalInterval<T>> node, T currentMax, bool recurseUp = true)
    {
        while (true)
        {
            if (node == null) return;

            if (node.Left != null && node.Right != null)
            {
                //if current Max is less than current End
                //then update current Max
                if (currentMax.CompareTo(node.Left.Value.MaxEnd) < 0) currentMax = node.Left.Value.MaxEnd;

                if (currentMax.CompareTo(node.Right.Value.MaxEnd) < 0) currentMax = node.Right.Value.MaxEnd;
            }
            else if (node.Left != null)
            {
                //if current Max is less than current End
                //then update current Max
                if (currentMax.CompareTo(node.Left.Value.MaxEnd) < 0) currentMax = node.Left.Value.MaxEnd;
            }
            else if (node.Right != null)
            {
                if (currentMax.CompareTo(node.Right.Value.MaxEnd) < 0) currentMax = node.Right.Value.MaxEnd;
            }

            foreach (var v in node.Value.End)
                if (currentMax.CompareTo(v) < 0)
                    currentMax = v;

            node.Value.MaxEnd = currentMax;


            if (recurseUp)
            {
                node = node.Parent;
                continue;
            }


            break;
        }
    }

    /// <summary>
    ///     Update Max recursively up each node in red-black tree.
    /// </summary>
    private void UpdateMax(RedBlackTreeNode<OneDimentionalInterval<T>> newRoot, bool recurseUp = true)
    {
        if (newRoot == null)
            return;

        newRoot.Value.MaxEnd = defaultValue.Value;

        if (newRoot.Left != null)
        {
            newRoot.Left.Value.MaxEnd = defaultValue.Value;
            UpdateMax(newRoot.Left, newRoot.Left.Value.MaxEnd, recurseUp);
        }

        if (newRoot.Right != null)
        {
            newRoot.Right.Value.MaxEnd = defaultValue.Value;
            UpdateMax(newRoot.Right, newRoot.Right.Value.MaxEnd, recurseUp);
        }

        UpdateMax(newRoot, newRoot.Value.MaxEnd, recurseUp);
    }
}

/// <summary>
///     One dimensional interval.
/// </summary>
internal class OneDimentionalInterval<T> : IComparable where T : IComparable
{
    public OneDimentionalInterval(T start, T end, Lazy<T> defaultValue)
    {
        Start = start;
        End = new List<T> { end };
        NextDimensionIntervals = new OneDimentionalIntervalTree<T>(defaultValue);
    }

    /// <summary>
    ///     Start of this interval range.
    /// </summary>
    public T Start { get; set; }

    /// <summary>
    ///     End of this interval range.
    /// </summary>
    public List<T> End { get; set; }

    /// <summary>
    ///     Max End interval under this interval.
    /// </summary>
    internal T MaxEnd { get; set; }

    /// <summary>
    ///     Holds intervals for the next dimension.
    /// </summary>
    internal OneDimentionalIntervalTree<T> NextDimensionIntervals { get; set; }

    /// <summary>
    ///     Mark the matching end index during overlap search
    ///     so that we can return the overlapping interval.
    /// </summary>
    internal int MatchingEndIndex { get; set; }

    public int CompareTo(object obj)
    {
        return Start.CompareTo(((OneDimentionalInterval<T>)obj).Start);
    }
}

/// <summary>
///     Compares two intervals.
/// </summary>
internal class IntervalComparer<T> : IEqualityComparer<Tuple<T[], T[]>> where T : IComparable
{
    public bool Equals(Tuple<T[], T[]> x, Tuple<T[], T[]> y)
    {
        if (x == y) return true;

        for (var i = 0; i < x.Item1.Length; i++)
        {
            if (!x.Item1[i].Equals(y.Item1[i])) return false;

            if (!x.Item2[i].Equals(y.Item2[i])) return false;
        }

        return true;
    }

    public int GetHashCode(Tuple<T[], T[]> x)
    {
        unchecked
        {
            if (x == null) return 0;
            var hash = 17;
            foreach (var element in x.Item1) hash = hash * 31 + element.GetHashCode();
            foreach (var element in x.Item2) hash = hash * 31 + element.GetHashCode();
            return hash;
        }
    }
}ParseOptions.0.json⁄x
cD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\K_DTree.cs›wusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A multiDimensional k-d tree implementation (Unbalanced).
/// </summary>
public class KdTree<T> : IEnumerable<T[]> where T : IComparable
{
    private readonly int dimensions;
    private KdTreeNode<T> root;

    public KdTree(int dimensions)
    {
        this.dimensions = dimensions;
        if (dimensions <= 0) throw new Exception("Dimension should be greater than 0.");
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T[]> GetEnumerator()
    {
        return new KdTreeEnumerator<T>(root);
    }

    /// <summary>
    ///     Inserts a new item to this Kd tree.
    ///     Time complexity: O(log(n))
    /// </summary>
    public void Insert(T[] point)
    {
        if (root == null)
        {
            root = new KdTreeNode<T>(dimensions, null);
            root.Points = new T[dimensions];
            CopyPoints(root.Points, point);
            Count++;
            return;
        }

        Insert(root, point, 0);
        Count++;
    }

    /// <summary>
    ///     Recursively find leaf node to insert
    ///     at each level comparing against the next dimension.
    /// </summary>
    private void Insert(KdTreeNode<T> currentNode, T[] point, int depth)
    {
        var currentDimension = depth % dimensions;

        if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
        {
            if (currentNode.Left == null)
            {
                currentNode.Left = new KdTreeNode<T>(dimensions, currentNode);
                currentNode.Left.Points = new T[dimensions];
                CopyPoints(currentNode.Left.Points, point);
                return;
            }

            Insert(currentNode.Left, point, depth + 1);
        }
        else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
        {
            if (currentNode.Right == null)
            {
                currentNode.Right = new KdTreeNode<T>(dimensions, currentNode);
                currentNode.Right.Points = new T[dimensions];
                CopyPoints(currentNode.Right.Points, point);
                return;
            }

            Insert(currentNode.Right, point, depth + 1);
        }
    }

    /// <summary>
    ///     Delete point.
    ///     Time complexity: O(log(n))
    /// </summary>
    public void Delete(T[] point)
    {
        if (root == null) throw new Exception("Empty tree");

        Delete(root, point, 0);
        Count--;
    }

    /// <summary>
    ///     Delete point by locating it recursively.
    /// </summary>
    private void Delete(KdTreeNode<T> currentNode, T[] point, int depth)
    {
        if (currentNode == null) throw new Exception("Given deletion point do not exist in this kd tree.");

        var currentDimension = depth % dimensions;

        if (DoMatch(currentNode.Points, point))
        {
            HandleDeleteCases(currentNode, point, depth);
            return;
        }

        if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            Delete(currentNode.Left, point, depth + 1);
        else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            Delete(currentNode.Right, point, depth + 1);
    }

    /// <summary>
    ///     Handle the three cases for deletion.
    /// </summary>
    private void HandleDeleteCases(KdTreeNode<T> currentNode, T[] point, int depth)
    {
        //case one node is leaf
        if (currentNode.IsLeaf)
        {
            if (currentNode == root)
            {
                root = null;
            }
            else
            {
                if (currentNode.IsLeftChild)
                    currentNode.Parent.Left = null;
                else
                    currentNode.Parent.Right = null;

                return;
            }
        }

        //case 2 right subtree is not null
        if (currentNode.Right != null)
        {
            var minNode = FindMin(currentNode.Right, depth % dimensions, depth + 1);
            CopyPoints(currentNode.Points, minNode.Points);

            Delete(currentNode.Right, minNode.Points, depth + 1);
        }
        //case 3 left subtree is not null
        else if (currentNode.Left != null)
        {
            var minNode = FindMin(currentNode.Left, depth % dimensions, depth + 1);
            CopyPoints(currentNode.Points, minNode.Points);

            Delete(currentNode.Left, minNode.Points, depth + 1);

            //now move to right
            currentNode.Right = currentNode.Left;
            currentNode.Left = null;
        }
    }

    /// <summary>
    ///     Copy points2 to point1.
    /// </summary>
    private void CopyPoints(T[] points1, T[] points2)
    {
        for (var i = 0; i < points1.Length; i++) points1[i] = points2[i];
    }

    /// <summary>
    ///     Find min value under this dimension.
    /// </summary>
    private KdTreeNode<T> FindMin(KdTreeNode<T> node, int searchdimension, int depth)
    {
        var currentDimension = depth % dimensions;

        if (currentDimension == searchdimension)
        {
            if (node.Left == null) return node;

            return FindMin(node.Left, searchdimension, depth + 1);
        }

        KdTreeNode<T> leftMin = null;
        if (node.Left != null) leftMin = FindMin(node.Left, searchdimension, depth + 1);

        KdTreeNode<T> rightMin = null;
        if (node.Right != null) rightMin = FindMin(node.Right, searchdimension, depth + 1);

        return Min(node, leftMin, rightMin, searchdimension);
    }

    /// <summary>
    ///     Returns min of given three nodes on search dimension.
    /// </summary>
    private KdTreeNode<T> Min(KdTreeNode<T> node,
        KdTreeNode<T> leftMin, KdTreeNode<T> rightMin,
        int searchdimension)
    {
        var min = node;

        if (leftMin != null && min.Points[searchdimension]
                .CompareTo(leftMin.Points[searchdimension]) > 0)
            min = leftMin;

        if (rightMin != null && min.Points[searchdimension]
                .CompareTo(rightMin.Points[searchdimension]) > 0)
            min = rightMin;

        return min;
    }

    /// <summary>
    ///     Are these two points matching.
    /// </summary>
    private bool DoMatch(T[] a, T[] b)
    {
        for (var i = 0; i < a.Length; i++)
            if (a[i].CompareTo(b[i]) != 0)
                return false;

        return true;
    }

    /// <summary>
    ///     Returns the nearest neigbour to point.
    ///     Time complexity: O(log(n))
    /// </summary>
    public T[] NearestNeighbour(IDistanceCalculator<T> distanceCalculator, T[] point)
    {
        if (root == null) throw new Exception("Empty tree");

        return FindNearestNeighbour(root, point, 0, distanceCalculator).Points;
    }

    /// <summary>
    ///     Recursively find leaf node to insert
    ///     at each level comparing against the next dimension.
    /// </summary>
    private KdTreeNode<T> FindNearestNeighbour(KdTreeNode<T> currentNode,
        T[] searchPoint, int depth,
        IDistanceCalculator<T> distanceCalculator)
    {
        var currentDimension = depth % dimensions;
        KdTreeNode<T> currentBest = null;

        var compareResult = searchPoint[currentDimension]
            .CompareTo(currentNode.Points[currentDimension]);

        //move toward search point until leaf is reached
        if (compareResult < 0)
        {
            if (currentNode.Left != null)
                currentBest = FindNearestNeighbour(currentNode.Left,
                    searchPoint, depth + 1, distanceCalculator);
            else
                currentBest = currentNode;

            //currentBest is greater than point to current node distance
            //or if right node sits on split plane
            //then also move left
            if (currentNode.Right != null &&
                (distanceCalculator.Compare(currentNode.Points[currentDimension], searchPoint[currentDimension],
                     searchPoint, currentBest.Points) < 0
                 || currentNode.Right.Points[currentDimension]
                     .CompareTo(currentNode.Points[currentDimension]) == 0))
            {
                var rightBest = FindNearestNeighbour(currentNode.Right,
                    searchPoint, depth + 1,
                    distanceCalculator);

                currentBest = GetClosestToPoint(distanceCalculator, currentBest, rightBest, searchPoint);
            }

            //now recurse up from leaf updating current Best
            currentBest = GetClosestToPoint(distanceCalculator, currentBest, currentNode, searchPoint);
        }
        else if (compareResult >= 0)
        {
            if (currentNode.Right != null)
                currentBest = FindNearestNeighbour(currentNode.Right,
                    searchPoint, depth + 1, distanceCalculator);
            else
                currentBest = currentNode;

            //currentBest is greater than point to current node distance
            //or if search point lies on split plane
            //then also move left
            if (currentNode.Left != null
                && (distanceCalculator.Compare(currentNode.Points[currentDimension], searchPoint[currentDimension],
                    searchPoint, currentBest.Points) < 0 || compareResult == 0))
            {
                var leftBest = FindNearestNeighbour(currentNode.Left,
                    searchPoint, depth + 1,
                    distanceCalculator);

                currentBest = GetClosestToPoint(distanceCalculator, currentBest, leftBest, searchPoint);
            }

            //now recurse up from leaf updating current Best
            currentBest = GetClosestToPoint(distanceCalculator, currentBest, currentNode, searchPoint);
        }


        return currentBest;
    }

    /// <summary>
    ///     Returns the closest node between currentBest and CurrentNode to point
    /// </summary>
    private KdTreeNode<T> GetClosestToPoint(IDistanceCalculator<T> distanceCalculator,
        KdTreeNode<T> currentBest, KdTreeNode<T> currentNode, T[] point)
    {
        if (distanceCalculator.Compare(currentBest.Points,
                currentNode.Points, point) < 0)
            return currentBest;

        return currentNode;
    }

    /// <summary>
    ///     Returns a list of nodes that are withing the given area
    ///     start and end ranges
    /// </summary>
    public List<T[]> RangeSearch(T[] start, T[] end)
    {
        var result = RangeSearch(new List<T[]>(), root,
            start, end, 0);

        return result;
    }

    /// <summary>
    ///     Recursively find points in given range.
    /// </summary>
    private List<T[]> RangeSearch(List<T[]> result,
        KdTreeNode<T> currentNode,
        T[] start, T[] end, int depth)
    {
        if (currentNode == null) return result;

        var currentDimension = depth % dimensions;

        if (currentNode.IsLeaf)
        {
            //start is less than current node
            if (InRange(currentNode, start, end)) result.Add(currentNode.Points);
        }
        //if start is less than current
        //move left
        else
        {
            if (start[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
                RangeSearch(result, currentNode.Left, start, end, depth + 1);
            //if start is greater than current
            //and end is greater than current
            //move right
            if (end[currentDimension].CompareTo(currentNode.Points[currentDimension]) > 0)
                RangeSearch(result, currentNode.Right, start, end, depth + 1);

            //start is less than current node
            if (InRange(currentNode, start, end)) result.Add(currentNode.Points);
        }

        return result;
    }

    /// <summary>
    ///     Is the point in node is within start and end points.
    /// </summary>
    private bool InRange(KdTreeNode<T> node, T[] start, T[] end)
    {
        for (var i = 0; i < node.Points.Length; i++)
            //if not (start is less than node && end is greater than node)
            if (!(start[i].CompareTo(node.Points[i]) <= 0
                  && end[i].CompareTo(node.Points[i]) >= 0))
                return false;

        return true;
    }
}

/// <summary>
///     k-d tree node.
/// </summary>
internal class KdTreeNode<T> where T : IComparable
{
    internal KdTreeNode(int dimensions, KdTreeNode<T> parent)
    {
        Points = new T[dimensions];
        Parent = parent;
    }

    internal T[] Points { get; set; }

    internal KdTreeNode<T> Left { get; set; }
    internal KdTreeNode<T> Right { get; set; }
    internal bool IsLeaf => Left == null && Right == null;

    internal KdTreeNode<T> Parent { get; set; }
    internal bool IsLeftChild => Parent.Left == this;
}

/// <summary>
///     A concrete implementation of this interface is required
///     when calling NearestNeigbour() for k-d tree.
/// </summary>
public interface IDistanceCalculator<T> where T : IComparable
{
    /// <summary>
    ///     Compare the distance between point A to point
    ///     and point B to point.
    /// </summary>
    /// <returns>similar result as IComparable.</returns>
    int Compare(T[] a, T[] b, T[] point);

    /// <summary>
    ///     Compare distance between point A to B
    ///     and the distance between point Start to End.
    /// </summary>
    /// <returns>similar result as IComparabl.e</returns>
    int Compare(T a, T b, T[] start, T[] end);
}

internal class KdTreeEnumerator<T> : IEnumerator<T[]> where T : IComparable
{
    private readonly KdTreeNode<T> root;
    private Stack<KdTreeNode<T>> progress;

    internal KdTreeEnumerator(KdTreeNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null)
        {
            progress = new Stack<KdTreeNode<T>>(new[] { root.Left, root.Right }.Where(x => x != null));
            Current = root.Points;
            return true;
        }

        if (progress.Count > 0)
        {
            var next = progress.Pop();
            Current = next.Points;

            foreach (var node in new[] { next.Left, next.Right }.Where(x => x != null)) progress.Push(node);

            return true;
        }

        return false;
    }

    public void Reset()
    {
        progress = null;
        Current = null;
    }

    public T[] Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        progress = null;
    }
}ParseOptions.0.jsonå7
dD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\QuadTree.csé6using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.Geometry;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A quadtree implementation.
/// </summary>
public class QuadTree<T> : IEnumerable<Tuple<Point, T>>
{
    //used to decide when the tree should be reconstructed
    private int deletionCount;

    private QuadTreeNode<T> root;
    private readonly double tolerance;

    public QuadTree(int precision = 5)
    {
        tolerance = Math.Round(Math.Pow(0.1, precision), precision);
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Tuple<Point, T>> GetEnumerator()
    {
        return new QuadTreeEnumerator<T>(root);
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    /// <param name="point">The co-ordinate.</param>
    /// <param name="value">The value associated with this co-ordinate if any.</param>
    public void Insert(Point point, T value = default)
    {
        root = Insert(root, point, value);
        Count++;
    }

    private QuadTreeNode<T> Insert(QuadTreeNode<T> current, Point point, T value)
    {
        if (current == null) return new QuadTreeNode<T>(point, value);

        if (current.Point.X.IsEqual(point.X, tolerance) && current.Point.Y.IsEqual(point.Y, tolerance))
            throw new Exception("Point already exists.");

        //south-west / north-west
        if (point.X.IsLessThan(current.Point.X, tolerance))
        {
            if (point.Y.IsLessThan(current.Point.Y, tolerance))
                current.Sw = Insert(current.Sw, point, value);
            else
                current.Nw = Insert(current.Nw, point, value);
        }
        //south-east / north-east
        else if (point.Y.IsLessThan(current.Point.Y, tolerance))
        {
            current.Se = Insert(current.Se, point, value);
        }
        else
        {
            current.Ne = Insert(current.Ne, point, value);
        }

        return current;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public List<Tuple<Point, T>> RangeSearch(Rectangle searchWindow)
    {
        return RangeSearch(root, searchWindow, new List<Tuple<Point, T>>());
    }

    private List<Tuple<Point, T>> RangeSearch(QuadTreeNode<T> current, Rectangle searchWindow,
        List<Tuple<Point, T>> result)
    {
        if (current == null) return result;

        //is inside the search rectangle
        if (current.Point.X >= searchWindow.LeftTop.X
            && current.Point.X <= searchWindow.RightBottom.X
            && current.Point.Y <= searchWindow.LeftTop.Y
            && current.Point.Y >= searchWindow.RightBottom.Y
            && !current.IsDeleted)
            result.Add(new Tuple<Point, T>(current.Point, current.Value));

        //south-west
        if (searchWindow.LeftTop.X < current.Point.X && searchWindow.RightBottom.Y < current.Point.Y)
            RangeSearch(current.Sw, searchWindow, result);
        //north-west
        if (searchWindow.LeftTop.X < current.Point.X && searchWindow.LeftTop.Y >= current.Point.Y)
            RangeSearch(current.Nw, searchWindow, result);
        //north-east
        if (searchWindow.RightBottom.X > current.Point.X && searchWindow.LeftTop.Y >= current.Point.Y)
            RangeSearch(current.Ne, searchWindow, result);
        //south-east
        if (searchWindow.RightBottom.X > current.Point.X && searchWindow.RightBottom.Y < current.Point.Y)
            RangeSearch(current.Se, searchWindow, result);

        return result;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public void Delete(Point p)
    {
        var point = Find(root, p);

        if (point == null || point.IsDeleted) throw new Exception("Point not found.");

        point.IsDeleted = true;
        Count--;
        deletionCount++;

        if (deletionCount >= Count)
        {
            Reconstruct();
            deletionCount = 0;
        }
    }

    private void Reconstruct()
    {
        QuadTreeNode<T> newRoot = null;

        foreach (var exisiting in this) newRoot = Insert(newRoot, exisiting.Item1, exisiting.Item2);

        root = newRoot;
    }

    private QuadTreeNode<T> Find(QuadTreeNode<T> current, Point point)
    {
        if (current == null) return null;

        if (current.Point.X.IsEqual(point.X, tolerance) && current.Point.Y.IsEqual(point.Y, tolerance))
            return current;

        //south-west / north-west
        if (point.X.IsLessThan(current.Point.X, tolerance))
        {
            if (point.Y.IsLessThan(current.Point.Y, tolerance))
                return Find(current.Sw, point);
            return Find(current.Nw, point);
        }

        //south-east / north-east
        if (point.Y.IsLessThan(current.Point.Y, tolerance))
            return Find(current.Se, point);
        return Find(current.Ne, point);
    }
}

internal class QuadTreeNode<T>
{
    //marked as deleted
    internal bool IsDeleted;

    //quadrants
    internal QuadTreeNode<T> Nw, Ne, Se, Sw;

    //co-ordinate
    internal Point Point;

    //actual data if any associated with this point
    internal T Value;

    internal QuadTreeNode(Point point, T value)
    {
        Point = point;
        Value = value;
    }
}

internal class QuadTreeEnumerator<T> : IEnumerator<Tuple<Point, T>>
{
    private readonly QuadTreeNode<T> root;

    private QuadTreeNode<T> current;
    private Stack<QuadTreeNode<T>> progress;

    internal QuadTreeEnumerator(QuadTreeNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null)
        {
            progress = new Stack<QuadTreeNode<T>>(new[] { root.Ne, root.Nw, root.Se, root.Sw }.Where(x => x != null));
            current = root;

            if (!current.IsDeleted) return true;

            return MoveNext();
        }

        while (progress.Count > 0)
        {
            var next = progress.Pop();

            foreach (var child in new[] { next.Ne, next.Nw, next.Se, next.Sw }.Where(x => x != null))
                progress.Push(child);

            if (next.IsDeleted) continue;

            current = next;
            return true;
        }

        return false;
    }

    public void Reset()
    {
        progress = null;
        current = null;
    }

    object IEnumerator.Current => Current;

    public Tuple<Point, T> Current => new(current.Point, current.Value);

    public void Dispose()
    {
        progress = null;
    }
}ParseOptions.0.jsonõE
eD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\RangeTree.csúDusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A multi-dimentional range tree implementation.
/// </summary>
public class RangeTree<T> : IEnumerable<T[]> where T : IComparable
{
    private readonly int dimensions;
    private readonly HashSet<T[]> items = new(new ArrayComparer<T>());

    private readonly OneDimentionalRangeTree<T> tree = new();

    public RangeTree(int dimensions)
    {
        if (dimensions <= 0) throw new Exception("Dimension should be greater than 0.");

        this.dimensions = dimensions;
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T[]> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public void Insert(T[] value)
    {
        ValidateDimensions(value);

        if (items.Contains(value)) throw new Exception("value exists.");
        var currentTree = tree;
        //get all overlaps
        //and insert next dimension value to each overlapping node
        for (var i = 0; i < dimensions; i++) currentTree = currentTree.Insert(value[i]).Tree;

        items.Add(value);
        Count++;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public void Delete(T[] value)
    {
        ValidateDimensions(value);

        if (!items.Contains(value)) throw new Exception("Item not found.");

        var found = false;
        DeleteRecursive(tree, value, 0, ref found);
        items.Remove(value);
        Count--;
    }

    /// <summary>
    ///     Recursively move until last dimension and then delete if found.
    /// </summary>
    private void DeleteRecursive(OneDimentionalRangeTree<T> tree, T[] value,
        int currentDimension, ref bool found)
    {
        var node = tree.Find(value[currentDimension]);

        if (node != null)
        {
            if (currentDimension + 1 == dimensions)
                found = true;
            else
                DeleteRecursive(node.Tree, value, currentDimension + 1, ref found);
        }

        //delete node if next dimension has no elements
        //or when this is the last dimension and we found element
        if (node != null && found && (currentDimension + 1 == dimensions
                                      || node.Tree.Count == 0 && currentDimension + 1 < dimensions))
            tree.Delete(value[currentDimension]);
    }

    /// <summary>
    ///     Get all points within given range.
    ///     Time complexity: O(n).
    /// </summary>
    public List<T[]> RangeSearch(T[] start, T[] end)
    {
        ValidateDimensions(start);
        ValidateDimensions(end);

        return RangeSearch(tree, start, end, 0);
    }

    /// <summary>
    ///     Recursively visit node and return points within given range.
    /// </summary>
    private List<T[]> RangeSearch(
        OneDimentionalRangeTree<T> currentTree,
        T[] start, T[] end, int dimension)
    {
        var nodes = currentTree.RangeSearch(start[dimension], end[dimension]);

        if (dimension + 1 == dimensions)
        {
            var result = new List<T[]>();

            foreach (var value in nodes.SelectMany(x => x.Values))
            {
                var thisDimResult = new T[dimensions];
                thisDimResult[dimension] = value;
                result.Add(thisDimResult);
            }

            return result;
        }
        else
        {
            var result = new List<T[]>();

            foreach (var node in nodes)
            {
                var nextDimResult = RangeSearch(node.Tree, start, end, dimension + 1);

                foreach (var value in node.Values)
                foreach (var nextResult in nextDimResult)
                {
                    nextResult[dimension] = value;
                    result.Add(nextResult);
                }
            }

            return result;
        }
    }

    /// <summary>
    ///     Validate dimensions for point length.
    /// </summary>
    private void ValidateDimensions(T[] start)
    {
        if (start == null) throw new ArgumentNullException(nameof(start));

        if (start.Length != dimensions) throw new Exception($"Expecting {dimensions} points.");
    }
}

/// <summary>
///     One dimensional range tree
///     by nesting node with r-b tree for next dimension.
/// </summary>
internal class OneDimentionalRangeTree<T> where T : IComparable
{
    internal RedBlackTree<RangeTreeNode<T>> Tree = new();

    internal int Count => Tree.Count;

    internal RangeTreeNode<T> Find(T value)
    {
        var result = Tree.FindNode(new RangeTreeNode<T>(value));
        if (result == null) throw new Exception("Item not found in this tree.");

        return result.Value;
    }

    internal RangeTreeNode<T> Insert(T value)
    {
        var newNode = new RangeTreeNode<T>(value);

        var existing = Tree.FindNode(newNode);
        if (existing != null)
        {
            existing.Value.Values.Add(value);
            return existing.Value;
        }

        Tree.Insert(newNode);
        return newNode;
    }

    internal void Delete(T value)
    {
        var existing = Tree.FindNode(new RangeTreeNode<T>(value));

        if (existing.Value.Values.Count == 1)
        {
            Tree.Delete(new RangeTreeNode<T>(value));
            return;
        }

        //remove last
        existing.Value.Values.RemoveAt(existing.Value.Values.Count - 1);
    }

    internal List<RangeTreeNode<T>> RangeSearch(T start, T end)
    {
        return GetInRange(new List<RangeTreeNode<T>>(),
            new Dictionary<RedBlackTreeNode<RangeTreeNode<T>>, bool>(),
            Tree.Root, start, end);
    }

    private List<RangeTreeNode<T>> GetInRange(List<RangeTreeNode<T>> result,
        Dictionary<RedBlackTreeNode<RangeTreeNode<T>>, bool> visited,
        RedBlackTreeNode<RangeTreeNode<T>> currentNode,
        T start, T end)
    {
        if (currentNode.IsLeaf)
        {
            //start is less than current node
            if (!InRange(currentNode, start, end)) return result;

            result.Add(currentNode.Value);
        }
        //if start is less than current
        //move left
        else
        {
            if (start.CompareTo(currentNode.Value.Value) <= 0)
            {
                if (currentNode.Left != null) GetInRange(result, visited, currentNode.Left, start, end);

                //start is less than current node
                if (!visited.ContainsKey(currentNode)
                    && InRange(currentNode, start, end))
                {
                    result.Add(currentNode.Value);
                    visited.Add(currentNode, false);
                }
            }

            //if start is greater than current
            //and end is greater than current
            //move right
            if (end.CompareTo(currentNode.Value.Value) < 0) return result;

            {
                if (currentNode.Right != null) GetInRange(result, visited, currentNode.Right, start, end);

                //start is less than current node
                if (visited.ContainsKey(currentNode) || !InRange(currentNode, start, end)) return result;

                result.Add(currentNode.Value);
                visited.Add(currentNode, false);
            }
        }

        return result;
    }

    /// <summary>
    ///     Checks if current node is in search range.
    /// </summary>
    private bool InRange(RedBlackTreeNode<RangeTreeNode<T>> currentNode, T start, T end)
    {
        //start is less than current and end is greater than current
        return start.CompareTo(currentNode.Value.Value) <= 0
               && end.CompareTo(currentNode.Value.Value) >= 0;
    }
}

/// <summary>
///     Range tree node.
/// </summary>
internal class RangeTreeNode<T> : IComparable where T : IComparable
{
    internal RangeTreeNode(T value)
    {
        Values = new List<T>(new[] { value });
        Tree = new OneDimentionalRangeTree<T>();
    }

    internal T Value => Values[0];

    internal List<T> Values { get; set; }

    internal OneDimentionalRangeTree<T> Tree { get; set; }

    public int CompareTo(object obj)
    {
        return Value.CompareTo(((RangeTreeNode<T>)obj).Value);
    }
}ParseOptions.0.json√⁄
hD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\RedBlackTree.cs¿Ÿusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A red black tree implementation.
/// </summary>
public class RedBlackTree<T> : IEnumerable<T> where T : IComparable
{
    //if enabled, lookup will fasten deletion/insertion/exists operations. 
    internal readonly Dictionary<T, BstNodeBase<T>> NodeLookUp;

    /// <param name="enableNodeLookUp">
    ///     Enabling lookup will fasten deletion/insertion/exists operations
    ///     at the cost of additional space.
    /// </param>
    /// <param name="equalityComparer">
    ///     Provide equality comparer for node lookup if enabled (required when T is not a value
    ///     type).
    /// </param>
    public RedBlackTree(bool enableNodeLookUp = false, IEqualityComparer<T> equalityComparer = null)
    {
        if (enableNodeLookUp)
        {
            if (!typeof(T).GetTypeInfo().IsValueType && equalityComparer == null)
                throw new ArgumentException(
                    "equalityComparer parameter is required when node lookup us enabled and T is not a value type.");

            NodeLookUp = new Dictionary<T, BstNodeBase<T>>(equalityComparer ?? EqualityComparer<T>.Default);
        }
    }

    /// <summary>
    ///     Initialize the BST with given sorted keys optionally.
    ///     Time complexity: O(n).
    /// </summary>
    /// <param name="sortedCollection">The sorted initial collection.</param>
    /// <param name="enableNodeLookUp">
    ///     Enabling lookup will fasten deletion/insertion/exists operations
    ///     at the cost of additional space.
    /// </param>
    /// <param name="equalityComparer">
    ///     Provide equality comparer for node lookup if enabled (required when T is not a value
    ///     type).
    /// </param>
    public RedBlackTree(IEnumerable<T> sortedCollection, bool enableNodeLookUp = false,
        IEqualityComparer<T> equalityComparer = null)
    {
        BstHelpers.ValidateSortedCollection(sortedCollection);
        var nodes = sortedCollection.Select(x => new RedBlackTreeNode<T>(null, x)).ToArray();
        Root = (RedBlackTreeNode<T>)BstHelpers.ToBst(nodes);
        AssignColors(Root);
        BstHelpers.AssignCount(Root);

        if (enableNodeLookUp)
        {
            if (!typeof(T).GetTypeInfo().IsValueType && equalityComparer == null)
                throw new ArgumentException(
                    "equalityComparer parameter is required when node lookup us enabled and T is not a value type.");

            NodeLookUp = nodes.ToDictionary(x => x.Value, x => x as BstNodeBase<T>,
                equalityComparer ?? EqualityComparer<T>.Default);
        }
    }

    internal RedBlackTreeNode<T> Root { get; set; }

    public int Count => Root == null ? 0 : Root.Count;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BstEnumerator<T>(Root);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public bool HasItem(T value)
    {
        if (Root == null) return false;

        if (NodeLookUp != null) return NodeLookUp.ContainsKey(value);

        return Find(value).Item1 != null;
    }

    /// <summary>
    ///     Time complexity: O(1)
    /// </summary>
    internal void Clear()
    {
        Root = null;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T Max()
    {
        var max = Root.FindMax();
        return max == null ? default : max.Value;
    }

    private RedBlackTreeNode<T> FindMax(RedBlackTreeNode<T> node)
    {
        return node.FindMax() as RedBlackTreeNode<T>;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T Min()
    {
        var min = Root.FindMin();
        return min == null ? default : min.Value;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public int IndexOf(T item)
    {
        return Root.Position(item);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T ElementAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentNullException("index");

        return Root.KthSmallest(index).Value;
    }

    internal RedBlackTreeNode<T> FindNode(T value)
    {
        return Root == null ? null : Find(value).Item1;
    }

    internal bool Exists(T value)
    {
        return FindNode(value) != null;
    }

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    internal (RedBlackTreeNode<T>, int) Find(T value)
    {
        if (NodeLookUp != null)
        {
            if (NodeLookUp.ContainsKey(value))
            {
                var node = NodeLookUp[value] as RedBlackTreeNode<T>;
                return (node, Root.Position(value));
            }

            return (null, -1);
        }

        var result = Root.Find(value);
        return (result.Item1 as RedBlackTreeNode<T>, result.Item2);
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    ///     Returns the position (index) of the value in sorted order of this BST.
    /// </summary>
    public int Insert(T value)
    {
        var node = InsertAndReturnNode(value);
        return node.Item2;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    internal (RedBlackTreeNode<T>, int) InsertAndReturnNode(T value)
    {
        //empty tree
        if (Root == null)
        {
            Root = new RedBlackTreeNode<T>(null, value) { NodeColor = RedBlackTreeNodeColor.Black };
            if (NodeLookUp != null) NodeLookUp[value] = Root;

            return (Root, 0);
        }

        var newNode = Insert(Root, value);

        if (NodeLookUp != null) NodeLookUp[value] = newNode.Item1;

        return newNode;
    }

    //O(log(n)) always
    private (RedBlackTreeNode<T>, int) Insert(RedBlackTreeNode<T> currentNode, T newNodeValue)
    {
        var insertionPosition = 0;

        while (true)
        {
            var compareResult = currentNode.Value.CompareTo(newNodeValue);

            //current node is less than new item
            if (compareResult < 0)
            {
                insertionPosition += (currentNode.Left != null ? currentNode.Left.Count : 0) + 1;

                //no right child
                if (currentNode.Right == null)
                {
                    //insert
                    var node = currentNode.Right = new RedBlackTreeNode<T>(currentNode, newNodeValue);
                    BalanceInsertion(currentNode.Right);
                    return (node, insertionPosition);
                }

                currentNode = currentNode.Right;
            }
            //current node is greater than new node
            else if (compareResult > 0)
            {
                if (currentNode.Left == null)
                {
                    //insert
                    var node = currentNode.Left = new RedBlackTreeNode<T>(currentNode, newNodeValue);
                    BalanceInsertion(currentNode.Left);
                    return (node, insertionPosition);
                }

                currentNode = currentNode.Left;
            }
            else
            {
                //duplicate
                throw new Exception("Item with same key exists");
            }
        }
    }

    private void BalanceInsertion(RedBlackTreeNode<T> nodeToBalance)
    {
        while (true)
        {
            if (nodeToBalance == Root)
            {
                nodeToBalance.NodeColor = RedBlackTreeNodeColor.Black;
                break;
            }

            //if node to balance is red
            if (nodeToBalance.NodeColor == RedBlackTreeNodeColor.Red)
                //red-red relation; fix it!
                if (nodeToBalance.Parent.NodeColor == RedBlackTreeNodeColor.Red)
                {
                    //red sibling
                    if (nodeToBalance.Parent.Sibling != null &&
                        nodeToBalance.Parent.Sibling.NodeColor == RedBlackTreeNodeColor.Red)
                    {
                        //mark both children of parent as black and move up balancing 
                        nodeToBalance.Parent.Sibling.NodeColor = RedBlackTreeNodeColor.Black;
                        nodeToBalance.Parent.NodeColor = RedBlackTreeNodeColor.Black;

                        //root is always black
                        if (nodeToBalance.Parent.Parent != Root)
                            nodeToBalance.Parent.Parent.NodeColor = RedBlackTreeNodeColor.Red;

                        nodeToBalance.UpdateCounts();
                        nodeToBalance.Parent.UpdateCounts();
                        nodeToBalance = nodeToBalance.Parent.Parent;
                    }
                    //absent sibling or black sibling
                    else if (nodeToBalance.Parent.Sibling == null ||
                             nodeToBalance.Parent.Sibling.NodeColor == RedBlackTreeNodeColor.Black)
                    {
                        if (nodeToBalance.IsLeftChild && nodeToBalance.Parent.IsLeftChild)
                        {
                            var newRoot = nodeToBalance.Parent;
                            SwapColors(nodeToBalance.Parent, nodeToBalance.Parent.Parent);
                            RightRotate(nodeToBalance.Parent.Parent);

                            if (newRoot == Root) Root.NodeColor = RedBlackTreeNodeColor.Black;

                            nodeToBalance.UpdateCounts();
                            nodeToBalance = newRoot;
                        }
                        else if (nodeToBalance.IsLeftChild && nodeToBalance.Parent.IsRightChild)
                        {
                            RightRotate(nodeToBalance.Parent);

                            var newRoot = nodeToBalance;

                            SwapColors(nodeToBalance.Parent, nodeToBalance);
                            LeftRotate(nodeToBalance.Parent);

                            if (newRoot == Root) Root.NodeColor = RedBlackTreeNodeColor.Black;

                            nodeToBalance.UpdateCounts();
                            nodeToBalance = newRoot;
                        }
                        else if (nodeToBalance.IsRightChild && nodeToBalance.Parent.IsRightChild)
                        {
                            var newRoot = nodeToBalance.Parent;
                            SwapColors(nodeToBalance.Parent, nodeToBalance.Parent.Parent);
                            LeftRotate(nodeToBalance.Parent.Parent);

                            if (newRoot == Root) Root.NodeColor = RedBlackTreeNodeColor.Black;

                            nodeToBalance.UpdateCounts();
                            nodeToBalance = newRoot;
                        }
                        else if (nodeToBalance.IsRightChild && nodeToBalance.Parent.IsLeftChild)
                        {
                            LeftRotate(nodeToBalance.Parent);

                            var newRoot = nodeToBalance;

                            SwapColors(nodeToBalance.Parent, nodeToBalance);
                            RightRotate(nodeToBalance.Parent);

                            if (newRoot == Root) Root.NodeColor = RedBlackTreeNodeColor.Black;

                            nodeToBalance.UpdateCounts();
                            nodeToBalance = newRoot;
                        }
                    }
                }

            if (nodeToBalance.Parent != null)
            {
                nodeToBalance.UpdateCounts();
                nodeToBalance = nodeToBalance.Parent;
                continue;
            }

            break;
        }

        nodeToBalance.UpdateCounts(true);
    }

    private void SwapColors(RedBlackTreeNode<T> node1, RedBlackTreeNode<T> node2)
    {
        var tmpColor = node2.NodeColor;
        node2.NodeColor = node1.NodeColor;
        node1.NodeColor = tmpColor;
    }

    /// <summary>
    ///     Delete if value exists.
    ///     Time complexity: O(log(n))
    ///     Returns the position (index) of the item if deleted; otherwise returns -1
    /// </summary>
    public int Delete(T value)
    {
        if (Root == null) return -1;

        var node = Find(value);

        if (node.Item1 == null) return -1;

        var position = node.Item2;

        Delete(node.Item1);

        if (NodeLookUp != null) NodeLookUp.Remove(value);

        return position;
    }


    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentException("index");

        var node = Root.KthSmallest(index) as RedBlackTreeNode<T>;

        var deletedValue = node.Value;

        Delete(node);

        if (NodeLookUp != null) NodeLookUp.Remove(deletedValue);

        return node.Value;
    }

    //O(log(n)) always
    private void Delete(RedBlackTreeNode<T> node)
    {
        //node is a leaf node
        if (node.IsLeaf)
        {
            //if color is red, we are good; no need to balance
            if (node.NodeColor == RedBlackTreeNodeColor.Red)
            {
                DeleteLeaf(node);
                node.Parent?.UpdateCounts(true);
                return;
            }

            DeleteLeaf(node);
            BalanceNode(node.Parent);
        }
        else
        {
            //case one - right tree is null (move sub tree up)
            if (node.Left != null && node.Right == null)
            {
                DeleteLeftNode(node);
                BalanceNode(node.Left);
            }
            //case two - left tree is null  (move sub tree up)
            else if (node.Right != null && node.Left == null)
            {
                DeleteRightNode(node);
                BalanceNode(node.Right);
            }
            //case three - two child trees 
            //replace the node value with maximum element of left subtree (left max node)
            //and then delete the left max node
            else
            {
                var maxLeftNode = FindMax(node.Left);

                if (NodeLookUp != null)
                {
                    NodeLookUp[node.Value] = maxLeftNode;
                    NodeLookUp[maxLeftNode.Value] = node;
                }

                node.Value = maxLeftNode.Value;

                //delete left max node
                Delete(maxLeftNode);
            }
        }
    }

    private void BalanceNode(RedBlackTreeNode<T> nodeToBalance)
    {
        //handle six cases
        while (nodeToBalance != null)
        {
            nodeToBalance.UpdateCounts();
            nodeToBalance = HandleDoubleBlack(nodeToBalance);
        }
    }

    private void DeleteLeaf(RedBlackTreeNode<T> node)
    {
        //if node is root
        if (node.Parent == null)
            Root = null;
        //assign nodes parent.left/right to null
        else if (node.IsLeftChild)
            node.Parent.Left = null;
        else
            node.Parent.Right = null;
    }

    private void DeleteRightNode(RedBlackTreeNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Right.Parent = null;
            Root = Root.Right;
            Root.NodeColor = RedBlackTreeNodeColor.Black;
            return;
        }

        //node is left child of parent
        if (node.IsLeftChild)
            node.Parent.Left = node.Right;
        //node is right child of parent
        else
            node.Parent.Right = node.Right;

        node.Right.Parent = node.Parent;

        if (node.Right.NodeColor != RedBlackTreeNodeColor.Red) return;

        //black deletion! But we can take its red child and recolor it to black
        //and we are done!
        node.Right.NodeColor = RedBlackTreeNodeColor.Black;
    }

    private void DeleteLeftNode(RedBlackTreeNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Left.Parent = null;
            Root = Root.Left;
            Root.NodeColor = RedBlackTreeNodeColor.Black;
            return;
        }

        //node is left child of parent
        if (node.IsLeftChild)
            node.Parent.Left = node.Left;
        //node is right child of parent
        else
            node.Parent.Right = node.Left;

        node.Left.Parent = node.Parent;

        if (node.Left.NodeColor != RedBlackTreeNodeColor.Red) return;

        //black deletion! But we can take its red child and recolor it to black
        //and we are done!
        node.Left.NodeColor = RedBlackTreeNodeColor.Black;
    }

    private void RightRotate(RedBlackTreeNode<T> node)
    {
        var prevRoot = node;
        var leftRightChild = prevRoot.Left.Right;

        var newRoot = node.Left;

        //make left child as root
        prevRoot.Left.Parent = prevRoot.Parent;

        if (prevRoot.Parent != null)
        {
            if (prevRoot.Parent.Left == prevRoot)
                prevRoot.Parent.Left = prevRoot.Left;
            else
                prevRoot.Parent.Right = prevRoot.Left;
        }

        //move prev root as right child of current root
        newRoot.Right = prevRoot;
        prevRoot.Parent = newRoot;

        //move right child of left child of prev root to left child of right child of new root
        newRoot.Right.Left = leftRightChild;
        if (newRoot.Right.Left != null) newRoot.Right.Left.Parent = newRoot.Right;

        if (prevRoot == Root) Root = newRoot;

        newRoot.Left.UpdateCounts();
        newRoot.Right.UpdateCounts();
        newRoot.UpdateCounts();
    }

    private void LeftRotate(RedBlackTreeNode<T> node)
    {
        var prevRoot = node;
        var rightLeftChild = prevRoot.Right.Left;

        var newRoot = node.Right;

        //make right child as root
        prevRoot.Right.Parent = prevRoot.Parent;

        if (prevRoot.Parent != null)
        {
            if (prevRoot.Parent.Left == prevRoot)
                prevRoot.Parent.Left = prevRoot.Right;
            else
                prevRoot.Parent.Right = prevRoot.Right;
        }

        //move prev root as left child of current root
        newRoot.Left = prevRoot;
        prevRoot.Parent = newRoot;

        //move left child of right child of prev root to right child of left child of new root
        newRoot.Left.Right = rightLeftChild;
        if (newRoot.Left.Right != null) newRoot.Left.Right.Parent = newRoot.Left;

        if (prevRoot == Root) Root = newRoot;

        newRoot.Left.UpdateCounts();
        newRoot.Right.UpdateCounts();
        newRoot.UpdateCounts();
    }

    private RedBlackTreeNode<T> HandleDoubleBlack(RedBlackTreeNode<T> node)
    {
        //case 1
        if (node == Root)
        {
            node.NodeColor = RedBlackTreeNodeColor.Black;
            return null;
        }

        //case 2
        if (node.Parent != null
            && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling != null
            && node.Sibling.NodeColor == RedBlackTreeNodeColor.Red
            && (node.Sibling.Left == null && node.Sibling.Right == null
                || node.Sibling.Left != null && node.Sibling.Right != null
                                             && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                                             && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black))
        {
            node.Parent.NodeColor = RedBlackTreeNodeColor.Red;
            node.Sibling.NodeColor = RedBlackTreeNodeColor.Black;

            if (node.Sibling.IsRightChild)
                LeftRotate(node.Parent);
            else
                RightRotate(node.Parent);

            return node;
        }

        //case 3
        if (node.Parent != null
            && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling != null
            && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
            && (node.Sibling.Left == null && node.Sibling.Right == null
                || node.Sibling.Left != null && node.Sibling.Right != null
                                             && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                                             && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black))
        {
            //pushed up the double black problem up to parent
            //so now it needs to be fixed
            node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;

            return node.Parent;
        }


        //case 4
        if (node.Parent != null
            && node.Parent.NodeColor == RedBlackTreeNodeColor.Red
            && node.Sibling != null
            && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
            && (node.Sibling.Left == null && node.Sibling.Right == null
                || node.Sibling.Left != null && node.Sibling.Right != null
                                             && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                                             && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black))
        {
            //just swap the color of parent and sibling
            //which will compensate the loss of black count 
            node.Parent.NodeColor = RedBlackTreeNodeColor.Black;
            node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;
            node.UpdateCounts(true);
            return null;
        }


        //case 5
        if (node.Parent != null
            && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling != null
            && node.Sibling.IsRightChild
            && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling.Left != null
            && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Red
            && node.Sibling.Right != null
            && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black)
        {
            node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;
            node.Sibling.Left.NodeColor = RedBlackTreeNodeColor.Black;
            RightRotate(node.Sibling);

            return node;
        }

        //case 5 mirror
        if (node.Parent != null
            && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling != null
            && node.Sibling.IsLeftChild
            && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling.Left != null
            && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling.Right != null
            && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Red)
        {
            node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;
            node.Sibling.Right.NodeColor = RedBlackTreeNodeColor.Black;
            LeftRotate(node.Sibling);

            return node;
        }

        //case 6
        if (node.Parent != null
            && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling != null
            && node.Sibling.IsRightChild
            && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling.Right != null
            && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Red)
        {
            //left rotate to increase the black count on left side by one
            //and mark the red right child of sibling to black 
            //to compensate the loss of Black on right side of parent
            node.Sibling.Right.NodeColor = RedBlackTreeNodeColor.Black;
            LeftRotate(node.Parent);
            node.UpdateCounts(true);
            return null;
        }

        //case 6 mirror
        if (node.Parent != null
            && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling != null
            && node.Sibling.IsLeftChild
            && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
            && node.Sibling.Left != null
            && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Red)
        {
            //right rotate to increase the black count on right side by one
            //and mark the red left child of sibling to black
            //to compensate the loss of Black on right side of parent
            node.Sibling.Left.NodeColor = RedBlackTreeNodeColor.Black;
            RightRotate(node.Parent);
            node.UpdateCounts(true);
            return null;
        }

        node.UpdateCounts(true);
        return null;
    }

    //assign valid colors assuming the given tree node and its children are in balanced state.
    private void AssignColors(RedBlackTreeNode<T> current)
    {
        if (current == null) return;

        AssignColors(current.Left);
        AssignColors(current.Right);

        if (current.IsLeaf)
            current.NodeColor = RedBlackTreeNodeColor.Red;
        else
            current.NodeColor = RedBlackTreeNodeColor.Black;
    }

    /// <summary>
    ///     Get the next lower value to given value in this BST.
    /// </summary>
    public T NextLower(T value)
    {
        var node = FindNode(value);
        if (node == null) return default;

        var next = node.NextLower();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Get the next higher to given value in this BST.
    /// </summary>
    public T NextHigher(T value)
    {
        var node = FindNode(value);
        if (node == null) return default;

        var next = node.NextHigher();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Descending enumerable.
    /// </summary>
    public IEnumerable<T> AsEnumerableDesc()
    {
        return GetEnumeratorDesc().AsEnumerable();
    }

    public IEnumerator<T> GetEnumeratorDesc()
    {
        return new BstEnumerator<T>(Root, false);
    }
}

internal enum RedBlackTreeNodeColor
{
    Black,
    Red
}

/// <summary>
///     Red black tree node
/// </summary>
internal class RedBlackTreeNode<T> : BstNodeBase<T> where T : IComparable
{
    internal RedBlackTreeNode(RedBlackTreeNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;
        NodeColor = RedBlackTreeNodeColor.Red;
    }

    internal new RedBlackTreeNode<T> Parent
    {
        get => (RedBlackTreeNode<T>)base.Parent;
        set => base.Parent = value;
    }

    internal new RedBlackTreeNode<T> Left
    {
        get => (RedBlackTreeNode<T>)base.Left;
        set => base.Left = value;
    }

    internal new RedBlackTreeNode<T> Right
    {
        get => (RedBlackTreeNode<T>)base.Right;
        set => base.Right = value;
    }

    internal RedBlackTreeNodeColor NodeColor { get; set; }

    internal RedBlackTreeNode<T> Sibling => Parent.Left == this ? Parent.Right : Parent.Left;
}ParseOptions.0.jsonÁá
aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\RTree.csÎÜusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.Geometry;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     An RTree implementation.
/// </summary>
public class RTree : IEnumerable<Polygon>
{
    private readonly int maxKeysPerNode;
    private readonly int minKeysPerNode;

    //If we don't use leaf mappings then deletion/Exists will be slow
    //because searching for deletion leaf is expensive when data is dense.
    private readonly Dictionary<Polygon, RTreeNode> leafMappings = new();

    internal RTreeNode Root;

    public RTree(int maxKeysPerNode)
    {
        if (maxKeysPerNode < 3) throw new Exception("Max keys per node should be atleast 3.");

        this.maxKeysPerNode = maxKeysPerNode;
        minKeysPerNode = maxKeysPerNode / 2;
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Polygon> GetEnumerator()
    {
        return leafMappings.Select(x => x.Key).GetEnumerator();
    }

    /// <summary>
    ///     Inserts given polygon.
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Insert(Polygon newPolygon)
    {
        var newNode = new RTreeNode(maxKeysPerNode, null)
        {
            MbRectangle = newPolygon.GetContainingRectangle()
        };

        leafMappings.Add(newPolygon, newNode);
        InsertToLeaf(newNode);
        Count++;
    }

    private void InsertToLeaf(RTreeNode newNode)
    {
        if (Root == null)
        {
            Root = new RTreeNode(maxKeysPerNode, null);
            Root.AddChild(newNode);
            return;
        }

        var leafToInsert = FindInsertionLeaf(Root, newNode);
        InsertAndSplit(leafToInsert, newNode);
    }

    /// <summary>
    ///     Inserts the given internal node to the level where it belongs using its height.
    /// </summary>
    private void InsertInternalNode(RTreeNode internalNode)
    {
        InsertInternalNode(Root, internalNode);
    }

    private void InsertInternalNode(RTreeNode currentNode, RTreeNode internalNode)
    {
        if (currentNode.Height == internalNode.Height + 1)
            InsertAndSplit(currentNode, internalNode);
        else
            InsertInternalNode(currentNode.GetMinimumEnlargementAreaMbr(internalNode.MbRectangle), internalNode);
    }

    /// <summary>
    ///     Find the leaf node to start initial insertion.
    /// </summary>
    private RTreeNode FindInsertionLeaf(RTreeNode node, RTreeNode newNode)
    {
        //if leaf then its time to insert
        if (node.IsLeaf) return node;

        return FindInsertionLeaf(node.GetMinimumEnlargementAreaMbr(newNode.MbRectangle), newNode);
    }

    /// <summary>
    ///     Insert and split recursively up until no split is required.
    /// </summary>
    private void InsertAndSplit(RTreeNode node, RTreeNode newValue)
    {
        //newValue have room to fit in this node
        if (node.KeyCount < maxKeysPerNode)
        {
            node.AddChild(newValue);
            ExpandAncestorMbRs(node);
            return;
        }

        var e = new List<RTreeNode>(new[] { newValue });
        e.AddRange(node.Children);

        var distantPairs = GetDistantPairs(e);

        //Let E be the set consisting of all current entries and new entry.
        //Select as seeds two entries e1, e2 ‚àà E, where the distance between
        //left and right is the maximum among all other pairs of entries from E
        var e1 = new RTreeNode(maxKeysPerNode, null);
        var e2 = new RTreeNode(maxKeysPerNode, null);

        e1.AddChild(distantPairs.Item1);
        e2.AddChild(distantPairs.Item2);

        e = e.Where(x => x != distantPairs.Item1 && x != distantPairs.Item2)
            .ToList();

        /*Examine the remaining members of E one by one and assign them
        to e1 or e2, depending on which of the MBRs of these nodes
        will require the minimum area enlargement so as to cover this entry.
        If a tie occurs, assign the entry to the node whose MBR has the smaller area.
        If a tie occurs again, assign the entry to the node that contains the smaller number of entries*/
        while (e.Count > 0)
        {
            var current = e[e.Count - 1];

            var leftEnlargementArea = e1.MbRectangle.GetEnlargementArea(current.MbRectangle);
            var rightEnlargementArea = e2.MbRectangle.GetEnlargementArea(current.MbRectangle);

            if (leftEnlargementArea == rightEnlargementArea)
            {
                var leftArea = e1.MbRectangle.Area();
                var rightArea = e2.MbRectangle.Area();

                if (leftArea == rightArea)
                {
                    if (e1.KeyCount < e2.KeyCount)
                        e1.AddChild(current);
                    else
                        e2.AddChild(current);
                }
                else if (leftArea < rightArea)
                {
                    e1.AddChild(current);
                }
                else
                {
                    e2.AddChild(current);
                }
            }
            else if (leftEnlargementArea < rightEnlargementArea)
            {
                e1.AddChild(current);
            }
            else
            {
                e2.AddChild(current);
            }

            e.RemoveAt(e.Count - 1);

            var remaining = e.Count;

            /*if during the assignment of entries, there remain Œª entries to be assigned
            and the one node contains minKeysPerNode ‚àí Œª entries then
            assign all the remaining entries to this node without considering
            the aforementioned criteria
            so that the node will contain at least minKeysPerNode entries */
            if (e1.KeyCount == minKeysPerNode - remaining)
            {
                foreach (var entry in e) e1.AddChild(entry);
                e.Clear();
            }
            else if (e2.KeyCount == minKeysPerNode - remaining)
            {
                foreach (var entry in e) e2.AddChild(entry);
                e.Clear();
            }
        }

        var parent = node.Parent;
        if (parent != null)
        {
            //replace current node with e1
            parent.SetChild(node.Index, e1);
            //insert overflow element to parent
            InsertAndSplit(parent, e2);
        }
        else
        {
            //node is the root.
            //increase the height of RTree by one by adding a new root.
            Root = new RTreeNode(maxKeysPerNode, null);
            Root.AddChild(e1);
            Root.AddChild(e2);
        }
    }

    private void ExpandAncestorMbRs(RTreeNode node)
    {
        while (node.Parent != null)
        {
            node.Parent.MbRectangle.Merge(node.MbRectangle);
            node.Parent.Height = node.Height + 1;
            node = node.Parent;
        }
    }

    /// <summary>
    ///     Get the pairs of rectangles farther apart by comparing enlargement areas.
    /// </summary>
    private Tuple<RTreeNode, RTreeNode> GetDistantPairs(List<RTreeNode> allEntries)
    {
        Tuple<RTreeNode, RTreeNode> result = null;

        var maxArea = double.MinValue;
        for (var i = 0; i < allEntries.Count; i++)
        for (var j = i + 1; j < allEntries.Count; j++)
        {
            var currentArea = allEntries[i].MbRectangle.GetEnlargementArea(allEntries[j].MbRectangle);
            if (currentArea > maxArea)
            {
                result = new Tuple<RTreeNode, RTreeNode>(allEntries[i], allEntries[j]);
                maxArea = currentArea;
            }
        }

        return result;
    }

    /// <summary>
    ///     Check if the given polygon exists in this Rtree.
    ///     Time complexity: O(1).
    /// </summary>
    public bool Exists(Polygon searchPolygon)
    {
        return leafMappings.ContainsKey(searchPolygon);
    }

    /// <summary>
    ///     Returns a list of polygons whose minimum bounded rectangle intersects with given search rectangle.
    /// </summary>
    public List<Polygon> RangeSearch(Rectangle searchRectangle)
    {
        return RangeSearch(Root, searchRectangle, new List<Polygon>());
    }

    /// <summary>
    ///     Returns a list of polygons that's contained within given search rectangle.
    /// </summary>
    private List<Polygon> RangeSearch(RTreeNode current, Rectangle searchRectangle, List<Polygon> result)
    {
        if (current.IsLeaf)
            foreach (var node in current.Children.Take(current.KeyCount))
                if (RectangleIntersection.DoIntersect(node.MbRectangle, searchRectangle))
                    result.Add(node.MbRectangle.Polygon);

        foreach (var node in current.Children.Take(current.KeyCount))
            if (RectangleIntersection.DoIntersect(node.MbRectangle, searchRectangle))
                RangeSearch(node, searchRectangle, result);

        return result;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Delete(Polygon polygon)
    {
        if (Root == null) throw new Exception("Empty tree.");

        if (!Exists(polygon)) throw new Exception("Given polygon do not belong to this tree.");

        var nodeToDelete = leafMappings[polygon];

        //delete 
        DeleteNode(nodeToDelete);
        CondenseTree(nodeToDelete.Parent);

        if (Root.KeyCount == 1 && !Root.IsLeaf)
        {
            Root = Root.Children[0];
            Root.Parent = null;
        }

        leafMappings.Remove(polygon);
        Count--;

        if (Count == 0) Root = null;
    }

    private void DeleteNode(RTreeNode nodeToDelete)
    {
        RemoveAt(nodeToDelete.Parent.Children, nodeToDelete.Index);
        nodeToDelete.Parent.KeyCount--;
        UpdateIndex(nodeToDelete.Parent.Children, nodeToDelete.Parent.KeyCount, nodeToDelete.Index);
    }

    private void RemoveAt(RTreeNode[] array, int index)
    {
        //shift elements right by one indice from index
        Array.Copy(array, index + 1, array, index, array.Length - index - 1);
    }

    private void UpdateIndex(RTreeNode[] children, int keyCount, int index)
    {
        for (var i = index; i < keyCount; i++) children[i].Index--;
    }

    private void CondenseTree(RTreeNode updatedleaf)
    {
        var current = updatedleaf;
        var toReinsert = new Stack<RTreeNode>();

        while (current != Root)
        {
            var parent = current.Parent;

            if (current.KeyCount < minKeysPerNode)
            {
                DeleteNode(current);
                foreach (var node in current.Children.Take(current.KeyCount)) toReinsert.Push(node);
            }
            else
            {
                ShrinkMbr(current);
            }

            current = parent;
        }

        //update root
        if (current.KeyCount > 0) ShrinkMbr(current);

        while (toReinsert.Count > 0)
        {
            var node = toReinsert.Pop();

            if (node.Height > 0)
                InsertInternalNode(node);
            else
                InsertToLeaf(node);
        }
    }

    private void ShrinkMbr(RTreeNode current)
    {
        current.MbRectangle = new MbRectangle(current.Children[0].MbRectangle);
        foreach (var node in current.Children.Skip(1).Take(current.KeyCount - 1))
            current.MbRectangle.Merge(node.MbRectangle);
    }

    /// <summary>
    ///     Clear all data in this R-tree.
    /// </summary>
    public void Clear()
    {
        Root = null;
        leafMappings.Clear();
        Count = 0;
    }
}

internal static class PolygonExtensions
{
    /// <summary>
    ///     Gets the imaginary rectangle that contains the polygon.
    /// </summary>
    internal static MbRectangle GetContainingRectangle(this Polygon polygon)
    {
        var x = polygon.Edges.SelectMany(z => new[] { z.Left.X, z.Right.X })
            .Aggregate(new
            {
                Max = double.MinValue,
                Min = double.MaxValue
            }, (accumulator, o) => new
            {
                Max = Math.Max(o, accumulator.Max),
                Min = Math.Min(o, accumulator.Min)
            });


        var y = polygon.Edges.SelectMany(z => new[] { z.Left.Y, z.Right.Y })
            .Aggregate(new
            {
                Max = double.MinValue,
                Min = double.MaxValue
            }, (accumulator, o) => new
            {
                Max = Math.Max(o, accumulator.Max),
                Min = Math.Min(o, accumulator.Min)
            });

        return new MbRectangle(new Point(x.Min, y.Max), new Point(x.Max, y.Min))
        {
            Polygon = polygon
        };
    }
}

internal class RTreeNode
{
    internal int Height;

    /// <summary>
    ///     Array Index of this node in parent's Children array
    /// </summary>
    internal int Index;

    internal int KeyCount;

    internal RTreeNode(int maxKeysPerNode, RTreeNode parent)
    {
        Parent = parent;
        Children = new RTreeNode[maxKeysPerNode];
    }

    internal MbRectangle MbRectangle { get; set; }

    internal RTreeNode Parent { get; set; }
    internal RTreeNode[] Children { get; set; }

    //leafs will hold the actual polygon
    //we assume here that bottom two node levels as leafs
    internal bool IsLeaf => MbRectangle.Polygon != null
                            || Children[0].MbRectangle.Polygon != null;

    internal void AddChild(RTreeNode child)
    {
        SetChild(KeyCount, child);
        KeyCount++;
    }

    /// <summary>
    ///     Set the child at specifid index.
    /// </summary>
    internal void SetChild(int index, RTreeNode child)
    {
        Children[index] = child;
        Children[index].Parent = this;
        Children[index].Index = index;

        if (MbRectangle == null)
            MbRectangle = new MbRectangle(child.MbRectangle);
        else
            MbRectangle.Merge(child.MbRectangle);

        Height = child.Height + 1;
    }

    /// <summary>
    ///     Select the child node whose MBR will require the minimum area enlargement
    ///     to cover the given polygon.
    /// </summary>
    internal RTreeNode GetMinimumEnlargementAreaMbr(MbRectangle newPolygon)
    {
        //order by enlargement area
        //then by minimum area
        return Children[Children.Take(KeyCount)
            .Select((node, index) => new { node, index })
            .OrderBy(x => x.node.MbRectangle.GetEnlargementArea(newPolygon))
            .ThenBy(x => x.node.MbRectangle.Area())
            .First().index];
    }
}

/// <summary>
///     Minimum bounded rectangle (MBR).
/// </summary>
internal class MbRectangle : Rectangle
{
    internal MbRectangle(Point leftTopCorner, Point rightBottomCorner)
    {
        LeftTop = leftTopCorner;
        RightBottom = rightBottomCorner;
    }

    internal MbRectangle(Rectangle rectangle)
    {
        LeftTop = new Point(rectangle.LeftTop.X, rectangle.LeftTop.Y);
        RightBottom = new Point(rectangle.RightBottom.X, rectangle.RightBottom.Y);
    }

    /// <summary>
    ///     The actual polygon if this MBR is a leaf.
    /// </summary>
    internal Polygon Polygon { get; set; }

    /// <summary>
    ///     Returns the required enlargement area to fit the given rectangle inside this minimum bounded rectangle.
    /// </summary>
    /// <param name="polygonToFit">The rectangle to fit inside current MBR.</param>
    internal double GetEnlargementArea(MbRectangle rectangleToFit)
    {
        return Math.Abs(GetMergedRectangle(rectangleToFit).Area() - Area());
    }

    /// <summary>
    ///     Set current rectangle with the merge of given rectangle.
    /// </summary>
    internal void Merge(MbRectangle rectangleToMerge)
    {
        var merged = GetMergedRectangle(rectangleToMerge);

        LeftTop = merged.LeftTop;
        RightBottom = merged.RightBottom;
    }

    /// <summary>
    ///     Merge the current rectangle with given rectangle.
    /// </summary>
    private Rectangle GetMergedRectangle(MbRectangle rectangleToMerge)
    {
        var leftTopCorner = new Point(LeftTop.X > rectangleToMerge.LeftTop.X ? rectangleToMerge.LeftTop.X : LeftTop.X,
            LeftTop.Y < rectangleToMerge.LeftTop.Y ? rectangleToMerge.LeftTop.Y : LeftTop.Y);

        var rightBottomCorner = new Point(
            RightBottom.X < rectangleToMerge.RightBottom.X ? rectangleToMerge.RightBottom.X : RightBottom.X,
            RightBottom.Y > rectangleToMerge.RightBottom.Y ? rectangleToMerge.RightBottom.Y : RightBottom.Y);

        return new MbRectangle(leftTopCorner, rightBottomCorner);
    }
}ParseOptions.0.json¨ 
gD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\SegmentTree.cs´using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A segment tree implementation.
/// </summary>
public class SegmentTree<T> : IEnumerable<T>
{
    /// <summary>
    ///     Default value to eliminate node during range search.
    ///     Default value for Sum operation is 0.
    ///     Default value for Min operation is Max Value (i.e int.Max if T is int).
    ///     default value for Max operation is Min Value(i.e int.Min if T is int).
    /// </summary>
    private readonly Func<T> defaultValue;

    private readonly T[] input;
    private readonly int length;

    /// <summary>
    ///     The operation function pointer.
    ///     Example operations are Sum, Min, Max etc.
    /// </summary>
    private readonly Func<T, T, T> operation;

    private readonly T[] segmentTree;

    /// <summary>
    ///     Constructs a segment tree using the specified operation function.
    ///     Operation function is the criteria for range queries.
    ///     For example operation function can return Max, Min or Sum of the two input elements.
    ///     Default value is the void value that will eliminate a node during operation comparisons.
    ///     For example if operation return min value then the default value will be largest value (int.Max for if T is int).
    ///     Or default value will be 0 if operation is sum.
    ///     Time complexity: O(n).
    /// </summary>
    public SegmentTree(T[] input, Func<T, T, T> operation, Func<T> defaultValue)
    {
        if (input == null || operation == null) throw new ArgumentNullException();

        this.input = input.Clone() as T[];

        var maxHeight = Math.Ceiling(Math.Log(input.Length, 2));
        var maxTreeNodes = 2 * (int)Math.Pow(2, maxHeight) - 1;
        segmentTree = new T[maxTreeNodes];
        this.operation = operation;
        this.defaultValue = defaultValue;

        length = input.Length;

        Construct(input, 0, input.Length - 1, 0);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return input.Select(x => x).GetEnumerator();
    }

    private T Construct(T[] input, int left, int right, int currentIndex)
    {
        if (left == right)
        {
            segmentTree[currentIndex] = input[left];
            return segmentTree[currentIndex];
        }

        var midIndex = GetMidIndex(left, right);

        segmentTree[currentIndex] = operation(Construct(input, left, midIndex, 2 * currentIndex + 1),
            Construct(input, midIndex + 1, right, 2 * currentIndex + 2));

        return segmentTree[currentIndex];
    }

    /// <summary>
    ///     Gets the operation aggregated result for given range of the input.
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T RangeResult(int startIndex, int endIndex)
    {
        if (startIndex < 0 || endIndex > length - 1
                           || endIndex < startIndex)
            throw new ArgumentException();

        return GetRangeResult(startIndex, endIndex, 0, length - 1, 0);
    }

    private T GetRangeResult(int start, int end, int left, int right, int currentIndex)
    {
        //total overlap so return the value
        if (left >= start && right <= end) return segmentTree[currentIndex];

        //no overlap, so return default
        if (right < start || left > end) return defaultValue();

        //partial overlap so dig in
        var midIndex = GetMidIndex(left, right);
        return operation(GetRangeResult(start, end, left, midIndex, 2 * currentIndex + 1),
            GetRangeResult(start, end, midIndex + 1, right, 2 * currentIndex + 2));
    }

    private int GetMidIndex(int left, int right)
    {
        return left + (right - left) / 2;
    }
}ParseOptions.0.jsonÀ
pD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\Shared\ArrayComparer.cs¡using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     Compares two arrays.
/// </summary>
internal class ArrayComparer<T> : IEqualityComparer<T[]>
{
    public bool Equals(T[] x, T[] y)
    {
        if (x == y) return true;

        for (var i = 0; i < x.Length; i++)
            if (!x[i].Equals(y[i]))
                return false;

        return true;
    }

    public int GetHashCode(T[] x)
    {
        unchecked
        {
            if (x == null) return 0;

            var hash = 17;

            foreach (var element in x) hash = hash * 31 + element.GetHashCode();

            return hash;
        }
    }
}ParseOptions.0.jsonÙ	
pD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\Shared\BSTEnumerator.csÍusing System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

//  implement IEnumerator.
internal class BstEnumerator<T> : IEnumerator<T> where T : IComparable
{
    private readonly bool asc;

    private readonly BstNodeBase<T> root;
    private BstNodeBase<T> current;

    internal BstEnumerator(BstNodeBase<T> root, bool asc = true)
    {
        this.root = root;
        this.asc = asc;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (current == null)
        {
            current = asc ? root.FindMin() : root.FindMax();
            return true;
        }

        var next = asc ? current.NextHigher() : current.NextLower();
        if (next != null)
        {
            current = next;
            return true;
        }

        return false;
    }

    public void Reset()
    {
        current = root;
    }

    public T Current => current.Value;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        current = null;
    }
}ParseOptions.0.jsonª$
pD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\Shared\BSTExtensions.cs±#using System;

namespace Advanced.Algorithms.DataStructures;

internal static class BstExtensions
{
    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    //O(log(n)) worst O(n) for unbalanced tree
    internal static (BstNodeBase<T>, int) Find<T>(this BstNodeBase<T> current, T value) where T : IComparable
    {
        var position = 0;

        while (true)
        {
            if (current == null) return (null, -1);

            var compareResult = current.Value.CompareTo(value);

            if (compareResult == 0)
            {
                position += current.Left != null ? current.Left.Count : 0;
                return (current, position);
            }

            if (compareResult > 0)
            {
                current = current.Left;
            }
            else
            {
                position += (current.Left != null ? current.Left.Count : 0) + 1;
                current = current.Right;
            }
        }
    }

    internal static BstNodeBase<T> FindMax<T>(this BstNodeBase<T> node) where T : IComparable
    {
        if (node == null) return null;

        while (true)
        {
            if (node.Right == null) return node;
            node = node.Right;
        }
    }

    internal static BstNodeBase<T> FindMin<T>(this BstNodeBase<T> node) where T : IComparable
    {
        if (node == null) return null;

        while (true)
        {
            if (node.Left == null) return node;
            node = node.Left;
        }
    }

    internal static BstNodeBase<T> NextLower<T>(this BstNodeBase<T> node) where T : IComparable
    {
        //root or left child
        if (node.Parent == null || node.IsLeftChild)
        {
            if (node.Left != null)
            {
                node = node.Left;

                while (node.Right != null) node = node.Right;

                return node;
            }

            while (node.Parent != null && node.IsLeftChild) node = node.Parent;

            return node?.Parent;
        }
        //right child

        if (node.Left != null)
        {
            node = node.Left;

            while (node.Right != null) node = node.Right;

            return node;
        }

        return node.Parent;
    }

    internal static BstNodeBase<T> NextHigher<T>(this BstNodeBase<T> node) where T : IComparable
    {
        //root or left child
        if (node.Parent == null || node.IsLeftChild)
        {
            if (node.Right != null)
            {
                node = node.Right;

                while (node.Left != null) node = node.Left;

                return node;
            }

            return node?.Parent;
        }
        //right child

        if (node.Right != null)
        {
            node = node.Right;

            while (node.Left != null) node = node.Left;

            return node;
        }

        while (node.Parent != null && node.IsRightChild) node = node.Parent;

        return node?.Parent;
    }

    internal static void UpdateCounts<T>(this BstNodeBase<T> node, bool spiralUp = false) where T : IComparable
    {
        while (node != null)
        {
            var leftCount = node.Left?.Count ?? 0;
            var rightCount = node.Right?.Count ?? 0;

            node.Count = leftCount + rightCount + 1;

            node = node.Parent;

            if (!spiralUp) break;
        }
    }

    //get the kth smallest element under given node
    internal static BstNodeBase<T> KthSmallest<T>(this BstNodeBase<T> node, int k) where T : IComparable
    {
        var leftCount = node.Left != null ? node.Left.Count : 0;

        if (k == leftCount) return node;

        if (k < leftCount) return KthSmallest(node.Left, k);

        return KthSmallest(node.Right, k - leftCount - 1);
    }

    //get the sorted order position of given item under given node
    internal static int Position<T>(this BstNodeBase<T> node, T item) where T : IComparable
    {
        if (node == null) return -1;

        var leftCount = node.Left != null ? node.Left.Count : 0;

        if (node.Value.CompareTo(item) == 0) return leftCount;

        if (item.CompareTo(node.Value) < 0) return Position(node.Left, item);

        var position = Position(node.Right, item);

        return position < 0 ? position : position + leftCount + 1;
    }
}ParseOptions.0.jsonê
mD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\Shared\BSTHelpers.csâusing System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

internal class BstHelpers
{
    internal static void ValidateSortedCollection<T>(IEnumerable<T> sortedCollection) where T : IComparable
    {
        if (!IsSorted(sortedCollection))
            throw new ArgumentException("Initial collection should have unique keys and be in sorted order.");
    }

    internal static BstNodeBase<T> ToBst<T>(BstNodeBase<T>[] sortedNodes) where T : IComparable
    {
        return ToBst(sortedNodes, 0, sortedNodes.Length - 1);
    }

    internal static int AssignCount<T>(BstNodeBase<T> node) where T : IComparable
    {
        if (node == null) return 0;

        node.Count = AssignCount(node.Left) + AssignCount(node.Right) + 1;

        return node.Count;
    }

    private static BstNodeBase<T> ToBst<T>(BstNodeBase<T>[] sortedNodes, int start, int end) where T : IComparable
    {
        if (start > end)
            return null;

        var mid = (start + end) / 2;
        var root = sortedNodes[mid];

        root.Left = ToBst(sortedNodes, start, mid - 1);
        if (root.Left != null) root.Left.Parent = root;

        root.Right = ToBst(sortedNodes, mid + 1, end);
        if (root.Right != null) root.Right.Parent = root;

        return root;
    }

    private static bool IsSorted<T>(IEnumerable<T> collection) where T : IComparable
    {
        var enumerator = collection.GetEnumerator();
        if (!enumerator.MoveNext()) return true;

        var previous = enumerator.Current;

        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;

            if (current.CompareTo(previous) <= 0) return false;
        }

        return true;
    }
}ParseOptions.0.jsonß
nD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\Shared\BSTNodeBase.csüusing System;

namespace Advanced.Algorithms.DataStructures;

internal abstract class BstNodeBase<T> where T : IComparable
{
    //Count of nodes under this node including this node.
    //Used to fasten kth smallest computation.
    internal int Count { get; set; } = 1;

    internal virtual BstNodeBase<T> Parent { get; set; }

    internal virtual BstNodeBase<T> Left { get; set; }
    internal virtual BstNodeBase<T> Right { get; set; }

    internal T Value { get; set; }

    internal bool IsLeftChild => Parent.Left == this;
    internal bool IsRightChild => Parent.Right == this;

    internal bool IsLeaf => Left == null && Right == null;
}ParseOptions.0.jsoníp
eD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\SplayTree.csìousing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A splay tree implementation.
/// </summary>
public class SplayTree<T> : IEnumerable<T> where T : IComparable
{
    public SplayTree()
    {
    }

    /// <summary>
    ///     Initialize the BST with given sorted keys.
    ///     Time complexity: O(n).
    /// </summary>
    /// <param name="sortedCollection">The sorted collection.</param>
    public SplayTree(IEnumerable<T> sortedCollection) : this()
    {
        BstHelpers.ValidateSortedCollection(sortedCollection);
        var nodes = sortedCollection.Select(x => new SplayTreeNode<T>(null, x)).ToArray();
        Root = (SplayTreeNode<T>)BstHelpers.ToBst(nodes);
        BstHelpers.AssignCount(Root);
    }

    internal SplayTreeNode<T> Root { get; set; }
    public int Count => Root == null ? 0 : Root.Count;

    //Implementation for the GetEnumerator method.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BstEnumerator<T>(Root);
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public bool HasItem(T value)
    {
        if (Root == null) return false;

        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    internal int GetHeight()
    {
        return GetHeight(Root);
    }

    private int GetHeight(SplayTreeNode<T> node)
    {
        if (node == null) return -1;

        return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public void Insert(T value)
    {
        if (Root == null)
        {
            Root = new SplayTreeNode<T>(null, value);
            return;
        }

        var newNode = Insert(Root, value);
        Splay(newNode);
    }

    //O(log(n)) always
    private SplayTreeNode<T> Insert(SplayTreeNode<T> currentNode, T newNodeValue)
    {
        while (true)
        {
            var compareResult = currentNode.Value.CompareTo(newNodeValue);

            //current node is less than new item
            if (compareResult < 0)
            {
                //no right child
                if (currentNode.Right == null)
                {
                    //insert
                    currentNode.Right = new SplayTreeNode<T>(currentNode, newNodeValue);
                    return currentNode.Right;
                }

                currentNode = currentNode.Right;
            }
            //current node is greater than new node
            else if (compareResult > 0)
            {
                if (currentNode.Left == null)
                {
                    //insert
                    currentNode.Left = new SplayTreeNode<T>(currentNode, newNodeValue);
                    return currentNode.Left;
                }

                currentNode = currentNode.Left;
            }
            else
            {
                throw new Exception("Item exists");
            }
        }
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public int IndexOf(T item)
    {
        return Root.Position(item);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T ElementAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentNullException("index");

        return Root.KthSmallest(index).Value;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public void Delete(T value)
    {
        if (Root == null) throw new Exception("Empty SplayTree");

        Delete(Root, value);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentException("index");

        var nodeToDelete = Root.KthSmallest(index) as SplayTreeNode<T>;

        Delete(nodeToDelete, nodeToDelete.Value);

        return nodeToDelete.Value;
    }

    private void Delete(SplayTreeNode<T> node, T value)
    {
        while (true)
        {
            var compareResult = node.Value.CompareTo(value);

            //node is less than the search value so move right to find the deletion node
            if (compareResult < 0)
            {
                node = node.Right ?? throw new Exception("Item do not exist");
                continue;
            }
            //node is less than the search value so move left to find the deletion node

            if (compareResult > 0)
            {
                node = node.Left ?? throw new Exception("Item do not exist");
                continue;
            }

            var parent = node.Parent;
            //node is a leaf node
            if (node.IsLeaf)
            {
                DeleteLeaf(node);
            }
            else
            {
                //case one - right tree is null (move sub tree up)
                if (node.Left != null && node.Right == null)
                {
                    DeleteLeftNode(node);
                }
                //case two - left tree is null  (move sub tree up)
                else if (node.Right != null && node.Left == null)
                {
                    DeleteRightNode(node);
                }
                //case three - two child trees 
                //replace the node value with maximum element of left subtree (left max node)
                //and then delete the left max node
                else
                {
                    var maxLeftNode = FindMax(node.Left);

                    node.Value = maxLeftNode.Value;

                    //delete left max node
                    Delete(node.Left, maxLeftNode.Value);
                }
            }

            if (parent != null) Splay(parent);

            break;
        }
    }

    private void DeleteLeaf(SplayTreeNode<T> node)
    {
        //if node is root
        if (node.Parent == null)
            Root = null;
        //assign nodes parent.left/right to null
        else if (node.IsLeftChild)
            node.Parent.Left = null;
        else
            node.Parent.Right = null;
    }

    private void DeleteRightNode(SplayTreeNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Right.Parent = null;
            Root = Root.Right;
            return;
        }

        //node is left child of parent
        if (node.IsLeftChild)
            node.Parent.Left = node.Right;
        //node is right child of parent
        else
            node.Parent.Right = node.Right;

        node.Right.Parent = node.Parent;
    }

    private void DeleteLeftNode(SplayTreeNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Left.Parent = null;
            Root = Root.Left;
            return;
        }

        //node is left child of parent
        if (node.IsLeftChild)
            node.Parent.Left = node.Left;
        //node is right child of parent
        else
            node.Parent.Right = node.Left;

        node.Left.Parent = node.Parent;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T FindMax()
    {
        return FindMax(Root).Value;
    }

    private SplayTreeNode<T> FindMax(SplayTreeNode<T> node)
    {
        while (true)
        {
            if (node.Right == null) return node;
            node = node.Right;
        }
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T FindMin()
    {
        return FindMin(Root).Value;
    }

    private SplayTreeNode<T> FindMin(SplayTreeNode<T> node)
    {
        while (true)
        {
            if (node.Left == null) return node;
            node = node.Left;
        }
    }

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    private SplayTreeNode<T> Find(SplayTreeNode<T> parent, T value)
    {
        while (true)
        {
            if (parent == null) return null;

            if (parent.Value.CompareTo(value) == 0) return parent;

            var left = Find(parent.Left, value);

            if (left != null) return left;
            parent = parent.Right;
        }
    }

    private void Splay(SplayTreeNode<T> x)
    {
        x.UpdateCounts();

        while (x.Parent != null)
        {
            if (x.Parent.Parent == null)
            {
                //zig step
                x = x.IsLeftChild ? RightRotate(x.Parent) : LeftRotate(x.Parent);
            }
            //zig-zig step
            else if (x.IsLeftChild && x.Parent.IsLeftChild)

            {
                RightRotate(x.Parent.Parent);
                x = RightRotate(x.Parent);
            }
            //zig-zig step mirror
            else if (x.IsRightChild && x.Parent.IsRightChild)
            {
                LeftRotate(x.Parent.Parent);
                x = LeftRotate(x.Parent);
            }
            //zig-zag step
            else if (x.IsLeftChild && x.Parent.IsRightChild)
            {
                RightRotate(x.Parent);
                x = LeftRotate(x.Parent);
            }
            //zig-zag step mirror
            else //if (x.IsRightChild && x.Parent.IsLeftChild)
            {
                LeftRotate(x.Parent);
                x = RightRotate(x.Parent);
            }

            x.UpdateCounts();
        }
    }

    /// <summary>
    ///     Rotates current root right and returns the new root node
    /// </summary>
    private SplayTreeNode<T> RightRotate(SplayTreeNode<T> currentRoot)
    {
        var prevRoot = currentRoot;
        var leftRightChild = prevRoot.Left.Right;

        var newRoot = currentRoot.Left;

        //make left child as root
        prevRoot.Left.Parent = prevRoot.Parent;

        if (prevRoot.Parent != null)
        {
            if (prevRoot.Parent.Left == prevRoot)
                prevRoot.Parent.Left = prevRoot.Left;
            else
                prevRoot.Parent.Right = prevRoot.Left;
        }

        //move prev root as right child of current root
        newRoot.Right = prevRoot;
        prevRoot.Parent = newRoot;

        //move right child of left child of prev root to left child of right child of new root
        newRoot.Right.Left = leftRightChild;
        if (newRoot.Right.Left != null) newRoot.Right.Left.Parent = newRoot.Right;

        newRoot.Left.UpdateCounts();
        newRoot.Right.UpdateCounts();
        newRoot.UpdateCounts();

        if (prevRoot == Root) Root = newRoot;

        return newRoot;
    }

    /// <summary>
    ///     Rotates the current root left and returns new root
    /// </summary>
    private SplayTreeNode<T> LeftRotate(SplayTreeNode<T> currentRoot)
    {
        var prevRoot = currentRoot;
        var rightLeftChild = prevRoot.Right.Left;

        var newRoot = currentRoot.Right;

        //make right child as root
        prevRoot.Right.Parent = prevRoot.Parent;

        if (prevRoot.Parent != null)
        {
            if (prevRoot.Parent.Left == prevRoot)
                prevRoot.Parent.Left = prevRoot.Right;
            else
                prevRoot.Parent.Right = prevRoot.Right;
        }

        //move prev root as left child of current root
        newRoot.Left = prevRoot;
        prevRoot.Parent = newRoot;

        //move left child of right child of prev root to right child of left child of new root
        newRoot.Left.Right = rightLeftChild;
        if (newRoot.Left.Right != null) newRoot.Left.Right.Parent = newRoot.Left;

        newRoot.Left.UpdateCounts();
        newRoot.Right.UpdateCounts();
        newRoot.UpdateCounts();

        if (prevRoot == Root) Root = newRoot;

        return newRoot;
    }


    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    private BstNodeBase<T> Find(T value)
    {
        return Root.Find(value).Item1;
    }

    /// <summary>
    ///     Get the next lower value to given value in this BST.
    ///     Time complexity: O(n).
    /// </summary>
    public T NextLower(T value)
    {
        var node = Find(value);
        if (node == null) return default;

        var next = node.NextLower();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Get the next higher value to given value in this BST.
    ///     Time complexity: O(n).
    /// </summary>
    public T NextHigher(T value)
    {
        var node = Find(value);
        if (node == null) return default;

        var next = node.NextHigher();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Descending enumerable.
    /// </summary>
    public IEnumerable<T> AsEnumerableDesc()
    {
        return GetEnumeratorDesc().AsEnumerable();
    }

    public IEnumerator<T> GetEnumeratorDesc()
    {
        return new BstEnumerator<T>(Root, false);
    }
}

internal class SplayTreeNode<T> : BstNodeBase<T> where T : IComparable
{
    internal SplayTreeNode(SplayTreeNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;
    }

    internal new SplayTreeNode<T> Parent
    {
        get => (SplayTreeNode<T>)base.Parent;
        set => base.Parent = value;
    }

    internal new SplayTreeNode<T> Left
    {
        get => (SplayTreeNode<T>)base.Left;
        set => base.Left = value;
    }

    internal new SplayTreeNode<T> Right
    {
        get => (SplayTreeNode<T>)base.Right;
        set => base.Right = value;
    }
}ParseOptions.0.json¢
fD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\SuffixTree.cs¢using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A suffix tree implementation using a trie.
/// </summary>
public class SuffixTree<T> : IEnumerable<T[]>
{
    private readonly HashSet<T[]> items = new(new ArrayComparer<T>());
    private readonly Trie<T> trie;

    public SuffixTree()
    {
        trie = new Trie<T>();
        Count = 0;
    }

    public int Count { private set; get; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T[]> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    /// <summary>
    ///     Insert a new entry to this suffix tree.
    ///     Time complexity: O(m^2) where m is the length of entry array.
    /// </summary>
    public void Insert(T[] entry)
    {
        if (entry == null) throw new ArgumentException();

        if (items.Contains(entry)) throw new Exception("Item exists.");

        for (var i = 0; i < entry.Length; i++)
        {
            var suffix = new T[entry.Length - i];
            Array.Copy(entry, i, suffix, 0, entry.Length - i);

            trie.Insert(suffix);
        }

        items.Add(entry);

        Count++;
    }

    /// <summary>
    ///     Deletes an existing entry from this suffix tree.
    ///     Time complexity: O(m^2) where m is the length of entry array.
    /// </summary>
    public void Delete(T[] entry)
    {
        if (entry == null) throw new ArgumentException();

        if (!items.Contains(entry)) throw new Exception("Item does'nt exist.");

        for (var i = 0; i < entry.Length; i++)
        {
            var suffix = new T[entry.Length - i];
            Array.Copy(entry, i, suffix, 0, entry.Length - i);

            trie.Delete(suffix);
        }

        items.Remove(entry);

        Count--;
    }

    /// <summary>
    ///     Returns true if the given entry pattern is in this suffix tree.
    ///     Time complexity: O(e) where e is the length of the given entry.
    /// </summary>
    public bool Contains(T[] pattern)
    {
        return trie.ContainsPrefix(pattern);
    }

    /// <summary>
    ///     Returns all sub-entries that starts with this search pattern.
    ///     Time complexity: O(rm) where r is the number of results and m is the average length of each entry.
    /// </summary>
    public List<T[]> StartsWith(T[] pattern)
    {
        return trie.StartsWith(pattern);
    }
}ParseOptions.0.json¡[
mD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\TernarySearchTree.cs∫Zusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A ternary search tree implementation.
/// </summary>
public class TernarySearchTree<T> : IEnumerable<T[]> where T : IComparable
{
    private TernarySearchTreeNode<T> root;

    public TernarySearchTree()
    {
        Count = 0;
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T[]> GetEnumerator()
    {
        return new TernarySearchTreeEnumerator<T>(root);
    }

    /// <summary>
    ///     Time complexity: O(m) where m is the length of entry.
    /// </summary>
    public void Insert(T[] entry)
    {
        Insert(ref root, null, entry, 0);
        Count++;
    }

    /// <summary>
    ///     Insert a new record to this ternary search tree after finding the end recursively.
    /// </summary>
    private void Insert(ref TernarySearchTreeNode<T> currentNode,
        TernarySearchTreeNode<T> parent,
        T[] entry, int currentIndex)
    {
        //create new node if empty
        if (currentNode == null) currentNode = new TernarySearchTreeNode<T>(parent, entry[currentIndex]);

        var compareResult = currentNode.Value.CompareTo(entry[currentIndex]);

        //current is greater? move left, move right otherwise
        //if current is equal then move center
        if (compareResult > 0)
        {
            //move left
            var left = currentNode.Left;
            Insert(ref left, parent, entry, currentIndex);
            currentNode.Left = left;
        }
        else if (compareResult < 0)
        {
            //move right
            var right = currentNode.Right;
            Insert(ref right, parent, entry, currentIndex);
            currentNode.Right = right;
        }
        else
        {
            if (currentIndex != entry.Length - 1)
            {
                //if equal we just skip to next element
                var middle = currentNode.Middle;
                Insert(ref middle, currentNode, entry, currentIndex + 1);
                currentNode.Middle = middle;
            }
            //end of word
            else
            {
                if (currentNode.IsEnd) throw new Exception("Item exists.");

                currentNode.IsEnd = true;
            }
        }
    }

    /// <summary>
    ///     Deletes a record from this ternary search tree.
    ///     Time complexity: O(m) where m is the length of entry.
    /// </summary>
    public void Delete(T[] entry)
    {
        Delete(root, entry, 0);
        Count--;
    }

    /// <summary>
    ///     Deletes a record from this TernarySearchTree after finding it recursively.
    /// </summary>
    private void Delete(TernarySearchTreeNode<T> currentNode,
        T[] entry, int currentIndex)
    {
        //empty node
        if (currentNode == null) throw new Exception("Item not found.");

        var compareResult = currentNode.Value.CompareTo(entry[currentIndex]);
        TernarySearchTreeNode<T> child;
        //current is greater? move left, move right otherwise
        //if current is equal then move center
        if (compareResult > 0)
        {
            //move left
            child = currentNode.Left;

            Delete(child, entry, currentIndex);
            //delete if middle is not end
            //and we if have'nt deleted the node yet
            if (child.HasChildren == false
                && !child.IsEnd)
                currentNode.Left = null;
        }
        else if (compareResult < 0)
        {
            //move right
            child = currentNode.Right;
            Delete(child, entry, currentIndex);
            //delete if middle is not end
            //and we if have'nt deleted the node yet
            if (child.HasChildren == false
                && !child.IsEnd)
                currentNode.Right = null;
        }
        else
        {
            if (currentIndex != entry.Length - 1)
            {
                //if equal we just skip to next element
                child = currentNode.Middle;
                Delete(child, entry, currentIndex + 1);
                //delete if middle is not end
                //and we if have'nt deleted the node yet
                if (child.HasChildren == false
                    && !child.IsEnd)
                    currentNode.Middle = null;
            }
            //end of word
            else
            {
                if (!currentNode.IsEnd) throw new Exception("Item not found.");

                //remove this end flag
                currentNode.IsEnd = false;
            }
        }
    }

    /// <summary>
    ///     Returns a list of records matching this prefix.
    ///     Time complexity: O(rm) where r is the number of results and m is the average length of each entry.
    /// </summary>
    public List<T[]> StartsWith(T[] prefix)
    {
        return StartsWith(root, prefix, 0);
    }

    /// <summary>
    ///     Recursively visit until end of prefix and then gather all suffixes under it.
    /// </summary>
    private List<T[]> StartsWith(TernarySearchTreeNode<T> currentNode, T[] searchPrefix, int currentIndex)
    {
        while (true)
        {
            if (currentNode == null) return new List<T[]>();

            var compareResult = currentNode.Value.CompareTo(searchPrefix[currentIndex]);
            //current is greater? move left, move right otherwise
            //if current is equal then move center
            if (compareResult > 0)
            {
                //move left
                currentNode = currentNode.Left;
                continue;
            }

            if (compareResult < 0)
            {
                //move right
                currentNode = currentNode.Right;
                continue;
            }

            //end of search Prefix, so gather all words under it
            if (currentIndex != searchPrefix.Length - 1)
            {
                currentNode = currentNode.Middle;
                currentIndex = currentIndex + 1;
                continue;
            }

            var result = new List<T[]>();

            GatherStartsWith(result, searchPrefix.ToList(), currentNode.Middle);

            return result;
        }
    }

    /// <summary>
    ///     Gathers all suffixes under this node appending with the given prefix
    /// </summary>
    private void GatherStartsWith(List<T[]> result, List<T> prefix, TernarySearchTreeNode<T> node)
    {
        while (true)
        {
            if (node == null)
            {
                result.Add(prefix.ToArray());
                return;
            }

            //end of word
            if (node.IsEnd)
                //append to end of prefix for new prefix
                result.Add(prefix.Concat(new[] { node.Value }).ToArray());

            if (node.Left != null) GatherStartsWith(result, prefix, node.Left);

            if (node.Middle != null)
            {
                //append to end of prefix for new prefix
                prefix.Add(node.Value);
                GatherStartsWith(result, prefix, node.Middle);
                prefix.RemoveAt(prefix.Count - 1);
            }

            if (node.Right != null)
            {
                node = node.Right;
                continue;
            }

            break;
        }
    }

    /// <summary>
    ///     Returns true if the entry exist.
    ///     Time complexity: O(e) where e is the length of the given entry.
    /// </summary>
    public bool Contains(T[] entry)
    {
        return Search(root, entry, 0, false);
    }


    /// <summary>
    ///     Returns true if the entry prefix exist.
    ///     Time complexity: O(e) where e is the length of the given entry.
    /// </summary>
    public bool ContainsPrefix(T[] entry)
    {
        return Search(root, entry, 0, true);
    }

    /// <summary>
    ///     Find if the record exist recursively.
    /// </summary>
    private bool Search(TernarySearchTreeNode<T> currentNode, T[] searchEntry, int currentIndex, bool isPrefixSearch)
    {
        while (true)
        {
            //create new node if empty
            if (currentNode == null) return false;

            //end of word, so return
            if (currentIndex == searchEntry.Length - 1) return isPrefixSearch || currentNode.IsEnd;

            var compareResult = currentNode.Value.CompareTo(searchEntry[currentIndex]);
            //current is greater? move left, move right otherwise
            //if current is equal then move center
            if (compareResult > 0)
            {
                //move left
                currentNode = currentNode.Left;
                continue;
            }

            if (compareResult < 0)
            {
                //move right
                currentNode = currentNode.Right;
                continue;
            }

            //if equal we just skip to next element
            currentNode = currentNode.Middle;
            currentIndex = currentIndex + 1;
        }
    }
}

internal class TernarySearchTreeNode<T> where T : IComparable
{
    internal TernarySearchTreeNode(TernarySearchTreeNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;
    }

    internal bool IsEnd { get; set; }
    internal T Value { get; set; }
    internal bool HasChildren => !(Left == null && Middle == null && Right == null);

    internal TernarySearchTreeNode<T> Parent { get; set; }

    internal TernarySearchTreeNode<T> Left { get; set; }
    internal TernarySearchTreeNode<T> Middle { get; set; }
    internal TernarySearchTreeNode<T> Right { get; set; }
}

internal class TernarySearchTreeEnumerator<T> : IEnumerator<T[]> where T : IComparable
{
    private readonly TernarySearchTreeNode<T> root;
    private Stack<TernarySearchTreeNode<T>> progress;

    internal TernarySearchTreeEnumerator(TernarySearchTreeNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null) progress = new Stack<TernarySearchTreeNode<T>>(new[] { root });

        while (progress.Count > 0)
        {
            var next = progress.Pop();

            foreach (var child in new[] { next.Left, next.Middle, next.Right }.Where(x => x != null))
                progress.Push(child);

            if (next.IsEnd)
            {
                Current = GetValue(next);
                return true;
            }
        }

        return false;
    }

    public void Reset()
    {
        progress = null;
        Current = null;
    }

    public T[] Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        progress = null;
    }

    private T[] GetValue(TernarySearchTreeNode<T> next)
    {
        var result = new Stack<T>();
        result.Push(next.Value);

        while (next.Parent != null && !next.Parent.Value.Equals(default(T)))
        {
            next = next.Parent;
            result.Push(next.Value);
        }

        return result.ToArray();
    }
}ParseOptions.0.json∑n
eD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\TreapTree.cs∏musing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A treap tree implementation.
/// </summary>
public class TreapTree<T> : IEnumerable<T> where T : IComparable
{
    private readonly Random rndGenerator = new();

    public TreapTree()
    {
    }

    /// <summary>
    ///     Initialize the BST with given sorted keys.
    ///     Time complexity: O(n).
    /// </summary>
    /// <param name="sortedCollection">The initial sorted collection.</param>
    public TreapTree(IEnumerable<T> sortedCollection) : this()
    {
        BstHelpers.ValidateSortedCollection(sortedCollection);
        var nodes = sortedCollection.Select(x => new TreapTreeNode<T>(null, x, rndGenerator.Next())).ToArray();
        Root = (TreapTreeNode<T>)BstHelpers.ToBst(nodes);
        BstHelpers.AssignCount(Root);
        Heapify(Root);
    }

    internal TreapTreeNode<T> Root { get; set; }
    public int Count => Root == null ? 0 : Root.Count;

    //Implementation for the GetEnumerator method.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BstEnumerator<T>(Root);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public bool HasItem(T value)
    {
        if (Root == null) return false;

        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    internal int GetHeight()
    {
        return GetHeight(Root);
    }

    private int GetHeight(TreapTreeNode<T> node)
    {
        if (node == null) return -1;

        return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public void Insert(T value)
    {
        if (Root == null)
        {
            Root = new TreapTreeNode<T>(null, value, rndGenerator.Next());
            return;
        }

        var newNode = Insert(Root, value);

        Heapify(newNode);
    }

    //O(log(n)) always
    private TreapTreeNode<T> Insert(TreapTreeNode<T> currentNode, T newNodeValue)
    {
        while (true)
        {
            var compareResult = currentNode.Value.CompareTo(newNodeValue);

            //current node is less than new item
            if (compareResult < 0)
            {
                //no right child
                if (currentNode.Right == null)
                {
                    //insert
                    currentNode.Right = new TreapTreeNode<T>(currentNode, newNodeValue, rndGenerator.Next());
                    return currentNode.Right;
                }

                currentNode = currentNode.Right;
            }
            //current node is greater than new node
            else if (compareResult > 0)
            {
                if (currentNode.Left == null)
                {
                    //insert
                    currentNode.Left = new TreapTreeNode<T>(currentNode, newNodeValue, rndGenerator.Next());
                    return currentNode.Left;
                }

                currentNode = currentNode.Left;
            }
            else
            {
                throw new Exception("Item exists");
            }
        }
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public int IndexOf(T item)
    {
        return Root.Position(item);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T ElementAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentNullException("index");

        return Root.KthSmallest(index).Value;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public void Delete(T value)
    {
        if (Root == null) throw new Exception("Empty TreapTree");

        Delete(Root, value);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentException("index");

        var nodeToDelete = Root.KthSmallest(index) as TreapTreeNode<T>;

        Delete(nodeToDelete, nodeToDelete.Value);

        return nodeToDelete.Value;
    }

    private void Delete(TreapTreeNode<T> node, T value)
    {
        while (true)
        {
            if (node != null)
            {
                var compareResult = node.Value.CompareTo(value);

                //node is less than the search value so move right to find the deletion node
                if (compareResult < 0)
                {
                    node = node.Right ?? throw new Exception("Item do not exist");
                    continue;
                }
                //node is less than the search value so move left to find the deletion node

                if (compareResult > 0)
                {
                    node = node.Left ?? throw new Exception("Item do not exist");
                    continue;
                }
            }

            //node is a leaf node
            if (node != null && node.IsLeaf)
            {
                DeleteLeaf(node);
            }
            else
            {
                //case one - right tree is null (move sub tree up)
                if (node?.Left != null && node.Right == null)
                {
                    DeleteLeftNode(node);
                }
                //case two - left tree is null  (move sub tree up)
                else if (node?.Right != null && node.Left == null)
                {
                    DeleteRightNode(node);
                }
                //case three - two child trees 
                //replace the node value with maximum element of left subtree (left max node)
                //and then delete the left max node
                else
                {
                    if (node != null)
                    {
                        var maxLeftNode = FindMax(node.Left);

                        node.Value = maxLeftNode.Value;

                        //delete left max node
                        node = node.Left;
                        value = maxLeftNode.Value;
                    }

                    continue;
                }
            }

            break;
        }

        node.UpdateCounts(true);
    }

    private void DeleteLeaf(TreapTreeNode<T> node)
    {
        //if node is root
        if (node.Parent == null)
            Root = null;
        //assign nodes parent.left/right to null
        else if (node.IsLeftChild)
            node.Parent.Left = null;
        else
            node.Parent.Right = null;
    }

    private void DeleteRightNode(TreapTreeNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Right.Parent = null;
            Root = Root.Right;
            return;
        }

        //node is left child of parent
        if (node.IsLeftChild)
            node.Parent.Left = node.Right;
        //node is right child of parent
        else
            node.Parent.Right = node.Right;

        node.Right.Parent = node.Parent;
    }

    private void DeleteLeftNode(TreapTreeNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Left.Parent = null;
            Root = Root.Left;
            return;
        }

        //node is left child of parent
        if (node.IsLeftChild)
            node.Parent.Left = node.Left;
        //node is right child of parent
        else
            node.Parent.Right = node.Left;

        node.Left.Parent = node.Parent;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T FindMax()
    {
        return FindMax(Root).Value;
    }

    private TreapTreeNode<T> FindMax(TreapTreeNode<T> node)
    {
        while (true)
        {
            if (node.Right == null) return node;
            node = node.Right;
        }
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T FindMin()
    {
        return FindMin(Root).Value;
    }

    private TreapTreeNode<T> FindMin(TreapTreeNode<T> node)
    {
        while (true)
        {
            if (node.Left == null) return node;

            node = node.Left;
        }
    }


    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    private TreapTreeNode<T> Find(TreapTreeNode<T> parent, T value)
    {
        while (true)
        {
            if (parent == null) return null;

            if (parent.Value.CompareTo(value) == 0) return parent;

            var left = Find(parent.Left, value);

            if (left != null) return left;

            parent = parent.Right;
        }
    }

    //reorder the tree node so that heap property is valid
    private void Heapify(TreapTreeNode<T> node)
    {
        while (node.Parent != null)
        {
            node.UpdateCounts();
            if (node.Priority < node.Parent.Priority)
                node = node.IsLeftChild ? RightRotate(node.Parent) : LeftRotate(node.Parent);
            else
                break;
        }

        node.UpdateCounts(true);
    }

    /// <summary>
    ///     Rotates current root right and returns the new root node
    /// </summary>
    private TreapTreeNode<T> RightRotate(TreapTreeNode<T> currentRoot)
    {
        var prevRoot = currentRoot;
        var leftRightChild = prevRoot.Left.Right;

        var newRoot = currentRoot.Left;

        //make left child as root
        prevRoot.Left.Parent = prevRoot.Parent;

        if (prevRoot.Parent != null)
        {
            if (prevRoot.Parent.Left == prevRoot)
                prevRoot.Parent.Left = prevRoot.Left;
            else
                prevRoot.Parent.Right = prevRoot.Left;
        }

        //move prev root as right child of current root
        newRoot.Right = prevRoot;
        prevRoot.Parent = newRoot;

        //move right child of left child of prev root to left child of right child of new root
        newRoot.Right.Left = leftRightChild;
        if (newRoot.Right.Left != null) newRoot.Right.Left.Parent = newRoot.Right;

        newRoot.Left.UpdateCounts();
        newRoot.Right.UpdateCounts();
        newRoot.UpdateCounts();

        if (prevRoot == Root) Root = newRoot;

        return newRoot;
    }

    /// <summary>
    ///     Rotates the current root left and returns new root
    /// </summary>
    private TreapTreeNode<T> LeftRotate(TreapTreeNode<T> currentRoot)
    {
        var prevRoot = currentRoot;
        var rightLeftChild = prevRoot.Right.Left;

        var newRoot = currentRoot.Right;

        //make right child as root
        prevRoot.Right.Parent = prevRoot.Parent;

        if (prevRoot.Parent != null)
        {
            if (prevRoot.Parent.Left == prevRoot)
                prevRoot.Parent.Left = prevRoot.Right;
            else
                prevRoot.Parent.Right = prevRoot.Right;
        }


        //move prev root as left child of current root
        newRoot.Left = prevRoot;
        prevRoot.Parent = newRoot;

        //move left child of right child of prev root to right child of left child of new root
        newRoot.Left.Right = rightLeftChild;
        if (newRoot.Left.Right != null) newRoot.Left.Right.Parent = newRoot.Left;

        newRoot.Left.UpdateCounts();
        newRoot.Right.UpdateCounts();
        newRoot.UpdateCounts();

        if (prevRoot == Root) Root = newRoot;

        return newRoot;
    }

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    private BstNodeBase<T> Find(T value)
    {
        return Root.Find(value).Item1;
    }

    /// <summary>
    ///     Get the next lower value to given value in this BST.
    ///     Time complexity: O(n).
    /// </summary>
    public T NextLower(T value)
    {
        var node = Find(value);
        if (node == null) return default;

        var next = node.NextLower();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Get the next higher value to given value in this BST.
    ///     Time complexity: O(n).
    /// </summary>
    public T NextHigher(T value)
    {
        var node = Find(value);
        if (node == null) return default;

        var next = node.NextHigher();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Descending enumerable.
    /// </summary>
    public IEnumerable<T> AsEnumerableDesc()
    {
        return GetEnumeratorDesc().AsEnumerable();
    }

    public IEnumerator<T> GetEnumeratorDesc()
    {
        return new BstEnumerator<T>(Root, false);
    }
}

internal class TreapTreeNode<T> : BstNodeBase<T> where T : IComparable
{
    internal TreapTreeNode(TreapTreeNode<T> parent, T value, int priority)
    {
        Parent = parent;
        Value = value;
        Priority = priority;
    }

    internal new TreapTreeNode<T> Parent
    {
        get => (TreapTreeNode<T>)base.Parent;
        set => base.Parent = value;
    }

    internal new TreapTreeNode<T> Left
    {
        get => (TreapTreeNode<T>)base.Left;
        set => base.Left = value;
    }

    internal new TreapTreeNode<T> Right
    {
        get => (TreapTreeNode<T>)base.Right;
        set => base.Right = value;
    }

    internal int Priority { get; set; }
}ParseOptions.0.json™0
`D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\Tree.cs∞/using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A tree implementation.
/// </summary>
public class Tree<T> : IEnumerable<T> where T : IComparable
{
    private TreeNode<T> Root { get; set; }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new TreeEnumerator<T>(Root);
    }

    /// <summary>
    ///     Time complexity:  O(n)
    /// </summary>
    public bool HasItem(T value)
    {
        if (Root == null) return false;

        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Time complexity:  O(n)
    /// </summary>
    public int GetHeight()
    {
        return GetHeight(Root);
    }

    /// <summary>
    ///     Time complexity:  O(n)
    /// </summary>
    public void Insert(T parent, T child)
    {
        if (Root == null)
        {
            Root = new TreeNode<T>(null, child);
            Count++;
            return;
        }

        var parentNode = Find(parent);

        if (parentNode == null) throw new ArgumentNullException();

        var exists = Find(Root, child) != null;

        if (exists) throw new ArgumentException("value already exists");

        parentNode.Children.InsertFirst(new TreeNode<T>(parentNode, child));
        Count++;
    }

    /// <summary>
    ///     Time complexity:  O(n)
    /// </summary>
    public void Delete(T value)
    {
        Delete(Root.Value, value);
    }

    /// <summary>
    ///     Time complexity:  O(n)
    /// </summary>
    public IEnumerable<T> Children(T value)
    {
        return Find(value)?.Children.Select(x => x.Value);
    }

    private TreeNode<T> Find(T value)
    {
        if (Root == null) return null;

        return Find(Root, value);
    }

    private int GetHeight(TreeNode<T> node)
    {
        if (node == null) return -1;

        var currentHeight = -1;

        foreach (var child in node.Children)
        {
            var childHeight = GetHeight(child);

            if (currentHeight < childHeight) currentHeight = childHeight;
        }

        currentHeight++;

        return currentHeight;
    }

    private void Delete(T parentValue, T value)
    {
        var parent = Find(parentValue);

        if (parent == null) throw new Exception("Cannot find parent");

        var itemToRemove = Find(parent, value);

        if (itemToRemove == null) throw new Exception("Cannot find item");

        //if item is root
        if (itemToRemove.Parent == null)
        {
            if (itemToRemove.Children.Count() == 0)
            {
                Root = null;
            }
            else
            {
                if (itemToRemove.Children.Count() == 1)
                {
                    Root = itemToRemove.Children.DeleteFirst();
                    Root.Parent = null;
                }
                else
                {
                    throw new Exception("Node have multiple children. Cannot delete node unambiguosly");
                }
            }
        }
        else
        {
            if (itemToRemove.Children.Count() == 0)
            {
                itemToRemove.Parent.Children.Delete(itemToRemove);
            }
            else
            {
                if (itemToRemove.Children.Count() == 1)
                {
                    var orphan = itemToRemove.Children.DeleteFirst();
                    orphan.Parent = itemToRemove.Parent;

                    itemToRemove.Parent.Children.InsertFirst(orphan);
                    itemToRemove.Parent.Children.Delete(itemToRemove);
                }
                else
                {
                    throw new Exception("Node have multiple children. Cannot delete node unambiguosly");
                }
            }
        }

        Count--;
    }

    private TreeNode<T> Find(TreeNode<T> parent, T value)
    {
        if (parent.Value.CompareTo(value) == 0) return parent;

        foreach (var child in parent.Children)
        {
            var result = Find(child, value);

            if (result != null) return result;
        }

        return null;
    }
}

internal class TreeNode<T> : IComparable where T : IComparable
{
    internal TreeNode(TreeNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;

        Children = new SinglyLinkedList<TreeNode<T>>();
    }

    internal T Value { get; set; }

    internal TreeNode<T> Parent { get; set; }
    internal SinglyLinkedList<TreeNode<T>> Children { get; set; }

    internal bool IsLeaf => Children.Count() == 0;

    public int CompareTo(object obj)
    {
        return Value.CompareTo(obj as TreeNode<T>);
    }
}

internal class TreeEnumerator<T> : IEnumerator<T> where T : IComparable
{
    private readonly TreeNode<T> root;
    private Stack<TreeNode<T>> progress;

    internal TreeEnumerator(TreeNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null)
        {
            progress = new Stack<TreeNode<T>>(root.Children);
            Current = root.Value;
            return true;
        }

        if (progress.Count > 0)
        {
            var next = progress.Pop();
            Current = next.Value;

            foreach (var child in next.Children) progress.Push(child);

            return true;
        }

        return false;
    }

    public void Reset()
    {
        progress = null;
        Current = default;
    }

    public T Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        progress = null;
    }
}ParseOptions.0.json‡=
`D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\DataStructures\Tree\Trie.csÊ<using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A trie (prefix tree) implementation.
/// </summary>
public class Trie<T> : IEnumerable<T[]>
{
    public Trie()
    {
        Root = new TrieNode<T>(null, default);
        Count = 0;
    }

    private TrieNode<T> Root { get; }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T[]> GetEnumerator()
    {
        return new TrieEnumerator<T>(Root);
    }

    /// <summary>
    ///     Insert a new record to this trie.
    ///     Time complexity: O(m) where m is the length of entry.
    /// </summary>
    public void Insert(T[] entry)
    {
        Insert(Root, entry, 0);
        Count++;
    }

    /// <summary>
    ///     Insert a new record to this trie after finding the end recursively.
    /// </summary>
    private void Insert(TrieNode<T> currentNode, T[] entry, int currentIndex)
    {
        while (true)
        {
            if (currentIndex == entry.Length)
            {
                currentNode.IsEnd = true;
                return;
            }

            if (currentNode.Children.ContainsKey(entry[currentIndex]) == false)
            {
                var newNode = new TrieNode<T>(currentNode, entry[currentIndex]);
                currentNode.Children.Add(entry[currentIndex], newNode);
                currentNode = newNode;
                currentIndex = currentIndex + 1;
            }
            else
            {
                currentNode = currentNode.Children[entry[currentIndex]];
                currentIndex = currentIndex + 1;
            }
        }
    }

    /// <summary>
    ///     Deletes a record from this trie.
    ///     Time complexity: O(m) where m is the length of entry.
    /// </summary>
    public void Delete(T[] entry)
    {
        Delete(Root, entry, 0);
        Count--;
    }

    /// <summary>
    ///     Deletes a record from this trie after finding it recursively.
    /// </summary>
    private void Delete(TrieNode<T> currentNode, T[] entry, int currentIndex)
    {
        if (currentIndex == entry.Length)
        {
            if (!currentNode.IsEnd) throw new Exception("Item not in trie.");

            currentNode.IsEnd = false;
            return;
        }

        if (currentNode.Children.ContainsKey(entry[currentIndex]) == false) throw new Exception("Item not in trie.");

        Delete(currentNode.Children[entry[currentIndex]], entry, currentIndex + 1);

        if (currentNode.Children[entry[currentIndex]].IsEmpty
            && !currentNode.IsEnd)
            currentNode.Children.Remove(entry[currentIndex]);
    }

    /// <summary>
    ///     Returns a list of records matching this prefix.
    ///     Time complexity: O(rm) where r is the number of results and m is the average length of each entry.
    /// </summary>
    public List<T[]> StartsWith(T[] prefix)
    {
        return StartsWith(Root, prefix, 0);
    }

    /// <summary>
    ///     Recursively visit until end of prefix
    ///     and then gather all sub entries under it.
    /// </summary>
    private List<T[]> StartsWith(TrieNode<T> currentNode, T[] searchPrefix, int currentIndex)
    {
        while (true)
        {
            if (currentIndex == searchPrefix.Length)
            {
                var result = new List<T[]>();

                //gather sub entries and prefix them with search entry prefix
                GatherStartsWith(result, searchPrefix, new List<T>(), currentNode);

                return result;
            }

            if (currentNode.Children.ContainsKey(searchPrefix[currentIndex]) == false) return new List<T[]>();

            currentNode = currentNode.Children[searchPrefix[currentIndex]];
            currentIndex = currentIndex + 1;
        }
    }

    /// <summary>
    ///     Gathers all suffixes under this node appending with the given prefix.
    /// </summary>
    private void GatherStartsWith(List<T[]> result, T[] searchPrefix, List<T> suffix,
        TrieNode<T> node)
    {
        //end of word
        if (node.IsEnd)
        {
            if (suffix != null)
                result.Add(searchPrefix.Concat(suffix).ToArray());
            else
                result.Add(searchPrefix);
        }

        //visit all children
        foreach (var child in node.Children)
        {
            //append to end of prefix for new prefix
            suffix.Add(child.Key);
            GatherStartsWith(result, searchPrefix, suffix, child.Value);
            suffix.RemoveAt(suffix.Count - 1);
        }
    }

    /// <summary>
    ///     Returns true if the entry exist.
    ///     Time complexity: O(e) where e is the length of the given entry.
    /// </summary>
    public bool Contains(T[] entry)
    {
        return Contains(Root, entry, 0, false);
    }

    /// <summary>
    ///     Returns true if any records match this prefix.
    ///     Time complexity: O(e) where e is the length of the given entry.
    /// </summary>
    public bool ContainsPrefix(T[] prefix)
    {
        return Contains(Root, prefix, 0, true);
    }

    /// <summary>
    ///     Find if the record exist recursively.
    /// </summary>
    private bool Contains(TrieNode<T> currentNode, T[] entry, int currentIndex, bool isPrefixSearch)
    {
        while (true)
        {
            if (currentIndex == entry.Length) return isPrefixSearch || currentNode.IsEnd;

            if (currentNode.Children.ContainsKey(entry[currentIndex]) == false) return false;

            currentNode = currentNode.Children[entry[currentIndex]];
            currentIndex = currentIndex + 1;
        }
    }
}

internal class TrieNode<T>
{
    internal TrieNode(TrieNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;
        Children = new Dictionary<T, TrieNode<T>>();
    }

    internal bool IsEmpty => Children.Count == 0;
    internal bool IsEnd { get; set; }
    internal TrieNode<T> Parent { get; set; }
    internal Dictionary<T, TrieNode<T>> Children { get; set; }
    internal T Value { get; set; }
}

internal class TrieEnumerator<T> : IEnumerator<T[]>
{
    private readonly TrieNode<T> root;
    private Stack<TrieNode<T>> progress;

    internal TrieEnumerator(TrieNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null) progress = new Stack<TrieNode<T>>(root.Children.Select(x => x.Value));

        while (progress.Count > 0)
        {
            var next = progress.Pop();

            foreach (var child in next.Children) progress.Push(child.Value);

            if (next.IsEnd)
            {
                Current = GetValue(next);
                return true;
            }
        }

        return false;
    }

    public void Reset()
    {
        progress = null;
        Current = null;
    }

    public T[] Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        progress = null;
    }

    private T[] GetValue(TrieNode<T> next)
    {
        var result = new Stack<T>();
        result.Push(next.Value);

        while (next.Parent != null && !next.Parent.Value.Equals(default(T)))
        {
            next = next.Parent;
            result.Push(next.Value);
        }

        return result.ToArray();
    }
}ParseOptions.0.json≈
^D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Distributed\AsyncQueue.csÕusing System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Distributed;

/// <summary>
///     A simple asynchronous multi-thread supporting producer/consumer FIFO queue with minimal locking.
/// </summary>
public class AsyncQueue<T>
{
    //consumer task queue and lock.
    private readonly Queue<TaskCompletionSource<T>> consumerQueue = new();

    //data queue.
    private readonly Queue<T> queue = new();
    private readonly SemaphoreSlim consumerQueueLock = new(1);

    public int Count => queue.Count;

    /// <summary>
    ///     Supports multi-threaded producers.
    ///     Time complexity: O(1).
    /// </summary>
    public async Task EnqueueAsync(T value, int millisecondsTimeout = int.MaxValue,
        CancellationToken taskCancellationToken = default)
    {
        await consumerQueueLock.WaitAsync(millisecondsTimeout, taskCancellationToken);

        if (consumerQueue.Count > 0)
        {
            var consumer = consumerQueue.Dequeue();
            consumer.TrySetResult(value);
        }
        else
        {
            queue.Enqueue(value);
        }

        consumerQueueLock.Release();
    }

    /// <summary>
    ///     Supports multi-threaded consumers.
    ///     Time complexity: O(1).
    /// </summary>
    public async Task<T> DequeueAsync(int millisecondsTimeout = int.MaxValue,
        CancellationToken taskCancellationToken = default)
    {
        await consumerQueueLock.WaitAsync(millisecondsTimeout, taskCancellationToken);

        TaskCompletionSource<T> consumer;

        try
        {
            if (queue.Count > 0)
            {
                var result = queue.Dequeue();
                return result;
            }

            consumer = new TaskCompletionSource<T>();
            taskCancellationToken.Register(() => consumer.TrySetCanceled());
            consumerQueue.Enqueue(consumer);
        }
        finally
        {
            consumerQueueLock.Release();
        }

        return await consumer.Task;
    }
}ParseOptions.0.json´
aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Distributed\CircularQueue.cs∞using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Distributed;

/// <summary>
///     Cicular queue aka Ring Buffer using fixed size array.
/// </summary>
public class CircularQueue<T>
{
    //points to the index new element should be inserted
    private int end;
    private readonly T[] queue;

    //points to the index of next element to be deleted
    private int start;

    public CircularQueue(int size)
    {
        queue = new T[size];
    }

    public int Count { get; private set; }

    /// <summary>
    ///     Note: When buffer overflows oldest data will be erased.
    ///     Time complexity: O(1)
    /// </summary>
    public T Enqueue(T data)
    {
        var deleted = default(T);

        //wrap around removing oldest element
        if (end > queue.Length - 1)
        {
            end = 0;

            if (start == 0)
            {
                deleted = queue[start];
                start++;
            }
        }

        //when end meets start after wraping around
        if (end == start && Count > 1)
        {
            deleted = queue[start];
            start++;
        }

        queue[end] = data;
        end++;

        if (Count < queue.Length) Count++;

        return deleted;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    /// <returns>Deleted items.</returns>
    public IEnumerable<T> Enqueue(T[] bulk)
    {
        return bulk.Select(item => Enqueue(item))
            .Where(deleted => !deleted.Equals(default(T))).ToList();
    }

    /// <summary>
    ///     O(1) Time complexity.
    /// </summary>
    public T Dequeue()
    {
        if (Count == 0) throw new Exception("Empty queue.");

        var element = queue[start];
        start++;

        //wrap around 
        if (start > queue.Length - 1)
        {
            start = 0;

            if (end == 0) end++;
        }

        Count--;

        if (start == end && Count > 1) end++;

        //reset
        if (Count == 0) start = end = 0;

        return element;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public IEnumerable<T> Dequeue(int bulkNumber)
    {
        var deletedList = new List<T>();
        while (bulkNumber > 0 && Count > 0)
        {
            var deleted = Dequeue();

            if (!deleted.Equals(default(T))) deletedList.Add(deleted);

            bulkNumber--;
        }

        return deletedList;
    }
}ParseOptions.0.jsonı#
bD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Distributed\ConsistentHash.cs˘"using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Advanced.Algorithms.Distributed;

/// <summary>
///     A consistant hash implementation with murmur hash.
/// </summary>
public class ConsistentHash<T>
{
    private readonly SortedDictionary<int, T> circle = new();
    private readonly int replicas;
    private int[] circleKeys;

    public ConsistentHash()
        : this(new List<T>(), 100)
    {
    }

    public ConsistentHash(IEnumerable<T> nodes, int replicas)
    {
        this.replicas = replicas;
        foreach (var node in nodes) AddNode(node);
    }

    /// <summary>
    ///     Add a new bucket.
    /// </summary>
    public void AddNode(T node)
    {
        for (var i = 0; i < replicas; i++)
        {
            var hash = GetHashCode(node.GetHashCode().ToString() + i);
            circle[hash] = node;
        }

        circleKeys = circle.Keys.ToArray();
    }

    /// <summary>
    ///     Get the bucket for the given Key.
    /// </summary>
    public T GetNode(string key)
    {
        var hash = GetHashCode(key);
        var first = NextClockWise(circleKeys, hash);
        return circle[circleKeys[first]];
    }

    /// <summary>
    ///     Remove a bucket from lookup.
    /// </summary>
    public void RemoveNode(T node)
    {
        for (var i = 0; i < replicas; i++)
        {
            var hash = GetHashCode(node.GetHashCode().ToString() + i);
            if (!circle.Remove(hash)) throw new Exception("Cannot remove a node that was never added.");
        }

        circleKeys = circle.Keys.ToArray();
    }


    /// <summary>
    ///     Move clockwise until we find a bucket with Key >= hashCode
    /// </summary>
    /// <returns>Returns the index of bucket</returns>
    private int NextClockWise(int[] keys, int hashCode)
    {
        var begin = 0;
        var end = keys.Length - 1;

        if (keys[end] < hashCode || keys[0] > hashCode) return 0;

        //do a binary search
        while (end - begin > 1)
        {
            var mid = (end + begin) / 2;
            if (keys[mid] >= hashCode)
                end = mid;
            else
                begin = mid;
        }

        return end;
    }

    private static int GetHashCode(string key)
    {
        return (int)MurmurHash2.Hash(Encoding.Unicode.GetBytes(key));
    }
}

/// <summary>
///     Adapted from https://github.com/wsq003/consistent-hash/blob/master/ConsistentHash.cs
/// </summary>
internal class MurmurHash2
{
    private const uint M = 0x5bd1e995;
    private const int R = 24;

    internal static uint Hash(byte[] data)
    {
        return Hash(data, 0xc58f1a7b);
    }

    internal static uint Hash(byte[] data, uint seed)
    {
        var length = data.Length;
        if (length == 0)
            return 0;
        var h = seed ^ (uint)length;
        var currentIndex = 0;
        // array will be length of Bytes but contains Uints
        // therefore the currentIndex will jump with +1 while length will jump with +4
        var hackArray = new BytetoUInt32Converter { Bytes = data }.UInts;
        while (length >= 4)
        {
            var k = hackArray[currentIndex++];
            k *= M;
            k ^= k >> R;
            k *= M;

            h *= M;
            h ^= k;
            length -= 4;
        }

        currentIndex *= 4; // fix the length
        switch (length)
        {
            case 3:
                h ^= (ushort)(data[currentIndex++] | (data[currentIndex++] << 8));
                h ^= (uint)data[currentIndex] << 16;
                h *= M;
                break;
            case 2:
                h ^= (ushort)(data[currentIndex++] | (data[currentIndex] << 8));
                h *= M;
                break;
            case 1:
                h ^= data[currentIndex];
                h *= M;
                break;
        }

        // Do a few final mixes of the hash to ensure the last few
        // bytes are well-incorporated.

        h ^= h >> 13;
        h *= M;
        h ^= h >> 15;

        return h;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct BytetoUInt32Converter
    {
        [FieldOffset(0)] public byte[] Bytes;

        [FieldOffset(0)] public readonly uint[] UInts;
    }
}ParseOptions.0.jsonï
\D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Distributed\LRUCache.csüusing System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Distributed;

/// <summary>
///     A least recently used cache implemetation.
/// </summary>
public class LruCache<TK, TV>
{
    private readonly int capacity;

    private readonly DoublyLinkedList<Tuple<TK, TV>> dll = new();

    private readonly Dictionary<TK, DoublyLinkedListNode<Tuple<TK, TV>>> lookUp = new();

    public LruCache(int capacity)
    {
        if (capacity <= 0) throw new Exception("Capacity must be a positive integer.");
        this.capacity = capacity;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public TV Get(TK key)
    {
        if (!lookUp.ContainsKey(key))
            return default;

        var node = lookUp[key];

        //move lately used node to beginning of ddl 
        dll.Delete(node);
        var newNode = dll.InsertFirst(node.Data);
        lookUp[key] = newNode;

        return node.Data.Item2;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Put(TK key, TV value)
    {
        //evict last node of ddl if capacity overflows
        if (lookUp.Count == capacity)
        {
            var nodeToEvict = dll.Last();
            lookUp.Remove(nodeToEvict.Item1);
            dll.DeleteLast();
        }

        //insert
        var newNode = dll.InsertFirst(new Tuple<TK, TV>(key, value));
        lookUp.Add(key, newNode);
    }
}ParseOptions.0.json±o
_D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\BentleyOttmann.cs∏nusing System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Bentley-Ottmann sweep line algorithm to find line intersections.
/// </summary>
public class BentleyOttmann
{
    private readonly PointComparer pointComparer;

    internal readonly double Tolerance;
    private RedBlackTree<Event> currentlyTrackedLines;

    private BHeap<Event> eventQueue;
    private HashSet<Event> eventQueueLookUp;

    private Dictionary<Point, HashSet<Tuple<Event, Event>>> intersectionEvents;
    private HashSet<Event> otherLines;

    private Dictionary<Event, Event> rightLeftEventLookUp;

    internal Line SweepLine;

    private HashSet<Event> verticalAndHorizontalLines;

    public BentleyOttmann(int precision = 5)
    {
        pointComparer = new PointComparer();
        Tolerance = Math.Round(Math.Pow(0.1, precision), precision);
    }

    private void Initialize(IEnumerable<Line> lineSegments)
    {
        SweepLine = new Line(new Point(0, 0), new Point(0, int.MaxValue), Tolerance);

        currentlyTrackedLines = new RedBlackTree<Event>(true, pointComparer);
        intersectionEvents = new Dictionary<Point, HashSet<Tuple<Event, Event>>>(pointComparer);

        verticalAndHorizontalLines = new HashSet<Event>();
        otherLines = new HashSet<Event>();

        rightLeftEventLookUp = lineSegments
            .Select(x =>
            {
                if (x.Left.X < 0 || x.Left.Y < 0 || x.Right.X < 0 || x.Right.Y < 0)
                    throw new Exception("Negative coordinates are not supported.");

                return new KeyValuePair<Event, Event>(
                    new Event(x.Left, pointComparer, EventType.Start, x, this),
                    new Event(x.Right, pointComparer, EventType.End, x, this)
                );
            }).ToDictionary(x => x.Value, x => x.Key);

        eventQueueLookUp = new HashSet<Event>(rightLeftEventLookUp.SelectMany(x => new[]
        {
            x.Key,
            x.Value
        }));

        eventQueue = new BHeap<Event>(SortDirection.Ascending, eventQueueLookUp, new EventQueueComparer());
    }

    public Dictionary<Point, List<Line>> FindIntersections(IEnumerable<Line> lineSegments)
    {
        Initialize(lineSegments);

        while (eventQueue.Count > 0)
        {
            var currentEvent = eventQueue.Extract();
            eventQueueLookUp.Remove(currentEvent);
            SweepTo(currentEvent);

            switch (currentEvent.Type)
            {
                case EventType.Start:

                    //special case
                    if (verticalAndHorizontalLines.Count > 0)
                        foreach (var line in verticalAndHorizontalLines)
                        {
                            var intersection = FindIntersection(currentEvent, line);
                            RecordIntersection(currentEvent, line, intersection);
                        }

                    //special case
                    if (currentEvent.Segment.IsVertical || currentEvent.Segment.IsHorizontal)
                    {
                        verticalAndHorizontalLines.Add(currentEvent);

                        foreach (var line in otherLines)
                        {
                            var intersection = FindIntersection(currentEvent, line);
                            RecordIntersection(currentEvent, line, intersection);
                        }

                        break;
                    }

                    otherLines.Add(currentEvent);

                    currentlyTrackedLines.Insert(currentEvent);

                    var lower = currentlyTrackedLines.NextLower(currentEvent);
                    var upper = currentlyTrackedLines.NextHigher(currentEvent);

                    var lowerIntersection = FindIntersection(currentEvent, lower);
                    RecordIntersection(currentEvent, lower, lowerIntersection);
                    EnqueueIntersectionEvent(currentEvent, lowerIntersection);

                    var upperIntersection = FindIntersection(currentEvent, upper);
                    RecordIntersection(currentEvent, upper, upperIntersection);
                    EnqueueIntersectionEvent(currentEvent, upperIntersection);

                    break;

                case EventType.End:

                    currentEvent = rightLeftEventLookUp[currentEvent];

                    //special case
                    if (currentEvent.Segment.IsVertical || currentEvent.Segment.IsHorizontal)
                    {
                        verticalAndHorizontalLines.Remove(currentEvent);
                        break;
                    }

                    otherLines.Remove(currentEvent);

                    lower = currentlyTrackedLines.NextLower(currentEvent);
                    upper = currentlyTrackedLines.NextHigher(currentEvent);

                    currentlyTrackedLines.Delete(currentEvent);

                    var upperLowerIntersection = FindIntersection(lower, upper);
                    RecordIntersection(lower, upper, upperLowerIntersection);
                    EnqueueIntersectionEvent(currentEvent, upperLowerIntersection);

                    break;

                case EventType.Intersection:

                    var intersectionLines = intersectionEvents[currentEvent];

                    foreach (var lines in intersectionLines)
                    {
                        //special case
                        if (lines.Item1.Segment.IsHorizontal || lines.Item1.Segment.IsVertical
                                                             || lines.Item2.Segment.IsHorizontal ||
                                                             lines.Item2.Segment.IsVertical)
                            continue;

                        SwapBstNodes(currentlyTrackedLines, lines.Item1, lines.Item2);

                        var upperLine = lines.Item1;
                        var upperUpper = currentlyTrackedLines.NextHigher(upperLine);

                        var newUpperIntersection = FindIntersection(upperLine, upperUpper);
                        RecordIntersection(upperLine, upperUpper, newUpperIntersection);
                        EnqueueIntersectionEvent(currentEvent, newUpperIntersection);

                        var lowerLine = lines.Item2;
                        var lowerLower = currentlyTrackedLines.NextLower(lowerLine);

                        var newLowerIntersection = FindIntersection(lowerLine, lowerLower);
                        RecordIntersection(lowerLine, lowerLower, newLowerIntersection);
                        EnqueueIntersectionEvent(currentEvent, newLowerIntersection);
                    }

                    break;
            }
        }

        return intersectionEvents.ToDictionary(x => x.Key,
            x => x.Value.SelectMany(y => new[] { y.Item1.Segment, y.Item2.Segment })
                .Distinct().ToList());
    }

    private void SweepTo(Event currentEvent)
    {
        SweepLine = new Line(new Point(currentEvent.X, 0), new Point(currentEvent.X, int.MaxValue), Tolerance);
    }

    internal void SwapBstNodes(RedBlackTree<Event> currentlyTrackedLines, Event value1, Event value2)
    {
        var node1 = currentlyTrackedLines.Find(value1).Item1;
        var node2 = currentlyTrackedLines.Find(value2).Item1;

        if (node1 == null || node2 == null) throw new Exception("Value1, Value2 or both was not found in this BST.");

        var tmp = node1.Value;
        node1.Value = node2.Value;
        node2.Value = tmp;

        currentlyTrackedLines.NodeLookUp[node1.Value] = node1;
        currentlyTrackedLines.NodeLookUp[node2.Value] = node2;
    }

    private void EnqueueIntersectionEvent(Event currentEvent, Point intersection)
    {
        if (intersection == null) return;

        var intersectionEvent = new Event(intersection, pointComparer, EventType.Intersection, null, this);

        if (intersectionEvent.X > SweepLine.Left.X
            || intersectionEvent.X == SweepLine.Left.X
            && intersectionEvent.Y > currentEvent.Y)
            if (!eventQueueLookUp.Contains(intersectionEvent))
            {
                eventQueue.Insert(intersectionEvent);
                eventQueueLookUp.Add(intersectionEvent);
            }
    }

    private Point FindIntersection(Event a, Event b)
    {
        if (a == null || b == null
                      || a.Type == EventType.Intersection
                      || b.Type == EventType.Intersection)
            return null;

        return a.Segment.Intersection(b.Segment, Tolerance);
    }

    private void RecordIntersection(Event line1, Event line2, Point intersection)
    {
        if (intersection == null) return;

        var existing = intersectionEvents.ContainsKey(intersection)
            ? intersectionEvents[intersection]
            : new HashSet<Tuple<Event, Event>>();

        if (line1.Segment.Slope.CompareTo(line2.Segment.Slope) > 0)
            existing.Add(new Tuple<Event, Event>(line1, line2));
        else
            existing.Add(new Tuple<Event, Event>(line2, line1));

        intersectionEvents[intersection] = existing;
    }
}

//point type
internal enum EventType
{
    Start = 0,
    Intersection = 1,
    End = 2
}

/// <summary>
///     A custom object representing start/end/intersection point.
/// </summary>
internal class Event : Point, IComparable
{
    private readonly PointComparer pointComparer;
    private readonly double tolerance;

    internal BentleyOttmann Algorithm;
    internal Point LastIntersection;

    internal Line LastSweepLine;

    //The full line only if not an intersection event
    internal Line Segment;

    internal EventType Type;

    internal Event(Point eventPoint, PointComparer pointComparer, EventType eventType,
        Line lineSegment, BentleyOttmann algorithm)
        : base(eventPoint.X, eventPoint.Y)
    {
        tolerance = algorithm.Tolerance;
        this.pointComparer = pointComparer;

        Type = eventType;
        Segment = lineSegment;
        Algorithm = algorithm;
    }

    public int CompareTo(object that)
    {
        if (Equals(that)) return 0;

        var thatEvent = that as Event;

        var line1 = Segment;
        var line2 = thatEvent.Segment;

        Point intersectionA;
        if (Type == EventType.Intersection)
        {
            intersectionA = this;
        }
        else
        {
            if (LastSweepLine == Algorithm.SweepLine)
            {
                intersectionA = LastIntersection;
            }
            else
            {
                intersectionA = LineIntersection.FindIntersection(line1, Algorithm.SweepLine, tolerance);
                LastSweepLine = Algorithm.SweepLine;
                LastIntersection = intersectionA;
            }
        }

        Point intersectionB;
        if (Type == EventType.Intersection)
        {
            intersectionB = thatEvent;
        }
        else
        {
            if (thatEvent.LastSweepLine == thatEvent.Algorithm.SweepLine)
            {
                intersectionB = thatEvent.LastIntersection;
            }
            else
            {
                intersectionB = LineIntersection.FindIntersection(line2, thatEvent.Algorithm.SweepLine, tolerance);
                thatEvent.LastSweepLine = thatEvent.Algorithm.SweepLine;
                thatEvent.LastIntersection = intersectionB;
            }
        }

        var result = intersectionA.Y.CompareTo(intersectionB.Y);
        if (result != 0) return result;

        //if Y is same use slope as comparison
        var slope1 = line1.Slope;

        //if Y is same use slope as comparison
        var slope2 = line2.Slope;

        result = slope1.CompareTo(slope2);
        if (result != 0) return result;

        //if slope is the same use diff of X co-ordinate
        result = line1.Left.X.CompareTo(line2.Left.X);
        if (result != 0) return result;

        //if diff of X co-ordinate is same use diff of Y co-ordinate
        result = line1.Left.Y.CompareTo(line2.Left.Y);

        //at this point this is guaranteed to be not same.
        //since we don't let duplicate lines with input HashSet of lines.
        //see line equals override in Line class.
        return result;
    }

    public override bool Equals(object that)
    {
        if (that == this) return true;

        var thatEvent = that as Event;

        if (Type != EventType.Intersection && thatEvent.Type == EventType.Intersection
            || Type == EventType.Intersection && thatEvent.Type != EventType.Intersection)
            return false;

        if (Type == EventType.Intersection && thatEvent.Type == EventType.Intersection)
            return pointComparer.Equals(this, thatEvent);

        return false;
    }

    public override int GetHashCode()
    {
        // Intersection events are compared by point location (see Equals),
        // so the hash must match. Start/End events use reference equality.
        if (Type == EventType.Intersection)
            return pointComparer.GetHashCode(this);

        return base.GetHashCode();
    }
}

//Used to override event comparison when using BMinHeap for Event queue.
internal class EventQueueComparer : Comparer<Event>
{
    public override int Compare(Event a, Event b)
    {
        //same object
        if (a == b) return 0;

        //compare X
        var result = a.X.CompareTo(b.X);

        if (result != 0) return result;

        //Left event first, then intersection and finally right.
        result = a.Type.CompareTo(b.Type);

        if (result != 0) return result;

        return a.Y.CompareTo(b.Y);
    }
}ParseOptions.0.json›
aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\ClosestPointPair.cs‚using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Closest-point pair finder.
/// </summary>
public class ClosestPointPair
{
    public static double Find(List<int[]> points)
    {
        var xSorted = points
            .Select(z => new Point(z[0], z[1]))
            .OrderBy(p => p.X).ToList();

        return Find(xSorted, 0, points.Count - 1);
    }

    public static double Find(List<Point> points, int left, int right)
    {
        if (right - left <= 3) return BruteForce(points, left, right);

        var mid = (left + right) / 2;

        var leftMin = Find(points, 0, mid);
        var rightMin = Find(points, mid + 1, right);

        var min = Math.Min(leftMin, rightMin);
        var midX = points[mid].X;

        var strips = new List<Point>();

        for (var i = left; i <= right; i++)
            if (Math.Abs(points[i].X - midX) < min)
                strips.Add(points[i]);

        //vertical strips within the radius of min
        strips = strips.OrderBy(p => p.Y).ToList();

        for (var i = 0; i < strips.Count; i++)
        for (var j = i + 1; j < strips.Count && Math.Abs(strips[i].Y - strips[j].Y) < min; j++)
            //check for radius 
            min = Math.Min(min, GetDistance(strips[i], strips[j]));

        return min;
    }

    private static double BruteForce(IList<Point> points, int left, int right)
    {
        var min = double.MaxValue;
        for (var i = left; i < right; i++)
        for (var j = left + 1; j <= right; j++)
            min = Math.Min(min, GetDistance(points[i], points[j]));
        return min;
    }

    /// <summary>
    ///     Eucledian distance.
    /// </summary>
    private static double GetDistance(Point point1, Point point2)
    {
        return Math.Sqrt(Math.Pow(Math.Abs(point1.X - point2.X), 2)
                         + Math.Pow(Math.Abs(point1.Y - point2.Y), 2));
    }
}ParseOptions.0.json˘
[D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\ConvexHull.csÑusing System.Collections.Generic;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Convex hull using jarvis's algorithm.
/// </summary>
public class ConvexHull
{
    public static List<int[]> Find(List<int[]> points)
    {
        var currentPointIndex = FindLeftMostPoint(points);
        var startingPointIndex = currentPointIndex;

        var result = new List<int[]>();

        do
        {
            result.Add(points[currentPointIndex]);

            //pick a random point as next Point
            var nextPointIndex = (currentPointIndex + 1) % points.Count;

            for (var i = 0; i < points.Count; i++)
            {
                if (i == nextPointIndex) continue;

                var orientation = GetOrientation(points[currentPointIndex],
                    points[i], points[nextPointIndex]);

                if (orientation == Orientation.ClockWise) nextPointIndex = i;
            }

            currentPointIndex = nextPointIndex;
        } while (currentPointIndex != startingPointIndex);

        return result;
    }

    /// <summary>
    ///     Compute the orientation of the lines formed by points p, q and r
    /// </summary>
    private static Orientation GetOrientation(int[] p, int[] q, int[] r)
    {
        int x1 = p[0], y1 = p[1];
        int x2 = q[0], y2 = q[1];
        int x3 = r[0], y3 = r[1];

        //using slope formula => (y2-y1)/(x2-x1) = (y3-y2)/(x3-x2) (if colinear)
        // derives to (y2-y1)(x3-x2)-(y3-y2)(x2-x1) == 0 
        var result = (y2 - y1) * (x3 - x2) - (y3 - y2) * (x2 - x1);

        //sign will give the direction
        if (result < 0) return Orientation.ClockWise;

        return result > 0 ? Orientation.AntiClockWise : Orientation.Colinear;
    }


    private static int FindLeftMostPoint(List<int[]> points)
    {
        var left = 0;

        for (var i = 1; i < points.Count; i++)
            if (points[i][0] < points[left][0])
                left = i;

        return left;
    }

    private enum Orientation
    {
        ClockWise = 0,
        AntiClockWise = 1,
        Colinear = 2
    }
}ParseOptions.0.json‘<
aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\LineIntersection.csŸ;using System;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Line intersection computer.
/// </summary>
public class LineIntersection
{
    /// <summary>
    ///     Returns Point of intersection if do intersect otherwise default Point (null).
    /// </summary>
    /// <param name="precision">precision tolerance.</param>
    /// <returns>The point of intersection.</returns>
    public static Point Find(Line lineA, Line lineB, int precision = 5)
    {
        var tolerance = Math.Round(Math.Pow(0.1, precision), precision);
        return FindIntersection(lineA, lineB, tolerance);
    }

    internal static Point FindIntersection(Line lineA, Line lineB, double tolerance)
    {
        if (lineA == lineB) throw new Exception("Both lines are the same.");

        //make lineA as left
        if (lineA.Left.X.CompareTo(lineB.Left.X) > 0)
        {
            var tmp = lineA;
            lineA = lineB;
            lineB = tmp;
        }
        else if (lineA.Left.X.CompareTo(lineB.Left.X) == 0)
        {
            if (lineA.Left.Y.CompareTo(lineB.Left.Y) > 0)
            {
                var tmp = lineA;
                lineA = lineB;
                lineB = tmp;
            }
        }

        double x1 = lineA.Left.X, y1 = lineA.Left.Y;
        double x2 = lineA.Right.X, y2 = lineA.Right.Y;

        double x3 = lineB.Left.X, y3 = lineB.Left.Y;
        double x4 = lineB.Right.X, y4 = lineB.Right.Y;


        //equations of the form x=c (two vertical overlapping lines)
        if (x1 == x2 && x3 == x4 && x1 == x3)
        {
            //get the first intersection in vertical sorted order of lines
            var firstIntersection = new Point(x3, y3);

            //x,y can intersect outside the line segment since line is infinitely long
            //so finally check if x, y is within both the line segments
            if (IsInsideLine(lineA, firstIntersection, tolerance) &&
                IsInsideLine(lineB, firstIntersection, tolerance))
                return new Point(x3, y3);
        }

        //equations of the form y=c (two overlapping horizontal lines)
        if (y1 == y2 && y3 == y4 && y1 == y3)
        {
            //get the first intersection in horizontal sorted order of lines
            var firstIntersection = new Point(x3, y3);

            //get the first intersection in sorted order
            //x,y can intersect outside the line segment since line is infinitely long
            //so finally check if x, y is within both the line segments
            if (IsInsideLine(lineA, firstIntersection, tolerance) &&
                IsInsideLine(lineB, firstIntersection, tolerance))
                return new Point(x3, y3);
        }

        //equations of the form x=c (two vertical lines)
        if (x1 == x2 && x3 == x4) return null;

        //equations of the form y=c (two horizontal lines)
        if (y1 == y2 && y3 == y4) return null;

        //general equation of line is y = mx + c where m is the slope
        //assume equation of line 1 as y1 = m1x1 + c1 
        //=> -m1x1 + y1 = c1 ----(1)
        //assume equation of line 2 as y2 = m2x2 + c2
        //=> -m2x2 + y2 = c2 -----(2)
        //if line 1 and 2 intersect then x1=x2=x and y1=y2=y where (x,y) is the intersection point
        //so we will get below two equations 
        //-m1x + y = c1 --------(3)
        //-m2x + y = c2 --------(4)

        double x, y;

        //lineA is vertical x1 = x2
        //slope will be infinity
        //so lets derive another solution
        if (Math.Abs(x1 - x2) < tolerance)
        {
            //compute slope of line 2 (m2) and c2
            var m2 = (y4 - y3) / (x4 - x3);
            var c2 = -m2 * x3 + y3;

            //equation of vertical line is x = c
            //if line 1 and 2 intersect then x1=c1=x
            //subsitute x=x1 in (4) => -m2x1 + y = c2
            // => y = c2 + m2x1 
            x = x1;
            y = c2 + m2 * x1;
        }
        //lineB is vertical x3 = x4
        //slope will be infinity
        //so lets derive another solution
        else if (Math.Abs(x3 - x4) < tolerance)
        {
            //compute slope of line 1 (m1) and c2
            var m1 = (y2 - y1) / (x2 - x1);
            var c1 = -m1 * x1 + y1;

            //equation of vertical line is x = c
            //if line 1 and 2 intersect then x3=c3=x
            //subsitute x=x3 in (3) => -m1x3 + y = c1
            // => y = c1 + m1x3 
            x = x3;
            y = c1 + m1 * x3;
        }
        //lineA and lineB are not vertical 
        //(could be horizontal we can handle it with slope = 0)
        else
        {
            //compute slope of line 1 (m1) and c2
            var m1 = (y2 - y1) / (x2 - x1);
            var c1 = -m1 * x1 + y1;

            //compute slope of line 2 (m2) and c2
            var m2 = (y4 - y3) / (x4 - x3);
            var c2 = -m2 * x3 + y3;

            //solving equations (3) and (4) => x = (c1-c2)/(m2-m1)
            //plugging x value in equation (4) => y = c2 + m2 * x
            x = (c1 - c2) / (m2 - m1);
            y = c2 + m2 * x;

            //verify by plugging intersection point (x, y)
            //in orginal equations (1) and (2) to see if they intersect
            //otherwise x,y values will not be finite and will fail this check
            if (!(Math.Abs(-m1 * x + y - c1) < tolerance
                  && Math.Abs(-m2 * x + y - c2) < tolerance))
                return null;
        }

        var result = new Point(x, y);

        //x,y can intersect outside the line segment since line is infinitely long
        //so finally check if x, y is within both the line segments
        if (IsInsideLine(lineA, result, tolerance) &&
            IsInsideLine(lineB, result, tolerance))
            return result;

        //return default null (no intersection)
        return null;
    }

    /// <summary>
    ///     Returns true if given point(x,y) is inside the given line segment.
    /// </summary>
    private static bool IsInsideLine(Line line, Point p, double tolerance)
    {
        double x = p.X, y = p.Y;

        var leftX = line.Left.X;
        var leftY = line.Left.Y;

        var rightX = line.Right.X;
        var rightY = line.Right.Y;

        return (x.IsGreaterThanOrEqual(leftX, tolerance) && x.IsLessThanOrEqual(rightX, tolerance)
                || x.IsGreaterThanOrEqual(rightX, tolerance) && x.IsLessThanOrEqual(leftX, tolerance))
               && (y.IsGreaterThanOrEqual(leftY, tolerance) && y.IsLessThanOrEqual(rightY, tolerance)
                   || y.IsGreaterThanOrEqual(rightY, tolerance) && y.IsLessThanOrEqual(leftY, tolerance));
    }
}

/// <summary>
///     Line extensions.
/// </summary>
public static class LineExtensions
{
    public static bool Intersects(this Line lineA, Line lineB, int precision = 5)
    {
        return LineIntersection.Find(lineA, lineB, precision) != null;
    }

    public static Point Intersection(this Line lineA, Line lineB, int precision = 5)
    {
        return LineIntersection.Find(lineA, lineB, precision);
    }

    internal static bool Intersects(this Line lineA, Line lineB, double tolerance)
    {
        return LineIntersection.FindIntersection(lineA, lineB, tolerance) != null;
    }

    internal static Point Intersection(this Line lineA, Line lineB, double tolerance)
    {
        return LineIntersection.FindIntersection(lineA, lineB, tolerance);
    }
}ParseOptions.0.jsonˆ
cD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\PointInsidePolygon.cs˘namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Check whether a given point is inside given polygon.
/// </summary>
public class PointInsidePolygon
{
    public static bool IsInside(Polygon polygon, Point point)
    {
        //a imaginary ray line from point to right infinity
        var rayLine = new Line(point, new Point(double.MaxValue, point.Y));

        var intersectionCount = 0;
        for (var i = 0; i < polygon.Edges.Count - 1; i++)
        {
            var edgeLine = polygon.Edges[i];

            if (LineIntersection.Find(rayLine, edgeLine) != null) intersectionCount++;
        }

        //should have odd intersections if point is inside the polygon
        return intersectionCount % 2 != 0;
    }
}ParseOptions.0.jsonß
^D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\PointRotation.csØusing System;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Rotates given point by given angle about given center.
/// </summary>
public class PointRotation
{
    public static Point Rotate(Point center, Point point, int angle)
    {
        var angleInRadians = angle * (Math.PI / 180);

        var cosTheta = Math.Cos(angleInRadians);
        var sinTheta = Math.Sin(angleInRadians);

        var x = cosTheta * (point.X - center.X) -
            sinTheta * (point.Y - center.Y) + center.X;

        var y = sinTheta * (point.X - center.X) +
                cosTheta * (point.Y - center.Y) + center.Y;

        return new Point(x, y);
    }
}ParseOptions.0.jsonœ
fD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\RectangleIntersection.csœusing System;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Rectangle intersection finder.
/// </summary>
public class RectangleIntersection
{
    /// <summary>
    ///     Returns the rectangle formed by the intersection if do intersect.
    ///     Otherwise default value of Rectangle struct.
    /// </summary>
    public static Rectangle FindIntersection(Rectangle a, Rectangle b)
    {
        //check for intersection
        if (!DoIntersect(a, b))
            //no intersection
            return null;

        var leftTopCorner = new Point
        (
            Math.Max(a.LeftTop.X, b.LeftTop.X),
            Math.Min(a.LeftTop.Y, b.LeftTop.Y)
        );


        var rightBottomCorner = new Point
        (
            Math.Min(a.RightBottom.X, b.RightBottom.X),
            Math.Max(a.RightBottom.Y, b.RightBottom.Y)
        );


        return new Rectangle
        {
            LeftTop = leftTopCorner,
            RightBottom = rightBottomCorner
        };
    }

    public static bool DoIntersect(Rectangle a, Rectangle b)
    {
        //check for intersection
        if (a.LeftTop.X > b.RightBottom.X // A is right of B   
            || a.RightBottom.X < b.LeftTop.X // A is left of B
            || a.RightBottom.Y > b.LeftTop.Y //A is above B
            || a.LeftTop.Y < b.RightBottom.Y) //A is below B
            //no intersection
            return false;

        return true;
    }
}ParseOptions.0.json¶
\D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\Shapes\Line.cs∞using System;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Line object.
/// </summary>
public class Line
{
    private readonly Lazy<double> slope;

    private Line()
    {
        slope = new Lazy<double>(() => CalcSlope());
    }

    internal Line(Point start, Point end, double tolerance)
        : this()
    {
        if (start.X < end.X)
        {
            Left = start;
            Right = end;
        }
        else if (start.X > end.X)
        {
            Left = end;
            Right = start;
        }
        else
        {
            //use Y
            if (start.Y < end.Y)
            {
                Left = start;
                Right = end;
            }
            else
            {
                Left = end;
                Right = start;
            }
        }
    }

    public Line(Point start, Point end, int precision = 5)
        : this(start, end, Math.Round(Math.Pow(0.1, precision), precision))
    {
    }

    public Point Left { get; }
    public Point Right { get; }

    public bool IsVertical => Left.X == Right.X;
    public bool IsHorizontal => Left.Y == Right.Y;

    public double Slope => slope.Value;

    private double CalcSlope()
    {
        Point left = Left, right = Right;

        //vertical line has infinite slope
        if (left.Y == right.Y) return double.MaxValue;

        return (right.Y - left.Y) / (right.X - left.X);
    }

    public Line Clone()
    {
        return new Line(Left.Clone(), Right.Clone());
    }
}ParseOptions.0.jsonΩ
]D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\Shapes\Point.cs∆namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Point object.
/// </summary>
public class Point
{
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; }
    public double Y { get; }

    public override string ToString()
    {
        return X.ToString("F") + " " + Y.ToString("F");
    }

    public Point Clone()
    {
        return new Point(X, Y);
    }
}ParseOptions.0.json≤
_D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\Shapes\Polygon.csπusing System.Collections.Generic;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Polygon object.
/// </summary>
public class Polygon
{
    /// <summary>
    ///     Create a polygon with given edges lines.
    /// </summary>
    public Polygon(List<Line> edges)
    {
        Edges = edges;
    }

    /// <summary>
    ///     Create polygon from the given list of consecutive boundary end points.
    ///     Last and first points will be connected.
    ///     If only one edge point is provided then this polygon will behave like a point,
    ///     a line is created with both ends having same edge point.
    /// </summary>
    public Polygon(List<Point> edgePoints)
    {
        Edges = new List<Line>();

        for (var i = 0; i < edgePoints.Count; i++)
            Edges.Add(new Line(edgePoints[i], edgePoints[(i + 1) % edgePoints.Count]));
    }

    public List<Line> Edges { get; set; }
}ParseOptions.0.json„
aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\Shapes\Rectangle.csË
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Rectangle object.
/// </summary>
public class Rectangle
{
    public Rectangle()
    {
    }

    public Rectangle(Point leftTop, Point rightBottom)
    {
        if (rightBottom.Y > leftTop.Y) throw new Exception("Top corner should have higher Y value than bottom.");

        if (leftTop.X > rightBottom.X) throw new Exception("Right corner should have higher X value than left.");

        LeftTop = leftTop;
        RightBottom = rightBottom;
    }

    public Point LeftTop { get; set; }
    public Point RightBottom { get; set; }

    internal double Length => Math.Abs(RightBottom.X - LeftTop.X);
    internal double Breadth => Math.Abs(LeftTop.Y - RightBottom.Y);

    internal double Area()
    {
        return Length * Breadth;
    }

    public Polygon ToPolygon()
    {
        var edges = new List<Line>();

        //add all four edge lines of this rectangle
        edges.Add(new Line(LeftTop, new Point(RightBottom.X, LeftTop.Y)));
        edges.Add(new Line(new Point(RightBottom.X, LeftTop.Y), RightBottom));
        edges.Add(new Line(RightBottom, new Point(LeftTop.X, RightBottom.Y)));
        edges.Add(new Line(new Point(LeftTop.X, RightBottom.Y), LeftTop));

        return new Polygon(edges);
    }
}ParseOptions.0.jsoní
hD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\Shared\DoubleExtensions.csêusing System;

namespace Advanced.Algorithms.Geometry;

internal static class DoubleExtensions
{
    internal static bool IsEqual(this double a, double b, double tolerance)
    {
        return Math.Abs(a - b) < tolerance;
    }

    internal static bool IsLessThan(this double a, double b, double tolerance)
    {
        return a - b < -tolerance;
    }

    internal static bool IsLessThanOrEqual(this double a, double b, double tolerance)
    {
        var result = a - b;

        return result < -tolerance || Math.Abs(result) < tolerance;
    }

    internal static bool IsGreaterThan(this double a, double b, double tolerance)
    {
        return a - b > tolerance;
    }

    internal static bool IsGreaterThanOrEqual(this double a, double b, double tolerance)
    {
        var result = a - b;
        return result > tolerance || Math.Abs(result) < tolerance;
    }
}ParseOptions.0.jsonˆ
eD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\Shared\PointComparer.cs˜using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Compares two points for geometric equality implementing IEqualityComparer.
/// </summary>
public class PointComparer : IEqualityComparer<Point>
{
    public bool Equals(Point x, Point y)
    {
        if (x == null && y == null) return true;

        // Check for null values 
        if (x == null || y == null) return false;

        if (x == y) return true;

        return x.X == y.X && x.Y == y.Y;
    }

    public int GetHashCode(Point point)
    {
        var hashCode = 33;
        hashCode = hashCode * -21 + point.X.GetHashCode();
        hashCode = hashCode * -21 + point.Y.GetHashCode();
        return hashCode;
    }
}ParseOptions.0.json®
iD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Geometry\Shared\RectangleComparer.cs•using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Compares two rectangles for geometrical equality implementing IEqualityComparer.
/// </summary>
public class RectangleComparer : IEqualityComparer<Rectangle>
{
    public bool Equals(Rectangle x, Rectangle y)
    {
        if (x == null && y == null) return true;

        // Check for null values 
        if (x == null || y == null) return false;

        return x.LeftTop.X == y.LeftTop.X
               && x.LeftTop.Y == y.LeftTop.Y
               && x.RightBottom.X == y.RightBottom.X
               && x.RightBottom.Y == y.RightBottom.Y;
    }

    public int GetHashCode(Rectangle rectangle)
    {
        var hashCode = 35;
        hashCode = hashCode * -26 + rectangle.LeftTop.GetHashCode();
        hashCode = hashCode * -26 + rectangle.RightBottom.GetHashCode();
        return hashCode;
    }
}ParseOptions.0.json„
yD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\ArticulationPoint\TarjansArticulationFinder.cs–using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Articulation point finder using Tarjan's algorithm.
/// </summary>
public class TarjansArticulationFinder<T>
{
    /// <summary>
    ///     Returns a list if articulation points in this graph.
    /// </summary>
    public List<T> FindArticulationPoints(IGraph<T> graph)
    {
        var visitTime = 0;
        return Dfs(graph.ReferenceVertex, new List<T>(),
            new Dictionary<T, int>(), new Dictionary<T, int>(),
            new Dictionary<T, T>(),
            ref visitTime);
    }

    /// <summary>
    ///     Do a depth first search to find articulation points by keeping track of
    ///     discovery nodes and checking for back edges using low/discovery time maps.
    /// </summary>
    private List<T> Dfs(IGraphVertex<T> currentVertex,
        List<T> result,
        Dictionary<T, int> discoveryTimeMap, Dictionary<T, int> lowTimeMap,
        Dictionary<T, T> parent, ref int discoveryTime)
    {
        var isArticulationPoint = false;

        discoveryTimeMap.Add(currentVertex.Key, discoveryTime);
        lowTimeMap.Add(currentVertex.Key, discoveryTime);

        //discovery childs in this iteration
        var discoveryChildCount = 0;

        foreach (var edge in currentVertex.Edges)
            if (!discoveryTimeMap.ContainsKey(edge.TargetVertexKey))
            {
                discoveryChildCount++;
                parent.Add(edge.TargetVertexKey, currentVertex.Key);

                discoveryTime++;
                Dfs(edge.TargetVertex, result,
                    discoveryTimeMap, lowTimeMap, parent, ref discoveryTime);

                //if neighbours lowTime is greater than current
                //then this is an articulation point 
                //because neighbour never had a chance to propogate any ancestors low value
                //since this is an isolated componant
                if (discoveryTimeMap[currentVertex.Key] <= lowTimeMap[edge.TargetVertexKey])
                    isArticulationPoint = true;
                else
                    //propogate lowTime index of neighbour so that ancestors can see it in DFS
                    lowTimeMap[currentVertex.Key] =
                        Math.Min(lowTimeMap[currentVertex.Key], lowTimeMap[edge.TargetVertexKey]);
            }
            else
            {
                //check if this edge target vertex is not in the current DFS path
                //even if edge target vertex was already visisted
                //update this so that ancestors can see it
                if (parent.ContainsKey(currentVertex.Key) == false
                    || !edge.TargetVertexKey.Equals(parent[currentVertex.Key]))
                    lowTimeMap[currentVertex.Key] =
                        Math.Min(lowTimeMap[currentVertex.Key], discoveryTimeMap[edge.TargetVertexKey]);
            }

        //if root of DFS with two or more children
        //or visitTime of this Vertex <=lowTime of any neighbour 
        if (parent.ContainsKey(currentVertex.Key) == false && discoveryChildCount >= 2 ||
            parent.ContainsKey(currentVertex.Key) && isArticulationPoint)
            result.Add(currentVertex.Key);


        return result;
    }
}ParseOptions.0.json†
hD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Bridge\TarjansBridgeFinder.csûusing System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Bridge finder using Tarjan's algorithm.
/// </summary>
public class TarjansBridgeFinder<T>
{
    /// <summary>
    ///     Returns a list if Bridge points in this graph.
    /// </summary>
    public List<Bridge<T>> FindBridges(IGraph<T> graph)
    {
        var visitTime = 0;
        return Dfs(graph.ReferenceVertex, new List<Bridge<T>>(),
            new Dictionary<T, int>(), new Dictionary<T, int>(),
            new Dictionary<T, T>(),
            ref visitTime);
    }

    /// <summary>
    ///     Do a depth first search to find Bridge edges by keeping track of
    ///     discovery nodes and checking for back edges using low/discovery time maps.
    /// </summary>
    private List<Bridge<T>> Dfs(IGraphVertex<T> currentVertex,
        List<Bridge<T>> result,
        Dictionary<T, int> discoveryTimeMap, Dictionary<T, int> lowTimeMap,
        Dictionary<T, T> parent, ref int discoveryTime)
    {
        discoveryTimeMap.Add(currentVertex.Key, discoveryTime);
        lowTimeMap.Add(currentVertex.Key, discoveryTime);

        //discovery childs in this iteration
        foreach (var edge in currentVertex.Edges)
            if (!discoveryTimeMap.ContainsKey(edge.TargetVertexKey))
            {
                parent.Add(edge.TargetVertexKey, currentVertex.Key);

                discoveryTime++;
                Dfs(edge.TargetVertex, result,
                    discoveryTimeMap, lowTimeMap, parent, ref discoveryTime);

                //propogate lowTime index of neighbour so that ancestors can see check for back edge
                lowTimeMap[currentVertex.Key] =
                    Math.Min(lowTimeMap[currentVertex.Key], lowTimeMap[edge.TargetVertexKey]);

                //if neighbours lowTime is less than current
                //then this is an Bridge point 
                //because neighbour never had a chance to propogate any ancestors low value
                //since this is an isolated componant
                if (discoveryTimeMap[currentVertex.Key] < lowTimeMap[edge.TargetVertexKey])
                    result.Add(new Bridge<T>(currentVertex.Key, edge.TargetVertexKey));
            }
            else
            {
                //check if this edge target vertex is not in the current DFS path
                //even if edge target vertex was already visisted
                //update discovery so that ancestors can see it
                if (parent.ContainsKey(currentVertex.Key) == false
                    || !edge.TargetVertexKey.Equals(parent[currentVertex.Key]))
                    lowTimeMap[currentVertex.Key] =
                        Math.Min(lowTimeMap[currentVertex.Key], discoveryTimeMap[edge.TargetVertexKey]);
            }

        return result;
    }
}

/// <summary>
///     The bridge object.
/// </summary>
public class Bridge<T>
{
    public Bridge(T vertexA, T vertexB)
    {
        this.VertexA = vertexA;
        this.VertexB = vertexB;
    }

    public T VertexA { get; }
    public T VertexB { get; }
}ParseOptions.0.json≥
_D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Coloring\MColorer.cs∫using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     An m-coloring algorithm implementation.
/// </summary>
public class MColorer<T, TC>
{
    /// <summary>
    ///     Returns true if all vertices can be colored using the given colors
    ///     in such a way so that no neighbours have same color.
    /// </summary>
    public MColorResult<T, TC> Color(IGraph<T> graph, TC[] colors)
    {
        var progress = new Dictionary<IGraphVertex<T>, TC>();

        foreach (var vertex in graph.VerticesAsEnumberable)
            if (!progress.ContainsKey(vertex))
                ColorRecursively(vertex, colors,
                    progress,
                    new HashSet<IGraphVertex<T>>());

        if (progress.Count != graph.VerticesCount) return new MColorResult<T, TC>(false, null);

        var result = new Dictionary<TC, List<T>>();

        foreach (var vertex in progress)
        {
            if (!result.ContainsKey(vertex.Value)) result.Add(vertex.Value, new List<T>());

            result[vertex.Value].Add(vertex.Key.Key);
        }

        return new MColorResult<T, TC>(true, result);
    }

    /// <summary>
    ///     Assign color to each new node.
    /// </summary>
    private Dictionary<IGraphVertex<T>, TC> ColorRecursively(IGraphVertex<T> vertex, TC[] colors,
        Dictionary<IGraphVertex<T>, TC> progress, HashSet<IGraphVertex<T>> visited)
    {
        foreach (var item in colors)
        {
            if (!IsSafe(progress, vertex, item)) continue;

            progress.Add(vertex, item);
            break;
        }

        if (visited.Contains(vertex) == false)
        {
            visited.Add(vertex);

            foreach (var edge in vertex.Edges)
            {
                if (visited.Contains(edge.TargetVertex)) continue;

                ColorRecursively(edge.TargetVertex, colors, progress, visited);
            }
        }

        return progress;
    }

    /// <summary>
    ///     Is it safe to assign this color to this vertex?
    /// </summary>
    private bool IsSafe(Dictionary<IGraphVertex<T>, TC> progress,
        IGraphVertex<T> vertex, TC color)
    {
        foreach (var edge in vertex.Edges)
            if (progress.ContainsKey(edge.TargetVertex)
                && progress[edge.TargetVertex].Equals(color))
                return false;

        return true;
    }
}

/// <summary>
///     M-coloring result object.
/// </summary>
public class MColorResult<T, TC>
{
    public MColorResult(bool canColor, Dictionary<TC, List<T>> partitions)
    {
        CanColor = canColor;
        Partitions = partitions;
    }

    public bool CanColor { get; }
    public Dictionary<TC, List<T>> Partitions { get; }
}ParseOptions.0.jsonπ
tD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Connectivity\KosarajuStronglyConnected.cs´using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A Kosaraju Strong Connected Component Algorithm Implementation.
/// </summary>
public class KosarajuStronglyConnected<T>
{
    /// <summary>
    ///     Returns all Connected Components using Kosaraju's Algorithm.
    /// </summary>
    public List<List<T>>
        FindStronglyConnectedComponents(IDiGraph<T> graph)
    {
        var visited = new HashSet<T>();
        var finishStack = new Stack<T>();

        //step one - create DFS finish visit stack
        foreach (var vertex in graph.VerticesAsEnumberable)
            if (!visited.Contains(vertex.Key))
                KosarajuStep1(vertex, visited, finishStack);

        //reverse edges
        var reverseGraph = ReverseEdges(graph);

        visited.Clear();

        var result = new List<List<T>>();

        //now pop finish stack and gather the components
        while (finishStack.Count > 0)
        {
            var currentVertex = reverseGraph.GetVertex(finishStack.Pop());

            if (!visited.Contains(currentVertex.Key))
                result.Add(KosarajuStep2(currentVertex, visited,
                    finishStack, new List<T>()));
        }

        return result;
    }

    /// <summary>
    ///     Just do a DFS keeping track on finish Stack of Vertices.
    /// </summary>
    private void KosarajuStep1(IDiGraphVertex<T> currentVertex,
        HashSet<T> visited,
        Stack<T> finishStack)
    {
        visited.Add(currentVertex.Key);

        foreach (var edge in currentVertex.OutEdges)
            if (!visited.Contains(edge.TargetVertexKey))
                KosarajuStep1(edge.TargetVertex, visited, finishStack);

        //finished visiting, so add to stack
        finishStack.Push(currentVertex.Key);
    }

    /// <summary>
    ///     In step two we just add all reachable nodes to result (connected componant).
    /// </summary>
    private List<T> KosarajuStep2(IDiGraphVertex<T> currentVertex,
        HashSet<T> visited, Stack<T> finishStack,
        List<T> result)
    {
        visited.Add(currentVertex.Key);
        result.Add(currentVertex.Key);

        foreach (var edge in currentVertex.OutEdges)
            if (!visited.Contains(edge.TargetVertexKey))
                KosarajuStep2(edge.TargetVertex, visited, finishStack, result);

        return result;
    }

    /// <summary>
    ///     Create a clone graph with reverse edge directions.
    /// </summary>
    private IDiGraph<T> ReverseEdges(IDiGraph<T> graph)
    {
        var newGraph = new DiGraph<T>();

        foreach (var vertex in graph.VerticesAsEnumberable) newGraph.AddVertex(vertex.Key);

        foreach (var vertex in graph.VerticesAsEnumberable)
        foreach (var edge in vertex.OutEdges)
            //reverse edge
            newGraph.AddEdge(edge.TargetVertexKey, vertex.Key);

        return newGraph;
    }
}ParseOptions.0.jsonﬁ
mD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Connectivity\TarjansBiConnected.cs◊using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Finds if a graph is BiConnected.
/// </summary>
public class TarjansBiConnected<T>
{
    /// <summary>
    ///     This is using ariticulation alogrithm based on the observation that
    ///     a graph is BiConnected if and only if there is no articulation Points.
    /// </summary>
    public bool IsBiConnected(IGraph<T> graph)
    {
        var algorithm = new TarjansArticulationFinder<T>();
        return algorithm.FindArticulationPoints(graph).Count == 0;
    }
}ParseOptions.0.json™
sD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Connectivity\TarjansStronglyConnected.csùusing System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Strongly connected using Tarjan's algorithm.
/// </summary>
public class TarjansStronglyConnected<T>
{
    /// <summary>
    ///     Rreturns a list of Strongly Connected components in this graph.
    /// </summary>
    public List<List<T>> FindStronglyConnectedComponents(IDiGraph<T> graph)
    {
        var result = new List<List<T>>();

        var discoveryTimeMap = new Dictionary<T, int>();
        var lowTimeMap = new Dictionary<T, int>();
        var pathStack = new Stack<T>();
        var pathStackMap = new HashSet<T>();
        var discoveryTime = 0;
        foreach (var vertex in graph.VerticesAsEnumberable)
            if (!discoveryTimeMap.ContainsKey(vertex.Key))
                Dfs(vertex,
                    result,
                    discoveryTimeMap, lowTimeMap,
                    pathStack, pathStackMap, ref discoveryTime);

        return result;
    }

    /// <summary>
    ///     Do a depth first search to find Strongly Connected by keeping track of
    ///     discovery nodes and checking for back edges using low/discovery time maps.
    /// </summary>
    private void Dfs(IDiGraphVertex<T> currentVertex,
        List<List<T>> result,
        Dictionary<T, int> discoveryTimeMap, Dictionary<T, int> lowTimeMap,
        Stack<T> pathStack,
        HashSet<T> pathStackMap, ref int discoveryTime)
    {
        discoveryTimeMap.Add(currentVertex.Key, discoveryTime);
        lowTimeMap.Add(currentVertex.Key, discoveryTime);
        pathStack.Push(currentVertex.Key);
        pathStackMap.Add(currentVertex.Key);

        foreach (var edge in currentVertex.OutEdges)
            if (!discoveryTimeMap.ContainsKey(edge.TargetVertexKey))
            {
                discoveryTime++;
                Dfs(edge.TargetVertex, result, discoveryTimeMap, lowTimeMap,
                    pathStack, pathStackMap, ref discoveryTime);

                //propogate lowTime index of neighbour so that ancestors can see it in DFS
                lowTimeMap[currentVertex.Key] =
                    Math.Min(lowTimeMap[currentVertex.Key], lowTimeMap[edge.TargetVertexKey]);
            }
            else
            {
                //ignore cross edges
                //even if edge vertex was already visisted
                //update this so that ancestors can see it
                if (pathStackMap.Contains(edge.TargetVertexKey))
                    lowTimeMap[currentVertex.Key] =
                        Math.Min(lowTimeMap[currentVertex.Key],
                            discoveryTimeMap[edge.TargetVertexKey]);
            }

        //if low is high this means we reached head of the DFS tree with strong connectivity
        //now print items in the stack
        if (lowTimeMap[currentVertex.Key] != discoveryTimeMap[currentVertex.Key]) return;

        var strongConnected = new List<T>();
        while (!pathStack.Peek().Equals(currentVertex.Key))
        {
            var vertex = pathStack.Pop();
            strongConnected.Add(vertex);
            pathStackMap.Remove(vertex);
        }

        //add current vertex
        var finalVertex = pathStack.Pop();
        strongConnected.Add(finalVertex);
        pathStackMap.Remove(finalVertex);

        result.Add(strongConnected);
    }
}ParseOptions.0.json€

bD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Cover\MinVertexCover.csﬂ	using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A minimum vertex conver algorithm implementation.
/// </summary>
public class MinVertexCover<T>
{
    public List<IGraphVertex<T>> GetMinVertexCover(IGraph<T> graph)
    {
        return GetMinVertexCover(graph.ReferenceVertex, new HashSet<IGraphVertex<T>>(),
            new List<IGraphVertex<T>>());
    }

    /// <summary>
    ///     An approximation algorithm for NP complete vertex cover problem.
    ///     Add a random edge vertices until done visiting all edges.
    /// </summary>
    private List<IGraphVertex<T>> GetMinVertexCover(IGraphVertex<T> vertex,
        HashSet<IGraphVertex<T>> visited, List<IGraphVertex<T>> cover)
    {
        visited.Add(vertex);

        foreach (var edge in vertex.Edges)
        {
            if (!cover.Contains(vertex) && !cover.Contains(edge.TargetVertex))
            {
                cover.Add(vertex);
                cover.Add(edge.TargetVertex);
            }

            if (!visited.Contains(edge.TargetVertex)) GetMinVertexCover(edge.TargetVertex, visited, cover);
        }

        return cover;
    }
}ParseOptions.0.jsonñ
\D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Cut\MinimumCut.cs†using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Compute minimum cut edges of given graph
///     using Edmond-Karps improved Ford-Fulkerson Max Flow Algorithm.
/// </summary>
public class MinCut<T, TW> where TW : IComparable
{
    private readonly IFlowOperators<TW> @operator;

    public MinCut(IFlowOperators<TW> @operator)
    {
        this.@operator = @operator;
    }

    public List<MinCutEdge<T>> ComputeMinCut(IDiGraph<T> graph,
        T source, T sink)
    {
        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultWeight.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IFlowOperators<int> operator implementation during initialization.");

        var edmondsKarpMaxFlow = new EdmondKarpMaxFlow<T, TW>(@operator);

        var maxFlowResidualGraph = edmondsKarpMaxFlow
            .ComputeMaxFlowAndReturnResidualGraph(graph, source, sink);

        //according to Min Max theory
        //the Min Cut can be obtained by Finding edges 
        //from Reachable Vertices from Source
        //to unreachable vertices in residual graph
        var reachableVertices = GetReachable(maxFlowResidualGraph, source);

        var result = new List<MinCutEdge<T>>();

        foreach (var vertex in reachableVertices)
        foreach (var edge in graph.GetVertex(vertex).OutEdges)
            //if unreachable
            if (!reachableVertices.Contains(edge.TargetVertexKey))
                result.Add(new MinCutEdge<T>(vertex, edge.TargetVertexKey));

        return result;
    }

    /// <summary>
    ///     Gets a list of reachable vertices in residual graph from source.
    /// </summary>
    private HashSet<T> GetReachable(WeightedDiGraph<T, TW> residualGraph,
        T source)
    {
        var visited = new HashSet<T>();

        Dfs(residualGraph.Vertices[source], visited);

        return visited;
    }

    /// <summary>
    ///     Recursive DFS.
    /// </summary>
    private void Dfs(WeightedDiGraphVertex<T, TW> currentResidualGraphVertex,
        HashSet<T> visited)
    {
        visited.Add(currentResidualGraphVertex.Key);

        foreach (var edge in currentResidualGraphVertex.OutEdges)
        {
            if (visited.Contains(edge.Key.Key)) continue;

            //reachable only if +ive weight (unsaturated edge)
            if (edge.Value.CompareTo(@operator.DefaultWeight) != 0) Dfs(edge.Key, visited);
        }
    }
}

/// <summary>
///     Minimum cut result object.
/// </summary>
public class MinCutEdge<T>
{
    public MinCutEdge(T source, T dest)
    {
        Source = source;
        Destination = dest;
    }

    public T Source { get; }
    public T Destination { get; }
}ParseOptions.0.json®
bD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Cycle\CycleDetection.cs¨
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Cycle detection using Depth First Search.
/// </summary>
public class CycleDetector<T>
{
    /// <summary>
    ///     Returns true if a cycle exists
    /// </summary>
    public bool HasCycle(IDiGraph<T> graph)
    {
        var visiting = new HashSet<T>();
        var visited = new HashSet<T>();

        foreach (var vertex in graph.VerticesAsEnumberable)
            if (!visited.Contains(vertex.Key))
                if (Dfs(vertex, visited, visiting))
                    return true;

        return false;
    }

    private bool Dfs(IDiGraphVertex<T> current,
        HashSet<T> visited, HashSet<T> visiting)
    {
        visiting.Add(current.Key);

        foreach (var edge in current.OutEdges)
        {
            //if we encountered a visiting vertex again
            //then their is a cycle
            if (visiting.Contains(edge.TargetVertexKey)) return true;

            if (visited.Contains(edge.TargetVertexKey)) continue;

            if (Dfs(edge.TargetVertex, visited, visiting)) return true;
        }

        visiting.Remove(current.Key);
        visited.Add(current.Key);

        return false;
    }
}ParseOptions.0.json◊<
^D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Flow\EdmondsKarp.csﬂ;using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     An Edmond Karp max flow implementation on weighted directed graph using
///     adjacency list representation of graph and residual graph.
/// </summary>
public class EdmondKarpMaxFlow<T, TW> where TW : IComparable
{
    private readonly IFlowOperators<TW> @operator;

    public EdmondKarpMaxFlow(IFlowOperators<TW> @operator)
    {
        this.@operator = @operator;
    }

    /// <summary>
    ///     Compute max flow by searching a path
    ///     and then augmenting the residual graph until
    ///     no more path exists in residual graph with possible flow.
    /// </summary>
    public TW ComputeMaxFlow(IDiGraph<T> graph,
        T source, T sink)
    {
        ValidateOperator(graph);

        var residualGraph = CreateResidualGraph(graph);

        var path = Bfs(residualGraph, source, sink);

        var result = @operator.DefaultWeight;

        while (path != null)
        {
            result = @operator.AddWeights(result, AugmentResidualGraph(graph, residualGraph, path));
            path = Bfs(residualGraph, source, sink);
        }

        return result;
    }


    /// <summary>
    ///     Compute max flow by searching a path
    ///     and then augmenting the residual graph until
    ///     no more path exists in residual graph with possible flow.
    /// </summary>
    public WeightedDiGraph<T, TW> ComputeMaxFlowAndReturnResidualGraph(IDiGraph<T> graph,
        T source, T sink)
    {
        ValidateOperator(graph);

        var residualGraph = CreateResidualGraph(graph);

        var path = Bfs(residualGraph, source, sink);

        var result = @operator.DefaultWeight;

        while (path != null)
        {
            result = @operator.AddWeights(result, AugmentResidualGraph(graph, residualGraph, path));
            path = Bfs(residualGraph, source, sink);
        }

        return residualGraph;
    }

    private void ValidateOperator(IDiGraph<T> graph)
    {
        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultWeight.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IFlowOperators<int> operator implementation during initialization.");
    }

    /// <summary>
    ///     Return all flow Paths.
    /// </summary>
    internal List<List<T>> ComputeMaxFlowAndReturnFlowPath(WeightedDiGraph<T, TW> graph,
        T source, T sink)
    {
        var residualGraph = CreateResidualGraph(graph);

        var path = Bfs(residualGraph, source, sink);

        var flow = @operator.DefaultWeight;

        var result = new List<List<T>>();
        while (path != null)
        {
            result.Add(path);
            flow = @operator.AddWeights(flow, AugmentResidualGraph(graph, residualGraph, path));
            path = Bfs(residualGraph, source, sink);
        }

        return result;
    }

    /// <summary>
    ///     Augment current Path to residual Graph.
    /// </summary>
    private TW AugmentResidualGraph(IDiGraph<T> graph,
        WeightedDiGraph<T, TW> residualGraph, List<T> path)
    {
        var min = @operator.MaxWeight;

        for (var i = 0; i < path.Count - 1; i++)
        {
            var vertex1 = residualGraph.FindVertex(path[i]);
            var vertex2 = residualGraph.FindVertex(path[i + 1]);

            var edgeValue = vertex1.OutEdges[vertex2];

            if (min.CompareTo(edgeValue) > 0) min = edgeValue;
        }

        //augment path
        for (var i = 0; i < path.Count - 1; i++)
        {
            var vertex1 = residualGraph.FindVertex(path[i]);
            var vertex2 = residualGraph.FindVertex(path[i + 1]);

            //substract from forward paths
            vertex1.OutEdges[vertex2] = @operator.SubstractWeights(vertex1.OutEdges[vertex2], min);

            //add for backward paths
            vertex2.OutEdges[vertex1] = @operator.AddWeights(vertex2.OutEdges[vertex1], min);
        }

        return min;
    }

    /// <summary>
    ///     Bredth first search to find a path to sink in residual graph from source.
    /// </summary>
    private List<T> Bfs(WeightedDiGraph<T, TW> residualGraph, T source, T sink)
    {
        //init parent lookup table to trace path
        var parentLookUp = new Dictionary<WeightedDiGraphVertex<T, TW>, WeightedDiGraphVertex<T, TW>>();
        foreach (var vertex in residualGraph.Vertices) parentLookUp.Add(vertex.Value, null);

        //regular BFS stuff
        var queue = new Queue<WeightedDiGraphVertex<T, TW>>();
        var visited = new HashSet<WeightedDiGraphVertex<T, TW>>();
        queue.Enqueue(residualGraph.Vertices[source]);
        visited.Add(residualGraph.Vertices[source]);

        WeightedDiGraphVertex<T, TW> currentVertex = null;

        while (queue.Count > 0)
        {
            currentVertex = queue.Dequeue();

            //reached sink? then break otherwise dig in
            if (currentVertex.Key.Equals(sink)) break;

            foreach (var edge in currentVertex.OutEdges)
                //visit only if edge have available flow
                if (!visited.Contains(edge.Key)
                    && edge.Value.CompareTo(@operator.DefaultWeight) > 0)
                {
                    //keep track of this to trace out path once sink is found
                    parentLookUp[edge.Key] = currentVertex;
                    queue.Enqueue(edge.Key);
                    visited.Add(edge.Key);
                }
        }

        //could'nt find a path
        if (currentVertex == null || !currentVertex.Key.Equals(sink)) return null;

        //traverse back from sink to find path to source
        var path = new Stack<T>();

        path.Push(sink);

        while (currentVertex != null && !currentVertex.Key.Equals(source))
        {
            path.Push(parentLookUp[currentVertex].Key);
            currentVertex = parentLookUp[currentVertex];
        }

        //now reverse the stack to get the path from source to sink
        var result = new List<T>();

        while (path.Count > 0) result.Add(path.Pop());

        return result;
    }

    /// <summary>
    ///     Clones this graph and creates a residual graph.
    /// </summary>
    private WeightedDiGraph<T, TW> CreateResidualGraph(IDiGraph<T> graph)
    {
        var newGraph = new WeightedDiGraph<T, TW>();

        //clone graph vertices
        foreach (var vertex in graph.VerticesAsEnumberable) newGraph.AddVertex(vertex.Key);

        //clone edges
        foreach (var vertex in graph.VerticesAsEnumberable)
            //Use either OutEdges or InEdges for cloning
            //here we use OutEdges
        foreach (var edge in vertex.OutEdges)
        {
            //original edge
            newGraph.AddEdge(vertex.Key, edge.TargetVertex.Key, edge.Weight<TW>());
            //add a backward edge for residual graph with edge value as default(W)
            newGraph.AddEdge(edge.TargetVertex.Key, vertex.Key, default);
        }

        return newGraph;
    }
}ParseOptions.0.jsonﬂ;
`D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Flow\FordFulkerson.csÂ:using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A ford-fulkerson max flox implementation on weighted directed graph using
///     adjacency list representation of graph and residual graph.
/// </summary>
public class FordFulkersonMaxFlow<T, TW> where TW : IComparable
{
    private readonly IFlowOperators<TW> @operator;

    public FordFulkersonMaxFlow(IFlowOperators<TW> @operator)
    {
        this.@operator = @operator;
    }

    /// <summary>
    ///     Compute max flow by searching a path
    ///     and then augmenting the residual graph until
    ///     no more path exists in residual graph with possible flow.
    /// </summary>
    public TW ComputeMaxFlow(IDiGraph<T> graph,
        T source, T sink)
    {
        ValidateOperator(graph);

        var residualGraph = CreateResidualGraph(graph);

        var path = Dfs(residualGraph, source, sink);

        var result = @operator.DefaultWeight;

        while (path != null)
        {
            result = @operator.AddWeights(result, AugmentResidualGraph(residualGraph, path));
            path = Dfs(residualGraph, source, sink);
        }

        return result;
    }


    /// <summary>
    ///     Return all flow Paths.
    /// </summary>
    public List<List<T>> ComputeMaxFlowAndReturnFlowPath(IDiGraph<T> graph,
        T source, T sink)
    {
        ValidateOperator(graph);

        var residualGraph = CreateResidualGraph(graph);

        var path = Dfs(residualGraph, source, sink);

        var flow = @operator.DefaultWeight;

        var result = new List<List<T>>();
        while (path != null)
        {
            result.Add(path);
            flow = @operator.AddWeights(flow, AugmentResidualGraph(residualGraph, path));
            path = Dfs(residualGraph, source, sink);
        }

        return result;
    }

    private void ValidateOperator(IDiGraph<T> graph)
    {
        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultWeight.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IFlowOperators<int> operator implementation during initialization.");
    }

    /// <summary>
    ///     Augment current Path to residual Graph.
    /// </summary>
    private TW AugmentResidualGraph(WeightedDiGraph<T, TW> residualGraph, List<T> path)
    {
        var min = @operator.MaxWeight;

        for (var i = 0; i < path.Count - 1; i++)
        {
            var vertex1 = residualGraph.FindVertex(path[i]);
            var vertex2 = residualGraph.FindVertex(path[i + 1]);

            var edgeValue = vertex1.OutEdges[vertex2];

            if (min.CompareTo(edgeValue) > 0) min = edgeValue;
        }

        //augment path
        for (var i = 0; i < path.Count - 1; i++)
        {
            var vertex1 = residualGraph.FindVertex(path[i]);
            var vertex2 = residualGraph.FindVertex(path[i + 1]);

            //substract from forward paths
            vertex1.OutEdges[vertex2] = @operator.SubstractWeights(vertex1.OutEdges[vertex2], min);

            //add for backward paths
            vertex2.OutEdges[vertex1] = @operator.AddWeights(vertex2.OutEdges[vertex1], min);
        }

        return min;
    }

    /// <summary>
    ///     Depth first search to find a path to sink in residual graph from source.
    /// </summary>
    private List<T> Dfs(WeightedDiGraph<T, TW> residualGraph, T source, T sink)
    {
        //init parent lookup table to trace path
        var parentLookUp = new Dictionary<WeightedDiGraphVertex<T, TW>, WeightedDiGraphVertex<T, TW>>();
        foreach (var vertex in residualGraph.Vertices) parentLookUp.Add(vertex.Value, null);

        //regular DFS stuff
        var stack = new Stack<WeightedDiGraphVertex<T, TW>>();
        var visited = new HashSet<WeightedDiGraphVertex<T, TW>>();
        stack.Push(residualGraph.Vertices[source]);
        visited.Add(residualGraph.Vertices[source]);

        WeightedDiGraphVertex<T, TW> currentVertex = null;

        while (stack.Count > 0)
        {
            currentVertex = stack.Pop();

            //reached sink? then break otherwise dig in
            if (currentVertex.Key.Equals(sink))
                break;
            foreach (var edge in currentVertex.OutEdges)
                //visit only if edge have available flow
                if (!visited.Contains(edge.Key)
                    && edge.Value.CompareTo(@operator.DefaultWeight) > 0)
                {
                    //keep track of this to trace out path once sink is found
                    parentLookUp[edge.Key] = currentVertex;
                    stack.Push(edge.Key);
                    visited.Add(edge.Key);
                }
        }

        //could'nt find a path
        if (currentVertex == null || !currentVertex.Key.Equals(sink)) return null;

        //traverse back from sink to find path to source
        var path = new Stack<T>();

        path.Push(sink);

        while (currentVertex != null && !currentVertex.Key.Equals(source))
        {
            path.Push(parentLookUp[currentVertex].Key);
            currentVertex = parentLookUp[currentVertex];
        }

        //now reverse the stack to get the path from source to sink
        var result = new List<T>();

        while (path.Count > 0) result.Add(path.Pop());

        return result;
    }

    /// <summary>
    ///     Clones this graph and creates a residual graph.
    /// </summary>
    private WeightedDiGraph<T, TW> CreateResidualGraph(IDiGraph<T> graph)
    {
        var newGraph = new WeightedDiGraph<T, TW>();

        //clone graph vertices
        foreach (var vertex in graph.VerticesAsEnumberable) newGraph.AddVertex(vertex.Key);

        //clone edges
        foreach (var vertex in graph.VerticesAsEnumberable)
            //Use either OutEdges or InEdges for cloning
            //here we use OutEdges
        foreach (var edge in vertex.OutEdges)
        {
            //original edge
            newGraph.AddEdge(vertex.Key, edge.TargetVertexKey, edge.Weight<TW>());
            //add a backward edge for residual graph with edge value as default(W)
            newGraph.AddEdge(edge.TargetVertexKey, vertex.Key, default);
        }

        return newGraph;
    }
}

/// <summary>
///     Operators to deal with generic Add, Substract etc on edge weights for flow algorithms such as ford-fulkerson
///     algorithm.
/// </summary>
public interface IFlowOperators<TW> where TW : IComparable
{
    /// <summary>
    ///     default value for this type W.
    /// </summary>
    TW DefaultWeight { get; }

    /// <summary>
    ///     returns the max for this type W.
    /// </summary>
    TW MaxWeight { get; }

    /// <summary>
    ///     add two weights.
    /// </summary>
    TW AddWeights(TW a, TW b);

    /// <summary>
    ///     substract b from a.
    /// </summary>
    TW SubstractWeights(TW a, TW b);
}ParseOptions.0.jsonß?
^D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Flow\PushRelabel.csØ>using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A Push-Relabel algorithm implementation.
/// </summary>
public class PushRelabelMaxFlow<T, TW> where TW : IComparable
{
    private readonly IFlowOperators<TW> @operator;

    public PushRelabelMaxFlow(IFlowOperators<TW> @operator)
    {
        this.@operator = @operator;
    }

    /// <summary>
    ///     Computes Max Flow using Push-Relabel algorithm.
    /// </summary>
    public TW ComputeMaxFlow(IDiGraph<T> graph,
        T source, T sink)
    {
        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultWeight.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IFlowOperators<int> operator implementation during initialization.");

        //clone to create a residual graph
        var residualGraph = CreateResidualGraph(graph);

        //init vertex Height and Overflow object (ResidualGraphVertexStatus)
        var vertexStatusMap = new Dictionary<T, ResidualGraphVertexStatus>();
        foreach (var vertex in residualGraph.Vertices)
            if (vertex.Value.Key.Equals(source))
                //for source vertex
                //init source height to Maximum (equal to total vertex count)
                vertexStatusMap.Add(vertex.Value.Key,
                    new ResidualGraphVertexStatus(residualGraph.Vertices.Count,
                        @operator.DefaultWeight));
            else
                vertexStatusMap.Add(vertex.Value.Key,
                    new ResidualGraphVertexStatus(0,
                        @operator.DefaultWeight));

        //init source neighbour overflow to capacity of source-neighbour edges
        foreach (var edge in residualGraph.Vertices[source].OutEdges.ToList())
        {
            //update edge vertex overflow
            vertexStatusMap[edge.Key.Key].Overflow = edge.Value;

            //increment reverse edge
            residualGraph.Vertices[edge.Key.Key]
                .OutEdges[residualGraph.Vertices[source]] = edge.Value;

            //set to minimum
            residualGraph.Vertices[source].OutEdges[edge.Key] = @operator.DefaultWeight;
        }

        var overflowVertex = FindOverflowVertex(vertexStatusMap, source, sink);

        //until there is not more overflow vertices
        while (!overflowVertex.Equals(default(T)))
        {
            //if we can't push this vertex
            if (!Push(residualGraph.Vertices[overflowVertex], vertexStatusMap))
                //increase its height and try again
                Relabel(residualGraph.Vertices[overflowVertex], vertexStatusMap);

            overflowVertex = FindOverflowVertex(vertexStatusMap, source, sink);
        }

        //overflow of sink will be the net flow
        return vertexStatusMap[sink].Overflow;
    }

    /// <summary>
    ///     Increases the height of a vertex by one greater than min height of neighbours.
    /// </summary>
    private void Relabel(WeightedDiGraphVertex<T, TW> vertex,
        Dictionary<T, ResidualGraphVertexStatus> vertexStatusMap)
    {
        var min = int.MaxValue;

        foreach (var edge in vertex.OutEdges)
            //+ive out capacity  
            if (min.CompareTo(vertexStatusMap[edge.Key.Key].Height) > 0
                && edge.Value.CompareTo(@operator.DefaultWeight) > 0)
                min = vertexStatusMap[edge.Key.Key].Height;

        vertexStatusMap[vertex.Key].Height = min + 1;
    }

    /// <summary>
    ///     Tries to Push the overflow in current vertex to neighbours if possible.
    ///     Push is possible if neighbour edge is not full
    ///     and any of neighbour has height of current vertex
    ///     otherwise returns false.
    /// </summary>
    private bool Push(WeightedDiGraphVertex<T, TW> overflowVertex,
        Dictionary<T, ResidualGraphVertexStatus> vertexStatusMap)
    {
        var overflow = vertexStatusMap[overflowVertex.Key].Overflow;

        foreach (var edge in overflowVertex.OutEdges)
            //if out edge has +ive weight and neighbour height is less then flow is possible
            if (edge.Value.CompareTo(@operator.DefaultWeight) > 0
                && vertexStatusMap[edge.Key.Key].Height
                < vertexStatusMap[overflowVertex.Key].Height)
            {
                var possibleWeightToPush = edge.Value.CompareTo(overflow) < 0 ? edge.Value : overflow;

                //decrement overflow
                vertexStatusMap[overflowVertex.Key].Overflow =
                    @operator.SubstractWeights(vertexStatusMap[overflowVertex.Key].Overflow,
                        possibleWeightToPush);

                //increment flow of target vertex
                vertexStatusMap[edge.Key.Key].Overflow =
                    @operator.AddWeights(vertexStatusMap[edge.Key.Key].Overflow,
                        possibleWeightToPush);

                //decrement edge weight
                overflowVertex.OutEdges[edge.Key] = @operator.SubstractWeights(edge.Value, possibleWeightToPush);
                //increment reverse edge weight
                edge.Key.OutEdges[overflowVertex] =
                    @operator.AddWeights(edge.Key.OutEdges[overflowVertex], possibleWeightToPush);

                return true;
            }

        return false;
    }

    /// <summary>
    ///     Returns a vertex with an overflow.
    /// </summary>
    private T FindOverflowVertex(Dictionary<T, ResidualGraphVertexStatus> vertexStatusMap,
        T source, T sink)
    {
        foreach (var vertexStatus in vertexStatusMap)
            //ignore source and sink (which can have non-zero overflow)
            if (!vertexStatus.Key.Equals(source) && !vertexStatus.Key.Equals(sink) &&
                vertexStatus.Value.Overflow.CompareTo(@operator.DefaultWeight) > 0)
                return vertexStatus.Key;

        return default;
    }

    /// <summary>
    ///     Clones this graph and creates a residual graph.
    /// </summary>
    private WeightedDiGraph<T, TW> CreateResidualGraph(IDiGraph<T> graph)
    {
        var newGraph = new WeightedDiGraph<T, TW>();

        //clone graph vertices
        foreach (var vertex in graph.VerticesAsEnumberable) newGraph.AddVertex(vertex.Key);

        //clone edges
        foreach (var vertex in graph.VerticesAsEnumberable)
            //Use either OutEdges or InEdges for cloning
            //here we use OutEdges
        foreach (var edge in vertex.OutEdges)
        {
            //original edge
            newGraph.AddEdge(vertex.Key, edge.TargetVertexKey, edge.Weight<TW>());
            //add a backward edge for residual graph with edge value as default(W)
            newGraph.AddEdge(edge.TargetVertexKey, vertex.Key, default);
        }

        return newGraph;
    }

    /// <summary>
    ///     An object to keep track of Vertex Overflow and Height.
    /// </summary>
    internal class ResidualGraphVertexStatus
    {
        public ResidualGraphVertexStatus(int height, TW overflow)
        {
            Height = height;
            Overflow = overflow;
        }

        /// <summary>
        ///     Current overflow in this vertex.
        /// </summary>
        public TW Overflow { get; set; }

        /// <summary>
        ///     Current height of the vertex.
        /// </summary>
        public int Height { get; set; }
    }
}ParseOptions.0.json%
hD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Matching\BiPartiteMatching.csÓ$using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Compute Max BiParitite Edges using Ford-Fukerson algorithm.
/// </summary>
public class BiPartiteMatching<T>
{
    private readonly IBiPartiteMatchOperators<T> @operator;

    public BiPartiteMatching(IBiPartiteMatchOperators<T> @operator)
    {
        this.@operator = @operator;
    }

    /// <summary>
    ///     Returns a list of Max BiPartite Match Edges.
    /// </summary>
    public List<MatchEdge<T>> GetMaxBiPartiteMatching(IGraph<T> graph)
    {
        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type T during initialization.");

        //check if the graph is BiPartite by coloring 2 colors
        var mColorer = new MColorer<T, int>();
        var colorResult = mColorer.Color(graph, new[] { 1, 2 });

        if (colorResult.CanColor == false) throw new Exception("Graph is not BiPartite.");

        return GetMaxBiPartiteMatching(graph, colorResult.Partitions);
    }

    /// <summary>
    ///     Get Max Match from Given BiPartitioned Graph.
    /// </summary>
    private List<MatchEdge<T>> GetMaxBiPartiteMatching(IGraph<T> graph,
        Dictionary<int, List<T>> partitions)
    {
        //add unit edges from dymmy source to group 1 vertices
        var dummySource = @operator.GetRandomUniqueVertex();
        if (graph.ContainsVertex(dummySource))
            throw new Exception("Dummy vertex provided is not unique to given graph.");

        //add unit edges from group 2 vertices to sink
        var dummySink = @operator.GetRandomUniqueVertex();
        if (graph.ContainsVertex(dummySink)) throw new Exception("Dummy vertex provided is not unique to given graph.");

        var workGraph = CreateFlowGraph(graph, dummySource, dummySink, partitions);

        //run ford fulkerson using edmon karp method
        var fordFulkerson = new EdmondKarpMaxFlow<T, int>(@operator);

        var flowPaths = fordFulkerson
            .ComputeMaxFlowAndReturnFlowPath(workGraph, dummySource, dummySink);

        //now gather all group1 to group 2 edges in residual graph with positive flow
        var result = new List<MatchEdge<T>>();

        foreach (var path in flowPaths) result.Add(new MatchEdge<T>(path[1], path[2]));

        return result;
    }

    /// <summary>
    ///     create a directed unit weighted graph with given dummySource to Patition 1 and Patition 2 to dummy sink.
    /// </summary>
    private static WeightedDiGraph<T, int> CreateFlowGraph(IGraph<T> graph,
        T dummySource, T dummySink,
        Dictionary<int, List<T>> partitions)
    {
        var workGraph = new WeightedDiGraph<T, int>();
        workGraph.AddVertex(dummySource);

        foreach (var group1Vertex in partitions[1])
        {
            workGraph.AddVertex(group1Vertex);
            workGraph.AddEdge(dummySource, group1Vertex, 1);
        }

        workGraph.AddVertex(dummySink);

        foreach (var group2Vertex in partitions[2])
        {
            workGraph.AddVertex(group2Vertex);
            workGraph.AddEdge(group2Vertex, dummySink, 1);
        }

        //now add directed edges from group 1 vertices to group 2 vertices
        foreach (var group1Vertex in partitions[1])
        foreach (var edge in graph.GetVertex(group1Vertex).Edges)
            workGraph.AddEdge(group1Vertex, edge.TargetVertexKey, 1);

        return workGraph;
    }
}

/// <summary>
///     The match result object.
/// </summary>
public class MatchEdge<T>
{
    public MatchEdge(T source, T target)
    {
        Source = source;
        Target = target;
    }

    public T Source { get; }
    public T Target { get; }

    public override bool Equals(object obj)
    {
        if (obj == this) return true;

        var tgt = obj as MatchEdge<T>;

        if (tgt is null) return false;

        return tgt.Source.Equals(Source) && tgt.Target.Equals(Target);
    }

    public override int GetHashCode()
    {
        return new { Source, Target }.GetHashCode();
    }
}

/// <summary>
///     Generic operator interface required by BiPartite matching algorithm.
/// </summary>
public interface IBiPartiteMatchOperators<T> : IFlowOperators<int>
{
    /// <summary>
    ///     Get a random unique vertex not in graph
    ///     required for dummy source/destination vertex for ford-fulkerson max flow.
    /// </summary>
    T GetRandomUniqueVertex();
}ParseOptions.0.json≠:
cD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Matching\HopcroftKarp.cs∞9using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Compute Max BiParitite Edges using Hopcroft Karp algorithm.
/// </summary>
public class HopcroftKarpMatching<T>
{
    /// <summary>
    ///     Returns a list of Max BiPartite Match Edges.
    /// </summary>
    public HashSet<MatchEdge<T>> GetMaxBiPartiteMatching(IGraph<T> graph)
    {
        //check if the graph is BiPartite by coloring 2 colors
        var mColorer = new MColorer<T, int>();
        var colorResult = mColorer.Color(graph, new[] { 1, 2 });

        if (colorResult.CanColor == false) throw new Exception("Graph is not BiPartite.");

        return GetMaxBiPartiteMatching(graph, colorResult.Partitions);
    }

    /// <summary>
    ///     Get Max Match from Given BiPartitioned Graph.
    /// </summary>
    private HashSet<MatchEdge<T>> GetMaxBiPartiteMatching(IGraph<T> graph,
        Dictionary<int, List<T>> partitions)
    {
        var matches = new HashSet<MatchEdge<T>>();

        var leftToRightMatchEdges = new Dictionary<T, T>();
        var rightToLeftMatchEdges = new Dictionary<T, T>();

        var freeVerticesOnRight = Bfs(graph, partitions, leftToRightMatchEdges, rightToLeftMatchEdges);
        //while there is an augmenting Path
        while (freeVerticesOnRight.Count > 0)
        {
            var visited = new HashSet<T>();
            var path = new HashSet<MatchEdge<T>>();

            foreach (var vertex in freeVerticesOnRight)
            {
                var currentPath = Dfs(graph,
                    leftToRightMatchEdges, rightToLeftMatchEdges, vertex, default, visited, true);

                if (currentPath != null) Union(path, currentPath);
            }

            Xor(matches, path, leftToRightMatchEdges, rightToLeftMatchEdges);

            freeVerticesOnRight = Bfs(graph, partitions, leftToRightMatchEdges, rightToLeftMatchEdges);
        }

        return matches;
    }

    /// <summary>
    ///     Returns list of free vertices on right if there is an augmenting Path from left to right.
    ///     An augmenting path is a path which starts from a free vertex
    ///     and ends at a free vertex via UnMatched (left -> right) and Matched (right -> left) edges alternatively.
    /// </summary>
    private List<T> Bfs(IGraph<T> graph,
        Dictionary<int, List<T>> partitions,
        Dictionary<T, T> leftToRightMatchEdges, Dictionary<T, T> rightToLeftMatchEdges)
    {
        var queue = new Queue<T>();
        var visited = new HashSet<T>();

        var freeVerticesOnRight = new List<T>();

        foreach (var vertex in partitions[1])
            //if this left vertex is free
            if (!leftToRightMatchEdges.ContainsKey(vertex) && !visited.Contains(vertex))
            {
                queue.Enqueue(vertex);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    visited.Add(vertex);

                    //unmatched edges left to right
                    foreach (var leftToRightEdge in graph.GetVertex(current).Edges)
                    {
                        if (visited.Contains(leftToRightEdge.TargetVertexKey)) continue;

                        //checking if this right vertex is free
                        if (!rightToLeftMatchEdges.ContainsKey(leftToRightEdge.TargetVertex.Key))
                            freeVerticesOnRight.Add(leftToRightEdge.TargetVertex.Key);
                        else
                            foreach (var rightToLeftEdge in leftToRightEdge.TargetVertex.Edges)
                                //matched edge right to left
                                if (leftToRightMatchEdges.ContainsKey(rightToLeftEdge.TargetVertexKey)
                                    && !visited.Contains(rightToLeftEdge.TargetVertexKey))
                                    queue.Enqueue(rightToLeftEdge.TargetVertexKey);

                        visited.Add(leftToRightEdge.TargetVertexKey);
                    }
                }
            }

        return freeVerticesOnRight;
    }

    /// <summary>
    ///     Find an augmenting path that start from a given free vertex on right and ending
    ///     at a free vertex on left, via Matched (right -> left) and UnMatched (left -> right) edges alternatively.
    ///     Return the matching edges along that path.
    /// </summary>
    private HashSet<MatchEdge<T>> Dfs(IGraph<T> graph,
        Dictionary<T, T> leftToRightMatchEdges,
        Dictionary<T, T> rightToLeftMatchEdges,
        T current,
        T previous,
        HashSet<T> visited,
        bool currentIsRight)
    {
        var currentIsLeft = !currentIsRight;

        if (visited.Contains(current)) return null;

        //free vertex on left found!
        if (currentIsLeft && !leftToRightMatchEdges.ContainsKey(current))
        {
            visited.Add(current);
            return new HashSet<MatchEdge<T>> { new(current, previous) };
        }

        //right to left should be unmatched edges
        if (currentIsRight && !rightToLeftMatchEdges.ContainsKey(current))
            foreach (var edge in graph.GetVertex(current).Edges)
            {
                var result = Dfs(graph, leftToRightMatchEdges, rightToLeftMatchEdges, edge.TargetVertexKey, current,
                    visited, !currentIsRight);
                if (result != null)
                {
                    result.Add(new MatchEdge<T>(edge.TargetVertexKey, current));
                    visited.Add(current);
                    return result;
                }
            }

        //left to right should be matched edges
        if (currentIsLeft && leftToRightMatchEdges.ContainsKey(current))
            foreach (var edge in graph.GetVertex(current).Edges)
            {
                var result = Dfs(graph, leftToRightMatchEdges, rightToLeftMatchEdges, edge.TargetVertexKey, current,
                    visited, !currentIsRight);
                if (result != null)
                {
                    result.Add(new MatchEdge<T>(current, edge.TargetVertexKey));
                    visited.Add(current);
                    return result;
                }
            }

        return null;
    }

    private void Union(HashSet<MatchEdge<T>> paths, HashSet<MatchEdge<T>> path)
    {
        foreach (var item in path)
            if (!paths.Contains(item))
                paths.Add(item);
    }

    private void Xor(HashSet<MatchEdge<T>> matches, HashSet<MatchEdge<T>> paths,
        Dictionary<T, T> leftToRightMatchEdges, Dictionary<T, T> rightToLeftMatchEdges)
    {
        foreach (var item in paths)
            if (matches.Contains(item))
            {
                matches.Remove(item);
                leftToRightMatchEdges.Remove(item.Source);
                rightToLeftMatchEdges.Remove(item.Target);
            }
            else
            {
                matches.Add(item);
                leftToRightMatchEdges.Add(item.Source, item.Target);
                rightToLeftMatchEdges.Add(item.Target, item.Source);
            }
    }
}ParseOptions.0.json§ 
jD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\MinimumSpanningTree\Kruskals.cs†using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.Sorting;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A Kruskal's alogorithm implementation
///     using merge sort and disjoint set.
/// </summary>
public class Kruskals<T, TW> where TW : IComparable
{
    /// <summary>
    ///     Find Minimum Spanning Tree of given weighted graph.
    /// </summary>
    /// <returns>List of MST edges</returns>
    public List<MstEdge<T, TW>>
        FindMinimumSpanningTree(IGraph<T> graph)
    {
        var edges = new List<MstEdge<T, TW>>();

        //gather all unique edges
        Dfs(graph.ReferenceVertex, new HashSet<T>(),
            new Dictionary<T, HashSet<T>>(),
            edges);

        //quick sort preparation
        var sortArray = new MstEdge<T, TW>[edges.Count];
        for (var i = 0; i < edges.Count; i++) sortArray[i] = edges[i];

        //quick sort edges
        var sortedEdges = MergeSort<MstEdge<T, TW>>.Sort(sortArray);

        var result = new List<MstEdge<T, TW>>();
        var disJointSet = new DisJointSet<T>();

        //create set
        foreach (var vertex in graph.VerticesAsEnumberable) disJointSet.MakeSet(vertex.Key);

        //pick each edge one by one
        //if both source and target belongs to same set 
        //then don't add the edge to result
        //otherwise add it to result and union sets
        for (var i = 0; i < edges.Count; i++)
        {
            var currentEdge = sortedEdges[i];

            var setA = disJointSet.FindSet(currentEdge.Source);
            var setB = disJointSet.FindSet(currentEdge.Destination);

            //can't pick edge with both ends already in MST
            if (setA.Equals(setB)) continue;

            result.Add(currentEdge);

            //union picked edge vertice sets
            disJointSet.Union(setA, setB);
        }

        return result;
    }

    /// <summary>
    ///     Do DFS to find all unique edges.
    /// </summary>
    private void Dfs(IGraphVertex<T> currentVertex, HashSet<T> visitedVertices, Dictionary<T, HashSet<T>> visitedEdges,
        List<MstEdge<T, TW>> result)
    {
        if (!visitedVertices.Contains(currentVertex.Key))
        {
            visitedVertices.Add(currentVertex.Key);

            foreach (var edge in currentVertex.Edges)
            {
                if (!visitedEdges.ContainsKey(currentVertex.Key)
                    || !visitedEdges[currentVertex.Key].Contains(edge.TargetVertexKey))
                {
                    result.Add(new MstEdge<T, TW>(currentVertex.Key, edge.TargetVertexKey, edge.Weight<TW>()));

                    //update visited edge
                    if (!visitedEdges.ContainsKey(currentVertex.Key))
                        visitedEdges.Add(currentVertex.Key, new HashSet<T>());

                    visitedEdges[currentVertex.Key].Add(edge.TargetVertexKey);

                    //update visited back edge
                    if (!visitedEdges.ContainsKey(edge.TargetVertexKey))
                        visitedEdges.Add(edge.TargetVertexKey, new HashSet<T>());

                    visitedEdges[edge.TargetVertexKey].Add(currentVertex.Key);
                }

                Dfs(edge.TargetVertex, visitedVertices, visitedEdges, result);
            }
        }
    }
}

/// <summary>
///     Minimum spanning tree edge object.
/// </summary>
public class MstEdge<T, TW> : IComparable where TW : IComparable
{
    internal MstEdge(T source, T dest, TW weight)
    {
        Source = source;
        Destination = dest;
        Weight = weight;
    }

    public T Source { get; }
    public T Destination { get; }
    public TW Weight { get; }

    public int CompareTo(object obj)
    {
        return Weight.CompareTo(((MstEdge<T, TW>)obj).Weight);
    }
}ParseOptions.0.jsonö
gD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\MinimumSpanningTree\Prims.csôusing System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A Prims algorithm implementation.
/// </summary>
public class Prims<T, TW> where TW : IComparable
{
    /// <summary>
    ///     Find Minimum Spanning Tree of given undirected graph.
    /// </summary>
    /// <returns>List of MST edges</returns>
    public List<MstEdge<T, TW>>
        FindMinimumSpanningTree(IGraph<T> graph)
    {
        var edges = new List<MstEdge<T, TW>>();

        //gather all unique edges
        Dfs(graph, graph.ReferenceVertex,
            new BHeap<MstEdge<T, TW>>(),
            new HashSet<T>(),
            edges);

        return edges;
    }

    /// <summary>
    ///     Do DFS to pick smallest weight neighbour edges
    ///     of current spanning tree one by one.
    /// </summary>
    /// <param name="spanTreeNeighbours"> Use Fibonacci Min Heap to pick smallest edge neighbour </param>
    /// <param name="spanTreeEdges">result MST edges</param>
    private void Dfs(IGraph<T> graph, IGraphVertex<T> currentVertex,
        BHeap<MstEdge<T, TW>> spanTreeNeighbours, HashSet<T> spanTreeVertices,
        List<MstEdge<T, TW>> spanTreeEdges)
    {
        while (true)
        {
            //add all edges to Fibonacci Heap
            //So that we can pick the min edge in next step
            foreach (var edge in currentVertex.Edges)
                spanTreeNeighbours.Insert(new MstEdge<T, TW>(currentVertex.Key, edge.TargetVertexKey, edge.Weight<TW>()));

            //pick min edge
            var minNeighbourEdge = spanTreeNeighbours.Extract();

            //skip edges already in MST
            while (spanTreeVertices.Contains(minNeighbourEdge.Source) &&
                   spanTreeVertices.Contains(minNeighbourEdge.Destination))
            {
                minNeighbourEdge = spanTreeNeighbours.Extract();

                //if no more neighbours to explore 
                //time to end exploring
                if (spanTreeNeighbours.Count == 0) return;
            }

            //keep track of visited vertices
            //do not duplicate vertex
            if (!spanTreeVertices.Contains(minNeighbourEdge.Source)) spanTreeVertices.Add(minNeighbourEdge.Source);

            //Destination vertex will never be a duplicate
            //since this is an unexplored Vertex
            spanTreeVertices.Add(minNeighbourEdge.Destination);

            //add edge to result
            spanTreeEdges.Add(minNeighbourEdge);

            //now explore the destination vertex
            var graph1 = graph;
            currentVertex = graph1.GetVertex(minNeighbourEdge.Destination);
        }
    }
}ParseOptions.0.json˙
bD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Search\BiDirectional.cs˛using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A BiDirectional Path Search on DiGraph.
/// </summary>
public class BiDirectional<T>
{
    /// <summary>
    ///     Returns true if Path exists from source to destination.
    /// </summary>
    public bool PathExists(IGraph<T> graph, T source, T destination)
    {
        return Bfs(graph, source, destination);
    }

    /// <summary>
    ///     Use breadth First Search from Source and Target until they meet.
    ///     If they could'nt find the element before they meet return false.
    /// </summary>
    private bool Bfs(IGraph<T> graph, T source, T destination)
    {
        var visitedA = new HashSet<T>();
        var visitedB = new HashSet<T>();

        var bfsQueueA = new Queue<IGraphVertex<T>>();
        var bfsQueueB = new Queue<IGraphVertex<T>>();

        bfsQueueA.Enqueue(graph.GetVertex(source));
        bfsQueueB.Enqueue(graph.GetVertex(destination));

        visitedA.Add(graph.GetVertex(source).Key);
        visitedB.Add(graph.GetVertex(destination).Key);

        //search from both ends for a Path
        while (true)
        {
            if (bfsQueueA.Count > 0)
            {
                var current = bfsQueueA.Dequeue();

                //intersects with search from other end
                if (visitedB.Contains(current.Key)) return true;

                foreach (var edge in current.Edges)
                {
                    if (visitedA.Contains(edge.TargetVertexKey)) continue;

                    visitedA.Add(edge.TargetVertexKey);
                    bfsQueueA.Enqueue(edge.TargetVertex);
                }
            }

            if (bfsQueueB.Count > 0)
            {
                var current = bfsQueueB.Dequeue();

                //intersects with search from other end
                if (visitedA.Contains(current.Key)) return true;

                foreach (var edge in current.Edges)
                {
                    if (visitedB.Contains(edge.TargetVertexKey)) continue;

                    visitedB.Add(edge.TargetVertexKey);
                    bfsQueueB.Enqueue(edge.TargetVertex);
                }
            }

            if (bfsQueueA.Count == 0 && bfsQueueB.Count == 0) break;
        }

        return false;
    }
}ParseOptions.0.json—

aD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Search\BreadthFirst.cs÷	using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Bread First Search implementation.
/// </summary>
public class BreadthFirst<T>
{
    /// <summary>
    ///     Returns true if item exists.
    /// </summary>
    public bool Find(IGraph<T> graph, T vertex)
    {
        return Bfs(graph.ReferenceVertex, new HashSet<T>(), vertex);
    }

    /// <summary>
    ///     BFS implementation.
    /// </summary>
    private bool Bfs(IGraphVertex<T> referenceVertex,
        HashSet<T> visited, T searchVertex)
    {
        var bfsQueue = new Queue<IGraphVertex<T>>();

        bfsQueue.Enqueue(referenceVertex);
        visited.Add(referenceVertex.Key);

        while (bfsQueue.Count > 0)
        {
            var current = bfsQueue.Dequeue();

            if (current.Key.Equals(searchVertex)) return true;

            foreach (var edge in current.Edges)
            {
                if (visited.Contains(edge.TargetVertexKey)) continue;

                visited.Add(edge.TargetVertexKey);
                bfsQueue.Enqueue(edge.TargetVertex);
            }
        }

        return false;
    }
}ParseOptions.0.json¶
_D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Search\DepthFirst.cs≠using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Depth First Search.
/// </summary>
public class DepthFirst<T>
{
    /// <summary>
    ///     Returns true if item exists.
    /// </summary>
    public bool Find(IGraph<T> graph, T vertex)
    {
        return Dfs(graph.ReferenceVertex, new HashSet<T>(), vertex);
    }

    /// <summary>
    ///     Recursive DFS.
    /// </summary>
    private bool Dfs(IGraphVertex<T> current,
        HashSet<T> visited, T searchVetex)
    {
        visited.Add(current.Key);

        if (current.Key.Equals(searchVetex)) return true;

        foreach (var edge in current.Edges)
        {
            if (visited.Contains(edge.TargetVertexKey)) continue;

            if (Dfs(edge.TargetVertex, visited, searchVetex)) return true;
        }

        return false;
    }
}ParseOptions.0.json¿<
`D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\ShortestPath\AStar.cs∆;using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A* algorithm implementation using Fibonacci Heap.
/// </summary>
public class AStarShortestPath<T, TW> where TW : IComparable
{
    private readonly IAStarHeuristic<T, TW> heuristic;
    private readonly IShortestPathOperators<TW> @operator;

    public AStarShortestPath(IShortestPathOperators<TW> @operator, IAStarHeuristic<T, TW> heuristic)
    {
        this.@operator = @operator;
        this.heuristic = heuristic;
    }

    /// <summary>
    ///     Search path to target using the heuristic.
    /// </summary>
    public ShortestPathResult<T, TW> FindShortestPath(IGraph<T> graph, T source, T destination)
    {
        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultValue.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IShortestPathOperators<int> operator implementation during initialization.");

        //regular argument checks
        if (graph?.GetVertex(source) == null || graph.GetVertex(destination) == null) throw new ArgumentException();

        //track progress for distance to each Vertex from source
        var progress = new Dictionary<T, TW>();

        //trace our current path by mapping current vertex to its Parent
        var parentMap = new Dictionary<T, T>();

        //min heap to pick next closest vertex 
        var minHeap = new FibonacciHeap<AStarWrap<T, TW>>();
        //keep references of heap Node for decrement key operation
        var heapMapping = new Dictionary<T, AStarWrap<T, TW>>();

        //add vertices to min heap and progress map
        foreach (var vertex in graph.VerticesAsEnumberable)
        {
            //init parent
            parentMap.Add(vertex.Key, default);

            //init to max value
            progress.Add(vertex.Key, @operator.MaxValue);

            if (vertex.Key.Equals(source)) continue;
        }

        //start from source vertex as current 
        var current = new AStarWrap<T, TW>(heuristic, destination)
        {
            Distance = @operator.DefaultValue,
            Vertex = source
        };

        //insert neighbour in heap
        minHeap.Insert(current);
        heapMapping[source] = current;

        //until heap is empty
        while (minHeap.Count > 0)
        {
            //next min vertex to visit
            current = minHeap.Extract();
            heapMapping.Remove(current.Vertex);

            //no path exists, so return max value
            if (current.Distance.Equals(@operator.MaxValue))
                return new ShortestPathResult<T, TW>(null, @operator.MaxValue);

            //visit neighbours of current
            foreach (var neighbour in graph.GetVertex(current.Vertex).Edges
                         .Where(x => !x.TargetVertexKey.Equals(source)))
            {
                //new distance to neighbour
                var newDistance = @operator.Sum(current.Distance,
                    graph.GetVertex(current.Vertex).GetEdge(neighbour.TargetVertex).Weight<TW>());

                //current distance to neighbour
                var existingDistance = progress[neighbour.TargetVertexKey];

                //update distance if new is better
                if (newDistance.CompareTo(existingDistance) < 0)
                {
                    progress[neighbour.TargetVertexKey] = newDistance;

                    if (heapMapping.ContainsKey(neighbour.TargetVertexKey))
                    {
                        //decrement distance to neighbour in heap
                        var decremented = new AStarWrap<T, TW>(heuristic, destination)
                        {
                            Distance = newDistance,
                            Vertex = neighbour.TargetVertexKey
                        };

                        minHeap.UpdateKey(heapMapping[neighbour.TargetVertexKey], decremented);
                        heapMapping[neighbour.TargetVertexKey] = decremented;
                    }
                    else
                    {
                        //insert neighbour in heap
                        var discovered = new AStarWrap<T, TW>(heuristic, destination)
                        {
                            Distance = newDistance,
                            Vertex = neighbour.TargetVertexKey
                        };

                        minHeap.Insert(discovered);
                        heapMapping[neighbour.TargetVertexKey] = discovered;
                    }

                    //trace parent
                    parentMap[neighbour.TargetVertexKey] = current.Vertex;
                }
            }
        }

        return TracePath(graph, parentMap, source, destination);
    }

    /// <summary>
    ///     Trace back path from destination to source using parent map.
    /// </summary>
    private ShortestPathResult<T, TW> TracePath(IGraph<T> graph, Dictionary<T, T> parentMap, T source, T destination)
    {
        //trace the path
        var pathStack = new Stack<T>();

        pathStack.Push(destination);

        var currentV = destination;
        while (!Equals(currentV, default(T)) && !Equals(parentMap[currentV], default(T)))
        {
            pathStack.Push(parentMap[currentV]);
            currentV = parentMap[currentV];
        }

        //return result
        var resultPath = new List<T>();
        var resultLength = @operator.DefaultValue;
        while (pathStack.Count > 0) resultPath.Add(pathStack.Pop());

        for (var i = 0; i < resultPath.Count - 1; i++)
            resultLength = @operator.Sum(resultLength,
                graph.GetVertex(resultPath[i]).GetEdge(graph.GetVertex(resultPath[i + 1])).Weight<TW>());

        return new ShortestPathResult<T, TW>(resultPath, resultLength);
    }
}

/// <summary>
///     Search heuristic used by A* search algorithm.
/// </summary>
public interface IAStarHeuristic<T, TW> where TW : IComparable
{
    /// <summary>
    ///     Return the distance to target for given sourcevertex as computed by the hueristic used for A* search.
    /// </summary>
    TW HueristicDistanceToTarget(T sourceVertex, T targetVertex);
}

//Node for our Fibonacci heap
internal class AStarWrap<T, TW> : IComparable where TW : IComparable
{
    private readonly T destinationVertex;
    private readonly IAStarHeuristic<T, TW> heuristic;

    internal AStarWrap(IAStarHeuristic<T, TW> heuristic, T destinationVertex)
    {
        this.heuristic = heuristic;
        this.destinationVertex = destinationVertex;
    }

    internal T Vertex { get; set; }
    internal TW Distance { get; set; }

    //compare distance to target using the heuristic provided
    public int CompareTo(object obj)
    {
        if (this == obj) return 0;

        var result1 = heuristic.HueristicDistanceToTarget(Vertex, destinationVertex);
        var result2 = heuristic.HueristicDistanceToTarget((obj as AStarWrap<T, TW>).Vertex, destinationVertex);

        return result1.CompareTo(result2);
    }
}ParseOptions.0.json´!
gD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\ShortestPath\Bellman-Ford.cs™ using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A Bellman Ford algorithm implementation.
/// </summary>
public class BellmanFordShortestPath<T, TW> where TW : IComparable
{
    private readonly IShortestPathOperators<TW> @operator;

    public BellmanFordShortestPath(IShortestPathOperators<TW> @operator)
    {
        this.@operator = @operator;
    }

    /// <summary>
    ///     Find shortest distance to target.
    /// </summary>
    public ShortestPathResult<T, TW> FindShortestPath(IDiGraph<T> graph,
        T source, T destination)
    {
        //regular argument checks
        if (graph == null || graph.GetVertex(source) == null
                          || graph.GetVertex(destination) == null)
            throw new ArgumentException("Empty Graph or invalid source/destination.");

        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultValue.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IShortestPathOperators<int> operator implementation during initialization.");

        var progress = new Dictionary<T, TW>();
        var parentMap = new Dictionary<T, T>();

        foreach (var vertex in graph.VerticesAsEnumberable)
        {
            parentMap.Add(vertex.Key, default);
            progress.Add(vertex.Key, @operator.MaxValue);
        }

        progress[source] = @operator.DefaultValue;

        var iterations = graph.VerticesCount - 1;
        var updated = true;

        while (iterations > 0 && updated)
        {
            updated = false;

            foreach (var vertex in graph.VerticesAsEnumberable)
            {
                //skip not discovered nodes
                if (progress[vertex.Key].Equals(@operator.MaxValue)) continue;

                foreach (var edge in vertex.OutEdges)
                {
                    var currentDistance = progress[edge.TargetVertexKey];
                    var newDistance = @operator.Sum(progress[vertex.Key],
                        vertex.GetOutEdge(edge.TargetVertex).Weight<TW>());

                    if (newDistance.CompareTo(currentDistance) < 0)
                    {
                        updated = true;
                        progress[edge.TargetVertexKey] = newDistance;
                        parentMap[edge.TargetVertexKey] = vertex.Key;
                    }
                }
            }

            iterations--;

            if (iterations < 0) throw new Exception("Negative cycle exists in this graph.");
        }

        return TracePath(graph, parentMap, source, destination);
    }

    /// <summary>
    ///     Trace back path from destination to source using parent map.
    /// </summary>
    private ShortestPathResult<T, TW> TracePath(IDiGraph<T> graph,
        Dictionary<T, T> parentMap, T source, T destination)
    {
        //trace the path
        var pathStack = new Stack<T>();

        pathStack.Push(destination);

        var currentV = destination;
        while (!Equals(currentV, default(T)) && !Equals(parentMap[currentV], default(T)))
        {
            pathStack.Push(parentMap[currentV]);
            currentV = parentMap[currentV];
        }

        //return result
        var resultPath = new List<T>();
        var resultLength = @operator.DefaultValue;
        while (pathStack.Count > 0) resultPath.Add(pathStack.Pop());

        for (var i = 0; i < resultPath.Count - 1; i++)
            resultLength = @operator.Sum(resultLength,
                graph.GetVertex(resultPath[i]).GetOutEdge(graph.GetVertex(resultPath[i + 1])).Weight<TW>());

        return new ShortestPathResult<T, TW>(resultPath, resultLength);
    }
}ParseOptions.0.json»6
dD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\ShortestPath\Dijikstra.cs 5using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A dijikstra algorithm implementation using Fibonacci Heap.
/// </summary>
public class DijikstraShortestPath<T, TW> where TW : IComparable
{
    private readonly IShortestPathOperators<TW> @operator;

    public DijikstraShortestPath(IShortestPathOperators<TW> @operator)
    {
        this.@operator = @operator;
    }

    /// <summary>
    ///     Get shortest distance to target.
    /// </summary>
    public ShortestPathResult<T, TW> FindShortestPath(IGraph<T> graph, T source, T destination)
    {
        //regular argument checks
        if (graph?.GetVertex(source) == null || graph.GetVertex(destination) == null) throw new ArgumentException();

        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultValue.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IShortestPathOperators<int> operator implementation during initialization.");

        //track progress for distance to each Vertex from source
        var progress = new Dictionary<T, TW>();

        //trace our current path by mapping current vertex to its Parent
        var parentMap = new Dictionary<T, T>();

        //min heap to pick next closest vertex 
        var minHeap = new FibonacciHeap<MinHeapWrap<T, TW>>();
        //keep references of heap Node for decrement key operation
        var heapMapping = new Dictionary<T, MinHeapWrap<T, TW>>();

        //add vertices to min heap and progress map
        foreach (var vertex in graph.VerticesAsEnumberable)
        {
            //init parent
            parentMap.Add(vertex.Key, default);

            //init to max value
            progress.Add(vertex.Key, @operator.MaxValue);

            if (vertex.Key.Equals(source)) continue;
        }

        //start from source vertex as current 
        var current = new MinHeapWrap<T, TW>
        {
            Distance = @operator.DefaultValue,
            Vertex = source
        };

        minHeap.Insert(current);
        heapMapping.Add(current.Vertex, current);

        //until heap is empty
        while (minHeap.Count > 0)
        {
            //next min vertex to visit
            current = minHeap.Extract();
            heapMapping.Remove(current.Vertex);

            //no path exists, so return max value
            if (current.Distance.Equals(@operator.MaxValue))
                return new ShortestPathResult<T, TW>(null, @operator.MaxValue);

            //visit neighbours of current
            foreach (var neighbour in graph.GetVertex(current.Vertex).Edges
                         .Where(x => !x.TargetVertexKey.Equals(source)))
            {
                //new distance to neighbour
                var newDistance = @operator.Sum(current.Distance,
                    graph.GetVertex(current.Vertex).GetEdge(neighbour.TargetVertex).Weight<TW>());

                //current distance to neighbour
                var existingDistance = progress[neighbour.TargetVertexKey];

                //update distance if new is better
                if (newDistance.CompareTo(existingDistance) < 0)
                {
                    progress[neighbour.TargetVertexKey] = newDistance;

                    if (!heapMapping.ContainsKey(neighbour.TargetVertexKey))
                    {
                        var wrap = new MinHeapWrap<T, TW> { Distance = newDistance, Vertex = neighbour.TargetVertexKey };
                        minHeap.Insert(wrap);
                        heapMapping.Add(neighbour.TargetVertexKey, wrap);
                    }
                    else
                    {
                        //decrement distance to neighbour in heap
                        var decremented = new MinHeapWrap<T, TW>
                            { Distance = newDistance, Vertex = neighbour.TargetVertexKey };
                        minHeap.UpdateKey(heapMapping[neighbour.TargetVertexKey], decremented);
                        heapMapping[neighbour.TargetVertexKey] = decremented;
                    }

                    //trace parent
                    parentMap[neighbour.TargetVertexKey] = current.Vertex;
                }
            }
        }

        return TracePath(graph, parentMap, source, destination);
    }

    /// <summary>
    ///     Trace back path from destination to source using parent map.
    /// </summary>
    private ShortestPathResult<T, TW> TracePath(IGraph<T> graph, Dictionary<T, T> parentMap, T source, T destination)
    {
        //trace the path
        var pathStack = new Stack<T>();

        pathStack.Push(destination);

        var currentV = destination;
        while (!Equals(currentV, default(T)) && !Equals(parentMap[currentV], default(T)))
        {
            pathStack.Push(parentMap[currentV]);
            currentV = parentMap[currentV];
        }

        //return result
        var resultPath = new List<T>();
        var resultLength = @operator.DefaultValue;
        while (pathStack.Count > 0) resultPath.Add(pathStack.Pop());

        for (var i = 0; i < resultPath.Count - 1; i++)
            resultLength = @operator.Sum(resultLength,
                graph.GetVertex(resultPath[i]).GetEdge(graph.GetVertex(resultPath[i + 1])).Weight<TW>());

        return new ShortestPathResult<T, TW>(resultPath, resultLength);
    }
}

/// <summary>
///     Generic operators interface required by shorted path algorithms.
/// </summary>
public interface IShortestPathOperators<TW> where TW : IComparable
{
    TW DefaultValue { get; }
    TW MaxValue { get; }
    TW Sum(TW a, TW b);
}

/// <summary>
///     Shortest path result object.
/// </summary>
public class ShortestPathResult<T, TW> where TW : IComparable
{
    public ShortestPathResult(List<T> path, TW length)
    {
        Length = length;
        Path = path;
    }

    public TW Length { get; internal set; }
    public List<T> Path { get; }
}

/// <summary>
///     For fibornacci heap node.
/// </summary>
internal class MinHeapWrap<T, TW> : IComparable where TW : IComparable
{
    internal T Vertex { get; set; }
    internal TW Distance { get; set; }

    public int CompareTo(object obj)
    {
        return Distance.CompareTo((obj as MinHeapWrap<T, TW>).Distance);
    }
}ParseOptions.0.json„(
iD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\ShortestPath\Floyd-Warshall.cs‡'using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A floyd-warshall shortest path algorithm implementation.
/// </summary>
public class FloydWarshallShortestPath<T, TW> where TW : IComparable
{
    private readonly IShortestPathOperators<TW> @operator;

    public FloydWarshallShortestPath(IShortestPathOperators<TW> @operator)
    {
        this.@operator = @operator;
    }

    public List<AllPairShortestPathResult<T, TW>> FindAllPairShortestPaths(IGraph<T> graph)
    {
        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultValue.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IShortestPathOperators<int> operator implementation during initialization.");

        //we need this vertex array index for generics
        //since array indices are int and T is unknown type
        var vertexIndex = new Dictionary<int, T>();
        var reverseVertexIndex = new Dictionary<T, int>();
        var i = 0;
        foreach (var vertex in graph.VerticesAsEnumberable)
        {
            vertexIndex.Add(i, vertex.Key);
            reverseVertexIndex.Add(vertex.Key, i);
            i++;
        }

        //init all distance to default Weight
        var result = new TW[graph.VerticesCount, graph.VerticesCount];
        //to trace the path
        var parent = new T[graph.VerticesCount, graph.VerticesCount];
        for (i = 0; i < graph.VerticesCount; i++)
        for (var j = 0; j < graph.VerticesCount; j++)
            result[i, j] = @operator.MaxValue;

        for (i = 0; i < graph.VerticesCount; i++) result[i, i] = @operator.DefaultValue;
        //now set the known edge weights to neighbours
        for (i = 0; i < graph.VerticesCount; i++)
            foreach (var edge in graph.GetVertex(vertexIndex[i]).Edges)
            {
                result[i, reverseVertexIndex[edge.TargetVertexKey]] = edge.Weight<TW>();
                parent[i, reverseVertexIndex[edge.TargetVertexKey]] = graph.GetVertex(vertexIndex[i]).Key;

                result[reverseVertexIndex[edge.TargetVertexKey], i] = edge.Weight<TW>();
                parent[reverseVertexIndex[edge.TargetVertexKey], i] = edge.TargetVertexKey;
            }

        //here is the meat of this algorithm
        //if we can reach node i to j via node k and if it is shorter pick that Distance
        for (var k = 0; k < graph.VerticesCount; k++)
        for (i = 0; i < graph.VerticesCount; i++)
        for (var j = 0; j < graph.VerticesCount; j++)
        {
            //no path
            if (result[i, k].Equals(@operator.MaxValue)
                || result[k, j].Equals(@operator.MaxValue))
                continue;

            var sum = @operator.Sum(result[i, k], result[k, j]);

            if (sum.CompareTo(result[i, j]) >= 0) continue;

            result[i, j] = sum;
            parent[i, j] = parent[k, j];
        }

        //trace path
        var finalResult = new List<AllPairShortestPathResult<T, TW>>();
        for (i = 0; i < graph.VerticesCount; i++)
        for (var j = 0; j < graph.VerticesCount; j++)
        {
            var source = vertexIndex[i];
            var dest = vertexIndex[j];
            var distance = result[i, j];
            var path = TracePath(result, parent, i, j, vertexIndex, reverseVertexIndex);

            finalResult.Add(new AllPairShortestPathResult<T, TW>(source, dest, distance, path));
        }

        return finalResult;
    }

    /// <summary>
    ///     Trace path from dest to source.
    /// </summary>
    private List<T> TracePath(TW[,] result, T[,] parent, int i, int j,
        Dictionary<int, T> vertexIndex, Dictionary<T, int> reverseVertexIndex)
    {
        var pathStack = new Stack<T>();

        pathStack.Push(vertexIndex[j]);

        var current = parent[i, j];
        while (i != j)
        {
            pathStack.Push(parent[i, j]);
            j = reverseVertexIndex[parent[i, j]];
        }

        var path = new List<T>();

        while (pathStack.Count > 0) path.Add(pathStack.Pop());

        return path;
    }
}

/// <summary>
///     All pairs shortest path algorithm result object.
/// </summary>
public class AllPairShortestPathResult<T, TW> where TW : IComparable
{
    public AllPairShortestPathResult(T source, T destination,
        TW distance, List<T> path)
    {
        Source = source;
        Destination = destination;
        Distance = distance;
        Path = path;
    }

    public T Source { get; }
    public T Destination { get; }

    public TW Distance { get; }

    public List<T> Path { get; }
}ParseOptions.0.jsonΩ"
cD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\ShortestPath\Johnsons.cs¿!using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A Johnson's shortest path algorithm implementation.
/// </summary>
public class JohnsonsShortestPath<T, TW> where TW : IComparable
{
    private readonly IJohnsonsShortestPathOperators<T, TW> @operator;

    public JohnsonsShortestPath(IJohnsonsShortestPathOperators<T, TW> @operator)
    {
        this.@operator = @operator;
    }

    public List<AllPairShortestPathResult<T, TW>>
        FindAllPairShortestPaths(IDiGraph<T> graph)
    {
        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultValue.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IJohnsonsShortestPathOperators<T, int> operator implementation during initialization.");

        var workGraph = Clone(graph);

        //add an extra vertex with zero weight edge to all nodes
        var randomVetex = @operator.RandomVertex();

        if (workGraph.Vertices.ContainsKey(randomVetex))
            throw new Exception("Random Vertex is not unique for given graph.");
        workGraph.AddVertex(randomVetex);

        foreach (var vertex in workGraph.Vertices) workGraph.AddEdge(randomVetex, vertex.Key, @operator.DefaultValue);

        //now compute shortest path from random vertex to all other vertices
        var bellmanFordSp = new BellmanFordShortestPath<T, TW>(@operator);
        var bellFordResult = new Dictionary<T, TW>();
        foreach (var vertex in workGraph.Vertices)
        {
            var result = bellmanFordSp.FindShortestPath(workGraph, randomVetex, vertex.Key);
            bellFordResult.Add(vertex.Key, result.Length);
        }

        //adjust edges so that all edge values are now +ive
        foreach (var vertex in workGraph.Vertices)
        foreach (var edge in vertex.Value.OutEdges.ToList())
            vertex.Value.OutEdges[edge.Key] = @operator.Substract(
                @operator.Sum(bellFordResult[vertex.Key], edge.Value),
                bellFordResult[edge.Key.Key]);

        workGraph.RemoveVertex(randomVetex);
        //now run dijikstra for all pairs of vertices
        //trace path
        var dijikstras = new DijikstraShortestPath<T, TW>(@operator);
        var finalResult = new List<AllPairShortestPathResult<T, TW>>();
        foreach (var vertexA in workGraph.Vertices)
        foreach (var vertexB in workGraph.Vertices)
        {
            var source = vertexA.Key;
            var dest = vertexB.Key;
            var sp = dijikstras.FindShortestPath(workGraph, source, dest);

            //no path exists
            if (sp.Length.Equals(@operator.MaxValue)) continue;

            var distance = sp.Length;
            var path = sp.Path;

            finalResult.Add(new AllPairShortestPathResult<T, TW>(source, dest, distance, path));
        }

        return finalResult;
    }

    private WeightedDiGraph<T, TW> Clone(IDiGraph<T> graph)
    {
        var newGraph = new WeightedDiGraph<T, TW>();

        foreach (var vertex in graph.VerticesAsEnumberable) newGraph.AddVertex(vertex.Key);

        foreach (var vertex in graph.VerticesAsEnumberable)
        foreach (var edge in vertex.OutEdges)
            newGraph.AddEdge(vertex.Key, edge.TargetVertexKey, edge.Weight<TW>());

        return newGraph;
    }
}

/// <summary>
///     A concrete implementation of this interface is required by Johnson's algorithm.
/// </summary>
public interface IJohnsonsShortestPathOperators<T, TW>
    : IShortestPathOperators<TW> where TW : IComparable
{
    /// <summary>
    ///     Substract a from b.
    /// </summary>
    TW Substract(TW a, TW b);

    /// <summary>
    ///     Gives a random vertex value not in the graph.
    /// </summary>
    T RandomVertex();
}ParseOptions.0.jsonä
mD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\ShortestPath\TravellingSalesman.csÉusing System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Uses dynamic programming for a
///     psuedo-polynomial time runTime complexity for this NP hard problem.
/// </summary>
public class TravellingSalesman<T, TW> where TW : IComparable
{
    private IShortestPathOperators<TW> @operator;

    public TW FindMinWeight(IGraph<T> graph, IShortestPathOperators<TW> @operator)
    {
        this.@operator = @operator;
        if (this.@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (this.@operator.DefaultValue.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IShortestPathOperators<int> operator implementation during initialization.");

        return FindMinWeight(graph.ReferenceVertex, graph.ReferenceVertex,
            graph.VerticesCount,
            new HashSet<IGraphVertex<T>>(),
            new Dictionary<string, TW>());
    }

    private TW FindMinWeight(IGraphVertex<T> sourceVertex,
        IGraphVertex<T> tgtVertex,
        int remainingVertexCount,
        HashSet<IGraphVertex<T>> visited,
        Dictionary<string, TW> cache)
    {
        var cacheKey = $"{sourceVertex.Key}-{remainingVertexCount}";

        if (cache.ContainsKey(cacheKey)) return cache[cacheKey];

        visited.Add(sourceVertex);

        var results = new List<TW>();

        foreach (var edge in sourceVertex.Edges)
        {
            //base case
            if (edge.TargetVertex.Equals(tgtVertex)
                && remainingVertexCount == 1)
            {
                results.Add(edge.Weight<TW>());
                break;
            }

            if (!visited.Contains(edge.TargetVertex))
            {
                var result = FindMinWeight(edge.TargetVertex, tgtVertex, remainingVertexCount - 1, visited, cache);

                if (!result.Equals(@operator.MaxValue)) results.Add(@operator.Sum(result, edge.Weight<TW>()));
            }
        }

        visited.Remove(sourceVertex);

        if (results.Count == 0) return @operator.MaxValue;

        var min = results.Min();
        cache.Add(cacheKey, min);
        return min;
    }
}ParseOptions.0.jsonˆ
dD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Sort\DepthFirstTopSort.cs¯
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Find Toplogical order of a graph using Depth First Search.
/// </summary>
public class DepthFirstTopSort<T>
{
    /// <summary>
    ///     Returns the vertices in Topologically Sorted Order.
    /// </summary>
    public List<T> GetTopSort(IDiGraph<T> graph)
    {
        var pathStack = new Stack<T>();
        var visited = new HashSet<T>();

        //we need a loop so that we can reach all vertices
        foreach (var vertex in graph.VerticesAsEnumberable)
            if (!visited.Contains(vertex.Key))
                Dfs(vertex, visited, pathStack);

        //now just pop the stack to result
        var result = new List<T>();
        while (pathStack.Count > 0) result.Add(pathStack.Pop());

        return result;
    }

    /// <summary>
    ///     Do a depth first search.
    /// </summary>
    private void Dfs(IDiGraphVertex<T> vertex,
        HashSet<T> visited, Stack<T> pathStack)
    {
        visited.Add(vertex.Key);

        foreach (var edge in vertex.OutEdges)
            if (!visited.Contains(edge.TargetVertexKey))
                Dfs(edge.TargetVertex, visited, pathStack);

        //add vertex to stack after all edges are visited
        pathStack.Push(vertex.Key);
    }
}ParseOptions.0.json¸
^D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Graph\Sort\KahnTopSort.csÑusing System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Find Toplogical order of a graph using Kahn's algorithm.
/// </summary>
public class KahnsTopSort<T>
{
    /// <summary>
    ///     Returns the vertices in Topologically Sorted Order.
    /// </summary>
    public List<T> GetTopSort(IDiGraph<T> graph)
    {
        var inEdgeMap = new Dictionary<T, int>();

        var kahnQueue = new Queue<T>();

        foreach (var vertex in graph.VerticesAsEnumberable)
        {
            inEdgeMap.Add(vertex.Key, vertex.InEdgeCount);

            //init queue with vertices having not in edges
            if (vertex.InEdgeCount == 0) kahnQueue.Enqueue(vertex.Key);
        }

        //no vertices with zero number of in edges
        if (kahnQueue.Count == 0) throw new Exception("Graph has a cycle.");

        var result = new List<T>();

        var visitCount = 0;
        //until queue is empty
        while (kahnQueue.Count > 0)
        {
            //cannot exceed vertex number of iterations
            if (visitCount > graph.VerticesCount) throw new Exception("Graph has a cycle.");

            //pick a neighbour
            var nextPick = graph.GetVertex(kahnQueue.Dequeue());

            //if in edge count is 0 then ready for result
            if (inEdgeMap[nextPick.Key] == 0) result.Add(nextPick.Key);

            //decrement in edge count for neighbours
            foreach (var edge in nextPick.OutEdges)
            {
                inEdgeMap[edge.TargetVertexKey]--;
                kahnQueue.Enqueue(edge.TargetVertexKey);
            }

            visitCount++;
        }

        return result;
    }
}ParseOptions.0.json¥

`D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Numerical\Exponentiation.cs∫	namespace Advanced.Algorithms.Numerical;

/// <summary>
///     A fast exponentiation algorithm implementation.
/// </summary>
public class FastExponentiation
{
    /// <summary>
    ///     Computes exponentiation using squaring.
    /// </summary>
    public static int BySquaring(int @base, int power)
    {
        while (true)
        {
            //using the algebraic result
            //a^-n  = (1/a)^n
            if (power < 0)
            {
                @base = 1 / @base;
                power = -power;
                continue;
            }

            switch (power)
            {
                case 0:
                    return 1;
                case 1:
                    return @base;
                default:
                    if (power % 2 == 0)
                    {
                        @base = @base * @base;
                        power = power / 2;
                        continue;
                    }
                    //power is odd
                    else
                    {
                        return @base * BySquaring(@base * @base, (power - 1) / 2);
                    }
            }
        }
    }
}ParseOptions.0.json≈
`D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Numerical\PrimeGenerator.csÀusing System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Numerical;

/// <summary>
///     A prime number generation algorithm using Sieve of Eratosthenes.
/// </summary>
public class PrimeGenerator
{
    public static List<int> GetAllPrimes(int max)
    {
        var primeTable = new bool[max + 1];

        var sqrt = Math.Sqrt(max);

        for (var i = 2; i < sqrt; i++)
        {
            //mark multiples of current number as true
            if (primeTable[i]) continue;

            for (var j = 2 * i; j <= max; j = j + i) primeTable[j] = true;
        }

        //now write back results
        var result = new List<int>();

        for (var i = 2; i < primeTable.Length; i++)
            if (!primeTable[i])
                result.Add(i);

        return result;
    }
}ParseOptions.0.jsonø
]D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Numerical\PrimeTester.cs»using System;

namespace Advanced.Algorithms.Numerical;

/// <summary>
///     Tests for Prime in School method optimized.
/// </summary>
public class PrimeTester
{
    /// <summary>
    ///     Check if given number is prime.
    /// </summary>
    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;

        if (number <= 3) return true;

        //number can be divided by 2 or 3
        if (number % 2 == 0 || number % 3 == 0) return false;

        //skip six numbers in each step
        //since we don't need to check for 3 even numbers 
        //and one number divisible by 3
        //inside the loop
        //check until square root of number
        var sqrt = Math.Sqrt(number);
        for (var i = 5; i <= sqrt; i = i + 6)
            //check for two potential primes
            if (number % i == 0 || number % (i + 2) == 0)
                return false;

        return true;
    }
}ParseOptions.0.jsonÜ
_D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Properties\AssemblyInfo.csçusing System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Advanced.Algorithms")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Advanced.Algorithms")]
[assembly: AssemblyCopyright("Copyright ¬©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: InternalsVisibleTo("Advanced.Algorithms.Tests, PublicKey=" +
                              "00240000048000009400000006020000002400005253413100040000010001009d6e7fa635694c" +
                              "4f15e68996fe058ea398fcc08bd885b68a9b1027d0a89d90b009565a4dae145119bdc6f8454241" +
                              "8e36122767529b7e2b3fb126f986e55458e01ddb5c2e93e913fce5c32d1ce98ac741a8dd2f1c76" +
                              "29bd6bfd11fe86f6ea25aacb1c16e085d61103b16f8f667068415b7b02633b61aff76406e10313" +
                              "0bcf3ee5")]
[assembly: InternalsVisibleTo("Advanced.Algorithms.Tests, PublicKey=" +
                              "00240000048000009400000006020000002400005253413100040000010001009d6e7fa635694c" +
                              "4f15e68996fe058ea398fcc08bd885b68a9b1027d0a89d90b009565a4dae145119bdc6f8454241" +
                              "8e36122767529b7e2b3fb126f986e55458e01ddb5c2e93e913fce5c32d1ce98ac741a8dd2f1c76" +
                              "29bd6bfd11fe86f6ea25aacb1c16e085d61103b16f8f667068415b7b02633b61aff76406e10313" +
                              "0bcf3ee5")]ParseOptions.0.jsoní
[D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Search\BinarySearch.csùnamespace Advanced.Algorithms.Search;

/// <summary>
///     A binary search algorithm implementation.
/// </summary>
public class BinarySearch
{
    public static int Search(int[] input, int element)
    {
        return Search(input, 0, input.Length - 1, element);
    }

    private static int Search(int[] input, int i, int j, int element)
    {
        while (true)
        {
            if (i == j)
            {
                if (input[i] == element) return i;

                return -1;
            }

            var mid = (i + j) / 2;

            if (input[mid] == element) return mid;

            if (input[mid] > element)
            {
                j = mid;
                continue;
            }

            i = mid + 1;
        }
    }
}ParseOptions.0.json—

YD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Search\BoyerMoore.csﬁ	using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Search;

/// <summary>
///     A boyer-moore majority finder algorithm implementation.
/// </summary>
public class BoyerMoore<T> where T : IComparable
{
    public static T FindMajority(IEnumerable<T> input)
    {
        var candidate = FindMajorityCandidate(input, input.Count());

        if (Verify(input, input.Count(), candidate)) return candidate;

        return default;
    }

    //Find majority candidate
    private static T FindMajorityCandidate(IEnumerable<T> input, int length)
    {
        var count = 1;
        var candidate = input.First();

        foreach (var element in input.Skip(1))
        {
            if (candidate.Equals(element))
                count++;
            else
                count--;

            if (count == 0)
            {
                candidate = element;
                count = 1;
            }
        }

        return candidate;
    }

    //verify that candidate is indeed the majority
    private static bool Verify(IEnumerable<T> input, int size, T candidate)
    {
        return input.Count(x => x.Equals(candidate)) > size / 2;
    }
}ParseOptions.0.jsonü
ZD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Search\QuickSelect.cs´using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.Sorting;

namespace Advanced.Algorithms.Search;

/// <summary>
///     A quick select for Kth smallest algorithm implementation.
/// </summary>
public class QuickSelect<T> where T : IComparable
{
    public static T FindSmallest(IEnumerable<T> input, int k)
    {
        var inputArray = input.ToArray();

        var left = 0;
        var right = inputArray.Length - 1;

        var rnd = new Random();

        while (left <= right)
        {
            var median = MedianOfMedian(inputArray, left, right);

            var pivot = 0;

            for (var i = left; i <= right; i++)
                if (inputArray[i].Equals(median))
                {
                    pivot = i;
                    break;
                }

            var newPivot = Partition(inputArray, left, right, pivot);

            if (newPivot == k - 1)
                return inputArray[newPivot];
            if (newPivot > k - 1)
                right = newPivot - 1;
            else
                left = newPivot + 1;
        }

        return default;
    }

    private static T MedianOfMedian(T[] input, int left, int right)
    {
        if (left.CompareTo(right) == 0) return input[left];

        var comparer = new CustomComparer<T>(SortDirection.Ascending, Comparer<T>.Default);

        var size = 5;
        var currentLeft = left;

        var medians = new T[(right - left) / size + 1];
        var medianIndex = -1;
        while (currentLeft <= right)
        {
            var currentRight = currentLeft + size - 1;

            if (currentRight <= right)
            {
                Sort(input, currentLeft, currentRight, comparer);
                medians[++medianIndex] = Median(input, currentLeft, currentRight);
            }
            else
            {
                Sort(input, currentLeft, right, comparer);
                medians[++medianIndex] = Median(input, currentLeft, right);
            }

            currentLeft = currentRight + 1;
        }

        if (medians.Length == 1) return medians[0];

        return MedianOfMedian(medians, 0, medians.Length - 1);
    }

    //partition using pivot
    private static int Partition(T[] input, int left, int right, int pivot)
    {
        var pivotValue = input[pivot];
        var newPivot = left;

        //prevent comparing pivot against itself
        Swap(input, pivot, right);

        for (var i = left; i < right; i++)
            if (input[i].CompareTo(pivotValue) < 0)
            {
                Swap(input, i, newPivot);
                newPivot++;
            }

        //move pivot back to middle
        Swap(input, newPivot, right);

        return newPivot;
    }

    private static void Sort(T[] input, int left, int right, CustomComparer<T> comparer)
    {
        MergeSort<T>.PartitionMerge(input, left, right, comparer);
    }

    private static T Median(T[] input, int left, int right)
    {
        return input[left + (right - left) / 2];
    }

    private static void Swap(T[] input, int i, int j)
    {
        if (i != j)
        {
            var tmp = input[i];
            input[i] = input[j];
            input[j] = tmp;
        }
    }
}ParseOptions.0.json£
]D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Shared\CustomComparer.cs¨using System;
using System.Collections.Generic;

namespace Advanced.Algorithms;

internal class CustomComparer<T> : IComparer<T> where T : IComparable
{
    private readonly IComparer<T> comparer;
    private readonly bool isMax;

    internal CustomComparer(SortDirection sortDirection, IComparer<T> comparer)
    {
        isMax = sortDirection == SortDirection.Descending;
        this.comparer = comparer;
    }

    public int Compare(T x, T y)
    {
        return !isMax ? comparer.Compare(x, y) : comparer.Compare(y, x);
    }
}ParseOptions.0.jsonﬁ
\D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Shared\SortDirection.csinamespace Advanced.Algorithms;

public enum SortDirection
{
    Ascending = 0,
    Descending = 1
}ParseOptions.0.json◊
ZD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\BubbleSort.cs„using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     A bubble sort implementation.
/// </summary>
public class BubbleSort<T> where T : IComparable
{
    /// <summary>
    ///     Time complexity: O(n^2).
    /// </summary>
    public static T[] Sort(T[] array, SortDirection sortDirection = SortDirection.Ascending)
    {
        var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        var swapped = true;

        while (swapped)
        {
            swapped = false;

            for (var i = 0; i < array.Length - 1; i++)
                //compare adjacent elements 
                if (comparer.Compare(array[i], array[i + 1]) > 0)
                {
                    var temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                    swapped = true;
                }
        }

        return array;
    }
}ParseOptions.0.jsonÙ
ZD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\BucketSort.csÄusing System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     A bucket sort implementation.
/// </summary>
public class BucketSort
{
    /// <summary>
    ///     Sort given integers using bucket sort with merge sort as sub sort.
    /// </summary>
    public static int[] Sort(int[] array, int bucketSize, SortDirection sortDirection = SortDirection.Ascending)
    {
        if (bucketSize < 0 || bucketSize > array.Length) throw new Exception("Invalid bucket size.");

        var buckets = new Dictionary<int, List<int>>();

        int i;
        for (i = 0; i < array.Length; i++)
        {
            if (bucketSize == 0) continue;

            var bucketIndex = array[i] / bucketSize;

            if (!buckets.ContainsKey(bucketIndex)) buckets.Add(bucketIndex, new List<int>());

            buckets[bucketIndex].Add(array[i]);
        }

        i = 0;
        var bucketKeys = new int[buckets.Count];
        foreach (var bucket in buckets.ToList())
        {
            buckets[bucket.Key] = new List<int>(MergeSort<int>
                .Sort(bucket.Value.ToArray(), sortDirection));

            bucketKeys[i] = bucket.Key;
            i++;
        }

        bucketKeys = MergeSort<int>.Sort(bucketKeys, sortDirection);

        var result = new int[array.Length];

        i = 0;
        foreach (var bucketKey in bucketKeys)
        {
            var bucket = buckets[bucketKey];
            Array.Copy(bucket.ToArray(), 0, result, i, bucket.Count);
            i += bucket.Count;
        }

        return result;
    }
}ParseOptions.0.json¶
\D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\CountingSort.cs∞using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     A counting sort implementation.
/// </summary>
public class CountingSort
{
    /// <summary>
    ///     Sort given integers.
    /// </summary>
    public static int[] Sort(IEnumerable<int> enumerable, SortDirection sortDirection = SortDirection.Ascending)
    {
        var lengthAndMax = GetLengthAndMax(enumerable);

        var length = lengthAndMax.Item1;
        var max = lengthAndMax.Item2;

        //add one more space for zero
        var countArray = new int[max + 1];

        //count the appearances of elements
        foreach (var item in enumerable)
        {
            if (item < 0) throw new Exception("Negative numbers not supported.");

            countArray[item]++;
        }

        //now aggregate and assign the sum from left to right
        var sum = countArray[0];
        for (var i = 1; i <= max; i++)
        {
            sum += countArray[i];
            countArray[i] = sum;
        }

        var result = new int[length];

        //now assign result
        foreach (var item in enumerable)
        {
            var index = countArray[item];
            result[sortDirection == SortDirection.Ascending ? index - 1 : result.Length - index] = item;
            countArray[item]--;
        }

        return result;
    }

    /// <summary>
    ///     Get Max of given array.
    /// </summary>
    private static Tuple<int, int> GetLengthAndMax(IEnumerable<int> array)
    {
        var length = 0;
        var max = int.MinValue;
        foreach (var item in array)
        {
            length++;
            if (item.CompareTo(max) > 0) max = item;
        }

        return new Tuple<int, int>(length, max);
    }
}ParseOptions.0.jsoné

XD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\HeapSort.csú	/* Unmerged change from project 'Advanced.Algorithms (netstandard1.0)'
Before:
using System;
After:
using Advanced.Algorithms.DataStructures;
using System;
*/

using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures;
/* Unmerged change from project 'Advanced.Algorithms (netstandard1.0)'
Before:
using System.Linq;
using Advanced.Algorithms.DataStructures;
After:
using System.Linq;
*/

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     A heap sort implementation.
/// </summary>
public class HeapSort<T> where T : IComparable
{
    /// <summary>
    ///     Time complexity: O(nlog(n)).
    /// </summary>
    public static T[] Sort(ICollection<T> collection, SortDirection sortDirection = SortDirection.Ascending)
    {
        //heapify
        var heap = new BHeap<T>(sortDirection, collection);

        //now extract min until empty and return them as sorted array
        var sortedArray = new T[collection.Count];
        var j = 0;
        while (heap.Count > 0)
        {
            sortedArray[j] = heap.Extract();
            j++;
        }

        return sortedArray;
    }
}ParseOptions.0.json˜
]D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\InsertionSort.csÄusing System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     An insertion sort implementation.
/// </summary>
public class InsertionSort<T> where T : IComparable
{
    /// <summary>
    ///     Time complexity: O(n^2).
    /// </summary>
    public static T[] Sort(T[] array, SortDirection sortDirection = SortDirection.Ascending)
    {
        var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        for (var i = 0; i < array.Length - 1; i++)
        for (var j = i + 1; j > 0; j--)
            if (comparer.Compare(array[j], array[j - 1]) < 0)
            {
                var temp = array[j - 1];
                array[j - 1] = array[j];
                array[j] = temp;
            }
            else
            {
                break;
            }

        return array;
    }
}ParseOptions.0.jsonÓ
YD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\MergeSort.cs˚using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     A merge sort implementation.
/// </summary>
public class MergeSort<T> where T : IComparable
{
    /// <summary>
    ///     Time complexity: O(nlog(n)).
    /// </summary>
    public static T[] Sort(T[] array, SortDirection sortDirection = SortDirection.Ascending)
    {
        PartitionMerge(array, 0, array.Length - 1, new CustomComparer<T>(sortDirection, Comparer<T>.Default));
        return array;
    }

    internal static void PartitionMerge(T[] array, int leftIndex, int rightIndex,
        CustomComparer<T> comparer)
    {
        if (leftIndex < 0 || rightIndex < 0 || rightIndex - leftIndex + 1 < 2) return;

        var middle = (leftIndex + rightIndex) / 2;

        PartitionMerge(array, leftIndex, middle, comparer);
        PartitionMerge(array, middle + 1, rightIndex, comparer);

        Merge(array, leftIndex, middle, rightIndex, comparer);
    }

    /// <summary>
    ///     Merge two sorted arrays.
    /// </summary>
    private static void Merge(T[] array, int leftStart, int middle, int rightEnd,
        CustomComparer<T> comparer)
    {
        var newLength = rightEnd - leftStart + 1;

        var result = new T[newLength];

        int i = leftStart, j = middle + 1, k = 0;
        //iteratively compare and pick min to result
        while (i <= middle && j <= rightEnd)
        {
            if (comparer.Compare(array[i], array[j]) < 0)
            {
                result[k] = array[i];
                i++;
            }
            else
            {
                result[k] = array[j];
                j++;
            }

            k++;
        }

        //copy left overs
        if (i <= middle)
            for (var l = i; l <= middle; l++)
            {
                result[k] = array[l];
                k++;
            }
        else
            for (var l = j; l <= rightEnd; l++)
            {
                result[k] = array[l];
                k++;
            }

        k = 0;
        //now write back result
        for (var g = leftStart; g <= rightEnd; g++)
        {
            array[g] = result[k];
            k++;
        }
    }
}ParseOptions.0.json÷
YD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\QuickSort.cs„using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     A quick sort implementation.
/// </summary>
public class QuickSort<T> where T : IComparable
{
    /// <summary>
    ///     Time complexity: O(n^2)
    /// </summary>
    public static T[] Sort(T[] array, SortDirection sortDirection = SortDirection.Ascending)
    {
        if (array.Length <= 1) return array;

        var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        Sort(array, 0, array.Length - 1, comparer);

        return array;
    }

    private static void Sort(T[] array, int startIndex, int endIndex, CustomComparer<T> comparer)
    {
        while (true)
        {
            //if only one element the do nothing
            if (startIndex < 0 || endIndex < 0 || endIndex - startIndex < 1) return;

            //set the wall to the left most index
            var wall = startIndex;

            //pick last index element on array as comparison pivot
            var pivot = array[endIndex];

            //swap elements greater than pivot to the right side of wall
            //others will be on left
            for (var j = wall; j <= endIndex; j++)
            {
                if (comparer.Compare(pivot, array[j]) <= 0 && j != endIndex) continue;

                var temp = array[wall];
                array[wall] = array[j];
                array[j] = temp;
                //increment to exclude the minimum element in subsequent comparisons
                wall++;
            }

            //sort left
            Sort(array, startIndex, wall - 2, comparer);
            //sort right
            startIndex = wall;
        }
    }
}ParseOptions.0.jsonÖ
YD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\RadixSort.csíusing System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     A radix sort implementation.
/// </summary>
public class RadixSort
{
    public static int[] Sort(int[] array, SortDirection sortDirection = SortDirection.Ascending)
    {
        int i;
        for (i = 0; i < array.Length; i++)
            if (array[i] < 0)
                throw new Exception("Negative numbers not supported.");

        var @base = 1;
        var max = array.Max();


        while (max / @base > 0)
        {
            //create a bucket for digits 0 to 9
            var buckets = new List<int>[10];

            for (i = 0; i < array.Length; i++)
            {
                var bucketIndex = array[i] / @base % 10;

                if (buckets[bucketIndex] == null) buckets[bucketIndex] = new List<int>();

                buckets[bucketIndex].Add(array[i]);
            }

            //now update array with what is in buckets
            var orderedBuckets = sortDirection == SortDirection.Ascending ? buckets : buckets.Reverse();

            i = 0;
            foreach (var bucket in orderedBuckets.Where(x => x != null))
            foreach (var item in bucket)
            {
                array[i] = item;
                i++;
            }

            @base *= 10;
        }

        return array;
    }
}ParseOptions.0.jsonÏ
]D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\SelectionSort.csıusing System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     A selection sort implementation.
/// </summary>
public class SelectionSort<T> where T : IComparable
{
    /// <summary>
    ///     Time complexity: O(n^2).
    /// </summary>
    public static T[] Sort(T[] array, SortDirection sortDirection = SortDirection.Ascending)
    {
        var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        for (var i = 0; i < array.Length; i++)
            //select the smallest item in sub array and move it to front
        for (var j = i + 1; j < array.Length; j++)
        {
            if (comparer.Compare(array[j], array[i]) >= 0) continue;

            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        return array;
    }
}ParseOptions.0.jsonä	
YD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\ShellSort.csóusing System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     A shell sort implementation.
/// </summary>
public class ShellSort<T> where T : IComparable
{
    public static T[] Sort(T[] array, SortDirection sortDirection = SortDirection.Ascending)
    {
        var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

        var k = array.Length / 2;
        var j = 0;

        while (k >= 1)
        {
            for (var i = k; i < array.Length; i = i + k, j = j + k)
            {
                if (comparer.Compare(array[i], array[j]) >= 0) continue;

                Swap(array, i, j);

                if (i <= k) continue;

                i -= k * 2;
                j -= k * 2;
            }

            j = 0;
            k /= 2;
        }

        return array;
    }

    private static void Swap(T[] array, int i, int j)
    {
        var tmp = array[i];
        array[i] = array[j];
        array[j] = tmp;
    }
}ParseOptions.0.json…
XD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Sorting\TreeSort.cs◊using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Sorting;

/// <summary>
///     A tree sort implementation.
/// </summary>
public class TreeSort<T> where T : IComparable
{
    /// <summary>
    ///     Time complexity: O(nlog(n)).
    /// </summary>
    public static IEnumerable<T> Sort(IEnumerable<T> enumerable, SortDirection sortDirection = SortDirection.Ascending)
    {
        //create BST
        var tree = new RedBlackTree<T>();
        foreach (var item in enumerable) tree.Insert(item);

        return sortDirection == SortDirection.Ascending ? tree.AsEnumerable() : tree.AsEnumerableDesc();
    }
}ParseOptions.0.jsonÍ&
bD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\String\ManachersPalindrome.csÓ%using System;
using System.Linq;
using System.Text;

namespace Advanced.Algorithms.String;

/// <summary>
///     A Manacher's longest palindrome implementation.
/// </summary>
public class ManachersPalindrome
{
    public int FindLongestPalindrome(string input)
    {
        if (input.Length <= 1) throw new ArgumentException("Invalid input");

        if (input.Contains("$")) throw new Exception("Input contain sentinel character $.");

        //for even length palindrome
        //we need to do this hack with $
        var array = input.ToCharArray();
        var modifiedInput = new StringBuilder();

        foreach (var item in array)
        {
            modifiedInput.Append("$");
            modifiedInput.Append(item.ToString());
        }

        modifiedInput.Append("$");

        var result = FindLongestPalindromeR(modifiedInput.ToString());

        //remove length of $ sentinel
        return result / 2;
    }

    /// <summary>
    ///     Find the longest palindrome in linear time.
    /// </summary>
    private int FindLongestPalindromeR(string input)
    {
        var palindromeLengths = new int[input.Length];

        int left = -1, right = 1;
        var length = 1;

        var i = 0;
        //loop through each char
        while (i < input.Length)
        {
            //terminate if end of input
            while (left >= 0 && right < input.Length)
                if (input[left] == input[right])
                {
                    left--;
                    right++;
                    length += 2;
                }
                else
                {
                    //end of current palindrome
                    break;
                }

            var @continue = false;

            //set length of current palindrome
            palindromeLengths[i] = length;

            //use mirror values on left side of palindrome
            //to fill palindrome lengths on right side of palindrome
            //so that we can save computations
            if (right > i + 2)
            {
                var l = i - 1;
                var r = i + 1;

                //start from current palindrome center
                //all the way to right end of current palindrome
                while (r < right)
                {
                    //find mirror char palindrome length
                    var mirrorLength = palindromeLengths[l];

                    //mirror palindrome left end exceeds
                    //current palindrom left end
                    if (l - mirrorLength / 2 < left + 1)
                    {
                        //set length equals to maximum
                        //we can reach and then continue exploring
                        palindromeLengths[r] = 2 * (l - (left + 1)) + 1;
                        r++;
                        l--;
                    }
                    //mirror palindrome is totally contained
                    //in our current palindrome
                    else if (l - mirrorLength / 2 > left + 1
                             && r + mirrorLength / 2 < right - 1)
                    {
                        //so just set the value and continue exploring
                        palindromeLengths[r] = palindromeLengths[l];
                        r++;
                        l--;
                    }
                    //mirror palindrome exactly fits inside right side
                    //of current palindrome
                    else
                    {
                        //set length equals to maximum
                        //and then continue exploring in main loop
                        length = palindromeLengths[l];

                        //continue to main loop
                        //update state values to skip
                        //already computed values
                        i = r;
                        left = i - length / 2 - 1;
                        right = i + length / 2 + 1;

                        @continue = true;
                        break;
                    }
                }

                //already computed until i-1 by now
                i = r;
            }

            //continue to main loop
            //states values are already set
            if (@continue) continue;

            //reset as usual
            left = i;
            right = i + 2;
            length = 1;

            i++;
        }

        return FindMax(palindromeLengths);
    }

    /// <summary>
    ///     Returns the max index in given int[] array.
    /// </summary>
    private int FindMax(int[] palindromeLengths)
    {
        return palindromeLengths.Concat(new[] { int.MinValue }).Max();
    }
}ParseOptions.0.json
YD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\String\Search\KMP.cs˝namespace Advanced.Algorithms.String;

/// <summary>
///     Knuth‚ÄìMorris‚ÄìPratt(KMP) string search implementation.
/// </summary>
public class Kmp
{
    /// <summary>
    ///     Returns the start index of first appearance
    ///     of pattern in input string.
    ///     Returns -1 if no match.
    /// </summary>
    public int Search(string input, string pattern)
    {
        var matchingInProgress = false;
        var matchIndex = new int[pattern.Length];
        var j = 0;

        //create match index of chars
        //to keep track of closest suffixes in pattern that form prefix of pattern
        for (var i = 1; i < pattern.Length; i++)
            //prefix don't match suffix anymore
            if (!pattern[i].Equals(pattern[j]))
            {
                //don't skip unmatched i for next iteration
                //since our for loop increments i
                if (matchingInProgress) i--;

                matchingInProgress = false;

                //move back j to the beginning of last matched char
                j = matchIndex[j == 0 ? 0 : j - 1];
            }
            //prefix match suffix so far
            else
            {
                matchingInProgress = true;
                //increment index of suffix 
                //to prefix index for corresponding char
                matchIndex[i] = j + 1;
                j++;
            }

        matchingInProgress = false;
        //now start matching
        j = 0;

        for (var i = 0; i < input.Length; i++)
            if (input[i] == pattern[j])
            {
                matchingInProgress = true;
                j++;

                //match complete
                if (j == pattern.Length) return i - pattern.Length + 1;
            }
            else
            {
                //reduce i by one so that next comparison won't skip current i
                //which is not matching with current j
                //since our for loop increments i
                if (matchingInProgress) i--;

                matchingInProgress = false;

                //jump back to closest suffix with prefix of pattern
                if (j != 0) j = matchIndex[j - 1];
            }

        return -1;
    }
}ParseOptions.0.jsonﬂ
_D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\String\Search\RabinKarp.csÊusing System;

namespace Advanced.Algorithms.String;

/// <summary>
///     A Rabin-Karp string search implementation.
/// </summary>
public class RabinKarp
{
    /// <summary>
    ///     Hard coding this, ideally should be a large prime
    ///     To reduce collisions.
    /// </summary>
    private const int PrimeNumber = 101;

    private readonly double tolerance = 0.00001;

    public int Search(string input, string pattern)
    {
        var patternHash = ComputeHash(pattern);
        var hash = ComputeHash(input.Substring(0, pattern.Length));

        if (Math.Abs(hash - patternHash) < tolerance)
            if (Valid(pattern, input.Substring(0, pattern.Length)))
                return 0;

        var lashHash = hash;

        for (var i = 1; i < input.Length - pattern.Length + 1; i++)
        {
            var newHash = ComputeHash(lashHash, pattern.Length, input[i - 1],
                input[i + pattern.Length - 1]);

            if (Math.Abs(newHash - patternHash) < tolerance)
                if (Valid(pattern, input.Substring(i, pattern.Length)))
                    return i;

            lashHash = newHash;
        }

        return -1;
    }


    /// <summary>
    ///     Returns true if matched hash string is same as the pattern.
    /// </summary>
    private bool Valid(string pattern, string match)
    {
        return pattern.Equals(match);
    }

    /// <summary>
    ///     Compute hash given a string.
    /// </summary>
    private double ComputeHash(string input)
    {
        double result = 0;
        for (var i = 0; i < input.Length; i++) result += input[i] * Math.Pow(PrimeNumber, i);

        return result;
    }

    /// <summary>
    ///     Compute hash given a newChar and last hash.
    /// </summary>
    private double ComputeHash(double lastHash, int patternLength,
        char removedChar, char newChar)
    {
        lastHash -= removedChar;
        var newHashHash = lastHash / PrimeNumber
                          + newChar * Math.Pow(PrimeNumber, patternLength - 1);

        return newHashHash;
    }
}ParseOptions.0.json≥
`D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\String\Search\ZAlgorithm.csπnamespace Advanced.Algorithms.String;

/// <summary>
///     A Z-algorithm implementation for string search.
/// </summary>
public class ZAlgorithm
{
    /// <summary>
    ///     Returns the start index of first appearance
    ///     of pattern in input string.
    ///     returns -1 if no match.
    /// </summary>
    public int Search(string input, string pattern)
    {
        var z = this.Z(pattern + input, pattern.Length);

        for (var i = pattern.Length; i < z.Length; i++)
            //if match length equals pattern Length + separator length
            if (z[i] == pattern.Length)
                //substract pattern length and separator length
                return i - pattern.Length;

        return -1;
    }

    /// <summary>
    ///     The z function computes the length of matching prefix at each char
    ///     in given input.
    private int[] Z(string input, int patternLength)
    {
        var result = new int[input.Length];

        var prefixIndex = 0;
        for (var i = 1; i < input.Length; i++)
        {
            var k = i;

            //increment prefixIndex (count of matching chars)
            while (k < input.Length
                   && prefixIndex < patternLength
                   && input[prefixIndex] == input[k])
            {
                prefixIndex++;
                k++;
            }

            //assign result
            result[i] = prefixIndex;

            //is prefixIndex > 1 we have a choice to use earlier values 
            //in result
            if (prefixIndex > 1)
            {
                //z-box left and right
                var left = i;
                var right = i + prefixIndex;

                prefixIndex = 1;
                for (var m = left + 1; m < right; m++)
                    if (m + result[prefixIndex] < right)
                    {
                        result[m] = result[prefixIndex];
                        prefixIndex++;
                    }
                    else
                    {
                        //move left end of z box to current element
                        prefixIndex = result[prefixIndex];

                        //cannot exceed size of input
                        if (m + prefixIndex < input.Length)
                        {
                            //increment right end of Z box as far as match goes
                            while (right < input.Length
                                   && prefixIndex < patternLength
                                   && input[prefixIndex] == input[right])
                            {
                                right++;
                                prefixIndex++;
                            }

                            result[m] = prefixIndex;
                            prefixIndex = 1;
                        }
                        else
                        {
                            //since i is assigned with right below
                            //and i is incremented by for loop do a right--
                            right--;
                            break;
                        }
                    }

                //move i to end of z box
                //since i is incremented by for loop do a right - 1
                i = right - 1;
            }

            //reset prefix Index
            prefixIndex = 0;
        }

        return result;
    }
}ParseOptions.0.json¢ 
]D:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Binary\BaseConversion.cs´using System;
using System.Text;

namespace Advanced.Algorithms.Binary;

/// <summary>
///     Base conversion implementation.
/// </summary>
public class BaseConversion
{
    /// <summary>
    ///     Converts base of given number to the target base.
    /// </summary>
    /// <param name="srcNumber">Input number in source base system.</param>
    /// <param name="srcBaseChars">Source base system characters in increasing order. For example 0123456789 for base 10.</param>
    /// <param name="dstBaseChars">Destination base system characters in increasing order. For example 01 for base 2.</param>
    /// <param name="precision">Required precision when dealing with fractions. Defaults to 32 places.</param>
    /// <returns>The result in target base as a string.</returns>
    public static string Convert(string srcNumber,
        string srcBaseChars,
        string dstBaseChars, int precision = 32)
    {
        srcNumber = srcNumber.Trim();
        if (srcNumber.Contains("."))
        {
            var tmp = srcNumber.Split('.');
            var whole = tmp[0].TrimEnd();
            var fraction = tmp[1].TrimStart();

            return ConvertWhole(whole, srcBaseChars, dstBaseChars) +
                   "." + ConvertFraction(fraction, srcBaseChars, dstBaseChars, precision);
        }

        return ConvertWhole(srcNumber, srcBaseChars, dstBaseChars);
    }

    /// <summary>
    ///     Converts the whole part of source number.
    /// </summary>
    private static string ConvertWhole(string srcNumber,
        string srcBaseChars,
        string dstBaseChars)
    {
        if (string.IsNullOrEmpty(srcNumber)) return string.Empty;

        var srcBase = srcBaseChars.Length;
        var dstBase = dstBaseChars.Length;

        if (srcBase <= 1) throw new Exception("Invalid source base length.");

        if (dstBase <= 1) throw new Exception("Invalid destination base length.");

        long base10Result = 0;
        var j = 0;
        //convert to base 10
        //move from least to most significant numbers
        for (var i = srcNumber.Length - 1; i >= 0; i--)
        {
            //eg. 1 * 2^0 
            base10Result += srcBaseChars.IndexOf(srcNumber[i])
                            * (long)Math.Pow(srcBase, j);
            j++;
        }

        var result = new StringBuilder();
        //now convert to target base
        while (base10Result != 0)
        {
            var rem = (int)base10Result % dstBase;
            result.Insert(0, dstBaseChars[rem]);
            base10Result = base10Result / dstBase;
        }

        return result.ToString();
    }

    /// <summary>
    ///     Converts the fractional part of source number.
    /// </summary>
    private static string ConvertFraction(string srcNumber,
        string srcBaseChars,
        string dstBaseChars, int maxPrecision)
    {
        if (string.IsNullOrEmpty(srcNumber)) return string.Empty;

        var srcBase = srcBaseChars.Length;
        var dstBase = dstBaseChars.Length;

        if (srcBase <= 1) throw new Exception("Invalid source base length.");

        if (dstBase <= 1) throw new Exception("Invalid destination base length.");

        decimal base10Result = 0;
        //convert to base 10
        //move from most significant numbers to least
        for (var i = 0; i < srcNumber.Length; i++)
            //eg. 1 * 1/(2^1) 
            base10Result += srcBaseChars.IndexOf(srcNumber[i])
                            * (decimal)(1 / Math.Pow(srcBase, i + 1));

        var result = new StringBuilder();
        //now convert to target base
        while (base10Result != 0 && maxPrecision > 0)
        {
            base10Result = base10Result * dstBase;
            result.Append(dstBaseChars[(int)Math.Floor(base10Result)]);
            base10Result -= Math.Floor(base10Result);
            maxPrecision--;
        }

        return result.ToString();
    }
}ParseOptions.0.json‚
XD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Binary\Logarithm.csnamespace Advanced.Algorithms.Binary;

/// <summary>
///     Logarithm calculator.
/// </summary>
public class Logarithm
{
    public static int CalcBase2LogFloor(int x)
    {
        //make all right most bits after MSB to 1s
        //for example make ..001000 => ..001111
        x = x | (x >> 1);
        x = x | (x >> 2);
        x = x | (x >> 4);
        x = x | (x >> 8);
        x = x | (x >> 16);

        //now log(x) base 2 = count the number of set bits - 1 
        //to find the count do the following steps

        //set bit count of 2 bit groups
        //0x0555.. will be like 010101010101..
        x = (x & 0x55555555) + ((x >> 1) & 0x55555555);

        //set bit count of 4 bit groups
        //0x0333.. will be like 001100110011..
        x = (x & 0x33333333) + ((x >> 2) & 0x33333333);

        //set bit count of 8 bit groups of 4
        //0x0f0f.. will be like 0000111100001111..
        x = (x & 0x0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F);

        //sum up the four groups of 8 bit
        x = (x & 0x000000FF)
            + ((x >> 8) & 0x000000FF)
            + ((x >> 16) & 0x000000FF)
            + ((x >> 24) & 0x000000FF);

        //-1 for log of base 2
        return x - 1;
    }

    public static int CalcBase10LogFloor(int x)
    {
        //using the below relation
        //log(x) base b = (log(x) base a) / (log(b) base a)
        var n = CalcBase2LogFloor(x);
        var d = CalcBase2LogFloor(10);

        return n / d;
    }
}ParseOptions.0.jsonÀ

RD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\Binary\GCD.csﬂ	namespace Advanced.Algorithms.Binary;

/// <summary>
///     GCD without division or mod operators but using substraction.
/// </summary>
public class Gcd
{
    public static int Find(int a, int b)
    {
        if (b == 0) return a;

        if (a == 0) return b;

        //fix negative numbers
        if (a < 0) a = -a;

        if (b < 0) b = -b;

        // p and q even
        if ((a & 1) == 0 && (b & 1) == 0)
            //divide both a and b by 2
            //multiply by 2 the result
            return Find(a >> 1, b >> 1) << 1;

        // a is even, b is odd

        if ((a & 1) == 0)
            //divide a by 2
            return Find(a >> 1, b);

        // a is odd, b is even
        if ((b & 1) == 0)
            //divide by by 2
            return Find(a, b >> 1);
        // a and b odd, a >= b

        if (a >= b)
            //since substracting two odd numbers gives an even number
            //divide (a-b) by 2 to reduce calculations
            return Find((a - b) >> 1, b);
        // a and b odd, a < b

        //since substracting two odd numbers gives an even number
        //divide (b-a) by 2 to reduce calculations
        return Find(a, (b - a) >> 1);
    }
}ParseOptions.0.json¯
çD:\a\advanced-algorithms\advanced-algorithms\src\Advanced.Algorithms\obj\Debug\netstandard2.0\.NETStandard,Version=v2.0.AssemblyAttributes.cs–// <autogenerated />
using System;
using System.Reflection;
[assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETStandard,Version=v2.0", FrameworkDisplayName = ".NET Standard 2.0")]
ParseOptions.0.json