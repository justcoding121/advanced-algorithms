using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.TopologicalSort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.TopologicalSort
{
    [TestClass]
    public class KahnsTopSort_Tests
    {
        [TestMethod]
        public void Smoke_Test_Top_Sort()
        {
            var graph = new AsDiGraph<char>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('E');
            graph.AddVertex('F');
            graph.AddVertex('G');
            graph.AddVertex('H');


            graph.AddEdge('A', 'B');
            graph.AddEdge('B', 'C');

            graph.AddEdge('C', 'D');
            graph.AddEdge('E', 'D');

            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');

            graph.AddEdge('F', 'H');

            var algo = new KahnsTopSort<char>();

            var result = algo.GetTopSort(graph);

            Assert.AreEqual(result.Length, 8);
        }
    }
}
