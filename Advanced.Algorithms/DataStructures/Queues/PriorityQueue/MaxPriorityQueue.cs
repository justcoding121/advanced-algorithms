using System;

namespace Advanced.Algorithms.DataStructures
{

    /// priority queue implementation using max heap
    /// assumaxg lower values of P have higher priority
    /// TODO implement IEnumerable
    public class MaxPriorityQueue<T> where T : IComparable
    {
        private readonly BMaxHeap<T> maxHeap = new BMaxHeap<T>();

        public void Enqueue(T queueItem)
        {
            maxHeap.Insert(queueItem);
        }

        public T Dequeue()
        {
            return maxHeap.ExtractMax();
        }
    }
}
