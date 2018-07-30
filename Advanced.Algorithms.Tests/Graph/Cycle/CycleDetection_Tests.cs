using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class CycleDetection_Tests
    {
        [TestMethod]
        public void Graph_Cycle_Detection_Tests()
        {
            var graph = new DiGraph<char>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('E');
            graph.AddVertex('F');
            graph.AddVertex('G');
            graph.AddVertex('H');


            graph.AddEdge('A', 'B');
            graph.AddEdge('B', 'C');
            graph.AddEdge('C', 'A');

            graph.AddEdge('C', 'D');
            graph.AddEdge('D', 'E');

            graph.AddEdge('E', 'F');
            graph.AddEdge('F', 'G');
            graph.AddEdge('G', 'E');

            graph.AddEdge('F', 'H');

            var algo = new CycleDetector<char>();

            Assert.IsTrue(algo.HasCycle(graph));

            graph.RemoveEdge('C', 'A');
            graph.RemoveEdge('G', 'E');

            Assert.IsFalse(algo.HasCycle(graph));
        }
    }
}
