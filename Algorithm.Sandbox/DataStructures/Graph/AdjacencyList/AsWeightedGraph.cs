using System;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyList
{
    public class AsWeightedGraphEdge<T, W> : IComparable where W : IComparable
    {
        public W Weight { get; set; }
        public AsWeightedGraphVertex<T, W> Target { get; set; }

        public AsWeightedGraphEdge(AsWeightedGraphVertex<T, W> target)
        {
            Target = target;
        }

        public int CompareTo(object obj)
        {
            return Weight.CompareTo(obj as AsWeightedGraphEdge<T, W>);
        }
    }

    public class AsWeightedGraphVertex<T, W> where W : IComparable
    {
        public T Value { get; set; }

        public AsHashSet<AsWeightedGraphEdge<T, W>> Edges { get; set; }

        public AsWeightedGraphVertex(T value)
        {
            this.Value = value;

            Edges = new AsHashSet<AsWeightedGraphEdge<T, W>>();
        }

    }

    public class AsWeightedGraph<T, W> where W : IComparable
    {
        public int VerticesCount => Vertices.Count;
        internal AsHashSet<AsWeightedGraphVertex<T, W>> Vertices { get; set; }

        public AsWeightedGraph()
        {
            Vertices = new AsHashSet<AsWeightedGraphVertex<T, W>>();
        }
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
                if (!Vertices.Contains(vertex.Value.Target))
                {
                    throw new Exception("Vertex edge target is not in this graph.");
                }
                vertex.Value.Target.Edges.Remove(vertex.Value);
            }

            Vertices.Remove(value);
        }

        public void AddEdge(AsWeightedGraphEdge<T, W> source, AsWeightedGraphEdge<T, W> dest)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source.Target) || !Vertices.Contains(dest.Target))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (source.Target.Edges.Contains(dest) || dest.Target.Edges.Contains(source))
            {
                throw new Exception("Edge exists already partially or totally.");
            }

            source.Target.Edges.Add(dest);
            dest.Target.Edges.Add(source);
        }

        public void RemoveEdge(AsWeightedGraphEdge<T, W> source, AsWeightedGraphEdge<T, W> dest)
        {

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source.Target) || !Vertices.Contains(dest.Target))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!source.Target.Edges.Contains(dest) || !dest.Target.Edges.Contains(source))
            {
                throw new Exception("Edge do not exists partially or totally.");
            }

            source.Target.Edges.Remove(dest);
            dest.Target.Edges.Remove(source);
        }

        public bool HasEdge(AsWeightedGraphVertex<T, W> source, AsWeightedGraphVertex<T, W> dest)
        {
            var sourceExists = false;

            foreach (var edge in source.Edges)
            {
                if (edge.Value.Target == dest)
                {
                    sourceExists = true;
                    break;
                }
            }

            var destExists = false;

            foreach (var edge in dest.Edges)
            {
                if (edge.Value.Target == source)
                {
                    destExists = true;
                    break;
                }
            }

            return sourceExists && destExists;
        }
    }
}
