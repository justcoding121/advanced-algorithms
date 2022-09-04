using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    /// <summary>
    /// A sorted Dictionary implementation using balanced binary search tree. IEnumerable will enumerate in sorted order.
    /// This may be better than regular Dictionary implementation which can give o(K) in worst case (but O(1) amortized when collisions K is avoided).
    /// </summary>
    /// <typeparam name="K">The key datatype.</typeparam>
    /// <typeparam name="V">The value datatype.</typeparam>
    public class OrderedDictionary<K, V> : IEnumerable<KeyValuePair<K, V>> where K : IComparable
    {
        //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
        private readonly RedBlackTree<OrderedKeyValuePair<K, V>> binarySearchTree;

        public int Count => binarySearchTree.Count;

        public OrderedDictionary()
        {
            binarySearchTree = new RedBlackTree<OrderedKeyValuePair<K, V>>();
        }

        /// <summary>
        /// Initialize the dictionary with given key value pairs sorted by key.
        /// Time complexity: log(n).
        /// </summary>
        public OrderedDictionary(IEnumerable<KeyValuePair<K, V>> sortedKeyValuePairs)
        {
            binarySearchTree = new RedBlackTree<OrderedKeyValuePair<K, V>>(sortedKeyValuePairs.Select(x => new OrderedKeyValuePair<K, V>(x.Key, x.Value)));
        }

        /// <summary>
        /// Does this dictionary contains the given key.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if this dictionary contains the given key.</returns> 
        public bool ContainsKey(K key)
        {
            return binarySearchTree.HasItem(new OrderedKeyValuePair<K, V>(key, default(V)));
        }

        /// <summary>
        /// Add a new value for given key.
        /// Time complexity: O(log(n)).
        /// Returns the position (index) of the key in sorted order of this OrderedDictionary.
        /// </summary>
        public int Add(K key, V value)
        {
            return binarySearchTree.Insert(new OrderedKeyValuePair<K, V>(key, value));
        }

        /// <summary>
        /// Get/set value for given key.
        /// Time complexity: O(log(n)).
        /// </summary>
        public V this[K key]
        {
            get
            {
                var node = binarySearchTree.FindNode(new OrderedKeyValuePair<K, V>(key, default(V)));
                if (node == null)
                {
                    throw new Exception("Key not found.");
                }

                return node.Value.Value;
            }
            set
            {
                if (ContainsKey(key))
                {
                    Remove(key);
                }

                Add(key, value);
            }
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public KeyValuePair<K, V> ElementAt(int index)
        {
            return binarySearchTree.ElementAt(index).ToKeyValuePair();
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public int IndexOf(K key)
        {
            return binarySearchTree.IndexOf(new OrderedKeyValuePair<K, V>(key, default(V)));
        }

        /// <summary>
        /// Remove the given key if it exists.
        /// Time complexity: O(log(n)).
        /// Returns the position (index) of the removed key if removed. Otherwise returns -1.
        /// </summary>
        public int Remove(K key)
        {
            return binarySearchTree.Delete(new OrderedKeyValuePair<K, V>(key, default(V)));
        }

        /// <summary>
        /// Remove the element at given index.
        /// Time complexity: O(log(n)).
        /// </summary>
        public KeyValuePair<K, V> RemoveAt(int index)
        {
            return binarySearchTree.RemoveAt(index).ToKeyValuePair();
        }

        /// <summary>
        /// Return the next higher key-value pair after given key in this dictionary.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <returns>Null if the given key does'nt exist or next key does'nt exist.</returns>
        public KeyValuePair<K, V> NextHigher(K key)
        {
            var next = binarySearchTree.NextHigher(new OrderedKeyValuePair<K, V>(key, default(V)));

            if (next.Equals(default(OrderedKeyValuePair<K, V>)))
            {
                return default(KeyValuePair<K, V>);
            }

            return new KeyValuePair<K, V>(next.Key, next.Value);
        }

        /// <summary>
        /// Return the next lower key-value pair before given key in this dictionary.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <returns>Null if the given key does'nt exist or previous key does'nt exist.</returns>
        public KeyValuePair<K, V> NextLower(K key)
        {
            var prev = binarySearchTree.NextLower(new OrderedKeyValuePair<K, V>(key, default(V)));

            if (prev.Equals(default(OrderedKeyValuePair<K, V>)))
            {
                return default(KeyValuePair<K, V>);
            }

            return new KeyValuePair<K, V>(prev.Key, prev.Value);
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public KeyValuePair<K, V> Max()
        {
            var max = binarySearchTree.Max();
            return max.Equals(default(OrderedKeyValuePair<K, V>)) ? default(KeyValuePair<K, V>)
                : max.ToKeyValuePair();
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public KeyValuePair<K, V> Min()
        {
            var min = binarySearchTree.Min();
            return min.Equals(default(OrderedKeyValuePair<K, V>)) ? default(KeyValuePair<K, V>)
                : min.ToKeyValuePair();
        }


        /// <summary>
        /// Clear the dictionary.
        /// Time complexity: O(log(n)).
        /// </summary>
        internal void Clear()
        {
            binarySearchTree.Clear();
        }

        /// <summary>
        /// Descending enumerable.
        /// </summary>
        public IEnumerable<KeyValuePair<K, V>> AsEnumerableDesc()
        {
            return GetEnumeratorDesc().AsEnumerable();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return new SortedDictionaryEnumerator<K, V>(binarySearchTree);
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumeratorDesc()
        {
            return new SortedDictionaryEnumerator<K, V>(binarySearchTree, false);
        }
    }

    internal struct OrderedKeyValuePair<K, V> : IComparable
                                 where K : IComparable
    {
        internal K Key { get; }
        internal V Value { get; set; }

        internal OrderedKeyValuePair(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

        public KeyValuePair<K, V> ToKeyValuePair()
        {
            return new KeyValuePair<K, V>(Key, Value);
        }

        public int CompareTo(object obj)
        {
            if (obj is OrderedKeyValuePair<K, V> itemToComare)
            {
                return Key.CompareTo(itemToComare.Key);
            }

            throw new ArgumentException("Compare object is nu");
        }

        public override bool Equals(object obj)
        {
            return Key.Equals(((OrderedKeyValuePair<K, V>)obj).Key);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

    }

    internal class SortedDictionaryEnumerator<K, V> : IEnumerator<KeyValuePair<K, V>> where K : IComparable
    {
        private bool asc;

        private RedBlackTree<OrderedKeyValuePair<K, V>> bst;
        private IEnumerator<OrderedKeyValuePair<K, V>> enumerator;

        internal SortedDictionaryEnumerator(RedBlackTree<OrderedKeyValuePair<K, V>> bst, bool asc = true)
        {

            this.bst = bst;
            this.enumerator = asc ? bst.GetEnumerator() : bst.GetEnumeratorDesc();
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

        public KeyValuePair<K, V> Current
        {
            get
            {
                return new KeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
            }
        }

        public void Dispose()
        {
            bst = null;
            enumerator = null;
        }
    }
}
