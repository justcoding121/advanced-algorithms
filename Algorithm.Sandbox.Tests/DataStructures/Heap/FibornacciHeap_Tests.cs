using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DataStructures.Heap
{
    [TestClass]
    public class FibornacciMinHeap_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void FibornacciMinHeap_Test()
        {
            int nodeCount = 10;
            //insert test
            var tree = new AsFibornacciMinHeap<int>();

            var nodePointers = new List<AsFibornacciTreeNode<int>>();
            //for (int i = 0; i <= nodeCount; i++)
            //{
            //    var node = tree.Insert(i);
            //    nodePointers.Add(node);

            //}

            //for (int i = 0; i <= nodeCount; i++)
            //{
            //    nodePointers[i].Value--;
            //    tree.DecrementKey(nodePointers[i]);
            //}

            int min = 0;
            //for (int i = 0; i <= nodeCount; i++)
            //{
            //    min = tree.ExtractMin();
            //    Assert.AreEqual(min, i - 1);
            //}

            nodePointers.Clear();

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).ToList();


            foreach (var item in testSeries)
            {
                nodePointers.Add(tree.Insert(item));
            }

            min = tree.ExtractMin();
            nodePointers = nodePointers.Where(x => x.Value != min).ToList();
            var resultSeries = new List<int>();

            var testCase = new List<int>() { -590,-82,-897,-700,-154,-485,-846,-692};

            for (int i = 0; i < nodePointers.Count; i++)
            {
                var value = testCase[i];
                Debug.WriteLine(value);
                nodePointers[i].Value = value;
                tree.DecrementKey(nodePointers[i]);
            }

            foreach (var item in nodePointers)
            {
                resultSeries.Add(item.Value);
            }

            var s = resultSeries.Distinct().Count();
            resultSeries.Sort();

            for (int i = 0; i < nodeCount - 2; i++)
            {
                min = tree.ExtractMin();
                Assert.AreEqual(resultSeries[i], min);
            }
        }
    }
}
