using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A BiDirectional Path Search on DiGraph.
/// </summary>
public class BiDirectional<T>
{
    /// <summary>
    ///     Returns true if Path exists from source to destination.
    /// </summary>
    public bool PathExists(IDiGraph<T> graph, T source, T destination)
    {
        return Bfs(graph, source, destination);
    }

    /// <summary>
    ///     Breadth-first search from source (out-edges) and destination (in-edges) until they meet.
    /// </summary>
    private static bool Bfs(IDiGraph<T> graph, T source, T destination)
    {
        if (source.Equals(destination)) return true;

        var visitedA = new HashSet<T>();
        var visitedB = new HashSet<T>();

        var bfsQueueA = new Queue<IDiGraphVertex<T>>();
        var bfsQueueB = new Queue<IDiGraphVertex<T>>();

        var sourceVertex = graph.GetVertex(source);
        var destVertex = graph.GetVertex(destination);

        bfsQueueA.Enqueue(sourceVertex);
        bfsQueueB.Enqueue(destVertex);

        visitedA.Add(sourceVertex.Key);
        visitedB.Add(destVertex.Key);

        while (true)
        {
            if (bfsQueueA.Count > 0)
            {
                var current = bfsQueueA.Dequeue();

                if (visitedB.Contains(current.Key)) return true;

                foreach (var edge in current.OutEdges)
                {
                    if (visitedA.Contains(edge.TargetVertexKey)) continue;

                    visitedA.Add(edge.TargetVertexKey);
                    bfsQueueA.Enqueue(edge.TargetVertex);
                }
            }

            if (bfsQueueB.Count > 0)
            {
                var current = bfsQueueB.Dequeue();

                if (visitedA.Contains(current.Key)) return true;

                // search backward from destination along in-edges
                foreach (var edge in current.InEdges)
                {
                    if (visitedB.Contains(edge.TargetVertexKey)) continue;

                    visitedB.Add(edge.TargetVertexKey);
                    bfsQueueB.Enqueue(edge.TargetVertex);
                }
            }

            if (bfsQueueA.Count == 0 && bfsQueueB.Count == 0) break;
        }

        return false;
    }
}
