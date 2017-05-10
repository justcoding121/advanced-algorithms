using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.ArticulationPoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.ArticulationPoints
{

    [TestClass]
    public class TarjansArticulation_Tests
    {
       
        [TestMethod]
        public void Smoke_TarjanArticulation_Test()
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


            graph.AddEdge('A', 'B');
            graph.AddEdge('A', 'C');
            graph.AddEdge('B', 'C');

            graph.AddEdge('C', 'D');
            graph.AddEdge('D', 'E');

            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');
            graph.AddEdge('G', 'E');

            graph.AddEdge('F', 'H');

            var algo = new TarjansArticulationFinder<char>();

            var result = algo.FindArticulationPoints(graph);

            Assert.AreEqual(4, result.Count);

            var expectedResult = new char[] { 'C', 'D', 'E', 'F' };

            foreach(var v in result)
            {
                Assert.IsTrue(expectedResult.Contains(v));
            }
        }
    }
}
