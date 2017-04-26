using Algorithm.Sandbox.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Algorithm.Sandbox.Tests.Sorting
{
    [TestClass]
    public class ShellSort_Tests
    {
        private static int[] TestArray =
            new int[] { 12, 7, 9, 8, 3, 10, 2, 1, 5, 11, 4, 6, 0 };

        /// <summary>
        /// </summary>
        [TestMethod]
        public void ShellSort_Smoke_Test()
        {
            var result = ShellSort<int>.Sort(TestArray);

            for (int i = 0; i < TestArray.Length; i++)
            {
                Assert.AreEqual(i, result[i]);
            }

        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void ShellSort_Stress_Test()
        {
            var rnd = new Random();
            var nodeCount = 1000;
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var result = ShellSort<int>.Sort(randomNumbers.ToArray());

            for (int i = 1; i <= nodeCount; i++)
            {
                Assert.AreEqual(i, result[i - 1]);
            }
        }


    }
}
