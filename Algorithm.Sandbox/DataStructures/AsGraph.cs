namespace Algorithm.Sandbox.DataStructures
{
    public class AsGraphVertex<I, V>
    {
        public I Identifier { get; set; }
        public V Value { get; set; }

        public AsSinglyLinkedList<AsGraphVertex<I, V>> Vertices { get; set; }

        public AsGraphVertex(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = value;

            Vertices = new AsSinglyLinkedList<AsGraphVertex<I, V>>();
        }

    }

    public class AsGraph<I, V>
    {
        public AsGraphVertex<I, V> ReferenceVertex { get; set; }

    }
}
