using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    /// <summary>
    /// A hash table implementation.
    /// </summary>
    /// <typeparam name="T">The value datatype.</typeparam>
    public class HashSet<T> : IEnumerable<T> 
    {
        private readonly IHashSet<T> hashSet;

        /// <param name="type">The hashSet implementation to use.</param>
        /// <param name="initialBucketSize"> The larger the bucket size lesser the collision, but memory matters!</param>
        public HashSet(HashSetType type = HashSetType.SeparateChaining, int initialBucketSize = 2)
        {
            if (initialBucketSize < 2)
            {
                throw new Exception("Bucket Size must be greater than 2.");

            }
            if (type == HashSetType.SeparateChaining)
            {
                hashSet = new SeparateChainingHashSet<T>(initialBucketSize);
            }
            else
            {
                hashSet = new OpenAddressHashSet<T>(initialBucketSize);
            }
        }

        /// <summary>
        /// The number of items in this hashset.
        /// </summary>
        public int Count => hashSet.Count;


        /// <summary>
        /// Does this hash table contains the given value.
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if this hashset contains the given value.</returns>
        public bool Contains(T value)
        {
            return hashSet.Contains(value);
        }

        /// <summary>
        /// Add a new value.
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public void Add(T value)
        {
            hashSet.Add(value);
        }

        /// <summary>
        /// Remove the given value.
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        public void Remove(T value)
        {
            hashSet.Remove(value);
        }

        /// <summary>
        /// Clear the hashtable.
        /// Time complexity: O(1).
        /// </summary>
        public void Clear()
        {
            hashSet.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }
    }

    internal interface IHashSet<T> : IEnumerable<T>
    {
        bool Contains(T value);
        void Add(T value);
        void Remove(T key);
        void Clear();

        int Count { get; }
    }

    /// <summary>
    /// The hash set implementation type.
    /// </summary>
    public enum HashSetType
    {
        SeparateChaining,
        OpenAddressing
    }
}
