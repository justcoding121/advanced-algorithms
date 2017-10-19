using Advanced.Algorithms.DataStructures;
using System;
using Advanced.Algorithms.DataStructures.Heap.Min;

namespace Advanced.Algorithms.Sorting
{
    public class HeapSort<T> where T : IComparable
    {
        //O(nlog(n))
        public static T[] Sort(T[] array)
        {
            //heapify
            var heap = new BMinHeap<T>();
            for (int i = 0; i < array.Length; i++)
            {
                heap.Insert(array[i]);
            }

            //now extract min until empty and return them as sorted array
            var sortedArray = new T[array.Length];
            int j = 0;
            while (heap.Count > 0)
            {
                sortedArray[j] = heap.ExtractMin();
                j++;
            }

            return sortedArray;
        }
    }
}
