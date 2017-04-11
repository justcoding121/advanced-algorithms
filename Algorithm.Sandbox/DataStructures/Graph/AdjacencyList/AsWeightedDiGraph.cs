using System;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyList
{
    /// <summary>
    /// A weighted graph vertex
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class AsWeightedDiGraphVertex<T, W> where W : IComparable
    {
        public T Value { get; private set; }

        public AsDictionary<AsWeightedDiGraphVertex<T, W>, W> OutEdges { get; set; }
        public AsDictionary<AsWeightedDiGraphVertex<T, W>, W> InEdges { get; set; }

        public AsWeightedDiGraphVertex(T value)
        {
            this.Value = value;

            OutEdges = new AsDictionary<AsWeightedDiGraphVertex<T, W>, W>();
            InEdges = new AsDictionary<AsWeightedDiGraphVertex<T, W>, W>();
        }

    }

    /// <summary>
    /// A weighted graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class AsWeightedDiGraph<T, W> where W : IComparable
    {
        public int VerticesCount => Vertices.Count;
        internal AsDictionary<T, AsWeightedDiGraphVertex<T, W>> Vertices { get; set; }

        /// <summary>
        /// return a reference vertex
        /// </summary>
        public AsWeightedDiGraphVertex<T, W> ReferenceVertex
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


        public AsWeightedDiGraph()
        {
            Vertices = new AsDictionary<T, AsWeightedDiGraphVertex<T, W>>();
        }

        /// <summary>
        /// Add a new vertex to this graph
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public AsWeightedDiGraphVertex<T, W> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new AsWeightedDiGraphVertex<T, W>(value);

            Vertices.Add(value, newVertex);

            return newVertex;
        }

        /// <summary>
        /// remove the given vertex
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
                if (!Vertices.ContainsKey(vertex.Key.Value))
                {
                    throw new Exception("Vertex incoming edge source vertex is not in this graph.");
                }

                vertex.Key.OutEdges.Remove(Vertices[value]);
            }

            foreach (var vertex in Vertices[value].OutEdges)
            {
                if (!Vertices.ContainsKey(vertex.Key.Value))
                {
                    throw new Exception("Vertex outgoing edge target vertex is not in this graph.");
                }

                vertex.Key.InEdges.Remove(Vertices[value]);
            }

            Vertices.Remove(value);
        }

        /// <summary>
        /// Add a new edge to this graph
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
                throw new Exception("Edge exists already partially or totally.");
            }

            Vertices[source].OutEdges.Add(Vertices[dest], weight);
            Vertices[dest].InEdges.Add(Vertices[source], weight);
        }

        /// <summary>
        /// remove the given edge from this graph
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
                throw new Exception("Edge do not exists partially or totally.");
            }

            Vertices[source].OutEdges.Remove(Vertices[dest]);
            Vertices[dest].InEdges.Remove(Vertices[source]);
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

            var sourceExists = false;

            foreach (var edge in Vertices[source].OutEdges)
            {
                if (edge.Key == Vertices[dest])
                {
                    sourceExists = true;
                    break;
                }
            }

            var destExists = false;

            foreach (var edge in Vertices[dest].InEdges)
            {
                if (edge.Key == Vertices[source])
                {
                    destExists = true;
                    break;
                }
            }

            return sourceExists && destExists;
        }
     
        /// <summary>
        /// returns the vertex with given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public AsWeightedDiGraphVertex<T, W> FindVertex(T value)
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
        internal AsWeightedDiGraph<T,W> Clone()
        {
            var newGraph = new AsWeightedDiGraph<T, W>();

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
