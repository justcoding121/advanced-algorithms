using System;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class FordFulkersonTests
    {
        /// <summary>
        ///     FordFulkerson Max Flow test
        /// </summary>
        [TestMethod]
        public void FordFulkerson_AdjacencyListGraph_Smoke_Test()
        {
            var graph = new WeightedDiGraph<char, int>();

            graph.AddVertex('S');
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('T');

            graph.AddEdge('S', 'A', 10);
            graph.AddEdge('S', 'C', 10);

            graph.AddEdge('A', 'B', 4);
            graph.AddEdge('A', 'C', 2);
            graph.AddEdge('A', 'D', 8);

            graph.AddEdge('B', 'T', 10);

            graph.AddEdge('C', 'D', 9);

            graph.AddEdge('D', 'B', 6);
            graph.AddEdge('D', 'T', 10);

            var algorithm = new FordFulkersonMaxFlow<char, int>(new FordFulkersonOperators());

            var result = algorithm.ComputeMaxFlow(graph, 'S', 'T');

            Assert.AreEqual(result, 19);
        }

        /// <summary>
        ///     FordFulkerson Max Flow test
        /// </summary>
        [TestMethod]
        public void FordFulkerson_AdjacencyMatrixGraph_Smoke_Test()
        {
            var graph = new Algorithms.DataStructures.Graph.AdjacencyMatrix.WeightedDiGraph<char, int>();

            graph.AddVertex('S');
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('T');

            graph.AddEdge('S', 'A', 10);
            graph.AddEdge('S', 'C', 10);

            graph.AddEdge('A', 'B', 4);
            graph.AddEdge('A', 'C', 2);
            graph.AddEdge('A', 'D', 8);

            graph.AddEdge('B', 'T', 10);

            graph.AddEdge('C', 'D', 9);

            graph.AddEdge('D', 'B', 6);
            graph.AddEdge('D', 'T', 10);

            var algorithm = new FordFulkersonMaxFlow<char, int>(new FordFulkersonOperators());

            var result = algorithm.ComputeMaxFlow(graph, 'S', 'T');

            Assert.AreEqual(result, 19);
        }

        [TestMethod]
        public void FordFulkerson_Paths_NoPath_And_NullOperator()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new FordFulkersonMaxFlow<char, int>(null).ComputeMaxFlow(new WeightedDiGraph<char, int>(), 'S', 'T'));

            var graph = new WeightedDiGraph<char, int>();
            graph.AddVertex('S');
            graph.AddVertex('A');
            graph.AddVertex('T');
            graph.AddEdge('S', 'A', 5);
            graph.AddEdge('A', 'T', 3);

            var algorithm = new FordFulkersonMaxFlow<char, int>(new FordFulkersonOperators());
            var paths = algorithm.ComputeMaxFlowAndReturnFlowPath(graph, 'S', 'T');
            Assert.IsTrue(paths.Count >= 1);
            Assert.AreEqual('S', paths[0][0]);
            Assert.AreEqual('T', paths[0][paths[0].Count - 1]);
            Assert.AreEqual(3, algorithm.ComputeMaxFlow(graph, 'S', 'T'));

            var disconnected = new WeightedDiGraph<char, int>();
            disconnected.AddVertex('S');
            disconnected.AddVertex('T');
            Assert.AreEqual(0, algorithm.ComputeMaxFlow(disconnected, 'S', 'T'));
            Assert.AreEqual(0, algorithm.ComputeMaxFlowAndReturnFlowPath(disconnected, 'S', 'T').Count);
        }

        /// <summary>
        ///     operators for generics
        ///     implemented for int type for edge weights
        /// </summary>
        public class FordFulkersonOperators : IFlowOperators<int>
        {
            public int AddWeights(int a, int b)
            {
                return checked(a + b);
            }

            public int DefaultWeight => 0;

            public int MaxWeight => int.MaxValue;

            public int SubstractWeights(int a, int b)
            {
                return checked(a - b);
            }
        }
    }
}