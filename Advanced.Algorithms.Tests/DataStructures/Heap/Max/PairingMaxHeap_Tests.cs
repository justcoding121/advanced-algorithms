using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class PairingMaxHeap_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void PairingMaxHeap_Test()
        {

            int nodeCount = 1000 * 10;
            //insert test
            var tree = new PairingMaxHeap<int>();

            var nodePointers = new List<PairingHeapNode<int>>();

            for (int i = 0; i <= nodeCount; i++)
            {
                var node = tree.Insert(i);
                nodePointers.Add(node);
            }

            for (int i = 0; i <= nodeCount; i++)
            {
                nodePointers[i].Value++;
                tree.IncrementKey(nodePointers[i]);
            }
            int max = 0;
            for (int i = nodeCount; i >= 0; i--)
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
