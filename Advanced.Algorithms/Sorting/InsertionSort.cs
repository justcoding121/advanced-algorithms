using System;

namespace Advanced.Algorithms.Sorting
{
    /// <summary>
    /// An insertion sort implementation.
    /// </summary>
    public class InsertionSort<T> where T : IComparable
    {
        /// <summary>
        /// Time complexity: O(n^2).
        /// </summary>
        public static T[] Sort(T[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (array[j].CompareTo(array[j-1]) < 0)
                    {
                        var temp = array[j-1];
                        array[j-1] = array[j];
                        array[j] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return array;
        }
    }
}
