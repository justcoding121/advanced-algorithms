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
    public class BreadFirst_Tests
    {

        [TestMethod]
        public void BreadthFirst_Smoke_Test()
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

            graph.AddEdge('A', 'B');
            graph.AddEdge('B', 'C');
            graph.AddEdge('C', 'D');
            graph.AddEdge('D', 'E');
            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');
            graph.AddEdge('G', 'H');
            graph.AddEdge('H', 'I');

            var algo = new BreadthFirst<char>();

            Assert.IsTrue(algo.Find(graph, 'D'));

            Assert.IsFalse(algo.Find(graph, 'M'));

        }


    }
}
