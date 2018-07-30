using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A sorted HashSet implementation using balanced binary search tree. IEnumerable will enumerate in sorted order.
    /// This may be better than regular HashSet implementation which can give o(K) in worst case (but O(1) amortized when collisions K is avoided).
    /// </summary>
    /// <typeparam name="V">The value datatype.</typeparam>
    public class SortedHashSet<V> : IEnumerable<V> where V : IComparable
    {
        //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
        private readonly RedBlackTree<V> binarySearchTree;

        public int Count => binarySearchTree.Count;

        public SortedHashSet()
        {
            binarySearchTree = new RedBlackTree<V>();
        }

        /// <summary>
        /// Does this hash table contains the given value.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if this hashset contains the given value.</returns>
        public bool Contains(V value)
        {
            return binarySearchTree.HasItem(value);
        }

        /// <summary>
        /// Add a new value.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <param name="value">The value to add.</param>
        public void Add(V value)
        {
            binarySearchTree.Insert(value);
        }

        /// <summary>
        /// Remove the given value.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <param name="value">The value to remove.</param>
        public void Remove(V value)
        {
            binarySearchTree.Delete(value);
        }

        /// <summary>
        /// Return the next higher value after given value in this hashset.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <returns>Null if the given value does'nt exist or next value does'nt exist.</returns>
        public V Next(V value)
        {
            return binarySearchTree.NextHigher(value);
        }

        /// <summary>
        /// Return the next lower value before given value in this HashSet.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <returns>Null if the given value does'nt exist or previous value does'nt exist.</returns>
        public V Previous(V value)
        {
            return binarySearchTree.NextLower(value);
        }

        /// <summary>
        /// Clear the hashtable.
        /// Time complexity: O(1).
        /// </summary>
        internal void Clear()
        {
            binarySearchTree.Clear();
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<V> GetEnumerator()
        {
            return binarySearchTree.GetEnumerator();
        }
    }
}
