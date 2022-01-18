using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A simple bloom filter implementation.
    /// </summary>
    public class BloomFilter<T>
    {
        private readonly BitArray filter;

        private readonly int numberOfHashFunctions;

        /// <summary>
        /// Higher the size lower the collision and 
        /// failure probablity.
        /// </summary>
        public BloomFilter(int size, int numberOfHashFunctions = 2)
        {
            if (size <= numberOfHashFunctions)
            {
                throw new ArgumentException("size cannot be less than or equal to numberOfHashFunctions.");
            }

            this.numberOfHashFunctions = numberOfHashFunctions;
            filter = new BitArray(size);
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public void AddKey(T key)
        {
            foreach (var hash in getHashes(key))
            {
                filter[hash % filter.Length] = true;
            }
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public bool KeyExists(T key)
        {
            foreach (var hash in getHashes(key))
            {
                if (filter[hash % filter.Length] == false)
                {
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<int> getHashes(T key)
        {
            for (var i = 1; i <= numberOfHashFunctions; i++)
            {
                var obj = new { Key = key, InitialValue = i };
                yield return Math.Abs(obj.GetHashCode());
            }
        }
    }
}
