using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsWeightedDiGraphEdge<I, V, W> where W : IComparable
    {
        public W Weight { get; set; }
        public AsWeightedDiGraphVertex<I, V, W> Target { get; set; }
    }
     
    public class AsWeightedDiGraphVertex<I, V, W> where W : IComparable
    {
        public I Identifier { get; set; }
        public V Value { get; private set; }

        public AsSinglyLinkedList<AsWeightedDiGraphEdge<I, V, W>> OutEdges { get; set; }
        public AsSinglyLinkedList<AsWeightedDiGraphEdge<I, V, W>> InEdges { get; set; }
        
        public AsWeightedDiGraphVertex(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = Value;

            OutEdges = new AsSinglyLinkedList<AsWeightedDiGraphEdge<I, V, W>>();
            InEdges = new AsSinglyLinkedList<AsWeightedDiGraphEdge<I, V, W>>();
        }

    }

    public class AsWeightedDiGraph<I, V, W> where W : IComparable
    {
        public AsWeightedDiGraphVertex<I, V, W> ReferenceVertex { get; set; }

    }
}
