using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList
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

    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    /// <summary>
    /// A graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Graph<T>
    {
        public int VerticesCount => Vertices.Count;
        internal Dictionary<T, GraphVertex<T>> Vertices { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Graph()
        {
            Vertices = new Dictionary<T, GraphVertex<T>>();
        }


        /// <summary>
        /// return a reference vertex  to start traversing Vertices
        ///  O(1) complexity
        /// </summary>
        public GraphVertex<T> ReferenceVertex
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


        /// <summary>
        /// add a new vertex to this graph
        /// O(1) complexity
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
        /// remove an existing vertex from this graph
        /// O(V) complexity
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
                v.Edges.Remove(Vertices[vertex]);
            }

            Vertices.Remove(vertex);
        }

        /// <summary>
        /// add and edge to this graph
        /// O(1) complexity
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
                throw new Exception("Edge already exists.");
            }

            Vertices[source].Edges.Add(Vertices[dest]);
            Vertices[dest].Edges.Add(Vertices[source]);
        }

        /// <summary>
        /// remove an edge from this graph
        ///  O(1) complexity
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
                throw new Exception("Edge do not exists.");
            }

            Vertices[source].Edges.Remove(Vertices[dest]);
            Vertices[dest].Edges.Remove(Vertices[source]);
        }

        /// <summary>
        /// do we have an edge between given source and destination?
        ///  O(1) complexity
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

        public List<T> GetAllEdges(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return Vertices[vertex].Edges.Select(x => x.Value).ToList();
        }

        /// <summary>
        /// returns the vertex object with given value
        ///  O(1) complexity
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
