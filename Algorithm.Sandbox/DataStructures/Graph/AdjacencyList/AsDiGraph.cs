using Algorithm.Sandbox.DataStructures;
using System;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// Graph vertex
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsDiGraphVertex<T>

    {
        public T Value { get; set; }

        public AsHashSet<AsDiGraphVertex<T>> OutEdges { get; set; }
        public AsHashSet<AsDiGraphVertex<T>> InEdges { get; set; }

        public AsDiGraphVertex(T value)
        {
            Value = value;
            OutEdges = new AsHashSet<AsDiGraphVertex<T>>();
            InEdges = new AsHashSet<AsDiGraphVertex<T>>();
        }

    }

    /// <summary>
    /// A directed graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsDiGraph<T>
    {
        public int VerticesCount => Vertices.Count;
        internal AsDictionary<T, AsDiGraphVertex<T>> Vertices { get; set; }

        /// <summary>
        /// return a reference vertex
        /// </summary>
        public AsDiGraphVertex<T> ReferenceVertex
        {
            get
            {
                var enumerator = Vertices.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    return enumerator.Current.Value;
                }

                return null;
            }
        }


        public AsDiGraph()
        {
            Vertices = new AsDictionary<T, AsDiGraphVertex<T>>();
        }

        /// <summary>
        /// add a new vertex to this graph
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public AsDiGraphVertex<T> AddVertex(T value)
        {
            if ( value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new AsDiGraphVertex<T>(value);

            Vertices.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// remove an existing vertex frm graph
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
                if (!Vertices.ContainsKey(vertex.Value.Value))
                {
                    throw new Exception("Vertex incoming edge source vertex is not in this graph.");
                }
                vertex.Value.OutEdges.Remove(Vertices[value]);
            }

            foreach (var vertex in Vertices[value].OutEdges)
            {
                if (!Vertices.ContainsKey(vertex.Value.Value))
                {
                    throw new Exception("Vertex outgoing edge target vertex is not in this graph.");
                }

                vertex.Value.InEdges.Remove(Vertices[value]);
            }

            Vertices.Remove(value);
        }

        /// <summary>
        /// add an edge from source to destination vertex
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
                throw new Exception("Edge exists already partially or totally.");
            }

            Vertices[source].OutEdges.Add(Vertices[dest]);
            Vertices[dest].InEdges.Add(Vertices[source]);
        }

        /// <summary>
        /// remove an existing edge between source & destination
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
                throw new Exception("Edge do not exists partially or totally.");
            }

            Vertices[source].OutEdges.Remove(Vertices[dest]);
            Vertices[dest].InEdges.Remove(Vertices[source]);
        }

        /// <summary>
        /// do we have an edge between the given source and destination?
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

        /// <summary>
        /// returns the vertex object with given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public AsDiGraphVertex<T> FindVertex(T value)
        {
            if (Vertices.ContainsKey(value))
            {
                return Vertices[value];
            }

            return null;
        }
    }
}
