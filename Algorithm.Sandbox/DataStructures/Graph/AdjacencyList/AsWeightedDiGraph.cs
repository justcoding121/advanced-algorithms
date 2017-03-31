using System;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyList
{
    public class AsWeightedDiGraphEdge<T, W> : IComparable where W : IComparable
    {
        public W Weight { get; set; }
        public AsWeightedDiGraphVertex<T, W> Target { get; set; }

        public AsWeightedDiGraphEdge(AsWeightedDiGraphVertex<T, W> target)
        {
            Target = target;
        }

        public int CompareTo(object obj)
        {
            return Weight.CompareTo(obj as AsWeightedDiGraphEdge<T, W>);
        }
    }

    public class AsWeightedDiGraphVertex<T, W> where W : IComparable
    {
        public T Value { get; private set; }

        public AsHashSet<AsWeightedDiGraphEdge<T, W>> OutEdges { get; set; }
        public AsHashSet<AsWeightedDiGraphEdge<T, W>> InEdges { get; set; }

        public AsWeightedDiGraphVertex(T value)
        {
            this.Value = Value;

            OutEdges = new AsHashSet<AsWeightedDiGraphEdge<T, W>>();
            InEdges = new AsHashSet<AsWeightedDiGraphEdge<T, W>>();
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
                if (!Vertices.Contains(vertex.Value.Target))
                {
                    throw new Exception("Vertex incoming edge source vertex is not in this graph.");
                }
                vertex.Value.Target.OutEdges.Remove(vertex.Value);
            }

            foreach (var vertex in value.OutEdges)
            {
                if (!Vertices.Contains(vertex.Value.Target))
                {
                    throw new Exception("Vertex outgoing edge target vertex is not in this graph.");
                }

                vertex.Value.Target.InEdges.Remove(vertex.Value);
            }

            Vertices.Remove(value);
        }

        public void AddEdge(AsWeightedDiGraphEdge<T, W> source, AsWeightedDiGraphEdge<T, W> dest)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source.Target) || !Vertices.Contains(dest.Target))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (source.Target.OutEdges.Contains(dest) || dest.Target.InEdges.Contains(source))
            {
                throw new Exception("Edge exists already partially or totally.");
            }

            source.Target.OutEdges.Add(dest);
            dest.Target.InEdges.Add(source);
        }

        public void RemoveEdge(AsWeightedDiGraphEdge<T, W> source, AsWeightedDiGraphEdge<T, W> dest)
        {

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!Vertices.Contains(source.Target) || !Vertices.Contains(dest.Target))
            {
                throw new Exception("Source or Destination Vertex is not in this graph.");
            }

            if (!source.Target.OutEdges.Contains(dest) || !dest.Target.InEdges.Contains(source))
            {
                throw new Exception("Edge do not exists partially or totally.");
            }

            source.Target.OutEdges.Remove(dest);
            dest.Target.InEdges.Remove(source);
        }

        public bool HasEdge(AsWeightedDiGraphVertex<T, W> source, AsWeightedDiGraphVertex<T, W> dest)
        {
            var sourceExists = false;

            foreach(var edge in source.OutEdges)
            {
                if(edge.Value.Target == dest)
                {
                    sourceExists = true;
                    break;
                }
            }

            var destExists = false;

            foreach (var edge in dest.InEdges)
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
