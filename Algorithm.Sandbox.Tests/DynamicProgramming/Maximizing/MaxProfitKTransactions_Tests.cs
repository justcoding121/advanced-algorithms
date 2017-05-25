using Algorithm.Sandbox.DynamicProgramming.Maximizing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Maximizing
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/maximum-profit-by-buying-and-selling-a-share-at-most-k-times/
    /// </summary>
    [TestClass]
    public class MaxProfitKTransactions_Tests
    {
        [TestMethod]
        public void MaxProfit_Smoke_Tests()
        {
           Assert.AreEqual(87,  MaxProfitKTransactions
                .GetProfit(new int[] { 10, 22, 5, 75, 65, 80 }, 2));

            Assert.AreEqual(12, MaxProfitKTransactions
                .GetProfit(new int[] { 12, 14, 17, 10, 14, 13, 12, 15 }, 3));

            Assert.AreEqual(72, MaxProfitKTransactions
                .GetProfit(new int[] { 100, 30, 15, 10, 8, 25, 80 }, 3));

            Assert.AreEqual(0, MaxProfitKTransactions
                .GetProfit(new int[] { 90, 80, 70, 60, 50 }, 1));
        }
    }
}
