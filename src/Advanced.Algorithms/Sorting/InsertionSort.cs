using System;
using System.Collections.Generic;

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
            
            for (int i = 1; i < array.Length; i++)
            {
                var key=array[i];
                var flag=0;
                for (int j = i - 1; j >= 0&&flag!=1;)
                {
                    if (key<array[j])
                    {
                         array[j + 1] = array[j];
                         j--;
                         array[j + 1] = key;
                    }
                    else
                    {
                       flag=1;
                    }
                }
            }

            return array;
        }
    }
}
