using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DataStructures.Set
{
    /// <summary>
    /// A simple bloom filter implementation in C#
    /// A probabilistic data structure as an alternative to HashSet
    /// Keeps track of given keys and returns answer to key exists query with
    /// very low probability of error
    /// </summary>
    public class BloomFilter<T>
    {
        private BitArray filter;

        /// <summary>
        /// Higher the size lower the collision and 
        /// failure probablity
        /// </summary>
        /// <param name="size"></param>
        public BloomFilter(int size)
        {
            filter = new BitArray(size);
        }

        /// <summary>
        /// Run time complexity is O(1)
        /// </summary>
        public void AddKey(T key)
        {
            var hashCode = key.GetHashCode();
       
            //set 8 consecutive bits (a byte)
            for (int i = 0; i < 8; i++)
            {
                var index = Math.Abs(hashCode + i) % filter.Length;
                filter[index] = true;
            }
        }

        /// <summary>
        /// Run time complexity is O(1)
        /// </summary>
        /// <returns></returns>
        public bool KeyExists(T key)
        {
            var hashCode = key.GetHashCode();
           

            //set 8 consecutive bits (a byte)
            for (int i = 0; i < 8; i++)
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
