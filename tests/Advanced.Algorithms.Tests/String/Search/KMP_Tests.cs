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
    }
}
