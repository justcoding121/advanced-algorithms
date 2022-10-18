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
    }
}