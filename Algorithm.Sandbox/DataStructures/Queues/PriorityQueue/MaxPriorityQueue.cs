using System;
using Algorithm.Sandbox.DataStructures.Heap.Max;

namespace Algorithm.Sandbox.DataStructures.Queues.PriorityQueue
{

    /// priority queue implementation using max heap
    /// assumaxg lower values of P have higher priority
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
