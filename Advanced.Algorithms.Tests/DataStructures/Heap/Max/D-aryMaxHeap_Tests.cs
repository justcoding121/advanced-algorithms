using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class D_aryMaxTree_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void D_ary_MaxHeap_Test()
        {
            var rnd = new Random();

            var initial = new List<int>(Enumerable.Range(0, 51)
                .OrderBy(x => rnd.Next()));

            var tree = new DaryMaxHeap<int>(4, initial);
            for (int i = 51; i <= 99; i++)
            {
                tree.Insert(i);
            }

            for (int i = 99; i >= 0; i--)
            {
                var max = tree.ExtractMax();
                Assert.AreEqual(max, i);
            }

            //IEnumerable tests.
            Assert.AreEqual(tree.Count, tree.Count());

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

            //IEnumerable tests.
            Assert.AreEqual(tree.Count, tree.Count());
        }
    }
}
