using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

/// <summary>
///     A graph implementation
///     IEnumerable enumerates all vertices.
/// </summary>
public class Graph<T> : IGraph<T>, IEnumerable<T>
{
    public Graph()
    {
        Vertices = new Dictionary<T, GraphVertex<T>>();
    }

    private Dictionary<T, GraphVertex<T>> Vertices { get; }

    /// <summary>
    ///     Returns a reference vertex.
    ///     Time complexity: O(1).
    /// </summary>
    private GraphVertex<T> ReferenceVertex
    {
        get
        {
            using (var enumerator = Vertices.GetEnumerator())
            {
                if (enumerator.MoveNext()) return enumerator.Current.Value;
            }

            return null;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return Vertices.Select(x => x.Key).GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator() as IEnumerator<T>;
    }

    public int VerticesCount => Vertices.Count;
    public bool IsWeightedGraph => false;

    IGraphVertex<T> IGraph<T>.ReferenceVertex => ReferenceVertex;

    /// <summary>
    ///     Do we have an edge between given source and destination?
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasEdge(T source, T dest)
    {
        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new ArgumentException("source or destination is not in this graph.");

        return Vertices[source].Edges.Contains(Vertices[dest])
               && Vertices[dest].Edges.Contains(Vertices[source]);
    }

    public bool ContainsVertex(T value)
    {
        return Vertices.ContainsKey(value);
    }

    public IGraphVertex<T> GetVertex(T value)
    {
        return Vertices[value];
    }

    IGraph<T> IGraph<T>.Clone()
    {
        return Clone();
    }

    public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => Vertices.Select(x => x.Value);


    /// <summary>
    ///     Add a new vertex to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddVertex(T value)
    {
        if (value == null) throw new ArgumentNullException();

        var newVertex = new GraphVertex<T>(value);

        Vertices.Add(value, newVertex);
    }

    /// <summary>
    ///     Remove an existing vertex from this graph.
    ///     Time complexity: O(V) where V is the number of vertices.
    /// </summary>
    public void RemoveVertex(T vertex)
    {
        if (vertex == null) throw new ArgumentNullException();

        if (!Vertices.ContainsKey(vertex)) throw new Exception("Vertex not in this graph.");

        foreach (var v in Vertices[vertex].Edges) v.Edges.Remove(Vertices[vertex]);

        Vertices.Remove(vertex);
    }

    /// <summary>
    ///     Add an edge to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new Exception("Source or Destination Vertex is not in this graph.");

        if (Vertices[source].Edges.Contains(Vertices[dest])
            || Vertices[dest].Edges.Contains(Vertices[source]))
            throw new Exception("Edge already exists.");

        Vertices[source].Edges.Add(Vertices[dest]);
        Vertices[dest].Edges.Add(Vertices[source]);
    }

    /// <summary>
    ///     Remove an edge from this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void RemoveEdge(T source, T dest)
    {
        if (source == null || dest == null) throw new ArgumentException();

        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            throw new Exception("Source or Destination Vertex is not in this graph.");

        if (!Vertices[source].Edges.Contains(Vertices[dest])
            || !Vertices[dest].Edges.Contains(Vertices[source]))
            throw new Exception("Edge do not exists.");

        Vertices[source].Edges.Remove(Vertices[dest]);
        Vertices[dest].Edges.Remove(Vertices[source]);
    }

    public IEnumerable<T> Edges(T vertex)
    {
        if (!Vertices.ContainsKey(vertex)) throw new ArgumentException("vertex is not in this graph.");

        return Vertices[vertex].Edges.Select(x => x.Key);
    }

    /// <summary>
    ///     Clones this graph.
    /// </summary>
    public Graph<T> Clone()
    {
        var newGraph = new Graph<T>();

        foreach (var vertex in Vertices) newGraph.AddVertex(vertex.Key);

        foreach (var vertex in Vertices)
        foreach (var edge in vertex.Value.Edges)
            newGraph.AddEdge(vertex.Value.Key, edge.Key);

        return newGraph;
    }

    /// <summary>
    ///     Graph vertex for adjacency list Graph implementation.
    ///     IEnumerable enumerates all the outgoing edge destination vertices.
    /// </summary>
    private class GraphVertex<T> : IEnumerable<T>, IGraphVertex<T>
    {
        public GraphVertex(T value)
        {
            Key = value;
            Edges = new HashSet<GraphVertex<T>>();
        }

        public HashSet<GraphVertex<T>> Edges { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Edges.Select(x => x.Key).GetEnumerator();
        }

        public T Key { get; }

        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => Edges.Select(x => new Edge<T, int>(x, 1));

        public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
        {
            return new Edge<T, int>(targetVertex, 1);
        }
    }
}