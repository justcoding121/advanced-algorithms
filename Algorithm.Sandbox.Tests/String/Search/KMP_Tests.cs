using Algorithm.Sandbox.String.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.String.Search
{
    [TestClass]
    public class KMP_Tests
    {
        [TestMethod]
        public void String_KMP_Test()
        {
            var kmpAlgo = new KMP();

            var index = kmpAlgo.Search("abdcdabcaabcdabcag", "abcdabca");

            Assert.AreEqual(9, index);

            index = kmpAlgo.Search("abcabababdefgabcd", "fgg");

            Assert.AreEqual(-1, index);

            index = kmpAlgo.Search("abxabcabcaby", "abcaby");

            Assert.AreEqual(6, index);
        }
    }
}
