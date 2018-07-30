using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class HopcroftKarp_Tests
    {

        [TestMethod]
        public void HopcroftKarp_Smoke_Test()
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

            var algo = new HopcroftKarpMatching<char>(null);

            var result = algo.GetMaxBiPartiteMatching(graph);

            Assert.AreEqual(result.Count, 4);

        }


    }
}
