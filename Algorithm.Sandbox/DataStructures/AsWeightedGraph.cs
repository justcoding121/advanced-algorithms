using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsWeightedGraphEdge<I, V, W> where W : IComparable
    {
        public W Weight { get; set; }
        public AsWeightedGraphVertex<I, V, W> Target { get; set; }
    }

    public class AsWeightedGraphVertex<I, V, W> where W : IComparable
    {
        public I Identifier { get; set; }
        public V Value { get; set; }

        public AsSinglyLinkedList<AsWeightedGraphEdge<I, V, W>> Edges { get; set; }

        public AsWeightedGraphVertex(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = value;

            Edges = new AsSinglyLinkedList<AsWeightedGraphEdge<I, V, W>>();
        }

    }

    public class AsWeightedGraph<I, V, W> where W : IComparable
    {
        public AsWeightedGraphVertex<I, V, W> ReferenceVertex { get; set; }  
    }
}
