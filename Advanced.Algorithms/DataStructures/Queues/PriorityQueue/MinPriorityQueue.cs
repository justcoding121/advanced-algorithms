using System;
using Advanced.Algorithms.DataStructures.Heap.Min;

namespace Advanced.Algorithms.DataStructures.Queues.PriorityQueue
{
    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    /// priority queue implementation using min heap
    /// assuming lower values of P have higher priority
    public class MinPriorityQueue<T> where T : IComparable
    {
        private BMinHeap<T> minHeap = new BMinHeap<T>();

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
