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
        public static int[] Sort(int[] array, Order order = Order.Ascending)
        {
            var max = getMax(array);

            //add one more space for zero
            var countArray = new int[max + 1];

            //count the appearances of elements
            foreach (var item in array)
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

            var result = new int[array.Length];

            //now assign result
            foreach (var item in array)
            {
                var index = countArray[item];
                result[order == Order.Ascending ? index-1 : result.Length - index] = item;
                countArray[item]--;
            }

            return result;
        }

        /// <summary>
        /// Get Max of given array.
        /// </summary>
        private static int getMax(int[] array)
        {
            var max = int.MinValue;

            foreach (var item in array)
            {
                if (item.CompareTo(max) > 0)
                {
                    max = item;
                }
            }

            return max;
        }
    }
}
