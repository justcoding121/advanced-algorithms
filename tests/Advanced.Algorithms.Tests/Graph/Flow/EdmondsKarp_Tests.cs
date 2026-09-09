using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class EdmondKarpTests
    {
        /// <summary>
        ///     EdmondKarp Max Flow test
        /// </summary>
        [TestMethod]
        public void EdmondKarp_AdjacencyListGraph_Smoke_Test()
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

            var algorithm = new EdmondKarpMaxFlow<char, int>(new EdmondKarpOperators());

            var result = algorithm.ComputeMaxFlow(graph, 'S', 'T');

            Assert.AreEqual(result, 19);
        }

        /// <summary>
        ///     EdmondKarp Max Flow test
        /// </summary>
        [TestMethod]
        public void EdmondKarp_AdjacencyMatrixGraph_Smoke_Test()
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

            var algorithm = new EdmondKarpMaxFlow<char, int>(new EdmondKarpOperators());

            var result = algorithm.ComputeMaxFlow(graph, 'S', 'T');

            Assert.AreEqual(result, 19);
        }

        [TestMethod]
        public void EdmondKarp_Oracle_Matches_FordFulkerson_And_PushRelabel()
        {
            var graph = new WeightedDiGraph<char, int>();
            foreach (var v in "SABCDT") graph.AddVertex(v);
            graph.AddEdge('S', 'A', 10);
            graph.AddEdge('S', 'C', 10);
            graph.AddEdge('A', 'B', 4);
            graph.AddEdge('A', 'C', 2);
            graph.AddEdge('A', 'D', 8);
            graph.AddEdge('B', 'T', 10);
            graph.AddEdge('C', 'D', 9);
            graph.AddEdge('D', 'B', 6);
            graph.AddEdge('D', 'T', 10);

            var op = new EdmondKarpOperators();
            var ek = new EdmondKarpMaxFlow<char, int>(op).ComputeMaxFlow(graph, 'S', 'T');
            var ff = new FordFulkersonMaxFlow<char, int>(op).ComputeMaxFlow(graph, 'S', 'T');
            var pr = new PushRelabelMaxFlow<char, int>(op).ComputeMaxFlow(graph, 'S', 'T');
            Assert.AreEqual(19, ek);
            Assert.AreEqual(ek, ff);
            Assert.AreEqual(ek, pr);
        }

        [TestMethod]
        public void EdmondKarp_Antiparallel_Edges()
        {
            var graph = new WeightedDiGraph<char, int>();
            graph.AddVertex('S');
            graph.AddVertex('T');
            graph.AddEdge('S', 'T', 5);
            graph.AddEdge('T', 'S', 3);

            Assert.AreEqual(5, new EdmondKarpMaxFlow<char, int>(new EdmondKarpOperators())
                .ComputeMaxFlow(graph, 'S', 'T'));
        }

        /// <summary>
        ///     operators for generics
        ///     implemented for int type for edge weights
        /// </summary>
        public class EdmondKarpOperators : IFlowOperators<int>
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