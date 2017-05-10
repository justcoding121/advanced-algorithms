using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.Coloring;
using System;
using Algorithm.Sandbox.GraphAlgorithms.Flow;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.Matching
{
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

    public interface IBiPartiteMatchOperators<T> : IFlowOperators<int>
    {
        /// <summary>
        /// Get a random unique vertex not in graph 
        /// required for dummy source/destination vertex for ford-fulkerson max flow
        /// </summary>
        /// <returns></returns>
        T GetRandomUniqueVertex();
    }

    /// <summary>
    ///  Compute Max BiParitite Edges using Ford-Fukerson algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BiPartiteMatching<T>
    {
        IBiPartiteMatchOperators<T> operators;
        public BiPartiteMatching(IBiPartiteMatchOperators<T> operators)
        {
            this.operators = operators;
        }

        /// <summary>
        /// Returns a list of Max BiPartite Match Edges
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public List<MatchEdge<T>> GetMaxBiPartiteMatching(AsGraph<T> graph)
        {
            //check if the graph is BiPartite by coloring 2 colors
            var mColorer = new MColorer<T, int>();
            var colorResult = mColorer.Color(graph, new int[] { 1, 2 });

            if (colorResult.CanColor == false)
            {
                throw new Exception("Graph is not BiPartite.");
            }

            return GetMaxBiPartiteMatching(graph, colorResult.Partitions);

        }

        /// <summary>
        /// Get Max Match from Given BiPartitioned Graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="partitions"></param>
        /// <returns></returns>
        private List<MatchEdge<T>> GetMaxBiPartiteMatching(AsGraph<T> graph,
            Dictionary<int, List<T>> partitions)
        {
            //add unit edges from dymmy source to group 1 vertices
            var dummySource = operators.GetRandomUniqueVertex();
            if (graph.Vertices.ContainsKey(dummySource))
            {
                throw new Exception("Dummy vertex provided is not unique to given graph.");
            }

            //add unit edges from group 2 vertices to sink
            var dummySink = operators.GetRandomUniqueVertex();
            if (graph.Vertices.ContainsKey(dummySink))
            {
                throw new Exception("Dummy vertex provided is not unique to given graph.");
            }

            var workGraph = createFlowGraph(graph, dummySource, dummySink, partitions);

            //run ford fulkerson using edmon karp method
            var fordFulkerson = new EdmondKarpMaxFlow<T, int>(operators);

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
        /// create a directed unit weighted graph with given dummySource to Patition 1 & Patition 2 to dummy sink
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="dummySource"></param>
        /// <param name="dummySink"></param>
        /// <param name="partitions"></param>
        /// <returns></returns>
        private AsWeightedDiGraph<T, int> createFlowGraph(AsGraph<T> graph,
            T dummySource, T dummySink,
            Dictionary<int, List<T>> partitions)
        {
            var workGraph = new AsWeightedDiGraph<T, int>();


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
                foreach (var edge in graph.Vertices[group1Vertex].Edges)
                {
                    workGraph.AddEdge(group1Vertex, edge.Value, 1);
                }

            }

            return workGraph;
        }
    }
}
