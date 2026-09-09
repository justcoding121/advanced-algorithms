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
    }
}
