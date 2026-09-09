using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Bread First Search implementation.
/// </summary>
public class BreadthFirst<T>
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

            if (Bfs(start, visited, vertex)) return true;
        }

        return false;
    }

    /// <summary>
    ///     BFS implementation.
    /// </summary>
    private static bool Bfs(IGraphVertex<T> referenceVertex,
        HashSet<T> visited, T searchVertex)
    {
        var bfsQueue = new Queue<IGraphVertex<T>>();

        bfsQueue.Enqueue(referenceVertex);
        visited.Add(referenceVertex.Key);

        while (bfsQueue.Count > 0)
        {
            var current = bfsQueue.Dequeue();

            if (current.Key.Equals(searchVertex)) return true;

            foreach (var edge in current.Edges)
            {
                if (visited.Contains(edge.TargetVertexKey)) continue;

                visited.Add(edge.TargetVertexKey);
                bfsQueue.Enqueue(edge.TargetVertex);
            }
        }

        return false;
    }
}