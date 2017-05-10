using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.TopologicalSort
{
    /// <summary>
    /// Find Toplogical order of a graph using Depth First Search
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DepthFirstTopSort<T>
    {
        /// <summary>
        /// Returns the vertices in Topologically Sorted Order
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public List<T> GetTopSort(AsDiGraph<T> graph)
        {
            var pathStack = new Stack<T>();
            var visited = new HashSet<T>();

            //we need a loop so that we can reach all vertices
            foreach(var vertex in graph.Vertices)
            {
                if(!visited.Contains(vertex.Key))
                {
                    DFS(vertex.Value, visited, pathStack);
                }
                
            }

            //now just pop the stack to result
            var result = new List<T>();
            while(pathStack.Count > 0)
            {
                result.Add(pathStack.Pop());
            }

            return result;
        }

        /// <summary>
        /// Do a depth first search
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="visited"></param>
        /// <param name="pathStack"></param>
        private void DFS(AsDiGraphVertex<T> vertex, 
            HashSet<T> visited, Stack<T> pathStack)
        {
            visited.Add(vertex.Value);

            foreach(var edge in vertex.OutEdges)
            {
                if(!visited.Contains(edge.Value))
                {
                    DFS(edge, visited, pathStack);
                }
            }

            //add vertex to stack after all edges are visited
            pathStack.Push(vertex.Value);
        }
    }
}
