using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A sparse set implementation.
    /// </summary>
    public class SparseSet : IEnumerable<int>
    {
        private readonly int[] sparse;
        private readonly int[] dense;

        public int Count { get; private set; }

        public SparseSet(int maxVal, int capacity)
        {
            sparse = Enumerable.Repeat(-1, maxVal + 1).ToArray(); 
            dense = Enumerable.Repeat(-1, capacity).ToArray();
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public void Add(int value)
        {
            if (value < 0)
            {
                throw new Exception("Negative values not supported.");
            }

            if (value >= sparse.Length)
            {
                throw new Exception("Item is greater than max value.");
            }

            if (Count >= dense.Length)
            {
                throw new Exception("Set reached its capacity.");
            }

            sparse[value] = Count;
            dense[Count] = value;
            Count++;
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public void Remove(int value)
        {
            if (value < 0)
            {
                throw new Exception("Negative values not supported.");
            }

            if (value >= sparse.Length)
            {
                throw new Exception("Item is greater than max value.");
            }

            if (HasItem(value) == false)
            {
                throw new Exception("Item do not exist.");
            }

            //find element
            var index = sparse[value];
            sparse[value] = -1;

            //replace index with last value of dense
            var lastVal = dense[Count - 1];
            dense[index] = lastVal;
            dense[Count - 1] = -1;

            //update sparse for lastVal
            sparse[lastVal] = index;

            Count--;
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public bool HasItem(int value)
        {
            var index = sparse[value];
            return index != -1 && dense[index] == value;
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public void Clear()
        {
            Count = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<int> GetEnumerator()
        {
            return dense.Take(Count).GetEnumerator();
        }
    }
}
