using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-4-longest-common-subsequence/
    /// </summary>
    [TestClass]
    public class LongestCommonSubSequence_Tests
    {
        [TestMethod]
        public void LongestCommonSubSequence_Smoke_Test()
        {
            Assert.AreEqual("ADH", LongestCommonSubSequence.FindSequence("ABCDGH", "AEDFHR"));
            Assert.AreEqual("GTAB", LongestCommonSubSequence.FindSequence("AGGTAB", "GXTXAYB"));
        }
    }
}
