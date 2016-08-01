namespace Algorithm.Sandbox.DataStructures
{
    public class AsTreeNode<I, V>
    {
        public I Indentifier { get; set; }
        public V Value { get; set; }

        public AsSinglyLinkedList<AsTreeNode<I,V>> Children { get; set; }

        public bool IsLeaf => Children.Count() == 0;

        public AsTreeNode(I identifier, V value)
        {
            this.Indentifier = identifier;
            this.Value = value;

            Children = new AsSinglyLinkedList<AsTreeNode<I,V>>();
        } 

    }

    public class AsTree<I, V> 
    {
        public AsTreeNode<I,V> Root { get; set; }

    }
}
