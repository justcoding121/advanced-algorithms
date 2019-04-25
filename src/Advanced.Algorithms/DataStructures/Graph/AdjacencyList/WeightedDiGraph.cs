using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A weighted graph implementation.
    /// IEnumerable enumerates all vertices.
    /// </summary>
    public class WeightedDiGraph<T, TW> : IDiGraph<T>, IGraph<T>, IEnumerable<T> where TW : IComparable
    {
        internal Dictionary<T, WeightedDiGraphVertex<T, TW>> Vertices { get; set; }

        public int VerticesCount => Vertices.Count;
        public bool IsWeightedGraph => true;

        public WeightedDiGraph()
        {
            Vertices = new Dictionary<T, WeightedDiGraphVertex<T, TW>>();
        }

        /// <summary>
        /// Returns a reference vertex.
        /// Time complexity: O(1).
        /// </summary>
        private WeightedDiGraphVertex<T, TW> referenceVertex
        {
            get
            {
                using (var enumerator = Vertices.GetEnumerator())
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

            var newVertex = new WeightedDiGraphVertex<T, TW>(value);

            Vertices.Add(value, newVertex);
        }

        /// <summary>
        /// Remove the given vertex.
        /// Time complexity: O(V) where V is the number of vertices.
        /// </summary>
        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!Vertices.ContainsKey(value))
            {
                throw new Exception("Vertex not in this graph.");
            }

            foreach (var vertex in Vertices[value].InEdges)
            {
                vertex.Key.OutEdges.Remove(Vertices[value]);
            }

            foreach (var vertex in Vertices[value].OutEdges)
            {
                vertex.Key.InEdges.Remove(Vertices[value]);
            }

            Vertices.Remove(value);
        }

        /// <summary>
        /// Add a new edge to this graph.
        /// Time complexity: O(1).
        /// </summary>
        public void AddEdge(T source, T dest, TW weight)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.ContainsKey(source)
                || !Vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (Vertices[source].OutEdges.ContainsKey(Vertices[dest])
                || Vertices[dest].InEdges.ContainsKey(Vertices[source]))
            {
                throw new Exception("Edge already exists.");
            }

            Vertices[source].OutEdges.Add(Vertices[dest], weight);
            Vertices[dest].InEdges.Add(Vertices[source], weight);
        }

        /// <summary>
        /// Remove the given edge from this graph.
        /// Time complexity: O(1).
        /// </summary>
        public void RemoveEdge(T source, T dest)
        {

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!Vertices[source].OutEdges.ContainsKey(Vertices[dest])
                || !Vertices[dest].InEdges.ContainsKey(Vertices[source]))
            {
                throw new Exception("Edge do not exist.");
            }

            Vertices[source].OutEdges.Remove(Vertices[dest]);
            Vertices[dest].InEdges.Remove(Vertices[source]);
        }

        /// <summary>
        /// Do we have an edge between given source and destination?
        /// Time complexity: O(1).
        /// </summary>
        public bool HasEdge(T source, T dest)
        {
            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new ArgumentException("source or destination is not in this graph.");
            }

            return Vertices[source].OutEdges.ContainsKey(Vertices[dest])
              && Vertices[dest].InEdges.ContainsKey(Vertices[source]);
        }

        public IEnumerable<Tuple<T, TW>> OutEdges(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return Vertices[vertex].OutEdges.Select(x => new Tuple<T, TW>(x.Key.Key, x.Value));
        }

        public IEnumerable<Tuple<T, TW>> InEdges(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return Vertices[vertex].InEdges.Select(x => new Tuple<T, TW>(x.Key.Key, x.Value));
        }

        /// <summary>
        /// Returns the vertex with given value.
        /// Time complexity: O(1).
        /// </summary>
        internal WeightedDiGraphVertex<T, TW> FindVertex(T value)
        {
            if (Vertices.ContainsKey(value))
            {
                return Vertices[value];
            }

            return null;
        }

        public bool ContainsVertex(T value)
        {
            return Vertices.ContainsKey(value);
        }

        IGraphVertex<T> IGraph<T>.GetVertex(T key)
        {
            return Vertices[key];
        }

        public IDiGraphVertex<T> GetVertex(T key)
        {
            return Vertices[key];
        }

        /// <summary>
        /// Clone this graph.
        /// </summary>
        public WeightedDiGraph<T, TW> Clone()
        {
            var newGraph = new WeightedDiGraph<T, TW>();

            foreach (var vertex in Vertices)
            {
                newGraph.AddVertex(vertex.Key);
            }

            foreach (var vertex in Vertices)
            {
                foreach (var edge in vertex.Value.OutEdges)
                {
                    newGraph.AddEdge(vertex.Value.Key, edge.Key.Key, edge.Value);
                }
            }

            return newGraph;
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
            return Vertices.Select(x => x.Key).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator() as IEnumerator<T>;
        }

        public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => Vertices.Select(x => x.Value);
        IEnumerable<IDiGraphVertex<T>> IDiGraph<T>.VerticesAsEnumberable => Vertices.Select(x => x.Value);
    }

    internal class WeightedDiGraphVertex<T, TW> : IDiGraphVertex<T>, IGraphVertex<T>, IEnumerable<T> where TW : IComparable
    {
        public T Key { get; set; }

        public Dictionary<WeightedDiGraphVertex<T, TW>, TW> OutEdges { get; }
        public Dictionary<WeightedDiGraphVertex<T, TW>, TW> InEdges { get; }

        IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.OutEdges => OutEdges.Select(x => new DiEdge<T, TW>(x.Key, x.Value));
        IEnumerable<IDiEdge<T>> IDiGraphVertex<T>.InEdges => InEdges.Select(x => new DiEdge<T, TW>(x.Key, x.Value));

        public int OutEdgeCount => OutEdges.Count;
        public int InEdgeCount => InEdges.Count;

        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => OutEdges.Select(x => new Edge<T, TW>(x.Key, x.Value));

        public WeightedDiGraphVertex(T value)
        {
            Key = value;

            OutEdges = new Dictionary<WeightedDiGraphVertex<T, TW>, TW>();
            InEdges = new Dictionary<WeightedDiGraphVertex<T, TW>, TW>();
        }

        public IDiEdge<T> GetOutEdge(IDiGraphVertex<T> targetVertex)
        {
            var key = targetVertex as WeightedDiGraphVertex<T, TW>;
            return new DiEdge<T, TW>(targetVertex, OutEdges[key]);
        }

        public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
        {
            var key = targetVertex as WeightedDiGraphVertex<T, TW>;
            return new Edge<T, TW>(targetVertex, OutEdges[key]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return OutEdges.Select(x => x.Key.Key).GetEnumerator();
        }
    }
}
