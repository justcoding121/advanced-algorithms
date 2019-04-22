using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.Sorting;
using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A Kruskal's alogorithm implementation
    /// using merge sort and disjoint set.
    /// </summary>
    public class Kruskals<T, W> where W : IComparable
    {
        /// <summary>
        /// Find Minimum Spanning Tree of given weighted graph.
        /// </summary>
        /// <returns>List of MST edges</returns>
        public List<MSTEdge<T, W>>
            FindMinimumSpanningTree(IGraph<T> graph)
        {
            var edges = new List<MSTEdge<T, W>>();

            //gather all unique edges
            dfs(graph.ReferenceVertex, new HashSet<T>(),
                new Dictionary<T, HashSet<T>>(),
                edges);

            //quick sort preparation
            var sortArray = new MSTEdge<T, W>[edges.Count];
            for (int i = 0; i < edges.Count; i++)
            {
                sortArray[i] = edges[i];
            }

            //quick sort edges
            var sortedEdges = MergeSort<MSTEdge<T, W>>.Sort(sortArray);

            var result = new List<MSTEdge<T, W>>();
            var disJointSet = new DisJointSet<T>();

            //create set
            foreach (var vertex in graph)
            {
                disJointSet.MakeSet(vertex.Value);
            }

            //pick each edge one by one
            //if both source and target belongs to same set 
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
        /// Do DFS to find all unique edges.
        /// </summary>
        private void dfs(IGraphVertex<T> currentVertex, HashSet<T> visitedVertices, Dictionary<T, HashSet<T>> visitedEdges,
            List<MSTEdge<T, W>> result)
        {
            if (!visitedVertices.Contains(currentVertex.Value))
            {
                visitedVertices.Add(currentVertex.Value);

                foreach (var edge in currentVertex.Edges)
                {
                    if (!visitedEdges.ContainsKey(currentVertex.Value)
                        || !visitedEdges[currentVertex.Value].Contains(edge.Value))
                    {
                        result.Add(new MSTEdge<T, W>(currentVertex.Value, edge.Value, edge.Weight<W>()));

                        //update visited edge
                        if (!visitedEdges.ContainsKey(currentVertex.Value))
                        {
                            visitedEdges.Add(currentVertex.Value, new HashSet<T>());
                        }

                        visitedEdges[currentVertex.Value].Add(edge.Value);

                        //update visited back edge
                        if (!visitedEdges.ContainsKey(edge.Value))
                        {
                            visitedEdges.Add(edge.Value, new HashSet<T>());
                        }

                        visitedEdges[edge.Value].Add(currentVertex.Value);
                    }

                    dfs(edge.Target, visitedVertices, visitedEdges, result);
                }

            }

        }

    }

    /// <summary>
    /// Minimum spanning tree edge object.
    /// </summary>
    public class MSTEdge<T, W> : IComparable where W : IComparable
    {
        public T Source { get; }
        public T Destination { get; }
        public W Weight { get; }

        internal MSTEdge(T source, T dest, W weight)
        {
            Source = source;
            Destination = dest;
            Weight = weight;
        }
        public int CompareTo(object obj)
        {
            return Weight.CompareTo(((MSTEdge<T, W>)obj).Weight);
        }
    }
}
