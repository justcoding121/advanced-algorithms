using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    internal class ArrayStack<T> : IStack<T>
    {
        public int Count { get; private set; }

        private List<T> list = new List<T>();

        //O(1)
        public T Pop()
        {
            if(Count == 0)
            {
                throw new Exception("Empty stack");
            }

            var result = list[list.Count - 1];
            list.RemoveAt(list.Count-1);
            Count--;
            return result;
        }

        //O(1)
        public void Push(T item)
        {
            list.Add(item);
            Count++;
        }

        //O(1)
        public T Peek()
        {
            if(Count == 0)
            {
                return default(T);
            }

            return list[list.Count - 1];
        }

      
    }
}
