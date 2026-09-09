using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures.Graph.AdjacencyList
{
    [TestClass]
    public class WeightedGraphTests
    {
        /// <summary>
        ///     key value dictionary tests
        /// </summary>
        [TestMethod]
        public void WeightedGraph_Smoke_Test()
        {
            var graph = new WeightedGraph<int, int>();

            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            graph.AddEdge(1, 2, 1);
            graph.AddEdge(2, 3, 2);
            graph.AddEdge(3, 4, 4);
            graph.AddEdge(4, 5, 5);
            graph.AddEdge(4, 1, 1);
            graph.AddEdge(3, 5, 0);

            Assert.AreEqual(3, graph.GetAllEdges(4).Count);
            Assert.AreEqual(2, graph.GetAllEdges(5).Count);

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

        [TestMethod]
        public void WeightedGraph_Empty_Missing_SelfLoop_Enumeration()
        {
            var graph = new WeightedGraph<int, int>();

            Assert.AreEqual(0, graph.VerticesCount);
            Assert.AreEqual(0, graph.Count());
            Assert.IsFalse(graph.ContainsVertex(1));
            Assert.ThrowsException<ArgumentException>(() => graph.HasEdge(1, 2));
            Assert.ThrowsException<ArgumentException>(() => graph.GetAllEdges(1));
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveVertex(1));

            graph.AddVertex(1);
            graph.AddVertex(2);
            Assert.AreEqual(2, graph.Count());
            Assert.AreEqual(1, graph.GetVertex(1).Key);
            Assert.AreEqual(0, graph.GetAllEdges(1).Count);

            graph.AddEdge(1, 2, 7);
            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.AreEqual(1, graph.GetAllEdges(1).Count);
            Assert.AreEqual(7, graph.GetAllEdges(1)[0].Item2);

            graph.RemoveEdge(1, 2);
            Assert.IsFalse(graph.HasEdge(1, 2));
            Assert.ThrowsException<InvalidOperationException>(() => graph.RemoveEdge(1, 2));
        }

        [TestMethod]
        public void WeightedGraph_Clone_And_VertexApi()
        {
            var graph = new WeightedGraph<int, int>();

            Assert.IsTrue(graph.IsWeightedGraph);
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveEdge(1, 2));
            Assert.ThrowsException<ArgumentException>(() => graph.AddEdge(1, 2, 1));

            var stringGraph = new WeightedGraph<string, int>();
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.AddVertex(null));
            Assert.ThrowsException<ArgumentException>(() => stringGraph.AddEdge(null, "a", 1));
            Assert.ThrowsException<ArgumentException>(() => stringGraph.RemoveEdge(null, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.RemoveVertex(null));

            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(1, 2, 5);
            graph.AddEdge(2, 3, 7);

            Assert.ThrowsException<InvalidOperationException>(() => graph.RemoveEdge(1, 3));
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveEdge(1, 99));

            var vertex = graph.GetVertex(2);
            Assert.AreEqual(2, vertex.Key);
            Assert.AreEqual(5, vertex.GetEdge(graph.GetVertex(1)).Weight<int>());
            Assert.AreEqual(2, vertex.Edges.Count());
            Assert.AreEqual(3, graph.VerticesAsEnumberable.Count());
            Assert.AreEqual(2, graph.GetAllEdges(2).Count);

            // vertices-only clone avoids undirected double-add of edges
            var vertexOnly = new WeightedGraph<int, int>();
            vertexOnly.AddVertex(1);
            vertexOnly.AddVertex(2);
            var vertexClone = vertexOnly.Clone();
            Assert.AreEqual(2, vertexClone.VerticesCount);
            Assert.IsFalse(vertexClone.HasEdge(1, 2));
            Assert.AreEqual(0, new WeightedGraph<int, int>().Clone().VerticesCount);

            graph.RemoveVertex(3);
            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.AreEqual(2, graph.VerticesCount);
        }

        [TestMethod]
        public void WeightedGraph_Adversarial_Vertex0_SelfLoop_Clone_Oracle()
        {
            var graph = new WeightedGraph<int, int>();
            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddEdge(0, 1, 5);
            graph.AddEdge(1, 2, 7);
            graph.AddEdge(0, 0, 3);

            Assert.IsTrue(graph.ContainsVertex(0));
            CollectionAssert.AreEquivalent(new[] { 0, 1, 2 }, graph.ToList());
            Assert.AreEqual(2, graph.GetAllEdges(0).Count);
            Assert.AreEqual(3, graph.GetAllEdges(0).Single(e => e.Item1.Equals(0)).Item2);
            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(0, 1, 9));

            var clone = graph.Clone();
            Assert.AreEqual(3, clone.VerticesCount);
            Assert.IsTrue(clone.HasEdge(0, 1));
            Assert.IsTrue(clone.HasEdge(1, 0));
            Assert.IsTrue(clone.HasEdge(0, 0));
            Assert.AreEqual(5, clone.GetAllEdges(0).Single(e => e.Item1.Equals(1)).Item2);
            Assert.AreEqual(3, clone.GetAllEdges(0).Single(e => e.Item1.Equals(0)).Item2);

            graph.RemoveEdge(0, 0);
            Assert.IsFalse(graph.HasEdge(0, 0));
            Assert.IsTrue(clone.HasEdge(0, 0));
            graph.RemoveVertex(0);
            Assert.ThrowsException<ArgumentException>(() => graph.GetAllEdges(0));
            Assert.IsTrue(clone.ContainsVertex(0));
        }
    }
}
