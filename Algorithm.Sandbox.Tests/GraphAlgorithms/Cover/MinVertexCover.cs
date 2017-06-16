using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.Coloring;
using Algorithm.Sandbox.GraphAlgorithms.Cover;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.GraphAlgorithms.Coloring
{
    [TestClass]
    public class MinVertexCover_Tests
    {

        [TestMethod]
        public void MinVertexCover_Smoke_Test()
        {
            var graph = new AsGraph<int>();

            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(0, 4);

            var algo = new MinVertexCover<int>();

            var result = algo.GetMinVertexCover(graph);

            Assert.IsTrue(result.Count() <= 2);

            graph.RemoveEdge(0, 4);

            graph.AddEdge(1, 4);

            result = algo.GetMinVertexCover(graph);
            Assert.IsTrue(result.Count() <= 4);
        }
    }
}
