using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algorithm.Sandbox.DynamicProgramming
{
    [TestClass]
    public class LongestCommonSubSequence_Tests
    {
        [TestMethod]
        public void Smoke_Test()
        {
            Assert.AreEqual("ADH", LongestCommonSubSequence.FindSequence("ABCDGH", "AEDFHR"));
            Assert.AreEqual("GTAB", LongestCommonSubSequence.FindSequence("AGGTAB", "GXTXAYB"));
        }
    }
}
