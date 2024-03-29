﻿using Advanced.Algorithms.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.String
{
    [TestClass]
    public class ManacherTests
    {
        [TestMethod]
        public void Manacher_Palindrome_Tests()
        {
            var manacher = new ManachersPalindrome();

            var length = manacher.FindLongestPalindrome("aacecaaa");
            Assert.IsTrue(length == 7);

            length = manacher.FindLongestPalindrome("baab");
            Assert.IsTrue(length == 4);

            length = manacher.FindLongestPalindrome("abaab");
            Assert.IsTrue(length == 4);

            length = manacher.FindLongestPalindrome("abaxabaxabb");
            Assert.IsTrue(length == 9);

            length = manacher.FindLongestPalindrome("abaxabaxabybaxabyb");
            Assert.IsTrue(length == 11);

            length = manacher.FindLongestPalindrome("abaxabaxabbaxabyb");
            Assert.IsTrue(length == 10);
        }
    }
}