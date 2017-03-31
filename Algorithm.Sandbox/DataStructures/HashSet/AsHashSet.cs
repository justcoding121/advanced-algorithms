using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{

    internal interface AsIHashSet<V> : IEnumerable<AsHashSetNode<V>>
    {
        bool Contains(V value);
        void Add(V value);
        void Remove(V key);
        void Clear();

        int Count { get; }
    }
    /// <summary>
    /// key-value set
    /// </summary>
    public class AsHashSetNode<V> 
    {
        public V Value;

        public AsHashSetNode(V value)
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
    /// <typeparam name="V"></typeparam>
    public class AsHashSet<V> : IEnumerable<AsHashSetNode<V>> 
    {
        private AsIHashSet<V> HashSet;
        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public AsHashSet(HashSetType type = HashSetType.SeparateChaining, int initialBucketSize = 2)
        {
            if (initialBucketSize < 2)
            {
                throw new Exception("Bucket Size must be greater than 2.");

            }
            if (type == HashSetType.SeparateChaining)
            {
                HashSet = new AsSeparateChainingHashSet<V>(initialBucketSize);
            }
            else
            {
                HashSet = new AsOpenAddressHashSet<V>(initialBucketSize);
            }
        }

        public int Count => HashSet.Count;


        //O(1) time complexity; worst case O(n)
        public bool Contains(V value)
        {
            return HashSet.Contains(value);
        }

        //O(1) time complexity; worst case O(n)
        //add an item to this hash table
        public void Add(V value)
        {
            HashSet.Add(value);
        }

        //O(1) time complexity; worst case O(n)
        public void Remove(V value)
        {
            HashSet.Remove(value);
        }

        /// <summary>
        /// clear hash table
        /// </summary>
        public void Clear()
        {
            HashSet.Clear();
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return HashSet.GetEnumerator();
        }

        public IEnumerator<AsHashSetNode<V>> GetEnumerator()
        {
            return HashSet.GetEnumerator();
        }

    }

}
