using Microsoft.VisualStudio.TestTools.UnitTesting;
using Advanced.Algorithms.String;

namespace Advanced.Algorithms.Tests.String
{
    [TestClass]
    public class KMP_Tests
    {
        [TestMethod]
        public void String_KMP_Test()
        {
            var algorithm = new KMP();

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
    }
}
