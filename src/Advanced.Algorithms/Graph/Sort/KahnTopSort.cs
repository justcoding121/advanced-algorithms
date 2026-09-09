using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Find Toplogical order of a graph using Kahn's algorithm.
/// </summary>
public class KahnsTopSort<T>
{
    /// <summary>
    ///     Returns the vertices in Topologically Sorted Order.
    /// </summary>
    public List<T> GetTopSort(IDiGraph<T> graph)
    {
        var inEdgeMap = new Dictionary<T, int>();

        var kahnQueue = new Queue<T>();

        foreach (var vertex in graph.VerticesAsEnumberable)
        {
            inEdgeMap.Add(vertex.Key, vertex.InEdgeCount);

            //init queue with vertices having not in edges
            if (vertex.InEdgeCount == 0) kahnQueue.Enqueue(vertex.Key);
        }

        //no vertices with zero number of in edges
        if (graph.VerticesCount > 0 && kahnQueue.Count == 0)
            throw new InvalidOperationException("Graph has a cycle.");

        var result = new List<T>();

        //until queue is empty
        while (kahnQueue.Count > 0)
        {
            var nextPick = graph.GetVertex(kahnQueue.Dequeue());
            result.Add(nextPick.Key);

            //decrement in edge count for neighbours; enqueue when indegree hits 0
            foreach (var edge in nextPick.OutEdges)
            {
                inEdgeMap[edge.TargetVertexKey]--;
                if (inEdgeMap[edge.TargetVertexKey] == 0) kahnQueue.Enqueue(edge.TargetVertexKey);
            }
        }

        if (result.Count != graph.VerticesCount)
            throw new InvalidOperationException("Graph has a cycle.");

        return result;
    }
}
