using Advanced.Algorithms.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Advanced.Algorithms.Tests.Search
{
    [TestClass]
    public class QuickSelect_Tests
    {
        [TestMethod]
        public void QuickSelect_Test()
        {
            var nodeCount = 10000;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToArray();

            var k = rnd.Next(1, nodeCount);

            var expected = k;
            var actual = QuickSelect<int>.FindSmallest(randomNumbers, k);

            Assert.AreEqual(actual, expected);
        }
    }
}
