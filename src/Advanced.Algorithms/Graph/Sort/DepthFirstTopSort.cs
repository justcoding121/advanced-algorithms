using System;
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
        var visiting = new HashSet<T>();

        //we need a loop so that we can reach all vertices
        foreach (var vertex in graph.VerticesAsEnumberable)
            if (!visited.Contains(vertex.Key))
                Dfs(vertex, visited, visiting, pathStack);

        //now just pop the stack to result
        var result = new List<T>();
        while (pathStack.Count > 0) result.Add(pathStack.Pop());

        return result;
    }

    /// <summary>
    ///     Do a depth first search.
    /// </summary>
    private void Dfs(IDiGraphVertex<T> vertex,
        HashSet<T> visited, HashSet<T> visiting, Stack<T> pathStack)
    {
        visiting.Add(vertex.Key);

        foreach (var edge in vertex.OutEdges)
        {
            if (visiting.Contains(edge.TargetVertexKey))
                throw new InvalidOperationException("Graph has a cycle.");

            if (!visited.Contains(edge.TargetVertexKey))
                Dfs(edge.TargetVertex, visited, visiting, pathStack);
        }

        visiting.Remove(vertex.Key);
        visited.Add(vertex.Key);

        //add vertex to stack after all edges are visited
        pathStack.Push(vertex.Key);
    }
}
