using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// https://www.hackerrank.com/challenges/maxsubarray
    /// </summary>
    [TestClass]
    public class MaxSumSequence_Tests
    {
        [TestMethod]
        public void LongestIncreasingSubSequence_Smoke_Test()
        {
            Assert.AreEqual(10, MaxSumSequence
                .FindMaxSumOfNonContiguousSequence(new int[] { 1, 2, 3, 4 }));

            Assert.AreEqual(10, MaxSumSequence
               .FindMaxSumOfContiguousSequence(new int[] { 1, 2, 3, 4 }));


            Assert.AreEqual(11, MaxSumSequence
           .FindMaxSumOfNonContiguousSequence(new int[] { 2, -1, 2, 3, 4, -5 }));

            Assert.AreEqual(10, MaxSumSequence
               .FindMaxSumOfContiguousSequence(new int[] { 2, -1, 2, 3, 4, -5 }));

        }
    }
}
