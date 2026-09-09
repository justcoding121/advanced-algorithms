using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures.Graph.AdjacencyMatrix
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

            graph.AddEdge(3, 2);
            Assert.AreEqual(2, graph.InEdges(2).Count());
            graph.RemoveEdge(3, 2);

            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 1);
            graph.AddEdge(3, 5);

            Assert.AreEqual(2, graph.OutEdges(4).Count());

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

            graph.AddEdge(4, 5);
            graph.RemoveVertex(4);

            graph.AddEdge(5, 5);
            graph.RemoveEdge(5, 5);
            graph.RemoveVertex(5);


            Assert.AreEqual(0, graph.VerticesCount);
        }

        [TestMethod]
        public void DiGraph_Empty_Missing_SelfLoop_Enumeration()
        {
            var graph = new DiGraph<int>();

            Assert.AreEqual(0, graph.VerticesCount);
            Assert.AreEqual(0, graph.Count());
            Assert.ThrowsException<InvalidOperationException>(() => { var _ = graph.ReferenceVertex; });
            Assert.IsFalse(graph.ContainsVertex(1));
            Assert.ThrowsException<ArgumentException>(() => graph.HasEdge(1, 2));
            Assert.ThrowsException<ArgumentException>(() => graph.OutEdges(1).ToList());
            Assert.ThrowsException<ArgumentException>(() => graph.InEdges(1).ToList());

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
        public void DiGraph_Resize_Clone_And_VertexApi()
        {
            var graph = new DiGraph<int>();

            Assert.IsFalse(graph.IsWeightedGraph);
            Assert.ThrowsException<ArgumentException>(() => graph.OutEdgeCount(1));
            Assert.ThrowsException<ArgumentException>(() => graph.InEdgeCount(1));
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveVertex(1));
            Assert.ThrowsException<ArgumentException>(() => graph.RemoveEdge(1, 2));
            Assert.ThrowsException<ArgumentException>(() => graph.AddEdge(1, 2));

            var stringGraph = new DiGraph<string>();
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.AddVertex(null));
            Assert.ThrowsException<ArgumentException>(() => stringGraph.HasEdge(null, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => stringGraph.RemoveVertex(null));

            for (var i = 1; i <= 9; i++)
            {
                graph.AddVertex(i);
            }

            Assert.ThrowsException<ArgumentException>(() => graph.AddVertex(1));
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);
            graph.AddEdge(8, 9);
            Assert.AreEqual(1, graph.OutEdgeCount(1));
            Assert.AreEqual(1, graph.InEdgeCount(1));
            Assert.AreEqual(1, graph.OutEdgeCount(8));

            Assert.ThrowsException<InvalidOperationException>(() => graph.RemoveEdge(1, 9));
            Assert.ThrowsException<InvalidOperationException>(() => graph.AddEdge(1, 2));

            var vertex = ((IDiGraph<int>)graph).GetVertex(2);
            Assert.AreEqual(2, vertex.Key);
            Assert.AreEqual(1, vertex.OutEdgeCount);
            Assert.AreEqual(1, vertex.InEdgeCount);
            Assert.AreEqual(3, vertex.GetOutEdge(((IDiGraph<int>)graph).GetVertex(3)).TargetVertexKey);
            Assert.AreEqual(1, vertex.OutEdges.Count());
            Assert.AreEqual(1, vertex.InEdges.Count());
            Assert.AreEqual(9, graph.VerticesAsEnumberable.Count());

            var clone = graph.Clone();
            Assert.AreEqual(9, clone.VerticesCount);
            Assert.IsTrue(clone.HasEdge(1, 2));
            Assert.IsTrue(clone.HasEdge(8, 9));
            Assert.IsFalse(clone.HasEdge(2, 1));

            for (var i = 9; i >= 5; i--)
            {
                graph.RemoveVertex(i);
            }

            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.IsTrue(graph.HasEdge(3, 1));
            Assert.AreEqual(4, graph.VerticesCount);

            graph.RemoveVertex(4);
            graph.RemoveVertex(3);
            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.AreEqual(2, graph.VerticesCount);

            graph.RemoveVertex(2);
            graph.RemoveVertex(1);
            Assert.AreEqual(0, graph.VerticesCount);
        }
    }
}
