using System;
using System.Collections;

namespace Advanced.Algorithms.DataStructures
{    
    /// <summary>
    /// A simple bloom filter implementation.
    /// </summary>
    public class BloomFilter<T>
    {
        private readonly BitArray filter;

        /// <summary>
        /// Higher the size lower the collision and 
        /// failure probablity.
        /// </summary>
        public BloomFilter(int size)
        {
            filter = new BitArray(size);
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public void AddKey(T key)
        {
            var hashCode = key.GetHashCode();
       
            //set 8 consecutive bits (a byte)
            for (var i = 0; i < 8; i++)
            {
                var index = Math.Abs(hashCode + i) % filter.Length;
                filter[index] = true;
            }
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public bool KeyExists(T key)
        {
            var hashCode = key.GetHashCode();
           

            //set 8 consecutive bits (a byte)
            for (var i = 0; i < 8; i++)
            {
                var index = Math.Abs(hashCode + i) % filter.Length;

                if (filter[index]== false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
