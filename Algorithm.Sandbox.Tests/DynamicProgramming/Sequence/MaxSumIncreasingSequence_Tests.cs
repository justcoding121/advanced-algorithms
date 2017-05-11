using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-14-maximum-sum-increasing-subsequence/
    /// </summary>
    [TestClass]
    public class MaxIncreasingSumSequence_Tests
    {
        [TestMethod]
        public void LongestIncreasingSubSequence_Smoke_Test()
        {
            Assert.AreEqual(106, MaxSumIncreasingSequence
                .FindSum(new int[] { 1, 101, 2, 3, 100, 4, 5 }));
                

        }
    }
}
