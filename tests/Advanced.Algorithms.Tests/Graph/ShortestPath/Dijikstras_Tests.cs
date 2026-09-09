using System.Linq;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class DijikstrasTests
    {
        [TestMethod]
        public void Dijikstra_AdjacencyListGraph_Smoke_Test()
        {
            var graph = new WeightedDiGraph<char, int>();

            graph.AddVertex('S');
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('T');

            graph.AddEdge('S', 'A', 8);
            graph.AddEdge('A', 'S', 2);
            graph.AddEdge('S', 'C', 10);

            graph.AddEdge('A', 'B', 10);
            graph.AddEdge('A', 'C', 1);
            graph.AddEdge('A', 'D', 8);

            graph.AddEdge('B', 'T', 4);

            graph.AddEdge('C', 'D', 1);

            graph.AddEdge('D', 'B', 1);
            graph.AddEdge('D', 'T', 10);

            var algorithm = new DijikstraShortestPath<char, int>(new DijikstraShortestPathOperators());

            var result = algorithm.FindShortestPath(graph, 'S', 'T');

            Assert.AreEqual(15, result.Length);

            var expectedPath = new[] { 'S', 'A', 'C', 'D', 'B', 'T' };
            for (var i = 0; i < expectedPath.Length; i++) Assert.AreEqual(expectedPath[i], result.Path[i]);
        }

        [TestMethod]
        public void Dijikstra_AdjacencyMatrixGraph_Smoke_Test()
        {
            var graph = new Algorithms.DataStructures.Graph.AdjacencyMatrix.WeightedDiGraph<char, int>();

            graph.AddVertex('S');
            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');
            graph.AddVertex('D');
            graph.AddVertex('T');

            graph.AddEdge('S', 'A', 8);
            graph.AddEdge('A', 'S', 2);
            graph.AddEdge('S', 'C', 10);

            graph.AddEdge('A', 'B', 10);
            graph.AddEdge('A', 'C', 1);
            graph.AddEdge('A', 'D', 8);

            graph.AddEdge('B', 'T', 4);

            graph.AddEdge('C', 'D', 1);

            graph.AddEdge('D', 'B', 1);
            graph.AddEdge('D', 'T', 10);

            var algorithm = new DijikstraShortestPath<char, int>(new DijikstraShortestPathOperators());

            var result = algorithm.FindShortestPath(graph, 'S', 'T');

            Assert.AreEqual(15, result.Length);

            var expectedPath = new[] { 'S', 'A', 'C', 'D', 'B', 'T' };
            for (var i = 0; i < expectedPath.Length; i++) Assert.AreEqual(expectedPath[i], result.Path[i]);
        }

        [TestMethod]
        public void Dijikstra_Oracle_Matches_BellmanFord_And_Floyd()
        {
            var digraph = new WeightedDiGraph<char, int>();
            foreach (var v in "SABCDT") digraph.AddVertex(v);
            digraph.AddEdge('S', 'A', 8);
            digraph.AddEdge('S', 'C', 10);
            digraph.AddEdge('A', 'B', 10);
            digraph.AddEdge('A', 'C', 1);
            digraph.AddEdge('A', 'D', 8);
            digraph.AddEdge('B', 'T', 4);
            digraph.AddEdge('C', 'D', 1);
            digraph.AddEdge('D', 'B', 1);
            digraph.AddEdge('D', 'T', 10);

            var undirected = new WeightedGraph<char, int>();
            foreach (var v in "SABCDT") undirected.AddVertex(v);
            undirected.AddEdge('S', 'A', 8);
            undirected.AddEdge('S', 'C', 10);
            undirected.AddEdge('A', 'B', 10);
            undirected.AddEdge('A', 'C', 1);
            undirected.AddEdge('A', 'D', 8);
            undirected.AddEdge('B', 'T', 4);
            undirected.AddEdge('C', 'D', 1);
            undirected.AddEdge('D', 'B', 1);
            undirected.AddEdge('D', 'T', 10);

            var op = new DijikstraShortestPathOperators();
            var di = new DijikstraShortestPath<char, int>(op);
            var bf = new BellmanFordShortestPath<char, int>(op);
            var fw = new FloydWarshallShortestPath<char, int>(op).FindAllPairShortestPaths(undirected);

            var st = di.FindShortestPath(digraph, 'S', 'T');
            Assert.AreEqual(15, st.Length);
            Assert.AreEqual(st.Length, bf.FindShortestPath(digraph, 'S', 'T').Length);
            Assert.AreEqual(st.Length, fw.First(x => x.Source == 'S' && x.Destination == 'T').Distance);
        }

        /// <summary>
        ///     generic operations for int type
        /// </summary>
        public class DijikstraShortestPathOperators : IShortestPathOperators<int>
        {
            public int DefaultValue => 0;

            public int MaxValue => int.MaxValue;

            public int Sum(int a, int b)
            {
                return checked(a + b);
            }
        }
    }
}