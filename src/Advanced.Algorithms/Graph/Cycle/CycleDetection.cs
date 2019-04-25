using Advanced.Algorithms.DataStructures.Graph;
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
        public bool HasCycle(IDiGraph<T> graph)
        {
            var visiting = new HashSet<T>();
            var visited = new HashSet<T>();

            foreach(var vertex in graph.VerticesAsEnumberable)
            {
                if (!visited.Contains(vertex.Key))
                {
                    if (dfs(vertex, visited, visiting))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool dfs(IDiGraphVertex<T> current, 
            HashSet<T> visited, HashSet<T> visiting)
        {
            visiting.Add(current.Key);

            foreach (var edge in current.OutEdges)
            {
                //if we encountered a visiting vertex again
                //then their is a cycle
                if(visiting.Contains(edge.TargetVertexKey))
                {
                    return true;
                }

                if (visited.Contains(edge.TargetVertexKey))
                {
                    continue;
                }

                if(dfs(edge.TargetVertex, visited, visiting))
                {
                    return true;
                }

            }

            visiting.Remove(current.Key);
            visited.Add(current.Key);

            return false;
        }
    }
}
