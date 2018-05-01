using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{

    internal interface IHashSet<TV> : IEnumerable<HashSetNode<TV>>
    {
        bool Contains(TV value);
        void Add(TV value);
        void Remove(TV key);
        void Clear();

        int Count { get; }
    }
    /// <summary>
    /// key-value set
    /// </summary>
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
    /// <summary>
    /// A hash table implementation (key value HashSet) with separate chaining
    /// TODO improve performance by using a Prime number greater than total elements as Bucket Size
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public class HashSet<TV> : IEnumerable<HashSetNode<TV>> 
    {
        private readonly IHashSet<TV> hashSet;
        //init with an expected size (the larger the size lesser the collission, but memory matters!)
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

        public int Count => hashSet.Count;


        //O(1) time complexity; worst case O(n)
        public bool Contains(TV value)
        {
            return hashSet.Contains(value);
        }

        //O(1) time complexity; worst case O(n)
        //add an item to this hash table
        public void Add(TV value)
        {
            hashSet.Add(value);
        }

        //O(1) time complexity; worst case O(n)
        public void Remove(TV value)
        {
            hashSet.Remove(value);
        }

        /// <summary>
        /// clear hash table
        /// </summary>
        public void Clear()
        {
            hashSet.Clear();
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }

        public IEnumerator<HashSetNode<TV>> GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }

    }

}
