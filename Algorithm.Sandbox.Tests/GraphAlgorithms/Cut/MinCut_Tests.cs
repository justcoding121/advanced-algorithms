using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.Cut;
using Algorithm.Sandbox.GraphAlgorithms.Flow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.Flow
{
    [TestClass]
    public class MinCut_Tests
    {
        /// <summary>
        /// Min Cut test
        /// </summary>
        [TestMethod]
        public void MinCut_Smoke_Test_1()
        {
            var graph = new AsWeightedDiGraph<char, int>();

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

            var algo = new MinCut<char, int>(new EdmondKarpOperators());

            var result = algo.ComputeMinCut(graph, 'S', 'T');

            Assert.AreEqual(result.Length, 2);
        }


        /// <summary>
        /// Min Cut test
        /// </summary>
        [TestMethod]
        public void MinCut_Smoke_Test_2()
        {
            var graph = new AsWeightedDiGraph<char, int>();

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


            var algo = new MinCut<char, int>(new EdmondKarpOperators());

            var result = algo.ComputeMinCut(graph, 'A', 'J');

            Assert.AreEqual(result.Length, 1);
        }
        /// <summary>
        /// operators for generics
        /// implemented for int type for edge weights
        /// </summary>
        public class EdmondKarpOperators : IFlowOperators<int>
        {
            public int AddWeights(int a, int b)
            {
                return checked(a + b);
            }

            public int defaultWeight
            {
                get
                {
                    return 0;
                }
            }

            public int MaxWeight
            {
                get
                {
                    return int.MaxValue;
                }
            }

            public int SubstractWeights(int a, int b)
            {
                return checked(a - b);
            }
        }
    }
}
