using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.Search
{
    /// <summary>
    /// Bread First Search implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BreadthFirst<T>
    {
        /// <summary>
        /// Returns true if item exists
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool Find(AsGraph<T> graph, T vertex)
        {
            return BFS(graph.ReferenceVertex, new HashSet<T>(), vertex);
        }

        /// <summary>
        /// BFS implementation
        /// </summary>
        /// <param name="referenceVertex"></param>
        /// <param name="HashSet"></param>
        /// <param name="searchVertex"></param>
        /// <returns></returns>
        private bool BFS(GraphVertex<T> referenceVertex,
            HashSet<T> visited, T searchVertex)
        {
            var bfsQueue = new Queue<GraphVertex<T>>();

            bfsQueue.Enqueue(referenceVertex);
            visited.Add(referenceVertex.Value);

            while (bfsQueue.Count > 0)
            {
                var current = bfsQueue.Dequeue();

                if (current.Value.Equals(searchVertex))
                {
                    return true;
                }

                foreach (var edge in current.Edges)
                {
                    if (!visited.Contains(edge.Value))
                    {
                        visited.Add(edge.Value);
                        bfsQueue.Enqueue(edge);
                    }
                }
            }

            return false;
        }
    }
}
