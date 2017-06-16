using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.ArticulationPoint;
using Algorithm.Sandbox.GraphAlgorithms.Connectivity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.Connectivity
{

    [TestClass]
    public class TarjansBiConnected_Tests
    {

        [TestMethod]
        public void TarjanIsBiConnected_Smoke_Test()
        {
            var graph = new AsGraph<char>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');

            graph.AddEdge('A', 'B');
            graph.AddEdge('A', 'C');
            graph.AddEdge('B', 'C');

         
            var algo = new TarjansBiConnected<char>();

            var result = algo.IsBiConnected(graph);

            Assert.IsTrue(result);

            graph.AddVertex('D');
            graph.AddVertex('E');
            graph.AddVertex('F');
            graph.AddVertex('G');
            graph.AddVertex('H');

            graph.AddEdge('C', 'D');
            graph.AddEdge('D', 'E');

            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');
            graph.AddEdge('G', 'E');

            graph.AddEdge('F', 'H');

            result = algo.IsBiConnected(graph);

            Assert.IsFalse(result);

        }
    }
}
