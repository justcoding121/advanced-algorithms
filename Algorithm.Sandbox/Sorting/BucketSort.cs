using Algorithm.Sandbox.DataStructures;
using System;

namespace Algorithm.Sandbox.Sorting
{

    /// <summary>
    /// A bucket sort implementation
    /// </summary>
    public class BucketSort 
    {
        /// <summary>
        /// Sort given integers using bucket sort with merge sort as sub sort
        /// </summary>
        /// <param name="array"></param>
        /// <param name="mst"></param>
        /// <returns></returns>
        public static int[] Sort(int[] array, int bucketSize)
        {
            if (bucketSize < 0 || bucketSize > array.Length)
            {
                throw new Exception("Invalid bucket size.");
            }

            var buckets = new AsDictionary<int, AsArrayList<int>>();

            int i;
            for (i = 0; i < array.Length; i++)
            {
                var bucketIndex = array[i] / bucketSize;

                if(!buckets.ContainsKey(bucketIndex))
                {
                    buckets.Add(bucketIndex, new AsArrayList<int>());
                }

                buckets[bucketIndex].Add(array[i]);
            }

            i = 0;
            var bucketKeys = new int[buckets.Count];
            foreach (var bucket in buckets)
            {
                bucket.Value = new AsArrayList<int>(MergeSort<int>
                    .Sort(bucket.Value.ToArray()));

                bucketKeys[i] = bucket.Key;
                i++;
            }

            bucketKeys = MergeSort<int>.Sort(bucketKeys);

            var result = new int[array.Length];

            i = 0;
            foreach (var bucketKey in bucketKeys)
            {
                var bucket = buckets[bucketKey];
                Array.Copy(bucket.ToArray(), 0, result, i, bucket.Length);
                i += bucket.Length;
            }

            return result;
        }

        /// <summary>
        /// Get Max of given array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static int getMin(int[] array)
        {
            var min = int.MaxValue;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                }
            }

            return min;
        }

        /// <summary>
        /// Get Max of given array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static int getMax(int[] array)
        {
            var max = int.MinValue;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }

            return max;
        }
    }
}
