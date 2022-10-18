using System;
using System.Collections.Generic;
using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A Prims algorithm implementation.
/// </summary>
public class Prims<T, TW> where TW : IComparable
{
    /// <summary>
    ///     Find Minimum Spanning Tree of given undirected graph.
    /// </summary>
    /// <returns>List of MST edges</returns>
    public List<MstEdge<T, TW>>
        FindMinimumSpanningTree(IGraph<T> graph)
    {
        var edges = new List<MstEdge<T, TW>>();

        //gather all unique edges
        Dfs(graph, graph.ReferenceVertex,
            new BHeap<MstEdge<T, TW>>(),
            new HashSet<T>(),
            edges);

        return edges;
    }

    /// <summary>
    ///     Do DFS to pick smallest weight neighbour edges
    ///     of current spanning tree one by one.
    /// </summary>
    /// <param name="spanTreeNeighbours"> Use Fibonacci Min Heap to pick smallest edge neighbour </param>
    /// <param name="spanTreeEdges">result MST edges</param>
    private void Dfs(IGraph<T> graph, IGraphVertex<T> currentVertex,
        BHeap<MstEdge<T, TW>> spanTreeNeighbours, HashSet<T> spanTreeVertices,
        List<MstEdge<T, TW>> spanTreeEdges)
    {
        while (true)
        {
            //add all edges to Fibonacci Heap
            //So that we can pick the min edge in next step
            foreach (var edge in currentVertex.Edges)
                spanTreeNeighbours.Insert(new MstEdge<T, TW>(currentVertex.Key, edge.TargetVertexKey, edge.Weight<TW>()));

            //pick min edge
            var minNeighbourEdge = spanTreeNeighbours.Extract();

            //skip edges already in MST
            while (spanTreeVertices.Contains(minNeighbourEdge.Source) &&
                   spanTreeVertices.Contains(minNeighbourEdge.Destination))
            {
                minNeighbourEdge = spanTreeNeighbours.Extract();

                //if no more neighbours to explore 
                //time to end exploring
                if (spanTreeNeighbours.Count == 0) return;
            }

            //keep track of visited vertices
            //do not duplicate vertex
            if (!spanTreeVertices.Contains(minNeighbourEdge.Source)) spanTreeVertices.Add(minNeighbourEdge.Source);

            //Destination vertex will never be a duplicate
            //since this is an unexplored Vertex
            spanTreeVertices.Add(minNeighbourEdge.Destination);

            //add edge to result
            spanTreeEdges.Add(minNeighbourEdge);

            //now explore the destination vertex
            var graph1 = graph;
            currentVertex = graph1.GetVertex(minNeighbourEdge.Destination);
        }
    }
}