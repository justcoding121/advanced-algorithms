using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Algorithm.Sandbox.DataStructures.Heap;
using Algorithm.Sandbox.DataStructures.Heap.Max;

namespace Algorithm.Sandbox.Tests.DataStructures.Heap.Max
{
    [TestClass]
    public class BinomialMaxHeap_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void BinomialMaxHeap_Test()
        {

            int nodeCount = 1000 * 10;
            //insert test
            var tree = new AsBinomialMaxHeap<int>();

            var nodePointers = new List<BinomialHeapNode<int>>();

            for (int i = 0; i <= nodeCount; i++)
            {
                var node = tree.Insert(i);
                nodePointers.Add(node);
                var theoreticalTreeCount = Convert.ToString(i + 1, 2).Replace("0", "").Length;
                var actualTreeCount = tree.heapForest.Count();

                Assert.AreEqual(theoreticalTreeCount, actualTreeCount);
            }

            for (int i = 0; i <= nodeCount; i++)
            {
                nodePointers[i].Value++;
                tree.IncrementKey(nodePointers[i]);
            }
            int max = 0;
            for (int i = nodeCount; i >=0 ; i--)
            {
                max = tree.ExtractMax();
                Assert.AreEqual(i + 1, max);
            }

            nodePointers.Clear();

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(x => rnd.Next()).ToList();


            foreach (var item in testSeries)
            {
                nodePointers.Add(tree.Insert(item));
            }

            max = tree.ExtractMax();
            nodePointers = nodePointers.Where(x => x.Value != max).ToList();
            var resultSeries = new List<int>();

            for (int i = 0; i < nodePointers.Count; i++)
            {
                nodePointers[i].Value = nodePointers[i].Value + rnd.Next(0, 1000);
                tree.IncrementKey(nodePointers[i]);
            }

            foreach (var item in nodePointers)
            {
                resultSeries.Add(item.Value);
            }

            resultSeries = resultSeries.OrderByDescending(x => x).ToList();

            for (int i = 0; i < nodeCount - 2; i++)
            {
                max = tree.ExtractMax();
                Assert.AreEqual(resultSeries[i], max);
            }
        }
    }
}
