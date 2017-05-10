using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.TopologicalSort
{
    /// <summary>
    /// Find Toplogical order of a graph using Kahn's algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KahnsTopSort<T>
    {
        /// <summary>
        /// Returns the vertices in Topologically Sorted Order
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public List<T> GetTopSort(AsDiGraph<T> graph)
        {
            var inEdgeMap = new Dictionary<T, int>();

            var kahnQueue = new Queue<T>();

            foreach (var vertex in graph.Vertices)
            {
                inEdgeMap.Add(vertex.Key, vertex.Value.InEdges.Count);

                //init queue with vertices having not in edges
                if(vertex.Value.InEdges.Count == 0)
                {
                    kahnQueue.Enqueue(vertex.Value.Value);
                }
            }

            //no vertices with zero number of in edges
            if (kahnQueue.Count == 0)
            {
                throw new Exception("Graph has a cycle.");
            }

            var result = new List<T>();

            int visitCount = 0;
            //until queue is empty
            while (kahnQueue.Count > 0)
            {
                //cannot exceed vertex number of iterations
                if (visitCount > graph.Vertices.Count)
                {
                    throw new Exception("Graph has a cycle.");
                }

                //pick a neighbour
                var nextPick = graph.Vertices[kahnQueue.Dequeue()];       

                //if in edge count is 0 then ready for result
                if(inEdgeMap[nextPick.Value] == 0)
                {
                    result.Add(nextPick.Value);
                }

                //decrement in edge count for neighbours
                foreach(var edge in nextPick.OutEdges)
                {
                    inEdgeMap[edge.Value]--;
                    kahnQueue.Enqueue(edge.Value);
                }

                visitCount++;
            }

            return result;
        }

      
    }
}
