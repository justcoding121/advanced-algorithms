using Algorithm.Sandbox.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace Algorithm.Sandbox.Tests.Sorting
{
    [TestClass]
    public class RadixSort_Tests
    {
        private static int[] TestArray =
            new int[] { 12, 7, 9, 8, 3, 10, 2, 1, 5, 11, 4, 6, 0 };

        /// <summary>
        /// </summary>
        [TestMethod]
        public void RadixSort_Smoke_Test()
        {
            var result = RadixSort.Sort(TestArray);

            for (int i = 0; i < TestArray.Length; i++)
            {
                Assert.AreEqual(i, result[i]);
            }

        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void RadixSort_Stress_Test()
        {
            int[] randomNumbers;
            int nodeCount = 1000 * 1000;
            var rnd = new Random();

            randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToArray();

            var timer = new Stopwatch();

            timer.Start();

            var result = RadixSort.Sort(randomNumbers);

            timer.Stop();

            Debug.WriteLine($"sorted {nodeCount} integers using radix sort in {timer.ElapsedMilliseconds} milliseconds.");

            for (int i = 1; i <= nodeCount; i++)
            {
                Assert.AreEqual(i, result[i - 1]);
            }
        }


    }
}
