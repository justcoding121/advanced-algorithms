using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A minimum vertex conver algorithm implementation.
/// </summary>
public class MinVertexCover<T>
{
    public List<IGraphVertex<T>> GetMinVertexCover(IGraph<T> graph)
    {
        var visited = new HashSet<IGraphVertex<T>>();
        var cover = new List<IGraphVertex<T>>();

        foreach (var vertex in graph.VerticesAsEnumberable)
            if (!visited.Contains(vertex))
                GetMinVertexCover(vertex, visited, cover);

        return cover;
    }

    /// <summary>
    ///     An approximation algorithm for NP complete vertex cover problem.
    ///     Add a random edge vertices until done visiting all edges.
    /// </summary>
    private List<IGraphVertex<T>> GetMinVertexCover(IGraphVertex<T> vertex,
        HashSet<IGraphVertex<T>> visited, List<IGraphVertex<T>> cover)
    {
        visited.Add(vertex);

        foreach (var edge in vertex.Edges)
        {
            if (!cover.Contains(vertex) && !cover.Contains(edge.TargetVertex))
            {
                cover.Add(vertex);
                cover.Add(edge.TargetVertex);
            }

            if (!visited.Contains(edge.TargetVertex)) GetMinVertexCover(edge.TargetVertex, visited, cover);
        }

        return cover;
    }
}
