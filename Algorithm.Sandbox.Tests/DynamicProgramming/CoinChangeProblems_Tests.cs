using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Sandbox.DynamicProgramming;
using Algorithm.Sandbox.DataStructures;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    [TestClass]
    public class CoinChangeProblems_Tests
    {
        /// <summary>
        /// Gets the minimum number of coins to fit in the amount 
        /// </summary>
        [TestMethod]
        public void MinCoinChange_Test()
        {
            int[] coins = { 25, 10, 5 };
            int amount = 30;

            var result = CoinChangeProblems.MinCoinChangeRecursive(amount, coins.Length, coins, new AsHashSet<int, int>());

            Assert.AreEqual(result, 2);
        }


        /// <summary>
        /// Gets the maximum number of coins to fit in the amount 
        /// </summary>
        [TestMethod]
        public  void MaxCoinChange_Test()
        {
            int[] coins = { 1, 2, 3 };
            int amount = 29;

            var result = CoinChangeProblems.MinCoinChangeRecursive(amount, coins.Length, coins, new AsHashSet<int, int>());

            Assert.AreEqual(result, 10);
        }
    }
}
