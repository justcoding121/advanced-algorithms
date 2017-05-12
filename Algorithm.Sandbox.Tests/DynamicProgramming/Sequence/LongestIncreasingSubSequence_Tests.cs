using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-3-longest-increasing-subsequence/
    /// </summary>
    [TestClass]
    public class LongestIncreasingSubSequence_Tests
    {
        [TestMethod]
        public void LongestIncreasingSubSequence_Smoke_Test()
        {
            Assert.AreEqual(6, 
                LongestIncreasingSubSequence.FindSequence(new int[] { 15, 27, 14, 38, 26, 55, 46, 65, 85 }));

            Assert.AreEqual(4,
                LongestIncreasingSubSequence.FindSequence(new int[] { 50, 3, 10, 7, 40, 80 }));

            Assert.AreEqual(1,
              LongestIncreasingSubSequence.FindSequence(new int[] { 3, 2 }));

            Assert.AreEqual(2,
              LongestIncreasingSubSequence.FindSequence(new int[] { 2, 3 }));

            Assert.AreEqual(6,
                LongestIncreasingSubSequence.FindSequence(new int[] { 10, 22, 9, 33, 21, 50, 41, 60, 80 }));
        }
    }
}
