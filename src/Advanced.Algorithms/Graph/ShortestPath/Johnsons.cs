using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A Johnson's shortest path algorithm implementation.
    /// </summary>
    public class JohnsonsShortestPath<T, W> where W : IComparable
    {
        readonly IJohnsonsShortestPathOperators<T, W> @operator;

        public JohnsonsShortestPath(IJohnsonsShortestPathOperators<T, W> @operator)
        {
            this.@operator = @operator;
        }

        public List<AllPairShortestPathResult<T, W>>
            FindAllPairShortestPaths(IDiGraph<T> graph)
        {
            if (this.@operator == null)
            {
                throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");
            }

            if (!graph.IsWeightedGraph)
            {
                if (this.@operator.DefaultValue.GetType() != typeof(int))
                {
                    throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                        "Provide an appropriate IJohnsonsShortestPathOperators<T, int> operator implementation during initialization.");
                }
            }

            var workGraph = clone(graph);

            //add an extra vertex with zero weight edge to all nodes
            var randomVetex = @operator.RandomVertex();

            if (workGraph.Vertices.ContainsKey(randomVetex))
            {
                throw new Exception("Random Vertex is not unique for given graph.");
            }
            workGraph.AddVertex(randomVetex);

            foreach (var vertex in workGraph.Vertices)
            {
                workGraph.AddEdge(randomVetex, vertex.Key, @operator.DefaultValue);
            }

            //now compute shortest path from random vertex to all other vertices
            var bellmanFordSp = new BellmanFordShortestPath<T, W>(@operator);
            var bellFordResult = new Dictionary<T, W>();
            foreach (var vertex in workGraph.Vertices)
            {
                var result = bellmanFordSp.FindShortestPath(workGraph, randomVetex, vertex.Key);
                bellFordResult.Add(vertex.Key, result.Length);
            }

            //adjust edges so that all edge values are now +ive
            foreach (var vertex in workGraph.Vertices)
            {
                foreach (var edge in vertex.Value.OutEdges.ToList())
                {
                    vertex.Value.OutEdges[edge.Key] = @operator.Substract(
                        @operator.Sum(bellFordResult[vertex.Key], edge.Value),
                        bellFordResult[edge.Key.Key]);
                }

            }

            workGraph.RemoveVertex(randomVetex);
            //now run dijikstra for all pairs of vertices
            //trace path
            var dijikstras = new DijikstraShortestPath<T, W>(@operator);
            var finalResult = new List<AllPairShortestPathResult<T, W>>();
            foreach (var vertexA in workGraph.Vertices)
            {
                foreach (var vertexB in workGraph.Vertices)
                {
                    var source = vertexA.Key;
                    var dest = vertexB.Key;
                    var sp = dijikstras.FindShortestPath(workGraph, source, dest);

                    //no path exists
                    if (sp.Length.Equals(@operator.MaxValue))
                    {
                        continue;
                    }

                    var distance = sp.Length;
                    var path = sp.Path;

                    finalResult.Add(new AllPairShortestPathResult<T, W>(source, dest, distance, path));

                }
            }

            return finalResult;
        }

        private WeightedDiGraph<T, W> clone(IDiGraph<T> graph)
        {
            var newGraph = new WeightedDiGraph<T, W>();

            foreach (var vertex in graph.VerticesAsEnumberable)
            {
                newGraph.AddVertex(vertex.Key);
            }

            foreach (var vertex in graph.VerticesAsEnumberable)
            {
                foreach (var edge in vertex.OutEdges)
                {
                    newGraph.AddEdge(vertex.Key, edge.TargetVertexKey, edge.Weight<W>());
                }
            }

            return newGraph;

        }
    }

    /// <summary>
    /// A concrete implementation of this interface is required by Johnson's algorithm.
    /// </summary>
    public interface IJohnsonsShortestPathOperators<T, W>
    : IShortestPathOperators<W> where W : IComparable
    {
        /// <summary>
        /// Substract a from b.
        /// </summary>
        W Substract(W a, W b);

        /// <summary>
        /// Gives a random vertex value not in the graph.
        /// </summary>
        T RandomVertex();
    }
}
