using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class BMaxHeap_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void BMaxHeap_Test()
        {
            //insert test
            var tree = new AsBMaxHeap<int>();

            for (int i = 0; i <= 99; i++)
            {
                tree.Insert(i);
            }

            for (int i = 99; i >= 0; i--)
            {
                var max = tree.ExtractMax();
                Assert.AreEqual(max, i);
            }

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, 49).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                tree.Insert(item);
            }

            for (int i = 49; i >= 1; i--)
            { 
                var max = tree.ExtractMax();
                Assert.AreEqual(max, i);
            }

        }
    }
}
