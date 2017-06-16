using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms;
using Algorithm.Sandbox.GraphAlgorithms.ShortestPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.ShortestPath
{
    [TestClass]
    public class FloydWarshalls_Tests
    {
        [TestMethod]
        public void FloydWarshall_Smoke_Test()
        {
            var graph = new AsWeightedGraph<char, int>();

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

            var algo = new FloydWarshallShortestPath<char, int>(new FloydWarshallShortestPathOperators());

            var result = algo.GetAllPairShortestPaths(graph);

            var testCase = result.First(x => x.Source == 'S' && x.Destination == 'T');
            Assert.AreEqual(15, testCase.Distance);

            var expectedPath = new char[] { 'S', 'A', 'C', 'D', 'B', 'T' };
            for (int i = 0; i < expectedPath.Length; i++)
            {
                Assert.AreEqual(expectedPath[i], testCase.Path[i]);
            }

            testCase = result.First(x => x.Source == 'T' && x.Destination == 'S');
            Assert.AreEqual(15, testCase.Distance);

            expectedPath = new char[] { 'T', 'B', 'D', 'C', 'A', 'S' };
            for (int i = 0; i < expectedPath.Length; i++)
            {
                Assert.AreEqual(expectedPath[i], testCase.Path[i]);
            }

        }

        /// <summary>
        /// generic operations for int type
        /// </summary>
        public class FloydWarshallShortestPathOperators : IShortestPathOperators<int>
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
