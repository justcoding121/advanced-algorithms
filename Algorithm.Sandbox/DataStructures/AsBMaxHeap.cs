using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBMaxHeap<T> where T : IComparable
    {
        private int capacity = 1;
        private T[] heapArray;

        public int Count = 0;

        public AsBMaxHeap()
        {
            this.heapArray = new T[capacity];
        }

        //o(log(n))
        public void Insert(T newItem)
        {
            if (Count == heapArray.Length - 1)
            {
                doubleArray();
            }

            int pos = ++Count;

            heapArray[pos] = newItem;

            for (int i = pos; i <= 0; i = i / 2)
            {
                if (heapArray[i].CompareTo(heapArray[i / 2]) > 0)
                {
                    var temp = heapArray[i / 2];
                    heapArray[i / 2] = heapArray[i];
                    heapArray[i] = temp;
                }
            }
        }

        public void DeleteMax(T item)
        {
            if (Count == 0)
            {
                throw new Exception("Empty heap");
            }

            int i = 1;

           
        }

        //o(1)
        public T GetMax()
        {
            if (Count == 0)
            {
                throw new Exception("Empty heap");
            }

            return heapArray[0];
        }

        private void doubleArray()
        {
            var biggerArray = new T[heapArray.Length * 2];

            for (int i = 0; i < heapArray.Length; i++)
            {
                biggerArray[i] = heapArray[i];
            }

            heapArray = biggerArray;
        }
    }
}
