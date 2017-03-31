using System;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyList
{
    public class AsDiGraphVertex<T>

    {
        public T Value { get; set; }

        public AsHashSet<AsDiGraphVertex<T>> OutEdges { get; set; }
        public AsHashSet<AsDiGraphVertex<T>> InEdges { get; set; }

        public AsDiGraphVertex(T value)
        {
            this.Value = value;

            OutEdges = new AsHashSet<AsDiGraphVertex<T>>();
            InEdges = new AsHashSet<AsDiGraphVertex<T>>();
        }

    }

    public class AsDiGraph<T>
    {
        public int VerticesCount => Vertices.Count;
        internal AsHashSet<AsDiGraphVertex<T>> Vertices { get; set; }

        public AsDiGraph()
        {
            Vertices = new AsHashSet<AsDiGraphVertex<T>>();
        }

        public AsDiGraphVertex<T> AddVertex(T value)
        {
            if ( value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new AsDiGraphVertex<T>(value);

            Vertices.Add(newVertex);

            return newVertex;
        }
        public void RemoveVertex(AsDiGraphVertex<T> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!Vertices.Contains(value))
            {
                throw new Exception("Vertex not in this graph.");
            }

            foreach (var vertex in value.InEdges)
            {
                if (!Vertices.Contains(vertex.Value))
                {
                    throw new Exception("Vertex incoming edge source vertex is not in this graph.");
                }
                vertex.Value.OutEdges.Remove(vertex.Value);
            }

            foreach (var vertex in value.OutEdges)
            {
                if (!Vertices.Contains(vertex.Value))
                {
                    throw new Exception("Vertex outgoing edge target vertex is not in this graph.");
                }

                vertex.Value.InEdges.Remove(vertex.Value);
            }

            Vertices.Remove(value);
        }

        public void AddEdge(AsDiGraphVertex<T> source, AsDiGraphVertex<T> dest)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source) || !Vertices.Contains(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (source.OutEdges.Contains(dest) || dest.InEdges.Contains(source))
            {
                throw new Exception("Edge exists already partially or totally.");
            }

            source.OutEdges.Add(dest);
            dest.InEdges.Add(source);
        }

        public void RemoveEdge(AsDiGraphVertex<T> source, AsDiGraphVertex<T> dest)
        {

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source) || !Vertices.Contains(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!source.OutEdges.Contains(dest) || !dest.InEdges.Contains(source))
            {
                throw new Exception("Edge do not exists partially or totally.");
            }

            source.OutEdges.Remove(dest);
            dest.InEdges.Remove(source);
        }

        public bool HasEdge(AsDiGraphVertex<T> source, AsDiGraphVertex<T> dest)
        {
            return source.OutEdges.Contains(dest) && dest.InEdges.Contains(source);
        }

        
    }
}
