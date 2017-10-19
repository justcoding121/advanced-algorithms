using System;
using Advanced.Algorithms.DataStructures.Heap.Max;

namespace Advanced.Algorithms.DataStructures.Queues.PriorityQueue
{

    /// priority queue implementation using max heap
    /// assumaxg lower values of P have higher priority
    /// TODO implement IEnumerable
    public class MaxPriorityQueue<T> where T : IComparable
    {
        private BMaxHeap<T> maxHeap = new BMaxHeap<T>();

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
