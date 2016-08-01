namespace Algorithm.Sandbox.DataStructures
{
    public class AsDiGraphVertex<I,V>
    {
        public I Identifier { get; set; }
        public V Value { get; set; }

        public AsSinglyLinkedList<AsDiGraphVertex<I,V>> OutVertices { get; set; }
        public AsSinglyLinkedList<AsDiGraphVertex<I,V>> InVertices { get; set; }

        public AsDiGraphVertex(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = value;

            OutVertices = new AsSinglyLinkedList<AsDiGraphVertex<I,V>>();
            InVertices = new AsSinglyLinkedList<AsDiGraphVertex<I, V>>();
        }

    }

    public class AsDiGraph<I, V>
    {
        public AsDiGraphVertex<I, V> ReferenceVertex { get; set; }

    }
}
