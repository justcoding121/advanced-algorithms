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

        [TestMethod]
        public void Manacher_Adversarial_Vs_BruteForce()
        {
            Assert.AreEqual(5, ManachersPalindrome.FindLongestPalindrome("ddddbddaca"));
            Assert.AreEqual(5, ManachersPalindrome.FindLongestPalindrome("adddbddc"));
            Assert.AreEqual(5, ManachersPalindrome.FindLongestPalindrome("dddbddbc"));

            var rnd = new Random(42);
            for (var n = 0; n < 400; n++)
            {
                var len = rnd.Next(2, 14);
                var chars = new char[len];
                for (var i = 0; i < len; i++) chars[i] = (char)('a' + rnd.Next(4));
                var s = new string(chars);
                if (s.IndexOf('$') >= 0) continue;

                Assert.AreEqual(BruteLongestPalindrome(s), ManachersPalindrome.FindLongestPalindrome(s), s);
            }
        }

        private static int BruteLongestPalindrome(string s)
        {
            var best = 1;
            for (var i = 0; i < s.Length; i++)
            for (var j = i; j < s.Length; j++)
            {
                var ok = true;
                for (int l = i, r = j; l < r; l++, r--)
                    if (s[l] != s[r])
                    {
                        ok = false;
                        break;
                    }

                if (ok) best = Math.Max(best, j - i + 1);
            }

            return best;
        }
    }
}
