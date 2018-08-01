using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    internal class LinkedListQueue<T> : IQueue<T>
    {
        private readonly DoublyLinkedList<T> list = new DoublyLinkedList<T>();

        public int Count { get; private set; }

        public void Enqueue(T item)
        {
            list.InsertFirst(item);
            Count++;
        }

        public T Dequeue()
        {
            if (list.Head == null)
            {
                throw new System.Exception("Empty Queue");
            }

            var result = list.DeleteLast();
            Count--;
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
