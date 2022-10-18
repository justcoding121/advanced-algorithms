using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class BellmanFordTests
    {
        [TestMethod]
        public void BellmanFord_AdjacencyList_Smoke_Test()
        {
            var graph = new WeightedDiGraph<char, int>();

            graph.AddVertex('S');
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('T');

            graph.AddEdge('S', 'A', -10);
            graph.AddEdge('S', 'C', -5);

            graph.AddEdge('A', 'B', 4);
            graph.AddEdge('A', 'C', 2);
            graph.AddEdge('A', 'D', 8);

            graph.AddEdge('B', 'T', 10);

            graph.AddEdge('C', 'D', 9);

            graph.AddEdge('D', 'B', 6);
            graph.AddEdge('D', 'T', 10);

            var algorithm = new BellmanFordShortestPath<char, int>(new BellmanFordShortestPathOperators());

            var result = algorithm.FindShortestPath(graph, 'S', 'T');

            Assert.AreEqual(4, result.Length);

            var expectedPath = new[] { 'S', 'A', 'B', 'T' };
            for (var i = 0; i < expectedPath.Length; i++) Assert.AreEqual(expectedPath[i], result.Path[i]);
        }

        [TestMethod]
        public void BellmanFord_AdjacencyMatrix_Smoke_Test()
        {
            var graph = new Algorithms.DataStructures.Graph.AdjacencyMatrix.WeightedDiGraph<char, int>();

            graph.AddVertex('S');
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('T');

            graph.AddEdge('S', 'A', -10);
            graph.AddEdge('S', 'C', -5);

            graph.AddEdge('A', 'B', 4);
            graph.AddEdge('A', 'C', 2);
            graph.AddEdge('A', 'D', 8);

            graph.AddEdge('B', 'T', 10);

            graph.AddEdge('C', 'D', 9);

            graph.AddEdge('D', 'B', 6);
            graph.AddEdge('D', 'T', 10);

            var algorithm = new BellmanFordShortestPath<char, int>(new BellmanFordShortestPathOperators());

            var result = algorithm.FindShortestPath(graph, 'S', 'T');

            Assert.AreEqual(4, result.Length);

            var expectedPath = new[] { 'S', 'A', 'B', 'T' };
            for (var i = 0; i < expectedPath.Length; i++) Assert.AreEqual(expectedPath[i], result.Path[i]);
        }

        /// <summary>
        ///     generic operations for int type
        /// </summary>
        public class BellmanFordShortestPathOperators : IShortestPathOperators<int>
        {
            public int DefaultValue => 0;

            public int MaxValue => int.MaxValue;

            public int Sum(int a, int b)
            {
                return checked(a + b);
            }
        }
    }
}