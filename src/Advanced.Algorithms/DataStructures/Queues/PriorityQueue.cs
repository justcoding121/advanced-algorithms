using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A priority queue implementation using heap
    /// </summary>
    public class PriorityQueue<T> : IEnumerable<T> where T : IComparable
    {
        private readonly BHeap<T> heap;
        public PriorityQueue(SortDirection sortDirection = SortDirection.Ascending)
        {
            heap = new BHeap<T>(sortDirection);
        }

        /// <summary>
        /// Time complexity:O(log(n)).
        /// </summary>
        public void Enqueue(T item)
        {
            heap.Insert(item);
        }

        /// <summary>
        /// Time complexity:O(log(n)).
        /// </summary>
        public T Dequeue()
        {
            return heap.Extract();
        }

        /// <summary>
        /// Time complexity:O(1).
        /// </summary>
        public T Peek()
        {
            return heap.Peek();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return heap.GetEnumerator();
        }
    }
}
