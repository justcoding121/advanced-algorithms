using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class MColoringTests
    {
        [TestMethod]
        public void MColoring_AdjacencyListGraph_Smoke_Test()
        {
            var graph = new Graph<int>();

            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);

            var algorithm = new MColorer<int, string>();

            var result = algorithm.Color(graph, new[] { "red", "green", "blue" });

            Assert.IsTrue(result.CanColor);
        }

        [TestMethod]
        public void MColoring_AdjacencyMatrixGraph_Smoke_Test()
        {
            var graph = new Algorithms.DataStructures.Graph.AdjacencyMatrix.Graph<int>();

            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);

            var algorithm = new MColorer<int, string>();

            var result = algorithm.Color(graph, new[] { "red", "green", "blue" });

            Assert.IsTrue(result.CanColor);
        }

        [TestMethod]
        public void MColoring_Odd_Cycle_Needs_Three_Colors()
        {
            var graph = new Graph<char>();
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddEdge('A', 'B');
            graph.AddEdge('B', 'C');
            graph.AddEdge('C', 'A');

            var algorithm = new MColorer<char, int>();

            Assert.IsFalse(algorithm.Color(graph, new[] { 1, 2 }).CanColor);

            var withThree = algorithm.Color(graph, new[] { 1, 2, 3 });
            Assert.IsTrue(withThree.CanColor);
            AssertProperColoring(graph, withThree);
        }

        [TestMethod]
        public void MColoring_Does_Not_Throw_When_Uncolorable()
        {
            var graph = new Graph<char>();
            foreach (var c in "abcd") graph.AddVertex(c);
            graph.AddEdge('a', 'b');
            graph.AddEdge('a', 'c');
            graph.AddEdge('b', 'c');
            graph.AddEdge('b', 'd');
            graph.AddEdge('c', 'd');

            var result = new MColorer<char, int>().Color(graph, new[] { 1, 2 });
            Assert.IsFalse(result.CanColor);
        }

        private static void AssertProperColoring(Graph<char> graph, MColorResult<char, int> result)
        {
            var colorOf = new Dictionary<char, int>();
            foreach (var part in result.Partitions)
            foreach (var v in part.Value)
                colorOf[v] = part.Key;

            foreach (char v in (System.Collections.Generic.IEnumerable<char>)graph)
            foreach (var to in graph.Edges(v))
                Assert.AreNotEqual(colorOf[v], colorOf[to]);
        }
    }
}