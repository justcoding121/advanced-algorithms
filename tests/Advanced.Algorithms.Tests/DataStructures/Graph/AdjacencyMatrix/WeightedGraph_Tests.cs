using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures.Graph.AdjacencyMatrix
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
            graph.AddEdge(3, 5, 6);

            Assert.AreEqual(2, graph.Edges(2).Count());

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
        public void WeightedGraph_Empty_Missing_Edge_Enumeration()
        {
            var graph = new WeightedGraph<int, int>();

            Assert.AreEqual(0, graph.VerticesCount);
            Assert.AreEqual(0, graph.Count());
            Assert.ThrowsException<InvalidOperationException>(() => { var _ = graph.ReferenceVertex; });
            Assert.IsFalse(graph.ContainsVertex(1));
            Assert.ThrowsException<ArgumentException>(() => graph.HasEdge(1, 2));
            Assert.ThrowsException<ArgumentException>(() => graph.Edges(1).ToList());
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveVertex(1));
            Assert.ThrowsException<ArgumentException>(() =>
            {
                graph.AddVertex(1);
                graph.AddEdge(1, 1, 0);
            });

            var g = new WeightedGraph<int, int>();
            g.AddVertex(1);
            g.AddVertex(2);
            Assert.AreEqual(2, g.Count());
            Assert.AreEqual(0, g.Edges(1).Count());

            g.AddEdge(1, 2, 7);
            Assert.IsTrue(g.HasEdge(1, 2));
            Assert.AreEqual(1, g.Edges(1).Count());
            Assert.AreEqual(7, g.Edges(1).First().Value);
            Assert.ThrowsException<InvalidOperationException>(() => g.AddEdge(1, 2, 8));

            g.RemoveEdge(1, 2);
            Assert.IsFalse(g.HasEdge(1, 2));
            Assert.ThrowsException<InvalidOperationException>(() => g.RemoveEdge(1, 2));
        }

        [TestMethod]
        public void WeightedGraph_Resize_Clone_And_VertexApi()
        {
            var graph = new WeightedGraph<int, int>();

            Assert.IsTrue(graph.IsWeightedGraph);
            Assert.ThrowsException<ArgumentException>(() => graph.EdgeCount(1));
            Assert.ThrowsException<ArgumentException>(() => graph.AddEdge(1, 2, 1));
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveEdge(1, 2));

            var stringGraph = new WeightedGraph<string, int>();
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.AddVertex(null));
            Assert.ThrowsException<ArgumentException>(() => stringGraph.HasEdge(null, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.RemoveVertex(null));

            for (var i = 1; i <= 9; i++)
            {
                graph.AddVertex(i);
            }

            Assert.ThrowsException<ArgumentException>(() => graph.AddVertex(1));
            Assert.ThrowsException<ArgumentException>(() => graph.AddEdge(1, 2, 0));

            graph.AddEdge(1, 2, 5);
            graph.AddEdge(2, 3, 7);
            graph.AddEdge(8, 9, 3);
            Assert.AreEqual(1, graph.EdgeCount(1));
            Assert.AreEqual(2, graph.EdgeCount(2));

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(1, 2, 9));
            Assert.ThrowsException<InvalidOperationException>(() => graph.RemoveEdge(1, 9));

            var vertex = graph.GetVertex(2);
            Assert.AreEqual(2, vertex.Key);
            Assert.AreEqual(5, vertex.GetEdge(graph.GetVertex(1)).Weight<int>());
            Assert.AreEqual(2, vertex.Edges.Count());
            Assert.AreEqual(9, graph.VerticesAsEnumberable.Count());

            var loopGraph = new WeightedGraph<int, int>();
            loopGraph.AddVertex(1);
            loopGraph.AddEdge(1, 1, 9);
            var loopClone = loopGraph.Clone();
            Assert.IsTrue(loopClone.HasEdge(1, 1));
            Assert.AreEqual(9, loopClone.Edges(1).First().Value);

            var vertexOnly = new WeightedGraph<int, int>();
            vertexOnly.AddVertex(1);
            vertexOnly.AddVertex(2);
            var vertexClone = vertexOnly.Clone();
            Assert.AreEqual(2, vertexClone.VerticesCount);
            Assert.IsFalse(vertexClone.HasEdge(1, 2));

            for (var i = 9; i >= 5; i--)
            {
                graph.RemoveVertex(i);
            }

            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.IsTrue(graph.HasEdge(2, 3));
            Assert.AreEqual(4, graph.VerticesCount);

            graph.RemoveVertex(4);
            graph.RemoveVertex(3);
            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.AreEqual(2, graph.VerticesCount);

            graph.RemoveVertex(2);
            graph.RemoveVertex(1);
            Assert.AreEqual(0, graph.VerticesCount);
            Assert.AreEqual(0, graph.Clone().VerticesCount);
        }

        [TestMethod]
        public void WeightedGraph_Adversarial_Vertex0_Resize_Clone_Oracle()
        {
            var graph = new WeightedGraph<int, int>();
            for (var i = 0; i < 5; i++)
            {
                graph.AddVertex(i);
            }

            graph.AddEdge(0, 1, 5);
            graph.AddEdge(3, 4, 11);
            graph.AddEdge(0, 0, 3);

            Assert.IsTrue(graph.ContainsVertex(0));
            CollectionAssert.AreEquivalent(new[] { 0, 1, 2, 3, 4 }, graph.ToList());
            Assert.AreEqual(3, graph.Edges(0).Single(e => e.Key.Equals(0)).Value);

            // shrink remaps matrix indices; GetEdge must follow live indices
            var v3 = graph.GetVertex(3);
            var v4 = graph.GetVertex(4);
            graph.RemoveVertex(0);
            graph.RemoveVertex(1);
            Assert.AreEqual(11, v3.GetEdge(v4).Weight<int>());
            Assert.AreEqual(11, graph.GetVertex(3).GetEdge(graph.GetVertex(4)).Weight<int>());
            Assert.IsTrue(graph.HasEdge(3, 4));

            var g2 = new WeightedGraph<int, int>();
            g2.AddVertex(0);
            g2.AddVertex(1);
            g2.AddEdge(0, 1, 5);
            g2.AddEdge(0, 0, 3);
            var clone = g2.Clone();
            Assert.IsTrue(clone.HasEdge(0, 1));
            Assert.IsTrue(clone.HasEdge(1, 0));
            Assert.IsTrue(clone.HasEdge(0, 0));
            Assert.AreEqual(5, clone.Edges(0).Single(e => e.Key.Equals(1)).Value);
            Assert.ThrowsException<ArgumentException>(() => g2.HasEdge(9, 1));
        }
    }
}
