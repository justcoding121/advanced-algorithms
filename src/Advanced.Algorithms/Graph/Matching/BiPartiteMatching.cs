using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    ///  Compute Max BiParitite Edges using Ford-Fukerson algorithm.
    /// </summary>
    public class BiPartiteMatching<T>
    {
        readonly IBiPartiteMatchOperators<T> @operator;
        public BiPartiteMatching(IBiPartiteMatchOperators<T> @operator)
        {
            this.@operator = @operator;
        }

        /// <summary>
        /// Returns a list of Max BiPartite Match Edges.
        /// </summary>
        public List<MatchEdge<T>> GetMaxBiPartiteMatching(IGraph<T> graph)
        {
            if (this.@operator == null)
            {
                throw new ArgumentException("Provide an operator implementation for generic type T during initialization.");
            }

            //check if the graph is BiPartite by coloring 2 colors
            var mColorer = new MColorer<T, int>();
            var colorResult = mColorer.Color(graph, new int[] { 1, 2 });

            if (colorResult.CanColor == false)
            {
                throw new Exception("Graph is not BiPartite.");
            }

            return getMaxBiPartiteMatching(graph, colorResult.Partitions);

        }

        /// <summary>
        /// Get Max Match from Given BiPartitioned Graph.
        /// </summary>
        private List<MatchEdge<T>> getMaxBiPartiteMatching(IGraph<T> graph,
            Dictionary<int, List<T>> partitions)
        {
            //add unit edges from dymmy source to group 1 vertices
            var dummySource = @operator.GetRandomUniqueVertex();
            if (graph.ContainsVertex(dummySource))
            {
                throw new Exception("Dummy vertex provided is not unique to given graph.");
            }

            //add unit edges from group 2 vertices to sink
            var dummySink = @operator.GetRandomUniqueVertex();
            if (graph.ContainsVertex(dummySink))
            {
                throw new Exception("Dummy vertex provided is not unique to given graph.");
            }

            var workGraph = createFlowGraph(graph, dummySource, dummySink, partitions);

            //run ford fulkerson using edmon karp method
            var fordFulkerson = new EdmondKarpMaxFlow<T, int>(@operator);

            var flowPaths = fordFulkerson
                .ComputeMaxFlowAndReturnFlowPath(workGraph, dummySource, dummySink);

            //now gather all group1 to group 2 edges in residual graph with positive flow
            var result = new List<MatchEdge<T>>();

            foreach (var path in flowPaths)
            {
                result.Add(new MatchEdge<T>(path[1], path[2]));
            }

            return result;
        }

        /// <summary>
        /// create a directed unit weighted graph with given dummySource to Patition 1 and Patition 2 to dummy sink.
        /// </summary>
        private static WeightedDiGraph<T, int> createFlowGraph(IGraph<T> graph,
            T dummySource, T dummySink,
            Dictionary<int, List<T>> partitions)
        {
            var workGraph = new WeightedDiGraph<T, int>();
            workGraph.AddVertex(dummySource);

            foreach (var group1Vertex in partitions[1])
            {
                workGraph.AddVertex(group1Vertex);
                workGraph.AddEdge(dummySource, group1Vertex, 1);
            }

            workGraph.AddVertex(dummySink);

            foreach (var group2Vertex in partitions[2])
            {
                workGraph.AddVertex(group2Vertex);
                workGraph.AddEdge(group2Vertex, dummySink, 1);
            }

            //now add directed edges from group 1 vertices to group 2 vertices
            foreach (var group1Vertex in partitions[1])
            {
                foreach (var edge in graph.GetVertex(group1Vertex).Edges)
                {
                    workGraph.AddEdge(group1Vertex, edge.TargetVertexKey, 1);
                }
            }

            return workGraph;
        }
    }

    /// <summary>
    /// The match result object.
    /// </summary>
    public class MatchEdge<T>
    {
        public T Source { get; }
        public T Target { get; }

        public MatchEdge(T source, T target)
        {
            Source = source;
            Target = target;
        }
    }

    /// <summary>
    /// Generic operator interface required by BiPartite matching algorithm.
    /// </summary>
    public interface IBiPartiteMatchOperators<T> : IFlowOperators<int>
    {
        /// <summary>
        /// Get a random unique vertex not in graph 
        /// required for dummy source/destination vertex for ford-fulkerson max flow.
        /// </summary>
        T GetRandomUniqueVertex();
    }
}
