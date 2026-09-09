using System;
using Advanced.Algorithms.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.String
{
    [TestClass]
    public class ManacherTests
    {
        [TestMethod]
        public void Manacher_Palindrome_Tests()
        {
            var length = ManachersPalindrome.FindLongestPalindrome("aacecaaa");
            Assert.IsTrue(length == 7);

            length = ManachersPalindrome.FindLongestPalindrome("baab");
            Assert.IsTrue(length == 4);

            length = ManachersPalindrome.FindLongestPalindrome("abaab");
            Assert.IsTrue(length == 4);

            length = ManachersPalindrome.FindLongestPalindrome("abaxabaxabb");
            Assert.IsTrue(length == 9);

            length = ManachersPalindrome.FindLongestPalindrome("abaxabaxabybaxabyb");
            Assert.IsTrue(length == 11);

            length = ManachersPalindrome.FindLongestPalindrome("abaxabaxabbaxabyb");
            Assert.IsTrue(length == 10);
        }

        [TestMethod]
        public void Manacher_Invalid_Input_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() => ManachersPalindrome.FindLongestPalindrome(""));
            Assert.ThrowsException<ArgumentException>(() => ManachersPalindrome.FindLongestPalindrome("a"));
            Assert.ThrowsException<ArgumentException>(() => ManachersPalindrome.FindLongestPalindrome("a$b"));
        }

        [TestMethod]
        public void Manacher_Two_Chars()
        {
            Assert.AreEqual(2, ManachersPalindrome.FindLongestPalindrome("aa"));
            Assert.AreEqual(1, ManachersPalindrome.FindLongestPalindrome("ab"));
        }
    }
}
