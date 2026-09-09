using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures.Graph.AdjacencyList
{
    [TestClass]
    public class GraphTests
    {
        /// <summary>
        ///     key value dictionary tests
        /// </summary>
        [TestMethod]
        public void Graph_Smoke_Test()
        {
            var graph = new Graph<int>();

            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 1);
            graph.AddEdge(3, 5);

            Assert.AreEqual(3, graph.Edges(4).Count());

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

            graph.AddEdge(5, 5);
            graph.RemoveEdge(5, 5);
            graph.RemoveVertex(5);

            Assert.AreEqual(0, graph.VerticesCount);
        }

        [TestMethod]
        public void Graph_Empty_Missing_SelfLoop_Enumeration()
        {
            var graph = new Graph<int>();

            Assert.AreEqual(0, graph.VerticesCount);
            Assert.AreEqual(0, graph.Count());
            Assert.IsFalse(graph.ContainsVertex(1));
            Assert.ThrowsException<ArgumentException>(() => graph.HasEdge(1, 2));
            Assert.ThrowsException<ArgumentException>(() => graph.Edges(1).ToList());
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveVertex(1));

            graph.AddVertex(1);
            Assert.AreEqual(1, graph.Count());
            Assert.IsTrue(graph.ContainsVertex(1));
            Assert.AreEqual(1, graph.GetVertex(1).Key);
            Assert.AreEqual(0, graph.Edges(1).Count());

            graph.AddEdge(1, 1);
            Assert.IsTrue(graph.HasEdge(1, 1));
            Assert.AreEqual(1, graph.Edges(1).Count());
            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(1, 1));

            graph.RemoveEdge(1, 1);
            Assert.IsFalse(graph.HasEdge(1, 1));
        }

        [TestMethod]
        public void Graph_Clone_And_VertexApi()
        {
            var graph = new Graph<int>();

            Assert.IsFalse(graph.IsWeightedGraph);
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveEdge(1, 2));
            Assert.ThrowsException<ArgumentException>(() => graph.AddEdge(1, 2));

            var stringGraph = new Graph<string>();
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.AddVertex(null));
            Assert.ThrowsException<ArgumentException>(() => stringGraph.AddEdge(null, "a"));
            Assert.ThrowsException<ArgumentException>(() => stringGraph.RemoveEdge(null, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.RemoveVertex(null));

            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(1, 2));
            Assert.ThrowsException<InvalidOperationException>(() => graph.RemoveEdge(1, 3));
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveEdge(1, 99));

            var vertex = graph.GetVertex(2);
            Assert.AreEqual(2, vertex.Key);
            Assert.AreEqual(1, vertex.GetEdge(graph.GetVertex(1)).Weight<int>());
            Assert.AreEqual(2, vertex.Edges.Count());
            Assert.AreEqual(3, graph.VerticesAsEnumberable.Count());
            Assert.AreEqual(2, graph.Edges(2).Count());

            // undirected Clone with a self-loop covers edge copy without double-add
            var loopGraph = new Graph<int>();
            loopGraph.AddVertex(1);
            loopGraph.AddEdge(1, 1);
            var loopClone = loopGraph.Clone();
            Assert.IsTrue(loopClone.HasEdge(1, 1));

            var vertexOnly = new Graph<int>();
            vertexOnly.AddVertex(1);
            vertexOnly.AddVertex(2);
            var vertexClone = vertexOnly.Clone();
            Assert.AreEqual(2, vertexClone.VerticesCount);
            Assert.IsFalse(vertexClone.HasEdge(1, 2));

            graph.RemoveVertex(3);
            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.AreEqual(2, graph.VerticesCount);
            Assert.AreEqual(0, new Graph<int>().Clone().VerticesCount);
        }

        [TestMethod]
        public void Graph_Adversarial_Vertex0_Clone_Oracle()
        {
            var graph = new Graph<int>();
            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 0);

            Assert.IsTrue(graph.ContainsVertex(0));
            CollectionAssert.AreEquivalent(new[] { 0, 1, 2 }, graph.ToList());
            CollectionAssert.AreEquivalent(new[] { 0, 1 }, graph.Edges(0).ToList());

            var clone = graph.Clone();
            Assert.AreEqual(3, clone.VerticesCount);
            Assert.IsTrue(clone.HasEdge(0, 1));
            Assert.IsTrue(clone.HasEdge(1, 0));
            Assert.IsTrue(clone.HasEdge(1, 2));
            Assert.IsTrue(clone.HasEdge(0, 0));
            Assert.AreEqual(2, clone.Edges(0).Count());

            graph.RemoveEdge(0, 0);
            graph.RemoveEdge(0, 1);
            graph.RemoveVertex(0);
            Assert.IsFalse(graph.ContainsVertex(0));
            Assert.ThrowsException<ArgumentException>(() => graph.HasEdge(0, 1));
            Assert.IsTrue(clone.HasEdge(0, 1));
            Assert.IsTrue(clone.ContainsVertex(0));
        }
    }
}
