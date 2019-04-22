using Advanced.Algorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// Bridge finder using Tarjan's algorithm.
    /// </summary>
    public class TarjansBridgeFinder<T>
    {
        /// <summary>
        /// Returns a list if Bridge points in this graph.
        /// </summary>
        public List<Bridge<T>> FindBridges(IGraph<T> graph)
        {
            int visitTime = 0;
            return dfs(graph.ReferenceVertex, new List<Bridge<T>>(),
                new Dictionary<T, int>(), new Dictionary<T, int>(),
                new Dictionary<T, T>(),
                ref visitTime);
        }

        /// <summary>
        /// Do a depth first search to find Bridge edges by keeping track of 
        /// discovery nodes and checking for back edges using low/discovery time maps.
        /// </summary>
        private List<Bridge<T>> dfs(IGraphVertex<T> currentVertex,
             List<Bridge<T>> result,
             Dictionary<T, int> discoveryTimeMap, Dictionary<T, int> lowTimeMap,
             Dictionary<T, T> parent, ref int discoveryTime)
        {

            discoveryTimeMap.Add(currentVertex.Key, discoveryTime);
            lowTimeMap.Add(currentVertex.Key, discoveryTime);

            //discovery childs in this iteration
            foreach (var edge in currentVertex.Edges)
            {
                if (!discoveryTimeMap.ContainsKey(edge.TargetVertexKey))
                {
                    parent.Add(edge.TargetVertexKey, currentVertex.Key);

                    discoveryTime++;
                    dfs(edge.TargetVertex, result,
                                discoveryTimeMap, lowTimeMap, parent, ref discoveryTime);

                    //propogate lowTime index of neighbour so that ancestors can see check for back edge
                    lowTimeMap[currentVertex.Key] =
                        Math.Min(lowTimeMap[currentVertex.Key], lowTimeMap[edge.TargetVertexKey]);

                    //if neighbours lowTime is less than current
                    //then this is an Bridge point 
                    //because neighbour never had a chance to propogate any ancestors low value
                    //since this is an isolated componant
                    if (discoveryTimeMap[currentVertex.Key] < lowTimeMap[edge.TargetVertexKey])
                    {
                        result.Add(new Bridge<T>(currentVertex.Key, edge.TargetVertexKey));
                    }

                }
                else
                {   
                    //check if this edge target vertex is not in the current DFS path
                    //even if edge target vertex was already visisted
                    //update discovery so that ancestors can see it
                    if (parent.ContainsKey(currentVertex.Key) == false
                        || !edge.TargetVertexKey.Equals(parent[currentVertex.Key]))
                    {
                        lowTimeMap[currentVertex.Key] =
                            Math.Min(lowTimeMap[currentVertex.Key], discoveryTimeMap[edge.TargetVertexKey]);
                    }
                }
            }      

            return result;
        }
    }

    /// <summary>
    /// The bridge object.
    /// </summary>
    public class Bridge<T>
    {
        public T vertexA { get; }
        public T vertexB { get; }

        public Bridge(T vertexA, T vertexB)
        {
            this.vertexA = vertexA;
            this.vertexB = vertexB;
        }
    }

}
