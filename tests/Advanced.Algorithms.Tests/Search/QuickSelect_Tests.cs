using System;
using System.Linq;
using Advanced.Algorithms.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Search
{
    [TestClass]
    public class QuickSelectTests
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

        [TestMethod]
        public void QuickSelect_Min_And_Max()
        {
            var input = new[] { 9, 1, 5, 3, 7 };

            Assert.AreEqual(1, QuickSelect<int>.FindSmallest(input, 1));
            Assert.AreEqual(9, QuickSelect<int>.FindSmallest(input, 5));
        }

        [TestMethod]
        public void QuickSelect_Single_Element()
        {
            Assert.AreEqual(4, QuickSelect<int>.FindSmallest(new[] { 4 }, 1));
        }

        [TestMethod]
        public void QuickSelect_With_Duplicates()
        {
            var input = new[] { 3, 1, 3, 2, 3 };

            Assert.AreEqual(1, QuickSelect<int>.FindSmallest(input, 1));
            Assert.AreEqual(3, QuickSelect<int>.FindSmallest(input, 5));
        }
    }
}
