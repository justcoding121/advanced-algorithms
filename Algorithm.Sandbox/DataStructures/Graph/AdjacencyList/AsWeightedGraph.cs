using System;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A weighted graph vertex
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class AsWeightedGraphVertex<T, W> where W : IComparable
    {
        public T Value { get; private set; }

        public AsDictionary<AsWeightedGraphVertex<T, W>, W> Edges { get; set; }

        public AsWeightedGraphVertex(T value)
        {
            this.Value = Value;

            Edges = new AsDictionary<AsWeightedGraphVertex<T, W>, W>();

        }

    }

    /// <summary>
    /// A weighted graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class AsWeightedGraph<T, W> where W : IComparable
    {
        public int VerticesCount => Vertices.Count;
        internal AsHashSet<AsWeightedGraphVertex<T, W>> Vertices { get; set; }

        public AsWeightedGraph()
        {
            Vertices = new AsHashSet<AsWeightedGraphVertex<T, W>>();
        }

        /// <summary>
        /// Add a new vertex to this graph
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public AsWeightedGraphVertex<T, W> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new AsWeightedGraphVertex<T, W>(value);

            Vertices.Add(newVertex);

            return newVertex;
        }

        /// <summary>
        /// remove given vertex from this graph
        /// </summary>
        /// <param name="value"></param>
        public void RemoveVertex(AsWeightedGraphVertex<T, W> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!Vertices.Contains(value))
            {
                throw new Exception("Vertex not in this graph.");
            }


            foreach (var vertex in value.Edges)
            {
                if (!Vertices.Contains(vertex.Key))
                {
                    throw new Exception("Vertex outgoing edge target vertex is not in this graph.");
                }

                vertex.Key.Edges.Remove(vertex.Key);
            }

            Vertices.Remove(value);
        }

        /// <summary>
        /// Add a new edge to this graph with given weight 
        /// and between given source and destination vertex
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="weight"></param>
        public void AddEdge(AsWeightedGraphVertex<T, W> source,
            AsWeightedGraphVertex<T, W> dest, W weight)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source) || !Vertices.Contains(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }


            source.Edges.Add(dest, weight);
            dest.Edges.Add(source, weight);
        }

        /// <summary>
        /// Remove given edge
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public void RemoveEdge(AsWeightedGraphVertex<T, W> source,
            AsWeightedGraphVertex<T, W> dest)
        {

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source) || !Vertices.Contains(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!source.Edges.ContainsKey(dest) || !dest.Edges.ContainsKey(source))
            {
                throw new Exception("Edge do not exists partially or totally.");
            }

            source.Edges.Remove(dest);
            dest.Edges.Remove(source);
        }

        /// <summary>
        /// Do we have an edge between given source and destination
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public bool HasEdge(AsWeightedGraphVertex<T, W> source,
            AsWeightedGraphVertex<T, W> dest)
        {
            var sourceExists = false;

            foreach (var edge in source.Edges)
            {
                if (edge.Key == dest)
                {
                    sourceExists = true;
                    break;
                }
            }

            var destExists = false;

            foreach (var edge in dest.Edges)
            {
                if (edge.Key == source)
                {
                    destExists = true;
                    break;
                }
            }

            return sourceExists && destExists;
        }

    }
}
