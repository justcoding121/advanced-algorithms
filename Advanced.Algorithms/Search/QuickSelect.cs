using System;

namespace Advanced.Algorithms.Search
{
    public class QuickSelect
    {
        public static int FindSmallest(int[] input, int k)
        {
            var left = 0;
            var right = input.Length - 1;

            var rnd = new Random();

            while (left <= right)
            {
                int pivot = rnd.Next(left, right);
                int newPivot = partition(input, left, right, pivot);

                if (newPivot == k - 1)
                {
                    return input[newPivot];
                }
                else if (newPivot > k - 1)
                {
                    right = newPivot - 1;
                }
                else
                {
                    left = newPivot + 1;
                }
            }

            return -1;
        }

        //partition using pivot
        private static int partition(int[] input, int left, int right, int pivot)
        {
            int pivotValue = input[pivot];
            var newPivot = left;

            //prevent comparing pivot against itself
            swap(input, pivot, right);

            for (int i = left; i < right; i++)
            {
                if (input[i] < pivotValue)
                {
                    swap(input, i, newPivot);
                    newPivot++;
                }
            }

            //move pivot back to middle
            swap(input, newPivot, right);

            return newPivot;
        }

        private static void swap(int[] input, int i, int j)
        {
            if (i != j)
            {
                var tmp = input[i];
                input[i] = input[j];
                input[j] = tmp;
            }

        }
    }
}
