using System;
using System.Linq;
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
    }
}
