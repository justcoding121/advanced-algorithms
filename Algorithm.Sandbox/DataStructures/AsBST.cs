namespace Algorithm.Sandbox.DataStructures
{
    public class AsBSTNode<I, V>
    {
        public I Identifier { get; set; }
        public V Value { get; set; }

        public AsBST<I, V> Left { get; set; }
        public AsBST<I, V> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBSTNode(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = value;
        }

    }

    public class AsBST<I, V>
    {
        public AsBSTNode<I, V> Root { get; set; }

    }
}
