using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A weighted graph vertex
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class WeightedDiGraphVertex<T, W> where W : IComparable
    {
        public T Value { get; private set; }
        public Dictionary<WeightedDiGraphVertex<T, W>, W> OutEdges { get; set; }
        public Dictionary<WeightedDiGraphVertex<T, W>, W> InEdges { get; set; }

        public WeightedDiGraphVertex(T value)
        {
            this.Value = value;

            OutEdges = new Dictionary<WeightedDiGraphVertex<T, W>, W>();
            InEdges = new Dictionary<WeightedDiGraphVertex<T, W>, W>();
        }

    }

    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    /// <summary>
    /// A weighted graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class WeightedDiGraph<T, W> where W : IComparable
    {
        public int VerticesCount => Vertices.Count;
        internal Dictionary<T, WeightedDiGraphVertex<T, W>> Vertices { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WeightedDiGraph()
        {
            Vertices = new Dictionary<T, WeightedDiGraphVertex<T, W>>();
        }

        /// <summary>
        /// return a reference vertex  to start traversing Vertices
        /// O(1) complexity
        /// </summary>
        public WeightedDiGraphVertex<T, W> ReferenceVertex
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
        /// Add a new vertex to this graph
        /// O(1) complexity
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WeightedDiGraphVertex<T, W> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new WeightedDiGraphVertex<T, W>(value);

            Vertices.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// remove the given vertex
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
                vertex.Key.OutEdges.Remove(Vertices[value]);
            }

            foreach (var vertex in Vertices[value].OutEdges)
            {
                vertex.Key.InEdges.Remove(Vertices[value]);
            }

            Vertices.Remove(value);
        }

        /// <summary>
        /// Add a new edge to this graph
        /// O(1) complexity
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="weight"></param>
        public void AddEdge(T source, T dest, W weight)
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
        /// remove the given edge from this graph
        /// O(1) complexity
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

            if (!Vertices[source].OutEdges.ContainsKey(Vertices[dest]) 
                || !Vertices[dest].InEdges.ContainsKey(Vertices[source]))
            {
                throw new Exception("Edge do not exist.");
            }

            Vertices[source].OutEdges.Remove(Vertices[dest]);
            Vertices[dest].InEdges.Remove(Vertices[source]);
        }

        /// <summary>
        /// do we have an edge between given source and destination?
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

           return Vertices[source].OutEdges.ContainsKey(Vertices[dest])
             && Vertices[dest].InEdges.ContainsKey(Vertices[source]);
        }

        public List<Tuple<T, W>> GetAllOutEdges(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return Vertices[vertex].OutEdges.Select(x =>new Tuple<T,W>(x.Key.Value, x.Value)).ToList();
        }

        public List<Tuple<T, W>> GetAllInEdges(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            return Vertices[vertex].InEdges.Select(x => new Tuple<T, W>(x.Key.Value, x.Value)).ToList();
        }

        /// <summary>
        /// returns the vertex with given value
        /// O(1) complexity
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WeightedDiGraphVertex<T, W> FindVertex(T value)
        {
            if(Vertices.ContainsKey(value))
            {
                return Vertices[value];
            }

            return null;
        }
        
        /// <summary>
        /// clone object
        /// </summary>
        /// <returns></returns>
        internal WeightedDiGraph<T,W> Clone()
        {
            var newGraph = new WeightedDiGraph<T, W>();

            foreach(var vertex in Vertices)
            {
                newGraph.AddVertex(vertex.Key);
            }

            foreach(var vertex in Vertices)
            {
                foreach(var edge in vertex.Value.OutEdges)
                {
                    newGraph.AddEdge(vertex.Value.Value, edge.Key.Value, edge.Value);
                }
            }

            return newGraph;
        }
    }
}
