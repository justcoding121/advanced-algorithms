using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A directed graph implementation.
    /// IEnumerable enumerates all vertices.
    /// </summary>
    public class DiGraph<T> : IGraph<T>, IDiGraph<T>, IEnumerable<T>
    {
        private Dictionary<T, DiGraphVertex<T>> vertices { get; set; }

        public int VerticesCount => vertices.Count;
        public bool IsWeightedGraph => false;

        public DiGraph()
        {
            vertices = new Dictionary<T, DiGraphVertex<T>>();
        }

        /// <summary>
        /// Return a reference vertex to start traversing Vertices
        /// Time complexity: O(1).
        /// </summary>
        private DiGraphVertex<T> referenceVertex
        {
            get
            {
                using (var enumerator = vertices.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        return enumerator.Current.Value;
                    }

                }

                return null;
            }
        }

        IDiGraphVertex<T> IDiGraph<T>.ReferenceVertex => referenceVertex;
        IGraphVertex<T> IGraph<T>.ReferenceVertex => referenceVertex;


        /// <summary>
        /// Add a new vertex to this graph.
        /// Time complexity: O(1).
        /// </summary>
        public void AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new DiGraphVertex<T>(value);

            vertices.Add(value, newVertex);
        }

        /// <summary>
        /// Remove an existing vertex frm graph.
        /// Time complexity: O(V) where V is the total number of vertices in this graph.
        /// </summary>
        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!vertices.ContainsKey(value))
            {
                throw new Exception("Vertex not in this graph.");
            }

            foreach (var vertex in vertices[value].InEdges)
            {
                vertex.OutEdges.Remove(vertices[value]);
            }

            foreach (var vertex in vertices[value].OutEdges)
            {
                vertex.InEdges.Remove(vertices[value]);
            }

            vertices.Remove(value);
        }

        /// <summary>
        /// Add an edge from source to destination vertex.
        /// Time complexity: O(1).
        /// </summary>
        public void AddEdge(T source, T dest)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!vertices.ContainsKey(source) || !vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (vertices[source].OutEdges.Contains(vertices[dest]) || vertices[dest].InEdges.Contains(vertices[source]))
            {
                throw new Exception("Edge already exists.");
            }

            vertices[source].OutEdges.Add(vertices[dest]);
            vertices[dest].InEdges.Add(vertices[source]);
        }

        /// <summary>
        /// Remove an existing edge between source and destination.
        /// Time complexity: O(1).
        /// </summary>
        public void RemoveEdge(T source, T dest)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!vertices.ContainsKey(source) || !vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!vertices[source].OutEdges.Contains(vertices[dest])
                || !vertices[dest].InEdges.Contains(vertices[source]))
            {
                throw new Exception("Edge do not exists.");
            }

            vertices[source].OutEdges.Remove(vertices[dest]);
            vertices[dest].InEdges.Remove(vertices[source]);
        }

        /// <summary>
        /// Do we have an edge between the given source and destination?
        /// Time complexity: O(1).
        /// </summary>
        public bool HasEdge(T source, T dest)
        {
            if (!vertices.ContainsKey(source) || !vertices.ContainsKey(dest))
            {
                throw new ArgumentException("source or destination is not in this graph.");
            }

            return vertices[source].OutEdges.Contains(vertices[dest])
                && vertices[dest].InEdges.Contains(vertices[source]);
        }

        public IEnumerable<T> OutEdges(T vertex)
        {
            if (!vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return vertices[vertex].OutEdges.Select(x => x.Key);
        }

        public IEnumerable<T> InEdges(T vertex)
        {
            if (!vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return vertices[vertex].InEdges.Select(x => x.Key);
        }

        /// <summary>
        /// Clones this graph.
        /// </summary>
        public DiGraph<T> Clone()
        {
            var newGraph = new DiGraph<T>();

            foreach (var vertex in vertices)
            {
                newGraph.AddVertex(vertex.Key);
            }

            foreach (var vertex in vertices)
            {
                foreach (var edge in vertex.Value.OutEdges)
                {
                    newGraph.AddEdge(vertex.Value.Key, edge.Key);
                }
            }

            return newGraph;
        }

        public bool ContainsVertex(T value)
        {
            return vertices.ContainsKey(value);
        }

        public IDiGraphVertex<T> GetVertex(T value)
        {
            return vertices[value];
        }

        IGraphVertex<T> IGraph<T>.GetVertex(T key)
        {
            return vertices[key];
        }

        IDiGraph<T> IDiGraph<T>.Clone()
        {
            return Clone();
        }

        IGraph<T> IGraph<T>.Clone()
        {
            return Clone();
        }

        public IEnumerator GetEnumerator()
        {
            return vertices.Select(x => x.Key).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator() as IEnumerator<T>;
        }

        public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => vertices.Select(x => x.Value);
        IEnumerable<IDiGraphVertex<T>> IDiGraph<T>.VerticesAsEnumberable => vertices.Select(x => x.Value);
    }

    internal class DiGraphVertex<T> : IDiGraphVertex<T>, IGraphVertex<T>, IEnumerable<T>
    {
        public T Key { get; set; }

        public HashSet<DiGraphVertex<T>> OutEdges { get; }
        public HashSet<DiGraphVertex<T>> InEdges { get; }

        IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.OutEdges => OutEdges.Select(x => new DiEdge<T, int>(x, 1));
        IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.InEdges => InEdges.Select(x => new DiEdge<T, int>(x, 1));

        public int OutEdgeCount => OutEdges.Count;
        public int InEdgeCount => InEdges.Count;

        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => OutEdges.Select(x => new Edge<T, int>(x, 1));

        public DiGraphVertex(T value)
        {
            Key = value;
            OutEdges = new HashSet<DiGraphVertex<T>>();
            InEdges = new HashSet<DiGraphVertex<T>>();
        }

        public IDiEdge<T> GetOutEdge(IDiGraphVertex<T> targetVertex)
        {
            return new DiEdge<T, int>(targetVertex, 1);
        }

        public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
        {
            return new Edge<T, int>(targetVertex, 1);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return OutEdges.Select(x => x.Key).GetEnumerator();
        }
    }
}
