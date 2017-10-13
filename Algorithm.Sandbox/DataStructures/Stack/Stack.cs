using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsStack<T> 
    {
        public int Count { get; private set; }

        private SinglyLinkedList<T> list = new SinglyLinkedList<T>();

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
            if(Count == 0)
            {
                return default(T);
            }

            return list.Head.Data;
        }

      
    }
}
