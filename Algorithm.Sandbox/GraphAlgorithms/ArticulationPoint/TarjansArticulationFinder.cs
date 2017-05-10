using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.ArticulationPoint
{
    /// <summary>
    /// Articulation point finder using Tarjan's algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TarjansArticulationFinder<T>
    {
        /// <summary>
        /// returns a list if articulation points in this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public List<T> FindArticulationPoints(AsGraph<T> graph)
        {
            int visitTime = 0;
            return DFS(graph.ReferenceVertex, new List<T>(),
                new Dictionary<T, int>(), new Dictionary<T, int>(),
                new Dictionary<T, T>(),
                ref visitTime);
        }

        /// <summary>
        /// Do a depth first search to find articulation points by keeping track of 
        /// discovery nodes and checking for back edges using low/discovery time maps
        /// </summary>
        /// <param name="currentVertex"></param>
        /// <param name="result"></param>
        /// <param name="discovery"></param>
        /// <param name="discoveryTimeMap"></param>
        /// <param name="lowTimeMap"></param>
        /// <param name="parent"></param>
        /// <param name="discoveryTime"></param>
        /// <returns></returns>
        private List<T> DFS(AsGraphVertex<T> currentVertex,
             List<T> result,
             Dictionary<T, int> discoveryTimeMap, Dictionary<T, int> lowTimeMap,
             Dictionary<T, T> parent, ref int discoveryTime)
        {

            var isArticulationPoint = false;

            discoveryTimeMap.Add(currentVertex.Value, discoveryTime);
            lowTimeMap.Add(currentVertex.Value, discoveryTime);

            //discovery childs in this iteration
            var discoveryChildCount = 0;

            foreach (var edge in currentVertex.Edges)
            {
                if (!discoveryTimeMap.ContainsKey(edge.Value))
                {
                    discoveryChildCount++;
                    parent.Add(edge.Value, currentVertex.Value);

                    discoveryTime++;
                    DFS(edge, result,
                                discoveryTimeMap, lowTimeMap, parent, ref discoveryTime);

                    //if neighbours lowTime is greater than current
                    //then this is an articulation point 
                    //because neighbour never had a chance to propogate any ancestors low value
                    //since this is an isolated componant
                    if (discoveryTimeMap[currentVertex.Value] <= lowTimeMap[edge.Value])
                    {
                        isArticulationPoint = true;
                    }
                    else
                    {
                        //propogate lowTime index of neighbour so that ancestors can see it in DFS
                        lowTimeMap[currentVertex.Value] =
                            Math.Min(lowTimeMap[currentVertex.Value], lowTimeMap[edge.Value]);

                    }
                }
                else
                {
                    //check if this edge target vertex is not in the current DFS path
                    //even if edge target vertex was already visisted
                    //update this so that ancestors can see it
                    if (parent.ContainsKey(currentVertex.Value) == false 
                        || !edge.Value.Equals(parent[currentVertex.Value]))
                    {
                        lowTimeMap[currentVertex.Value] =
                        Math.Min(lowTimeMap[currentVertex.Value], discoveryTimeMap[edge.Value]);
                    }
                }
            }

            //if root of DFS with two or more children
            //or visitTime of this Vertex <=lowTime of any neighbour 
            if ((parent.ContainsKey(currentVertex.Value) == false && discoveryChildCount >= 2) ||
                    (parent.ContainsKey(currentVertex.Value) && isArticulationPoint))
            {
                result.Add(currentVertex.Value);
            }


            return result;
        }
    }
}
