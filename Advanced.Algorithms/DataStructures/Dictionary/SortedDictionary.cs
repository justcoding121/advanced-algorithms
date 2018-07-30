using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A sorted Dictionary implementation using balanced binary search tree. IEnumerable will enumerate in sorted order.
    /// This may be better than regular Dictionary implementation which can give o(K) in worst case (but O(1) amortized when collisions K is avoided).
    /// </summary>
    /// <typeparam name="TK">The key datatype.</typeparam>
    /// <typeparam name="TV">The value datatype.</typeparam>
    public class SortedDictionary<TK, TV> : IEnumerable<KeyValuePair<TK, TV>> where TK : IComparable
    {
        //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
        private readonly RedBlackTree<SortedDictionaryNode<TK, TV>> binarySearchTree;

        public int Count => binarySearchTree.Count;

        public SortedDictionary()
        {
            binarySearchTree = new RedBlackTree<SortedDictionaryNode<TK, TV>>();
        }

        /// <summary>
        /// Does this dictionary contains the given key.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if this dictionary contains the given key.</returns> 
        public bool ContainsKey(TK key)
        {
            return binarySearchTree.HasItem(new SortedDictionaryNode<TK, TV>(key, default(TV)));
        }

        /// <summary>
        /// Add a new value for given key.
        /// Time complexity: O(log(n)).
        /// </summary>
        public void Add(TK key, TV value)
        {
            binarySearchTree.Insert(new SortedDictionaryNode<TK, TV>(key, value));
        }

        /// <summary>
        /// Get/set value for given key.
        /// Time complexity: O(log(n)).
        /// </summary>
        public TV this[TK key]
        {
            get
            {
                var node = binarySearchTree.FindNode(new SortedDictionaryNode<TK, TV>(key, default(TV)));
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
        public void Remove(TK key)
        {
            binarySearchTree.Delete(new SortedDictionaryNode<TK, TV>(key, default(TV)));
        }

        /// <summary>
        /// Return the next higher key-value pair after given key in this dictionary.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <returns>Null if the given key does'nt exist or next key does'nt exist.</returns>
        public KeyValuePair<TK, TV> NextHigher(TK key)
        {
            var next = binarySearchTree.NextHigher(new SortedDictionaryNode<TK, TV>(key, default(TV)));

            if(next == null)
            {
                return default(KeyValuePair<TK, TV>);
            }

            return new KeyValuePair<TK, TV>(next.Key, next.Value);
        }

        /// <summary>
        /// Return the next lower key-value pair before given key in this dictionary.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <returns>Null if the given key does'nt exist or previous key does'nt exist.</returns>
        public KeyValuePair<TK, TV> NextLower(TK key)
        {
            var prev = binarySearchTree.NextLower(new SortedDictionaryNode<TK, TV>(key, default(TV)));

            if (prev == null)
            {
                return default(KeyValuePair<TK, TV>);
            }

            return new KeyValuePair<TK, TV>(prev.Key, prev.Value);
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

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            return new SortedDictionaryEnumerator<TK, TV>(binarySearchTree);
        }
    }

    internal class SortedDictionaryNode<TK, TV> : IComparable
                                 where TK : IComparable
    {
        internal TK Key { get; }
        internal TV Value { get; set; }

        internal SortedDictionaryNode(TK key, TV value)
        {
            this.Key = key;
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            if (obj is SortedDictionaryNode<TK, TV> itemToComare)
            {
                return Key.CompareTo(itemToComare.Key);
            }

            throw new ArgumentException();
        }
    }

    internal class SortedDictionaryEnumerator<TK, TV> : IEnumerator<KeyValuePair<TK, TV>> where TK : IComparable
    {
        private RedBlackTree<SortedDictionaryNode<TK, TV>> bst;
        private IEnumerator<SortedDictionaryNode<TK, TV>> enumerator;

        internal SortedDictionaryEnumerator(RedBlackTree<SortedDictionaryNode<TK, TV>> bst)
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

        public KeyValuePair<TK, TV> Current
        {
            get
            {
                return new KeyValuePair<TK, TV>(enumerator.Current.Key, enumerator.Current.Value);
            }
        }

        public void Dispose()
        {
            bst = null;
            enumerator = null;
        }
    }
}
