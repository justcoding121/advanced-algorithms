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

            var index = manacher.FindLongestPalindrome("abaxabaxabb");

            Assert.IsTrue(index == 5);

            index = manacher.FindLongestPalindrome("abaxabaxabybaxabyb");

            Assert.IsTrue(index == 10);
        }
    }
}
