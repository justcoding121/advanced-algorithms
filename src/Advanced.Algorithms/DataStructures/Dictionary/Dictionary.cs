using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    /// <summary>
    /// A dictionary implementation.
    /// </summary>
    /// <typeparam name="K">The key datatype.</typeparam>
    /// <typeparam name="V">The value datatype.</typeparam>
    public class Dictionary<K, V> : IEnumerable<KeyValuePair<K, V>> 
    {
        private readonly IDictionary<K, V> dictionary;

        /// <param name="type">The dictionary implementation to use.</param>
        /// <param name="initialBucketSize">The larger the bucket size lesser the collision, but memory matters!</param>
        public Dictionary(DictionaryType type = DictionaryType.SeparateChaining, int initialBucketSize = 2)
        {
            if (initialBucketSize < 2)
            {
                throw new Exception("Bucket Size must be greater than 2.");
            }

            if (type == DictionaryType.SeparateChaining)
            {
                dictionary = new SeparateChainingDictionary<K, V>(initialBucketSize);
            }
            else
            {
                dictionary = new OpenAddressDictionary<K, V>(initialBucketSize);
            }
        }

        /// <summary>
        /// The number of items in this hashset.
        /// </summary>
        public int Count => dictionary.Count;

        /// <summary>
        /// Get/set value for given key.
        /// Time complexity: O(1) amortized.
        /// </summary>
        public V this[K key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        /// <summary>
        /// Does this dictionary contains the given key.
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="value">The key to check.</param>
        /// <returns>True if this dictionary contains the given key.</returns>
        public bool ContainsKey(K key)
        {
            return dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Add a new key for given value.
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value for the given key.</param>
        public void Add(K key, V value)
        {
            dictionary.Add(key, value);
        }

        /// <summary>
        /// Remove the given key along with its value.
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        public void Remove(K key)
        {
            dictionary.Remove(key);
        }

        /// <summary>
        /// Clear the dictionary.
        /// Time complexity: O(1).
        /// </summary>
        public void Clear()
        {
            dictionary.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }
    }

    internal interface IDictionary<K, V> : IEnumerable<KeyValuePair<K, V>>
    {
        V this[K key] { get; set; }

        bool ContainsKey(K key);
        void Add(K key, V value);
        void Remove(K key);
        void Clear();

        int Count { get; }
    }

    /// <summary>
    /// The dictionary implementation type.
    /// </summary>
    public enum DictionaryType
    {
        SeparateChaining,
        OpenAddressing
    }
}
