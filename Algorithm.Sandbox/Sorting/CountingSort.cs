namespace Algorithm.Sandbox.Sorting
{
    /// <summary>
    /// A counting sort implementation
    /// </summary>
    public class CountingSort
    {
        /// <summary>
        /// Sort given integers
        /// </summary>
        /// <param name="array"></param>
        /// <param name="mst"></param>
        /// <returns></returns>
        public static int[] Sort(int[] array)
        {
            var max = getMax(array);

            //add one more space for zero
            var countArray = new int[max + 1];

            //count the appearances of elements
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    throw new System.Exception("Negative numbers not supported.");
                }

                countArray[array[i]]++;
            }

            //now aggregate & assign the sum from left to right
            var sum = countArray[0];
            for (int i = 1; i <= max; i++)
            {
                sum += countArray[i];
                countArray[i] = sum;
            }

            var result = new int[array.Length];

            //now assign result
            for (int i = 0; i < array.Length; i++)
            {
                var index = countArray[array[i]];
                result[index-1] = array[i];
                countArray[array[i]]--;
            }

            return result;
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
