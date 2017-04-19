using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public AsArrayList<T> GetTopSort(AsDiGraph<T> graph)
        {
            var pathStack = new AsStack<T>();
            var visited = new AsHashSet<T>();

            //we need a loop so that we can reach all vertices
            foreach(var vertex in graph.Vertices)
            {
                if(!visited.Contains(vertex.Key))
                {
                    DFS(vertex.Value, visited, pathStack);
                }
                
            }

            //now just pop the stack to result
            var result = new AsArrayList<T>();
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
            AsHashSet<T> visited, AsStack<T> pathStack)
        {
            visited.Add(vertex.Value);

            foreach(var edge in vertex.OutEdges)
            {
                if(!visited.Contains(edge.Value.Value))
                {
                    DFS(edge.Value, visited, pathStack);
                }
            }

            //add vertex to stack after all edges are visited
            pathStack.Push(vertex.Value);
        }
    }
}
