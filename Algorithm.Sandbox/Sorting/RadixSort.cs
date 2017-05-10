using System.Collections.Generic;

namespace Algorithm.Sandbox.Sorting
{
    /// <summary>
    /// A radix sort implementation
    /// </summary>
    public class RadixSort
    {
        /// <summary>
        /// Sort given integers
        /// </summary>
        /// <param name="array"></param>
        /// <param name="mst"></param>
        /// <returns></returns>
        public static int[] Sort(int[] array)
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
            int max = getMax(array);

           
            while (max/@base > 0)
            {
                //create a bucket for digits 0 to 9
                var buckets = new List<int>[10];

                for (i = 0; i < array.Length; i++)
                {
                    var bucketIndex = (array[i]/@base) % 10;

                    if(buckets[bucketIndex] == null)
                    {
                        buckets[bucketIndex] = new List<int>();
                    }

                    buckets[bucketIndex].Add(array[i]);
                }

                //now update array with what is in buckets
                i = 0;
                foreach (var bucket in buckets)
                {
                    if (bucket != null)
                    {
                        for (int j = 0; j < bucket.Count; j++)
                        {
                            array[i] = bucket[j];
                            i++;
                        }
                    }
                }

                @base *= 10;
            }

            return array;
        }

        /// <summary>
        /// Get Max of given array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static int getMax(int[] array)
        {
            var max = int.MinValue;

            for(int i =0;i<array.Length;i++)
            {
                if(array[i] > max)
                {
                    max = array[i];
                }
            }

            return max;
        }
    }
}
