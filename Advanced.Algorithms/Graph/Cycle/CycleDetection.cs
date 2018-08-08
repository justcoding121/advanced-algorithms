using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// Cycle detection using Depth First Search.
    /// </summary>
    public class CycleDetector<T>
    {
        /// <summary>
        /// Returns true if a cycle exists
        /// </summary>
        public bool HasCycle(DiGraph<T> graph)
        {
            var visiting = new HashSet<T>();
            var visited = new HashSet<T>();

            foreach(var vertex in graph.Vertices)
            {
                if (!visited.Contains(vertex.Value.Value))
                {
                    if (dfs(vertex.Value, visited, visiting))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool dfs(DiGraphVertex<T> current, 
            HashSet<T> visited, HashSet<T> visiting)
        {
            visiting.Add(current.Value);

            foreach (var edge in current.OutEdges)
            {
                //if we encountered a visiting vertex again
                //then their is a cycle
                if(visiting.Contains(edge.Value))
                {
                    return true;
                }

                if (visited.Contains(edge.Value))
                {
                    continue;
                }

                if(dfs(edge, visited, visiting))
                {
                    return true;
                }

            }

            visiting.Remove(current.Value);
            visited.Add(current.Value);

            return false;
        }
    }
}
