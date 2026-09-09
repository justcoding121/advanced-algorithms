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

        [TestMethod]
        public void BellmanFord_Oracle_Matches_Dijkstra_On_NonNegative()
        {
            var graph = new WeightedDiGraph<char, int>();
            foreach (var v in "SABCDT") graph.AddVertex(v);
            graph.AddEdge('S', 'A', 8);
            graph.AddEdge('S', 'C', 10);
            graph.AddEdge('A', 'B', 10);
            graph.AddEdge('A', 'C', 1);
            graph.AddEdge('A', 'D', 8);
            graph.AddEdge('B', 'T', 4);
            graph.AddEdge('C', 'D', 1);
            graph.AddEdge('D', 'B', 1);
            graph.AddEdge('D', 'T', 10);

            var op = new BellmanFordShortestPathOperators();
            var bf = new BellmanFordShortestPath<char, int>(op);
            var di = new DijikstraShortestPath<char, int>(op);

            foreach (var s in "SABCDT")
            foreach (var t in "SABCDT")
            {
                var a = bf.FindShortestPath(graph, s, t);
                var b = di.FindShortestPath(graph, s, t);
                Assert.AreEqual(b.Length, a.Length, $"{s}->{t}");
            }
        }

        [TestMethod]
        public void BellmanFord_NegativeCycle_Throws()
        {
            var graph = new WeightedDiGraph<char, int>();
            foreach (var v in "ABC") graph.AddVertex(v);
            graph.AddEdge('A', 'B', 1);
            graph.AddEdge('B', 'C', 1);
            graph.AddEdge('C', 'A', -3);

            Assert.ThrowsException<System.InvalidOperationException>(() =>
                new BellmanFordShortestPath<char, int>(new BellmanFordShortestPathOperators())
                    .FindShortestPath(graph, 'A', 'C'));
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