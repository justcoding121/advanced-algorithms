using Algorithm.Sandbox.DataStructures;
using System;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class KnackSackProblems
    {
       
        //costs O(2^n) without memoizing
        //costs O(W*n) in total after all recursion is complete
        public static int KnackSack_10_Recursive(int W, int[] weights,
            int[] values, 
            int n, AsDictionary<string, int> memozingCache)
        {
            var cacheKey = W + string.Empty + n;

            if (memozingCache.ContainsKey(cacheKey))
            {
                return memozingCache[cacheKey];
            }

            int result;
            var i = n - 1;
          
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

                //pick maximum value of adding this object and possibility of skipping this object
                result = Math.Max(prev + values[i],
                    KnackSack_10_Recursive(W, weights, values, n - 1, memozingCache));
            }

            memozingCache.Add(cacheKey, result);

            return result;
        }

        //greedy solution for fractional variant
        public static double KnackSack_Fractional(int W, int[] weights, int[] values)
        {

            //compute ratios to find importance of weights
            var ratios = new double[weights.Length];
            //O(n)
            for (int i = 0; i < weights.Length; i++)
            {
                ratios[i] = values[i] / weights[i];
            }
            //(o(nlog(n)
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

            //O(n) + O(nlog(n)) + O(n) = O(nlog(n))
            return resultValue;
        }
    }
}
