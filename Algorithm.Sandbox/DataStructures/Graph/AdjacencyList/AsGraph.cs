using System;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyList
{
    public class AsGraphVertex<T> 
    {
        public T Value { get; set; }

        public AsHashSet<AsGraphVertex<T>> Edges { get; set; }

        public AsGraphVertex(T value)
        {
            this.Value = value;

            Edges = new AsHashSet<AsGraphVertex<T>>();
        }

    }

    public class AsGraph<T>
    {
        public int VerticesCount => Vertices.Count;
        internal AsHashSet<AsGraphVertex<T>> Vertices { get; set; }

        public AsGraph()
        {
            Vertices = new AsHashSet<AsGraphVertex<T>>();
        }

        public AsGraphVertex<T> AddVertex(T value)
        {
            if (value  == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new AsGraphVertex<T>(value);
  
            Vertices.Add(newVertex);

            return newVertex;
        }
        public void RemoveVertex(AsGraphVertex<T> vertex)
        {
            if (vertex == null)
            {
                throw new ArgumentNullException();
            }

            if (!Vertices.Contains(vertex))
            {
                throw new Exception("Vertex not in this graph.");
            }

            foreach (var v in vertex.Edges)
            {
                if (!Vertices.Contains(v.Value))
                {
                    throw new Exception("Vertex edge is not in this graph.");
                }

                v.Value.Edges.Remove(vertex);
            }

            Vertices.Remove(vertex);
        }

        public void AddEdge(AsGraphVertex<T> source, AsGraphVertex<T> dest)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source) || !Vertices.Contains(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (source.Edges.Contains(dest) || dest.Edges.Contains(source))
            {
                throw new Exception("Edge exists already partially or totally.");
            }

            source.Edges.Add(dest);
            dest.Edges.Add(source);
        }

        public void RemoveEdge(AsGraphVertex<T> source, AsGraphVertex<T> dest)
        {

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source) || !Vertices.Contains(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!source.Edges.Contains(dest) || !dest.Edges.Contains(source))
            {
                throw new Exception("Edge do not exists partially or totally.");
            }

            source.Edges.Remove(dest);
            dest.Edges.Remove(source);
        }

        public bool HasEdge(AsGraphVertex<T> source, AsGraphVertex<T> dest)
        {
            return source.Edges.Contains(dest) && dest.Edges.Contains(source);
        }

    }
}
