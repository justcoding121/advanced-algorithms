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

        [TestMethod]
        public void QuickSelect_Oracle_Against_Sorted_K()
        {
            var rnd = new Random(7);
            var fixtures = new[]
            {
                new[] { 9, 1, 5, 3, 7 },
                new[] { 3, 1, 3, 2, 3 },
                new[] { 4 },
                new[] { 2, 2, 2, 2 },
                new[] { 5, 4, 3, 2, 1, 0 },
                new[] { -3, 10, -3, 0, 7, 10 }
            };

            foreach (var input in fixtures) AssertMatchesSortedK(input);

            for (var t = 0; t < 40; t++)
            {
                var n = rnd.Next(1, 35);
                var input = Enumerable.Range(0, n).Select(_ => rnd.Next(-50, 50)).ToArray();
                AssertMatchesSortedK(input);
            }
        }

        private static void AssertMatchesSortedK(int[] input)
        {
            var sorted = (int[])input.Clone();
            Array.Sort(sorted);

            for (var k = 1; k <= input.Length; k++)
            {
                var actual = QuickSelect<int>.FindSmallest((int[])input.Clone(), k);
                Assert.AreEqual(sorted[k - 1], actual);
            }

            Assert.AreEqual(0, QuickSelect<int>.FindSmallest(input, 0));
            Assert.AreEqual(0, QuickSelect<int>.FindSmallest(input, input.Length + 1));
        }
    }
}
