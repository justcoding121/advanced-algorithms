using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.DataStructures.Set;
using Algorithm.Sandbox.Sorting;
using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.MinimumSpanningTree
{
    /// <summary>
    /// MST edge object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class MSTEdge<T, W> : IComparable where W: IComparable
    {
        public T Source { get; }
        public T Destination { get; }
        public W Weight { get;  }

        internal MSTEdge(T source, T dest, W weight)
        {
            Source = source;
            Destination = dest;
            Weight = weight;
        }
        public int CompareTo(object obj)
        {
            return Weight.CompareTo((obj as MSTEdge<T,W>).Weight);
        }
    }

    /// <summary>
    /// A Kruskal's alogorithm implementation
    /// using merge sort & disjoint set
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class Kruskals<T, W> where W : IComparable
    {
        /// <summary>
        /// Find Minimum Spanning Tree of given weighted graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>List of MST edges</returns>
        public List<MSTEdge<T,W>>
            FindMinimumSpanningTree(AsWeightedGraph<T, W> graph)
        {
            var edges = new List<MSTEdge<T,W>>();


            //gather all unique edges
            DFS(graph.ReferenceVertex, new HashSet<T>(), 
                new Dictionary<T, HashSet<T>>(),
                edges);

            //quick sort preparation
            var sortArray = new MSTEdge<T,W>[edges.Count];
            for (int i = 0; i < edges.Count; i++)
            {
                sortArray[i] = edges[i];
            }

            //quick sort edges
            var sortedEdges = MergeSort<MSTEdge<T,W>>.Sort(sortArray);

            var result = new List<MSTEdge<T,W>>();
            var disJointSet = new AsDisJointSet<T>();

            //create set
            foreach (var vertex in graph.Vertices)
            {
                disJointSet.MakeSet(vertex.Key);
            }


            //pick each edge one by one
            //if both source & target belongs to same set 
            //then don't add the edge to result
            //otherwise add it to result and union sets
            for (int i = 0; i < edges.Count; i++)
            {
                var currentEdge = sortedEdges[i];

                var setA = disJointSet.FindSet(currentEdge.Source);
                var setB = disJointSet.FindSet(currentEdge.Destination);

                //can't pick edge with both ends already in MST
                if (setA.Equals(setB))
                {
                    continue;
                }

                result.Add(currentEdge);

                //union picked edge vertice sets
                disJointSet.Union(setA, setB);
            }

            return result;
        }

        /// <summary>
        /// Do DFS to find all unique edges
        /// </summary>
        /// <param name="currentVertex"></param>
        /// <param name="visitedVertices"></param>
        /// <param name="visitedEdges"></param>
        /// <param name="result"></param>
        private void DFS(AsWeightedGraphVertex<T, W> currentVertex,
            HashSet<T> visitedVertices,
            Dictionary<T, HashSet<T>> visitedEdges,
            List<MSTEdge<T,W>> result)
        {
            if (!visitedVertices.Contains(currentVertex.Value))
            {
                visitedVertices.Add(currentVertex.Value);

                foreach (var edge in currentVertex.Edges)
                {
                    if (!visitedEdges.ContainsKey(currentVertex.Value)
                        || !visitedEdges[currentVertex.Value].Contains(edge.Key.Value))
                    {
                        result.Add(new MSTEdge<T, W>(currentVertex.Value, edge.Key.Value, edge.Value));

                        //update visited edge
                        if(!visitedEdges.ContainsKey(currentVertex.Value))
                        {
                            visitedEdges.Add(currentVertex.Value, new HashSet<T>());
                        }

                        visitedEdges[currentVertex.Value].Add(edge.Key.Value);

                        //update visited back edge
                        if (!visitedEdges.ContainsKey(edge.Key.Value))
                        {
                            visitedEdges.Add(edge.Key.Value, new HashSet<T>());
                        }

                        visitedEdges[edge.Key.Value].Add(currentVertex.Value);
                    }


                    DFS(edge.Key, visitedVertices, visitedEdges, result);
                }

            }

        
        }

    }
}
