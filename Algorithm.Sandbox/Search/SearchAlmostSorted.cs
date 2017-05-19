using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Search
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/search-almost-sorted-array/
    /// </summary>
    public class SearchAlmostSorted
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
                return SearchRecursive(input, i, mid, element);
            }

            return SearchRecursive(input, mid + 1, j, element);

        }
    }
}
