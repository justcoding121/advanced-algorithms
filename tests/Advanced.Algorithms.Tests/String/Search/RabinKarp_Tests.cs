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
        }
    }
}
