using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return DFS(graph.ReferenceVertex, new AsHashSet<T>(), vertex);
        }

        /// <summary>
        /// Recursive DFS
        /// </summary>
        /// <param name="current"></param>
        /// <param name="visited"></param>
        /// <param name="searchVetex"></param>
        /// <returns></returns>
        private bool DFS(AsGraphVertex<T> current,
            AsHashSet<T> visited, T searchVetex)
        {
            visited.Add(current.Value);

            if (current.Value.Equals(searchVetex))
            {
                return true;
            }

            foreach (var edge in current.Edges)
            {
                if (!visited.Contains(edge.Value.Value))
                {
                    if (DFS(edge.Value, visited, searchVetex))
                    {
                        return true;
                    }

                }
            }

            return false;
        }
    }
}
