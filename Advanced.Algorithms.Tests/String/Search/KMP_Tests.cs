using Advanced.Algorithms.String.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Tests.String.Search
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
