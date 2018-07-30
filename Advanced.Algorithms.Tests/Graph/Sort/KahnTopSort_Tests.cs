using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class KahnsTopSort_Tests
    {
        [TestMethod]
        public void Kahns_Topological_Sort_Smoke_Test()
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


            graph.AddEdge('A', 'B');
            graph.AddEdge('B', 'C');

            graph.AddEdge('C', 'D');
            graph.AddEdge('E', 'D');

            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');

            graph.AddEdge('F', 'H');

            var algo = new KahnsTopSort<char>();

            var result = algo.GetTopSort(graph);

            Assert.AreEqual(result.Count, 8);
        }
    }
}
