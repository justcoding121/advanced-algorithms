using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures.Graph.AdjacencyList
{
    [TestClass]
    public class DiGraphTests
    {
        /// <summary>
        ///     key value dictionary tests
        /// </summary>
        [TestMethod]
        public void DiGraph_Smoke_Test()
        {
            var graph = new DiGraph<int>();

            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            graph.AddEdge(1, 2);
            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.IsFalse(graph.HasEdge(2, 1));

            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 1);
            graph.AddEdge(3, 5);

            //IEnumerable test using linq
            Assert.AreEqual(graph.VerticesCount, graph.Count());

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

            graph.AddEdge(5, 5);
            graph.RemoveEdge(5, 5);
            graph.RemoveVertex(5);

            Assert.AreEqual(0, graph.VerticesCount);

            //IEnumerable test using linq
            Assert.AreEqual(graph.VerticesCount, graph.Count());
        }

        [TestMethod]
        public void DiGraph_Empty_Missing_SelfLoop_Enumeration()
        {
            var graph = new DiGraph<int>();

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

            graph.AddEdge(1, 1);
            Assert.IsTrue(graph.HasEdge(1, 1));
            Assert.AreEqual(1, graph.OutEdges(1).Count());
            Assert.AreEqual(1, graph.InEdges(1).Count());
            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(1, 1));

            graph.AddEdge(1, 2);
            var clone = graph.Clone();
            Assert.AreEqual(2, clone.VerticesCount);
            Assert.IsTrue(clone.HasEdge(1, 1));
            Assert.IsTrue(clone.HasEdge(1, 2));
            Assert.IsFalse(clone.HasEdge(2, 1));
        }

        [TestMethod]
        public void DiGraph_Clone_And_VertexApi()
        {
            var graph = new DiGraph<int>();

            Assert.IsFalse(graph.IsWeightedGraph);
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveEdge(1, 2));
            Assert.ThrowsException<ArgumentException>(() => graph.AddEdge(1, 2));

            var stringGraph = new DiGraph<string>();
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.AddVertex(null));
            Assert.ThrowsException<ArgumentException>(() => stringGraph.AddEdge(null, "a"));
            Assert.ThrowsException<ArgumentException>(() => stringGraph.RemoveEdge(null, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.RemoveVertex(null));

            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);

            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(1, 2));
            Assert.ThrowsException<InvalidOperationException>(() => graph.RemoveEdge(2, 1));
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveEdge(1, 99));

            var vertex = graph.GetVertex(2);
            Assert.AreEqual(2, vertex.Key);
            Assert.AreEqual(1, vertex.OutEdgeCount);
            Assert.AreEqual(1, vertex.InEdgeCount);
            Assert.AreEqual(1, vertex.GetOutEdge(graph.GetVertex(3)).Weight<int>());
            Assert.AreEqual(1, vertex.OutEdges.Count());
            Assert.AreEqual(1, vertex.InEdges.Count());
            Assert.AreEqual(1, vertex.OutEdgeCount);
            Assert.AreEqual(1, ((IEnumerable<int>)vertex).Count());
            Assert.AreEqual(3, graph.VerticesAsEnumberable.Count());

            graph.RemoveVertex(3);
            Assert.IsFalse(graph.ContainsVertex(3));
            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.AreEqual(2, graph.VerticesCount);
            Assert.AreEqual(0, new DiGraph<int>().Clone().VerticesCount);
        }
    }
}
