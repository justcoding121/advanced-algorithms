using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix;

/// <summary>
///     A weighted graph implementation using dynamically growing/shrinking adjacency matrix array.
///     IEnumerable enumerates all vertices.
/// </summary>
public class WeightedGraph<T, TW> : IGraph<T>, IEnumerable<T> where TW : IComparable
{
    private const string VertexNotInGraph = "vertex is not in this graph.";

    private TW[,] matrix;
    private int nextAvailableIndex;
    private Dictionary<int, T> reverseVertexIndices;

    private Dictionary<T, int> vertexIndices;
    private readonly Dictionary<T, WeightedGraphVertex> vertexObjects;

    public WeightedGraph()
    {
        vertexIndices = new Dictionary<T, int>();
        reverseVertexIndices = new Dictionary<int, T>();
        vertexObjects = new Dictionary<T, WeightedGraphVertex>();
        matrix = new TW[1, 1];
    }

    private int MaxSize => matrix.GetLength(0);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return vertexIndices.Select(x => x.Key).GetEnumerator();
    }

    public int VerticesCount { get; private set; }

    public bool IsWeightedGraph => true;

    public IGraphVertex<T> ReferenceVertex => GetReferenceVertex();

    /// <summary>
    ///     Do we have an edge between given source and destination?
    ///     Time complexity: O(1).
    /// </summary>
    public bool HasEdge(T source, T destination)
    {
        if (source is null || destination is null)
            throw new ArgumentException("source or destination is null.");

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(destination))
            throw new ArgumentException("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[destination];

        if (!matrix[sourceIndex, destIndex].Equals(default(TW))
            && !matrix[destIndex, sourceIndex].Equals(default(TW)))
            return true;

        return false;
    }

    public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => vertexObjects.Select(x => x.Value);

    public bool ContainsVertex(T key)
    {
        return vertexObjects.ContainsKey(key);
    }

    public IGraphVertex<T> GetVertex(T key)
    {
        return vertexObjects[key];
    }

    IGraph<T> IGraph<T>.Clone()
    {
        return Clone();
    }

    private WeightedGraphVertex GetReferenceVertex()
    {
        if (VerticesCount == 0) throw new InvalidOperationException("Empty graph.");

        return vertexObjects[this.First()];
    }

    /// <summary>
    ///     Add a new vertex to this graph.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddVertex(T value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        if (vertexIndices.ContainsKey(value)) throw new ArgumentException("Vertex exists.");

        if (VerticesCount < MaxSize / 2) HalfMatrixSize();

        if (nextAvailableIndex == MaxSize) DoubleMatrixSize();

        vertexIndices.Add(value, nextAvailableIndex);
        reverseVertexIndices.Add(nextAvailableIndex, value);
        vertexObjects.Add(value, new WeightedGraphVertex(this, value));
        nextAvailableIndex++;
        VerticesCount++;
    }

    /// <summary>
    ///     Remove given vertex from this graph.
    ///     Time complexity: O(V) where V is the number of vertices.
    /// </summary>
    public void RemoveVertex(T value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));

        if (!vertexIndices.ContainsKey(value)) throw new ArgumentException("Vertex does'nt exist.");

        if (VerticesCount <= MaxSize / 2) HalfMatrixSize();

        var index = vertexIndices[value];

        //clear edges
        for (var i = 0; i < MaxSize; i++)
        {
            matrix[i, index] = default;
            matrix[index, i] = default;
        }

        reverseVertexIndices.Remove(index);
        vertexIndices.Remove(value);
        vertexObjects.Remove(value);

        VerticesCount--;
    }

    /// <summary>
    ///     Add a new edge to this graph with given weight
    ///     and between given source and destination vertex.
    ///     Time complexity: O(1).
    /// </summary>
    public void AddEdge(T source, T dest, TW weight)
    {
        if (weight.Equals(default(TW))) throw new ArgumentException("Cannot add default edge weight.");

        if (source is null || dest is null)
            throw new ArgumentException("source or destination is null.");

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new ArgumentException("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (!matrix[sourceIndex, destIndex].Equals(default(TW))
            && !matrix[destIndex, sourceIndex].Equals(default(TW)))
            throw new InvalidOperationException("Edge already exists.");

        matrix[sourceIndex, destIndex] = weight;
        matrix[destIndex, sourceIndex] = weight;
    }

    /// <summary>
    ///     Remove given edge.
    ///     Time complexity: O(1).
    /// </summary>
    public void RemoveEdge(T source, T dest)
    {
        if (source is null || dest is null)
            throw new ArgumentException("source or destination is null.");

        if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            throw new ArgumentException("Source or destination vertex does'nt exist.");

        var sourceIndex = vertexIndices[source];
        var destIndex = vertexIndices[dest];
        if (matrix[sourceIndex, destIndex].Equals(default(TW))
            && matrix[destIndex, sourceIndex].Equals(default(TW)))
            throw new InvalidOperationException("Edge do not exists.");

        matrix[sourceIndex, destIndex] = default;
        matrix[destIndex, sourceIndex] = default;
    }

    public IEnumerable<KeyValuePair<T, TW>> Edges(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException(VertexNotInGraph);

        return EdgesIterator(vertex);
    }

    private IEnumerable<KeyValuePair<T, TW>> EdgesIterator(T vertex)
    {
        var index = vertexIndices[vertex];

        for (var i = 0; i < MaxSize; i++)
        {
            if (!matrix[i, index].Equals(default(TW)))
                yield return new KeyValuePair<T, TW>(reverseVertexIndices[i], matrix[i, index]);
        }
    }

    public int EdgeCount(T vertex)
    {
        if (!vertexIndices.ContainsKey(vertex)) throw new ArgumentException(VertexNotInGraph);

        var index = vertexIndices[vertex];

        var count = 0;

        for (var i = 0; i < MaxSize; i++)
        {
            if (!matrix[i, index].Equals(default(TW)))
                count++;
        }

        return count;
    }

    private void DoubleMatrixSize()
    {
        var newMatrix = new TW[MaxSize * 2, MaxSize * 2];

        var newVertexIndices = new Dictionary<T, int>();
        var newReverseIndices = new Dictionary<int, T>();

        var k = 0;
        foreach (var vertex in vertexIndices)
        {
            newVertexIndices.Add(vertex.Key, k);
            newReverseIndices.Add(k, vertex.Key);
            k++;
        }

        nextAvailableIndex = k;

        for (var i = 0; i < MaxSize; i++)
        {
            for (var j = i; j < MaxSize; j++)
            {
                if (!matrix[i, j].Equals(default(TW)) && !matrix[j, i].Equals(default(TW))
                                                      && reverseVertexIndices.ContainsKey(i)
                                                      && reverseVertexIndices.ContainsKey(j))
                {
                    var newI = newVertexIndices[reverseVertexIndices[i]];
                    var newJ = newVertexIndices[reverseVertexIndices[j]];

                    newMatrix[newI, newJ] = matrix[i, j];
                    newMatrix[newJ, newI] = matrix[j, i];
                }
            }
        }

        matrix = newMatrix;
        vertexIndices = newVertexIndices;
        reverseVertexIndices = newReverseIndices;
    }

    private void HalfMatrixSize()
    {
        var newMatrix = new TW[MaxSize / 2, MaxSize / 2];

        var newVertexIndices = new Dictionary<T, int>();
        var newReverseIndices = new Dictionary<int, T>();

        var k = 0;
        foreach (var vertex in vertexIndices)
        {
            newVertexIndices.Add(vertex.Key, k);
            newReverseIndices.Add(k, vertex.Key);
            k++;
        }

        nextAvailableIndex = k;

        for (var i = 0; i < MaxSize; i++)
        {
            for (var j = i; j < MaxSize; j++)
            {
                if (!matrix[i, j].Equals(default(TW)) && !matrix[j, i].Equals(default(TW))
                                                      && reverseVertexIndices.ContainsKey(i)
                                                      && reverseVertexIndices.ContainsKey(j))
                {
                    var newI = newVertexIndices[reverseVertexIndices[i]];
                    var newJ = newVertexIndices[reverseVertexIndices[j]];

                    newMatrix[newI, newJ] = matrix[i, j];
                    newMatrix[newJ, newI] = matrix[j, i];
                }
            }
        }

        matrix = newMatrix;
        vertexIndices = newVertexIndices;
        reverseVertexIndices = newReverseIndices;
    }

    /// <summary>
    ///     Clones this graph.
    /// </summary>
    public WeightedGraph<T, TW> Clone()
    {
        var graph = new WeightedGraph<T, TW>();

        foreach (var vertex in this)
        {
            graph.AddVertex(vertex);
        }

        foreach (var vertex in this)
        {
            foreach (var edge in Edges(vertex))
                graph.AddEdge(vertex, edge.Key, edge.Value);
        }

        return graph;
    }

    private sealed class WeightedGraphVertex : IGraphVertex<T>
    {
        private readonly WeightedGraph<T, TW> graph;
        private readonly int vertexIndex;

        internal WeightedGraphVertex(WeightedGraph<T, TW> graph, T vertexKey)
        {
            if (!graph.vertexIndices.ContainsKey(vertexKey))
                throw new ArgumentException(VertexNotInGraph);

            this.graph = graph;
            Key = vertexKey;
            vertexIndex = graph.vertexIndices[vertexKey];
        }

        private TW[,] Matrix => graph.matrix;

        private Dictionary<T, int> VertexIndices => graph.vertexIndices;

        public T Key { get; }


        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => graph.Edges(Key)
            .Select(x => new Edge<T, TW>(graph.vertexObjects[x.Key], x.Value));

        public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
        {
            if (!VertexIndices.ContainsKey(targetVertex.Key))
                throw new ArgumentException(VertexNotInGraph);

            var index = VertexIndices[targetVertex.Key];
            return new Edge<T, TW>(targetVertex, Matrix[vertexIndex, index]);
        }
    }
}
