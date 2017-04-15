using System;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.Flow;
using Algorithm.Sandbox.GraphAlgorithms.Matching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.Matching
{
    [TestClass]
    public class HopcroftKarp_Tests
    {
        /// <summary>
        /// Test Max BiParitite Edges using Ford-Fukerson algorithm
        /// </summary>
        [TestMethod]
        public void HopcroftKarp_Smoke_Test()
        {

            var graph = new AsGraph<char>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('E');

            graph.AddVertex('F');
            graph.AddVertex('G');
            graph.AddVertex('H');
            graph.AddVertex('I');

            graph.AddEdge('A', 'F');
            graph.AddEdge('B', 'F');
            graph.AddEdge('B', 'G');
            graph.AddEdge('C', 'H');
            graph.AddEdge('C', 'I');
            graph.AddEdge('D', 'G');
            graph.AddEdge('D', 'H');
            graph.AddEdge('E', 'F');
            graph.AddEdge('E', 'I');

            var algo = new HopcroftKarpMatching<char>(null);

            var result = algo.GetMaxBiPartiteMatching(graph);

            Assert.AreEqual(result.Length, 4);

        }


    }
}
