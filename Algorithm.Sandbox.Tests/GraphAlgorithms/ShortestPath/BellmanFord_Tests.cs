using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.ShortestPath
{
    [TestClass]
    public class BellmanFord_Tests
    {
        [TestMethod]
        public void Smoke_Test_BellmanFord()
        {
            var graph = new AsWeightedDiGraph<char, int>();

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

            var algo = new BellmanFordShortestPath<char, int>(new BellmanFordShortestPathOperators());

            var result = algo.GetShortestPath(graph, 'S', 'T');

            Assert.AreEqual(4, result.Length);

            var expectedPath = new char[] { 'S', 'A', 'B', 'T' };
            for (int i = 0; i < expectedPath.Length; i++)
            {
                Assert.AreEqual(expectedPath[i], result.Path[i]);
            }

        }

        /// <summary>
        /// generic operations for int type
        /// </summary>
        public class BellmanFordShortestPathOperators : IShortestPathOperators<int>
        {
            public int DefaultValue
            {
                get
                {
                    return 0;
                }
            }

            public int MaxValue
            {
                get
                {
                    return int.MaxValue;
                }
            }

            public int Sum(int a, int b)
            {
                return checked(a + b);
            }
        }
    }
}
