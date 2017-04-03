using System;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyList
{
    public class AsWeightedDiGraphVertex<T, W> where W : IComparable
    {
        public T Value { get; private set; }

        public AsDictionary<AsWeightedDiGraphVertex<T, W>, W> OutEdges { get; set; }
        public AsDictionary<AsWeightedDiGraphVertex<T, W>, W> InEdges { get; set; }

        public AsWeightedDiGraphVertex(T value)
        {
            this.Value = Value;

            OutEdges = new AsDictionary<AsWeightedDiGraphVertex<T, W>, W>();
            InEdges = new AsDictionary<AsWeightedDiGraphVertex<T, W>, W>();
        }

    }

    public class AsWeightedDiGraph<T, W> where W : IComparable
    {
        public int VerticesCount => Vertices.Count;
        internal AsHashSet<AsWeightedDiGraphVertex<T, W>> Vertices { get; set; }

        public AsWeightedDiGraph()
        {
            Vertices = new AsHashSet<AsWeightedDiGraphVertex<T, W>>();
        }

        public AsWeightedDiGraphVertex<T, W> AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            var newVertex = new AsWeightedDiGraphVertex<T, W>(value);

            Vertices.Add(newVertex);

            return newVertex;
        }

        public void RemoveVertex(AsWeightedDiGraphVertex<T, W> value)
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
                if (!Vertices.Contains(vertex.Key))
                {
                    throw new Exception("Vertex incoming edge source vertex is not in this graph.");
                }

                vertex.Key.OutEdges.Remove(vertex.Key);
            }

            foreach (var vertex in value.OutEdges)
            {
                if (!Vertices.Contains(vertex.Key))
                {
                    throw new Exception("Vertex outgoing edge target vertex is not in this graph.");
                }

                vertex.Key.InEdges.Remove(vertex.Key);
            }

            Vertices.Remove(value);
        }

        public void AddEdge(AsWeightedDiGraphVertex<T, W> source,
            AsWeightedDiGraphVertex<T, W> dest, W weight)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source) || !Vertices.Contains(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (source.OutEdges.ContainsKey(dest) || dest.InEdges.ContainsKey(source))
            {
                throw new Exception("Edge exists already partially or totally.");
            }

            source.OutEdges.Add(dest, weight);
            dest.InEdges.Add(source, weight);
        }

        public void RemoveEdge(AsWeightedDiGraphVertex<T, W> source,
            AsWeightedDiGraphVertex<T, W> dest)
        {

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source) || !Vertices.Contains(dest))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!source.OutEdges.ContainsKey(dest) || !dest.InEdges.ContainsKey(source))
            {
                throw new Exception("Edge do not exists partially or totally.");
            }

            source.OutEdges.Remove(dest);
            dest.InEdges.Remove(source);
        }

        public bool HasEdge(AsWeightedDiGraphVertex<T, W> source,
            AsWeightedDiGraphVertex<T, W> dest)
        {
            var sourceExists = false;

            foreach (var edge in source.OutEdges)
            {
                if (edge.Key == dest)
                {
                    sourceExists = true;
                    break;
                }
            }

            var destExists = false;

            foreach (var edge in dest.InEdges)
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
