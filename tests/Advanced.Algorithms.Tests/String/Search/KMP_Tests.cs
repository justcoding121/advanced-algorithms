using System;
using Advanced.Algorithms.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.String
{
    [TestClass]
    public class KmpTests
    {
        [TestMethod]
        public void String_KMP_Test()
        {
            var index = Kmp.Search("xabcabzabc", "abc");

            Assert.AreEqual(1, index);

            index = Kmp.Search("abdcdaabxaabxcaabxaabxay", "aabxaabxcaabxaabxay");

            Assert.AreEqual(5, index);

            index = Kmp.Search("aaaabaaaaaaa", "aaaa");

            Assert.AreEqual(0, index);

            index = Kmp.Search("abcabababdefgabcd", "fga");

            Assert.AreEqual(11, index);

            index = Kmp.Search("abxabcabcaby", "abcaby");

            Assert.AreEqual(6, index);

            index = Kmp.Search("abxabcabcaby", "abx");

            Assert.AreEqual(0, index);
        }

        [TestMethod]
        public void String_KMP_No_Match_And_Corners()
        {
            Assert.AreEqual(-1, Kmp.Search("abcdef", "xyz"));
            Assert.AreEqual(5, Kmp.Search("abcdef", "f"));
            Assert.AreEqual(0, Kmp.Search("a", "a"));
            Assert.AreEqual(-1, Kmp.Search("a", "ab"));
        }

        [TestMethod]
        public void String_KMP_Adversarial_Vs_IndexOf()
        {
            Assert.AreEqual(-1, Kmp.Search("", "a"));
            Assert.AreEqual(-1, Kmp.Search("abc", "aa"));

            var rnd = new Random(11);
            for (var n = 0; n < 500; n++)
            {
                var text = RandomString(rnd, rnd.Next(0, 40), 5);
                var pattern = RandomString(rnd, rnd.Next(1, 8), 5);
                Assert.AreEqual(text.IndexOf(pattern, StringComparison.Ordinal),
                    Kmp.Search(text, pattern),
                    $"text='{text}' pattern='{pattern}'");
            }
        }

        private static string RandomString(Random rnd, int length, int alphabet)
        {
            var chars = new char[length];
            for (var i = 0; i < length; i++) chars[i] = (char)('a' + rnd.Next(alphabet));
            return new string(chars);
        }
    }
}
