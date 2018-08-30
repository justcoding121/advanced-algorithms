using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
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
            var rnd = new Random();

            var initial = new List<int>(Enumerable.Range(0, 51)
                .OrderBy(x => rnd.Next()));

            //insert test
            var tree = new BMaxHeap<int>(initial);

            for (int i = 51; i <= 99; i++)
            {
                tree.Insert(i);
            }

            for (int i = 0; i <= 99; i++)
            {
                var Max = tree.ExtractMax();
                Assert.AreEqual(Max, 99 - i);
            }

            //IEnumerable tests.
            Assert.AreEqual(tree.Count, tree.Count());

            var testSeries = Enumerable.Range(1, 49)
                .OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                tree.Insert(item);
            }

            for (int i = 1; i <= 49; i++)
            {
                var max = tree.ExtractMax();
                Assert.AreEqual(max, 49 - i + 1);
            }

            //IEnumerable tests.
            Assert.AreEqual(tree.Count, tree.Count());

        }
    }
}
