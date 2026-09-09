using System;
using Advanced.Algorithms.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.String
{
    [TestClass]
    public class ZTests
    {
        [TestMethod]
        public void String_Z_Test()
        {
            var index = ZAlgorithm.Search("xabcabzabc", "abc");

            Assert.AreEqual(1, index);

            index = ZAlgorithm.Search("abdcdaabxaabxcaabxaabxay", "aabxaabxcaabxaabxay");

            Assert.AreEqual(5, index);

            index = ZAlgorithm.Search("aaaabaaaaaaa", "aaaa");

            Assert.AreEqual(0, index);

            index = ZAlgorithm.Search("abcabababdefgabcd", "fga");

            Assert.AreEqual(11, index);

            index = ZAlgorithm.Search("abxabcabcaby", "abcaby");

            Assert.AreEqual(6, index);

            index = ZAlgorithm.Search("abxabcabcaby", "abx");

            Assert.AreEqual(0, index);
        }

        [TestMethod]
        public void String_Z_No_Match_And_Corners()
        {
            Assert.AreEqual(-1, ZAlgorithm.Search("abcdef", "xyz"));
            Assert.AreEqual(5, ZAlgorithm.Search("abcdef", "f"));
            Assert.AreEqual(0, ZAlgorithm.Search("a", "a"));
        }

        [TestMethod]
        public void String_Z_Adversarial_Vs_IndexOf()
        {
            //false positive without pattern/input separator
            Assert.AreEqual(-1, ZAlgorithm.Search("abc", "aa"));
            Assert.AreEqual(-1, ZAlgorithm.Search("ababa", "aa"));

            var rnd = new Random(7);
            for (var n = 0; n < 500; n++)
            {
                var text = RandomString(rnd, rnd.Next(0, 40), 5);
                var pattern = RandomString(rnd, rnd.Next(1, 8), 5);
                Assert.AreEqual(text.IndexOf(pattern, StringComparison.Ordinal),
                    ZAlgorithm.Search(text, pattern),
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
