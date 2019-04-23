
using Advanced.Algorithms.DataStructures.Graph;
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

        public W FindMinWeight(IGraph<T> graph, IShortestPathOperators<W> @operator)
        {
            this.@operator = @operator;
            if (this.@operator == null)
            {
                throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");
            }

            if (!graph.IsWeightedGraph)
            {
                if (this.@operator.DefaultValue.GetType() != typeof(int))
                {
                    throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                        "Provide an appropriate IShortestPathOperators<int> operator implementation during initialization.");
                }
            }

            return findMinWeight(graph.ReferenceVertex, graph.ReferenceVertex,
                                graph.VerticesCount,
                                new HashSet<IGraphVertex<T>>(),
                                new Dictionary<string, W>());
        }

        private W findMinWeight(IGraphVertex<T> sourceVertex,
            IGraphVertex<T> tgtVertex,
            int remainingVertexCount,
            HashSet<IGraphVertex<T>> visited,
            Dictionary<string, W> cache)
        {
            var cacheKey = $"{sourceVertex.Key}-{remainingVertexCount}";

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            visited.Add(sourceVertex);

            var results = new List<W>();

            foreach (var edge in sourceVertex.Edges)
            {
                //base case
                if (edge.TargetVertex.Equals(tgtVertex)
                    && remainingVertexCount == 1)
                {
                    results.Add(edge.Weight<W>());
                    break;
                }

                if (!visited.Contains(edge.TargetVertex))
                {
                    var result = findMinWeight(edge.TargetVertex, tgtVertex, remainingVertexCount - 1, visited, cache);

                    if (!result.Equals(@operator.MaxValue))
                    {
                        results.Add(@operator.Sum(result, edge.Weight<W>()));
                    }

                }
            }

            visited.Remove(sourceVertex);

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
