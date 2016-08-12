using System;

namespace Algorithm.Sandbox.Sorting
{
    public class MergeSort<T> where T : IComparable
    {
        //O(nlog(n)), worst O(n^2)
        public static T[] Sort(T[] array)
        {

            MergeSortR(array, 0, array.Length);

            return array;
        }

        private static void MergeSortR(T[] array, int leftIndex, int rightIndex)
        {
            if (leftIndex < 0 || rightIndex < 0 || (rightIndex - leftIndex) < 1)
            {
                return;
            }

            int middle = (leftIndex + rightIndex) / 2;

            MergeSortR(array, leftIndex, middle);
            MergeSortR(array, middle + 1, rightIndex);

            Merge(array, leftIndex, middle, middle + 1, rightIndex);
        }

        /// <summary>
        /// merge two sorted arrays
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private static void Merge(T[] array, int leftStart, int leftEnd, int rightStart, int rightEnd)
        {

            int i = leftStart, j = rightStart, k = 0;
            while (i > 0 && j > 0)
            {
                if (left[i].CompareTo(right[j]) < 0)
                {
                    result[k] = left[i];
                    i++;
                }
                else
                {
                    result[k] = right[j];
                    j++;
                }
                k++;
            }

            if (i > 0)
            {
                for (int l = i; l < left.Length; l++)
                {
                    result[k] = left[l];
                }
            }
            else
            {
                for (int l = j; l < right.Length; l++)
                {
                    result[k] = right[l];
                }
            }

            return result;
        }
    }
}
