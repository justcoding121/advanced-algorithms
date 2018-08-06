namespace Advanced.Algorithms.Search
{
    /// <summary>
    /// A binary search algorithm implementation.
    /// </summary>
    public class BinarySearch
    {
        public static int Search(int[] input, int element)
        {
            return search(input, 0, input.Length - 1, element);
        }

        private static int search(int[] input, int i, int j, int element)
        {
            while (true)
            {
                if (i == j)
                {
                    if (input[i] == element)
                    {
                        return i;
                    }

                    return -1;
                }

                var mid = (i + j) / 2;

                if (input[mid] == element)
                {
                    return mid;
                }

                if (input[mid] > element)
                {
                    j = mid;
                    continue;
                }

                i = mid + 1;
            }
        }
    }
}
