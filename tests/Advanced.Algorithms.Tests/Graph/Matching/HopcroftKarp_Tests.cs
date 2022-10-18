using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class HopcroftKarpTests
    {
        [TestMethod]
        public void HopcroftKarp_AdjacencyListGraph_Smoke_Test()
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
            graph.AddVertex('I');

            graph.AddEdge('A', 'F');
            graph.AddEdge('B', 'F');
            graph.AddEdge('B', 'G');
            graph.AddEdge('C', 'H');
            graph.AddEdge('C', 'I');
            graph.AddEdge('D', 'G');
            graph.AddEdge('D', 'H');
            graph.AddEdge('E', 'F');
            graph.AddEdge('E', 'I');

            var algorithm = new HopcroftKarpMatching<char>();

            var result = algorithm.GetMaxBiPartiteMatching(graph);

            Assert.AreEqual(result.Count, 4);
        }

        [TestMethod]
        public void HopcroftKarp_AdjacencyMatrixGraph_Smoke_Test()
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
            graph.AddVertex('I');

            graph.AddEdge('A', 'F');
            graph.AddEdge('B', 'F');
            graph.AddEdge('B', 'G');
            graph.AddEdge('C', 'H');
            graph.AddEdge('C', 'I');
            graph.AddEdge('D', 'G');
            graph.AddEdge('D', 'H');
            graph.AddEdge('E', 'F');
            graph.AddEdge('E', 'I');

            var algorithm = new HopcroftKarpMatching<char>();

            var result = algorithm.GetMaxBiPartiteMatching(graph);

            Assert.AreEqual(result.Count, 4);
        }

        [TestMethod]
        public void HopcroftKarp_AdjacencyListGraph_Accurancy_Test()
        {
            var graph = new Graph<char>();

            graph.AddVertex('E');
            graph.AddVertex('N');
            graph.AddVertex('J');
            graph.AddVertex('O');
            graph.AddVertex('Y');
            graph.AddVertex('Z');

            graph.AddVertex('1');
            graph.AddVertex('2');
            graph.AddVertex('3');
            graph.AddVertex('4');
            graph.AddVertex('5');
            graph.AddVertex('6');

            graph.AddEdge('E', '1');

            graph.AddEdge('N', '4');

            graph.AddEdge('J', '1');
            graph.AddEdge('J', '2');
            graph.AddEdge('J', '4');

            graph.AddEdge('O', '2');
            graph.AddEdge('O', '3');

            graph.AddEdge('Y', '3');
            graph.AddEdge('Y', '5');

            graph.AddEdge('Z', '4');
            graph.AddEdge('Z', '5');
            graph.AddEdge('Z', '6');

            var algorithm = new HopcroftKarpMatching<char>();

            var result = algorithm.GetMaxBiPartiteMatching(graph);

            Assert.AreEqual(result.Count, 6);
        }


        [TestMethod]
        public void HopcroftKarp_AdjacencyListGraph_Accurancy_Test_Fully_Connected_Bipartite_Graph()
        {
            var graph = new Graph<char>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('E');
            graph.AddVertex('F');

            graph.AddEdge('A', 'D');
            graph.AddEdge('A', 'E');
            graph.AddEdge('A', 'F');
            graph.AddEdge('B', 'D');
            graph.AddEdge('B', 'E');
            graph.AddEdge('B', 'F');
            graph.AddEdge('C', 'D');
            graph.AddEdge('C', 'E');
            graph.AddEdge('C', 'F');

            var algorithm = new HopcroftKarpMatching<char>();

            var result = algorithm.GetMaxBiPartiteMatching(graph);

            Assert.AreEqual(result.Count, 3);
        }
    }
}