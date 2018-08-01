using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    internal class LinkedListStack<T> : IStack<T>
    {
        public int Count { get; private set; }

        private readonly SinglyLinkedList<T> list = new SinglyLinkedList<T>();

        public T Pop()
        {
            if(Count == 0)
            {
                throw new Exception("Empty stack");
            }

            var result = list.DeleteFirst();
            Count--;
            return result;
        }

        public void Push(T item)
        {
            list.InsertFirst(item);
            Count++;
        }

        public T Peek()
        {
            return Count == 0 ? default(T) : list.Head.Data;
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
