using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class BiDirectional_Tests
    {

        [TestMethod]
        public void BiDirectional_Smoke_Test()
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
            graph.AddVertex('I');

            graph.AddEdge('A', 'B');
            graph.AddEdge('B', 'C');
            graph.AddEdge('C', 'D');
            graph.AddEdge('D', 'E');
            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');
            graph.AddEdge('G', 'H');
            graph.AddEdge('H', 'I');

            var algo = new BiDirectional<char>();

            Assert.IsTrue(algo.PathExists(graph, 'A', 'I'));

            graph.RemoveEdge('D', 'E');
            graph.AddEdge('E', 'D');

            Assert.IsFalse(algo.PathExists(graph, 'A', 'I'));

        }


    }
}
