using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A Kosaraju Strong Connected Component Algorithm Implementation.
/// </summary>
public class KosarajuStronglyConnected<T>
{
    /// <summary>
    ///     Returns all Connected Components using Kosaraju's Algorithm.
    /// </summary>
    public List<List<T>>
        FindStronglyConnectedComponents(IDiGraph<T> graph)
    {
        var visited = new HashSet<T>();
        var finishStack = new Stack<T>();

        //step one - create DFS finish visit stack
        foreach (var vertex in graph.VerticesAsEnumberable)
            if (!visited.Contains(vertex.Key))
                KosarajuStep1(vertex, visited, finishStack);

        //reverse edges
        var reverseGraph = ReverseEdges(graph);

        visited.Clear();

        var result = new List<List<T>>();

        //now pop finish stack and gather the components
        while (finishStack.Count > 0)
        {
            var currentVertex = reverseGraph.GetVertex(finishStack.Pop());

            if (!visited.Contains(currentVertex.Key))
                result.Add(KosarajuStep2(currentVertex, visited,
                    finishStack, new List<T>()));
        }

        return result;
    }

    /// <summary>
    ///     Just do a DFS keeping track on finish Stack of Vertices.
    /// </summary>
    private void KosarajuStep1(IDiGraphVertex<T> currentVertex,
        HashSet<T> visited,
        Stack<T> finishStack)
    {
        visited.Add(currentVertex.Key);

        foreach (var edge in currentVertex.OutEdges)
            if (!visited.Contains(edge.TargetVertexKey))
                KosarajuStep1(edge.TargetVertex, visited, finishStack);

        //finished visiting, so add to stack
        finishStack.Push(currentVertex.Key);
    }

    /// <summary>
    ///     In step two we just add all reachable nodes to result (connected componant).
    /// </summary>
    private List<T> KosarajuStep2(IDiGraphVertex<T> currentVertex,
        HashSet<T> visited, Stack<T> finishStack,
        List<T> result)
    {
        visited.Add(currentVertex.Key);
        result.Add(currentVertex.Key);

        foreach (var edge in currentVertex.OutEdges)
            if (!visited.Contains(edge.TargetVertexKey))
                KosarajuStep2(edge.TargetVertex, visited, finishStack, result);

        return result;
    }

    /// <summary>
    ///     Create a clone graph with reverse edge directions.
    /// </summary>
    private IDiGraph<T> ReverseEdges(IDiGraph<T> graph)
    {
        var newGraph = new DiGraph<T>();

        foreach (var vertex in graph.VerticesAsEnumberable) newGraph.AddVertex(vertex.Key);

        foreach (var vertex in graph.VerticesAsEnumberable)
        foreach (var edge in vertex.OutEdges)
            //reverse edge
            newGraph.AddEdge(edge.TargetVertexKey, vertex.Key);

        return newGraph;
    }
}