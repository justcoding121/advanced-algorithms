using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A Bellman Ford algorithm implementation.
    /// </summary>
    public class BellmanFordShortestPath<T, W> where W : IComparable
    {
        readonly IShortestPathOperators<W> operators;
        public BellmanFordShortestPath(IShortestPathOperators<W> operators)
        {
            this.operators = operators;
        }

        /// <summary>
        /// Get shortest distance to target.
        /// </summary>
        public ShortestPathResult<T, W> GetShortestPath(WeightedDiGraph<T, W> graph, T source, T destination)
        {
            //regular argument checks
            if (graph == null || graph.FindVertex(source) == null
                || graph.FindVertex(destination) == null)
            {
                throw new ArgumentException();
            }

            var progress = new Dictionary<T, W>();
            var parentMap = new Dictionary<T, T>();

            foreach (var vertex in graph.Vertices)
            {
                parentMap.Add(vertex.Key, default(T));
                progress.Add(vertex.Key, operators.MaxValue);
            }

            progress[source] = operators.DefaultValue;

            var iterations = graph.Vertices.Count - 1;
            var updated = true;

            while (iterations > 0 && updated)
            {
                updated = false;

                foreach (var vertex in graph.Vertices)
                {
                    //skip not discovered nodes
                    if (progress[vertex.Key].Equals(operators.MaxValue))
                    {
                        continue;
                    }

                    foreach (var edge in vertex.Value.OutEdges)
                    {
                        var currentDistance = progress[edge.Key.Value];
                        var newDistance = operators.Sum(progress[vertex.Key],
                            vertex.Value.OutEdges[edge.Key]);

                        if (newDistance.CompareTo(currentDistance) < 0)
                        {
                            updated = true;
                            progress[edge.Key.Value] = newDistance;
                            parentMap[edge.Key.Value] = vertex.Key;
                        }

                    }
                }

                iterations--;

                if(iterations < 0)
                {
                    throw new Exception("Negative cycle exists in this graph.");
                }
            }

            return tracePath(graph, parentMap, source, destination);
        }

        /// <summary>
        /// Trace back path from destination to source using parent map.
        /// </summary>
        private ShortestPathResult<T, W> tracePath(WeightedDiGraph<T, W> graph,
            Dictionary<T, T> parentMap, T source, T destination)
        {
            //trace the path
            var pathStack = new Stack<T>();

            pathStack.Push(destination);

            var currentV = destination;
            while (!Equals(currentV, default(T)) && !Equals(parentMap[currentV],default(T)))
            {
                pathStack.Push(parentMap[currentV]);
                currentV = parentMap[currentV];
            }

            //return result
            var resultPath = new List<T>();
            var resultLength = operators.DefaultValue;
            while (pathStack.Count > 0)
            {
                resultPath.Add(pathStack.Pop());
            }

            for (var i = 0; i < resultPath.Count - 1; i++)
            {
                resultLength = operators.Sum(resultLength,
                    graph.Vertices[resultPath[i]].OutEdges[graph.Vertices[resultPath[i + 1]]]);
            }

            return new ShortestPathResult<T, W>(resultPath, resultLength);
        }
    }
}
