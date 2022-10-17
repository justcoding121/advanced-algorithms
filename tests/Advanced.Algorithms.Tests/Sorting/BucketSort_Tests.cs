using System;
using System.Linq;
using Advanced.Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Sorting
{
    [TestClass]
    public class BucketSort_Tests
    {
        private static readonly int[] testArray =
            { 12, 7, 9, 8, 3, 10, 2, 1, 5, 11, 4, 6, 0 };

        [TestMethod]
        public void BucketSort_Ascending_Smoke_Test()
        {
            var result = BucketSort.Sort(testArray, 11);

            for (var i = 0; i < testArray.Length; i++) Assert.AreEqual(i, result[i]);
        }

        [TestMethod]
        public void BucketSort_Descending_Smoke_Test()
        {
            var result = BucketSort.Sort(testArray, 11, SortDirection.Descending);

            for (var i = 0; i < testArray.Length; i++) Assert.AreEqual(testArray.Length - i - 1, result[i]);
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
    }
}