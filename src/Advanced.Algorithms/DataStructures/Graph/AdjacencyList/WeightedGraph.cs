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
        private Dictionary<T, WeightedGraphVertex<T, TW>> vertices { get; set; }

        public int VerticesCount => vertices.Count;
        public bool IsWeightedGraph => true;

        public WeightedGraph()
        {
            vertices = new Dictionary<T, WeightedGraphVertex<T, TW>>();
        }

        /// <summary>
        /// Returns a reference vertex.
        /// Time complexity: O(1).
        /// </summary>
        private WeightedGraphVertex<T, TW> referenceVertex
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

            var newVertex = new WeightedGraphVertex<T, TW>(value);

            vertices.Add(value, newVertex);
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

            if (!vertices.ContainsKey(value))
            {
                throw new Exception("Vertex not in this graph.");
            }


            foreach (var vertex in vertices[value].Edges)
            {
                vertex.Key.Edges.Remove(vertices[value]);
            }

            vertices.Remove(value);
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

            if (!vertices.ContainsKey(source) || !vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }


            vertices[source].Edges.Add(vertices[dest], weight);
            vertices[dest].Edges.Add(vertices[source], weight);
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

            if (!vertices.ContainsKey(source) || !vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!vertices[source].Edges.ContainsKey(vertices[dest])
                || !vertices[dest].Edges.ContainsKey(vertices[source]))
            {
                throw new Exception("Edge do not exists.");
            }

            vertices[source].Edges.Remove(vertices[dest]);
            vertices[dest].Edges.Remove(vertices[source]);
        }

        /// <summary>
        /// Do we have an edge between given source and destination?
        /// Time complexity: O(1).
        /// </summary>
        public bool HasEdge(T source, T dest)
        {
            if (!vertices.ContainsKey(source) || !vertices.ContainsKey(dest))
            {
                throw new ArgumentException("source or destination is not in this graph.");
            }

            return vertices[source].Edges.ContainsKey(vertices[dest])
                   && vertices[dest].Edges.ContainsKey(vertices[source]);

        }

        public List<Tuple<T, TW>> GetAllEdges(T vertex)
        {
            if (!vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return vertices[vertex].Edges.Select(x => new Tuple<T, TW>(x.Key.Key, x.Value)).ToList();
        }

        public bool ContainsVertex(T value)
        {
            return vertices.ContainsKey(value);
        }

        public IGraphVertex<T> GetVertex(T value)
        {
            return vertices[value];
        }

        /// <summary>
        /// Clones this graph.
        /// </summary>
        public WeightedGraph<T, TW> Clone()
        {
            var newGraph = new WeightedGraph<T, TW>();

            foreach (var vertex in vertices)
            {
                newGraph.AddVertex(vertex.Key);
            }

            foreach (var vertex in vertices)
            {
                foreach (var edge in vertex.Value.Edges)
                {
                    newGraph.AddEdge(vertex.Value.Key, edge.Key.Key, edge.Value);
                }
            }

            return newGraph;
        }

        public IEnumerator GetEnumerator()
        {
            return vertices.Select(x => x.Key).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator() as IEnumerator<T>;
        }

        IGraph<T> IGraph<T>.Clone()
        {
            return Clone();
        }

        public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => vertices.Select(x => x.Value);
    }

    internal class WeightedGraphVertex<T, TW> : IGraphVertex<T>, IEnumerable<T> where TW : IComparable
    {
        public T Key { get; set; }

        public Dictionary<WeightedGraphVertex<T, TW>, TW> Edges { get; }
        T IGraphVertex<T>.Key => Key;

        IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => Edges.Select(x => new Edge<T, TW>(x.Key, x.Value));

        public WeightedGraphVertex(T key)
        {
            Key = key;
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
            return Edges.Select(x => x.Key.Key).GetEnumerator();
        }
    }
}
