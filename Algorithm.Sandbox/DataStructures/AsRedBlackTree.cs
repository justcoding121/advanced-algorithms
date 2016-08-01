namespace Algorithm.Sandbox.DataStructures
{
    public class AsRedBlackTreeNode<I, V>
    {
        public I Identifier { get; set; }
        public V Value { get; set; }

        public AsRedBlackTree<I, V> Left { get; set; }
        public AsRedBlackTree<I, V> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsRedBlackTreeNode(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = value;
        }

    }

    public class AsRedBlackTree<I, V>
    {
        public AsRedBlackTreeNode<I, V> Root { get; set; }

    }
}
