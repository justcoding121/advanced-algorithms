using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    /// <summary>
    /// A sorted HashSet implementation using balanced binary search tree. IEnumerable will enumerate in sorted order.
    /// This may be better than regular HashSet implementation which can give o(K) in worst case (but O(1) amortized when collisions K is avoided).
    /// </summary>
    /// <typeparam name="T">The value datatype.</typeparam>
    public class OrderedHashSet<T> : IEnumerable<T> where T : IComparable
    {
        //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
        private readonly RedBlackTree<T> binarySearchTree;

        public int Count => binarySearchTree.Count;

        public OrderedHashSet()
        {
            binarySearchTree = new RedBlackTree<T>();
        }

        /// <summary>
        /// Initialize the sorted hashset with given sorted key collection.
        /// Time complexity: log(n).
        /// </summary>
        public OrderedHashSet(IEnumerable<T> sortedKeys)
        {
            binarySearchTree = new RedBlackTree<T>(sortedKeys);
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public T this[int index]
        {
            get => ElementAt(index);

        }

        /// <summary>
        /// Does this hash table contains the given value.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if this hashset contains the given value.</returns>
        public bool Contains(T value)
        {
            return binarySearchTree.HasItem(value);
        }

        /// <summary>
        /// Add a new key.
        /// Time complexity: O(log(n)).
        /// Returns the position (index) of the key in sorted order of this OrderedHashSet.
        /// </summary>
        public int Add(T key)
        {
            return binarySearchTree.Insert(key);
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public T ElementAt(int index)
        {
            return binarySearchTree.ElementAt(index);
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public int IndexOf(T key)
        {
            return binarySearchTree.IndexOf(key);
        }

        /// <summary>
        /// Remove the given key if present.
        /// Time complexity: O(log(n)).
        /// Returns the position (index) of the removed key if removed. Otherwise returns -1.
        /// </summary>
        public int Remove(T key)
        {
           return binarySearchTree.Delete(key);
        }

        /// <summary>
        /// Remove the element at given index.
        /// Time complexity: O(log(n)).
        /// </summary>
        public T RemoveAt(int index)
        {
           return binarySearchTree.RemoveAt(index);
        }

        /// <summary>
        /// Return the next higher value after given value in this hashset.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <returns>Null if the given value does'nt exist or next value does'nt exist.</returns>
        public T NextHigher(T value)
        {
            return binarySearchTree.NextHigher(value);
        }

        /// <summary>
        /// Return the next lower value before given value in this HashSet.
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <returns>Null if the given value does'nt exist or previous value does'nt exist.</returns>
        public T NextLower(T value)
        {
            return binarySearchTree.NextLower(value);
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public T Max()
        {
            return binarySearchTree.Max();
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public T Min()
        {
            return binarySearchTree.Min();
        }

        /// <summary>
        /// Clear the hashtable.
        /// Time complexity: O(1).
        /// </summary>
        internal void Clear()
        {
            binarySearchTree.Clear();
        }

        /// <summary>
        /// Descending enumerable.
        /// </summary>
        public IEnumerable<T> AsEnumerableDesc()
        {
            return GetEnumeratorDesc().AsEnumerable();
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return binarySearchTree.GetEnumerator();
        }

        public IEnumerator<T> GetEnumeratorDesc()
        {
            return binarySearchTree.GetEnumeratorDesc();
        }
    }
}
