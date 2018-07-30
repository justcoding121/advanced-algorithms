using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{

    [TestClass]
    public class TarjansBiConnected_Tests
    {

        [TestMethod]
        public void TarjanIsBiConnected_Smoke_Test()
        {
            var graph = new Graph<char>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');

            graph.AddEdge('A', 'B');
            graph.AddEdge('A', 'C');
            graph.AddEdge('B', 'C');

         
            var algo = new TarjansBiConnected<char>();

            var result = algo.IsBiConnected(graph);

            Assert.IsTrue(result);

            graph.AddVertex('D');
            graph.AddVertex('E');
            graph.AddVertex('F');
            graph.AddVertex('G');
            graph.AddVertex('H');

            graph.AddEdge('C', 'D');
            graph.AddEdge('D', 'E');

            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');
            graph.AddEdge('G', 'E');

            graph.AddEdge('F', 'H');

            result = algo.IsBiConnected(graph);

            Assert.IsFalse(result);

        }
    }
}
