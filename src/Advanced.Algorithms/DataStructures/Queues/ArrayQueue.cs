using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    internal class ArrayQueue<T> : IQueue<T>
    {
        private readonly List<T> list = new List<T>();

        public int Count { get; private set; }

        public void Enqueue(T item)
        {
            list.Insert(0, item);
            Count++;
        }

        public T Dequeue()
        {
            if (list.Count == 0)
            {
                throw new System.Exception("Empty Queue");
            }

            var result = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
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
