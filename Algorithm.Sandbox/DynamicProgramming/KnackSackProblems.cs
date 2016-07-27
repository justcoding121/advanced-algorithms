using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class KnackSackProblems
    {
        //1-0 knacksack
        public static void KnackSack10()
        {
            //sample inputs
            int[] weights = new int[] { 2, 3, 4, 5 };
            int[] values = new int[] { 3, 7, 2, 9 };

            //max weight capacity of bag
            int W = 5;

            var result = KnackSack_10_Recursive(W, weights, values, weights.Length, new Dictionary<int, int>());

            Console.WriteLine(result);
        }

        //costs O(2^n) without recursion
        //costs O(W*n) in total after all recursion is complete
        private static int KnackSack_10_Recursive(int W, int[] weights, int[] values, int n, Dictionary<int, int> memozingCache)
        {
            int result;
            var i = n - 1;

            if(memozingCache.ContainsKey(W))
            {
                return memozingCache[W];
            }

            if (i < 0)
            {
                result = 0;
            }
            //skip this weight; its even greater than the maximum Weight W
            else if (weights[i] > W)
            {
                //skip this weight
                result = KnackSack_10_Recursive(W, weights, values, n - 1, memozingCache);
            }
            else
            {
                //compute maximum value for n-1 objects
                var prev = KnackSack_10_Recursive(W - weights[i], weights, values, n - 1, memozingCache);

                //pick maximum of adding this object and possibility of skipping this object
                result = Math.Max(prev + values[i],
                    KnackSack_10_Recursive(W, weights, values, n - 1, memozingCache));
            }

            memozingCache[W] = result;

            return result;
        }

        //greedy solution for fractional variant
        public static void KnackSack_Fractional()
        {
            //sample inputs
            int[] weights = new int[] { 5, 20, 10, 12 };
            int[] values = new int[] { 50, 140, 60, 60 };

            //max weight capacity of bag
            int W = 30;

            //compute ratios to find importance of weights
            var ratios = new double[weights.Length];
            //O(n)
            for (int i = 0; i < weights.Length; i++)
            {
                ratios[i] = values[i] / weights[i];
            }
            //(o(nlogn)
            //quick sort by desc weights
            for (int i = 0; i < weights.Length; i++)
            {
                var pivot = i;

                for (int j = pivot + 1; j < weights.Length; j++)
                {
                    if (ratios[i] < ratios[j])
                    {
                        ratios[i] = ratios[j];
                        weights[i] = weights[j];
                        values[i] = values[j];
                    }
                }
            }

            double resultValue = 0;

            //O(n)
            int k = 0;
            //fill in the bag
            while (true)
            {
                var balanceWeight = W - weights[k];

                if (balanceWeight >= 0)
                {
                    W -= weights[k];
                    resultValue += values[k];
                }
                else
                {
                    //remaining is fractional so spilt and insert (increment the value as well)
                    resultValue += (W / (double)weights[k]) * values[k];
                    break;
                }

                k++;
            }

            //O(n) + O(nlogn) + O(n) = O(nlogn)
            Console.WriteLine(resultValue);
        }
    }
}
