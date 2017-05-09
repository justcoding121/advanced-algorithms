using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    [TestClass]
    public class LongestPalindrome_Tests
    {
        [TestMethod]
        public void LongestPalindrome_Smoke_Tests()
        {
            var longestPalindrome = new LongestPalindrome();

            var length = longestPalindrome.FindPalindrome("aacecaaa");
            Assert.IsTrue(length == 7);

            length = longestPalindrome.FindPalindrome("baab");
            Assert.IsTrue(length == 4);

            length = longestPalindrome.FindPalindrome("abaab");
            Assert.IsTrue(length == 4);

            length = longestPalindrome.FindPalindrome("abaxabaxabb");
            Assert.IsTrue(length == 9);

            length = longestPalindrome.FindPalindrome("abaxabaxabybaxabyb");
            Assert.IsTrue(length == 11);

            length = longestPalindrome.FindPalindrome("abaxabaxabbaxabyb");
            Assert.IsTrue(length == 10);
        }
    }
}
