using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    [TestClass]
    public class ShortestPalindrome_Tests
    {
        [TestMethod]
        public void ShortestPalindrome_Smoke_Tests()
        {
            Assert.AreEqual("aaacecaaa", ShortestPalindrome.FindShortest("aacecaaa"));
        }
    }
}
