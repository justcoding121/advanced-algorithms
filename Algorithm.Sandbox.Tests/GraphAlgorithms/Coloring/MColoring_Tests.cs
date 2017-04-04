using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.Coloring;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.Coloring
{
    [TestClass]
    public class MColoring_Tests
    {
        /// <summary>
        /// Gets the minimum number of coins to fit in the amount 
        /// </summary>
        [TestMethod]
        public void Smoke_MColoring_Test()
        {
            var graph = new AsGraph<int>();

            var vertex_0 = graph.AddVertex(0);
            var vertex_1 = graph.AddVertex(1);
            var vertex_2 = graph.AddVertex(2);
            var vertex_3 = graph.AddVertex(3);

            graph.AddEdge(vertex_0, vertex_1);
            graph.AddEdge(vertex_0, vertex_2);
            graph.AddEdge(vertex_0, vertex_3);
            graph.AddEdge(vertex_1, vertex_2);
            graph.AddEdge(vertex_2, vertex_3);

            var algo = new MColoring<int, string>();

            var result = algo.CanColor(graph, new string[] { "red", "green", "blue" });

            Assert.IsTrue(result);
        }
    }
}
