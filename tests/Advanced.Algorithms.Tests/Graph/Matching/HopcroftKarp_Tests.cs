using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class HopcroftKarp_Tests
    {

        [TestMethod]
        public void HopcroftKarp_AdjacencyListGraph_Smoke_Test()
        {

            var graph = new Advanced.Algorithms.DataStructures.Graph.AdjacencyList.Graph<char>();

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
        public void HopcroftKarp_AdjacencyListGraph_Accurancy_Test_1()
        {

            var graph = new Advanced.Algorithms.DataStructures.Graph.AdjacencyList.Graph<char>();

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

            Assert.AreEqual(result.Count, 4);

        }
    }
}
