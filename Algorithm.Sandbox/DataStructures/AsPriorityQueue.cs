using System;

namespace Algorithm.Sandbox.DataStructures
{

    /// priority queue implementation using min heap
    /// assuming lower values of P have higher priority
    public class AsPriorityQueue<T> where T : IComparable
    {
        private AsBMinHeap<T> minHeap = new AsBMinHeap<T>();

        public void Enqueue(T queueItem)
        {
            minHeap.Insert(queueItem);
        }

        public T Dequeue()
        {
            return minHeap.ExtractMin();
        }
    }
}
