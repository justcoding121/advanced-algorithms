using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class MinCut_Tests
    {
        /// <summary>
        ///     Min Cut test
        /// </summary>
        [TestMethod]
        public void MinCut_AdjacencyListGraph_Smoke_Test_1()
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

            var algorithm = new MinCut<char, int>(new EdmondKarpOperators());

            var result = algorithm.ComputeMinCut(graph, 'S', 'T');

            Assert.AreEqual(result.Count, 2);
        }


        /// <summary>
        ///     Min Cut test
        /// </summary>
        [TestMethod]
        public void MinCut_AdjacencyListGraph_Smoke_Test_2()
        {
            var graph = new WeightedDiGraph<char, int>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('E');
            graph.AddVertex('F');
            graph.AddVertex('G');
            graph.AddVertex('H');
            graph.AddVertex('I');
            graph.AddVertex('J');


            graph.AddEdge('A', 'B', 1);
            graph.AddEdge('A', 'C', 1);
            graph.AddEdge('A', 'D', 1);

            graph.AddEdge('B', 'E', 1);
            graph.AddEdge('C', 'E', 1);
            graph.AddEdge('D', 'E', 1);

            graph.AddEdge('E', 'F', 1);

            graph.AddEdge('F', 'G', 1);
            graph.AddEdge('F', 'H', 1);
            graph.AddEdge('F', 'I', 1);

            graph.AddEdge('G', 'J', 1);
            graph.AddEdge('H', 'J', 1);
            graph.AddEdge('I', 'J', 1);


            var algorithm = new MinCut<char, int>(new EdmondKarpOperators());

            var result = algorithm.ComputeMinCut(graph, 'A', 'J');

            Assert.AreEqual(result.Count, 1);
        }

        /// <summary>
        ///     Min Cut test
        /// </summary>
        [TestMethod]
        public void MinCut_AdjacencyListMatrix_Smoke_Test_1()
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

            var algorithm = new MinCut<char, int>(new EdmondKarpOperators());

            var result = algorithm.ComputeMinCut(graph, 'S', 'T');

            Assert.AreEqual(result.Count, 2);
        }


        /// <summary>
        ///     Min Cut test
        /// </summary>
        [TestMethod]
        public void MinCut_AdjacencyMatrixGraph_Smoke_Test_2()
        {
            var graph = new Algorithms.DataStructures.Graph.AdjacencyMatrix.WeightedDiGraph<char, int>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('E');
            graph.AddVertex('F');
            graph.AddVertex('G');
            graph.AddVertex('H');
            graph.AddVertex('I');
            graph.AddVertex('J');


            graph.AddEdge('A', 'B', 1);
            graph.AddEdge('A', 'C', 1);
            graph.AddEdge('A', 'D', 1);

            graph.AddEdge('B', 'E', 1);
            graph.AddEdge('C', 'E', 1);
            graph.AddEdge('D', 'E', 1);

            graph.AddEdge('E', 'F', 1);

            graph.AddEdge('F', 'G', 1);
            graph.AddEdge('F', 'H', 1);
            graph.AddEdge('F', 'I', 1);

            graph.AddEdge('G', 'J', 1);
            graph.AddEdge('H', 'J', 1);
            graph.AddEdge('I', 'J', 1);


            var algorithm = new MinCut<char, int>(new EdmondKarpOperators());

            var result = algorithm.ComputeMinCut(graph, 'A', 'J');

            Assert.AreEqual(result.Count, 1);
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

            public int defaultWeight => 0;

            public int MaxWeight => int.MaxValue;

            public int SubstractWeights(int a, int b)
            {
                return checked(a - b);
            }
        }
    }
}