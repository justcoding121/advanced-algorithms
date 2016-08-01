using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsWeightedGraphEdge<T, W> : IComparable where W : IComparable
    {
        public W Weight { get; set; }
        public AsWeightedGraphVertex<T, W> Target { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class AsWeightedGraphVertex<T, W> where W : IComparable
    {
        public T Value { get; set; }

        public AsSinglyLinkedList<AsWeightedGraphEdge<T, W>> Edges { get; set; }

        public AsWeightedGraphVertex(T value)
        {
            this.Value = value;

            Edges = new AsSinglyLinkedList<AsWeightedGraphEdge<T, W>>();
        }

    }

    public class AsWeightedGraph<T, W> where W : IComparable
    {
        public AsWeightedGraphVertex<T, W> ReferenceVertex { get; set; }  
    }
}
