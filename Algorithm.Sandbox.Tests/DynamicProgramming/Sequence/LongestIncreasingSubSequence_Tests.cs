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
                LongestIncreasingSubSequence
                .FindSequence(new int[] { 85, 27, 14, 38, 26, 55, 46, 65, 85 }));

            Assert.AreEqual(6,
                LongestIncreasingSubSequence.FindSequence(new int[] { 10, 22, 9, 33, 21, 50, 41, 60, 80 }));
        }
    }
}
