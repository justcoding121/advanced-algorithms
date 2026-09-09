using System;
using Advanced.Algorithms.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.String
{
    [TestClass]
    public class RabinKarpTests
    {
        [TestMethod]
        public void String_RabinKarp_Test()
        {
            var algorithm = new RabinKarp();

            var index = algorithm.Search("xabcabzabc", "abc");

            Assert.AreEqual(1, index);

            index = algorithm.Search("abdcdaabxaabxcaabxaabxay", "aabxaabxcaabxaabxay");

            Assert.AreEqual(5, index);

            index = algorithm.Search("aaaabaaaaaaa", "aaaa");

            Assert.AreEqual(0, index);

            index = algorithm.Search("abcabababdefgabcd", "fga");

            Assert.AreEqual(11, index);

            index = algorithm.Search("abxabcabcaby", "abcaby");

            Assert.AreEqual(6, index);

            index = algorithm.Search("abxabcabcaby", "abx");

            Assert.AreEqual(0, index);
        }

        [TestMethod]
        public void String_RabinKarp_No_Match_And_Corners()
        {
            var algorithm = new RabinKarp();

            Assert.AreEqual(-1, algorithm.Search("abcdef", "xyz"));
            Assert.AreEqual(5, algorithm.Search("abcdef", "f"));
            Assert.AreEqual(0, algorithm.Search("a", "a"));
            Assert.AreEqual(-1, algorithm.Search("", "a"));
            Assert.AreEqual(-1, algorithm.Search("a", "ab"));
        }

        [TestMethod]
        public void String_RabinKarp_Adversarial_Vs_IndexOf()
        {
            var algorithm = new RabinKarp();
            var rnd = new Random(13);

            for (var n = 0; n < 500; n++)
            {
                var text = RandomString(rnd, rnd.Next(0, 50), 4);
                var pattern = RandomString(rnd, rnd.Next(1, 12), 4);
                Assert.AreEqual(text.IndexOf(pattern, StringComparison.Ordinal),
                    algorithm.Search(text, pattern),
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
