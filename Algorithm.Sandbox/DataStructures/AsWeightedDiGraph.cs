namespace Algorithm.Sandbox.DataStructures
{
    public class AsWeightedDiGraphEdge<T, U>
    {
        public U Weight { get; set; }
        public AsWeightedDiGraphVertex<T, U> Target { get; set; }
    }

    public class AsWeightedDiGraphVertex<T, U>
    {
        public T Identifier { get; set; }
        public AsSinglyLinkedList<AsWeightedDiGraphEdge<T, U>> OutEdges { get; set; }
        public AsSinglyLinkedList<AsWeightedDiGraphEdge<T, U>> InEdges { get; set; }

        public AsWeightedDiGraphVertex(T identifier)
        {
            this.Identifier = identifier;
            OutEdges = new AsSinglyLinkedList<AsWeightedDiGraphEdge<T, U>>();
            InEdges = new AsSinglyLinkedList<AsWeightedDiGraphEdge<T, U>>();
        }

    }

    public class AsWeightedDiGraph<T, U>
    {
        public AsWeightedDiGraphVertex<T, U> ReferenceVertex { get; set; }

        public AsWeightedDiGraph(T referenceNodeData)
        {
            ReferenceVertex = new AsWeightedDiGraphVertex<T, U>(referenceNodeData);
        }
    }
}
