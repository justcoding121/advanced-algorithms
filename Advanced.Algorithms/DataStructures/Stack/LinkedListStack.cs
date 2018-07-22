using System;

namespace Advanced.Algorithms.DataStructures
{
    internal class LinkedListStack<T> : IStack<T>
    {
        public int Count { get; private set; }

        private readonly SinglyLinkedList<T> list = new SinglyLinkedList<T>();

        //O(1)
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

        //O(1)
        public void Push(T item)
        {
            list.InsertFirst(item);
            Count++;
        }

        //O(1)
        public T Peek()
        {
            return Count == 0 ? default(T) : list.Head.Data;
        }
     
    }
}
