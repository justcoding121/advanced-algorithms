using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Sorting
{
    /// <summary>
    /// A shell sort implementation.
    /// </summary>
    public class ShellSort<T> where T : IComparable
    {
        public static T[] Sort(T[] array, SortDirection sortDirection = SortDirection.Ascending)
        {
            var comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);

            var k = array.Length / 2;
            var j = 0;

            while (k >= 1)
            {
                for (int i = k; i < array.Length; i = i + k, j = j + k)
                {
                    if (comparer.Compare(array[i], array[j]) >= 0)
                    {
                        continue;
                    }

                    swap(array, i, j);

                    if (i <= k)
                    {
                        continue;
                    }

                    i -= k * 2;
                    j -= k * 2;
                }

                j = 0;
                k /= 2;
            }

            return array;
        }

        private static void swap(T[] array, int i, int j)
        {
            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }
}
