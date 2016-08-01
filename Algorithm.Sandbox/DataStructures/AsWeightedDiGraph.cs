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

        public AsSinglyLinkedList<AsWeightedDiGraphEdge<T, W>> OutEdges { get; set; }
        public AsSinglyLinkedList<AsWeightedDiGraphEdge<T, W>> InEdges { get; set; }
        
        public AsWeightedDiGraphVertex(T value)
        {
            this.Value = Value;

            OutEdges = new AsSinglyLinkedList<AsWeightedDiGraphEdge<T, W>>();
            InEdges = new AsSinglyLinkedList<AsWeightedDiGraphEdge<T, W>>();
        }

    }

    public class AsWeightedDiGraph<T, W> where W : IComparable
    {
        public AsWeightedDiGraphVertex<T, W> ReferenceVertex { get; set; }

    }
}
