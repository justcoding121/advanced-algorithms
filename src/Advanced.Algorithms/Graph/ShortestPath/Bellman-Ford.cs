using Advanced.Algorithms.DataStructures.Graph;
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
        /// Find shortest distance to target.
        /// </summary>
        public ShortestPathResult<T, W> FindShortestPath(IDiGraph<T> graph,
            T source, T destination)
        {
            //regular argument checks
            if (graph == null || graph.GetVertex(source) == null
                || graph.GetVertex(destination) == null)
            {
                throw new ArgumentException();
            }

            var progress = new Dictionary<T, W>();
            var parentMap = new Dictionary<T, T>();

            foreach (var vertex in graph)
            {
                parentMap.Add(vertex.Value, default(T));
                progress.Add(vertex.Value, operators.MaxValue);
            }

            progress[source] = operators.DefaultValue;

            var iterations = graph.VerticesCount - 1;
            var updated = true;

            while (iterations > 0 && updated)
            {
                updated = false;

                foreach (var vertex in graph)
                {
                    //skip not discovered nodes
                    if (progress[vertex.Value].Equals(operators.MaxValue))
                    {
                        continue;
                    }

                    foreach (var edge in vertex.OutEdges)
                    {
                        var currentDistance = progress[edge.Value];
                        var newDistance = operators.Sum(progress[vertex.Value],
                            vertex.GetOutEdge(edge.Target).Weight<W>());

                        if (newDistance.CompareTo(currentDistance) < 0)
                        {
                            updated = true;
                            progress[edge.Value] = newDistance;
                            parentMap[edge.Value] = vertex.Value;
                        }

                    }
                }

                iterations--;

                if (iterations < 0)
                {
                    throw new Exception("Negative cycle exists in this graph.");
                }
            }

            return tracePath(graph, parentMap, source, destination);
        }

        /// <summary>
        /// Trace back path from destination to source using parent map.
        /// </summary>
        private ShortestPathResult<T, W> tracePath(IDiGraph<T> graph,
            Dictionary<T, T> parentMap, T source, T destination)
        {
            //trace the path
            var pathStack = new Stack<T>();

            pathStack.Push(destination);

            var currentV = destination;
            while (!Equals(currentV, default(T)) && !Equals(parentMap[currentV], default(T)))
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
                    graph.GetVertex(resultPath[i]).GetOutEdge(graph.GetVertex(resultPath[i + 1])).Weight<W>());
            }

            return new ShortestPathResult<T, W>(resultPath, resultLength);
        }
    }
}
