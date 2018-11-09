using Advanced.Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Advanced.Algorithms.Tests.Sorting
{
    [TestClass]
    public class HeapSort_Tests
    {
        private static int[] testArray =
            new int[] { 12, 7, 9, 8, 3, 10, 2, 1, 5, 11, 4, 6, 0 };

        [TestMethod]
        public void HeapSort_Ascending_Smoke_Test()
        {
            var result = HeapSort<int>.Sort(testArray);

            for (int i = 0; i < testArray.Length; i++)
            {
                Assert.AreEqual(i, result[i]);
            }
        }

        [TestMethod]
        public void HeapSort_Descending_Smoke_Test()
        {
            var result = HeapSort<int>.Sort(testArray, SortDirection.Descending);

            for (int i = 0; i < testArray.Length; i++)
            {
                Assert.AreEqual(testArray.Length - i - 1, result[i]);
            }
        }

        [TestMethod]
        public void HeapSort_Ascending_Stress_Test()
        {
            var rnd = new Random();
            var nodeCount = 1000;
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var result = HeapSort<int>.Sort(randomNumbers.ToArray());

            for (int i = 1; i <= nodeCount; i++)
            {
                Assert.AreEqual(i, result[i - 1]);
            }
        }

        [TestMethod]
        public void HeapSort_Descending_Stress_Test()
        {
            var rnd = new Random();
            var nodeCount = 1000;
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var result = HeapSort<int>.Sort(randomNumbers.ToArray(), SortDirection.Descending);

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.AreEqual(randomNumbers.Count - i, result[i]);
            }
        }

    }
}
