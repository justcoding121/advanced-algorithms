using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures.Graph.AdjacencyList
{
    [TestClass]
    public class WeightedDiGraphTests
    {
        /// <summary>
        ///     key value dictionary tests
        /// </summary>
        [TestMethod]
        public void WeightedDiGraph_Smoke_Test()
        {
            var graph = new WeightedDiGraph<int, int>();

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

            Assert.AreEqual(2, graph.OutEdges(4).Count());
            Assert.AreEqual(2, graph.InEdges(5).Count());

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
        public void WeightedDiGraph_Empty_Missing_SelfLoop_Enumeration()
        {
            var graph = new WeightedDiGraph<int, int>();

            Assert.AreEqual(0, graph.VerticesCount);
            Assert.AreEqual(0, graph.Count());
            Assert.IsFalse(graph.ContainsVertex(1));
            Assert.ThrowsException<ArgumentException>(() => graph.HasEdge(1, 2));
            Assert.ThrowsException<ArgumentException>(() => graph.OutEdges(1).ToList());
            Assert.ThrowsException<ArgumentException>(() => graph.InEdges(1).ToList());
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveVertex(1));

            graph.AddVertex(1);
            graph.AddVertex(2);
            Assert.AreEqual(2, graph.Count());
            Assert.AreEqual(1, graph.GetVertex(1).Key);

            graph.AddEdge(1, 1, 3);
            Assert.IsTrue(graph.HasEdge(1, 1));
            Assert.AreEqual(1, graph.OutEdges(1).Count());
            Assert.AreEqual(1, graph.InEdges(1).Count());
            Assert.AreEqual(3, graph.OutEdges(1).First().Item2);
            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(1, 1, 4));

            graph.AddEdge(1, 2, 5);
            var clone = graph.Clone();
            Assert.AreEqual(2, clone.VerticesCount);
            Assert.IsTrue(clone.HasEdge(1, 1));
            Assert.IsTrue(clone.HasEdge(1, 2));
            Assert.IsFalse(clone.HasEdge(2, 1));
        }

        [TestMethod]
        public void WeightedDiGraph_Clone_And_VertexApi()
        {
            var graph = new WeightedDiGraph<int, int>();

            Assert.IsTrue(graph.IsWeightedGraph);
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveEdge(1, 2));
            Assert.ThrowsException<ArgumentException>(() => graph.AddEdge(1, 2, 1));

            var stringGraph = new WeightedDiGraph<string, int>();
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.AddVertex(null));
            Assert.ThrowsException<ArgumentException>(() => stringGraph.AddEdge(null, "a", 1));
            Assert.ThrowsException<ArgumentException>(() => stringGraph.RemoveEdge(null, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.RemoveVertex(null));

            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(1, 2, 5);
            graph.AddEdge(2, 3, 7);
            graph.AddEdge(3, 1, 9);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(1, 2, 1));
            Assert.ThrowsException<InvalidOperationException>(() => graph.RemoveEdge(2, 1));
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveEdge(1, 99));

            var vertex = graph.GetVertex(2);
            Assert.AreEqual(2, vertex.Key);
            Assert.AreEqual(1, vertex.OutEdgeCount);
            Assert.AreEqual(1, vertex.InEdgeCount);
            Assert.AreEqual(7, vertex.GetOutEdge(graph.GetVertex(3)).Weight<int>());
            Assert.AreEqual(1, vertex.OutEdges.Count());
            Assert.AreEqual(1, vertex.InEdges.Count());
            Assert.AreEqual(1, vertex.OutEdgeCount);
            Assert.AreEqual(1, ((IEnumerable<int>)vertex).Count());
            Assert.AreEqual(3, graph.VerticesAsEnumberable.Count());
            Assert.AreEqual(1, graph.OutEdges(2).Count());
            Assert.AreEqual(1, graph.InEdges(2).Count());

            graph.RemoveVertex(3);
            Assert.IsFalse(graph.ContainsVertex(3));
            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.AreEqual(2, graph.VerticesCount);
            Assert.AreEqual(0, new WeightedDiGraph<int, int>().Clone().VerticesCount);
        }

        [TestMethod]
        public void WeightedDiGraph_Adversarial_Vertex0_Clone_Oracle()
        {
            var graph = new WeightedDiGraph<int, int>();
            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddEdge(0, 1, 5);
            graph.AddEdge(1, 2, 7);
            graph.AddEdge(0, 0, 3);
            graph.AddEdge(2, 0, 4);

            Assert.IsTrue(graph.ContainsVertex(0));
            CollectionAssert.AreEquivalent(new[] { 0, 1, 2 }, graph.ToList());
            Assert.AreEqual(2, graph.OutEdges(0).Count());
            Assert.AreEqual(2, graph.InEdges(0).Count());
            Assert.AreEqual(3, graph.OutEdges(0).Single(e => e.Item1.Equals(0)).Item2);

            var clone = graph.Clone();
            Assert.AreEqual(3, clone.VerticesCount);
            Assert.IsTrue(clone.HasEdge(0, 1));
            Assert.IsFalse(clone.HasEdge(1, 0));
            Assert.IsTrue(clone.HasEdge(0, 0));
            Assert.AreEqual(5, clone.OutEdges(0).Single(e => e.Item1.Equals(1)).Item2);
            Assert.AreEqual(4, clone.InEdges(0).Single(e => e.Item1.Equals(2)).Item2);

            graph.RemoveEdge(2, 0);
            graph.RemoveVertex(2);
            Assert.ThrowsException<ArgumentException>(() => graph.OutEdges(2).ToList());
            Assert.IsTrue(clone.HasEdge(2, 0));
        }
    }
}
