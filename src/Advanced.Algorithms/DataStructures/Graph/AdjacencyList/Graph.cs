using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A graph implementation
    /// IEnumerable enumerates all vertices.
    /// </summary>
    public class Graph<T> : IGraph<T>, IEnumerable<T>
    {
        private Dictionary<T, GraphVertex<T>> vertices { get; set; }

        public int VerticesCount => vertices.Count;
        public bool IsWeightedGraph => false;

        public Graph()
        {
            vertices = new Dictionary<T, GraphVertex<T>>();
        }

        /// <summary>
        /// Returns a reference vertex.
        /// Time complexity: O(1).
        /// </summary>
        private GraphVertex<T> referenceVertex
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

            var newVertex = new GraphVertex<T>(value);

            vertices.Add(value, newVertex);
        }

        /// <summary>
        /// Remove an existing vertex from this graph.
        /// Time complexity: O(V) where V is the number of vertices.
        /// </summary>
        public void RemoveVertex(T vertex)
        {
            if (vertex == null)
            {
                throw new ArgumentNullException();
            }

            if (!vertices.ContainsKey(vertex))
            {
                throw new Exception("Vertex not in this graph.");
            }

            foreach (var v in vertices[vertex].Edges)
            {
                v.Edges.Remove(vertices[vertex]);
            }

            vertices.Remove(vertex);
        }

        /// <summary>
        /// Add an edge to this graph.
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

            if (vertices[source].Edges.Contains(vertices[dest])
                || vertices[dest].Edges.Contains(vertices[source]))
            {
                throw new Exception("Edge already exists.");
            }

            vertices[source].Edges.Add(vertices[dest]);
            vertices[dest].Edges.Add(vertices[source]);
        }

        /// <summary>
        /// Remove an edge from this graph.
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

            if (!vertices[source].Edges.Contains(vertices[dest])
                || !vertices[dest].Edges.Contains(vertices[source]))
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

            return vertices[source].Edges.Contains(vertices[dest])
                && vertices[dest].Edges.Contains(vertices[source]);
        }

        public IEnumerable<T> Edges(T vertex)
        {
            if (!vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return vertices[vertex].Edges.Select(x => x.Key);
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
        public Graph<T> Clone()
        {
            var newGraph = new Graph<T>();

            foreach (var vertex in vertices)
            {
                newGraph.AddVertex(vertex.Key);
            }

            foreach (var vertex in vertices)
            {
                foreach (var edge in vertex.Value.Edges)
                {
                    newGraph.AddEdge(vertex.Value.Key, edge.Key);
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

        /// <summary>
        /// Graph vertex for adjacency list Graph implementation. 
        /// IEnumerable enumerates all the outgoing edge destination vertices.
        /// </summary>
        private class GraphVertex<T> : IEnumerable<T>, IGraphVertex<T>
        {
            public T Key { get; set; }

            public HashSet<GraphVertex<T>> Edges { get; }

            IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => Edges.Select(x => new Edge<T, int>(x, 1));

            public GraphVertex(T value)
            {
                Key = value;
                Edges = new HashSet<GraphVertex<T>>();
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
                return Edges.Select(x => x.Key).GetEnumerator();
            }
        }
    }


}
