using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsStack<T> 
    {
        public int Count { get; private set; }

        private AsSinglyLinkedList<T> list = new AsSinglyLinkedList<T>();

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
