using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Sequence
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-15-longest-bitonic-subsequence/
    /// </summary>
    [TestClass]
    public class LongestBitonicSequence_Tests
    {
        [TestMethod]
        public void LongestBitonicSequence_Smoke_Test()
        {
            Assert.AreEqual(6, LongestBitonicSubSequence
                .FindSequence(new int[] { 1, 11, 2, 10, 4, 5, 2, 1 }));

            Assert.AreEqual(5, LongestBitonicSubSequence
                .FindSequence(new int[] { 12, 11, 40, 5, 3, 1 }));

            Assert.AreEqual(5, LongestBitonicSubSequence
                .FindSequence(new int[] { 80, 60, 30, 40, 20, 10 }));

            Assert.AreEqual(7, LongestBitonicSubSequence
                .FindSequence(new int[] {0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5,
              13, 3, 11, 7, 15}));
        }
    }
}
