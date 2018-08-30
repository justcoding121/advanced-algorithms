
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// Uses dynamic programming for a
    /// psuedo-polynomial time runTime complexity for this NP hard problem.
    /// </summary>
    public class TravellingSalesman
    {
        public static int GetMinWeight(WeightedDiGraph<int, int> graph)
        {
            return getMinWeight(graph.ReferenceVertex, graph.ReferenceVertex,
                                graph.VerticesCount,
                                new HashSet<WeightedDiGraphVertex<int, int>>(),
                                new Dictionary<string, int>());
        }

        private static int getMinWeight(WeightedDiGraphVertex<int, int> currentVertex,
            WeightedDiGraphVertex<int, int> tgtVertex,
            int remainingVertexCount,
            HashSet<WeightedDiGraphVertex<int, int>> visited,
            Dictionary<string, int> cache)
        {
            var cacheKey = $"{currentVertex.Value}-{remainingVertexCount}";

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            visited.Add(currentVertex);

            var results = new List<int>();

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
                    var result = getMinWeight(vertex.Key, tgtVertex, remainingVertexCount - 1, visited, cache);

                    if (result != int.MaxValue)
                    {
                        results.Add(result + vertex.Value);
                    }

                }
            }

            visited.Remove(currentVertex);

            if (results.Count == 0)
            {
                return int.MaxValue;
            }

            var min = results.Min();
            cache.Add(cacheKey, min);
            return min;
        }
    }
}
