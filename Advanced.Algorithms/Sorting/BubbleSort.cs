using System;

namespace Advanced.Algorithms.Sorting
{
    /// <summary>
    /// A bubble sort implementation.
    /// </summary>
    public class BubbleSort<T> where T : IComparable
    {
        /// <summary>
        /// Time complexity: O(n^2).
        /// </summary>
        public static T[] Sort(T[] array)
        {
            var swapped = true;

            while (swapped)
            {
                swapped = false;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    //compare adjacent elements 
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        var temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }
            }

            return array;
        }
    }
}
