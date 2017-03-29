using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsWeightedDiGraphEdge<T, W> : IComparable where W : IComparable
    {
        public W Weight { get; set; }
        public AsWeightedDiGraphVertex<T, W> Target { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
     
    public class AsWeightedDiGraphVertex<T, W> where W : IComparable
    {
        public T Value { get; private set; }

        public AsArrayList<AsWeightedDiGraphEdge<T, W>> OutEdges { get; set; }
        public AsArrayList<AsWeightedDiGraphEdge<T, W>> InEdges { get; set; }
        
        public AsWeightedDiGraphVertex(T value)
        {
            this.Value = Value;

            OutEdges = new AsArrayList<AsWeightedDiGraphEdge<T, W>>();
            InEdges = new AsArrayList<AsWeightedDiGraphEdge<T, W>>();
        }

    }

    public class AsWeightedDiGraph<T, W> where W : IComparable
    {
        public AsWeightedDiGraphVertex<T, W> ReferenceVertex { get; set; }

        public AsWeightedDiGraphVertex<T,W> AddVertex(T value,
          AsArrayList<AsWeightedDiGraphEdge<T, W>> inEdges, AsArrayList<AsWeightedDiGraphEdge<T, W>> outEdges)
        {
            if (inEdges == null || outEdges == null || value == null)
            {
                throw new ArgumentNullException();
            }

            if (inEdges.Length == 0 && outEdges.Length == 0)
            {
                throw new ArgumentException();
            }

            throw new NotImplementedException();
        }
        public void RemoveVertex(AsWeightedDiGraphVertex<T, W> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            throw new NotImplementedException();
        }

        public void AddEdge(AsWeightedDiGraphVertex<T,W> source, AsWeightedDiGraphVertex<T,W> dest, W weight)
        {

        }

        public void RemoveEdge(AsWeightedDiGraphVertex<T,W> source, AsWeightedDiGraphVertex<T,W> dest)
        {

        }
    }
}
