using Algorithm.Sandbox.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.String
{
    [TestClass]
    public class Manacher_Tests
    {
        [TestMethod]
        public void Manacher_Palindrome_Tests()
        {
            var manacher = new ManachersPalindrome();

            var length = manacher.FindLongestPalindrome("aba");
            Assert.IsTrue(length == 3);

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
