using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Depth First Search.
/// </summary>
public class DepthFirst<T>
{
    /// <summary>
    ///     Returns true if item exists (searches all connected components).
    /// </summary>
    public bool Find(IGraph<T> graph, T vertex)
    {
        if (graph.VerticesCount == 0) return false;

        var visited = new HashSet<T>();

        foreach (var start in graph.VerticesAsEnumberable)
        {
            if (visited.Contains(start.Key)) continue;

            if (Dfs(start, visited, vertex)) return true;
        }

        return false;
    }

    /// <summary>
    ///     Recursive DFS.
    /// </summary>
    private bool Dfs(IGraphVertex<T> current,
        HashSet<T> visited, T searchVetex)
    {
        visited.Add(current.Key);

        if (current.Key.Equals(searchVetex)) return true;

        foreach (var edge in current.Edges)
        {
            if (visited.Contains(edge.TargetVertexKey)) continue;

            if (Dfs(edge.TargetVertex, visited, searchVetex)) return true;
        }

        return false;
    }
}