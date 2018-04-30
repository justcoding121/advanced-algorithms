namespace Advanced.Algorithms.Search
{

    public class SearchAlmostSorted
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

                if (mid > 0 && input[mid - 1] == element)
                {
                    return mid - 1;
                }

                if (mid < input.Length - 1 && input[mid + 1] == element)
                {
                    return mid + 1;
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
