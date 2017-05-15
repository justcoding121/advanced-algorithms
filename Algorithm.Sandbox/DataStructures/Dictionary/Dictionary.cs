using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{

    internal interface AsIDictionary<K, V> : IEnumerable<DictionaryNode<K, V>>
    {
        V this[K key] { get; set; }

        bool ContainsKey(K key);
        void Add(K key, V value);
        void Remove(K key);
        void Clear();

        int Count { get; }
    }
    /// <summary>
    /// key-value set
    /// </summary>
    public class DictionaryNode<K, V> 
    {
        public K Key;
        public V Value;

        public DictionaryNode(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

    }

    public enum DictionaryType
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
    public class AsDictionary<K, V> : IEnumerable<DictionaryNode<K, V>> 
    {
        private AsIDictionary<K, V> Dictionary;
        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public AsDictionary(DictionaryType type = DictionaryType.SeparateChaining, int initialBucketSize = 2)
        {
            if (initialBucketSize < 2)
            {
                throw new Exception("Bucket Size must be greater than 2.");

            }
            if (type == DictionaryType.SeparateChaining)
            {
                Dictionary = new SeparateChainingDictionary<K, V>(initialBucketSize);
            }
            else
            {
                Dictionary = new OpenAddressDictionary<K, V>(initialBucketSize);
            }
        }

        public int Count => Dictionary.Count;

        public V this[K key]
        {
            get { return Dictionary[key]; }
            set { Dictionary[key] = value; }

        }

        //O(1) time complexity; worst case O(n)
        public bool ContainsKey(K key)
        {
            return Dictionary.ContainsKey(key);
        }

        //O(1) time complexity; worst case O(n)
        //add an item to this hash table
        public void Add(K key, V value)
        {
            Dictionary.Add(key, value);
        }

        //O(1) time complexity; worst case O(n)
        public void Remove(K key)
        {
            Dictionary.Remove(key);
        }

        /// <summary>
        /// clear hash table
        /// </summary>
        public void Clear()
        {
            Dictionary.Clear();
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        public IEnumerator<DictionaryNode<K, V>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

    }

}
