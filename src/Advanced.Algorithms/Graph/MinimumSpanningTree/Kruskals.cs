using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.Sorting;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A Kruskal's alogorithm implementation
///     using merge sort and disjoint set.
/// </summary>
public class Kruskals<T, TW> where TW : IComparable
{
    /// <summary>
    ///     Find Minimum Spanning Tree of given weighted graph.
    /// </summary>
    /// <returns>List of MST edges</returns>
    public List<MstEdge<T, TW>>
        FindMinimumSpanningTree(IGraph<T> graph)
    {
        var edges = new List<MstEdge<T, TW>>();

        //gather all unique edges
        Dfs(graph.ReferenceVertex, new HashSet<T>(),
            new Dictionary<T, HashSet<T>>(),
            edges);

        //quick sort preparation
        var sortArray = new MstEdge<T, TW>[edges.Count];
        for (var i = 0; i < edges.Count; i++) sortArray[i] = edges[i];

        //quick sort edges
        var sortedEdges = MergeSort<MstEdge<T, TW>>.Sort(sortArray);

        var result = new List<MstEdge<T, TW>>();
        var disJointSet = new DisJointSet<T>();

        //create set
        foreach (var vertex in graph.VerticesAsEnumberable) disJointSet.MakeSet(vertex.Key);

        //pick each edge one by one
        //if both source and target belongs to same set 
        //then don't add the edge to result
        //otherwise add it to result and union sets
        for (var i = 0; i < edges.Count; i++)
        {
            var currentEdge = sortedEdges[i];

            var setA = disJointSet.FindSet(currentEdge.Source);
            var setB = disJointSet.FindSet(currentEdge.Destination);

            //can't pick edge with both ends already in MST
            if (setA.Equals(setB)) continue;

            result.Add(currentEdge);

            //union picked edge vertice sets
            disJointSet.Union(setA, setB);
        }

        return result;
    }

    /// <summary>
    ///     Do DFS to find all unique edges.
    /// </summary>
    private void Dfs(IGraphVertex<T> currentVertex, HashSet<T> visitedVertices, Dictionary<T, HashSet<T>> visitedEdges,
        List<MstEdge<T, TW>> result)
    {
        if (!visitedVertices.Contains(currentVertex.Key))
        {
            visitedVertices.Add(currentVertex.Key);

            foreach (var edge in currentVertex.Edges)
            {
                if (!visitedEdges.ContainsKey(currentVertex.Key)
                    || !visitedEdges[currentVertex.Key].Contains(edge.TargetVertexKey))
                {
                    result.Add(new MstEdge<T, TW>(currentVertex.Key, edge.TargetVertexKey, edge.Weight<TW>()));

                    //update visited edge
                    if (!visitedEdges.ContainsKey(currentVertex.Key))
                        visitedEdges.Add(currentVertex.Key, new HashSet<T>());

                    visitedEdges[currentVertex.Key].Add(edge.TargetVertexKey);

                    //update visited back edge
                    if (!visitedEdges.ContainsKey(edge.TargetVertexKey))
                        visitedEdges.Add(edge.TargetVertexKey, new HashSet<T>());

                    visitedEdges[edge.TargetVertexKey].Add(currentVertex.Key);
                }

                Dfs(edge.TargetVertex, visitedVertices, visitedEdges, result);
            }
        }
    }
}

/// <summary>
///     Minimum spanning tree edge object.
/// </summary>
public class MstEdge<T, TW> : IComparable where TW : IComparable
{
    internal MstEdge(T source, T dest, TW weight)
    {
        Source = source;
        Destination = dest;
        Weight = weight;
    }

    public T Source { get; }
    public T Destination { get; }
    public TW Weight { get; }

    public int CompareTo(object obj)
    {
        return Weight.CompareTo(((MstEdge<T, TW>)obj).Weight);
    }
}