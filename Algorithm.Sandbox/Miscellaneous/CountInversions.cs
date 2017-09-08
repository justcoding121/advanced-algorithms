using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Miscellaneous
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/counting-inversions/
    /// </summary>
    public class CountInversions
    {
        public static int Count(int[] arr)
        {
            return MergeCount(arr, 0, arr.Length - 1);
        }

        private static int MergeCount(int[] arr, int left, int right)
        {
            if (right <= left)
            {
                return 0;
            }

            var mid = (left + right) / 2;

            var leftHalfInversionCount = MergeCount(arr, left, mid);
            var rightHalfInversionCount = MergeCount(arr, mid + 1, right);

            var inversionCount = leftHalfInversionCount + rightHalfInversionCount;

            var mergeResult = new List<int>();

            var i = left;
            var j = mid + 1;

            while (i <= mid && j <= right)
            {
                if (arr[i] < arr[j])
                {
                    mergeResult.Add(arr[i++]);
                }
                else
                {
                    mergeResult.Add(arr[j++]);

                    //current element + remaining elements on left half
                    //all of them will be greater than the current element on right
                    //so we would need inversion
                    inversionCount += 1 + (mid - i);
                }
            }

            var mergedCount = mergeResult.Count;
            while (i <= mid)
            {
                mergeResult.Add(arr[i++]);
            }

            while (j <= right)
            {
                mergeResult.Add(arr[j++]);
            }

            var l = 0;
            for (int k = left; k <= right; k++)
            {
                arr[k] = mergeResult[l++];
            }

            return inversionCount;

        }
    }
}
