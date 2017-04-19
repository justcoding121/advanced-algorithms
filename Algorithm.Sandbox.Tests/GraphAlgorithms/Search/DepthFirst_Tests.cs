using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.Search
{
    [TestClass]
    public class DepthFirst_Tests
    {

        [TestMethod]
        public void DepthFirst_Smoke_Test()
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

            var algo = new DepthFirst<char>();

            Assert.IsTrue(algo.Find(graph, 'D'));

            Assert.IsFalse(algo.Find(graph, 'M'));

        }


    }
}
