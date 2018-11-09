using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Sorting
{
    /// <summary>
    /// A counting sort implementation.
    /// </summary>
    public class CountingSort
    {
        /// <summary>
        /// Sort given integers.
        /// </summary>
        public static int[] Sort(IEnumerable<int> enumerable, SortDirection sortDirection = SortDirection.Ascending)
        {
            var lengthAndMax = getLengthAndMax(enumerable);

            var length = lengthAndMax.Item1;
            var max = lengthAndMax.Item2;

            //add one more space for zero
            var countArray = new int[max + 1];

            //count the appearances of elements
            foreach (var item in enumerable)
            {
                if (item < 0)
                {
                    throw new System.Exception("Negative numbers not supported.");
                }

                countArray[item]++;
            }

            //now aggregate and assign the sum from left to right
            var sum = countArray[0];
            for (var i = 1; i <= max; i++)
            {
                sum += countArray[i];
                countArray[i] = sum;
            }

            var result = new int[length];

            //now assign result
            foreach (var item in enumerable)
            {
                var index = countArray[item];
                result[sortDirection == SortDirection.Ascending ? index-1 : result.Length - index] = item;
                countArray[item]--;
            }

            return result;
        }

        /// <summary>
        /// Get Max of given array.
        /// </summary>
        private static Tuple<int, int> getLengthAndMax(IEnumerable<int> array)
        {
            var length = 0;
            var max = int.MinValue;
            foreach (var item in array)
            {
                length++;
                if (item.CompareTo(max) > 0)
                {
                    max = item;
                }
            }

            return new Tuple<int, int>(length, max);
        }
    }
}
