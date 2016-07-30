using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsStack<T>
    {
        private AsSinglyLinkedList<T> list = new AsSinglyLinkedList<T>();

        public T Pop()
        {
            if(list.Head == null)
            {
                throw new Exception("Empty stack");
            }

            return list.DeleteFirst();
        }

        public void Push(T item)
        {
            list.InsertFirst(item);
        }

        public T Peek()
        {
            if(list.Head == null)
            {
                return default(T);
            }

            return list.Head.Data;
        }

        public int Count()
        {
            return list.Count();
        }
    }
}
