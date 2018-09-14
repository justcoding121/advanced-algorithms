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
        /// Rreturns a list if Strongly Connected components in this graph.
        /// </summary>
        public List<List<T>> FindStronglyConnectedComponents(DiGraph<T> graph)
        {
            var result = new List<List<T>>();

            var discoveryTimeMap = new Dictionary<T, int>();
            var lowTimeMap = new Dictionary<T, int>();
            var pathStack = new Stack<T>();
            var pathStackMap = new HashSet<T>();
            var discoveryTime = 0;
            foreach (var vertex in graph.Vertices)
            {
                if (!discoveryTimeMap.ContainsKey(vertex.Key))
                {
                    DFS(vertex.Value,
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
        private void DFS(DiGraphVertex<T> currentVertex,
             List<List<T>> result,
             Dictionary<T, int> discoveryTimeMap, Dictionary<T, int> lowTimeMap,
             Stack<T> pathStack,
             HashSet<T> pathStackMap, ref int discoveryTime)
        {

            discoveryTimeMap.Add(currentVertex.Value, discoveryTime);
            lowTimeMap.Add(currentVertex.Value, discoveryTime);
            pathStack.Push(currentVertex.Value);
            pathStackMap.Add(currentVertex.Value);

            foreach (var edge in currentVertex.OutEdges)
            {
                if (!discoveryTimeMap.ContainsKey(edge.Value))
                {
                    discoveryTime++;
                    DFS(edge, result, discoveryTimeMap, lowTimeMap,
                                pathStack, pathStackMap, ref discoveryTime);

                    //propogate lowTime index of neighbour so that ancestors can see it in DFS
                    lowTimeMap[currentVertex.Value] =
                        Math.Min(lowTimeMap[currentVertex.Value], lowTimeMap[edge.Value]);


                }
                else
                {
                    //ignore cross edges
                    //even if edge vertex was already visisted
                    //update this so that ancestors can see it
                    if (pathStackMap.Contains(edge.Value))
                    {
                        lowTimeMap[currentVertex.Value] =
                            Math.Min(lowTimeMap[currentVertex.Value],
                            discoveryTimeMap[edge.Value]);
                    }
                }
            }

            //if low is high this means we reached head of the DFS tree with strong connectivity
            //now print items in the stack
            if (lowTimeMap[currentVertex.Value] != discoveryTimeMap[currentVertex.Value])
            {
                return;
            }

            var strongConnected = new List<T>();
            while (!pathStack.Peek().Equals(currentVertex.Value))
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
