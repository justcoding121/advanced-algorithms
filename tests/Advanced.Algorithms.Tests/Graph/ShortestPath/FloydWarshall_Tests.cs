using System.Linq;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class FloydWarshallsTests
    {
        [TestMethod]
        public void FloydWarshall_AdjacencyListGraph_Smoke_Test()
        {
            var graph = new WeightedGraph<char, int>();

            graph.AddVertex('S');
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('T');

            graph.AddEdge('S', 'A', 8);
            graph.AddEdge('S', 'C', 10);

            graph.AddEdge('A', 'B', 10);
            graph.AddEdge('A', 'C', 1);
            graph.AddEdge('A', 'D', 8);

            graph.AddEdge('B', 'T', 4);

            graph.AddEdge('C', 'D', 1);

            graph.AddEdge('D', 'B', 1);
            graph.AddEdge('D', 'T', 10);

            var algorithm = new FloydWarshallShortestPath<char, int>(new FloydWarshallShortestPathOperators());

            var result = algorithm.FindAllPairShortestPaths(graph);

            var testCase = result.First(x => x.Source == 'S' && x.Destination == 'T');
            Assert.AreEqual(15, testCase.Distance);

            var expectedPath = new[] { 'S', 'A', 'C', 'D', 'B', 'T' };
            for (var i = 0; i < expectedPath.Length; i++) Assert.AreEqual(expectedPath[i], testCase.Path[i]);

            testCase = result.First(x => x.Source == 'T' && x.Destination == 'S');
            Assert.AreEqual(15, testCase.Distance);

            expectedPath = new[] { 'T', 'B', 'D', 'C', 'A', 'S' };
            for (var i = 0; i < expectedPath.Length; i++) Assert.AreEqual(expectedPath[i], testCase.Path[i]);
        }

        [TestMethod]
        public void FloydWarshall_AdjacencyMartixGraph_Smoke_Test()
        {
            var graph = new Algorithms.DataStructures.Graph.AdjacencyMatrix.WeightedGraph<char, int>();

            graph.AddVertex('S');
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('T');

            graph.AddEdge('S', 'A', 8);
            graph.AddEdge('S', 'C', 10);

            graph.AddEdge('A', 'B', 10);
            graph.AddEdge('A', 'C', 1);
            graph.AddEdge('A', 'D', 8);

            graph.AddEdge('B', 'T', 4);

            graph.AddEdge('C', 'D', 1);

            graph.AddEdge('D', 'B', 1);
            graph.AddEdge('D', 'T', 10);

            var algorithm = new FloydWarshallShortestPath<char, int>(new FloydWarshallShortestPathOperators());

            var result = algorithm.FindAllPairShortestPaths(graph);

            var testCase = result.First(x => x.Source == 'S' && x.Destination == 'T');
            Assert.AreEqual(15, testCase.Distance);

            var expectedPath = new[] { 'S', 'A', 'C', 'D', 'B', 'T' };
            for (var i = 0; i < expectedPath.Length; i++) Assert.AreEqual(expectedPath[i], testCase.Path[i]);

            testCase = result.First(x => x.Source == 'T' && x.Destination == 'S');
            Assert.AreEqual(15, testCase.Distance);

            expectedPath = new[] { 'T', 'B', 'D', 'C', 'A', 'S' };
            for (var i = 0; i < expectedPath.Length; i++) Assert.AreEqual(expectedPath[i], testCase.Path[i]);
        }

        /// <summary>
        ///     generic operations for int type
        /// </summary>
        public class FloydWarshallShortestPathOperators : IShortestPathOperators<int>
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