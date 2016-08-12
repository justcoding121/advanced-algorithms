using System;

namespace Algorithm.Sandbox.Sorting
{
    public class MergeSort<T> where T : IComparable
    {
        //O(nlog(n)) time complexity always
        //O(n) memory
        public static T[] Sort(T[] array)
        {
            MergeSortR(array, 0, array.Length - 1);

            return array;
        }

        private static void MergeSortR(T[] array, int leftIndex, int rightIndex)
        {
            if (leftIndex < 0 || rightIndex < 0 || (rightIndex - leftIndex + 1) < 2)
            {
                return;
            }

            int middle = (leftIndex + rightIndex) / 2;

            MergeSortR(array, leftIndex, middle);
            MergeSortR(array, middle + 1, rightIndex);

            Merge(array, leftIndex, middle, rightIndex);
        }

        /// <summary>
        /// merge two sorted arrays
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private static void Merge(T[] array, int leftStart, int middle, int rightEnd)
        {
            var newLength = rightEnd - leftStart + 1;

            var result = new T[newLength];

            int i = leftStart, j = middle + 1, k = 0;
            //iteratively compare and pick min to result
            while (i <= middle && j <= rightEnd)
            {
                if (array[i].CompareTo(array[j]) < 0)
                {
                    result[k] = array[i];
                    i++;
                }
                else
                {
                    result[k] = array[j];
                    j++;
                }
                k++;
            }

            //copy left overs
            if (i <= middle)
            {
                for (int l = i; l <= middle; l++)
                {
                    result[k] = array[l];
                    k++;
                }
            }
            else
            {
                for (int l = j; l <= rightEnd; l++)
                {
                    result[k] = array[l];
                    k++;
                }
            }

            k = 0;
            //now write back result
            for (int g = leftStart; g <= rightEnd; g++)
            {
                array[g] = result[k];
                k++;
            }

        }
    }
}
