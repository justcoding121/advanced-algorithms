namespace Algorithm.Sandbox.DataStructures
{
    public class AsWeightedGraphEdge<T, U>
    {
        public U Weight { get; set; }
        public AsWeightedGraphVertex<T, U> Target { get; set; }
    }

    public class AsWeightedGraphVertex<T, U>
    {
        public T Identifier { get; set; }
        public AsSinglyLinkedList<AsWeightedGraphEdge<T, U>> Edges { get; set; }

        public AsWeightedGraphVertex(T identifier)
        {
            this.Identifier = identifier;
            Edges = new AsSinglyLinkedList<AsWeightedGraphEdge<T, U>>();
        }

    }

    public class AsWeightedGraph<T, U>
    {
        public AsWeightedGraphVertex<T, U> ReferenceVertex { get; set; }

        public AsWeightedGraph(T referenceNodeData)
        {
            ReferenceVertex = new AsWeightedGraphVertex<T, U>(referenceNodeData);
        }
    }
}
