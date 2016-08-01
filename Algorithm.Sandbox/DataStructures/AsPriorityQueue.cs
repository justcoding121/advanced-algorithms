using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsPriorityQueueItem<P,V> where P : IComparable
    {
        public P Priority { get; set; }
        public V Value { get; set; }
    }
    /// <summary>
    /// priority queue implementation using min heap
    /// assuming lower values of P have higher priority
    /// </summary>
    /// <typeparam name="P"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class AsPriorityQueue<P, V> where P : IComparable
    {
        private AsBMinHeap<P, V> minHeap = new AsBMinHeap<P, V>();

        public void Enqueue(AsPriorityQueueItem<P, V> queueItem)
        {
            throw new NotImplementedException();
        }

        public AsPriorityQueueItem<P, V> Dequeue()
        {
            throw new NotImplementedException();
        }
    }
}
