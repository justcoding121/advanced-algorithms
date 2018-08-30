using System.Linq;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class MinVertexCover_Tests
    {

        [TestMethod]
        public void MinVertexCover_Smoke_Test()
        {
            var graph = new Graph<int>();

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
