using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.Flow;
using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.Cut
{
    public class MinCutEdge<T>
    {
        public T Source { get; }
        public T Destination { get; }

        public MinCutEdge(T source, T dest)
        {
            Source = source;
            Destination = dest;
        }
    }

    /// <summary>
    /// Commpute minimum cut edges of given graph 
    /// using Edmond Karps improved Ford-Fulkerson Max Flow Algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class MinCut<T, W> where W : IComparable
    {
        IFlowOperators<W> operators;
        public MinCut(IFlowOperators<W> operators)
        {
            this.operators = operators;
        }

        public List<MinCutEdge<T>> ComputeMinCut(AsWeightedDiGraph<T, W> graph,
        T source, T sink)
        {
            var edmondsKarpMaxFlow = new EdmondKarpMaxFlow<T, W>(operators);

            var maxFlowResidualGraph = edmondsKarpMaxFlow
                .ComputeMaxFlowAndReturnResidualGraph(graph, source, sink);

            //according to Min Max theory
            //the Min Cut can be obtained by Finding edges 
            //from Reachable Vertices from Source
            //to unreachable vertices in residual graph
            var reachableVertices = GetReachable(graph, maxFlowResidualGraph, source);

            var result = new List<MinCutEdge<T>>();

            foreach (var vertex in reachableVertices)
            {
                foreach (var edge in graph.Vertices[vertex].OutEdges)
                {
                    //if unreachable
                    if (!reachableVertices.Contains(edge.Key.Value))
                    {
                        result.Add(new MinCutEdge<T>(vertex, edge.Key.Value));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a list of reachable vertices in residual graph from source
        /// </summary>
        /// <param name="residualGraph"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public HashSet<T> GetReachable(AsWeightedDiGraph<T, W> graph,
            AsWeightedDiGraph<T, W> residualGraph,
            T source)
        {
            var visited = new HashSet<T>();

            DFS(graph, residualGraph.Vertices[source], visited);

            return visited;
        }

        /// <summary>
        /// Recursive DFS
        /// </summary>
        /// <param name="currentResidualGraphVertex"></param>
        /// <param name="visited"></param>
        /// <param name="searchVetex"></param>
        /// <returns></returns>
        private void DFS(AsWeightedDiGraph<T, W> graph,
            AsWeightedDiGraphVertex<T, W> currentResidualGraphVertex,
            HashSet<T> visited)
        {
            visited.Add(currentResidualGraphVertex.Value);

            foreach (var edge in currentResidualGraphVertex.OutEdges)
            {
                if (!visited.Contains(edge.Key.Value))
                {
                    //reachable only if +ive weight (unsaturated edge)
                    if (edge.Value.CompareTo(operators.defaultWeight) != 0)
                    {
                        DFS(graph, edge.Key, visited);
                    }
                }

            }

        }
    }
}
