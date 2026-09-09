using System;
using System.Linq;
using Advanced.Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Sorting
{
    [TestClass]
    public class TreeSortTests
    {
        private static readonly int[] TestArray =
            { 12, 7, 9, 8, 3, 10, 2, 1, 5, 11, 4, 6, 0 };

        [TestMethod]
        public void TreeSort_Ascending_Smoke_Test()
        {
            var result = TreeSort<int>.Sort(TestArray).ToArray();

            for (var i = 0; i < TestArray.Length; i++) Assert.AreEqual(i, result[i]);
        }

        [TestMethod]
        public void TreeSort_Descending_Smoke_Test()
        {
            var result = TreeSort<int>.Sort(TestArray, SortDirection.Descending).ToArray();

            for (var i = 0; i < TestArray.Length; i++) Assert.AreEqual(TestArray.Length - i - 1, result[i]);
        }

        [TestMethod]
        public void TreeSort_Ascending_Stress_Test()
        {
            var rnd = new Random();
            var nodeCount = 1000;
            var randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            var result = TreeSort<int>.Sort(randomNumbers).ToArray();

            for (var i = 1; i <= nodeCount; i++) Assert.AreEqual(i, result[i - 1]);
        }

        [TestMethod]
        public void TreeSort_Descending_Stress_Test()
        {
            var rnd = new Random();
            var nodeCount = 1000;
            var randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            var result = TreeSort<int>.Sort(randomNumbers, SortDirection.Descending).ToArray();

            for (var i = 0; i < nodeCount; i++) Assert.AreEqual(randomNumbers.Count - i, result[i]);
        }

        [TestMethod]
        public void TreeSort_Corner_Cases()
        {
            CollectionAssert.AreEqual(new int[0], TreeSort<int>.Sort(new int[0]).ToArray());
            CollectionAssert.AreEqual(new[] { 7 }, TreeSort<int>.Sort(new[] { 7 }).ToArray());
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, TreeSort<int>.Sort(new[] { 1, 2, 3, 4 }).ToArray());
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, TreeSort<int>.Sort(new[] { 4, 3, 2, 1 }).ToArray());
        }

        [TestMethod]
        public void TreeSort_Oracle_Against_Array_Sort()
        {
            var rnd = new Random(42);
            var fixtures = new[]
            {
                Array.Empty<int>(),
                new[] { 7 },
                new[] { 1, 2, 3, 4 },
                new[] { 4, 3, 2, 1 },
                new[] { 2, 1 },
                new[] { 0 }
            };

            foreach (var fixture in fixtures) AssertMatchesArraySort(fixture);

            for (var t = 0; t < 40; t++)
            {
                var n = rnd.Next(0, 35);
                var input = Enumerable.Range(0, n).Select(_ => rnd.Next(-30, 30)).Distinct().ToArray();
                AssertMatchesArraySort(input);
            }
        }

        [TestMethod]
        public void TreeSort_Duplicates_Throw()
        {
            Assert.ThrowsException<ArgumentException>(() => TreeSort<int>.Sort(new[] { 1, 2, 2, 3 }).ToArray());
        }

        private static void AssertMatchesArraySort(int[] input)
        {
            var expectedAsc = (int[])input.Clone();
            Array.Sort(expectedAsc);
            CollectionAssert.AreEqual(expectedAsc, TreeSort<int>.Sort(input).ToArray());

            var expectedDesc = (int[])expectedAsc.Clone();
            Array.Reverse(expectedDesc);
            CollectionAssert.AreEqual(expectedDesc, TreeSort<int>.Sort(input, SortDirection.Descending).ToArray());
        }
    }
}
