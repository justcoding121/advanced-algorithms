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
        readonly IShortestPathOperators<W> @operator;
        public BellmanFordShortestPath(IShortestPathOperators<W> @operator)
        {
            this.@operator = @operator;
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
                throw new ArgumentException("Empty Graph or invalid source/destination.");
            }

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

            var progress = new Dictionary<T, W>();
            var parentMap = new Dictionary<T, T>();

            foreach (var vertex in graph.VerticesAsEnumberable)
            {
                parentMap.Add(vertex.Key, default(T));
                progress.Add(vertex.Key, @operator.MaxValue);
            }

            progress[source] = @operator.DefaultValue;

            var iterations = graph.VerticesCount - 1;
            var updated = true;

            while (iterations > 0 && updated)
            {
                updated = false;

                foreach (var vertex in graph.VerticesAsEnumberable)
                {
                    //skip not discovered nodes
                    if (progress[vertex.Key].Equals(@operator.MaxValue))
                    {
                        continue;
                    }

                    foreach (var edge in vertex.OutEdges)
                    {
                        var currentDistance = progress[edge.TargetVertexKey];
                        var newDistance = @operator.Sum(progress[vertex.Key],
                            vertex.GetOutEdge(edge.TargetVertex).Weight<W>());

                        if (newDistance.CompareTo(currentDistance) < 0)
                        {
                            updated = true;
                            progress[edge.TargetVertexKey] = newDistance;
                            parentMap[edge.TargetVertexKey] = vertex.Key;
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
            var resultLength = @operator.DefaultValue;
            while (pathStack.Count > 0)
            {
                resultPath.Add(pathStack.Pop());
            }

            for (var i = 0; i < resultPath.Count - 1; i++)
            {
                resultLength = @operator.Sum(resultLength,
                    graph.GetVertex(resultPath[i]).GetOutEdge(graph.GetVertex(resultPath[i + 1])).Weight<W>());
            }

            return new ShortestPathResult<T, W>(resultPath, resultLength);
        }
    }
}
