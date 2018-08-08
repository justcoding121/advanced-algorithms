using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A priority queue implementation using min heap,
    /// assuming that lower values have a higher priority.
    /// </summary>
    public class MinPriorityQueue<T> : IEnumerable<T> where T : IComparable
    {
        private readonly BMinHeap<T> minHeap = new BMinHeap<T>();

        /// <summary>
        /// Time complexity:O(log(n)).
        /// </summary>
        public void Enqueue(T item)
        {
            minHeap.Insert(item);
        }

        /// <summary>
        /// Time complexity:O(log(n)).
        /// </summary>
        public T Dequeue()
        {
            return minHeap.ExtractMin();
        }

        /// <summary>
        /// Time complexity:O(1).
        /// </summary>
        public T Peek()
        {
            return minHeap.PeekMin();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return minHeap.GetEnumerator();
        }
    }
}
