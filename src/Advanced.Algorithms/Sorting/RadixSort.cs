using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Sorting
{
    /// <summary>
    /// A radix sort implementation.
    /// </summary>
    public class RadixSort
    {
        public static int[] Sort(int[] array, Order order = Order.Ascending)
        {
            int i;
            for (i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    throw new System.Exception("Negative numbers not supported.");
                }
            }

            var @base = 1;
            var max = getMax(array);


            while (max / @base > 0)
            {
                //create a bucket for digits 0 to 9
                var buckets = new List<int>[10];

                for (i = 0; i < array.Length; i++)
                {
                    var bucketIndex = array[i] / @base % 10;

                    if (buckets[bucketIndex] == null)
                    {
                        buckets[bucketIndex] = new List<int>();
                    }

                    buckets[bucketIndex].Add(array[i]);
                }

                //now update array with what is in buckets
                var orderedBuckets = order == Order.Ascending ?
                                        buckets : buckets.Reverse();

                i = 0;
                foreach (var bucket in orderedBuckets.Where(x => x != null))
                {
                    foreach (var item in bucket)
                    {
                        array[i] = item;
                        i++;
                    }
                }

                @base *= 10;
            }

            return array;
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
