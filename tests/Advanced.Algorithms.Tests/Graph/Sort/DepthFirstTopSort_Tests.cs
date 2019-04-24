using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class DepthFirstTopSort_Tests
    {
        [TestMethod]
        public void DFS_Topological_Sort_AdjancencyListGraph_Smoke_Test()
        {
            var graph = new Advanced.Algorithms.DataStructures.Graph.AdjacencyList.DiGraph<char>();

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

            var algorithm = new DepthFirstTopSort<char>();

            var result = algorithm.GetTopSort(graph);

            Assert.AreEqual(result.Count, 8);
        }

        [TestMethod]
        public void DFS_Topological_Sort_AdjancencyMatrixGraph_Smoke_Test()
        {
            var graph = new Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix.DiGraph<char>();

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

            var algorithm = new DepthFirstTopSort<char>();

            var result = algorithm.GetTopSort(graph);

            Assert.AreEqual(result.Count, 8);
        }
    }
}
