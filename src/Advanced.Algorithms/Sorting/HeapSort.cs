
/* Unmerged change from project 'Advanced.Algorithms (netstandard1.0)'
Before:
using System;
After:
using Advanced.Algorithms.DataStructures;
using System;
*/
using Advanced.Algorithms.DataStructures;

/* Unmerged change from project 'Advanced.Algorithms (netstandard1.0)'
Before:
using System.Linq;
using Advanced.Algorithms.DataStructures;
After:
using System.Linq;
*/
using System;
using System.Collections.Generic;

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
        public static T[] Sort(ICollection<T> collection, SortDirection sortDirection = SortDirection.Ascending)
        {
            //heapify
            var heap = new BHeap<T>(sortDirection, collection);

            //now extract min until empty and return them as sorted array
            var sortedArray = new T[collection.Count];
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
