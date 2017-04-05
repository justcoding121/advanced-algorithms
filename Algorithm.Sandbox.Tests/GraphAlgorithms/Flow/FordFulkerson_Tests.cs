using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.Flow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.Flow
{
    [TestClass]
    public class FordFulkerson_Tests
    {
        /// <summary>
        /// FordFulkerson Max Flow test
        /// </summary>
        [TestMethod]
        public void FordFulkerson_Smoke_Test()
        {
            var graph = new AsWeightedDiGraph<int, int>();

            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            graph.AddEdge(1, 2, 1);
            graph.AddEdge(2, 3, 2);
            graph.AddEdge(3, 4, 3);
            graph.AddEdge(4, 5, 1);
            graph.AddEdge(4, 1, 6);
            graph.AddEdge(3, 5, 4);

            var algo = new FordFulkersonMaxFlow<int, int>(new FordFulkersonOperators());

            var result = algo.ComputeMaxFlow(graph, 1, 5);
        }

        public class FordFulkersonOperators : IFordFulkersonOperators<int>
        {
            public int Add(int a, int b)
            {
                return a + b;
            }

            public int defaultWeight()
            {
                return 0;
            }

            public int MaxWeight()
            {
                return int.MaxValue;
            }

            public int Substract(int a, int b)
            {
                return a - b;
            }
        }
    }
}
