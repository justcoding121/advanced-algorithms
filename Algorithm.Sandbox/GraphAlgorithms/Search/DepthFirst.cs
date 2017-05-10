using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.Search
{
    /// <summary>
    /// Depth First Search
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DepthFirst<T>
    {
        /// <summary>
        /// Returns true if item exists
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool Find(AsGraph<T> graph, T vertex)
        {
            return DFS(graph.ReferenceVertex, new HashSet<T>(), vertex);
        }

        /// <summary>
        /// Recursive DFS
        /// </summary>
        /// <param name="current"></param>
        /// <param name="visited"></param>
        /// <param name="searchVetex"></param>
        /// <returns></returns>
        private bool DFS(AsGraphVertex<T> current,
            HashSet<T> visited, T searchVetex)
        {
            visited.Add(current.Value);

            if (current.Value.Equals(searchVetex))
            {
                return true;
            }

            foreach (var edge in current.Edges)
            {
                if (!visited.Contains(edge.Value))
                {
                    if (DFS(edge, visited, searchVetex))
                    {
                        return true;
                    }

                }
            }

            return false;
        }
    }
}
