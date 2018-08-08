using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    /// <summary>
    /// A queue implementation.
    /// </summary>
    public class Queue<T> : IEnumerable<T>
    {
        private readonly IQueue<T> queue;

        /// <summary>
        /// The number of items in the queue.
        /// </summary>
        public int Count => queue.Count;

        /// <param name="type">The queue implementation type.</param>
        public Queue(QueueType type = QueueType.Array)
        {
            if (type == QueueType.Array)
            {
                queue = new ArrayQueue<T>();
            }
            else
            {
                queue = new LinkedListQueue<T>();
            }
        }

        /// <summary>
        /// Time complexity:O(1).
        /// </summary>
        public void Enqueue(T item)
        {
            queue.Enqueue(item);
        }

        /// <summary>
        /// Time complexity:O(1).
        /// </summary>
        public T Dequeue()
        {
            return queue.Dequeue();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queue.GetEnumerator();
        }
    }

    internal interface IQueue<T> : IEnumerable<T>
    {
        int Count { get; }
        void Enqueue(T item);
        T Dequeue();
    }

    /// <summary>
    /// The queue implementation types.
    /// </summary>
    public enum QueueType
    {
        Array = 0,
        LinkedList = 1
    }

}
