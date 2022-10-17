using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Graph;

/// <summary>
///     UnDirected graph. (When implemented on a directed graphs only outgoing edges are considered as Edges).
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGraph<T>
{
    bool IsWeightedGraph { get; }

    int VerticesCount { get; }
    IGraphVertex<T> ReferenceVertex { get; }
    IEnumerable<IGraphVertex<T>> VerticesAsEnumberable { get; }
    bool ContainsVertex(T key);
    IGraphVertex<T> GetVertex(T key);

    bool HasEdge(T source, T destination);

    IGraph<T> Clone();
}

public interface IGraphVertex<T>
{
    T Key { get; }
    IEnumerable<IEdge<T>> Edges { get; }

    IEdge<T> GetEdge(IGraphVertex<T> targetVertex);
}

public interface IEdge<T>
{
    T TargetVertexKey { get; }
    IGraphVertex<T> TargetVertex { get; }
    W Weight<W>() where W : IComparable;
}

internal class Edge<T, C> : IEdge<T> where C : IComparable
{
    private readonly object weight;

    internal Edge(IGraphVertex<T> target, C weight)
    {
        TargetVertex = target;
        this.weight = weight;
    }

    public T TargetVertexKey => TargetVertex.Key;

    public IGraphVertex<T> TargetVertex { get; }

    public W Weight<W>() where W : IComparable
    {
        return (W)weight;
    }
}