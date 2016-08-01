namespace Algorithm.Sandbox.DataStructures
{
    public class AsAVLTreeNode<I, V>
    {
        public I Identifier { get; set; }
        public V Value { get; set; }

        public AsAVLTree<I,V> Left { get; set; }
        public AsAVLTree<I,V> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsAVLTreeNode(I identifier, V value)
        {
            this.Identifier = identifier;
            this.Value = value;
        }

    }

    public class AsAVLTree<I, V>
    {
        public AsAVLTreeNode<I, V> Root { get; set; }

    }
}
