using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{

    internal interface AsIHashSetAsHashSet<K, V> : IEnumerable<AsHashSetNode<K, V>> where K : IComparable
    {
        V this[K key] { get; set; }

        bool ContainsKey(K key);
        void Add(K key, V value);
        void Remove(K key);
        void Clear();

        int Count { get;  }
    }
    /// <summary>
    /// key-value set
    /// </summary>
    public class AsHashSetNode<K, V> : IComparable where K : IComparable
    {
        public K Key;
        public V Value;

        public AsHashSetNode(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as AsHashSetNode<K, V>);
        }

        private int CompareTo(AsHashSetNode<K, V> node)
        {
            return Key.CompareTo(node.Key);
        }
    }

    public enum HashSetType
    {
        SeparateChaining,
        OpenAddressing
    }
    /// <summary>
    /// A hash table implementation (key value dictionary) with separate chaining
    /// TODO improve performance by using a Prime number greater than total elements as Bucket Size
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class AsHashSet<K, V> : IEnumerable<AsHashSetNode<K, V>> where K : IComparable
    {
        private AsIHashSetAsHashSet<K, V> hashSet;
        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public AsHashSet(int initialBucketSize = 2, HashSetType type = HashSetType.OpenAddressing)
        {
            if (type == HashSetType.SeparateChaining)
            {
                hashSet = new AsSeparateChainingHashSet<K, V>(initialBucketSize);
            }
            else
            {
                hashSet = new AsOpenAddressHashSet<K, V>(initialBucketSize);
            }
        }

        public int Count => hashSet.Count;

        public V this[K key]
        {
            get { return hashSet[key]; }
            set { hashSet[key] =  value; }

        }

        //O(1) time complexity; worst case O(n)
        public bool ContainsKey(K key)
        {
            return hashSet.ContainsKey(key);
        }

        //O(1) time complexity; worst case O(n)
        //add an item to this hash table
        public void Add(K key, V value)
        {
           hashSet.Add(key, value);
        }

        //O(1) time complexity; worst case O(n)
        public void Remove(K key)
        {
            hashSet.Remove(key);
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

        public IEnumerator<AsHashSetNode<K, V>> GetEnumerator()
        {
            return hashSet.GetEnumerator();
        }

        

    }

   
}
