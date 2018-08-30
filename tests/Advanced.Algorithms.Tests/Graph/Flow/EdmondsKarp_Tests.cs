using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class EdmondKarp_Tests
    {
        /// <summary>
        /// EdmondKarp Max Flow test
        /// </summary>
        [TestMethod]
        public void EdmondKarp_Smoke_Test()
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

            var algo = new EdmondKarpMaxFlow<char, int>(new EdmondKarpOperators());

            var result = algo.ComputeMaxFlow(graph, 'S', 'T');

            Assert.AreEqual(result, 19);
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
