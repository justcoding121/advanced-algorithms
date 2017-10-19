using Advanced.Algorithms.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// https://leetcode.com/problems/shortest-palindrome/#/description
    /// </summary>
    [TestClass]
    public class ShortestPalindrome_Tests
    {
        [TestMethod]
        public void ShortestPalindrome_Smoke_Tests()
        {
            Assert.AreEqual("aaacecaaa", ShortestPalindrome.FindShortest("aacecaaa"));
            Assert.AreEqual("dcbabcd", ShortestPalindrome.FindShortest("abcd"));
        }
    }
}
