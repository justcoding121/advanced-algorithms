using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsQueue<T> 
    {
        private AsDoublyLinkedList<T> list = new AsDoublyLinkedList<T>();

        public int Count { get; private set; }
        //O(1)
        public void Enqueue(T item)
        {
            list.InsertFirst(item);
            Count++;
        }
        //O(1)
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

     
    }

}
