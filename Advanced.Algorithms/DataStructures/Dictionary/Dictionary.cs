using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{

    internal interface IDictionary<TK, TV> : IEnumerable<DictionaryNode<TK, TV>>
    {
        TV this[TK key] { get; set; }

        bool ContainsKey(TK key);
        void Add(TK key, TV value);
        void Remove(TK key);
        void Clear();

        int Count { get; }
    }
    /// <summary>
    /// key-value set
    /// </summary>
    public class DictionaryNode<TK, TV> 
    {
        public TK Key;
        public TV Value;

        public DictionaryNode(TK key, TV value)
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
    /// <typeparam name="TK"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public class Dictionary<TK, TV> : IEnumerable<DictionaryNode<TK, TV>> 
    {
        private readonly IDictionary<TK, TV> dictionary;
        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public Dictionary(DictionaryType type = DictionaryType.SeparateChaining, int initialBucketSize = 2)
        {
            if (initialBucketSize < 2)
            {
                throw new Exception("Bucket Size must be greater than 2.");

            }
            if (type == DictionaryType.SeparateChaining)
            {
                dictionary = new SeparateChainingDictionary<TK, TV>(initialBucketSize);
            }
            else
            {
                dictionary = new OpenAddressDictionary<TK, TV>(initialBucketSize);
            }
        }

        public int Count => dictionary.Count;

        public TV this[TK key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        //O(1) time complexity; worst case O(n)
        public bool ContainsKey(TK key)
        {
            return dictionary.ContainsKey(key);
        }

        //O(1) time complexity; worst case O(n)
        //add an item to this hash table
        public void Add(TK key, TV value)
        {
            dictionary.Add(key, value);
        }

        //O(1) time complexity; worst case O(n)
        public void Remove(TK key)
        {
            dictionary.Remove(key);
        }

        /// <summary>
        /// clear hash table
        /// </summary>
        public void Clear()
        {
            dictionary.Clear();
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public IEnumerator<DictionaryNode<TK, TV>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }
    }

}
