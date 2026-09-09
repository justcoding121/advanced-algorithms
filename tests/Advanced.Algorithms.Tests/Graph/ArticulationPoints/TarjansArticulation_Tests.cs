using System.Linq;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class TarjansArticulationTests
    {
        [TestMethod]
        public void TarjanArticulation_AdjacencyListGraph_Smoke_Test()
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

            var algorithm = new TarjansArticulationFinder<char>();

            var result = algorithm.FindArticulationPoints(graph);

            Assert.AreEqual(4, result.Count);

            var expectedResult = new[] { 'C', 'D', 'E', 'F' };

            foreach (var v in result) Assert.IsTrue(expectedResult.Contains(v));
        }

        [TestMethod]
        public void TarjanArticulation_AdjacencyMatrixGraph_Smoke_Test()
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

            var algorithm = new TarjansArticulationFinder<char>();

            var result = algorithm.FindArticulationPoints(graph);

            Assert.AreEqual(4, result.Count);

            var expectedResult = new[] { 'C', 'D', 'E', 'F' };

            foreach (var v in result) Assert.IsTrue(expectedResult.Contains(v));
        }

        [TestMethod]
        public void TarjanArticulation_Disconnected_Components()
        {
            var graph = new Graph<char>();
            foreach (var c in "ABCDEF") graph.AddVertex(c);
            // triangle (no AP) + path APs in second component: D-E-F => E is AP
            graph.AddEdge('A', 'B');
            graph.AddEdge('B', 'C');
            graph.AddEdge('C', 'A');
            graph.AddEdge('D', 'E');
            graph.AddEdge('E', 'F');

            var result = new TarjansArticulationFinder<char>().FindArticulationPoints(graph);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual('E', result[0]);
        }
    }
}