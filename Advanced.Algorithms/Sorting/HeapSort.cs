using System;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Sorting
{
    /// <summary>
    /// A heap sort implementation.
    /// </summary>
    public class HeapSort<T> where T : IComparable
    {
        /// <summary>
        /// Time complexity: O(nlog(n)).
        /// </summary>
        public static T[] Sort(T[] array)
        {
            //heapify
            var heap = new BMinHeap<T>();
            foreach (var item in array)
            {
                heap.Insert(item);
            }

            //now extract min until empty and return them as sorted array
            var sortedArray = new T[array.Length];
            var j = 0;
            while (heap.Count > 0)
            {
                sortedArray[j] = heap.ExtractMin();
                j++;
            }

            return sortedArray;
        }
    }
}
