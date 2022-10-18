using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     Uses dynamic programming for a
///     psuedo-polynomial time runTime complexity for this NP hard problem.
/// </summary>
public class TravellingSalesman<T, TW> where TW : IComparable
{
    private IShortestPathOperators<TW> @operator;

    public TW FindMinWeight(IGraph<T> graph, IShortestPathOperators<TW> @operator)
    {
        this.@operator = @operator;
        if (this.@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (this.@operator.DefaultValue.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IShortestPathOperators<int> operator implementation during initialization.");

        return FindMinWeight(graph.ReferenceVertex, graph.ReferenceVertex,
            graph.VerticesCount,
            new HashSet<IGraphVertex<T>>(),
            new Dictionary<string, TW>());
    }

    private TW FindMinWeight(IGraphVertex<T> sourceVertex,
        IGraphVertex<T> tgtVertex,
        int remainingVertexCount,
        HashSet<IGraphVertex<T>> visited,
        Dictionary<string, TW> cache)
    {
        var cacheKey = $"{sourceVertex.Key}-{remainingVertexCount}";

        if (cache.ContainsKey(cacheKey)) return cache[cacheKey];

        visited.Add(sourceVertex);

        var results = new List<TW>();

        foreach (var edge in sourceVertex.Edges)
        {
            //base case
            if (edge.TargetVertex.Equals(tgtVertex)
                && remainingVertexCount == 1)
            {
                results.Add(edge.Weight<TW>());
                break;
            }

            if (!visited.Contains(edge.TargetVertex))
            {
                var result = FindMinWeight(edge.TargetVertex, tgtVertex, remainingVertexCount - 1, visited, cache);

                if (!result.Equals(@operator.MaxValue)) results.Add(@operator.Sum(result, edge.Weight<TW>()));
            }
        }

        visited.Remove(sourceVertex);

        if (results.Count == 0) return @operator.MaxValue;

        var min = results.Min();
        cache.Add(cacheKey, min);
        return min;
    }
}