using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DataStructures.Heap
{
    [TestClass]
    public class BinomialMinHeap_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void BinomialMinHeap_Test()
        {
            //insert test
            var tree = new AsBinomialMinHeap<int>();

            for (int i = 0; i <= 50; i++)
            {
                tree.Insert(i);

                var theoreticalTreeCount = Convert.ToString(i + 1, 2).Replace("0", "").Length;
                var actualTreeCount = tree.heapForest.Length;

                Assert.AreEqual(theoreticalTreeCount, actualTreeCount);
            }

            for (int i = 0; i <= 50; i++)
            {
                var min = tree.ExtractMin();
                Assert.AreEqual(min, i);
            }

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, 49).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                tree.Insert(item);
            }

            for (int i = 1; i <= 49; i++)
            {
                var min = tree.ExtractMin();
                Assert.AreEqual(min, i);
            }

        }
    }
}
