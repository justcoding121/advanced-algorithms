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
    public class BloomFilter
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
        public void AddKey()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Run time complexity is O(1)
        /// </summary>
        /// <returns></returns>
        public bool KeyExists()
        {
            throw new NotImplementedException();
        }

       
    }
}
