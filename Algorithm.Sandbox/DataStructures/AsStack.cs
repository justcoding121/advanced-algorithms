using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsStack<T>
    {
        private AsSinglyLinkedList<T> list = new AsSinglyLinkedList<T>();

        //O(1)
        public T Pop()
        {
            if(list.Head == null)
            {
                throw new Exception("Empty stack");
            }

            return list.DeleteFirst();
        }

        //O(1)
        public void Push(T item)
        {
            list.InsertFirst(item);
        }

        //O(1)
        public T Peek()
        {
            if(list.Head == null)
            {
                return default(T);
            }

            return list.Head.Data;
        }

        //O(n)
        public int Count()
        {
            return list.Count();
        }
    }
}
