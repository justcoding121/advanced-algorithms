using Advanced.Algorithms.DataStructures.Graph;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// Depth First Search.
    /// </summary>
    public class DepthFirst<T>
    {
        /// <summary>
        /// Returns true if item exists.
        /// </summary>
        public bool Find(IGraph<T> graph, T vertex)
        {
            return dfs(graph.ReferenceVertex, new HashSet<T>(), vertex);
        }

        /// <summary>
        /// Recursive DFS.
        /// </summary>
        private bool dfs(IGraphVertex<T> current,
            HashSet<T> visited, T searchVetex)
        {
            visited.Add(current.Value);

            if (current.Value.Equals(searchVetex))
            {
                return true;
            }

            foreach (var edge in current.Edges)
            {
                if (visited.Contains(edge.Value))
                {
                    continue;
                }

                if (dfs(edge.Target, visited, searchVetex))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
