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
    public class BiDirectional_Tests
    {

        [TestMethod]
        public void BiDirectional_Smoke_Test()
        {
            var graph = new AsDiGraph<char>();

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

            var algo = new BiDirectional<char>();

            Assert.IsTrue(algo.PathExists(graph, 'A', 'I'));

            graph.RemoveEdge('D', 'E');
            graph.AddEdge('E', 'D');

            Assert.IsFalse(algo.PathExists(graph, 'A', 'I'));

        }


    }
}
