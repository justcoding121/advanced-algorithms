using Advanced.Algorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{

    /// <summary>
    /// Articulation point finder using Tarjan's algorithm.
    /// </summary>
    public class TarjansArticulationFinder<T>
    {
        /// <summary>
        /// Returns a list if articulation points in this graph.
        /// </summary>
        public List<T> FindArticulationPoints(IGraph<T> graph)
        {
            int visitTime = 0;
            return dfs(graph.ReferenceVertex, new List<T>(),
                new Dictionary<T, int>(), new Dictionary<T, int>(),
                new Dictionary<T, T>(),
                ref visitTime);
        }

        /// <summary>
        /// Do a depth first search to find articulation points by keeping track of 
        /// discovery nodes and checking for back edges using low/discovery time maps.
        /// </summary>
        private List<T> dfs(IGraphVertex<T> currentVertex,
             List<T> result,
             Dictionary<T, int> discoveryTimeMap, Dictionary<T, int> lowTimeMap,
             Dictionary<T, T> parent, ref int discoveryTime)
        {

            var isArticulationPoint = false;

            discoveryTimeMap.Add(currentVertex.Key, discoveryTime);
            lowTimeMap.Add(currentVertex.Key, discoveryTime);

            //discovery childs in this iteration
            var discoveryChildCount = 0;

            foreach (var edge in currentVertex.Edges)
            {
                if (!discoveryTimeMap.ContainsKey(edge.TargetVertexKey))
                {
                    discoveryChildCount++;
                    parent.Add(edge.TargetVertexKey, currentVertex.Key);

                    discoveryTime++;
                    dfs(edge.TargetVertex, result,
                                discoveryTimeMap, lowTimeMap, parent, ref discoveryTime);

                    //if neighbours lowTime is greater than current
                    //then this is an articulation point 
                    //because neighbour never had a chance to propogate any ancestors low value
                    //since this is an isolated componant
                    if (discoveryTimeMap[currentVertex.Key] <= lowTimeMap[edge.TargetVertexKey])
                    {
                        isArticulationPoint = true;
                    }
                    else
                    {
                        //propogate lowTime index of neighbour so that ancestors can see it in DFS
                        lowTimeMap[currentVertex.Key] =
                            Math.Min(lowTimeMap[currentVertex.Key], lowTimeMap[edge.TargetVertexKey]);

                    }
                }
                else
                {
                    //check if this edge target vertex is not in the current DFS path
                    //even if edge target vertex was already visisted
                    //update this so that ancestors can see it
                    if (parent.ContainsKey(currentVertex.Key) == false
                        || !edge.TargetVertexKey.Equals(parent[currentVertex.Key]))
                    {
                        lowTimeMap[currentVertex.Key] =
                        Math.Min(lowTimeMap[currentVertex.Key], discoveryTimeMap[edge.TargetVertexKey]);
                    }
                }
            }

            //if root of DFS with two or more children
            //or visitTime of this Vertex <=lowTime of any neighbour 
            if (parent.ContainsKey(currentVertex.Key) == false && discoveryChildCount >= 2 ||
                    parent.ContainsKey(currentVertex.Key) && isArticulationPoint)
            {
                result.Add(currentVertex.Key);
            }


            return result;
        }
    }
}
