namespace Algorithm.Sandbox.DataStructures
{
    public class AsTreapTreeNode<I, V>
    {
        public I Identifier { get; set; }
        public V Value { get; set; }

        public AsTreapTree<I, V> Left { get; set; }
        public AsTreapTree<I, V> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsTreapTreeNode(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = value;
        }

    }

    public class AsTreapTree<I, V>
    {
        public AsTreapTreeNode<I, V> Root { get; set; }

    }
}
