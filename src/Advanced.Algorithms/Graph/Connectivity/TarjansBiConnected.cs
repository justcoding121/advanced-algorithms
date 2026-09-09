using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Finds if a graph is BiConnected.
/// </summary>
public class TarjansBiConnected<T>
{
    /// <summary>
    ///     A graph is BiConnected if and only if it is connected and has no articulation points.
    /// </summary>
    public bool IsBiConnected(IGraph<T> graph)
    {
        if (graph.VerticesCount == 0) return false;

        if (!IsConnected(graph)) return false;

        var algorithm = new TarjansArticulationFinder<T>();
        return algorithm.FindArticulationPoints(graph).Count == 0;
    }

    private static bool IsConnected(IGraph<T> graph)
    {
        var visited = new HashSet<T>();
        var stack = new Stack<IGraphVertex<T>>();
        var start = graph.ReferenceVertex;
        stack.Push(start);
        visited.Add(start.Key);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            foreach (var edge in current.Edges)
            {
                if (visited.Contains(edge.TargetVertexKey)) continue;
                visited.Add(edge.TargetVertexKey);
                stack.Push(edge.TargetVertex);
            }
        }

        return visited.Count == graph.VerticesCount;
    }
}
