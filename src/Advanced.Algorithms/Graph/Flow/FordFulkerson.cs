using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A ford-fulkerson max flox implementation on weighted directed graph using
///     adjacency list representation of graph and residual graph.
/// </summary>
public class FordFulkersonMaxFlow<T, TW> where TW : IComparable
{
    private readonly IFlowOperators<TW> @operator;

    public FordFulkersonMaxFlow(IFlowOperators<TW> @operator)
    {
        this.@operator = @operator;
    }

    /// <summary>
    ///     Compute max flow by searching a path
    ///     and then augmenting the residual graph until
    ///     no more path exists in residual graph with possible flow.
    /// </summary>
    public TW ComputeMaxFlow(IDiGraph<T> graph,
        T source, T sink)
    {
        ValidateOperator(graph);

        var residualGraph = CreateResidualGraph(graph);

        var path = Dfs(residualGraph, source, sink);

        var result = @operator.DefaultWeight;

        while (path != null)
        {
            result = @operator.AddWeights(result, AugmentResidualGraph(residualGraph, path));
            path = Dfs(residualGraph, source, sink);
        }

        return result;
    }


    /// <summary>
    ///     Return all flow Paths.
    /// </summary>
    public List<List<T>> ComputeMaxFlowAndReturnFlowPath(IDiGraph<T> graph,
        T source, T sink)
    {
        ValidateOperator(graph);

        var residualGraph = CreateResidualGraph(graph);

        var path = Dfs(residualGraph, source, sink);

        var flow = @operator.DefaultWeight;

        var result = new List<List<T>>();
        while (path != null)
        {
            result.Add(path);
            flow = @operator.AddWeights(flow, AugmentResidualGraph(residualGraph, path));
            path = Dfs(residualGraph, source, sink);
        }

        return result;
    }

    private void ValidateOperator(IDiGraph<T> graph)
    {
        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultWeight.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IFlowOperators<int> operator implementation during initialization.");
    }

    /// <summary>
    ///     Augment current Path to residual Graph.
    /// </summary>
    private TW AugmentResidualGraph(WeightedDiGraph<T, TW> residualGraph, List<T> path)
    {
        var min = @operator.MaxWeight;

        for (var i = 0; i < path.Count - 1; i++)
        {
            var vertex1 = residualGraph.FindVertex(path[i]);
            var vertex2 = residualGraph.FindVertex(path[i + 1]);

            var edgeValue = vertex1.OutEdges[vertex2];

            if (min.CompareTo(edgeValue) > 0) min = edgeValue;
        }

        //augment path
        for (var i = 0; i < path.Count - 1; i++)
        {
            var vertex1 = residualGraph.FindVertex(path[i]);
            var vertex2 = residualGraph.FindVertex(path[i + 1]);

            //substract from forward paths
            vertex1.OutEdges[vertex2] = @operator.SubstractWeights(vertex1.OutEdges[vertex2], min);

            //add for backward paths
            vertex2.OutEdges[vertex1] = @operator.AddWeights(vertex2.OutEdges[vertex1], min);
        }

        return min;
    }

    /// <summary>
    ///     Depth first search to find a path to sink in residual graph from source.
    /// </summary>
    private List<T> Dfs(WeightedDiGraph<T, TW> residualGraph, T source, T sink)
    {
        //init parent lookup table to trace path
        var parentLookUp = new Dictionary<WeightedDiGraphVertex<T, TW>, WeightedDiGraphVertex<T, TW>>();
        foreach (var vertex in residualGraph.Vertices) parentLookUp.Add(vertex.Value, null);

        //regular DFS stuff
        var stack = new Stack<WeightedDiGraphVertex<T, TW>>();
        var visited = new HashSet<WeightedDiGraphVertex<T, TW>>();
        stack.Push(residualGraph.Vertices[source]);
        visited.Add(residualGraph.Vertices[source]);

        WeightedDiGraphVertex<T, TW> currentVertex = null;

        while (stack.Count > 0)
        {
            currentVertex = stack.Pop();

            //reached sink? then break otherwise dig in
            if (currentVertex.Key.Equals(sink))
                break;
            foreach (var edge in currentVertex.OutEdges)
                //visit only if edge have available flow
                if (!visited.Contains(edge.Key)
                    && edge.Value.CompareTo(@operator.DefaultWeight) > 0)
                {
                    //keep track of this to trace out path once sink is found
                    parentLookUp[edge.Key] = currentVertex;
                    stack.Push(edge.Key);
                    visited.Add(edge.Key);
                }
        }

        //could'nt find a path
        if (currentVertex == null || !currentVertex.Key.Equals(sink)) return null;

        //traverse back from sink to find path to source
        var path = new Stack<T>();

        path.Push(sink);

        while (currentVertex != null && !currentVertex.Key.Equals(source))
        {
            path.Push(parentLookUp[currentVertex].Key);
            currentVertex = parentLookUp[currentVertex];
        }

        //now reverse the stack to get the path from source to sink
        var result = new List<T>();

        while (path.Count > 0) result.Add(path.Pop());

        return result;
    }

    /// <summary>
    ///     Clones this graph and creates a residual graph.
    /// </summary>
    private WeightedDiGraph<T, TW> CreateResidualGraph(IDiGraph<T> graph)
    {
        var newGraph = new WeightedDiGraph<T, TW>();

        //clone graph vertices
        foreach (var vertex in graph.VerticesAsEnumberable) newGraph.AddVertex(vertex.Key);

        //clone edges
        foreach (var vertex in graph.VerticesAsEnumberable)
            //Use either OutEdges or InEdges for cloning
            //here we use OutEdges
        foreach (var edge in vertex.OutEdges)
        {
            //original edge
            newGraph.AddEdge(vertex.Key, edge.TargetVertexKey, edge.Weight<TW>());
            //add a backward edge for residual graph with edge value as default(W)
            newGraph.AddEdge(edge.TargetVertexKey, vertex.Key, default);
        }

        return newGraph;
    }
}

/// <summary>
///     Operators to deal with generic Add, Substract etc on edge weights for flow algorithms such as ford-fulkerson
///     algorithm.
/// </summary>
public interface IFlowOperators<TW> where TW : IComparable
{
    /// <summary>
    ///     default value for this type W.
    /// </summary>
    TW DefaultWeight { get; }

    /// <summary>
    ///     returns the max for this type W.
    /// </summary>
    TW MaxWeight { get; }

    /// <summary>
    ///     add two weights.
    /// </summary>
    TW AddWeights(TW a, TW b);

    /// <summary>
    ///     substract b from a.
    /// </summary>
    TW SubstractWeights(TW a, TW b);
}