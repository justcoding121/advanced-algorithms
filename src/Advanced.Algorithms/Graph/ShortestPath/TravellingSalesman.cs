
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// Uses dynamic programming for a
    /// psuedo-polynomial time runTime complexity for this NP hard problem.
    /// </summary>
    public class TravellingSalesman<T, W> where W : IComparable
    {
        IShortestPathOperators<W> @operator;

        public W FindMinWeight(WeightedDiGraph<T, W> graph, IShortestPathOperators<W> @operator)
        {
            this.@operator = @operator;

            return findMinWeight(graph.ReferenceVertex, graph.ReferenceVertex,
                                graph.VerticesCount,
                                new HashSet<WeightedDiGraphVertex<T, W>>(),
                                new Dictionary<string, W>());
        }

        private W findMinWeight(WeightedDiGraphVertex<T, W> currentVertex,
            WeightedDiGraphVertex<T, W> tgtVertex,
            int remainingVertexCount,
            HashSet<WeightedDiGraphVertex<T, W>> visited,
            Dictionary<string, W> cache)
        {
            var cacheKey = $"{currentVertex.Value}-{remainingVertexCount}";

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            visited.Add(currentVertex);

            var results = new List<W>();

            foreach (var vertex in currentVertex.OutEdges)
            {
                //base case
                if (vertex.Key == tgtVertex
                    && remainingVertexCount == 1)
                {
                    results.Add(vertex.Value);
                    break;
                }

                if (!visited.Contains(vertex.Key))
                {
                    var result = findMinWeight(vertex.Key, tgtVertex, remainingVertexCount - 1, visited, cache);

                    if (!result.Equals(@operator.MaxValue))
                    {
                        results.Add(@operator.Sum(result, vertex.Value));
                    }

                }
            }

            visited.Remove(currentVertex);

            if (results.Count == 0)
            {
                return @operator.MaxValue;
            }

            var min = results.Min();
            cache.Add(cacheKey, min);
            return min;
        }
    }
}
