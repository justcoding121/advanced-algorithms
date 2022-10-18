using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Find Toplogical order of a graph using Depth First Search.
/// </summary>
public class DepthFirstTopSort<T>
{
    /// <summary>
    ///     Returns the vertices in Topologically Sorted Order.
    /// </summary>
    public List<T> GetTopSort(IDiGraph<T> graph)
    {
        var pathStack = new Stack<T>();
        var visited = new HashSet<T>();

        //we need a loop so that we can reach all vertices
        foreach (var vertex in graph.VerticesAsEnumberable)
            if (!visited.Contains(vertex.Key))
                Dfs(vertex, visited, pathStack);

        //now just pop the stack to result
        var result = new List<T>();
        while (pathStack.Count > 0) result.Add(pathStack.Pop());

        return result;
    }

    /// <summary>
    ///     Do a depth first search.
    /// </summary>
    private void Dfs(IDiGraphVertex<T> vertex,
        HashSet<T> visited, Stack<T> pathStack)
    {
        visited.Add(vertex.Key);

        foreach (var edge in vertex.OutEdges)
            if (!visited.Contains(edge.TargetVertexKey))
                Dfs(edge.TargetVertex, visited, pathStack);

        //add vertex to stack after all edges are visited
        pathStack.Push(vertex.Key);
    }
}