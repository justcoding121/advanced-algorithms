namespace Algorithm.Sandbox.Search
{
    public class BinarySearch
    {
        public static int Search(int[] input, int element)
        {
            return SearchRecursive(input, 0, input.Length - 1, element);
        }

        private static int SearchRecursive(int[] input, int i, int j, int element)
        {
            if (i == j)
            {
                if (input[i] == element)
                {
                    return i;
                }

                return -1;
            }

            var mid =  (i + j) / 2;

            if (input[mid] == element)
            {
                return mid;
            }

            if (input[mid] > element)
            {
                return SearchRecursive(input, i, mid, element);
            }

            return SearchRecursive(input, mid + 1, j, element);

        }
    }
}
