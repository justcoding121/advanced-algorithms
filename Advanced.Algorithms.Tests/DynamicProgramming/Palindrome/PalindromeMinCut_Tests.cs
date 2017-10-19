using Advanced.Algorithms.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-17-palindrome-partitioning/
    /// </summary>
    [TestClass]
    public class PalindromeMinCut_Tests
    {
        [TestMethod]
        public void PalindromeMinCut_Smoke_Tests()
        {
            var minPalindromePartitions
                = PalindromeMinCut.GetMinCut("ababbbabbababa");

            Assert.AreEqual(4, minPalindromePartitions.Count);
        }
    }
}
