using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class DepthFirstTopSortTests
    {
        [TestMethod]
        public void DFS_Topological_Sort_AdjancencyListGraph_Smoke_Test()
        {
            var graph = new DiGraph<char>();

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

            var algorithm = new DepthFirstTopSort<char>();

            var result = algorithm.GetTopSort(graph);

            Assert.AreEqual(result.Count, 8);
            AssertTopological(graph, result);
        }

        [TestMethod]
        public void DFS_Topological_Sort_AdjancencyMatrixGraph_Smoke_Test()
        {
            var graph = new Algorithms.DataStructures.Graph.AdjacencyMatrix.DiGraph<char>();

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

            var algorithm = new DepthFirstTopSort<char>();

            var result = algorithm.GetTopSort(graph);

            Assert.AreEqual(result.Count, 8);
            AssertTopological(graph, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DFS_Topological_Sort_Throws_On_Cycle()
        {
            var graph = new DiGraph<char>();
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddEdge('A', 'B');
            graph.AddEdge('B', 'C');
            graph.AddEdge('C', 'A');

            new DepthFirstTopSort<char>().GetTopSort(graph);
        }

        private static void AssertTopological(DiGraph<char> graph, List<char> order)
        {
            var index = order.Select((v, i) => (v, i)).ToDictionary(x => x.v, x => x.i);
            foreach (char v in (System.Collections.Generic.IEnumerable<char>)graph)
            foreach (var to in graph.OutEdges(v))
                Assert.IsTrue(index[v] < index[to], $"edge {v}->{to} violates topo order");
        }

        private static void AssertTopological(
            Algorithms.DataStructures.Graph.AdjacencyMatrix.DiGraph<char> graph, List<char> order)
        {
            var index = order.Select((v, i) => (v, i)).ToDictionary(x => x.v, x => x.i);
            foreach (char v in (System.Collections.Generic.IEnumerable<char>)graph)
            foreach (var to in graph.OutEdges(v))
                Assert.IsTrue(index[v] < index[to], $"edge {v}->{to} violates topo order");
        }
    }
}
