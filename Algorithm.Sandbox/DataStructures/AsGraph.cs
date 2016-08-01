namespace Algorithm.Sandbox.DataStructures
{
    public class AsGraphVertex<T>
    {
        public T identifier { get; set; }
        public AsSinglyLinkedList<AsGraphVertex<T>> Neighbours { get; set; }

        public AsGraphVertex(T identifier)
        {
            this.identifier = identifier;
            Neighbours = new AsSinglyLinkedList<AsGraphVertex<T>>();
        }

    }

    public class AsGraph<T>
    {
        public AsGraphVertex<T> ReferenceVertex { get; set; }

        public AsGraph(T referenceVertexIdentifier)
        {
            ReferenceVertex = new AsGraphVertex<T>(referenceVertexIdentifier);
        }
    }
}
