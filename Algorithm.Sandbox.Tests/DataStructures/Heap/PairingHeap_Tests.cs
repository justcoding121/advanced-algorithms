using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Sandbox.Tests.DataStructures.Heap
{
    [TestClass]
    public class PairingMinHeap_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void PairingMinHeap_Test()
        {

            int nodeCount = 1000 * 10;
            //insert test
            var tree = new AsPairingMinHeap<int>();

            var nodePointers = new List<PairingTreeNode<int>>();

            for (int i = 0; i <= nodeCount; i++)
            {
                var node = tree.Insert(i);
                nodePointers.Add(node);
            }

            for (int i = 0; i <= nodeCount; i++)
            {
                nodePointers[i].Value--;
                tree.DecrementKey(nodePointers[i]);
            }
            int min = 0;
            for (int i = 0; i <= nodeCount; i++)
            {
                min = tree.ExtractMin();
                Assert.AreEqual(min, i - 1);
            }

            nodePointers.Clear();

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(x => rnd.Next()).ToList();


            foreach (var item in testSeries)
            {
                nodePointers.Add(tree.Insert(item));
            }

            min = tree.ExtractMin();
            nodePointers = nodePointers.Where(x => x.Value != min).ToList();
            var resultSeries = new List<int>();

            for (int i = 0; i < nodePointers.Count; i++)
            {
                nodePointers[i].Value = nodePointers[i].Value - rnd.Next(0, 1000);
                tree.DecrementKey(nodePointers[i]);
            }

            foreach (var item in nodePointers)
            {
                resultSeries.Add(item.Value);
            }

            resultSeries.Sort();

            for (int i = 0; i < nodeCount - 2; i++)
            {
                min = tree.ExtractMin();
                Assert.AreEqual(resultSeries[i], min);
            }

        }
    }
}
