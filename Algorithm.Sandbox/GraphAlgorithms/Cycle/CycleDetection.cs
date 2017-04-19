using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;

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
            var visiting = new AsHashSet<T>();
            var visited = new AsHashSet<T>();

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
            AsHashSet<T> visited, AsHashSet<T> visiting)
        {
            visiting.Add(current.Value);

            foreach (var edge in current.OutEdges)
            {
                //if we encountered a visiting vertex again
                //then their is a cycle
                if(visiting.Contains(edge.Value.Value))
                {
                    return true;
                }
             
                if (!visited.Contains(edge.Value.Value))
                {
                    if(DFS(edge.Value, visited, visiting))
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
