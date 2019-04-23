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
    public class WeightedGraph<T, TW> : IGraph<T>, IEnumerable<T> where TW : IComparable
    {
        public int VerticesCount => Vertices.Count;
        internal Dictionary<T, WeightedGraphVertex<T, TW>> Vertices { get; set; }
        public bool IsWeightedGraph => true;

        public WeightedGraph()
        {
            Vertices = new Dictionary<T, WeightedGraphVertex<T, TW>>();
        }

        /// <summary>
        /// Returns a reference vertex.
        /// Time complexity: O(1).
        /// </summary>
        public WeightedGraphVertex<T, TW> ReferenceVertex
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

        IGraphVertex<T> IGraph<T>.ReferenceVertex => ReferenceVertex;


        /// <summary>
        /// Add a new vertex to this graph.
        /// Time complexity: O(1).
        /// </summary>
        public WeightedGraphVertex<T, TW> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new WeightedGraphVertex<T, TW>(value);

            Vertices.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// Remove given vertex from this graph.
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


            foreach (var vertex in Vertices[value].Edges)
            {
                vertex.Key.Edges.Remove(Vertices[value]);
            }

            Vertices.Remove(value);
        }

        /// <summary>
        /// Add a new edge to this graph with given weight 
        /// and between given source and destination vertex.
        /// Time complexity: O(1).
        /// </summary>
        public void AddEdge(T source, T dest, TW weight)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }


            Vertices[source].Edges.Add(Vertices[dest], weight);
            Vertices[dest].Edges.Add(Vertices[source], weight);
        }

        /// <summary>
        /// Remove given edge.
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

            if (!Vertices[source].Edges.ContainsKey(Vertices[dest])
                || !Vertices[dest].Edges.ContainsKey(Vertices[source]))
            {
                throw new Exception("Edge do not exists.");
            }

            Vertices[source].Edges.Remove(Vertices[dest]);
            Vertices[dest].Edges.Remove(Vertices[source]);
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

            return Vertices[source].Edges.ContainsKey(Vertices[dest])
                   && Vertices[dest].Edges.ContainsKey(Vertices[source]);

        }

        public List<Tuple<T, TW>> GetAllEdges(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return Vertices[vertex].Edges.Select(x => new Tuple<T, TW>(x.Key.Value, x.Value)).ToList();
        }

        /// <summary>
        /// Find the Vertex with given value.
        /// Time complexity: O(1).
        /// </summary>
        public WeightedGraphVertex<T, TW> FindVertex(T value)
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

        public IGraphVertex<T> GetVertex(T value)
        {
            return Vertices[value];
        }

        /// <summary>
        /// Clones this graph.
        /// </summary>
        public WeightedGraph<T, TW> Clone()
        {
            var newGraph = new WeightedGraph<T, TW>();

            foreach (var vertex in Vertices)
            {
                newGraph.AddVertex(vertex.Key);
            }

            foreach (var vertex in Vertices)
            {
                foreach (var edge in vertex.Value.Edges)
                {
                    newGraph.AddEdge(vertex.Value.Value, edge.Key.Value, edge.Value);
                }
            }

            return newGraph;
        }

        public IEnumerator GetEnumerator()
        {
            return Vertices.Select(x => x.Key).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator() as IEnumerator<T>;
        }

        IGraph<T> IGraph<T>.Clone()
        {
            return Clone();
        }

        public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => Vertices.Select(x => x.Value);
    }

    /// <summary>
    /// A weighted graph vertex for adjacency list Graph implementation. 
    /// IEnumerable enumerates all the outgoing edge destination vertices.
    /// </summary>
    public class WeightedGraphVertex<T, TW> : IGraphVertex<T>, IEnumerable<T> where TW : IComparable
    {
        public T Value { get; private set; }

        public Dictionary<WeightedGraphVertex<T, TW>, TW> Edges { get; }
        T IGraphVertex<T>.Key => Value;

        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => Edges.Select(x => new Edge<T, TW>(x.Key, x.Value));

        public WeightedGraphVertex(T value)
        {
            Value = value;
            Edges = new Dictionary<WeightedGraphVertex<T, TW>, TW>();
        }

        public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
        {
            return new Edge<T, TW>(targetVertex, Edges[targetVertex as WeightedGraphVertex<T, TW>]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Edges.Select(x => x.Key.Value).GetEnumerator();
        }
    }
}
