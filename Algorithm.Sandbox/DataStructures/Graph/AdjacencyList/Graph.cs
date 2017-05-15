using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A graph vertex
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GraphVertex<T>
    {
        public T Value { get; set; }

        public HashSet<GraphVertex<T>> Edges { get; set; }

        public GraphVertex(T value)
        {
            Value = value;
            Edges = new HashSet<GraphVertex<T>>();
        }

    }

    /// <summary>
    /// A graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsGraph<T>
    {
        public GraphVertex<T> ReferenceVertex
        {
            get
            {
                var enumerator = Vertices.GetEnumerator();
                if(enumerator.MoveNext())
                {
                    return enumerator.Current.Value;
                }

                return null;
            }
        }

        public int VerticesCount => Vertices.Count;
        internal Dictionary<T, GraphVertex<T>> Vertices { get; set; }

        public AsGraph()
        {
            Vertices = new Dictionary<T, GraphVertex<T>>();
        }

        /// <summary>
        /// add a new vertex to this graph
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GraphVertex<T> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new GraphVertex<T>(value);

            Vertices.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// remove and existing vertex from this graph
        /// </summary>
        /// <param name="vertex"></param>
        public void RemoveVertex(T vertex)
        {
            if (vertex == null)
            {
                throw new ArgumentNullException();
            }

            if (!Vertices.ContainsKey(vertex))
            {
                throw new Exception("Vertex not in this graph.");
            }

            foreach (var v in Vertices[vertex].Edges)
            {
                if (!Vertices.ContainsKey(v.Value))
                {
                    throw new Exception("Vertex edge is not in this graph.");
                }

                v.Edges.Remove(Vertices[vertex]);
            }

            Vertices.Remove(vertex);
        }

        /// <summary>
        /// add and edge to this graph
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public void AddEdge(T source, T dest)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (Vertices[source].Edges.Contains(Vertices[dest]) 
                || Vertices[dest].Edges.Contains(Vertices[source]))
            {
                throw new Exception("Edge exists already partially or totally.");
            }

            Vertices[source].Edges.Add(Vertices[dest]);
            Vertices[dest].Edges.Add(Vertices[source]);
        }

        /// <summary>
        /// remove an edge from this graph
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
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

            if (!Vertices[source].Edges.Contains(Vertices[dest]) 
                || !Vertices[dest].Edges.Contains(Vertices[source]))
            {
                throw new Exception("Edge do not exists partially or totally.");
            }

            Vertices[source].Edges.Remove(Vertices[dest]);
            Vertices[dest].Edges.Remove(Vertices[source]);
        }

        /// <summary>
        /// do we have an edge between given source and destination?
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public bool HasEdge(T source, T dest)
        {
            if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(dest))
            {
                throw new ArgumentException("source or destination is not in this graph.");
            }

            return Vertices[source].Edges.Contains(Vertices[dest]) 
                && Vertices[dest].Edges.Contains(Vertices[source]);
        }

        /// <summary>
        /// returns the vertex object with given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GraphVertex<T> FindVertex(T value)
        {
            if (Vertices.ContainsKey(value))
            {
                return Vertices[value];
            }

            return null;
        }

    }
}
