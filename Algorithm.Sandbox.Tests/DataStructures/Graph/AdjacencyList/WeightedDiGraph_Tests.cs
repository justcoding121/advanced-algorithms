using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DataStructures.Graph.AdjacencyList
{
    [TestClass]
    public class WeightedDiGraph_Tests
    {
        /// <summary>
        /// key value dictionary tests 
        /// </summary>
        [TestMethod]
        public void WeightedDiGraph_Smoke_Test()
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

            Assert.AreEqual(5, graph.VerticesCount);

            Assert.IsTrue(graph.HasEdge(1, 2));

            graph.RemoveEdge(1, 2);

            Assert.IsFalse(graph.HasEdge(1, 2));

            graph.RemoveEdge(2, 3);
            graph.RemoveEdge(3, 4);
            graph.RemoveEdge(4, 5);
            graph.RemoveEdge(4, 1);

            Assert.IsTrue(graph.HasEdge(3, 5));
            graph.RemoveEdge(3, 5);
            Assert.IsFalse(graph.HasEdge(3, 5));

            graph.RemoveVertex(1);
            graph.RemoveVertex(2);
            graph.RemoveVertex(3);
            graph.RemoveVertex(4);
            graph.RemoveVertex(5);

            Assert.AreEqual(0, graph.VerticesCount);
        }
    }
}
