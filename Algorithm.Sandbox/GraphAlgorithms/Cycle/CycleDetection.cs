using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.Cycle
{
    /// <summary>
    /// Cycle detection using Depth First Search
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CycleDetector<T>
    {
        /// <summary>
        /// Returns true if a cycle exists
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public bool HasCycle(AsDiGraph<T> graph)
        {
            var visiting = new HashSet<T>();
            var visited = new HashSet<T>();

            foreach(var vertex in graph.Vertices)
            {
                if (!visited.Contains(vertex.Value.Value))
                {
                    if (DFS(vertex.Value, visited, visiting))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DFS(AsDiGraphVertex<T> current, 
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
             
                if (!visited.Contains(edge.Value))
                {
                    if(DFS(edge, visited, visiting))
                    {
                        return true;
                    }
                }
             
            }

            visiting.Remove(current.Value);
            visited.Add(current.Value);

            return false;
        }
    }
}
