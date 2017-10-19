using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// Graph vertex
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DiGraphVertex<T>
    {
        public T Value { get; set; }

        public HashSet<DiGraphVertex<T>> OutEdges { get; set; }
        public HashSet<DiGraphVertex<T>> InEdges { get; set; }

        public DiGraphVertex(T value)
        {
            Value = value;
            OutEdges = new HashSet<DiGraphVertex<T>>();
            InEdges = new HashSet<DiGraphVertex<T>>();
        }

    }

    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    /// <summary>
    /// A directed graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DiGraph<T>
    {
        public int VerticesCount => Vertices.Count;
        internal Dictionary<T, DiGraphVertex<T>> Vertices { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DiGraph()
        {
            Vertices = new Dictionary<T, DiGraphVertex<T>>();
        }

        /// <summary>
        /// return a reference vertex  to start traversing Vertices
        /// O(1) complexity
        /// </summary>
        public DiGraphVertex<T> ReferenceVertex
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
        public DiGraphVertex<T> AddVertex(T value)
        {
            if ( value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new DiGraphVertex<T>(value);

            Vertices.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// remove an existing vertex frm graph
        /// O(V) complexity
        /// </summary>
        /// <param name="value"></param>
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
                vertex.OutEdges.Remove(Vertices[value]);
            }

            foreach (var vertex in Vertices[value].OutEdges)
            {
                vertex.InEdges.Remove(Vertices[value]);
            }

            Vertices.Remove(value);
        }

        /// <summary>
        /// add an edge from source to destination vertex
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

            if (Vertices[source].OutEdges.Contains(Vertices[dest]) || Vertices[dest].InEdges.Contains(Vertices[source]))
            {
                throw new Exception("Edge already exists.");
            }

            Vertices[source].OutEdges.Add(Vertices[dest]);
            Vertices[dest].InEdges.Add(Vertices[source]);
        }

        /// <summary>
        /// remove an existing edge between source & destination
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

            if (!Vertices[source].OutEdges.Contains(Vertices[dest]) 
                || !Vertices[dest].InEdges.Contains(Vertices[source]))
            {
                throw new Exception("Edge do not exists.");
            }

            Vertices[source].OutEdges.Remove(Vertices[dest]);
            Vertices[dest].InEdges.Remove(Vertices[source]);
        }

        /// <summary>
        /// do we have an edge between the given source and destination?
        /// O(1) complexity
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

            return Vertices[source].OutEdges.Contains(Vertices[dest]) 
                && Vertices[dest].InEdges.Contains(Vertices[source]);
        }

        public List<T> GetAllOutEdges(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return Vertices[vertex].OutEdges.Select(x=>x.Value).ToList();
        }

        public List<T> GetAllInEdges(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return Vertices[vertex].InEdges.Select(x => x.Value).ToList();
        }

        /// <summary>
        /// returns the vertex object with given value
        /// O(1) complexity
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DiGraphVertex<T> FindVertex(T value)
        {
            if (Vertices.ContainsKey(value))
            {
                return Vertices[value];
            }

            return null;
        }

        /// <summary>
        /// clones object
        /// </summary>
        /// <returns></returns>
        internal DiGraph<T> Clone()
        {
            var newGraph = new DiGraph<T>();

            foreach (var vertex in Vertices)
            {
                newGraph.AddVertex(vertex.Key);
            }

            foreach (var vertex in Vertices)
            {
                foreach (var edge in vertex.Value.OutEdges)
                {
                    newGraph.AddEdge(vertex.Value.Value, edge.Value);
                }
            }

            return newGraph;
        }
    }
}
