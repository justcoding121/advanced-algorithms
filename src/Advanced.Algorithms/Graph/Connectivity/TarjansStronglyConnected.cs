using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// Strongly connected using Tarjan's algorithm.
    /// </summary>
    public class TarjansStronglyConnected<T>
    {
        /// <summary>
        /// Rreturns a list of Strongly Connected components in this graph.
        /// </summary>
        public List<List<T>> FindStronglyConnectedComponents(IDiGraph<T> graph)
        {
            var result = new List<List<T>>();

            var discoveryTimeMap = new Dictionary<T, int>();
            var lowTimeMap = new Dictionary<T, int>();
            var pathStack = new Stack<T>();
            var pathStackMap = new HashSet<T>();
            var discoveryTime = 0;
            foreach (var vertex in graph.VerticesAsEnumberable)
            {
                if (!discoveryTimeMap.ContainsKey(vertex.Key))
                {
                    DFS(vertex,
                     result,
                     discoveryTimeMap, lowTimeMap,
                     pathStack, pathStackMap, ref discoveryTime);

                }
            }

            return result;
        }

        /// <summary>
        /// Do a depth first search to find Strongly Connected by keeping track of 
        /// discovery nodes and checking for back edges using low/discovery time maps.
        /// </summary>
        private void DFS(IDiGraphVertex<T> currentVertex,
             List<List<T>> result,
             Dictionary<T, int> discoveryTimeMap, Dictionary<T, int> lowTimeMap,
             Stack<T> pathStack,
             HashSet<T> pathStackMap, ref int discoveryTime)
        {

            discoveryTimeMap.Add(currentVertex.Key, discoveryTime);
            lowTimeMap.Add(currentVertex.Key, discoveryTime);
            pathStack.Push(currentVertex.Key);
            pathStackMap.Add(currentVertex.Key);

            foreach (var edge in currentVertex.OutEdges)
            {
                if (!discoveryTimeMap.ContainsKey(edge.TargetVertexKey))
                {
                    discoveryTime++;
                    DFS(edge.TargetVertex, result, discoveryTimeMap, lowTimeMap,
                                pathStack, pathStackMap, ref discoveryTime);

                    //propogate lowTime index of neighbour so that ancestors can see it in DFS
                    lowTimeMap[currentVertex.Key] =
                        Math.Min(lowTimeMap[currentVertex.Key], lowTimeMap[edge.TargetVertexKey]);


                }
                else
                {
                    //ignore cross edges
                    //even if edge vertex was already visisted
                    //update this so that ancestors can see it
                    if (pathStackMap.Contains(edge.TargetVertexKey))
                    {
                        lowTimeMap[currentVertex.Key] =
                            Math.Min(lowTimeMap[currentVertex.Key],
                            discoveryTimeMap[edge.TargetVertexKey]);
                    }
                }
            }

            //if low is high this means we reached head of the DFS tree with strong connectivity
            //now print items in the stack
            if (lowTimeMap[currentVertex.Key] != discoveryTimeMap[currentVertex.Key])
            {
                return;
            }

            var strongConnected = new List<T>();
            while (!pathStack.Peek().Equals(currentVertex.Key))
            {
                var vertex = pathStack.Pop();
                strongConnected.Add(vertex);
                pathStackMap.Remove(vertex);
            }

            //add current vertex
            var finalVertex = pathStack.Pop();
            strongConnected.Add(finalVertex);
            pathStackMap.Remove(finalVertex);

            result.Add(strongConnected);
        }
    }
}
