using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests
{
    /// <summary>
    /// Problem details below
    /// https://en.wikipedia.org/wiki/Travelling_salesman_problem
    /// </summary>
    [TestClass]
    public class TravellingSalesman_Tests
    {
        [TestMethod]
        public void SmokeTest()
        {
            var graph = new AsWeightedDiGraph<int, int>();

            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);

            graph.AddEdge(0, 1, 1);
            graph.AddEdge(0, 2, 15);
            graph.AddEdge(0, 3, 6);

            graph.AddEdge(1, 0, 2);
            graph.AddEdge(1, 2, 7);
            graph.AddEdge(1, 3, 3);

            graph.AddEdge(2, 0, 9);
            graph.AddEdge(2, 1, 6);
            graph.AddEdge(2, 3, 12);

            graph.AddEdge(3, 0, 10);
            graph.AddEdge(3, 1, 4);
            graph.AddEdge(3, 2, 8);

            Assert.AreEqual(21, TravellingSalesman.GetMinWeight(graph));
        }
    }
}
