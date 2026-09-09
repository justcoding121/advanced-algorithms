using System;
using System.Linq;
using Advanced.Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Sorting
{
    [TestClass]
    public class BucketSortTests
    {
        private static readonly int[] TestArray =
            { 12, 7, 9, 8, 3, 10, 2, 1, 5, 11, 4, 6, 0 };

        [TestMethod]
        public void BucketSort_Ascending_Smoke_Test()
        {
            var result = BucketSort.Sort(TestArray, 11);

            for (var i = 0; i < TestArray.Length; i++) Assert.AreEqual(i, result[i]);
        }

        [TestMethod]
        public void BucketSort_Descending_Smoke_Test()
        {
            var result = BucketSort.Sort(TestArray, 11, SortDirection.Descending);

            for (var i = 0; i < TestArray.Length; i++) Assert.AreEqual(TestArray.Length - i - 1, result[i]);
        }

        [TestMethod]
        public void BucketSort_Ascending_Stress_Test()
        {
            var rnd = new Random();
            var nodeCount = 1000;
            var randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            var result = BucketSort.Sort(randomNumbers.ToArray(), 4);

            for (var i = 1; i <= nodeCount; i++) Assert.AreEqual(i, result[i - 1]);
        }

        [TestMethod]
        public void BucketSort_Descending_Stress_Test()
        {
            var rnd = new Random();
            var nodeCount = 1000;
            var randomNumbers = Enumerable.Range(1, nodeCount)
                .OrderBy(x => rnd.Next())
                .ToList();

            var result = BucketSort.Sort(randomNumbers.ToArray(), 4, SortDirection.Descending);

            for (var i = 0; i < nodeCount; i++) Assert.AreEqual(randomNumbers.Count - i, result[i]);
        }

        [TestMethod]
        public void BucketSort_Corner_Cases()
        {
            CollectionAssert.AreEqual(new int[0], BucketSort.Sort(new int[0], 0));
            CollectionAssert.AreEqual(new[] { 7 }, BucketSort.Sort(new[] { 7 }, 1));
            CollectionAssert.AreEqual(new[] { 1, 2, 2, 3 }, BucketSort.Sort(new[] { 3, 1, 2, 2 }, 2));
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, BucketSort.Sort(new[] { 1, 2, 3, 4 }, 2));
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, BucketSort.Sort(new[] { 4, 3, 2, 1 }, 2));
        }

        [TestMethod]
        public void BucketSort_Invalid_Bucket_Size_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() => BucketSort.Sort(new[] { 1, 2 }, -1));
            Assert.ThrowsException<ArgumentException>(() => BucketSort.Sort(new[] { 1, 2 }, 0));
            Assert.ThrowsException<ArgumentException>(() => BucketSort.Sort(new[] { 1, 2 }, 3));
        }

        [TestMethod]
        public void BucketSort_Oracle_Against_Array_Sort()
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
            var bucketSize = input.Length == 0 ? 0 : Math.Max(1, input.Length / 3);
            var expectedAsc = (int[])input.Clone();
            Array.Sort(expectedAsc);
            CollectionAssert.AreEqual(expectedAsc, BucketSort.Sort((int[])input.Clone(), bucketSize));

            var expectedDesc = (int[])expectedAsc.Clone();
            Array.Reverse(expectedDesc);
            CollectionAssert.AreEqual(expectedDesc, BucketSort.Sort((int[])input.Clone(), bucketSize, SortDirection.Descending));
        }
    }
}
