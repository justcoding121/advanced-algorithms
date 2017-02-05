using Algorithm.Sandbox.DataStructures;
using System;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class CoinChangeProblems
    {

        //O(amount * n^n) without memoization?
        //O(amount * n) with memoization
        public static int MinCoinChangeRecursive(int amount, int n, int[] coins, AsHashSet<int, int> memoizingCache)
        {
            var key = amount;

            if (memoizingCache.ContainsKey(key))
            {
                return memoizingCache[key];
            }

            int result;

            //no coins to pick from
            if (amount <= 0 || n < 0)
            {
                result = 0;
            }

            var min = int.MaxValue;

            for (int j = 0; j < n; j++)
            {
                //if this coin size is greater than the sum skip it; no use of this coin
                if (coins[j] <= amount)
                {
                    var prevMin = MinCoinChangeRecursive(amount - coins[j], n, coins, memoizingCache) + 1;

                    if (min > prevMin)
                    {
                        min = prevMin;
                    }
                }
            }

            if (min == int.MaxValue)
            {
                min = 0;
            }

            result = min;

            memoizingCache.Add(key, result);

            return result;

        }

    

        //O(amount * n^n) without memoization?
        //O(amount * n) with memoization
        private static int MaxCoinChangeRecursive(int amount, int n, int[] coins, AsHashSet<int, int> memoizingCache)
        {
            var key = amount;

            if (memoizingCache.ContainsKey(key))
            {
                return memoizingCache[key];
            }

            int result;

            //no coins to pick from
            if (amount <= 0 || n < 0)
            {
                result = 0;
            }

            var max = 0;

            for (int j = 0; j < n; j++)
            {
                //if this coin size is greater than the sum skip it; no use of this coin
                if (coins[j] <= amount)
                {
                    var prevMax = MaxCoinChangeRecursive(amount - coins[j], n, coins, memoizingCache) + 1;

                    if (max < prevMax)
                    {
                        max = prevMax;
                    }
                }
            }

            result = max;

            memoizingCache.Add(key, result);

            return result;

        }
    }
}
