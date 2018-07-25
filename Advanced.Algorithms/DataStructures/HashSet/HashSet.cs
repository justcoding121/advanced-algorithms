using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A hash table implementation.
    /// </summary>
    /// <typeparam name="TV">The value datatype.</typeparam>
    public class HashSet<TV> : IEnumerable<HashSetNode<TV>> 
    {
        private readonly IHashSet<TV> hashSet;

        /// <summary>
        /// Constructor.
        /// </summary>
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
                hashSet = new SeparateChainingHashSet<TV>(initialBucketSize);
            }
            else
            {
                hashSet = new OpenAddressHashSet<TV>(initialBucketSize);
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
        public bool Contains(TV value)
        {
            return hashSet.Contains(value);
        }

        /// <summary>
        /// Add a new value.
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public void Add(TV value)
        {
            hashSet.Add(value);
        }

        /// <summary>
        /// Remove the given value.
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        public void Remove(TV value)
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

        public IEnumerator<HashSetNode<TV>> GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }
    }

    internal interface IHashSet<TV> : IEnumerable<HashSetNode<TV>>
    {
        bool Contains(TV value);
        void Add(TV value);
        void Remove(TV key);
        void Clear();

        int Count { get; }
    }

    public class HashSetNode<TV>
    {
        public TV Value;

        public HashSetNode(TV value)
        {
            this.Value = value;
        }
    }

    public enum HashSetType
    {
        SeparateChaining,
        OpenAddressing
    }
}
