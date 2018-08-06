using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    /// <summary>
    /// A sorted Dictionary implementation using balanced binary search tree. IEnumerable will enumerate in sorted order.
    /// This may be better than regular Dictionary implementation which can give o(K) in worst case (but O(1) amortized when collisions K is avoided).
    /// </summary>
    /// <typeparam name="K">The key datatype.</typeparam>
    /// <typeparam name="V">The value datatype.</typeparam>
    public class SortedDictionary<K, V> : IEnumerable<KeyValuePair<K, V>> where K : IComparable
    {
        //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
        private readonly RedBlackTree<SortedDictionaryNode<K, V>> binarySearchTree;

        public int Count => binarySearchTree.Count;

        public SortedDictionary()
        {
            binarySearchTree = new RedBlackTree<SortedDictionaryNode<K, V>>();
        }

        /// <summary>
        /// Does this dictionary contains the given key.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if this dictionary contains the given key.</returns> 
        public bool ContainsKey(K key)
        {
            return binarySearchTree.HasItem(new SortedDictionaryNode<K, V>(key, default(V)));
        }

        /// <summary>
        /// Add a new value for given key.
        /// Time complexity: O(log(n)).
        /// </summary>
        public void Add(K key, V value)
        {
            binarySearchTree.Insert(new SortedDictionaryNode<K, V>(key, value));
        }

        /// <summary>
        /// Get/set value for given key.
        /// Time complexity: O(log(n)).
        /// </summary>
        public V this[K key]
        {
            get
            {
                var node = binarySearchTree.FindNode(new SortedDictionaryNode<K, V>(key, default(V)));
                if (node == null)
                {
                    throw new Exception("Key not found.");
                }

                return node.Value.Value;
            }
            set
            {
                if(ContainsKey(key))
                {
                    Remove(key);
                }
              
                Add(key, value);
            }
        }

        /// <summary>
        /// Remove the given key.
        /// Time complexity: O(log(n)).
        /// </summary>
        public void Remove(K key)
        {
            binarySearchTree.Delete(new SortedDictionaryNode<K, V>(key, default(V)));
        }

        /// <summary>
        /// Return the next higher key-value pair after given key in this dictionary.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <returns>Null if the given key does'nt exist or next key does'nt exist.</returns>
        public KeyValuePair<K, V> NextHigher(K key)
        {
            var next = binarySearchTree.NextHigher(new SortedDictionaryNode<K, V>(key, default(V)));

            if(next == null)
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
            var prev = binarySearchTree.NextLower(new SortedDictionaryNode<K, V>(key, default(V)));

            if (prev == null)
            {
                return default(KeyValuePair<K, V>);
            }

            return new KeyValuePair<K, V>(prev.Key, prev.Value);
        }

        /// <summary>
        /// Clear the dictionary.
        /// Time complexity: O(1).
        /// </summary>
        internal void Clear()
        {
            binarySearchTree.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return new SortedDictionaryEnumerator<K, V>(binarySearchTree);
        }
    }

    internal class SortedDictionaryNode<K, V> : IComparable
                                 where K : IComparable
    {
        internal K Key { get; }
        internal V Value { get; set; }

        internal SortedDictionaryNode(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            if (obj is SortedDictionaryNode<K, V> itemToComare)
            {
                return Key.CompareTo(itemToComare.Key);
            }

            throw new ArgumentException();
        }
    }

    internal class SortedDictionaryEnumerator<K, V> : IEnumerator<KeyValuePair<K, V>> where K : IComparable
    {
        private RedBlackTree<SortedDictionaryNode<K, V>> bst;
        private IEnumerator<SortedDictionaryNode<K, V>> enumerator;

        internal SortedDictionaryEnumerator(RedBlackTree<SortedDictionaryNode<K, V>> bst)
        {
            this.bst = bst;
            this.enumerator = bst.GetEnumerator();
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
