using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem details below
    /// https://en.wikipedia.org/wiki/Travelling_salesman_problem
    /// </summary>
    public class TravellingSalesman
    {
        public static int GetMinWeight(AsWeightedDiGraph<int, int> graph)
        {
            return GetMinWeight(graph.ReferenceVertex, graph.ReferenceVertex,
                                graph.VerticesCount,
                                new HashSet<WeightedDiGraphVertex<int, int>>(),
                                new Dictionary<string, int>());
        }

        public static int GetMinWeight(WeightedDiGraphVertex<int, int> currentVertex,
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
                    var result = GetMinWeight(vertex.Key, tgtVertex, remainingVertexCount - 1, visited, cache);

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
