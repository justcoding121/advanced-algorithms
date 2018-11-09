using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Sorting
{
    /// <summary>
    /// A quick sort implementation.
    /// </summary>
    public class QuickSort<T> where T : IComparable
    {
        /// <summary>
        /// Time complexity: O(n^2)
        /// </summary>
        public static T[] Sort(T[] array, SortDirection sortDirection = SortDirection.Ascending)
        {
            if (array.Length <= 1)
            {
                return array;
            }

            var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

            sort(array, 0, array.Length - 1, comparer);

            return array;
        }

        private static void sort(T[] array, int startIndex, int endIndex, CustomComparer<T> comparer)
        {
            while (true)
            {
                //if only one element the do nothing
                if (startIndex < 0 || endIndex < 0 || endIndex - startIndex < 1)
                {
                    return;
                }

                //set the wall to the left most index
                var wall = startIndex;

                //pick last index element on array as comparison pivot
                var pivot = array[endIndex];

                //swap elements greater than pivot to the right side of wall
                //others will be on left
                for (var j = wall; j <= endIndex; j++)
                {
                    if (comparer.Compare(pivot, array[j]) <= 0 && j != endIndex)
                    {
                        continue;
                    }

                    var temp = array[wall];
                    array[wall] = array[j];
                    array[j] = temp;
                    //increment to exclude the minimum element in subsequent comparisons
                    wall++;
                }

                //sort left
                sort(array, startIndex, wall - 2, comparer);
                //sort right
                startIndex = wall;
            }
        }
    }
}
