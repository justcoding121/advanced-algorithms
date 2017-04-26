using Algorithm.Sandbox.DataStructures;
using System;

namespace Algorithm.Sandbox.Sorting
{

    /// <summary>
    /// A bucket sort implementation
    /// </summary>
    public class BucketSort<T> where T : IComparable
    {
        /// <summary>
        /// Sort given integers using bucket sort with merge sort as sub sort
        /// </summary>
        /// <param name="array"></param>
        /// <param name="mst"></param>
        /// <returns></returns>
        public static T[] Sort(T[] array, int bucketSize)
        {
            if (bucketSize < 0 || bucketSize > array.Length)
            {
                throw new Exception("Invalid bucket size.");
            }

            var buckets = new AsDictionary<T, T[]>();

            int i;
            for (i = 0; i < array.Length; i = i + bucketSize)
            {
                //for last bucket the size would be remaining elements on array
                var copySize = i + bucketSize < array.Length ? bucketSize 
                                                : array.Length - i;

                var bucket = new T[copySize];
                Array.Copy(array, i, bucket, 0, copySize);
                bucket = MergeSort<T>.Sort(bucket);

                buckets.Add(bucket[0], bucket);
            }

            i = 0;
            var bucketKeys = new T[buckets.Count];
            foreach (var bucket in buckets)
            {
                bucketKeys[i] = bucket.Key;
                i++;
            }

            bucketKeys = MergeSort<T>.Sort(bucketKeys);

            var result = new T[array.Length];

            i = 0;
            foreach (var bucketKey in bucketKeys)
            {
                var bucket = buckets[bucketKey];
                Array.Copy(bucket, 0, result, i, bucket.Length);
                i += bucket.Length;
            }

            return result;
        }
    }
}
