using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Compute minimum cut edges of given graph
///     using Edmond-Karps improved Ford-Fulkerson Max Flow Algorithm.
/// </summary>
public class MinCut<T, TW> where TW : IComparable
{
    private readonly IFlowOperators<TW> @operator;

    public MinCut(IFlowOperators<TW> @operator)
    {
        this.@operator = @operator;
    }

    public List<MinCutEdge<T>> ComputeMinCut(IDiGraph<T> graph,
        T source, T sink)
    {
        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultWeight.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IFlowOperators<int> operator implementation during initialization.");

        var edmondsKarpMaxFlow = new EdmondKarpMaxFlow<T, TW>(@operator);

        var maxFlowResidualGraph = edmondsKarpMaxFlow
            .ComputeMaxFlowAndReturnResidualGraph(graph, source, sink);

        //according to Min Max theory
        //the Min Cut can be obtained by Finding edges 
        //from Reachable Vertices from Source
        //to unreachable vertices in residual graph
        var reachableVertices = GetReachable(maxFlowResidualGraph, source);

        var result = new List<MinCutEdge<T>>();

        foreach (var vertex in reachableVertices)
        foreach (var edge in graph.GetVertex(vertex).OutEdges)
            //if unreachable
            if (!reachableVertices.Contains(edge.TargetVertexKey))
                result.Add(new MinCutEdge<T>(vertex, edge.TargetVertexKey));

        return result;
    }

    /// <summary>
    ///     Gets a list of reachable vertices in residual graph from source.
    /// </summary>
    private HashSet<T> GetReachable(WeightedDiGraph<T, TW> residualGraph,
        T source)
    {
        var visited = new HashSet<T>();

        Dfs(residualGraph.Vertices[source], visited);

        return visited;
    }

    /// <summary>
    ///     Recursive DFS.
    /// </summary>
    private void Dfs(WeightedDiGraphVertex<T, TW> currentResidualGraphVertex,
        HashSet<T> visited)
    {
        visited.Add(currentResidualGraphVertex.Key);

        foreach (var edge in currentResidualGraphVertex.OutEdges)
        {
            if (visited.Contains(edge.Key.Key)) continue;

            //reachable only if +ive weight (unsaturated edge)
            if (edge.Value.CompareTo(@operator.DefaultWeight) != 0) Dfs(edge.Key, visited);
        }
    }
}

/// <summary>
///     Minimum cut result object.
/// </summary>
public class MinCutEdge<T>
{
    public MinCutEdge(T source, T dest)
    {
        Source = source;
        Destination = dest;
    }

    public T Source { get; }
    public T Destination { get; }
}