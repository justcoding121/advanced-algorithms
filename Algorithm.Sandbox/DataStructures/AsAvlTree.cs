namespace Algorithm.Sandbox.DataStructures
{
    public class AsAVLTreeNode<T>
    {
        public T data { get; set; }

        public AsAVLTree<T> Left { get; set; }
        public AsAVLTree<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsAVLTreeNode(T data)
        {
            this.data = data;
        }

    }

    public class AsAVLTree<T>
    {
        public AsAVLTreeNode<T> Root { get; set; }

        public AsAVLTree(T rootData)
        {
            Root = new AsAVLTreeNode<T>(rootData);
        }

    }
}
