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

            var vertex_1 = graph.AddVertex(1);
            var vertex_2 = graph.AddVertex(2);
            var vertex_3 = graph.AddVertex(3);
            var vertex_4 = graph.AddVertex(4);
            var vertex_5 = graph.AddVertex(5);

            graph.AddEdge(vertex_1, vertex_2);
            graph.AddEdge(vertex_2, vertex_3);
            graph.AddEdge(vertex_3, vertex_4);
            graph.AddEdge(vertex_4, vertex_5);
            graph.AddEdge(vertex_4, vertex_1);
            graph.AddEdge(vertex_3, vertex_5);

            Assert.AreEqual(5, graph.VerticesCount);

            Assert.IsTrue(graph.HasEdge(vertex_1, vertex_2));

            graph.RemoveEdge(vertex_1, vertex_2);

            Assert.IsFalse(graph.HasEdge(vertex_1, vertex_2));

            graph.RemoveEdge(vertex_2, vertex_3);
            graph.RemoveEdge(vertex_3, vertex_4);
            graph.RemoveEdge(vertex_4, vertex_5);
            graph.RemoveEdge(vertex_4, vertex_1);

            Assert.IsTrue(graph.HasEdge(vertex_3, vertex_5));
            graph.RemoveEdge(vertex_3, vertex_5);
            Assert.IsFalse(graph.HasEdge(vertex_3, vertex_5));

            graph.RemoveVertex(vertex_1);
            graph.RemoveVertex(vertex_2);
            graph.RemoveVertex(vertex_3);
            graph.RemoveVertex(vertex_4);
            graph.RemoveVertex(vertex_5);

            Assert.AreEqual(0, graph.VerticesCount);
        }
    }
}
