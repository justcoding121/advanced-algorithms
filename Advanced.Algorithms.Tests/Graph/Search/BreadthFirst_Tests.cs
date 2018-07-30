using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class BreadFirst_Tests
    {

        [TestMethod]
        public void BreadthFirst_Smoke_Test()
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

            graph.AddEdge('A', 'B');
            graph.AddEdge('B', 'C');
            graph.AddEdge('C', 'D');
            graph.AddEdge('D', 'E');
            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');
            graph.AddEdge('G', 'H');
            graph.AddEdge('H', 'I');

            var algo = new BreadthFirst<char>();

            Assert.IsTrue(algo.Find(graph, 'D'));

            Assert.IsFalse(algo.Find(graph, 'M'));

        }


    }
}
