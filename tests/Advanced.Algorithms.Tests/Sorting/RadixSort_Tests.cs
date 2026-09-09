using System;
using System.Linq;
using Advanced.Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Sorting
{
    [TestClass]
    public class RadixSortTests
    {
        private static readonly int[] TestArray =
            { 12, 7, 9, 8, 3, 10, 2, 1, 5, 11, 4, 6, 0 };

        [TestMethod]
        public void RadixSort_Ascending_Smoke_Test()
        {
            var result = RadixSort.Sort(TestArray);

            for (var i = 0; i < TestArray.Length; i++) Assert.AreEqual(i, result[i]);
        }

        [TestMethod]
        public void RadixSort_Descending_Smoke_Test()
        {
            var result = RadixSort.Sort(TestArray, SortDirection.Descending);

            for (var i = 0; i < TestArray.Length; i++) Assert.AreEqual(TestArray.Length - i - 1, result[i]);
        }

        [TestMethod]
        public void RadixSort_Ascending_Stress_Test()
        {
            var rnd = new Random();
            var nodeCount = 1000;
            var randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            var result = RadixSort.Sort(randomNumbers.ToArray());

            for (var i = 1; i <= nodeCount; i++) Assert.AreEqual(i, result[i - 1]);
        }

        [TestMethod]
        public void RadixSort_Descending_Stress_Test()
        {
            var rnd = new Random();
            var nodeCount = 1000;
            var randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            var result = RadixSort.Sort(randomNumbers.ToArray(), SortDirection.Descending);

            for (var i = 0; i < nodeCount; i++) Assert.AreEqual(randomNumbers.Count - i, result[i]);
        }

        [TestMethod]
        public void RadixSort_Corner_Cases()
        {
            CollectionAssert.AreEqual(Array.Empty<int>(), RadixSort.Sort(Array.Empty<int>()));
            CollectionAssert.AreEqual(new[] { 7 }, RadixSort.Sort(new[] { 7 }));
            CollectionAssert.AreEqual(new[] { 1, 2, 2, 3 }, RadixSort.Sort(new[] { 3, 1, 2, 2 }));
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, RadixSort.Sort(new[] { 1, 2, 3, 4 }));
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, RadixSort.Sort(new[] { 4, 3, 2, 1 }));
        }

        [TestMethod]
        public void RadixSort_Negative_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() => RadixSort.Sort(new[] { 1, -1, 2 }));
        }

        [TestMethod]
        public void RadixSort_Oracle_Against_Array_Sort()
        {
            var rnd = new Random(42);
            var fixtures = new[]
            {
                Array.Empty<int>(),
                new[] { 7 },
                new[] { 3, 1, 2, 2 },
                new[] { 1, 2, 3, 4 },
                new[] { 4, 3, 2, 1 },
                new[] { 5, 5, 5, 5 },
                new[] { 2, 1 },
                new[] { 0 }
            };

            foreach (var fixture in fixtures) AssertMatchesArraySort(fixture);

            for (var t = 0; t < 40; t++)
            {
                var n = rnd.Next(0, 35);
                var input = Enumerable.Range(0, n).Select(_ => rnd.Next(0, 50)).ToArray();
                AssertMatchesArraySort(input);
            }
        }

        private static void AssertMatchesArraySort(int[] input)
        {
            var expectedAsc = (int[])input.Clone();
            Array.Sort(expectedAsc);
            CollectionAssert.AreEqual(expectedAsc, RadixSort.Sort((int[])input.Clone()));

            var expectedDesc = (int[])expectedAsc.Clone();
            Array.Reverse(expectedDesc);
            CollectionAssert.AreEqual(expectedDesc, RadixSort.Sort((int[])input.Clone(), SortDirection.Descending));
        }
    }
}
