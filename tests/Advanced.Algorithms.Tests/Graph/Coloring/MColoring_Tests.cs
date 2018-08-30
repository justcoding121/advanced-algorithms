using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class MColoring_Tests
    {
       
        [TestMethod]
        public void MColoring_Smoke_Test()
        {
            var graph = new Graph<int>();

            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);

            var algo = new MColorer<int, string>();

            var result = algo.Color(graph, new string[] { "red", "green", "blue" });

            Assert.IsTrue(result.CanColor);
        }
    }
}
