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
            var kmpAlgo = new KMP();

            var index = kmpAlgo.Search("xabcabzabc", "abc");

            Assert.AreEqual(1, index);

            index = kmpAlgo.Search("abdcdaabxaabxcaabxaabxay", "aabxaabxcaabxaabxay");

            Assert.AreEqual(5, index);

            index = kmpAlgo.Search("aaaabaaaaaaa", "aaaa");

            Assert.AreEqual(0, index);

            index = kmpAlgo.Search("abcabababdefgabcd", "fga");

            Assert.AreEqual(11, index);

            index = kmpAlgo.Search("abxabcabcaby", "abcaby");

            Assert.AreEqual(6, index);

            index = kmpAlgo.Search("abxabcabcaby", "abx");

            Assert.AreEqual(0, index);
        }
    }
}
