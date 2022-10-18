using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class TarjansBridgeTests
    {
        [TestMethod]
        public void TarjanBridge_AdjacencyListGraph_Smoke_Test()
        {
            var graph = new Graph<char>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('E');
            graph.AddVertex('F');
            graph.AddVertex('G');
            graph.AddVertex('H');


            graph.AddEdge('A', 'B');
            graph.AddEdge('A', 'C');
            graph.AddEdge('B', 'C');

            graph.AddEdge('C', 'D');
            graph.AddEdge('D', 'E');

            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');
            graph.AddEdge('G', 'E');

            graph.AddEdge('F', 'H');

            var algorithm = new TarjansBridgeFinder<char>();

            var result = algorithm.FindBridges(graph);

            Assert.AreEqual(3, result.Count);

            var expected = new List<char[]>
            {
                new[] { 'C', 'D' },
                new[] { 'D', 'E' },
                new[] { 'F', 'H' }
            };

            foreach (var bridge in result)
                Assert.IsTrue(expected.Any(x => bridge.VertexA == x[0]
                                                && bridge.VertexB == x[1]));
        }

        [TestMethod]
        public void TarjanBridge_AdjacencyMatrixGraph_Smoke_Test()
        {
            var graph = new Algorithms.DataStructures.Graph.AdjacencyMatrix.Graph<char>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('E');
            graph.AddVertex('F');
            graph.AddVertex('G');
            graph.AddVertex('H');


            graph.AddEdge('A', 'B');
            graph.AddEdge('A', 'C');
            graph.AddEdge('B', 'C');

            graph.AddEdge('C', 'D');
            graph.AddEdge('D', 'E');

            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');
            graph.AddEdge('G', 'E');

            graph.AddEdge('F', 'H');

            var algorithm = new TarjansBridgeFinder<char>();

            var result = algorithm.FindBridges(graph);

            Assert.AreEqual(3, result.Count);

            var expected = new List<char[]>
            {
                new[] { 'C', 'D' },
                new[] { 'D', 'E' },
                new[] { 'F', 'H' }
            };

            foreach (var bridge in result)
                Assert.IsTrue(expected.Any(x => bridge.VertexA == x[0]
                                                && bridge.VertexB == x[1]));
        }
    }
}