using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Heap.Max;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures.Heap.Max
{
    [TestClass]
    public class AsD_aryMaxTree_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void AsD_aryHeap_Test()
        {
            var initial = new List<int>();

            for (int i = 0; i <= 50; i++)
            {
                initial.Add(i);
            }

            //insert test
            var tree = new D_aryMaxHeap<int>(4, initial);

            for (int i = 51; i <= 99; i++)
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

            for (int i = 49; i > 0; i--)
            {
                var max = tree.ExtractMax();
                Assert.AreEqual(i, max);
            }

        }
    }
}
