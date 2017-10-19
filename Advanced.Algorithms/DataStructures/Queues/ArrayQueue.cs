using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Queues
{
    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    internal class ArrayQueue<T> : IQueue<T>
    {
        private List<T> list = new List<T>();

        public int Count { get; private set; }
        //O(1)
        public void Enqueue(T item)
        {
            list.Insert(0, item);
            Count++;
        }
        //O(1)
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

     
    }

}
