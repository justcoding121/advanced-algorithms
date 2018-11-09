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
        public static T[] Sort(T[] array, Order order = Order.Ascending)
        {
            //heapify
            var heap = new BHeap<T>(order, array);

            //now extract min until empty and return them as sorted array
            var sortedArray = new T[array.Length];
            var j = 0;
            while (heap.Count > 0)
            {
                sortedArray[j] = heap.Extract();
                j++;
            }

            return sortedArray;
        }
    }
}
